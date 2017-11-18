using System.Drawing;

namespace Engine
{
    public class CameraComponent : GameComponent
    {
        // FIXME: Near and Far plane values are totalyy bogus. Use something sensible!
        // (Far plane seems to be the z-limit after which there is no rendering)
        // TODO: See relation between vision angle and those values (they seem to depend on nearPlane too).
        public class Lens
        {
            public float
                left = -1.0f,
                right = 1.0f,
                bottom = -1.0f,
                top = 1.0f;
            
            public float
                nearPlane = 1.5f,
                farPlane = 200.0f;
        }

        public Lens lens = new Lens();
        public RectangleF viewport = new RectangleF(0.0f, 0.0f, 1.0f, 1.0f);
        public Color clearColor = Color.Black;
    }
}