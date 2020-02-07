using LichEngine.GameCode.Components;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace LichEngine.GameCode.Scenes
{
    class SandBoxScene : Scene
    {
        public TmxMap TiledMap;
        public override void Initialize()
        {
            base.Initialize();
            ClearColor = Color.CornflowerBlue;
            SetDesignResolution(640, 360, SceneResolutionPolicy.ShowAll);
            

            TiledMap = Content.LoadTiledMap("Content/TiledMaps/CaveTest.tmx");
            var spawnObject = TiledMap.GetObjectGroup("objects").Objects["spawn"];

            var tiledEntity = CreateEntity("tiledmap");
            tiledEntity.AddComponent(new TiledMapRenderer(TiledMap, "main"));
            
            var player = CreateEntity("player", new Vector2(spawnObject.X, spawnObject.Y));
            var playerComponent = new Player();
            player.AddComponent(playerComponent);
            System.Console.WriteLine(player.Id);
            
            

            //var box = CreateEntity("box");
            //box.Transform.Position = new Vector2(129, 129);
            //box.AddComponent(new CircleCollider(32));

            //AddRenderer(new DefaultRenderer(camera: player));

        }

        
    }
}
