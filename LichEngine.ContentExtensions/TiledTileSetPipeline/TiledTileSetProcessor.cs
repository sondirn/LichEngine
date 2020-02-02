using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [ContentProcessor(DisplayName = "Tiled TileSet Processor")]
    public class TiledTileSetProcessor : ContentProcessor<TiledTileSetContent, TiledTileSetProcessorResult>
    {
        public override TiledTileSetProcessorResult Process(TiledTileSetContent input, ContentProcessorContext context)
        {
            foreach (var item in input.Image.Source)
            {
                input.Image.Source = Path.GetFileNameWithoutExtension(input.Image.Source);
            }
            return new TiledTileSetProcessorResult(input, context.Logger);
        }
    }
}
