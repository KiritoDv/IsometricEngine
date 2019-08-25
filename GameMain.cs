using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections;
using MonoGame.Extended;
using System.Collections.Generic;
using IsometricEngine.engine;
using IsometricEngine.player;
using IsometricEngine.engine.tiles;
using IsometricEngine.engine.camera;

namespace IsometricEngine
{
    public class GameMain : Game{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<string, Texture2D> textures;
        Tilemap map;
        Player p;
        Camera2d cam;
        
        public GameMain(){
            graphics = new GraphicsDeviceManager(this);            
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            IsMouseVisible = true;
        }

        protected override void Initialize(){
            cam = new Camera2d();
            this.map = new Tilemap(40,40,40);
            this.textures = new Dictionary<string, Texture2D>();
            this.p = new Player();

            for(int i = 0; i < 10; i++){
                for(int k = 0; k < 10; k++){
                    this.map.setTile(i, 1, k, new Stone());   
                }
            }

            for(int i = 0; i < 10; i++){
                for(int k = 0; k < 10; k++){
                    this.map.setTile(i, 2, k, new Grass());
                }
            }
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);            

            this.textures.Add("Grass", Content.Load<Texture2D>("sprites/Grass"));
            this.textures.Add("Grass3", Content.Load<Texture2D>("sprites/Grass3"));
            this.textures.Add("G2M", Content.Load<Texture2D>("sprites/G2M"));
            this.textures.Add("SmallStone", Content.Load<Texture2D>("sprites/SmallStone"));
            this.textures.Add("Stone", Content.Load<Texture2D>("sprites/Stone"));
            this.textures.Add("Dirt", Content.Load<Texture2D>("sprites/Dirt"));            
        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 150;
            if(Keyboard.GetState().IsKeyDown(Keys.W))
                this.p.y -= speed * delta;

            if(Keyboard.GetState().IsKeyDown(Keys.A))
                this.p.x -= speed * delta;

            if(Keyboard.GetState().IsKeyDown(Keys.S))
                this.p.y += speed * delta;

            if(Keyboard.GetState().IsKeyDown(Keys.D))
                this.p.x += speed * delta;
            Debug.WriteLine("FPS: "+((int)(1 / gameTime.ElapsedGameTime.TotalSeconds)));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            cam.Pos = new Vector2( MathHelper.Lerp(cam.Pos.X, p.x, 0.08f),MathHelper.Lerp(cam.Pos.Y, p.y, 0.08f));
            cam.Zoom = 1;

            MouseState m = Mouse.GetState();


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.get_transformation(GraphicsDevice));
            for(int y = 0; y < this.map.points.GetLength(1); y++){
                for(int x = 0; x < this.map.points.GetLength(0); x++){
                    for(int z = 0; z < this.map.points.GetLength(2); z++){
                        if(this.map.points[x, y, z] != null){
                            ITile tile = this.map.points[x, y, z];
                            int baseX = this.map.points.GetLength(0)*8;
                            int baseY = this.map.points.GetLength(1)/2;
                            spriteBatch.Draw(this.textures[tile.tileTexture], new Rectangle(baseX+((x-z)*(32/2)), baseY+(((x+z)*(32/4))-((32/2)*(y))), tile.width, tile.height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, y/((float)this.map.points.GetLength(1)));
                        }
                    }   
                }
            }            
            spriteBatch.Draw(this.textures["G2M"], new Vector2(this.p.x, this.p.y), null, Color.White);

            Texture2D rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
            
            rect.SetData(new[] { Color.Red });
            
            spriteBatch.Draw(rect, new Rectangle(10, 10, 10, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}