using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [XmlRoot(ElementName = "tile")]
    public class TiledMapTileContent
    {
        [XmlAttribute(AttributeName = "gid")]
        public int Gid { get; set; }
    }
}