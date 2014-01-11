OpenWorld.Engine
================

A C# game engine based on OpenGL and OpenTK.

The engine is based on an XNA-like design but has some improvements done.
There is no sound or physics engine supported yet but it is planned to change
this later.

Features:
- Simplified handling of OpenGL 3.3 with models, textures, shaders and buffer objects
- PostProcessing Pipelines
- AssetManager (Model import with Assimp.Net)
- Gui system with some controls (oriented on System.Windows.Forms)
- Theoretic multi-platform support (Not tested, but should work)
- Docuemnted source code what fullfilles all microsoft code analysis rules
- Demo Apps

Future Planning:
- Sound (3D and music)
- Scene management with component-based entities (like Unity3D)
- Improved asset manager
- Even more demos apps
- Improved documentation (also outside of code)

To compile this, you'll need Microsoft Visual Studio 2010 or newer and following libraries:
- OpenTK (www.opentk.com)
- Assimp.Net (https://code.google.com/p/assimp-net/)
- SharpFont (included, get it with nuget or at https://github.com/Robmaister/SharpFont)

If you use it and encounter a bug, please report it to felix@masterq32.de