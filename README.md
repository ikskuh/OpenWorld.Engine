OpenWorld.Engine
================

A C# game engine based on OpenGL and OpenTK.

The engine is based on a XNA-like design but has some improvements done.
There is no sound or physics engine supported yet but it is planned to change
this later.

Features:
- Simplified handling of OpenGL 3.3 with models, textures, shaders and buffer objects
- PostProcessing Pipelines
- Flexible AssetManager (Model import with Assimp.Net)
- Gui system oriented on System.Windows.Forms
- Jitter physics
- Easy localization system
- Scene management with component-based entities (like Unity3D)
- Theoretic multi-platform support (Not tested, but should work)
- Documented source code which fulfilles all microsoft code analysis rules
- Demo Apps

Future Planning:
Look at the Feature-TODO.txt to see what is planned and what is work-in-progress.

Get the project with git:
https://github.com/MasterQ32/OpenWorld.Engine

To compile this, you'll need Microsoft Visual Studio 2013 and following libraries:
- OpenTK (www.opentk.com)
- Assimp.Net (https://code.google.com/p/assimp-net/)
- SharpFont (included, get it with nuget or at https://github.com/Robmaister/SharpFont)

If you use it and encounter a bug, please report it to with GitHub issues.