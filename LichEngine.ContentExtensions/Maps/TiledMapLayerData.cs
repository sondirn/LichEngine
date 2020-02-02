using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "data")]
    public class TiledMapLayerData
    {
        [XmlElement(ElementName = "tile")]
        public List<TiledMapTile> Tile { get; set; }
    }
}