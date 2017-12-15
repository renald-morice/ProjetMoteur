using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Engine {
    public class SceneManager {

        //----------
        //Attributes
        //----------
        private static SceneManager instance = null;
        private List<Scene> _allScenes;
        private Scene activeScene;

        //----------
        //Properties
        //----------
        public Scene ActiveScene { get => activeScene; set => activeScene = value; }

        //----------
        //Constructor
        //----------

        private SceneManager() {
            _allScenes = new List<Scene>();
            ActiveScene = null;
        }

        //Singleton non-thread safe !! (Use lock if needed)
        public static SceneManager Instance {
            get {
                if (instance == null) {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        //----------
        //Methods
        //----------
        public Scene AddEmptyScene(string name = "Scene") {
            Scene scene = new Scene(name);
            // I see you...
            // FIXME: This should be accessible from anywhere,
            //  maybe make it a global, an attribute of each gameObject.
            scene.Instantiate<Camera>();

            _allScenes.Add(scene);
            return scene;
        }

        public void AddScene(Scene scene) {
            _allScenes.Add(scene);
        }

        public void RemoveScene(Scene scene) {
            _allScenes.Remove(scene);
        }

        //Only return the first Scene named like the parameter
        public Scene GetScene(string name) {
            return _allScenes.Find(c => c.Name == name);
        }

        // TODO: Need to unload previous scene (if load succeded)
        public Scene Load()
        {
            // TODO: Use game settings
            string sceneDirectory = "../../Scenes/";
            string sceneFile = sceneDirectory + "Main" + ".json";

            Scene result = null;

            try
            {
                string data = File.ReadAllText(@sceneFile);
                result = JsonConvert.DeserializeObject<Scene>(data, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    TypeNameHandling = TypeNameHandling.Objects,
                    ObjectCreationHandling = ObjectCreationHandling.Reuse
                });
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            System.Console.WriteLine(result);

            if (result != null) activeScene = result;
            
            return result;
        }
    }
}
