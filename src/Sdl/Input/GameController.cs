using System;

namespace Sdl.Input
{
    /// <summary>
    /// A game controller.
    /// </summary>
    public sealed unsafe class GameController : NativePointerBase<Native.SDL_GameController, GameController>
    {
        /// <summary>
        /// The number of mappings for game controllers.
        /// </summary>
        public static int MappingsCount =>
            Sdl.Native.SDL_GameControllerNumMappings();

        /// <summary>
        /// The mapping for this game controller.
        /// </summary>
        public string? Mapping
        {
            get
            {
                using var mapping = Sdl.Native.SDL_GameControllerMapping(Native);
                return mapping.ToString();
            }
        }

        /// <summary>
        /// The name of the game controller, if any.
        /// </summary>
        public string? Name =>
            Sdl.Native.SDL_GameControllerName(Native);

        /// <summary>
        /// The player index assigned to the controller.
        /// </summary>
        public int PlayerIndex
        {
            get => Sdl.Native.SDL_GameControllerGetPlayerIndex(Native);
            set => Sdl.Native.SDL_GameControllerSetPlayerIndex(Native, value);
        }

        /// <summary>
        /// The vendor code of the game controller
        /// </summary>
        public ushort Vendor =>
            Sdl.Native.SDL_GameControllerGetVendor(Native);

        /// <summary>
        /// The product code of the game controller.
        /// </summary>
        public ushort Product =>
            Sdl.Native.SDL_GameControllerGetProduct(Native);

        /// <summary>
        /// The product version of the game controller.
        /// </summary>
        public ushort ProductVersion =>
            Sdl.Native.SDL_GameControllerGetProductVersion(Native);

        /// <summary>
        /// Whether the game controller is attached.
        /// </summary>
        public bool Attached =>
            Sdl.Native.SDL_GameControllerGetAttached(Native);

        /// <summary>
        /// Gets the underlying joystick.
        /// </summary>
        public Joystick Joystick =>
            Joystick.PointerToInstanceNotNull(Sdl.Native.SDL_GameControllerGetJoystick(Native));

        /// <summary>
        /// Gets the type of the game controller.
        /// </summary>
        public GameControllerType Type =>
            Sdl.Native.SDL_GameControllerGetType(Native);

        /// <summary>
        /// An event fired when a game controller is added to the system.
        /// </summary>
        public static event EventHandler<GameControllerAddedEventArgs>? Added;

        /// <summary>
        /// An event fired when the game controller axis is moved.
        /// </summary>
        public event EventHandler<GameControllerAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event fired when the game controller button is pressed.
        /// </summary>
        public event EventHandler<GameControllerButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event fired when the game controller button is released.
        /// </summary>
        public event EventHandler<GameControllerButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event fired when a game controller is removed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event fired when a game controller is remapped.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Remapped;

        internal static GameController? Get(Native.SDL_JoystickID joystickId) =>
            PointerToInstance(Sdl.Native.SDL_GameControllerFromInstanceID(joystickId));

        /// <summary>
        /// Gets the game controller corresponding to the player index.
        /// </summary>
        /// <param name="playerIndex">The index of the player.</param>
        /// <returns>The game controller corresponding to the player.</returns>
        public static GameController? Get(int playerIndex) =>
            PointerToInstance(Sdl.Native.SDL_GameControllerFromPlayerIndex(playerIndex));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_GameControllerClose(Native);
            base.Dispose();
        }

        /// <summary>
        /// Adds game controller mappings from the data source.
        /// </summary>
        /// <param name="rwops">The data source.</param>
        /// <param name="shouldDispose">Whether the data source should be disposed after use.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(RWOps rwops, bool shouldDispose) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_GameControllerAddMappingsFromRW(rwops.Native, shouldDispose));

        /// <summary>
        /// Adds game controller mappings from a file.
        /// </summary>
        /// <param name="filename">The filename that holds the mappings.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(string filename) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_GameControllerAddMappingsFromFile(filename));

        /// <summary>
        /// Adds a game controller mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns><c>true</c> if the mapping was added, <c>false</c> if it updated an existing mapping.</returns>
        public static bool AddMapping(string mapping) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_GameControllerAddMapping(mapping)) == 1;

        /// <summary>
        /// Gets a controller mapping.
        /// </summary>
        /// <param name="index">The index of the mapping.</param>
        /// <returns>The mapping, if it exists.</returns>
        public static string? GetMapping(int index)
        {
            using var mapping = Sdl.Native.SDL_GameControllerMappingForIndex(index);
            return mapping.ToString();
        }

        /// <summary>
        /// Gets a controller mapping.
        /// </summary>
        /// <param name="id">The GUID of the mapping.</param>
        /// <returns>The mapping, if it exists.</returns>
        public static string? GetMapping(Guid id)
        {
            using var mapping = Sdl.Native.SDL_GameControllerMappingForGUID(id);
            return mapping.ToString();
        }

        /// <summary>
        /// Sets whether game controller events are polled.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The previous state.</returns>
        public static State SetEventState(State state) =>
            (State)Sdl.Native.SDL_GameControllerEventState(state);

        /// <summary>
        /// Polls game controllers for events.
        /// </summary>
        public static void Update() =>
            Sdl.Native.SDL_GameControllerUpdate();

        /// <summary>
        /// Maps a name to an axis.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The axis.</returns>
        public static GameControllerAxis GetAxisFromString(string name) =>
            Sdl.Native.SDL_GameControllerGetAxisFromString(name);

        /// <summary>
        /// Maps an axis to a name.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The name.</returns>
        public static string GetStringForAxis(GameControllerAxis axis) =>
            Sdl.Native.SDL_GameControllerGetStringForAxis(axis);

        /// <summary>
        /// Gets the binding for the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetAxisBinding(GameControllerAxis axis) =>
            GameControllerBinding.FromNative(Sdl.Native.SDL_GameControllerGetBindForAxis(Native, axis));

        /// <summary>
        /// The axis value.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(GameControllerAxis axis) =>
            Sdl.Native.SDL_GameControllerGetAxis(Native, axis);

        /// <summary>
        /// Maps a name to an button.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The button.</returns>
        public static GameControllerButton GetButtonFromString(string name) =>
            Sdl.Native.SDL_GameControllerGetButtonFromString(name);

        /// <summary>
        /// Maps an button to a name.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The name.</returns>
        public static string GetStringForButton(GameControllerButton button) =>
            Sdl.Native.SDL_GameControllerGetStringForButton(button);

        /// <summary>
        /// Gets the binding for the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetButtonBinding(GameControllerButton button) =>
            GameControllerBinding.FromNative(Sdl.Native.SDL_GameControllerGetBindForButton(Native, button));

        /// <summary>
        /// The button value.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Whether the button is pressed.</returns>
        public bool GetButton(GameControllerButton button) =>
            Sdl.Native.SDL_GameControllerGetButton(Native, button);

        /// <summary>
        /// Rumbles the game controller.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_GameControllerRumble(Native, lowFrequency, highFrequency, duration));

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Sdl.Native.SDL_EventType.ControllerAxisMotion:
                    {
                        var controller = Get(e.Caxis.Which);
                        controller?.AxisMotion?.Invoke(controller, new GameControllerAxisMotionEventArgs(e.Caxis));
                        break;
                    }

                case Sdl.Native.SDL_EventType.ControllerButtonDown:
                    {
                        var controller = Get(e.Cbutton.Which);
                        controller?.ButtonDown?.Invoke(controller, new GameControllerButtonEventArgs(e.Cbutton));
                        break;
                    }

                case Sdl.Native.SDL_EventType.ControllerButtonUp:
                    {
                        var controller = Get(e.Cbutton.Which);
                        controller?.ButtonUp?.Invoke(controller, new GameControllerButtonEventArgs(e.Cbutton));
                        break;
                    }

                case Sdl.Native.SDL_EventType.ControllerDeviceAdded:
                    {
                        Added?.Invoke(null, new GameControllerAddedEventArgs(e.Cdevice));
                        break;
                    }

                case Sdl.Native.SDL_EventType.ControllerDeviceRemoved:
                    {
                        var controller = Get(new Native.SDL_JoystickID(e.Cdevice.Which));
                        controller?.Removed?.Invoke(controller, new SdlEventArgs(e.Common));
                        break;
                    }

                case Sdl.Native.SDL_EventType.ControllerDeviceRemapped:
                    {
                        var controller = Get(new Native.SDL_JoystickID(e.Cdevice.Which));
                        controller?.Remapped?.Invoke(controller, new SdlEventArgs(e.Common));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
