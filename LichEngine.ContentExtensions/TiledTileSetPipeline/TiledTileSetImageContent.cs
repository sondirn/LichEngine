using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [XmlRoot(ElementName = "image")]
    public class TiledTileSetImageContent
    {
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }
    }
}