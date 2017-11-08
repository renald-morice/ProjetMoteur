using System;
using System.Collections.Generic;

namespace Engine
{
	public class RenderSystem : ISystem
	{
        /*----------------------------------------------------*/
        /* RMO 8/11/17 : new encapsulation with Scene's class */
        /*----------------------------------------------------*/
        /*public void Iterate(List<GameEntity> allEntities) {
			var allComponents = main.GetAllComponents<IRenderComponent>(allEntities);

			foreach (IRenderComponent component in allComponents) {
				component.Render();
			}
		}*/

        public void Iterate(Scene scene) {
            List<IRenderComponent> allComponents = scene.GetAllComponents<IRenderComponent>();

            foreach (IRenderComponent component in allComponents) {
                component.Render();
            }
        }

    }
}

