namespace Sdl.Touch
{
    /// <summary>
    /// An instance of a haptic effect.
    /// </summary>
    public sealed unsafe class HapticEffectInstance : NativeIndexBase<Native.SDL_Haptic, int, HapticEffectInstance>
    {
        /// <summary>
        /// Whether the effect instance is currently running.
        /// </summary>
        public bool EffectRunning =>
            Sdl.Native.CheckErrorBool(Sdl.Native.SDL_HapticGetEffectStatus(Native, Index));

        /// <summary>
        /// Updates the effect instance.
        /// </summary>
        /// <param name="effect">The effect to update to.</param>
        public void UpdateEffect(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            _ = Sdl.Native.CheckError(Sdl.Native.SDL_HapticUpdateEffect(Native, Index, in nativeEffect));
        }

        /// <summary>
        /// Runs the effect.
        /// </summary>
        /// <param name="iterations">The number of times to run the effect.</param>
        public void RunEffect(uint iterations) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticRunEffect(Native, Index, iterations));

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public void StopEffect() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticStopEffect(Native, Index));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_HapticDestroyEffect(Native, Index);
            base.Dispose();
        }
    }
}
