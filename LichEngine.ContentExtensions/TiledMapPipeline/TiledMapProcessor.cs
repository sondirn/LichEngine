using Microsoft.Xna.Framework.Content.Pipeline;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [ContentProcessor(DisplayName = "Tiled Map Processor")]
    public class TiledMapProcessor : ContentProcessor<TiledMapContent, TiledMapProcessorResult>
    {
        public override TiledMapProcessorResult Process(TiledMapContent map, ContentProcessorContext context)
        {
            return new TiledMapProcessorResult(map, context.Logger);
        }
    }
}