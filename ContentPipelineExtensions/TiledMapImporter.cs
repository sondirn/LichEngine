using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPipelineExtensions
{
    [ContentImporter(".json", DefaultProcessor = "TiledMapProcessor", DisplayName = "Tiled Map Importer")]
    public class TiledMapImporter : ContentImporter<TiledMapContent>
    {
        public override TiledMapContent Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing JSON map: {0}", filename);
            using(var file = File.OpenText(filename))
            {
                var serializer = new JsonSerializer();
                var serializedMap = (TiledMapContent)serializer.Deserialize(file, typeof(TiledMapContent));
                return serializedMap;
            }
        }
    }
}
