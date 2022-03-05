namespace Sdl.Touch
{
    /// <summary>
    /// A description of a haptic effect.
    /// </summary>
    public abstract class HapticEffect
    {
        internal abstract Native.SDL_HapticEffect ToNative();
    }
}
