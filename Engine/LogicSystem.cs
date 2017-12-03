using System;
using System.Collections.Generic;

namespace Engine
{
	public class LogicSystem : System<ILogicComponent>
	{
        public override void Iterate() {
            foreach (var component in _newComponents)
            {
                component.Start();
            }
            
            UpdateComponentList();
            
            foreach (var component in _components) {
                component.Update();
            }
        }
	}
}

