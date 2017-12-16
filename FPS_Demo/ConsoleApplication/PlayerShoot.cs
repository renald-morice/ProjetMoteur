using System.Numerics;
using Engine;
using Engine.Primitives;
using Engine.Utils;
using Jitter;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints.SingleBody;
using Jitter.LinearMath;

namespace FPS_Demo
{
    public class PlayerShoot : GameComponent, ILogicComponent
    {
        public Camera camera;
        public float fireRate = 2.0f;
        public float power = 100.0f;
        public string button;

        private float _timeSinceFire = 0.0f;
        private World _world;
        private RigidBodyComponent _body;
        
        public void Start()
        {
            _world = Game.Instance.GetSystem<PhysicSystem>().world;
            _body = gameObject.GetComponent<RigidBodyComponent>();
        }

        public void Update()
        {   
            if (_timeSinceFire > 0.0f)
            {
                _timeSinceFire -= 1.0f / Game.Instance.FPS;
            }
            else if (Input.GetButtonDown(button))
            {
                _timeSinceFire = 1.0f / fireRate;

                Vector3 origin = gameObject.transform.position;
                Vector3 direction = MathUtils.Rotate(-Vector3.UnitZ, gameObject.transform.rotation);

                origin += 2 * direction * gameObject.transform.scale.Z;

                var projectile = SceneManager.Instance.ActiveScene.Instantiate<Cube>();
                projectile.transform.scale = new Vector3(1.0f, 1.0f, 2);
                projectile.transform.position = origin;
                projectile.transform.rotation = gameObject.transform.rotation;
                
                var projectileBody = projectile.AddComponent<RigidBodyComponent>();
                projectileBody.rigidBody.AffectedByGravity = false;
                projectileBody.rigidBody.LinearVelocity = MathUtils.ToJVector(direction * power);
                _world.AddConstraint(new FixedAngle(projectileBody.rigidBody));
                
                if (Input.GetButtonDown("Shoot")) gameObject.GetComponent<SpeakerComponent>()?.Play(@"..\..\Resources\Audio\shotgun-mossberg590.mp3");
            }
        }

        public void LateUpdate()
        {
            var onlyYRotation = new Quaternion(0, camera.transform.rotation.Y, 0, camera.transform.rotation.W);
            Quaternion.Normalize(onlyYRotation);
            
            camera.transform.rotation = onlyYRotation;
        }
    }
}