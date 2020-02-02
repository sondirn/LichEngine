using LichEngine.ContentExtensions.HelperObjects;
using LichEngine.ContentExtensions.Maps;
using LichEngine.ContentExtensions.TiledMapPipeline;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    [ContentTypeWriter]
    public class TiledMapWriter : ContentTypeWriter<TiledMapProcessorResult>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform) =>     typeof(TiledMapContent).AssemblyQualifiedName;

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => typeof(TiledMapReader).AssemblyQualifiedName;

        protected override void Write(ContentWriter output, TiledMapProcessorResult value)
        {
            string data = XmlHelper.ObjectToXml(value.Map);
            output.Write(data);
        }
    }
}
