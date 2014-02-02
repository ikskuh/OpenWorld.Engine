using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Select Demo:");
			Console.WriteLine("1: Simple Window");
			Console.WriteLine("2: Gui Demo");
			Console.WriteLine("3: 3D and Asset Demo");
			Console.WriteLine("4: Scene Management Demo");
			Console.WriteLine("0: Exit");

			do{
				var key = Console.ReadKey(true);
				switch(key.Key)
				{
					case ConsoleKey.D1:
						StartDemo<BasicSetup>();
						return;
					case ConsoleKey.D2:
						StartDemo<GuiDemo>();
						return;
					case ConsoleKey.D3:
						StartDemo<AssetDemo>();
						return;
					case ConsoleKey.D4:
						StartDemo<SceneManagementDemo>();
						return;
					case ConsoleKey.D0:
						return;
				}
			} while(true);

		}

		static void StartDemo<T>()
			where T: Game
		{
			// Create the game and run it.
			var game = Activator.CreateInstance<T>();
			game.Run();
		}
	}
}
