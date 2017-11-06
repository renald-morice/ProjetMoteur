using System;
using System.Collections.Generic;

namespace Engine
{
	public interface ISystem
	{
		void Iterate(List<GameEntity> components);
	}
}

