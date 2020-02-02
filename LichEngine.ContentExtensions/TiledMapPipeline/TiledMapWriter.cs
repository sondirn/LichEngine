using LichEngine.ContentExtensions.Maps;
using LichEngine.ContentExtensions.TiledMapPipeline;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [ContentTypeWriter]
    public class TiledMapWriter : ContentTypeWriter<TiledMapProcessorResult>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform) =>     typeof(TiledMapContent).AssemblyQualifiedName;

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => typeof(TiledMapReader).AssemblyQualifiedName;

        protected override void Write(ContentWriter output, TiledMapProcessorResult value)
        {
            string json = JsonConvert.SerializeObject(value.Map);

            output.Write(json);
        }
    }
}
