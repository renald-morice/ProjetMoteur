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

            /*----------------------------------------------------*/
            /* RMO 8/11/17 : new encapsulation with Scene's class */
            /*----------------------------------------------------*/
            /*List<GameEntity> allEntities = new List<GameEntity>();

			GameObject entity = new GameObject ();
			entity.AddComponent<HelloWorldComponent>();

			allEntities.Add(entity);

            while (!quit) {
				foreach (ISystem system in allSystems) {
					system.Iterate(allEntities);
				}

				// TODO: Sleep until next frame
			}*/

            SceneManager sceneManager = SceneManager.Instance;

            //Scene's creation with one object (one component in it)
            Scene firstScene = sceneManager.AddEmptyScene("firstScene");
            GameObject firstObject = firstScene.AddEmptyGameObject("FirstObject");
            firstObject.AddComponent<HelloWorldComponent>();

            //Set the new scene as active
            sceneManager.ActiveScene = firstScene;

            while (!quit) {
                foreach (ISystem system in allSystems) {
                    if(sceneManager.ActiveScene != null) system.Iterate(sceneManager.ActiveScene);
                }

                // TODO: Sleep until next frame
            }

            return 0;
		}

        /*----------------------------------------------------*/
        /* RMO 8/11/17 : new encapsulation with Scene's class */
        /*----------------------------------------------------*/
        /*public static List<T> GetAllComponents<T>(List<GameEntity> allEntities) where T : class {
			List<T> result = new List<T>();

			// FIXME: This is _so_ slow...
			foreach (GameEntity entity in allEntities) {
				List<T> components = entity.GetComponents<T>();
				components.RemoveAll (c => (c as Component).isActive == false);

				result.AddRange(components);
			}

			return result;
		}*/
    }
}

