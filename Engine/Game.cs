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
        public SceneManager sceneManager;
        
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
                _game.allSystems.Add(new PhysicSystem(1.0f / _game.FPS, 10));
                // TODO: Add other outputs
                _game.allSystems.Add(new RenderSystem());

                _game.sceneManager = SceneManager.Instance;
            };

            window.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, window.Width, window.Height);
            };

            window.UpdateFrame += (sender, e) =>
            {
                // TODO: See NOTE below. This should be moved to whatever place the input handling is done.
                if (window.Keyboard[Key.Escape]) _game.quit = true;

                if (_game.sceneManager.ActiveScene != null)
                {
                    foreach (ISystem system in _game.allSystems)
                    {
                        system.Iterate();
                    }
                    
                    foreach (ISystem system in _game.allSystems) {
                        system.LateIterate();
                    }
                    
                    foreach (ISystem system in _game.allSystems) {
                        system.BuryComponents();
                    }
                    
                    //Update AudioMaster
                    //AudioMaster.Instance.GetFmodSystem().update();
                }

                // NOTE(francois): This is done here, because input handling is also a 'system'. So the quit event is
                //  recorded in the foreach loop above.
                //  Another solution would be to handle inputs diffently (it is always the first system anyway).
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
