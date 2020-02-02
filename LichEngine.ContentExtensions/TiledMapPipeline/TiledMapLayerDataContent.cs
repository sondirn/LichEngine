using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [XmlRoot(ElementName = "data")]
    public class TiledMapLayerDataContent
    {
        [XmlElement(ElementName = "tile")]
        public List<TiledMapTileContent> Tiles { get; set; }
    }
}