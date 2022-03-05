﻿namespace Sdl.Graphics
{
    /// <summary>
    /// Event arguments for a window event that has a position.
    /// </summary>
    public sealed class LocationEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The location of the event.
        /// </summary>
        public Point Location { get; }

        internal LocationEventArgs(Native.SDL_WindowEvent e) : base(e.Timestamp)
        {
            Location = new Point(e.Data1, e.Data2);
        }
    }
}
