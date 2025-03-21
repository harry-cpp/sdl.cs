﻿namespace Sdl.Graphics
{
    /// <summary>
    /// The default window shape.
    /// </summary>
    public sealed class DefaultWindowShapeMode : WindowShapeMode
    {
        internal override Native.SDL_WindowShapeMode ToNative() =>
            new(Native.WindowShapeMode.Default);
    }
}
