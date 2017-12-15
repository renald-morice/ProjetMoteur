using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Engine.Primitives;
using Engine.Utils;
using Engine;
using Engine.Primitives.Renderer;

// TODO:
//       Load / Save scene (from / to JSON? : See System.Web.Script.Serialization)

namespace FPS_Demo
{
	public class main
	{
		private static void MakePlayer(Scene scene, Player type, out GameObject player, out Camera camera)
		{
			player = scene.Instantiate<Cube>();
			player.transform.scale = new Vector3(1, 3, 1);
			player.AddComponent<RigidBodyComponent>();
			//player.AddComponent<SpeakerComponent>();
			
			var p = player.AddComponent<PlayerMovement>();
			p.type = type;

			switch (type)
			{
				case Player.One:
				{
					player.transform.position = new Vector3(-50, 0, -50);
					player.transform.rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathUtils.Deg2Rad(-135));
					camera = scene.GetGameObject("Main Camera") as Camera;		
				} break;
				case Player.Two:
				{
					player.transform.position = new Vector3(50, 0, 50);
					player.transform.rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathUtils.Deg2Rad(45));
					camera = scene.Instantiate<Camera>();
				} break;
					
				default:
				{
					throw new Exception("toto");
				}
			}

			p.camera = camera;
			
			camera.transform.SetParent(player.transform);
			camera.transform.SetLocalPosition(new Vector3(0, 1, 1) * 5);
			camera.transform.localRotation = Quaternion.Identity;
			var cameraComponent = camera.GetComponent<CameraComponent>();
			
			cameraComponent.viewport.Width = 0.5f;

			var shoot = player.AddComponent<PlayerShoot>();
			shoot.camera = camera;
			
			switch (type)
			{
				case Player.One:
				{
					shoot.button = "Fire1";
				} break;

				case Player.Two:
				{
					cameraComponent.viewport.X = 0.5f;
					shoot.button = "Fire2"; // TODO: Change this to a numpad key
				} break;
			}
		} 
	
		[STAThread]
		public static int Main(String[] args)
		{
			Game game = Game.Instance;

			game.window.Load += (sender, e) =>
			{
				// TODO: Load default scene.
				//    	   (First, create a metadata file for the game with the necessary settings
				//       (see TODO about GameWindow), as well as a default scene to load)

				/*Scene firstScene = game.sceneManager.AddEmptyScene("Main");
				
				GameObject ground = firstScene.Instantiate<Cube>();
				ground.transform.position = new Vector3(0, -5, 0);
				ground.transform.scale = new Vector3(100, 1, 100);
				var body = ground.AddComponent<RigidBodyComponent>();
				body.isStatic = true;
			

				GameObject playerOne;
				Camera cameraOne;
				MakePlayer(firstScene, Player.One, out playerOne, out cameraOne);
				
				GameObject playerTwo;
				Camera cameraTwo;
				MakePlayer(firstScene, Player.Two, out playerTwo, out cameraTwo);
				
				firstScene.Save();*/
				game.sceneManager.Load("Main");

				//firstScene.GetGameObject("Main Camera").AddComponent<CameraMouseMovement>();
				//Set the new scene as active
				//game.sceneManager.ActiveScene = firstScene;
			};

			game.Run();

			return 0;
		}
	}
}