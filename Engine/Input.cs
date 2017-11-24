using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace Engine
{
    public class Input
    {
        private static MouseDevice _mouse;
        private static KeyboardDevice _keyboard;
        
        private static Dictionary<string, Key> _buttonMap = new Dictionary<string, Key>();
        // NOTE(francois)/@HACK: This a quick hack to know if a button has just been pressed / released.
        private static Dictionary<string, bool> _buttonStatus = new Dictionary<string, bool>();

        public enum Axis
        {
            Horizontal,
            Vertical
        }

        public static void Init()
        {
            _mouse = Game.Instance.window.Mouse;
            _keyboard = Game.Instance.window.Keyboard;

            // TODO: Load from a settings file.
            // TODO: Handle Azerty/Qwerty keyboards.
            _buttonMap["Left1"] = Key.A;
            _buttonMap["Left2"] = Key.Left;
            
            _buttonMap["Right1"] = Key.D;
            _buttonMap["Right2"] = Key.Right;
            
            _buttonMap["Up1"] = Key.W;
            _buttonMap["Up2"] = Key.Up;
            
            _buttonMap["Down1"] = Key.S;
            _buttonMap["Down2"] = Key.Down;
            
            _buttonMap["Fire1"] = Key.A;
            _buttonMap["Fire2"] = Key.E;
            
            _buttonMap["Jump"] = Key.Space;
            
            SaveOldButtonsStatus();
        }

        public static void SaveOldButtonsStatus()
        {
            foreach (var b in _buttonMap)
            {
                var status = GetButton(b.Key);
                _buttonStatus[b.Key] = status;
            }
        }

        private static bool GetOldButton(string name)
        {
            try
            {
                var status = _buttonStatus[name];
                return status;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine($"Invalid button name '{name}'.");
                return false;
            }
        }
        
        public static bool GetButton(string name)
        {
            try
            {
                var key = _buttonMap[name];
                return _keyboard[key];
            }
            catch (Exception e)
            {
                Console.Out.WriteLine($"Invalid button name '{name}'.");
                return false;
            }
        }
        
        // FIXME: This seems to not register every down and up event.
        // TODO: Use _keyboard.onDown to modify _buttonStatus directly (and remove SaveOldButtonsStatus),
        // just keep it for Init (or not).
        public static bool GetButtonDown(string name)
        {
            return !GetOldButton(name) && GetButton(name);
        }

        public static bool GetButtonUp(string name)
        {
            return GetOldButton(name) && !GetButton(name);
        }

        // TODO: Add joystick handling
        public static float GetAxis(Axis axis)
        {
            float result = 0;
            
            switch (axis)
            {
                case Axis.Horizontal:
                    if (GetButton("Left1") || GetButton("Left2")) result -= 1;
                    if (GetButton("Right1") || GetButton("Right2")) result += 1;
                    break;
                    
                case Axis.Vertical:
                    if (GetButton("Down1") || GetButton("Down2")) result -= 1;
                    if (GetButton("Up1") || GetButton("Up2")) result += 1;
                    break;
            }

            // TODO: (When joysticks are handled)
            // Return now if result is != 0 (buttons have priority over the joysticks)
            
            return result;
        }
    }
}