using System;
using System.Collections.Generic;

namespace Engine
{
	public class LogicSystem : ISystem
	{
		public void Iterate(List<GameEntity> allEntities) {
			var allComponents = main.GetAllComponents<ILogicComponent>(allEntities);

			foreach (ILogicComponent component in allComponents) {
				component.Update();
			}
		}
	}
}

