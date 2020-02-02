using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [XmlRoot(ElementName = "frame")]
    public class TiledTileSetAnimationFrameContent
    {
        [XmlAttribute(AttributeName = "tileid")]
        public int Tileid { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public double Duration { get; set; }
    }
}