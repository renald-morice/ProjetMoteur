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
		        component.Render();
	        }

	        game.window.SwapBuffers();
        }
    }
}

