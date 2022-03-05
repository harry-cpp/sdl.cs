using System;

using Vulkan;

namespace Sdl.Graphics.Vulkan
{
    /// <summary>
    /// SDL Vulkan related methods.
    /// </summary>
    public static class VulkanExtensions
    {
        /// <summary>
        /// Creates a Vulkan ready surface.
        /// </summary>
        /// <param name="window">Window from which to use the surface.</param>
        /// <param name="instance">Vulkan instance to be used for the surface creation.</param>
        /// <returns></returns>
        /// <exception cref="Sdl.SdlException">If the surface has failed to be created.</exception>
        public static unsafe SurfaceKhr CreateVulkanSurface(this Window window, Instance instance)
        {
            var windowCreated = Sdl.Native.SDL_Vulkan_CreateSurface(window.Native, ((IMarshalling)instance).Handle, out UInt64 surface);

            if (!windowCreated)
                throw new SdlException();

            var surfaceKhr = (SurfaceKhr)Activator.CreateInstance(typeof(SurfaceKhr), true)!;
            var prop = surfaceKhr.GetType().GetField("m", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            prop.SetValue(surfaceKhr, surface);

            return surfaceKhr;
        }

        /// <summary>
        /// Get the names of the Vulkan instance extensions needed to create a surface with CreateVulkanSurface.
        /// </summary>
        /// <param name="window">A window for which the required Vulkan instance extensions should be retrieved (will be deprecated in a future release).</param>
        /// <returns>List of required Vulkan extensions.</returns>
        /// <exception cref="Sdl.SdlException">If the SDL_Vulkan_GetInstanceExtensions has failed to acquire a list of required extensions.</exception>
        public static unsafe string[] GetVulkanInstanceExtensions(this Window window)
        {
            if (!Sdl.Native.SDL_Vulkan_GetInstanceExtensions(window.Native, out uint pCount, IntPtr.Zero))
                throw new SdlException();

            var rawExtensions = new IntPtr[pCount];
            if (!Sdl.Native.SDL_Vulkan_GetInstanceExtensions(window.Native, out pCount, rawExtensions))
                throw new SdlException();

            var extensions = new string[pCount];
            for (int i = 0; i < pCount; i++)
            {
                var utf8string = new Utf8String((byte*)rawExtensions[i]);
                extensions[i] = utf8string.ToString() ?? string.Empty;
            }
            return extensions;
        }
    }
}
