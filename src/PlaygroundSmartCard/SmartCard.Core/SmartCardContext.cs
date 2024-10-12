using System;
using SmartCard.Core.Internal;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a smart card context.
    /// </summary>
    public class SmartCardContext
    {
        #region Property(s)

        /// <summary>
        /// Gets the scope of the smart card context.
        /// </summary>
        public SmartCardScope Scope { get; }

        /// <summary>
        /// Gets or sets the handle to the smart card context.
        /// </summary>
        public IntPtr Context { get; private set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardContext"/> class with the specified scope.
        /// </summary>
        /// <param name="scope">The scope of the smart card context.</param>
        public SmartCardContext(SmartCardScope scope)
        {
            Scope = scope;
            Context = IntPtr.Zero;
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Establishes a connection to the smart card resource manager.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> indicating the result of the operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the scope value is not supported.</exception>
        public SmartCardResult Establish()
        {
            uint scope;
            switch (Scope)
            {
                case SmartCardScope.User:
                    scope = WinSCardScope.SCARD_SCOPE_USER;
                    break;
                case SmartCardScope.Terminal:
                    scope = WinSCardScope.SCARD_SCOPE_TERMINAL;
                    break;
                case SmartCardScope.System:
                    scope = WinSCardScope.SCARD_SCOPE_SYSTEM;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Scope), Scope, "This scope value is not supported.");
            }

            var hContext = IntPtr.Zero;
            var result = WinSCardAPI.SCardEstablishContext(scope, IntPtr.Zero, IntPtr.Zero, ref hContext);
            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResultHelper.CreateErrorResult(result);
            }

            Context = hContext;

            return SmartCardResultHelper.CreateSuccessResult();
        }

        /// <summary>
        /// Releases the connection to the smart card resource manager.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> indicating the result of the operation.</returns>
        public SmartCardResult Release()
        {
            if (Context != IntPtr.Zero)
            {
                var result = WinSCardAPI.SCardReleaseContext(Context);
                if (result != WinSCardError.SCARD_S_SUCCESS)
                {
                    return SmartCardResultHelper.CreateErrorResult(result);
                }

                Context = IntPtr.Zero;
            }

            return SmartCardResultHelper.CreateSuccessResult();
        }

        #endregion
    }
}