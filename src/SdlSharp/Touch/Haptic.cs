using System.Collections.Generic;

using Sdl.Touch;

namespace Sdl
{
    /// <summary>
    /// A class that represents a haptic effect.
    /// </summary>
    public sealed unsafe class Haptic : NativePointerBase<Native.SDL_Haptic, Haptic>
    {
        private static ItemCollection<HapticInfo>? s_hapticInfos;

        /// <summary>
        /// An infinity value.
        /// </summary>
        public const uint Infinity = uint.MaxValue;

        /// <summary>
        /// The current haptic effects.
        /// </summary>
        public static IReadOnlyCollection<HapticInfo> Haptics => s_hapticInfos ??= new ItemCollection<HapticInfo>(
            index => Sdl.Native.CheckNotNull(HapticInfo.IndexToInstance(index)),
            Sdl.Native.SDL_NumHaptics);

        /// <summary>
        /// Information about this effect.
        /// </summary>
        public HapticInfo Info =>
            HapticInfo.IndexToInstance(Sdl.Native.SDL_HapticIndex(Native));

        /// <summary>
        /// The number of effects that can be stored.
        /// </summary>
        public int MaxEffects =>
            Sdl.Native.SDL_HapticNumEffects(Native);

        /// <summary>
        /// The effects currently playing.
        /// </summary>
        public int EffectsPlaying =>
            Sdl.Native.SDL_HapticNumEffectsPlaying(Native);

        /// <summary>
        /// The capabilities of the haptic device.
        /// </summary>
        public HapticCapabilities Capabilities =>
            (HapticCapabilities)Sdl.Native.SDL_HapticQuery(Native) & HapticCapabilities.All;

        /// <summary>
        /// The number of axes.
        /// </summary>
        public int AxisCount =>
            Sdl.Native.SDL_HapticNumAxes(Native);

        /// <summary>
        /// Whether rumble is supported.
        /// </summary>
        public bool RumbleSupported => Sdl.Native.CheckErrorBool(Sdl.Native.SDL_HapticRumbleSupported(Native));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_HapticClose(Native);
            base.Dispose();
        }

        /// <summary>
        /// Determines whether the effect is supported.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>Whether the effect is supported.</returns>
        public bool EffectSupported(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            return Sdl.Native.SDL_HapticEffectSupported(Native, in nativeEffect);
        }

        /// <summary>
        /// Creates a new instance of a haptic effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The effect instance.</returns>
        public HapticEffectInstance NewEffect(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            return HapticEffectInstance.IndexToInstance(Native, Sdl.Native.CheckError(Sdl.Native.SDL_HapticNewEffect(Native, in nativeEffect)));
        }

        /// <summary>
        /// Sets the gain.
        /// </summary>
        /// <param name="gain">The gain amount.</param>
        public void SetGain(int gain) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticSetGain(Native, gain));

        /// <summary>
        /// Sets the global autocenter of the device.
        /// </summary>
        /// <param name="autocenter">The autocenter value.</param>
        public void SetAutocenter(int autocenter) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticSetAutocenter(Native, autocenter));

        /// <summary>
        /// Pauses the effect.
        /// </summary>
        public void Pause() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticPause(Native));

        /// <summary>
        /// Unpauses the effect.
        /// </summary>
        public void Unpause() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticUnpause(Native));

        /// <summary>
        /// Stops all effect instances.
        /// </summary>
        public void StopAll() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticStopAll(Native));

        /// <summary>
        /// Initializes rumble support.
        /// </summary>
        public void RumbleInit() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticRumbleInit(Native));

        /// <summary>
        /// Plays a rumble effect.
        /// </summary>
        /// <param name="strength">The strength of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        public void RumblePlay(float strength, uint length) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticRumblePlay(Native, strength, length));

        /// <summary>
        /// Stops the rumble effect.
        /// </summary>
        public void RumbleStop() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_HapticRumbleStop(Native));
    }
}
