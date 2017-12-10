using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using Engine.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Quaternion = System.Numerics.Quaternion;

namespace Engine
{
	public class RenderSystem : System<IRenderComponent>
	{
        public override void Iterate()
        {
	        UpdateComponentList();
	        
	        var scene = SceneManager.Instance.ActiveScene;
	        
	        List<CameraComponent> allCameras = scene.GetAllComponents<CameraComponent>();
	        if (allCameras.Count == 0) return;
	        
	        var game = Game.Instance;
	        var window = game.window;

	        // Clear whole screen to black
	        GL.Disable(EnableCap.ScissorTest);
	        
	        GL.ClearColor(Color.Black);
	        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
	        
	        GL.Enable(EnableCap.ScissorTest);
	        
	        GL.Enable(EnableCap.DepthTest);
	        
	        // Render each camera viewport
	        foreach (var camera in allCameras)
	        {
		        var cameraPos = camera.gameObject.transform.position;
		        var cameraRot = camera.gameObject.transform.rotation;
		        var cameraLens = camera.lens;
		        var cameraViewport = camera.viewport;

		        var vX = (int) (cameraViewport.X * window.Width);
		        var vY = (int) (cameraViewport.Y * window.Height);
		        var vW = (int) (cameraViewport.Width * window.Width);
		        var vH = (int) (cameraViewport.Height * window.Height);
		        
		        GL.Viewport(vX, vY, vW, vH);		        
		        
		        GL.Scissor(vX, vY, vW, vH); // Clear color area uses scissor region instead of viewport
		        GL.ClearColor(camera.clearColor);
		        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		        
		        GL.MatrixMode(MatrixMode.Projection);
		        GL.LoadIdentity();

		        GL.MatrixMode(MatrixMode.Modelview);
		        GL.LoadIdentity();

		        GL.Frustum(cameraLens.left, cameraLens.right, cameraLens.bottom, cameraLens.top,
			        	   cameraLens.nearPlane, cameraLens.farPlane);

		        // Invert camera position and rotation
		        GL.Translate(-cameraPos.X, -cameraPos.Y, -cameraPos.Z);
		        GL.Rotate(-MathUtils.DegAngleFromQuaternion(cameraRot.W), cameraRot.X, cameraRot.Y, cameraRot.Z);

		        foreach (IRenderComponent component in _components)
		        {
			        _RenderComponent(component);
		        }
	        }

	        game.window.SwapBuffers();
        }

		private void _RenderComponent(IRenderComponent component)
		{
			GL.MatrixMode(MatrixMode.Modelview);
			GL.PushMatrix();

			// FIXME: Everything that needs rendering has to be attached to a gameObject
			//  (otherwhise, there is no transform, hence no position)
			//  This check should probably be done directly when querying components.
			
			// Translate, rotate then scale the component
			if (component is GameComponent)
			{
				var gameObject = (component as GameComponent).gameObject;
				var position = gameObject.transform.position;
				var rotation = gameObject.transform.rotation;
				var scale = gameObject.transform.scale;

				var angle = MathUtils.DegAngleFromQuaternion(rotation.W);

				GL.Translate(position.X, position.Y, position.Z);
				GL.Rotate(angle, rotation.X, rotation.Y, rotation.Z);
				GL.Scale(scale.X, scale.Y, scale.Z);
			}
			
			component.Render();
			
			GL.PopMatrix();
		}
    }
}

