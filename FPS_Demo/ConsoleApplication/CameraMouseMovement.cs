using System;
using System.Numerics;
using Engine;

namespace Engine
{
    public class CameraMouseMovement : GameComponent, ILogicComponent
    {
        public float speed = 0.1f;

        public void Start()
        {
        }
        
        public void Update()
        {
            float badMouseBeahviour = 10  - (Input.mouseWheel / 2);
            
            var movement = new Vector3(0, 0, 0);
		
            if (Input.GetButton("Left2")) movement.X -= 1;
            if (Input.GetButton("Right2")) movement.X += 1;
		
            if (Input.GetButton("Down2")) movement.Y -= 1;
            if (Input.GetButton("Up2")) movement.Y += 1;

            var length = movement.Length();
            if (length != 0) movement /= length;
		
            movement *= speed * ((badMouseBeahviour * badMouseBeahviour / 10) + 1);

            gameObject.transform.position +=  movement;
            
            var position = gameObject.transform.position;
            gameObject.transform.position = new Vector3(position.X, position.Y, badMouseBeahviour * Math.Abs(badMouseBeahviour));  
        }
    }
}