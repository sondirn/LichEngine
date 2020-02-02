using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [XmlRoot(ElementName = "tile")]
    public class TiledTileSetTileContent
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "animation")]
        public TiledTileSetTileAnimationContent Animation { get; set; }
    }
}