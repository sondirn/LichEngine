using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "tile")]
    public class TiledTileSetTile
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "animation")]
        public TiledTileSetTileAnimation Animation { get; set; }
    }
}