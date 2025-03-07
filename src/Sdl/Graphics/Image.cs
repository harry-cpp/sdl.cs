﻿namespace Sdl.Graphics
{
    /// <summary>
    /// Image handling from SDL_image
    /// </summary>
    public static unsafe class Image
    {
        /// <summary>
        /// Loads an image from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The image.</returns>
        public static Surface Load(string filename) =>
            Surface.PointerToInstanceNotNull(Native.IMG_Load(filename));

        /// <summary>
        /// Loads an image from a file compatible with a target surface.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="targetSurface">The target surface.</param>
        /// <returns>The image.</returns>
        public static Surface Load(string filename, Surface targetSurface)
        {
            using var loadedSurface = Load(filename);
            return loadedSurface.Convert(targetSurface.PixelFormat);
        }

        /// <summary>
        /// Loads an image from a file into a texture.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="colorKey">The color key for the image.</param>
        /// <returns>The image.</returns>
        public static Texture Load(string filename, Renderer renderer, Color? colorKey = default)
        {
            using var loadedSurface = Load(filename);
            if (colorKey != null)
            {
                loadedSurface.ColorKey = loadedSurface.PixelFormat.Map(colorKey.Value.Red, colorKey.Value.Green, colorKey.Value.Blue, colorKey.Value.Alpha);
            }
            return renderer.CreateTexture(loadedSurface);
        }

        /// <summary>
        /// Loads an image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <returns>The image.</returns>
        public static Surface Load(RWOps rwops, bool shouldDispose, string type) =>
            Surface.PointerToInstanceNotNull(Native.IMG_LoadTyped_RW(rwops.Native, shouldDispose, type));

        /// <summary>
        /// Loads an image from a storage into a texture.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="colorKey">The color key for the image.</param>
        /// <returns>The image.</returns>
        public static Texture Load(RWOps rwops, bool shouldDispose, string type, Renderer renderer, Color? colorKey = default)
        {
            using var loadedSurface = Load(rwops, shouldDispose, type);
            if (colorKey != null)
            {
                loadedSurface.ColorKey = loadedSurface.PixelFormat.Map(colorKey.Value.Red, colorKey.Value.Green, colorKey.Value.Blue, colorKey.Value.Alpha);
            }
            return renderer.CreateTexture(loadedSurface);
        }

        /// <summary>
        /// Loads an image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <returns>The image.</returns>
        public static Surface Load(RWOps rwops, bool shouldDispose) =>
            Surface.PointerToInstanceNotNull(Native.IMG_Load_RW(rwops.Native, shouldDispose));

        /// <summary>
        /// Loads an image as a texture from a file.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, string filename) =>
            Texture.PointerToInstanceNotNull(Native.IMG_LoadTexture(renderer.Native, filename));

        /// <summary>
        /// Loads an image as a texture from a storage.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, RWOps rwops, bool shouldDispose, string type) =>
            Texture.PointerToInstanceNotNull(Native.IMG_LoadTextureTyped_RW(renderer.Native, rwops.Native, shouldDispose, type));

        /// <summary>
        /// Loads an image as a texture from a storage.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, RWOps rwops, bool shouldDispose) =>
            Texture.PointerToInstanceNotNull(Native.IMG_LoadTexture_RW(renderer.Native, rwops.Native, shouldDispose));

        /// <summary>
        /// Whether the storage is an ICO image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsIco(RWOps rwops) =>
            Native.IMG_isICO(rwops.Native);

        /// <summary>
        /// Whether the storage is an CUR image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsCur(RWOps rwops) =>
            Native.IMG_isCUR(rwops.Native);

        /// <summary>
        /// Whether the storage is an BMP image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsBmp(RWOps rwops) =>
            Native.IMG_isBMP(rwops.Native);

        /// <summary>
        /// Whether the storage is an GIF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsGif(RWOps rwops) =>
            Native.IMG_isGIF(rwops.Native);

        /// <summary>
        /// Whether the storage is an JPG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsJpg(RWOps rwops) =>
            Native.IMG_isJPG(rwops.Native);

        /// <summary>
        /// Whether the storage is an LBM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsLbm(RWOps rwops) =>
            Native.IMG_isLBM(rwops.Native);

        /// <summary>
        /// Whether the storage is an PCX image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPcx(RWOps rwops) =>
            Native.IMG_isPCX(rwops.Native);

        /// <summary>
        /// Whether the storage is an PNG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPng(RWOps rwops) =>
            Native.IMG_isPNG(rwops.Native);

        /// <summary>
        /// Whether the storage is an PNM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPnm(RWOps rwops) =>
            Native.IMG_isPNM(rwops.Native);

        /// <summary>
        /// Whether the storage is an SVG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsSvg(RWOps rwops) =>
            Native.IMG_isSVG(rwops.Native);

        /// <summary>
        /// Whether the storage is an TIFF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsTif(RWOps rwops) =>
            Native.IMG_isTIF(rwops.Native);

        /// <summary>
        /// Whether the storage is an XCF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXcf(RWOps rwops) =>
            Native.IMG_isXCF(rwops.Native);

        /// <summary>
        /// Whether the storage is an XPM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXpm(RWOps rwops) =>
            Native.IMG_isXPM(rwops.Native);

        /// <summary>
        /// Whether the storage is an XV image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXv(RWOps rwops) =>
            Native.IMG_isXV(rwops.Native);

        /// <summary>
        /// Whether the storage is an WEBP image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsWebp(RWOps rwops) =>
            Native.IMG_isWEBP(rwops.Native);

        /// <summary>
        /// Load a Ico image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadIco(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadICO_RW(rwops.Native));

        /// <summary>
        /// Load a Cur image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadCur(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadCUR_RW(rwops.Native));

        /// <summary>
        /// Load a Bmp image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadBmp(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadBMP_RW(rwops.Native));

        /// <summary>
        /// Load a Gif image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadGif(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadGIF_RW(rwops.Native));

        /// <summary>
        /// Load a Jpg image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadJpg(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadJPG_RW(rwops.Native));

        /// <summary>
        /// Load a Lbm image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadLbm(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadLBM_RW(rwops.Native));

        /// <summary>
        /// Load a Pcx image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPcx(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadPCX_RW(rwops.Native));

        /// <summary>
        /// Load a Png image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPng(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadPNG_RW(rwops.Native));

        /// <summary>
        /// Load a Pnm image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPnm(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadPNM_RW(rwops.Native));

        /// <summary>
        /// Load a Svg image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadSvg(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadSVG_RW(rwops.Native));

        /// <summary>
        /// Load a Tga image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadTga(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadTGA_RW(rwops.Native));

        /// <summary>
        /// Load a Tif image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadTif(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadTIF_RW(rwops.Native));

        /// <summary>
        /// Load a Xcf image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXcf(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadXCF_RW(rwops.Native));

        /// <summary>
        /// Load a Xpm image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXpm(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadXPM_RW(rwops.Native));

        /// <summary>
        /// Load a Xv image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXv(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadXV_RW(rwops.Native));

        /// <summary>
        /// Load a Webp image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadWebp(RWOps rwops) =>
                    Surface.PointerToInstanceNotNull(Native.IMG_LoadWEBP_RW(rwops.Native));

        /// <summary>
        /// Saves a surface as a PNG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="filename">The filename.</param>
        public static void SavePng(Surface surface, string filename) =>
            Native.CheckError(Native.IMG_SavePNG(surface.Native, filename));

        /// <summary>
        /// Saves a surface as a PNG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        public static void SavePng(Surface surface, RWOps rwops, bool shouldDispose) =>
            Native.CheckError(Native.IMG_SavePNG_RW(surface.Native, rwops.Native, shouldDispose));

        /// <summary>
        /// Saves a surface as a JPG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="quality">The quality to save as.</param>
        public static void SaveJpg(Surface surface, string filename, int quality) =>
            Native.CheckError(Native.IMG_SaveJPG(surface.Native, filename, quality));

        /// <summary>
        /// Saves a surface as a JPG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="quality">The quality to save as.</param>
        public static void SaveJpg(Surface surface, RWOps rwops, bool shouldDispose, int quality) =>
            Native.CheckError(Native.IMG_SaveJPG_RW(surface.Native, rwops.Native, shouldDispose, quality));
    }
}
