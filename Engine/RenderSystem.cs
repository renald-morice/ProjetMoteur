using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
	public class RenderSystem : ISystem
	{
        public void Iterate(Scene scene) {
            List<IRenderComponent> allComponents = scene.GetAllComponents<IRenderComponent>();
	        
	        var game = Game.Instance;

	        // render graphics
	        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
 
	        GL.MatrixMode(MatrixMode.Projection);
	        GL.LoadIdentity();
	        
	        // TODO: Use camera position and orientation
	        //  (also, check to see if a component is in view (and all that jazz) before drawing it)
	        
	        // TODO: Btw, this should be in its own Input namespace or whatever, instead of querying it
	        //  from game.window (even if it just returns whatever game.window stores).
	        double badMouseBeahviour = 1  - (game.window.Mouse.WheelPrecise / 2);
	        if (badMouseBeahviour < 0) badMouseBeahviour = 0;
	        
	        GL.Ortho(-badMouseBeahviour, badMouseBeahviour, -badMouseBeahviour, badMouseBeahviour, 0.0, 4.0);

	        foreach (IRenderComponent component in allComponents)
	        {
		        _RenderComponent(component);
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

				var angle = Math.Acos(rotation.W) * 2 * 180 / Math.PI;

				GL.Translate(position.X, position.Y, position.Z);
				// FIXME: Rotation in X and Y are broken!
				GL.Rotate(angle, rotation.X, rotation.Y, rotation.Z);
				GL.Scale(scale.X, scale.Y, scale.Z);
			}
			
			component.Render();
			
			GL.PopMatrix();
		}
    }
}

