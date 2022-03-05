using System;
using System.Collections.Generic;

namespace Sdl.Input
{
    /// <summary>
    /// A joystick.
    /// </summary>
    public sealed unsafe class Joystick : NativePointerBase<Native.SDL_Joystick, Joystick>
    {
        private static ItemCollection<JoystickInfo>? s_infos;

        /// <summary>
        /// The joysticks in the system.
        /// </summary>
        public static IReadOnlyList<JoystickInfo> Infos => s_infos ??= new ItemCollection<JoystickInfo>(JoystickInfo.Get, Sdl.Native.SDL_NumJoysticks);

        /// <summary>
        /// The name of the joystick, if any.
        /// </summary>
        public string? Name => Sdl.Native.SDL_JoystickName(Native);

        /// <summary>
        /// The player index assigned to this joystick.
        /// </summary>
        public int PlayerIndex
        {
            get => Sdl.Native.SDL_JoystickGetPlayerIndex(Native);
            set => Sdl.Native.SDL_JoystickSetPlayerIndex(Native, value);
        }

        /// <summary>
        /// The GUID of this joystick.
        /// </summary>
        public Guid Id => Sdl.Native.SDL_JoystickGetGUID(Native);

        /// <summary>
        /// The vendor code for this joystick.
        /// </summary>
        public ushort Vendor => Sdl.Native.SDL_JoystickGetVendor(Native);

        /// <summary>
        /// The product code for this joystick.
        /// </summary>
        public ushort Product => Sdl.Native.SDL_JoystickGetProduct(Native);

        /// <summary>
        /// The product version of this joystick.
        /// </summary>
        public ushort ProductVersion => Sdl.Native.SDL_JoystickGetProductVersion(Native);

        /// <summary>
        /// The type of the joystick.
        /// </summary>
        public JoystickType Type => Sdl.Native.SDL_JoystickGetType(Native);

        /// <summary>
        /// Whether the joystick is attached.
        /// </summary>
        public bool Attached => Sdl.Native.SDL_JoystickGetAttached(Native);

        /// <summary>
        /// The number of axes in the joystick.
        /// </summary>
        public int Axes => Sdl.Native.SDL_JoystickNumAxes(Native);

        /// <summary>
        /// The number of balls in the joystick.
        /// </summary>
        public int Balls => Sdl.Native.SDL_JoystickNumBalls(Native);

        /// <summary>
        /// The number of hats in the joystick.
        /// </summary>
        public int Hats => Sdl.Native.SDL_JoystickNumHats(Native);

        /// <summary>
        /// The number of buttons in the joystick.
        /// </summary>
        public int Buttons => Sdl.Native.SDL_JoystickNumButtons(Native);

        /// <summary>
        /// The power level of the joystick.
        /// </summary>
        public JoystickPowerLevel PowerLevel => Sdl.Native.SDL_JoystickCurrentPowerLevel(Native);

        /// <summary>
        /// Whether the joystick supports haptic effects.
        /// </summary>
        public bool IsHaptic =>
            Sdl.Native.SDL_JoystickIsHaptic(Native);

        /// <summary>
        /// Returns the haptic support for the joystick.
        /// </summary>
        public Haptic Haptic =>
            Haptic.PointerToInstanceNotNull(Sdl.Native.SDL_HapticOpenFromJoystick(Native));

        /// <summary>
        /// An event that is fired when a joystick is added.
        /// </summary>
        public static event EventHandler<JoystickAddedEventArgs>? Added;

        /// <summary>
        /// An event that fires when there is motion on an axis.
        /// </summary>
        public event EventHandler<JoystickAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event that fires when there is motion on a ball.
        /// </summary>
        public event EventHandler<JoystickBallMotionEventArgs>? BallMotion;

        /// <summary>
        /// An event that fires when a button is pressed.
        /// </summary>
        public event EventHandler<JoystickButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event that fires when a button is released.
        /// </summary>
        public event EventHandler<JoystickButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event that is fired when a joystick is removed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event that fires when a hat moves.
        /// </summary>
        public event EventHandler<JoystickHatMotionEventArgs>? HatMotion;

        internal static Joystick Get(Native.SDL_JoystickID instanceId) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_JoystickFromInstanceID(instanceId));

        /// <summary>
        /// Gets the joystick that corresponds to the player index.
        /// </summary>
        /// <param name="playerIndex">The player index.</param>
        /// <returns>The joystick that corresponds to the player index.</returns>
        public static Joystick Get(int playerIndex) =>
            PointerToInstanceNotNull(Sdl.Native.SDL_JoystickFromPlayerIndex(playerIndex));

        /// <summary>
        /// Locks the joysticks.
        /// </summary>
        public static void LockJoysticks() =>
            Sdl.Native.SDL_LockJoysticks();

        /// <summary>
        /// Unlocks the joysticks.
        /// </summary>
        public static void UnlockJoysticks() =>
            Sdl.Native.SDL_UnlockJoysticks();

        /// <summary>
        /// Polls joysticks for events.
        /// </summary>
        public static void Update() =>
            Sdl.Native.SDL_JoystickUpdate();

        /// <inheritdoc/>
        public override void Dispose()
        {
            Sdl.Native.SDL_JoystickClose(Native);
            base.Dispose();
        }

        /// <summary>
        /// Sets the state for joystick events.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The old state.</returns>
        public static State SetEventState(State state) =>
            (State)Sdl.Native.SDL_JoystickEventState(state);

        /// <summary>
        /// Gets the value of the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(int axis) =>
            Sdl.Native.SDL_JoystickGetAxis(Native, axis);

        /// <summary>
        /// Gets the axis's initial state.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>Whether the axis exists.</returns>
        public bool GetAxisInitialState(int axis, out short state) =>
            Sdl.Native.SDL_JoystickGetAxisInitialState(Native, axis, out state);

        /// <summary>
        /// Gets the value of the hat.
        /// </summary>
        /// <param name="hat">The hat.</param>
        /// <returns>The value.</returns>
        public HatState GetHat(int hat) =>
            Sdl.Native.SDL_JoystickGetHat(Native, hat);

        /// <summary>
        /// Gets the value of the ball.
        /// </summary>
        /// <param name="ball">The ball.</param>
        /// <returns>The value.</returns>
        public (int XDelta, int YDelta) GetBall(int ball)
        {
            _ = Sdl.Native.CheckError(Sdl.Native.SDL_JoystickGetBall(Native, ball, out var dx, out var dy));
            return (dx, dy);
        }

        /// <summary>
        /// Gets the button state.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns><c>true</c> if the button is pressed, <c>false</c> otherwise.</returns>
        public bool GetButton(int button) =>
            Sdl.Native.SDL_JoystickGetButton(Native, button);

        /// <summary>
        /// Rumbles the joystick.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Sdl.Native.CheckError(Sdl.Native.SDL_JoystickRumble(Native, lowFrequency, highFrequency, duration));

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Sdl.Native.SDL_EventType.JoystickAxisMotion:
                    {
                        var joystick = Get(e.Jaxis.Which);
                        joystick?.AxisMotion?.Invoke(joystick, new JoystickAxisMotionEventArgs(e.Jaxis));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickBallMotion:
                    {
                        var joystick = Get(e.Jball.Which);
                        joystick?.BallMotion?.Invoke(joystick, new JoystickBallMotionEventArgs(e.Jball));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickButtonDown:
                    {
                        var joystick = Get(e.Jbutton.Which);
                        joystick?.ButtonDown?.Invoke(joystick, new JoystickButtonEventArgs(e.Jbutton));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickButtonUp:
                    {
                        var joystick = Get(e.Jbutton.Which);
                        joystick?.ButtonUp?.Invoke(joystick, new JoystickButtonEventArgs(e.Jbutton));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickDeviceAdded:
                    {
                        Added?.Invoke(null, new JoystickAddedEventArgs(e.Jdevice));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickDeviceRemoved:
                    {
                        var joystick = Get(new Native.SDL_JoystickID(e.Jdevice.Which));
                        joystick.Removed?.Invoke(joystick, new SdlEventArgs(e.Common));
                        break;
                    }

                case Sdl.Native.SDL_EventType.JoystickHatMotion:
                    {
                        var joystick = Get(e.Jhat.Which);
                        joystick?.HatMotion?.Invoke(joystick, new JoystickHatMotionEventArgs(e.Jhat));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
