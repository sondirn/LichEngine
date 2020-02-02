﻿using Newtonsoft.Json;

namespace LichEngine.ContentExtensions.Maps
{
    public class TiledMapLayer
    {
        [JsonProperty("data")]
        public long[] Data { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opacity")]
        public long Opacity { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }
}