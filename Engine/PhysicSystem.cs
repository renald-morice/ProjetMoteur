using System.Collections.Generic;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.LinearMath;
using OpenTK.Graphics;

namespace Engine
{
    public class PhysicSystem : System<IPhysicComponent>
    {
        private readonly int updatesPerFrame;
        
        private float _frameTime;
        public World world {get; private set;}
        
        public PhysicSystem(float frameTime, int updatesPerFrame = 1)
        {
            this.updatesPerFrame = updatesPerFrame;
            _frameTime = frameTime;
            
            world = new World(new CollisionSystemSAP());
        }
        
        public override void Iterate()
        {
            UpdateComponentList();
            
            foreach (var component in _components)
            {
                component.PreUpdate();
            }

            for (int i = 0; i < updatesPerFrame; ++i)
            {
                world.Step(_frameTime / updatesPerFrame, true);
                
                foreach (var component in _components)
                {
                    component.FixedUpdate();
                }
            }

            foreach (var component in _components)
            {
                component.PostUpdate();
            }
        }
    }
}