using Engine.Utils;
using OpenTK.Graphics.OpenGL;

namespace Engine.Primitives.Renderer
{
    public class ShapeRenderer : GameComponent, IRenderComponent
    {
        protected VBOUtils.Vbo<VertexPositionColor> _handle;
        protected PrimitiveType _mode;
        
        public void Render()
        {
            VBOUtils.DrawVBO(_mode, ref _handle);
        }
    }
}