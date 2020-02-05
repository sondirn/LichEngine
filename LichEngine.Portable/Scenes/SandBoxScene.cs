﻿using LichEngine.GameCode.Components;
using Microsoft.Xna.Framework;
using Nez;

namespace LichEngine.GameCode.Scenes
{
    class SandBoxScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            ClearColor = Color.CornflowerBlue;
            SetDesignResolution(640, 360, SceneResolutionPolicy.FixedHeight);
            AddRenderer(new DefaultRenderer());

            var tiledMap = Content.LoadTiledMap("Content/TiledMaps/MapTest.tmx");
            var tiledEntity = CreateEntity("tiled map");
            tiledEntity.AddComponent(new TiledMapRenderer(tiledMap));

            var player = CreateEntity("player");
            player.AddComponent(new Player());
            
        }

        
    }
}
