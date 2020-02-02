using LichEngine.Portable.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Comora;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Graphics.Effects;
using System;
using LichEngine.ContentExtensions.Maps;
using System.IO;

namespace LichEngine.GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LichEngineMain : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private TiledMap tiledMap;
        

        public LichEngineMain()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            Content.RootDirectory = "Content";

        }

        
        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            string path = @"TiledMaps\MapTest";
            string parent = Directory.GetParent(path).Name;
            
            tiledMap = Content.Load<TiledMap>( parent + @"\" + @"MapTest");
            string tileSetPath = tiledMap.Tileset[0].Source;
            Console.WriteLine(tileSetPath);

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            new AssetManager(Content);
            font = Content.Load<SpriteFont>("MyFont");
            
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //camera.
            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
#if WINDOWS
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend ,SamplerState.PointClamp);
            spriteBatch.DrawString(font, "DIRECTX", new Vector2(100, 100), Color.Green, 0f, Vector2.Zero, .3f, SpriteEffects.None, 0f);
            var Texture = AssetManager.GetAsset<Texture2D>("DefaultTexture");
            spriteBatch.Draw(Texture, new Vector2(300, 300), Color.White);
            spriteBatch.End();
#endif
#if LINUX
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend ,SamplerState.PointClamp);
            spriteBatch.DrawString(font, "OPENGL", new Vector2(100, 100), Color.Orange, 0f, Vector2.Zero, .3f, SpriteEffects.None, 0f);
            var Texture = AssetManager.GetAsset<Texture2D>("DefaultTexture");
            spriteBatch.Draw(Texture, new Vector2(300, 300), Color.White);
            spriteBatch.End();
#endif

            base.Draw(gameTime);
        }
    }

    
}

