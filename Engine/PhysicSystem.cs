using System;
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
        
        // NOTE(francois)/FIXME: Forces are dependent on the number of physics updates per frame.
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
            
            foreach (var component in _components)
            {
                component.CopyState();
            }

            float updateTime = _frameTime / updatesPerFrame;
            
            for (int i = 0; i < updatesPerFrame; ++i)
            {    
                world.Step(updateTime, true);
                
                foreach (var component in _components)
                {
                    component.ApplyChanges();
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