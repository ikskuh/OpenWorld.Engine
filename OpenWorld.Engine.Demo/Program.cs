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
					case ConsoleKey.D0:
						return;
				}
			} while(true);

		}

		static void StartDemo<T>()
			where T: Game
		{
			// Create window to host game.
			using (Window window = new Window(1024, 768))
			{
				// Create instance of game
				window.Game = Activator.CreateInstance<T>();
				
				// Start the update loop.
				window.Run(60, 60);
			}
		}
	}
}
