﻿using Sdl;
using Sdl.Graphics;
using Sdl.Input;

using Application app = new(Subsystems.Video);
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Drawing on the Screen", windowRectangle, WindowOptions.Shown);

using var sunflowers = Surface.LoadBmp("Sunflowers.bmp");

var stretch = false;

Keyboard.KeyDown += (s, e) =>
{
    switch (e.Keycode)
    {
        case Keycode.s:
            stretch = !stretch;
            break;
    }
};

while (app.IsRunning)
{
    while (app.DispatchEvent() > 0)
    {
    }

    if (stretch)
    {
        sunflowers.BlitScaled(window.Surface, null, (Point.Origin, windowSize));
    }
    else
    {
        sunflowers.Blit(window.Surface);
    }

    window.UpdateSurface();
}
