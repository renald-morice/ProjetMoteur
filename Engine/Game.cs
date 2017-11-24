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
        
        public GameWindow window { get; private set; }

        public bool quit;
        public float FPS { get; private set; }
        
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
                
                Input.Init();
                
                // TODO: Add inputs
                _game.allSystems.Add(new LogicSystem());
                _game.allSystems.Add(new PhysicSystem(1.0f / _game.FPS, 1));
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
                secondObject.AddComponent<RigidBodyComponent>();

                for (int i = 0; i < 10; ++i)
                {
                    var cube = firstScene.Instantiate<Cube>();
                    cube.transform.position = new Vector3(0, 10 + i * 2, 0);
                    cube.AddComponent<RigidBodyComponent>();
                }

                GameObject ground = firstScene.Instantiate<Cube>();
                ground.transform.position = new Vector3(0, -5, 0);
                ground.transform.scale = new Vector3(100, 1, 100);
                var body = ground.AddComponent<RigidBodyComponent>();
                body.rigidBody.IsStatic = true;
                
                var camera = firstScene.GetGameObject("Main Camera"); 
                camera.AddComponent<CameraMouseMovement>();

                /*
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
                 */
                var toto = camera.GetComponent<CameraComponent>();
                //toto.viewport.Width = 0.5f;
                toto.clearColor = Color.DarkBlue;

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
                    if (_game.sceneManager.ActiveScene != null) system.Iterate();
                }

                // NOTE(francois): This is done here, because input handling is also a 'system'. So the quit event is
                //  recorded in the foreach loop above.
                //  Another solution would be to handle inputs diffently (it is always the fist system anyway).
                if (_game.quit) window.Exit();
                
                Input.SaveOldButtonsStatus();
                
                //Console.Out.WriteLine(1.0f / (window.UpdateTime + window.RenderTime));
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

        public T GetSystem<T>() where T : class // FIXME: Be more precise than just 'class'.
        {
            T result = allSystems.Find(s => s is T) as T;
            
            return result ;
        }

        public void RegisterComponent(Component component)
        {
            foreach (var system in allSystems)
            {
                if (system.IsValidComponent(component)) system.TrackComponent(component);
            }
        }

        public void UnregisterComponent(Component component)
        {
            foreach (var system in allSystems)
            {
                if (system.IsValidComponent(component)) system.UntrackComponent(component);
            }
        }
    }
}