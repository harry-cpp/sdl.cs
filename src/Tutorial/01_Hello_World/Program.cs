using Sdl;
using Sdl.Graphics;

using var app = new Application(Subsystems.Video);
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Hello, World!", windowRectangle, WindowOptions.Shown);

while (app.IsRunning)
{
    while (app.DispatchEvent() > 0)
    {
    }
}
