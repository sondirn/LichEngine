using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.Utilities.Input
{
    public class GameInput
    {
        private static KeyboardStateExtended CurrentKeyboardState;
        public static void Update()
        {
            CurrentKeyboardState = KeyboardExtended.GetState();
        }

        public static KeyboardStateExtended GetKeyboardState()
        {
            return CurrentKeyboardState;
        }

        public static bool CapsLock => CurrentKeyboardState.CapsLock;
        public static bool NumLock => CurrentKeyboardState.NumLock;
        public static bool IsShiftDown() => CurrentKeyboardState.IsShiftDown();
        public static bool IsControlDown() => CurrentKeyboardState.IsControlDown();
        public static bool IsAltDown() => CurrentKeyboardState.IsAltDown();
        public static bool IsKeyDown(Keys key) => CurrentKeyboardState.IsKeyDown(key);
        public static bool IsKeyUp(Keys key) => CurrentKeyboardState.IsKeyUp(key);
        public static Keys[] GetPressedKeys() => CurrentKeyboardState.GetPressedKeys();
        public static bool WasKeyJustDown(Keys key) => CurrentKeyboardState.WasKeyJustUp(key);
        public static bool WasKeyJustUp(Keys key) => CurrentKeyboardState.WasKeyJustDown(key);
        public static bool WasAnyKeyJustDown() => CurrentKeyboardState.WasAnyKeyJustDown();
        public static float GetAxis(string direction)
        {
            switch (direction)
            {
                case "Horizontal":
                    return (IsKeyDown(Keys.A) ? -1 : 0) + (IsKeyDown(Keys.D) ? 1 : 0);
                //: IsKeyDown(Keys.Left) ? -1 : IsKeyDown(Keys.Right) ? 1

                case "Vertical":
                    return (IsKeyDown(Keys.W) ? -1 : 0) + (IsKeyDown(Keys.S) ? 1 : 0);

                default:
                    return 0;
            }
        }
    }
}

