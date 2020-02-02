using Microsoft.Xna.Framework.Content.Pipeline;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    public class TiledMapProcessorResult
    {
        public TiledMapContent Map;
        public ContentBuildLogger Logger;

        public TiledMapProcessorResult(TiledMapContent map, ContentBuildLogger logger)
        {
            Map = map;
            Logger = logger;
        }
    }
}