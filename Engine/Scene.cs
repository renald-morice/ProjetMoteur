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

            
            var window = Game.Instance.window;
            
            // I see you...
            // FIXME: This should be accessible from anywhere,
            //  maybe make it a global, an attribute of each gameObject.
            Instantiate<Camera>();
        }

        //----------
        //Methods
        //----------
        public GameObject AddEmptyGameObject(string name="GameObject") {
            GameObject gameObject = new GameObject(name);

            _allGameObjects.Add(gameObject);
            return gameObject;
        }

        // TODO: Should this take any parameters (position, rotation, name, ...?)
        public T Instantiate<T>() where T : GameObject
        {
            T result = (T) Activator.CreateInstance(typeof(T), new object[] {});
            
            _allGameObjects.Add(result);
            return result;
        }

        public void RemoveGameObject(GameObject gameObject) {
            _allGameObjects.Remove(gameObject);
        }

        //Only return the first GameObject named like the parameter
        public GameObject GetGameObject(string name) {
            return _allGameObjects.Find(c => c.Name == name);
        }

        //Get all components in the GameObjects of the scene
        //A GameObject is a child of GameEntity
        public List<T> GetAllComponents<T>(bool activeComponentOnly=true) where T : class {

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
