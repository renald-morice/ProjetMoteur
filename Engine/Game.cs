using System;
using System.Collections.Generic;
using Engine.Primitives;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
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
                firstObject.AddComponent<HelloWorldComponent>();

                GameObject secondObject = firstScene.Instantiate<Cube>();
                secondObject.AddComponent<HelloWorldComponent>();

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