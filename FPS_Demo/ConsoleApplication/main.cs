using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Engine.Primitives;
using Engine.Utils;
using Engine;

// TODO:
//       Load / Save scene (from / to JSON? : See System.Web.Script.Serialization)

namespace FPS_Demo
{
	public class main
	{
		[STAThread]
		public static int Main(String[] args)
		{
			Game game = Game.Instance;

			game.window.Load += (sender, e) =>
			{
				// TODO: Load default scene.
				//    	   (First, create a metadata file for the game with the necessary settings
				//       (see TODO about GameWindow), as well as a default scene to load)

				//Scene's creation with one object (one component in it)
				Scene firstScene = game.sceneManager.AddEmptyScene("firstScene");
				GameObject firstObject = firstScene.AddEmptyGameObject("FirstObject");
				//firstObject.AddComponent<HelloWorldComponent>();
				firstObject.transform.position = new Vector3(0, 1, 5);
				firstObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), MathUtils.Deg2Rad(180));

				GameObject secondObject = firstScene.Instantiate<Cube>();
				secondObject.AddComponent<HelloWorldComponent>();
				secondObject.AddComponent<RigidBodyComponent>();

				for (int i = 0; i < 10; ++i)
				{
					var cube = firstScene.Instantiate<Cube>();
					cube.transform.position = new Vector3(0, 10 + i * 2, 0);
					cube.AddComponent<RigidBodyComponent>();
				}

				GameObject ground = firstScene.Instantiate<Cube>();
				ground.transform.position = new Vector3(0, -5, 0);
				ground.transform.scale = new Vector3(100, 1, 100);
				var body = ground.AddComponent<RigidBodyComponent>();
				body.rigidBody.IsStatic = true;

				var camera = firstScene.GetGameObject("Main Camera");
				camera.AddComponent<CameraMouseMovement>();

				/*
				var cameraComponent = firstObject.AddComponent<CameraComponent>();
				cameraComponent.viewport.X = 0.5f;
				cameraComponent.viewport.Y = 0.5f;
				cameraComponent.viewport.Width = 0.5f;
				cameraComponent.viewport.Height = 0.5f;
				cameraComponent.clearColor = Color.White;
	
				var tutu = firstObject.AddComponent<CameraComponent>();
				tutu.viewport.X = 0.5f;
				tutu.viewport.Y = 0.0f;
				tutu.viewport.Width = 0.5f;
				tutu.viewport.Height = 0.5f;
				tutu.clearColor = Color.Gray;
				tutu.lens.left = -0.2f;
				tutu.lens.right = 0.2f;
				tutu.lens.bottom = -0.2f;
				tutu.lens.top = 0.2f;
				 */
				var toto = camera.GetComponent<CameraComponent>();
				//toto.viewport.Width = 0.5f;
				toto.clearColor = Color.DarkBlue;

				//Set the new scene as active
				game.sceneManager.ActiveScene = firstScene;
			};

			game.Run();

			return 0;
		}
	}
}