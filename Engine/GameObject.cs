using System;

namespace Engine
{
	public class GameObject : GameEntity
	{
        private string _name;
        private Transform transform;

        public GameObject(string name) {
            this.Name = name;
        }

        public string Name { get => _name; set => _name = value; }
        public Transform Transform { get => transform; set => transform = value; }

    }
}

