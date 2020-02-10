using LichEngine.GameCode.Components;
using LichEngine.Portable.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;
using Nez.Sprites;
using Nez.Tiled;

namespace LichEngine.GameCode.Scenes
{
    class SandBoxScene : Scene
    {
        public TmxMap TiledMap;
        public bool testtest;
        public override void Initialize()
        {
            testtest = false;
            base.Initialize();
            ClearColor = Color.DarkBlue;
            SetDesignResolution(640, 360, SceneResolutionPolicy.ShowAll);
            

            TiledMap = Content.LoadTiledMap("Content/TiledMaps/CaveTest.tmx");
            var spawnObject = TiledMap.GetObjectGroup("objects").Objects["spawn"];

            var tiledEntity = CreateEntity("tiledmap");
            tiledEntity.AddComponent(new TiledMapRenderer(TiledMap, "main"));
            
            var player = CreateEntity("player", new Vector2(spawnObject.X, spawnObject.Y));
            var playerComponent = new Player();
            player.AddComponent(playerComponent);
            System.Console.WriteLine(player.Id);

            var test = CreateEntity("test", new Vector2(spawnObject.X, spawnObject.Y));
            test.AddComponent(new StaticObject());



        }
        public override void Update()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                Core.Scene = new SandBoxScene();
            }
            base.Update();
        }

        
    }
}
