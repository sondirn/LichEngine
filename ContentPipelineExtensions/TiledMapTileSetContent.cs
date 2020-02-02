using Newtonsoft.Json;

namespace ContentPipelineExtensions
{
    public class TiledMapTileSetContent
    {
        [JsonProperty("firstgid")]
        public long Firstgid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}