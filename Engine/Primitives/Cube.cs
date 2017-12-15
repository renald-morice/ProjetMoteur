using Engine.Primitives.Renderer;

namespace Engine.Primitives
{
    public class Cube : GameObject
    {
        // FIXME?: See FIXME? in GameObject
        public Cube() : base("Cube")
        {
        }
        
        public Cube(string name) : base(name)
        {
        }

        public override void Init()
        {
            AddComponent<CubeRenderer>();
        }
    }
}