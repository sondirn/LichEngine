﻿using LichEngine.ContentExtensions.HelperObjects;
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
            WriteTiledMap(output, value.Map);
        }

        public static void WriteTiledMap(ContentWriter writer, TiledMapContent map)
        {
            writer.Write(map.Width);
            writer.Write(map.Height);
            writer.Write(map.Tilewidth);
            writer.Write(map.Tileheight);
            writer.Write(map.TileSets.Count);
            foreach (TiledMapTileSetContent tileSet in map.TileSets)
            {
                WriteTiledMapTiledSet(writer, tileSet);
            }
            writer.Write(map.MapLayers.Count);
            foreach (TiledMapLayerContent mapLayer in map.MapLayers)
            {
                WriteTiledMapLayer(writer, mapLayer);
            }
        }

        public static void WriteTiledMapTiledSet(ContentWriter writer, TiledMapTileSetContent tileSet)
        {
            writer.Write(tileSet.Firstgid); //int
            writer.Write(tileSet.Name);//string
            writer.Write(tileSet.Tilewidth);//int
            writer.Write(tileSet.Tileheight);//int
            writer.Write(tileSet.Tilecount);//int
            writer.Write(tileSet.Columns);//int
            //Image content
            writer.Write(tileSet.Image.Source);//string
            writer.Write(tileSet.Image.Width);//int
            writer.Write(tileSet.Image.Height);//int
            //TileSetTileContent
            writer.Write(tileSet.Tiles.Count);
            foreach (var item in tileSet.Tiles)
            {
                if (item.Animation != null)
                {
                    writer.Write(item.Id);
                    //animation
                    writer.Write(item.Animation.Frame.Count);
                    foreach (var frame in item.Animation.Frame)
                    {
                        writer.Write(frame.Tileid);
                        writer.Write(frame.Duration);
                    }
                }
                
            }
        }

        public static void WriteTiledMapLayer(ContentWriter writer, TiledMapLayerContent mapLayer)
        {
            //Write data on this liine ----- 
            writer.Write(mapLayer.Id);
            writer.Write(mapLayer.Width);
            writer.Write(mapLayer.Height);
            writer.Write(mapLayer.Name);
            WriteTiledMapLayerData(writer, mapLayer.Data);
        }
        public static void WriteTiledMapLayerData(ContentWriter writer, TiledMapLayerDataContent data)
        {
            writer.Write(data.Tiles.Count);
            foreach (TiledMapTileContent tile in data.Tiles)
            {
                writer.Write(tile.Gid);
            }
        }
    }
}
