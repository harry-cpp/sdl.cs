﻿namespace Sdl.Touch
{
    /// <summary>
    /// A custom haptic effect.
    /// </summary>
    public sealed class CustomHapticEffect : HapticEffect
    {
        /// <summary>
        /// The direction of the effect.
        /// </summary>
        public HapticDirection Direction { get; }

        /// <summary>
        /// The length of the effect.
        /// </summary>
        public uint Length { get; }

        /// <summary>
        /// The delay before the effect.
        /// </summary>
        public ushort Delay { get; }

        /// <summary>
        /// The button that triggers the effect.
        /// </summary>
        public ushort Button { get; }

        /// <summary>
        /// Minimum interval between effects.
        /// </summary>
        public ushort Interval { get; }

        /// <summary>
        /// Axes to use.
        /// </summary>
        public byte Channels { get; }

        /// <summary>
        /// Sample periods.
        /// </summary>
        public ushort Period { get; }

        /// <summary>
        /// Amount of samples.
        /// </summary>
        public ushort Samples { get; }

        /// <summary>
        /// The samples.
        /// </summary>
        public ushort[] Data { get; }

        /// <summary>
        /// The attach length of the effect.
        /// </summary>
        public ushort AttackLength { get; }

        /// <summary>
        /// The attack level of the effect.
        /// </summary>
        public ushort AttackLevel { get; }

        /// <summary>
        /// The fade length of the effect.
        /// </summary>
        public ushort FadeLength { get; }

        /// <summary>
        /// The fade level of the effect.
        /// </summary>
        public ushort FadeLevel { get; }

        /// <summary>
        /// Creates a new custom haptic effect.
        /// </summary>
        /// <param name="direction">The direction of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        /// <param name="delay">The delay before the effect.</param>
        /// <param name="button">The button that triggers the effect.</param>
        /// <param name="interval">Minimum interval between effects.</param>
        /// <param name="channels">Axes to use.</param>
        /// <param name="period">Sample periods.</param>
        /// <param name="samples">Amount of samples.</param>
        /// <param name="data">The samples.</param>
        /// <param name="attackLength">The attach length of the effect.</param>
        /// <param name="attackLevel">The attack level of the effect.</param>
        /// <param name="fadeLength">The fade length of the effect.</param>
        /// <param name="fadeLevel">The fade level of the effect.</param>
        public CustomHapticEffect(HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, byte channels, ushort period, ushort samples, ushort[] data, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
        {
            Direction = direction;
            Length = length;
            Delay = delay;
            Button = button;
            Interval = interval;
            Channels = channels;
            Period = period;
            Samples = samples;
            Data = data;
            AttackLength = attackLength;
            AttackLevel = attackLevel;
            FadeLength = fadeLength;
            FadeLevel = fadeLevel;
        }

        internal override Native.SDL_HapticEffect ToNative() =>
            new()
            {
                _custom = new Native.SDL_HapticCustom(Native.SDL_HapticType.Custom, Direction.ToNative(), Length, Delay, Button, Interval, Channels, Period, Samples, Data, AttackLength, AttackLevel, FadeLength, FadeLevel)
            };
    }
}
