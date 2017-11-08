using System;
using System.Collections.Generic;

namespace Engine
{
	public interface ISystem
	{
        /*----------------------------------------------------*/
        /* RMO 8/11/17 : new encapsulation with Scene's class */
        /*----------------------------------------------------*/
        //void Iterate(List<GameEntity> components);
        void Iterate(Scene scene);
    }
}

