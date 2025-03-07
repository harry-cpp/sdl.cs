﻿using Sdl.Graphics;

namespace Sdl.Input
{
    /// <summary>
    /// Event arguments for mouse motion events.
    /// </summary>
    public sealed class MouseMotionEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => Window.Get(_windowId);

        /// <summary>
        /// The event comes from touch rather than a mouse.
        /// </summary>
        public bool IsTouch { get; }

        /// <summary>
        /// The state of the buttons.
        /// </summary>
        public MouseButton Buttons { get; }

        /// <summary>
        /// The location of the event.
        /// </summary>
        public Point Location { get; }

        /// <summary>
        /// The relative location of the event.
        /// </summary>
        public Point RelativeLocation { get; }

        internal MouseMotionEventArgs(Native.SDL_MouseMotionEvent motion) : base(motion.Timestamp)
        {
            _windowId = motion.WindowId;
            IsTouch = motion.Which == uint.MaxValue;
            Buttons = (MouseButton)motion.State;
            Location = (motion.X, motion.Y);
            RelativeLocation = (motion.XRel, motion.YRel);
        }
    }
}
