using System;
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
	    
		// NOTE/TODO(francois): The library we use for serialization works better if it can use a default constructor.
		// 	To ensure that the serialization is flawless, _nothing_ must be done inside the constructor
		// (aside from what is done here).
		// This is why there is a Init method below, that is called when the entity is added.
		// For example, if you want to add a default component to a GameObject, do it in Init.
		// Why? Because, if done inside the constructor, when intializing, there would be (depending on the settings):
		// - either _two_ components (one when the constructor is called, and one added later by the deserialization)
		// - or one component inside the GameObject but _two_ registered on their system (same reason as above).
		// The TODO is to find a better way to do things, or have people more aware of this issue
		// (not many people will read this comment!)
		//
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
