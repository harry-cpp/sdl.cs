using System.Runtime.InteropServices;

namespace Sdl.Sound
{
    /// <summary>
    /// A callback when music is finished playing.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MusicFinishedCallback();
}
