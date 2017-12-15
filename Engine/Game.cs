using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using Engine.Primitives;
using Engine.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace Engine
{
    // NOTE(francois): This is a singleton simply because I can not be bothered to care right now...
    public class Game
    {
        private static Game _game;
        
        private List<ISystem> allSystems;
        private SceneManager sceneManager;
        //private Scene scene;
        
        public GameWindow window { get; private set; }

        public bool quit;
        public double FPS { get; private set; }
        
        public static Game InitGame()
        {
            _game = new Game();
            // TODO: Load game settings (screen width, height, framerate, ...)
            _game.window = new GameWindow();
            _game.FPS = 60;

            var window = _game.window;
                
            window.Load += (sender, e) =>
            {
                // setup settings, load textures, sounds
                window.VSync = VSyncMode.On;
                
                _game.allSystems = new List<ISystem>();

                // TODO: Add inputs
                _game.allSystems.Add(new LogicSystem());
                // TODO: Add other outputs
                _game.allSystems.Add(new RenderSystem());

                _game.sceneManager = SceneManager.Instance;

                // TODO: Load default scene.
                //       (First, create a metadata file for the game with the necessary settings
                //       (see TODO about GameWindow), as well as a default scene to load)

                //Scene's creation with one object (one component in it)
                
                Scene firstScene = _game.sceneManager.AddEmptyScene("firstScene");
                GameObject firstObject = firstScene.AddEmptyGameObject("FirstObject");
                //firstObject.AddComponent<HelloWorldComponent>();
                firstObject.transform.position = new Vector3(0, 1, 5);
                firstObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), MathUtils.Deg2Rad(180));
                
                GameObject secondObject = firstScene.Instantiate<Cube>();
                secondObject.AddComponent<HelloWorldComponent>();
                
                var camera = firstScene.GetGameObject("Main Camera"); 
                camera.AddComponent<CameraMouseMovement>();

                var cameraComponent = firstObject.AddComponent<CameraComponent>();
                cameraComponent.viewport.X = 0.5f;
                cameraComponent.viewport.Y = 0.5f;
                cameraComponent.viewport.Width = 0.5f;
                cameraComponent.viewport.Height = 0.5f;
                cameraComponent.clearColor = Color.White;

                var tutu = firstObject.AddComponent<CameraComponent>();
                tutu.viewport.X = 0.5f;
                tutu.viewport.Y = 0.0f;
                tutu.viewport.Width = 0.5f;
                tutu.viewport.Height = 0.5f;
                tutu.clearColor = Color.Gray;
                tutu.lens.left = -0.2f;
                tutu.lens.right = 0.2f;
                tutu.lens.bottom = -0.2f;
                tutu.lens.top = 0.2f;

                var toto = camera.GetComponent<CameraComponent>();
                toto.viewport.Width = 0.5f;
                toto.clearColor = Color.DarkBlue;

                //Serializizing a created scene: firstScene
                firstScene.Save("sceneSave.xml");

                //Deserializing an existing Scene
                //TODO: Loading from the xml file isn't working we can only get the name of the scene
                //A problem occur to return the GameObject
                Scene loadScene = Scene.LoadFromFile("sceneSave.xml");
                loadScene.Save("loadedScene.xml");

                //Alternative solution use an empty scene that we fill with all GameObjects
                Scene testScene = _game.sceneManager.AddEmptyScene("testScene.xml");
                firstObject.Save("objectSave.xml");
                secondObject.Save("objectSave2.xml");
                GameObject testObject = testScene.AddEmptyGameObject("testObject");
                GameObject testObject2 = testScene.AddEmptyGameObject("testObject2");
                testObject = GameObject.LoadFromFile("objectSave.xml");
                testObject2 = GameObject.LoadFromFile("objectSave2.xml");
                testObject.Save("testObject.xml");
                testObject2.Save("testObject2.xml");
                testScene.Save("testScene.xml");
                
                //Set the new scene as active                
                _game.sceneManager.ActiveScene = firstScene;
            };

            window.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, window.Width, window.Height);
            };

            window.UpdateFrame += (sender, e) =>
            {
                // TODO: See NOTE below. This should be moved to whatever place the input handling is done.
                if (window.Keyboard[Key.Escape]) _game.quit = true;   
            
                foreach (ISystem system in _game.allSystems) {
                    if (_game.sceneManager.ActiveScene != null) system.Iterate(_game.sceneManager.ActiveScene);
                }

                // NOTE(francois): This is done here, because input handling is also a 'system'. So the quit event is
                //  recorded in the foreach loop above.
                //  Another solution would be to handle inputs diffently (it is always the fist system anyway).
                if (_game.quit) window.Exit();
                
                Console.Out.WriteLine(_game.window.RenderTime);
            };

            return _game;
        }

        public void Run()
        {
             _game.window.Run(_game.FPS);   
        }

        public static Game Instance
        {
            get
            {
                if (_game != null) return _game;

                _game = InitGame();

                return _game;
            }
        }
    }
}