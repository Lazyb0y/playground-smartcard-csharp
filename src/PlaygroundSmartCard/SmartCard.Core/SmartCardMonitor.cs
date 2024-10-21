using System;
using System.Collections.Concurrent;
using System.Threading;
using SmartCard.Core.EventArgs;
using SmartCard.Core.WinSCard;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a monitor for smart card readers.
    /// </summary>
    public class SmartCardMonitor : IDisposable
    {
        #region Declaration(s)

        private bool _disposed;
        private readonly SmartCardContext _context;
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _readerTokens;

        #endregion

        #region Singleton

        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<SmartCardMonitor> instance =
            new Lazy<SmartCardMonitor>(() => new SmartCardMonitor());

        /// <summary>
        /// Gets the singleton instance of the <see cref="SmartCardMonitor"/> class.
        /// </summary>
        public static SmartCardMonitor Instance => instance.Value;

        #endregion

        #region Event(s)

        /// <summary>
        /// Occurs when the card status changes.
        /// </summary>
        public event EventHandler<CardStatusChangedEventArgs> CardStatusChanged;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardMonitor"/> class with the scope <see cref="SmartCardScope.System"/>.
        /// </summary>
        private SmartCardMonitor()
        {
            _context = new SmartCardContext(SmartCardScope.System);

            var result = _context.Establish();
            if (!result.Success)
            {
                throw new SmartCardException(result.ErrorMessage, result.ErrorCode);
            }

            _readerTokens = new ConcurrentDictionary<string, CancellationTokenSource>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SmartCardMonitor"/> class.
        /// </summary>
        ~SmartCardMonitor()
        {
            Dispose(false);
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Starts monitoring a smart card reader for card status changes.
        /// </summary>
        /// <param name="readerName">The name of the smart card reader.</param>
        /// <returns><c>true</c> if monitoring started successfully; otherwise, <c>false</c>.</returns>
        public bool StartMonitoring(string readerName)
        {
            if (_readerTokens.ContainsKey(readerName))
            {
                return true;
            }

            var tokenSource = new CancellationTokenSource();
            if (_readerTokens.TryAdd(readerName, tokenSource))
            {
                var token = tokenSource.Token;
                var thread = new Thread(() => MonitorReader(readerName, token));
                thread.Start();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Stops monitoring a smart card reader for card status changes.
        /// </summary>
        /// <param name="readerName">The name of the smart card reader.</param>
        public void StopMonitoring(string readerName)
        {
            if (_readerTokens.TryGetValue(readerName, out var tokenSource))
            {
                tokenSource.Cancel();
            }
        }

        /// <summary>
        /// Stops monitoring all smart card readers for card status changes.
        /// </summary>
        public void StopAllMonitoring()
        {
            foreach (var reader in _readerTokens.Keys)
            {
                if (_readerTokens.TryGetValue(reader, out var tokenSource))
                {
                    tokenSource.Cancel();
                }
            }
        }

        /// <summary>
        /// Monitors a smart card reader for card status changes.
        /// </summary>
        /// <param name="readerName">The name of the smart card reader.</param>
        /// <param name="token">The cancellation token.</param>
        private void MonitorReader(string readerName, CancellationToken token)
        {
            WinSCardReaderState[] readerStates =
            {
                new WinSCardReaderState
                {
                    ReaderName = readerName,
                    CurrentState = WinSCardState.SCARD_STATE_UNAWARE
                }
            };

            try
            {
                while (!token.IsCancellationRequested)
                {
                    var result = WinSCardAPI.SCardGetStatusChange(
                        _context.Context,
                        1000,
                        readerStates,
                        (uint)readerStates.Length
                    );

                    if (result == 0)
                    {
                        for (var i = 0; i < readerStates.Length; i++)
                        {
                            if ((readerStates[i].EventState & WinSCardState.SCARD_STATE_CHANGED) == WinSCardState.SCARD_STATE_CHANGED)
                            {
                                SmartCardStatus? status = null;

                                if ((readerStates[i].EventState & WinSCardState.SCARD_STATE_PRESENT) == WinSCardState.SCARD_STATE_PRESENT &&
                                    (readerStates[i].CurrentState & WinSCardState.SCARD_STATE_PRESENT) != WinSCardState.SCARD_STATE_PRESENT)
                                {
                                    status = SmartCardStatus.Inserted;
                                }
                                else if ((readerStates[i].EventState & WinSCardState.SCARD_STATE_EMPTY) == WinSCardState.SCARD_STATE_EMPTY &&
                                         (readerStates[i].CurrentState & WinSCardState.SCARD_STATE_EMPTY) != WinSCardState.SCARD_STATE_EMPTY)
                                {
                                    status = SmartCardStatus.Ejected;
                                }

                                if (status.HasValue && readerStates[i].CurrentState != WinSCardState.SCARD_STATE_UNAWARE)
                                {
                                    OnCardStatusChanged(new CardStatusChangedEventArgs(readerName, status.Value));
                                }

                                readerStates[i].CurrentState = readerStates[i].EventState;
                            }
                        }
                    }
                }
            }
            finally
            {
                _readerTokens.TryRemove(readerName, out _);
                Console.WriteLine($"Monitoring stopped for reader: {readerName}");
            }
        }

        /// <summary>
        /// Raises the <see cref="CardStatusChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnCardStatusChanged(CardStatusChangedEventArgs e)
        {
            var handler = CardStatusChanged;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopAllMonitoring();
                }

                try
                {
                    var result = _context.Release();
                    if (!result.Success)
                    {
                        Console.WriteLine(
                            $"An error occurred during disposal of context: [{result.ErrorCode}] {result.ErrorMessage}");
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine($"An error occurred during disposal: {x}");
                }

                _disposed = true;
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}