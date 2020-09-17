﻿using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A class that represents storage.
    /// </summary>
    public sealed unsafe class RWOps : NativePointerBase<Native.SDL_RWops, RWOps>
    {
        /// <summary>
        /// Returns the size of the storage.
        /// </summary>
        public long Size => Native.SDL_RWsize(Pointer);

        /// <summary>
        /// Creates a storage from a filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="mode">The file mode.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(string filename, string mode) =>
            PointerToInstanceNotNull(Native.SDL_RWFromFile(filename, mode));

        /// <summary>
        /// Creates a storage over a block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(NativeMemoryBlock memory) =>
            PointerToInstanceNotNull(Native.SDL_RWFromMem(memory.Pointer, (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(NativeMemoryBlock memory) =>
            PointerToInstanceNotNull(Native.SDL_RWFromConstMem(memory.Pointer, (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only byte array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(byte[] array)
        {
            var instance = PointerToInstanceNotNull(Native.SDL_AllocRW());
            var wrapper = new ReadOnlyByteArrayWrapper(array);
            instance.Pointer->Type = Native.SDL_RWOpsType.Unknown;
            instance.Pointer->Size = Marshal.GetFunctionPointerForDelegate<Native.SizeRWOps>(wrapper.Size);
            instance.Pointer->Seek = Marshal.GetFunctionPointerForDelegate<Native.SeekRWOps>(wrapper.Seek);
            instance.Pointer->Read = Marshal.GetFunctionPointerForDelegate<Native.ReadRWOps>(wrapper.Read);
            instance.Pointer->Write = Marshal.GetFunctionPointerForDelegate<Native.WriteRWOps>(ReadOnlyByteArrayWrapper.Write);
            instance.Pointer->Close = Marshal.GetFunctionPointerForDelegate<Native.CloseRWOps>(wrapper.Close);
            return instance;
        }

        /// <summary>
        /// Seeks to a point in the storage.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="type">The type of seek to perform.</param>
        /// <returns>The new location.</returns>
        public long Seek(long offset, SeekType type) =>
            Native.SDL_RWseek(Pointer, offset, type);

        /// <summary>
        /// Reads a value from the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The value, or <c>null</c> if the value could not be read.</returns>
        public T? Read<T>() where T : unmanaged
        {
            T value = default;
            return Native.SDL_RWread(Pointer, &value, (uint)sizeof(T), 1) == 0 ? (T?)null : value;
        }

        /// <summary>
        /// Writes a value to the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value was written, <c>false</c> otherwise.</returns>
        public bool Write<T>(T value) where T : unmanaged => Native.SDL_RWwrite(Pointer, &value, (uint)sizeof(T), 1) != 0;

        /// <inheritdoc/>
        public override void Dispose()
        {
            _ = Native.CheckError(Native.SDL_RWclose(Pointer));
            base.Dispose();
        }

        /// <summary>
        /// Reads an unsigned byte.
        /// </summary>
        /// <returns>The value.</returns>
        public byte ReadU8() => Native.SDL_ReadU8(Pointer);

        /// <summary>
        /// Reads a little-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadLE16() => Native.SDL_ReadLE16(Pointer);

        /// <summary>
        /// Reads a big-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadBE16() => Native.SDL_ReadBE16(Pointer);

        /// <summary>
        /// Reads a little-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadLE32() => Native.SDL_ReadLE32(Pointer);

        /// <summary>
        /// Reads a big-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadBE32() => Native.SDL_ReadBE32(Pointer);

        /// <summary>
        /// Reads a little-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadLE64() => Native.SDL_ReadLE64(Pointer);

        /// <summary>
        /// Reads a big-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadBE64() => Native.SDL_ReadBE64(Pointer);

        /// <summary>
        /// Writes an unsigned byte.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteU8(byte value) => Native.CheckErrorZero(Native.SDL_WriteU8(Pointer, value));

        /// <summary>
        /// Writes a little-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE16(ushort value) => Native.CheckErrorZero(Native.SDL_WriteLE16(Pointer, value));

        /// <summary>
        /// Writes a big-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE16(ushort value) => Native.CheckErrorZero(Native.SDL_WriteBE16(Pointer, value));

        /// <summary>
        /// Writes a little-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE32(uint value) => Native.CheckErrorZero(Native.SDL_WriteLE32(Pointer, value));

        /// <summary>
        /// Writes a big-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE32(uint value) => Native.CheckErrorZero(Native.SDL_WriteBE32(Pointer, value));

        /// <summary>
        /// Writes a little-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE64(ulong value) => Native.CheckErrorZero(Native.SDL_WriteLE64(Pointer, value));

        /// <summary>
        /// Writes a big-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE64(ulong value) => Native.CheckErrorZero(Native.SDL_WriteBE64(Pointer, value));

        private sealed class ReadOnlyByteArrayWrapper
        {
            private readonly byte[] _array;
            private int _index;
            private bool _isClosed;

            public ReadOnlyByteArrayWrapper(byte[] array)
            {
                _array = array;
            }

            public long Size(Native.SDL_RWops* _) => _isClosed ? -1 : _array.Length;

            internal int Close(Native.SDL_RWops* _)
            {
                if (_isClosed)
                {
                    return -1;
                }
                _isClosed = true;
                return 0;
            }

            internal long Seek(Native.SDL_RWops* _, long offset, SeekType whence)
            {
                var newIndex = _index;

                switch (whence)
                {
                    case SeekType.Set:
                        newIndex = (int)offset;
                        break;

                    case SeekType.Current:
                        newIndex += (int)offset;
                        break;

                    case SeekType.End:
                        newIndex = _array.Length + (int)offset;
                        break;
                }

                if (newIndex < 0 || newIndex >= _array.Length)
                {
                    return -1;
                }

                _index = newIndex;
                return _index;
            }

            internal nuint Read(Native.SDL_RWops* _, void* ptr, nuint size, nuint maxnum)
            {
                uint numberRead = 0;
                var sizeNumber = (uint)size;
                var maxNumNumber = (uint)maxnum;
                var bytePointer = (byte*)ptr;
                var byteIndex = 0;

                bool InBounds() => _index + sizeNumber < _array.Length;

                if (_isClosed || !InBounds())
                {
                    return 0;
                }

                while (maxNumNumber > 0 && InBounds())
                {
                    maxNumNumber--;
                    numberRead++;

                    for (var index = 0; index < sizeNumber; index++)
                    {
                        bytePointer[byteIndex++] = _array[_index++];
                    }
                }

                return (nuint)numberRead;
            }

#pragma warning disable IDE1006, RCS1163
            internal static nuint Write(Native.SDL_RWops* _1, void* _2, nuint _3, nuint _4) => 0;
        }
    }
}
