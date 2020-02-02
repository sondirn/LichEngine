using LichEngine.ContentExtensions.HelperObjects;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.ContentExtensions.TiledTileSetPipeline
{
    [ContentTypeWriter]
    public class TiledTileSetWriter : ContentTypeWriter<TiledTileSetProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform) => string.Empty;
        public override string GetRuntimeType(TargetPlatform targetPlatform) => string.Empty;
        protected override void Write(ContentWriter output, TiledTileSetProcessorResult value)
        {
            string data = XmlHelper.ObjectToXml(value.TileSet);
            output.Write(data);
        }
    }
}
