using System;
using System.Collections.Generic;

namespace Engine
{
	public class LogicSystem : ISystem
	{
        /*----------------------------------------------------*/
        /* RMO 8/11/17 : new encapsulation with Scene's class */
        /*----------------------------------------------------*/
        /*public void Iterate(List<GameEntity> allEntities){
			var allComponents = main.GetAllComponents<ILogicComponent>(allEntities);

			foreach (ILogicComponent component in allComponents) {
				component.Update();
			}
		}*/

        public void Iterate(Scene scene) {
            List<ILogicComponent> allComponents = scene.GetAllActiveComponents<ILogicComponent>();

            foreach (ILogicComponent component in allComponents) {
                component.Update();
            }
        }

    }
}

