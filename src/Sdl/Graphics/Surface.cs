﻿using System;

namespace Sdl.Graphics
{
    /// <summary>
    /// A surface.
    /// </summary>
    public sealed unsafe class Surface : NativePointerBase<Native.SDL_Surface, Surface>
    {
        /// <summary>
        /// The YUV conversion mode.
        /// </summary>
        public static YuvConversionMode YuvConversionMode
        {
            get => Sdl.Native.SDL_GetYUVConversionMode();
            set => Sdl.Native.SDL_SetYUVConversionMode(value);
        }

        /// <summary>
        /// The size of the surface.
        /// </summary>
        public Size Size => (Native->Width, Native->Height);

        /// <summary>
        /// The pitch of the surface.
        /// </summary>
        public int Pitch => Native->Pitch;

        /// <summary>
        /// The color modulator.
        /// </summary>
        public (byte Red, byte Green, byte Blue) ColorMod
        {
            get
            {
                _ = Sdl.Native.CheckError(Sdl.Native.SDL_GetSurfaceColorMod(Native, out var red, out var green, out var blue));
                return (red, green, blue);
            }
            set => _ = Sdl.Native.CheckError(Sdl.Native.SDL_SetSurfaceColorMod(Native, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// The alpha modulator.
        /// </summary>
        public byte AlphaMod
        {
            get
            {
                _ = Sdl.Native.CheckError(Sdl.Native.SDL_GetSurfaceAlphaMod(Native, out var alpha));
                return alpha;
            }
            set => _ = Sdl.Native.CheckError(Sdl.Native.SDL_SetSurfaceAlphaMod(Native, value));
        }

        /// <summary>
        /// The blend mode.
        /// </summary>
        public BlendMode BlendMode
        {
            get
            {
                _ = Sdl.Native.CheckError(Sdl.Native.SDL_GetSurfaceBlendMode(Native, out var mode));
                return mode;
            }

            set => Sdl.Native.CheckError(Sdl.Native.SDL_SetSurfaceBlendMode(Native, value));
        }

        /// <summary>
        /// The color key defines a pixel value that will be treated as transparent in a blit.
        /// </summary>
        public PixelColor? ColorKey
        {
            get
            {
                if (!Sdl.Native.SDL_HasColorKey(Native))
                {
                    return null;
                }

                _ = Sdl.Native.CheckError(Sdl.Native.SDL_GetColorKey(Native, out var key));
                return key;
            }
            set => _ = Sdl.Native.CheckError(Sdl.Native.SDL_SetColorKey(Native, value != null, value ?? (default)));
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat =>
            PixelFormat.PointerToInstanceNotNull(Native->Format);

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="mask">The color mask.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, Color mask) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_CreateRGBSurface(0, size.Width, size.Height, depth, mask.Red, mask.Green, mask.Blue, mask.Alpha));

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="format">The pixel format.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, EnumeratedPixelFormat format) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_CreateRGBSurfaceWithFormat(0, size.Width, size.Height, depth, format.Format));

        /// <summary>
        /// Creates a surface from a set of pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="pitch">The pitch of the surface.</param>
        /// <param name="mask">The color mask.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Span<byte> pixels, Size size, int depth, int pitch, Color mask)
        {
            fixed (byte* pixelsPointer = pixels)
            {
                return PointerToInstanceNotNull(Sdl.Native.SDL_CreateRGBSurfaceFrom(pixelsPointer, size.Width, size.Height, depth, pitch, mask.Red, mask.Green, mask.Blue, mask.Alpha));
            }
        }

        /// <summary>
        /// Creates a surface from a set of pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="pitch">The pitch of the surface.</param>
        /// <param name="format">The pixel format.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Span<byte> pixels, Size size, int depth, int pitch, EnumeratedPixelFormat format)
        {
            fixed (byte* pixelsPointer = pixels)
            {
                return PointerToInstanceNotNull(Sdl.Native.SDL_CreateRGBSurfaceWithFormatFrom(pixelsPointer, size.Width, size.Height, depth, pitch, format.Format));
            }
        }

        /// <summary>
        /// Gets the YUV conversion mode for a particular resolution.
        /// </summary>
        /// <param name="size">The resolution.</param>
        /// <returns>The conversion mode.</returns>
        public static YuvConversionMode GetYuvConversionModeForResolution(Size size) =>
            Sdl.Native.SDL_GetYUVConversionModeForResolution(size.Width, size.Height);

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_FreeSurface(Native);
            base.Dispose();
        }

        /// <summary>
        /// Loads a BMP into a surface.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(string filename) =>
            LoadBmp(RWOps.Create(filename, "rb"), true);

        /// <summary>
        /// Loads a BMP into a surface.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading the surface.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(RWOps rwops, bool shouldDispose) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_LoadBMP_RW(rwops.Native, shouldDispose));

        /// <summary>
        /// Loads a BMP into a surface compatible with a target surface.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <param name="targetSurface">The target surface.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(string filename, Surface targetSurface)
        {
            using var loadedSurface = LoadBmp(filename);
            return loadedSurface.Convert(targetSurface.PixelFormat);
        }

        /// <summary>
        /// Sets the palette.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public void SetPalette(Palette palette) =>
            _ = Sdl.Native.CheckError(Sdl.Native.SDL_SetSurfacePalette(Native, palette.Native));

        /// <summary>
        /// Locks the surface.
        /// </summary>
        public void Lock() =>
            _ = Sdl.Native.CheckError(Sdl.Native.SDL_LockSurface(Native));

        /// <summary>
        /// Unlocks the surface.
        /// </summary>
        public void Unlock() =>
            Sdl.Native.SDL_UnlockSurface(Native);

        /// <summary>
        /// The pixels of the surface.
        /// </summary>
        /// <typeparam name="T">The type of the pixel.</typeparam>
        /// <returns>The pixels.</returns>
        public Span<T> GetPixels<T>() => Sdl.Native.PixelsToSpan<T>(Native->Pixels, Native->Pitch, Native->Height);

        /// <summary>
        /// Saves the surface to a BMP.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveBmp(string filename) =>
            SaveBmp(RWOps.Create(filename, "wb"), true);

        /// <summary>
        /// Saves the surface to a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after saving the surface.</param>
        public void SaveBmp(RWOps rwops, bool shouldDispose) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_SaveBMP_RW(Native, rwops.Native, shouldDispose));

        /// <summary>
        /// Sets RLE acceleration hint for the surface.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public void SetRle(bool flag) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_SetSurfaceRLE(Native, flag));

        /// <summary>
        /// Gets the clipping rectangle.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetClippingRectangle()
        {
            Sdl.Native.SDL_GetClipRect(Native, out var rect);
            return rect;
        }

        /// <summary>
        /// Sets the clipping rectangle.
        /// </summary>
        /// <param name="clipRect">The clipping rectangle.</param>
        /// <returns><c>true</c> if the rectangle intersects the surface, otherwise <c>false</c> and blits will be completely clipped.</returns>
        public bool SetClippingRectangle(Rectangle? clipRect)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (clipRect.HasValue)
            {
                rect = clipRect.Value;
                rectPointer = &rect;
            }

            return Sdl.Native.SDL_SetClipRect(Native, rectPointer);
        }

        /// <summary>
        /// Duplicates the surface.
        /// </summary>
        /// <returns>The duplicate surface.</returns>
        public Surface Duplicate() =>
            PointerToInstanceNotNull(Sdl.Native.SDL_DuplicateSurface(Native));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(PixelFormat format) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_ConvertSurface(Native, format.Native));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(EnumeratedPixelFormat format) =>
           PointerToInstanceNotNull(Sdl.Native.SDL_ConvertSurfaceFormat(Native, format));

        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangle(Rectangle? rectangle, PixelColor color)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = Sdl.Native.CheckError(Sdl.Native.SDL_FillRect(Native, rectPointer, color));
        }

        /// <summary>
        /// Fills rectangles.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangles(Rectangle[] rectangles, PixelColor color) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_FillRects(Native, rectangles, rectangles.Length, color));

        /// <summary>
        /// Blits from the surface to another surface.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void Blit(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            var sourcePointer = (Rectangle*)null;
            var destinationPointer = (Rectangle*)null;
            Rectangle sourceRect;
            Rectangle destinationRect;

            if (sourceRectangle.HasValue)
            {
                sourceRect = sourceRectangle.Value;
                sourcePointer = &sourceRect;
            }

            if (destinationRectangle.HasValue)
            {
                destinationRect = destinationRectangle.Value;
                destinationPointer = &destinationRect;
            }

            _ = Sdl.Native.CheckError(Sdl.Native.SDL_UpperBlit(Native, sourcePointer, destination.Native, destinationPointer));
        }

        /// <summary>
        /// Blits from the surface to another surface with scaling.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void BlitScaled(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            var sourcePointer = (Rectangle*)null;
            var destinationPointer = (Rectangle*)null;
            Rectangle sourceRect;
            Rectangle destinationRect;

            if (sourceRectangle.HasValue)
            {
                sourceRect = sourceRectangle.Value;
                sourcePointer = &sourceRect;
            }

            if (destinationRectangle.HasValue)
            {
                destinationRect = destinationRectangle.Value;
                destinationPointer = &destinationRect;
            }

            _ = Sdl.Native.CheckError(Sdl.Native.SDL_UpperBlitScaled(Native, sourcePointer, destination.Native, destinationPointer));
        }
    }
}
