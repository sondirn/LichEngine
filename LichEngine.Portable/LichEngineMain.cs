﻿using LichEngine.GameCode.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
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

    }

    
}

