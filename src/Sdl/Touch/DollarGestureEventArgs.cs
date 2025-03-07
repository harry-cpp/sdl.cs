﻿namespace Sdl.Touch
{
    /// <summary>
    /// Event arguments for dollar gesture events.
    /// </summary>
    public sealed class DollarGestureEventArgs : SdlEventArgs
    {
        private readonly Native.SDL_GestureID _gestureId;
        private readonly Native.SDL_TouchID _deviceIndex;

        /// <summary>
        /// The touch device that had the gesture.
        /// </summary>
        public TouchDevice Device => TouchDevice.IndexToInstance(_deviceIndex);

        /// <summary>
        /// The gesture.
        /// </summary>
        public Gesture Gesture => Gesture.IndexToInstance(_gestureId);

        /// <summary>
        /// The number of fingers involed.
        /// </summary>
        public uint FingerCount { get; }

        /// <summary>
        /// The difference between the gesture template and the actual performed gesture.
        /// </summary>
        public float Error { get; }

        /// <summary>
        /// The center of the gesture.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The center of the gesture.
        /// </summary>
        public float Y { get; }

        internal DollarGestureEventArgs(Native.SDL_DollarGestureEvent dollarGesture) : base(dollarGesture.Timestamp)
        {
            _deviceIndex = dollarGesture.TouchId;
            _gestureId = dollarGesture.GestureId;
            FingerCount = dollarGesture.NumFingers;
            Error = dollarGesture.Error;
            X = dollarGesture.X;
            Y = dollarGesture.Y;
        }
    }
}
