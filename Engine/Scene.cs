using System;
using System.Collections.Generic;

namespace Engine{

    public class Scene {

        //----------
        //Attributes
        //----------
        private string _name;
        private List<GameObject> _allGameObjects;

        //----------
        //Properties
        //----------
        public string Name { get => _name; set => _name = value; }
        public List<GameObject> AllGameObjects { get => _allGameObjects; }

        //----------
        //Constructor
        //----------
        public Scene(string name) {
            _name = name;
            _allGameObjects = new List<GameObject>();
        }

        //----------
        //Methods
        //----------
        public GameObject AddEmptyGameObject(string name="GameObject") {
            GameObject gameObject = new GameObject(name);

            _allGameObjects.Add(gameObject);
            return gameObject;
        }

        public void RemoveGameObject(GameObject gameObject) {
            _allGameObjects.Remove(gameObject);
        }

        //Only return the first GameObject named like the parameter
        public GameObject GetGameObject(string name) {
            return _allGameObjects.Find(c => c.Name == name);
        }

        //Get all active components in the GameObjects of the scene
        //A GameObject is a child of GameEntity
        public List<T> GetAllActiveComponents<T>(bool activeComponentOnly=true) where T : class {

            List<T> result = new List<T>();

            // FIXME: This is _so_ slow...
            foreach (GameObject gameObject in _allGameObjects) {
                List<T> components = ((GameEntity)gameObject).GetComponents<T>();
                result.AddRange(components.FindAll(c => (c as Component).isActive == activeComponentOnly));  
            }

            return result;
        }


    }
}
