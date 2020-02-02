using Microsoft.Xna.Framework.Content.Pipeline;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    public class TiledTileSetProcessorResult
    {
        public TiledTileSetContent TileSet;
        public ContentBuildLogger logger;

        public TiledTileSetProcessorResult(TiledTileSetContent input, ContentBuildLogger logger)
        {
            this.TileSet = input;
            this.logger = logger;
        }
    }
}