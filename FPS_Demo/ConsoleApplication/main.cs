using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Engine.Primitives;
using Engine.Utils;
using Engine;
using Jitter;
using Jitter.Dynamics.Constraints;
using OpenTK.Graphics.OpenGL;
using FixedAngle = Jitter.Dynamics.Constraints.SingleBody.FixedAngle;

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

				Scene firstScene = game.sceneManager.AddEmptyScene("Main");
				
				GameObject ground = firstScene.Instantiate<Cube>();
				ground.transform.position = new Vector3(0, -5, 0);
				ground.transform.scale = new Vector3(100, 1, 100);
				var body = ground.AddComponent<RigidBodyComponent>();
				body.rigidBody.IsStatic = true;
				
				GameObject playerOne = firstScene.Instantiate<Cube>();
				playerOne.transform.scale = new Vector3(1, 3, 1);
				playerOne.transform.position = new Vector3(-50, 0, -50);
				playerOne.transform.rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathUtils.Deg2Rad(-135));
				playerOne.AddComponent<RigidBodyComponent>();
				var p1 = playerOne.AddComponent<PlayerMovement>();
				p1.type = Player.One;
				
				GameObject playerTwo = firstScene.Instantiate<Cube>();
				playerTwo.transform.scale = new Vector3(1, 3, 1);
				playerTwo.transform.position = new Vector3(50, 0, 50);
				playerTwo.transform.rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathUtils.Deg2Rad(45));
				playerTwo.AddComponent<RigidBodyComponent>();
				var p2 = playerTwo.AddComponent<PlayerMovement>();
				p2.type = Player.Two;

				var cameraOne = firstScene.GetGameObject("Main Camera");
				cameraOne.transform.SetParent(playerOne.transform);
				cameraOne.transform.localPosition = MathUtils.Rotate(new Vector3(0, 1, 1) * 5, playerOne.transform.rotation);
				cameraOne.transform.localRotation = Quaternion.Identity;
				var cameraOneComponent = cameraOne.GetComponent<CameraComponent>();
				cameraOneComponent.viewport.Width = 0.5f;
				
				var cameraTwo = firstScene.Instantiate<Camera>();
				cameraTwo.transform.SetParent(playerTwo.transform);
				cameraTwo.transform.localPosition = MathUtils.Rotate(new Vector3(0, 1, 1) * 5, playerTwo.transform.rotation);
				cameraTwo.transform.localRotation = Quaternion.Identity;
				var cameraTwoComponent = cameraTwo.GetComponent<CameraComponent>();
				cameraTwoComponent.viewport.Width = 0.5f;
				cameraTwoComponent.viewport.X = 0.5f;

				//firstScene.GetGameObject("Main Camera").AddComponent<CameraMouseMovement>();
				//Set the new scene as active
				game.sceneManager.ActiveScene = firstScene;
			};

			game.Run();

			return 0;
		}
	}
}