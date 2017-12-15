﻿using System;
using System.Numerics;
using Newtonsoft.Json;

namespace Engine
{
	public class GameObject : GameEntity
	{
		// NOTE(françois): transform itself can not be modified, only its attributes.
		[JsonProperty]
        public Transform transform { get;  private set; } = new Transform();
		
		// TODO?/FIXME?: Btw, should this be moved to GameEntity?
		[JsonProperty]
	    public string Name { get; private set; }
	    
	   	// FIXME?: The fact that this constructor needs a name means that every derived class of GameObject
		//  needs to implement it so they can be instantiated in Scene.Instantiate<T>.
		//  Otherwhise, there needs to be two different constructors, one which takes a name and another that does not.
		//  (A default value for the parameter did not change anything)
        public GameObject(string name) {
            this.Name = name;
        }

		virtual public void Init()
		{
		}
	}
}
