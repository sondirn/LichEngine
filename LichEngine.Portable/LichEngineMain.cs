using LichEngine.GameCode.Scenes;
using Nez;
using Nez.Console;

namespace LichEngine.Portable
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LichEngineMain : Core
    {
        public LichEngineMain() : base(1280, 720, false, true, "LichEngine")
        {
            //Core.DebugRenderEnabled = true;
            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Scene = new SandBoxScene();
            ExitOnEscapeKeypress = false;
        }

        [Command("debugrender", "Toggles rendering of Debug elements")]
        private static void DebugRender()
        {
            DebugRenderEnabled = !DebugRenderEnabled;
        }

        [Command("fullscreen", "Toggles fullscreen of game window")]
        private static void ScreenMode()
        {
            Screen.SetSize(Screen.MonitorWidth, Screen.MonitorHeight);
            Screen.IsFullscreen = !Screen.IsFullscreen;
            Screen.ApplyChanges();
        }

        [Command("resolution", "changes resolution of window, default is 1280x720. Resolution is based off of height in a 16:9 aspect ratio")]
        private static void ChangeResolution(int height = 720)
        {
            var minHeight = height / 9;
            var trueWidth = minHeight * 16;
            Screen.SetSize(trueWidth, height);
        }

        [Command("exit", "Exits the game")]
        private static void ExitGame()
        {
            Core.Exit();
        }
    }
}