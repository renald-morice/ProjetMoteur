using Engine.Utils;
using OpenTK.Graphics.OpenGL;

namespace Engine.Primitives.Renderer
{
    // NOTE(francois): Well... This does not draw a cube... But it's _way_ harder!
    //  (and it uses VBO, which should be implemented later)
    public class CubeRenderer : ShapeRenderer
    {
        public CubeRenderer()
        {
            _handle = VBOUtils.CubeVbo;
            _mode = PrimitiveType.Triangles;
        }
    }
}