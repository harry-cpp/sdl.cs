﻿using Sdl;
using Sdl.Graphics;
using Sdl.Input;

using Application app = new(Subsystems.Video, ImageFormats.Jpg, hints: new[] { (Hint.RenderScaleQuality, "1") });
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Drawing", windowRectangle, WindowOptions.Shown);
using var renderer = Renderer.Create(window, -1, RendererOptions.Accelerated);

Rectangle? viewport = null;

Keyboard.KeyDown += (s, e) => viewport = e.Keycode switch
{
    Keycode.Number0 => null,
    Keycode.Number1 => (Point.Origin, (windowSize.Width / 2, windowSize.Height / 2)),
    Keycode.Number2 => ((windowSize.Width / 2, 0), (windowSize.Width / 2, windowSize.Height / 2)),
    Keycode.Number3 => ((0, windowSize.Height / 2), (windowSize.Width, windowSize.Height / 2)),
    _ => viewport
};

while (app.IsRunning)
{
    while (app.DispatchEvent() > 0)
    {
    }
    
    renderer.DrawColor = Colors.White;
    renderer.Clear();

    renderer.Viewport = viewport;
    var size = viewport?.Size ?? windowSize;

    renderer.DrawColor = Colors.Red;
    renderer.FillRectangle(((size.Width / 4, size.Height / 4), (size.Width / 2, size.Height / 2)));

    renderer.DrawColor = Colors.Green;
    renderer.DrawRectangle(((size.Width / 6, size.Height / 6), (size.Width * 2 / 3, size.Height * 2 / 3)));

    renderer.DrawColor = Colors.Blue;
    renderer.DrawLine((0, size.Height / 2), (size.Width, size.Height / 2));

    renderer.DrawColor = Colors.Yellow;
    for (var i = 0; i < size.Height; i += 4)
    {
        renderer.DrawPoint((size.Width / 2, i));
    }

    renderer.Viewport = null;

    renderer.Present();
}
