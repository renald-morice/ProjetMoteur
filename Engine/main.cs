using System;
using System.Collections.Generic;
using System.Numerics;

// TODO:
//       Load / Save scene (from / to JSON? : See System.Web.Script.Serialization)

namespace Engine
{
	public class main
	{
		[STAThread]
		public static int Main (String[] args)
		{

            Game game = Game.Instance;
			
			game.Run();

            return 0;
		}
    }
}

