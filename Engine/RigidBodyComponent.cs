using System;
using System.Numerics;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;

namespace Engine
{
    // TODO: Allow specifying/modifying the shape
    public class RigidBodyComponent : GameComponent, IPhysicComponent
    {
        // NOTE(francois): This should be private. And we should have wrappers.
        //  But no TIME.
        // FIXME: JitterPhysics does not implement kinematic bodies (bodies without collisions).
        // NOTE(francois): Unlike Unity, RigidBody and Collider are the same.
        public RigidBody rigidBody;
        
        // TODO: Add OnGround property

        public override void Awake()
        {
            var scale = gameObject.transform.scale;
            rigidBody = new RigidBody(new BoxShape(2 *scale.X, 2 * scale.Y, 2 * scale.Z));
            
            PhysicSystem system = Game.Instance.GetSystem<PhysicSystem>();
            system.world.AddBody(rigidBody);

            rigidBody.AllowDeactivation = false;
            CopyState();
        }

        public void CopyState()
        {
            var position = gameObject.transform.position;
            var rotation = gameObject.transform.rotation;
            
            rigidBody.Position = new JVector(position.X, position.Y, position.Z);
            // NOTE(francois): I tried using JMatrix.CreateFromAxisAngle and it did not work (both in rad and deg). 
            rigidBody.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(rotation.X, rotation.Y, rotation.Z, rotation.W));
        }
        
        public void ApplyChanges()
        {
            var position = rigidBody.Position;
            var rotation = JQuaternion.CreateFromMatrix(rigidBody.Orientation);
            
            gameObject.transform.position = new Vector3(position.X, position.Y, position.Z);
            gameObject.transform.rotation = new Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W);
        }
        
        public void PreUpdate()
        {
            
        }
        
        public void FixedUpdate()
        {
            
        }

        public void PostUpdate()
        {
            
        }

        ~RigidBodyComponent()
        {
            PhysicSystem system = Game.Instance.GetSystem<PhysicSystem>();
            system.world.RemoveBody(rigidBody);
        }
    }
}