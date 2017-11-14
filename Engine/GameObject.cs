using System;

namespace Engine
{
	public class GameObject : GameEntity
	{
		// NOTE (françois): transform itself can not be modified, only its attributes.
        public Transform transform { get; private set; }= new Transform();
	    public string Name { get; private set; }
	    
        public GameObject(string name) {
            this.Name = name;
        }
    }
}

