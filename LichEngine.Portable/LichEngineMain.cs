using LichEngine.GameCode.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;
using Nez.Tiled;
using System.IO;


namespace LichEngine.Portable
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LichEngineMain : Core
    {

        public LichEngineMain() : base(1280, 720, false,true, "LichEngine")
        {
            //Core.DebugRenderEnabled = true;
            IsFixedTimeStep = true;

        }

        protected override void Initialize()
        {
            base.Initialize();
            Scene = new SandBoxScene();
            //Screen.SetSize(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            //Window.IsBorderless = true;
        }


        [Command("debugrender", "Toggles rendering of Debug elements")]
        static void DebugRender()
        {
            DebugRenderEnabled = !DebugRenderEnabled;
        }

        [Command("fullscreen", "Toggles fullscreen of game window")]
        static void FullScreen()
        {
            Screen.SetSize(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            Screen.IsFullscreen = true;
            //Instance.Window.IsBorderless = !Instance.Window.IsBorderless;
        }

        [Command("resolution", "changes resolution of window, default is 1280x720. Resolution is based off of height in a 16:9 aspect ratio")]
        static void ChangeResolution(int height = 720)
        {
            var minHeight = height / 9;
            var trueWidth = minHeight * 16;
            Screen.SetSize(trueWidth, height);
        }
    }

    
}

