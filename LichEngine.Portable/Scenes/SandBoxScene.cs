using LichEngine.GameCode.Components;
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
            var tex = Content.Load<Texture2D>(Nez.Content.Textures.DefaultTexture);
            test.AddComponent(new SpriteRenderer(tex));
            var collider = test.AddComponent<BoxCollider>();
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);


            var test1 = CreateEntity("test", new Vector2(spawnObject.X + 100, spawnObject.Y));
            var tex1 = Content.Load<Texture2D>(Nez.Content.Textures.DefaultTexture);
            test1.AddComponent(new SpriteRenderer(tex1));
            var collider1 = test1.AddComponent<BoxCollider>();
            Flags.SetFlagExclusive(ref collider1.CollidesWithLayers, 0);



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
