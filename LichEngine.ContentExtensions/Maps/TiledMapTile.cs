using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "tile")]
    public class TiledMapTile
    {
        [XmlAttribute(AttributeName = "gid")]
        public int Gid { get; set; }
    }
}