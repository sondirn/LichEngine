using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "frame")]
    public class TiledTileSetAnimationFrame
    {
        [XmlAttribute(AttributeName = "tileid")]
        public int Tileid { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public double Duration { get; set; }
    }
}