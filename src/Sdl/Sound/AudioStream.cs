﻿using System;

namespace Sdl.Sound
{
    /// <summary>
    /// An audio stream that converts audio from one format to another.
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public sealed unsafe class AudioStream : NativePointerBase<Native.SDL_AudioStream, AudioStream>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        /// <summary>
        /// The number of bytes available in the stream.
        /// </summary>
        public int Available =>
            Sdl.Native.SDL_AudioStreamAvailable(Native);

        /// <summary>
        /// Creates a new audio stream.
        /// </summary>
        /// <param name="sourceFormat">The source audio format.</param>
        /// <param name="sourceChannels">The source audio channel count.</param>
        /// <param name="sourceRate">The source audio bitrate.</param>
        /// <param name="destinationFormat">The destination audio format.</param>
        /// <param name="destinationChannels">The destination audio channel count.</param>
        /// <param name="destinationRate">The destination audio bitrate.</param>
        /// <returns>The new audio stream.</returns>
        public static AudioStream Create(AudioFormat sourceFormat, byte sourceChannels, int sourceRate, AudioFormat destinationFormat, byte destinationChannels, int destinationRate) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_NewAudioStream(sourceFormat, sourceChannels, sourceRate, destinationFormat, destinationChannels, destinationRate));

        /// <summary>
        /// Puts data into the stream.
        /// </summary>
        /// <param name="data"></param>
        public void Put(Span<byte> data)
        {
            fixed (byte* dataPointer = data)
            {
                _ = Sdl.Native.CheckError(Sdl.Native.SDL_AudioStreamPut(Native, dataPointer, data.Length));
            }
        }

        /// <summary>
        /// Gets data from the stream.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The number of bytes actually read.</returns>
        public int Get(Span<byte> data)
        {
            fixed (byte* dataPointer = data)
            {
                return Sdl.Native.CheckError(Sdl.Native.SDL_AudioStreamGet(Native, dataPointer, data.Length));
            }
        }

        /// <summary>
        /// Flushes the audio stream.
        /// </summary>
        public void Flush() =>
            Sdl.Native.CheckError(Sdl.Native.SDL_AudioStreamFlush(Native));

        /// <summary>
        /// Clears any pending data in the audio stream.
        /// </summary>
        public void Clear() =>
            Sdl.Native.SDL_AudioStreamClear(Native);

        /// <summary>
        /// Closes an audio stream.
        /// </summary>
        public override void Dispose()
        {
            Sdl.Native.SDL_FreeAudioStream(Native);
            base.Dispose();
        }
    }
}
