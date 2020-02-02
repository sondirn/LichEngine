using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPipelineExtensions
{
    public class TiledMapWriter : ContentTypeWriter<TiledMapProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return string.Empty;
        }



        protected override void Write(ContentWriter output, TiledMapProcessorResult value)
        {
            throw new NotImplementedException();
        }

        
    }
}
