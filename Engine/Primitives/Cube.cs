namespace Engine.Primitives
{
    public class Cube : GameObject
    {
        // FIXME?: See FIXME? in GameObject
        public Cube() : base("Cube")
        {
            AddComponent<CubeRenderer>();
        }
        
        public Cube(string name) : base(name)
        {
            AddComponent<CubeRenderer>();
        }
    }
}