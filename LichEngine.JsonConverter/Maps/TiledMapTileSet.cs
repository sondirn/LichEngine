using Newtonsoft.Json;

namespace LichEngine.ContentExtensions.Maps
{
    public class TiledMapTileSet
    {
        [JsonProperty("firstgid")]
        public long Firstgid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}