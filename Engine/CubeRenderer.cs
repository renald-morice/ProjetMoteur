using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    // NOTE(francois): Well... This does not draw a cube... But it's _way_ harder!
    //  (and it uses VBO, which should be implemented later)
    public class CubeRenderer : GameComponent, IRenderComponent
    {
        
        // FIXME: If we do everything this way, the engine will be _really_ slow.
        //  But for now, it is better than nothing!
        public void Render()
        {
            GL.Begin(PrimitiveType.Triangles);

            // TODO: Use translation, rotation, scale, ...
            
            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(-1.0f, 1.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(-1.0f, -1.0f);
            GL.Color3(Color.IndianRed);
            GL.Vertex2(1.0f, 1.0f);
            
            GL.Color3(Color.IndianRed);
            GL.Vertex2(1.0f, 1.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f, -1.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(-1.0f, -1.0f);
 
            GL.End();
        }
    }
}