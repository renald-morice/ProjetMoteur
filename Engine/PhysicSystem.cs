using System.Collections.Generic;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.LinearMath;
using OpenTK.Graphics;

namespace Engine
{
    public class PhysicSystem : ISystem
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
        
        public void Iterate(Scene scene)
        {
            List<IPhysicComponent> allComponents = scene.GetAllComponents<IPhysicComponent>();
            
            foreach (var component in allComponents)
            {
                component.PreUpdate();
            }

            for (int i = 0; i < updatesPerFrame; ++i)
            {
                world.Step(_frameTime / updatesPerFrame, true);
                
                foreach (var component in allComponents)
                {
                    component.FixedUpdate();
                }
            }

            foreach (var component in allComponents)
            {
                component.PostUpdate();
            }
        }
    }
}