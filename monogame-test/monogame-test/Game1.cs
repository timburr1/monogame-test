using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_test
{
    public class Game1 : Game
    {
        Texture2D dogTexture;
        Vector2 dogPosition;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            dogPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            dogTexture = Content.Load<Texture2D>("dogR");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Up))
                dogPosition.Y -= 8;

            if (kbState.IsKeyDown(Keys.Down))
                dogPosition.Y += 8;

            if (kbState.IsKeyDown(Keys.Left)) { 
                dogTexture = Content.Load<Texture2D>("dogL");
                dogPosition.X -= 8;
            }
            if (kbState.IsKeyDown(Keys.Right)) {
                dogTexture = Content.Load<Texture2D>("dogR");
                dogPosition.X += 8;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(dogTexture, 
                dogPosition, 
                null, 
                Color.White,
                0f,
                new Vector2(dogTexture.Width / 2, dogTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
