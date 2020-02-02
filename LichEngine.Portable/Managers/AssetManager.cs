using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LichEngine.Portable.Managers
{
    public class AssetManager
    {
        private static ContentManager contentManager;
        private static Dictionary<string, Texture2D> textures;
        public AssetManager(ContentManager _contentManager)
        {
            contentManager = _contentManager;
            textures = new Dictionary<string, Texture2D>();
            var defaultTexture = contentManager.Load<Texture2D>("Textures/DefaultTexture");
            textures.Add("DefaultTexture", defaultTexture);
        }

        public static T GetAsset<T>(string key) where T : class
        {
            if(typeof(T) == typeof(Texture2D))
            {
                if (!textures.ContainsKey(key))
                {
                    try
                    {
                        AddAsset<Texture2D>(key);
                        var result = textures[key];
                        return (T)Convert.ChangeType(result, typeof(T));
                    }
                    catch 
                    {
                        return (T)Convert.ChangeType(textures["DefaultTexture"], typeof(T));
                    }

                }
                else 
                { 
                    var result = textures[key];
                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
            else
            {
                return null;
            }
              
        }

        public static void AddAsset<T>(string key) where T : class
        {
            if (typeof(T) == typeof(Texture2D))
            {
                try
                {
                    var tex = contentManager.Load<Texture2D>("Textures/" + key);
                    textures.Add(key, tex);
                }
                catch
                {
                    throw new Exception("Could not Load Texture, does not exist in current directory");
                }
            }
        }
        public static void Flush()
        {
            textures.Clear();
        }
    }
    public enum AssetType
    {
        TEXTURE_2D,
        AUDIO,
        SPRITE_FONT,
        EFFECT
    }
}
