﻿namespace Sdl
{
    /// <summary>
    /// Flags for a message box.
    /// </summary>
    public enum MessageBoxType
    {
        /// <summary>
        /// Error message box.
        /// </summary>
        Error = 0x00000010,

        /// <summary>
        /// Warning message box.
        /// </summary>
        Warning = 0x00000020,

        /// <summary>
        /// Information message box.
        /// </summary>
        Information = 0x00000040,

        /// <summary>
        /// Buttons placed left to right.
        /// </summary>
        ButtonsLeftToRight = 0x00000080,

        /// <summary>
        /// Buttons placed right to left.
        /// </summary>
        ButtonsRightToLeft = 0x00000100,
    }
}
