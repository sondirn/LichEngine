using Newtonsoft.Json;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    public class TiledMapTileSetContent
    {
        [JsonProperty("firstgid")]
        public long Firstgid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}