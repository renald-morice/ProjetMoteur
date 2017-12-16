using System;
using System.Numerics;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using Newtonsoft.Json;

namespace Engine
{
    // TODO: Allow specifying/modifying the shape
    public class RigidBodyComponent : GameComponent, IPhysicComponent
    {
        // NOTE(francois): This should be private. And we should have wrappers.
        //  But no TIME.
        // FIXME: JitterPhysics does not implement kinematic bodies (bodies without collisions).
        // NOTE(francois): Unlike Unity, RigidBody and Collider are the same.
        // FIXME: Because RigidBody does not have a default constructor (and I do not have the time to find another way),
        // it is not possible to serialize it.
        // The current "solution" is to move everything that is needed up here.
        [JsonIgnore]
        public RigidBody rigidBody;
        public bool isStatic;
        
        // TODO: Add OnGround property

        // FIXME/TODO: As it stands, setting the gameObject transform before adding a rigid body,
        // or first adding the rigid body and then modifying the transform, is... totally different!
        // This is not intended and must be fixed!
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
            rigidBody.IsStatic = isStatic;
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

        public override void OnDestroy()
        {
            PhysicSystem system = Game.Instance.GetSystem<PhysicSystem>();
            system.world.RemoveBody(rigidBody);
        }
    }
}