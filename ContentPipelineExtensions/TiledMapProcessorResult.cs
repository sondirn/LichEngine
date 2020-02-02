using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPipelineExtensions
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
