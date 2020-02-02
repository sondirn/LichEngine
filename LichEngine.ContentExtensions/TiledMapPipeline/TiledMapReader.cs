using LichEngine.ContentExtensions.HelperObjects;
using LichEngine.ContentExtensions.Maps;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LichEngine.ContentExtensions.TiledMapPipeline
{
    public class TiledMapReader : ContentTypeReader<TiledMap>
    {
        protected override TiledMap Read(ContentReader input, TiledMap existingInstance)
        {
            var data = input.ReadString();
            TiledMap mapresult = XmlHelper.XmlToObject<TiledMap>(data);
            return mapresult;
        }

        
    }
}
