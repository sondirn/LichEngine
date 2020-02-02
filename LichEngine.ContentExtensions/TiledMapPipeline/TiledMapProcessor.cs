using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [ContentProcessor(DisplayName = "Tiled Map Processor")]
    public class TiledMapProcessor : ContentProcessor<TiledMapContent, TiledMapProcessorResult>
    {
        public override TiledMapProcessorResult Process(TiledMapContent map, ContentProcessorContext context)
        {
            foreach (var item in map.TileSets)
            {
                item.Source = Path.GetFileNameWithoutExtension(item.Source);
            }
            return new TiledMapProcessorResult(map, context.Logger);
        }
    }
}