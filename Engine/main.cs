using System;
using System.Collections.Generic;

// TODO: Add a 'Scene' concept.
//       Load / Save scene (from / to JSON? : See System.Web.Script.Serialization)

namespace Engine
{
	public class main
	{
		public static int Main (String[] args)
		{
			bool quit = false;

			var allSystems = new List<ISystem>();

			// TODO: Add inputs
			allSystems.Add(new LogicSystem());
			// TODO: Add other outputs
			allSystems.Add(new RenderSystem());

			List<GameEntity> allEntities = new List<GameEntity>();

			GameObject entity = new GameObject ();
			entity.AddComponent<HelloWorldComponent>();

			allEntities.Add(entity);

			while (!quit) {
				foreach (ISystem system in allSystems) {
					system.Iterate(allEntities);
				}

				// TODO: Sleep until next frame
			}

			return 0;
		}

		public static List<T> GetAllComponents<T>(List<GameEntity> allEntities) where T : class {
			List<T> result = new List<T>();

			// FIXME: This is _so_ slow...
			foreach (GameEntity entity in allEntities) {
				List<T> components = entity.GetComponents<T>();
				components.RemoveAll (c => (c as Component).isActive == false);

				result.AddRange(components);
			}

			return result;
		}
	}
}

