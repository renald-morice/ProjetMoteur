using System;
using System.Collections.Generic;

namespace Engine
{
	public interface ISystem
	{
        void Iterate(Scene scene);
    }
}

