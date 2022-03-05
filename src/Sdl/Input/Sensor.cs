using System;

namespace Sdl.Input
{
    /// <summary>
    /// A sensor in the device.
    /// </summary>
    public sealed unsafe class Sensor : NativePointerBase<Native.SDL_Sensor, Sensor>
    {
        /// <summary>
        /// The sensor's name.
        /// </summary>
        public string Name =>
            Sdl.Native.SDL_SensorGetName(Native);

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            Sdl.Native.SDL_SensorGetType(Native);

        /// <summary>
        /// A non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            Sdl.Native.SDL_SensorGetNonPortableType(Native);

        /// <summary>
        /// An event fired when a sensor is updated.
        /// </summary>
        public event EventHandler<SensorUpdatedEventArgs>? Updated;

        internal static Sensor Get(Native.SDL_SensorID instanceId) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_SensorFromInstanceID(instanceId));

        /// <summary>
        /// Gets the data from the sensor.
        /// </summary>
        /// <param name="data">Where to put the data.</param>
        public void GetData(float[] data) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_SensorGetData(Native, data, data.Length));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_SensorClose(Native);
            base.Dispose();
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            var sensor = Get(e.Sensor.Which);

            switch (e.Type)
            {
                case Sdl.Native.SDL_EventType.SensorUpdate:
                    sensor.Updated?.Invoke(sensor, new SensorUpdatedEventArgs(e.Sensor));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
