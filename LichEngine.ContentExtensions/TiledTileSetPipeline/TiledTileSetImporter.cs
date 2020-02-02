using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [ContentImporter(".tsx", DefaultProcessor = "TiledTileSetProcessor", DisplayName ="Tiled TileSet Importer")]
    public class TiledTileSetImporter : ContentImporter<TiledTileSetContent>
    {
        public override TiledTileSetContent Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing TSX Tileset: {0}", filename);
            XmlSerializer serializer = new XmlSerializer(typeof(TiledTileSetContent));
            using(FileStream stream = new FileStream(filename, FileMode.Open))
            {
                TiledTileSetContent result = (TiledTileSetContent)serializer.Deserialize(stream);
                return result;
            }
        }
    }
}
