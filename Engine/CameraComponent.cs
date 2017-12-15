using System;
using System.Drawing;

namespace Engine
{
    public class CameraComponent : GameComponent, ILogicComponent
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

        public void Start() {
            
        }

        public void Update() {

            FMOD.VECTOR positionFmodVect;
            positionFmodVect.x = gameObject.transform.position.X;
            positionFmodVect.y = gameObject.transform.position.Y;
            positionFmodVect.z = gameObject.transform.position.Z;

            // TODO: add true velocity of camera
            FMOD.VECTOR velocityFmodVect;
            velocityFmodVect.x = 0.0f;
            velocityFmodVect.y = 0.0f;
            velocityFmodVect.z = 0.0f;

            FMOD.VECTOR forward;
            forward.x = 0.0f;
            forward.y = 0.0f;
            forward.z = 1.0f;

            FMOD.VECTOR up;
            up.x = 0.0f;
            up.y = 1.0f;
            up.z = 0.0f;

            //AudioMaster.Instance.GetFmodSystem().set3DListenerAttributes(0, ref positionFmodVect, ref velocityFmodVect, ref forward, ref up);
        }
        
        public void LateUpdate() {}
    }
}