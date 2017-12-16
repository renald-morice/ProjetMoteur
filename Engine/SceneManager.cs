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
        // NOTE/FIXME/TODO(francois):
        // After numerous design changes, having a list of scenes is not straight forward
        // (and may not be the most common case):
        // components are automatically added to their respective system,
        // even if they are not in the currently displayed scene.
        // Another option would be to delay that addition by moving it oustide the constructor.
        // We would have to add components to their system _when the scene is set to active_ and when we add them
        // (assuming we can not add a component to a game object that is not in the scene).
        //
        // (All this, remembering this is not an editor but an engine so, dynamically building
        //  scenes would be done beforehand or one by one).
        //private List<Scene> _allScenes;
        private Scene activeScene;

        //----------
        //Properties
        //----------
        public Scene ActiveScene { get => activeScene; set => activeScene = value; }

        //----------
        //Constructor
        //----------

        private SceneManager() {
            //_allScenes = new List<Scene>();
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
        public Scene EmptyScene(string name = "Scene") {
            Scene scene = new Scene(name);
            // I see you...
            // FIXME: This should be accessible from anywhere,
            //  maybe make it a global, an attribute of each gameObject.
            scene.Instantiate<Camera>();

            activeScene = scene;

            //_allScenes.Add(scene);
            return scene;
        }

        /*public void AddScene(Scene scene) {
            //_allScenes.Add(scene);
        }

        public void RemoveScene(Scene scene) {
            _allScenes.Remove(scene);
        }

        //Only return the first Scene named like the parameter
        public Scene GetScene(string name) {
            return _allScenes.Find(c => c.Name == name);
        }*/

        private void UnLoadActiveScene()
        {
            if (activeScene != null)
            {
                foreach (var gameObject in activeScene.AllGameObjects)
                {
                    gameObject.Dispose();
                }
            }
        }

        public void SetActiveScene(Scene scene)
        {
            if (scene != activeScene)
            {
                UnLoadActiveScene();
                activeScene = scene;
            }
        }
        
        // NOTE(francois): On Json serialization.
        // So we are all clear, I added that today in a hurry. It is not optimal, it is merely working.
        // (btw, [JsonProperty] is just so it can serialize (and deserialize) private attributes)
        public Scene Load(string name)
        {
            // TODO: Use game settings
            string sceneDirectory = "../../Scenes/";
            string sceneFile = $"{sceneDirectory}{name}.json";

            Scene result = null;

            try
            {
                string data = File.ReadAllText(@sceneFile);
                result = JsonConvert.DeserializeObject<Scene>(data, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    TypeNameHandling = TypeNameHandling.Objects,
                    ObjectCreationHandling = ObjectCreationHandling.Reuse
                });

                // It _is_ useful after all!
                foreach (var component in result.GetAllComponents<Component>())
                {
                    component.Awake();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            if (result != null)
            {
                //AddScene(result);
                UnLoadActiveScene();
                
                activeScene = result;
            }
            
            return result;
        }
    }
}
