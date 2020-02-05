using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "animation")]
    public class TiledTileSetTileAnimation
    {
        [XmlElement(ElementName = "frame")]
        public List<TiledTileSetAnimationFrame> Frame { get; set; }
    }
}