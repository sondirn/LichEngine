using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
	[XmlRoot(ElementName = "map")]
	public class TiledMapContent
	{



		[XmlElement(ElementName = "tileset")]
		public List<TiledMapTileSetContent> TileSets { get; set; }
		[XmlElement(ElementName = "layer")]
		public List<TiledMapLayerContent> MapLayers { get; set; }

		[XmlAttribute(AttributeName = "width")]
		public int Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public int Height { get; set; }
		[XmlAttribute(AttributeName = "tilewidth")]
		public int Tilewidth { get; set; }
		[XmlAttribute(AttributeName = "tileheight")]
		public int Tileheight { get; set; }

		public string Name { get; set; }
	}
}
