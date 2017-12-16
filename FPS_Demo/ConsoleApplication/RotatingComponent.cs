using System;
using System.Numerics;
using Engine;
using Engine.Utils;

namespace FPS_Demo
{
    public class RotatingComponent : GameComponent, ILogicComponent
    {
        public Vector3 axis = Vector3.Zero;
        public float speed;
        
        private RigidBodyComponent _body;
        
        public void Start()
        {
            _body = gameObject.GetComponent<RigidBodyComponent>();
        }

        public void Update()
        {
            _body.rigidBody.AngularVelocity = MathUtils.ToJVector(axis * speed);
        }

        public void LateUpdate()
        {

        }
    }
}