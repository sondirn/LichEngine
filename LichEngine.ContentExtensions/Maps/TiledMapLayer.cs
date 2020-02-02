using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.Maps
{
	[XmlRoot(ElementName = "layer")]
	public class TiledMapLayer
    {
		[XmlElement(ElementName = "data")]
		public TiledMapLayerData Data { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public int Id { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public int Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public int Height { get; set; }
	}
}