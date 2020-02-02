using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPipelineExtensions
{
    [ContentProcessor(DisplayName = "Tiled Map Processor")]
    public class TiledMapProcessor : ContentProcessor<TiledMapContent, TiledMapProcessorResult>
    {
        public override TiledMapProcessorResult Process(TiledMapContent input, ContentProcessorContext context)
        {
            return new TiledMapProcessorResult(input, context.Logger);
        }
    }
}
