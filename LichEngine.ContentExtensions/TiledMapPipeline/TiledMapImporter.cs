using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [ContentImporter(".tmx", DefaultProcessor = "TiledMapProcessor",
            DisplayName = "Tiled Map Importer")]
    public class TiledMapImporter : ContentImporter<TiledMapContent>
    {
        public override TiledMapContent Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing TMX Map: {0}", filename);
            XmlSerializer serializer = new XmlSerializer(typeof(TiledMapContent));
            using(FileStream stream = new FileStream(filename, FileMode.Open))
            {
                TiledMapContent result = (TiledMapContent)serializer.Deserialize(stream);
                return result;
            }
        }
    }
}
