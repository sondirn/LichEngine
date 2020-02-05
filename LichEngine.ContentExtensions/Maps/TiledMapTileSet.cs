using LichEngine.ContentExtensions.TiledTileSetPipeline;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
    [XmlRoot(ElementName = "tileset")]
    public class TiledMapTileSet
    {
		[XmlElement(ElementName = "image")]
		public TiledTileSetImage Image { get; set; }

		[XmlElement(ElementName = "tile")]
		public List<TiledTileSetTile> Tiles { get; set; }
		[XmlAttribute(AttributeName = "firstgid")]
		public int Firstgid { get; set; }
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