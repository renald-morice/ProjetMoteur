using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Engine{

    public class Scene {

        //----------
        //Attributes
        //----------
        [JsonProperty]
        private string _name;
        [JsonProperty]
        private List<GameObject> _allGameObjects;

        //----------
        //Properties
        //----------
        [JsonIgnore]
        public string Name { get => _name; set => _name = value; }
        [JsonIgnore]
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
            gameObject.Init(); // In case we decide to do something anyway.
            
            return gameObject;
        }

        // TODO: Should this take any parameters (position, rotation, name, ...?)
        public T Instantiate<T>() where T : GameObject
        {
            T result = (T) Activator.CreateInstance(typeof(T), new object[] {});
            
            _allGameObjects.Add(result);
            result.Init();
            
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

        public bool Save()
        {
            // TODO: Use game settings
            string sceneDirectory = "../../Scenes/";
            string sceneFile = sceneDirectory + _name + ".json";

            string result = "";

            try
            {
                System.IO.Directory.CreateDirectory(@sceneDirectory);
                
                result = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.All,
                        TypeNameHandling = TypeNameHandling.Objects
                    }
                );
                
                File.WriteAllText(@sceneFile, result);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return false;
            }

            System.Console.WriteLine(result);
            return true;
        }
    }
}
