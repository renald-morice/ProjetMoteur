using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Engine;
using Engine.Utils;
using Jitter.LinearMath;

namespace FPS_Demo
{
    public enum Player
    {
        One,
        Two
    }
    
    public class PlayerMovement : GameComponent, ILogicComponent
    {
        public float speed = 10.0f;
        public Player type;
        public Camera camera;
        
        private RigidBodyComponent _rigidBody;

        public void Start()
        {
            _rigidBody = gameObject.GetComponent<RigidBodyComponent>();
        }
        
        public void Update()
        {
            var movement = new Vector3(0, 0, 0);

            if (type == Player.One)
            {
                if (Input.GetButton("Left1")) movement.X -= 1;
                if (Input.GetButton("Right1")) movement.X += 1;

                if (Input.GetButton("Down1")) movement.Z += 1;
                if (Input.GetButton("Up1")) movement.Z -= 1;
                
                if (Input.GetButtonDown("Jump")) SceneManager.Instance.Load("Main");
            }
            else
            {
                if (Input.GetButton("Left2")) movement.X -= 1;
                if (Input.GetButton("Right2")) movement.X += 1;

                if (Input.GetButton("Down2")) movement.Z += 1;
                if (Input.GetButton("Up2")) movement.Z -= 1;
            }

            var length = movement.Length();
            if (length != 0) movement /= length;

            movement = MathUtils.Rotate(movement, camera.transform.rotation);
            
            _rigidBody.rigidBody.AddForce(new JVector(movement.X, movement.Y, movement.Z) * speed * 500.0f);
        }
        
        public void LateUpdate() {}
    }
}