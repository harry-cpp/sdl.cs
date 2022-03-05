# sdl.cs

This is a my personal fork of https://github.com/panopticoncentral/sdl-sharp, feel free tho to make PRs against this repo if you so choose to use it :)

The sdl.cs library is a [.NET](https://dotnet.microsoft.com/) wrapper around the [Simple DirectMedia Layer](https://www.libsdl.org/index.php) (SDL) library, a cross-platform graphics, audio, and input library. It provides both a set of low-level APIs that enables calling most SDL APIs directly, and a set of high-level object-oriented classes that abstracts working with the SDL API in a more .NET-friendly way.

You can include sdl.cs in your project using the [sdl.cs](https://www.nuget.org/packages/sdl.cs) NuGet package.

Branch|Status
---|---
develop|![develop](https://github.com/harry-cpp/sdl.cs/workflows/Build/badge.svg)

## Tutorial

The following are walkthroughs that show off various aspects of SDL and SDL#. ALl code can be found in a single solution in the `Tutorial` source subdirectory.

**NOTE:** All tutorials assume a working knowledge of C#.

Number | Title | Description
--- | --- | ---
1 | [Hello, World!](src/Tutorial/01_Hello_World/01_Hello_World.md) | Setting up the environment and creating your first SDL window
2 | [Event Driven Programming](src/Tutorial/02_Event_Driven_Programming/02_Event_Driven_Programming.md) | Responding to events and input
3 | [Drawing on the Screen](src/Tutorial/03_Drawing_on_the_Screen/03_Drawing_on_the_Screen.md) | Drawing on the screen
4 | [Working with Images](src/Tutorial/04_Working_with_Images/04_Working_with_Images.md) | Working with different image formats
5 | [Hardware Acceleration](src/Tutorial/05_Hardware_Acceleration/05_Hardware_Acceleration.md) | Using graphics hardware to speed up drawing
6 | [Drawing](src/Tutorial/06_Drawing/06_Drawing.md) | Other drawing primitives
7 | [Sprites](src/Tutorial/07_Sprites/07_Sprites.md) | Working with sprites
