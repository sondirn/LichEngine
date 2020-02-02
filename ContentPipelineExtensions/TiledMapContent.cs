using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPipelineExtensions
{
    public class TiledMapContent
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("infinite")]
        public bool Infinite { get; set; }

        [JsonProperty("layers")]
        public TiledMapLayerContent[] Layers { get; set; }

        [JsonProperty("tileheight")]
        public long Tileheight { get; set; }

        [JsonProperty("tilewidth")]
        public long Tilewidth { get; set; }

        [JsonProperty("tilesets")]
        public TiledMapTileSetContent[] Tilesets { get; set; }
    }
}
