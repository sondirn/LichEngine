using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [XmlRoot(ElementName = "tileset")]
    public class TiledMapTileSetContent
    {

        [XmlAttribute(AttributeName = "firstgid")]
        public int Firstgid { get; set; }
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
    }
}