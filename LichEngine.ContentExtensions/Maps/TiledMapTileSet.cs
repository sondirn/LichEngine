using Newtonsoft.Json;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "tileset")]
    public class TiledMapTileSet
    {
        [XmlAttribute(AttributeName = "firstgid")]
        public int Firstgid { get; set; }
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
    }
}