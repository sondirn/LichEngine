using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
	[XmlRoot(ElementName = "map")]
	public class TiledMap
    {
		[XmlElement(ElementName = "tileset")]
		public List<TiledMapTileSet> Tileset { get; set; }
		[XmlElement(ElementName = "layer")]
		public List<TiledMapLayer> Layer { get; set; }

		[XmlAttribute(AttributeName = "width")]
		public int Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public int Height { get; set; }
		[XmlAttribute(AttributeName = "tilewidth")]
		public int Tilewidth { get; set; }
		[XmlAttribute(AttributeName = "tileheight")]
		public int Tileheight { get; set; }

	}
}
