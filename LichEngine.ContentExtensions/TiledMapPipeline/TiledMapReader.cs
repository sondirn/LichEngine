using LichEngine.ContentExtensions.HelperObjects;
using LichEngine.ContentExtensions.Maps;
using LichEngine.ContentExtensions.TiledTileSetPipeline;
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
            TiledMap result = ReadTiledMap(input);
            ReadTileSets(input,ref result);
            ReadTiledMapLayer(input, ref result);
            return result;
        }

        public static TiledMap ReadTiledMap(ContentReader reader)
        {
            var name = reader.AssetName;
            var width = reader.ReadInt32();
            var height = reader.ReadInt32();
            var tileWidth = reader.ReadInt32();
            var tileHeight = reader.ReadInt32();
            return new TiledMap
            {
                Name = name,
                Width = width,
                Height = height,
                Tilewidth = tileWidth,
                Tileheight = tileHeight
            };

        }

        public static void ReadTileSets(ContentReader reader, ref TiledMap map)
        {
            var tileSetCount = reader.ReadInt32();
            if(map.Tileset == null)
            {
                map.Tileset = new List<TiledMapTileSet>();
            }
            for (int i = 0; i < tileSetCount; i++)
            {
                var firstGid = reader.ReadInt32();
                var name = reader.ReadString();
                var tilewidth = reader.ReadInt32();
                var tileheight = reader.ReadInt32();
                var tilecount = reader.ReadInt32();
                var colums = reader.ReadInt32();
                var tileset = new TiledMapTileSet
                {
                    Firstgid = firstGid,
                    Name = name,
                    Tilewidth = tilewidth,
                    Tileheight = tileheight,
                    Tilecount = tilecount,
                    Columns = colums
                };
                var imageSource = reader.ReadString();
                var imageWidth = reader.ReadInt32();
                var imageHeight = reader.ReadInt32();
                tileset.Image = new TiledTileSetImage
                {
                    Source = imageSource,
                    Height = imageHeight,
                    Width = imageWidth
                };
                var tilesCount = reader.ReadInt32();
                tileset.Tiles = new List<TiledTileSetTile>();

                for (int x = 0; x < tilesCount; x++)
                {
                    
                    var tilesetTile_id = reader.ReadInt32();
                    var tilesetTile = new TiledTileSetTile { Id = tilesetTile_id };
                    var frameCount = reader.ReadInt32();
                    tilesetTile.Animation = new TiledTileSetTileAnimation();
                    tilesetTile.Animation.Frame = new List<TiledTileSetAnimationFrame>();
                    for (int y = 0; y < frameCount; y++)
                    {
                        var frameTileId = reader.ReadInt32();
                        var frameduration = reader.ReadDouble();
                        var frame = new TiledTileSetAnimationFrame
                        {
                            Tileid = frameTileId,
                            Duration = frameduration,
                        };
                        tilesetTile.Animation.Frame.Add(frame);
                    }
                    tileset.Tiles.Add(tilesetTile);
                }
                map.Tileset.Add(tileset);

            }
        }

        public static void ReadTiledMapLayer(ContentReader reader, ref TiledMap map)
        {
            int layernum = reader.ReadInt32();
            map.Layer = new List<TiledMapLayer>();
            for (int i = 0; i < layernum; i++)
            {
                var id = reader.ReadInt32();
                var width = reader.ReadInt32();
                var height = reader.ReadInt32();
                var name = reader.ReadString();
                map.Layer.Add(new TiledMapLayer
                {
                    Id = id,
                    Width = width,
                    Height = height,
                    Name = name
                });
                map.Layer[i].Data = new TiledMapLayerData();
                var tileCount = reader.ReadInt32();
                map.Layer[i].Data.Tile = new List<TiledMapTile>();
                for (int x = 0; x < tileCount; x++)
                {
                    
                    var tilegid = reader.ReadInt32();
                    var tiledata = new TiledMapTile()
                    {
                        Gid = tilegid
                    };
                    map.Layer[i].Data.Tile.Add(tiledata);
                }
            }
        }

        
    }
}
