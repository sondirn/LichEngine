using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [XmlRoot(ElementName = "animation")]
    public class TiledTileSetTileAnimationContent
    {
        [XmlElement(ElementName = "frame")]
        public List<TiledTileSetAnimationFrameContent> Frame { get; set; }
    }
}