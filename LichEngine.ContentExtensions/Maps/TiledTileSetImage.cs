using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "image")]
    public class TiledTileSetImage
    { 
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }
    }
}