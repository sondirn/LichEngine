using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
	[XmlRoot(ElementName = "tileset")]
	public class TiledTileSetContent
    {
		[XmlElement(ElementName = "image")]
		public TiledTileSetImageContent Image { get; set; }
		
		[XmlElement(ElementName = "tile")]
		public List<TiledTileSetTileContent> Tile { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "tilewidth")]
		public int Tilewidth { get; set; }
		[XmlAttribute(AttributeName = "tileheight")]
		public int Tileheight { get; set; }
		[XmlAttribute(AttributeName = "tilecount")]
		public int Tilecount { get; set; }
		[XmlAttribute(AttributeName = "columns")]
		public int Columns { get; set; }
	}
}