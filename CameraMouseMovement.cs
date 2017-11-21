using System;
using System.Numerics;
using OpenTK.Input;

namespace Engine
{
    public class CameraMouseMovement : GameComponent, ILogicComponent
    {
        private MouseDevice _mouse;
        private KeyboardDevice _keyStates;

        public float speed = 0.1f;

        public void Start()
        {
        }
        
        public void Update()
        {
            // TODO: Btw, this should be in its own Input namespace or whatever, instead of querying it
            //  from game.window (even if it just returns whatever game.window stores).
            if (_mouse == null) _mouse = Game.Instance.window.Mouse;
            
            float badMouseBeahviour = 10  - (_mouse.WheelPrecise / 2);
            
            var movement = new Vector3(0, 0, 0);

            if (_keyStates == null) _keyStates = Game.Instance.window.Keyboard;
		
            if (_keyStates[Key.Left]) movement.X -= 1;
            if (_keyStates[Key.Right]) movement.X += 1;
		
            if (_keyStates[Key.Down]) movement.Y -= 1;
            if (_keyStates[Key.Up]) movement.Y += 1;

            var length = movement.Length();
            if (length != 0) movement /= length;
		
            movement *= speed * ((badMouseBeahviour * badMouseBeahviour / 10) + 1);

            gameObject.transform.position +=  movement;
            
            var position = gameObject.transform.position;
            gameObject.transform.position = new Vector3(position.X, position.Y, badMouseBeahviour * Math.Abs(badMouseBeahviour));  
        }
    }
}