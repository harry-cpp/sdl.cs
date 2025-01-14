﻿using Sdl.Graphics;

namespace Sdl
{
    /// <summary>
    /// Event arguments for a drop event.
    /// </summary>
    public class DropEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window being dropped on, if any. 
        /// </summary>
        public Window? Window => _windowId == 0 ? null : Window.Get(_windowId);

        internal DropEventArgs(Native.SDL_DropEvent drop) : base(drop.Timestamp)
        {
            _windowId = drop.WindowId;
        }
    }
}
