using LichEngine.GameCode.Components;
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
            SetDesignResolution(640, 360, SceneResolutionPolicy.ShowAll);
            

            var tiledMap = Content.LoadTiledMap("Content/TiledMaps/MapTest.tmx");
            var tiledEntity = CreateEntity("tiledmap");
            tiledEntity.AddComponent(new TiledMapRenderer(tiledMap));
            
            var player = CreateEntity("player");
            player.AddComponent(new Player());
            System.Console.WriteLine(player.Id);
            
            var camera = CreateEntity("player");
            var _camera = new FollowCamera(player);
            camera.AddComponent(_camera);
            _camera.FollowLerp = .1f;

            var box = CreateEntity("box");
            box.Transform.Position = new Vector2(129, 129);
            box.AddComponent(new CircleCollider(32));

            AddRenderer(new DefaultRenderer(camera: _camera.Camera));

        }

        
    }
}
