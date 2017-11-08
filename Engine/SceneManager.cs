using System;
using System.Collections.Generic;

namespace Engine {

    class SceneManager {

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

            _allScenes.Add(scene);
            return scene;
        }

        public void RemoveScene(Scene scene) {
            _allScenes.Remove(scene);
        }

        //Only return the first Scene named like the parameter
        public Scene GetGameObject(string name) {
            return _allScenes.Find(c => c.Name == name);
        }

    }
}
