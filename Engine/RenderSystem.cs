using System;
using System.Collections.Generic;

namespace Engine
{
	public class RenderSystem : ISystem
	{
		public void Iterate(List<GameEntity> allEntities) {
			var allComponents = main.GetAllComponents<IRenderComponent>(allEntities);

			foreach (IRenderComponent component in allComponents) {
				component.Render();
			}
		}
	}
}

