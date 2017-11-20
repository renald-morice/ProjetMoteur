﻿using System;
using System.Numerics;
using Engine.Utils;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;

namespace Engine
{
    public class RigidBodyComponent : GameComponent, IPhysicComponent
    {
        // NOTE(francois): This should be private. And we should have wrappers.
        //  But no TIME.
        public RigidBody rigidBody;

        public RigidBodyComponent()
        {
            rigidBody = new RigidBody(new BoxShape(1, 1, 1));
            
            PhysicSystem system = Game.Instance.GetSystem<PhysicSystem>();
            system.world.AddBody(rigidBody);
        }
        
        public void PreUpdate()
        {
            var position = gameObject.transform.position;
            var rotation = gameObject.transform.rotation;
            
            rigidBody.Position = new JVector(position.X, position.Y, position.Z);
            rigidBody.Orientation = JMatrix.CreateFromAxisAngle(new JVector(rotation.X, rotation.Y, rotation.Z),
                                                                MathUtils.AngleFromQuaternion(rotation.W));
        }

        public void FixedUpdate()
        {
        }

        public void PostUpdate()
        {
            var position = rigidBody.Position;
            var rotation = JQuaternion.CreateFromMatrix(rigidBody.Orientation);
            
            gameObject.transform.position = new Vector3(position.X, position.Y, position.Z);
            gameObject.transform.rotation = new Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W);
        }
    }
}