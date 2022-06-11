using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace monogame_test
{
    public class Game1 : Game
    {
        Texture2D dogTexture;
        Vector2 dogPosition;
        int DOG_SIZE = 64;

        Texture2D meatball;
        List<Vector2> meatballPositions;
        int MEATBALL_SIZE = 32;
        bool meatballFlag = false;

        Random random = new Random();

        //scoreboard variables
        SpriteFont font;
        int hunger; 

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
            dogPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            
            meatballPositions = new List<Vector2>();
            for(var i=0; i < 6; i++) 
                meatballPositions.Add(new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth - MEATBALL_SIZE), random.Next(0, _graphics.PreferredBackBufferHeight - MEATBALL_SIZE)));

            hunger = random.Next(4, 12);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            dogTexture = Content.Load<Texture2D>("dogR");
            meatball = Content.Load<Texture2D>("meatball");

            font = Content.Load<SpriteFont>("Yoster Island");
        }

        bool checkCollision(Vector2 meatballPosition)
        {
            //dog is above meatball
            if (dogPosition.Y + DOG_SIZE < meatballPosition.Y)
                return false;
            
            //dog is below meatball
            if (meatballPosition.Y + MEATBALL_SIZE < dogPosition.Y)
                return false;

            //dog is left of meatball
            if (dogPosition.X + DOG_SIZE < meatballPosition.X)
                return false;

            //dog is right of meatball
            if (meatballPosition.X + MEATBALL_SIZE < dogPosition.X)
                return false;

            return true;
        }

        protected override void Update(GameTime gameTime)
        {
            // quit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // add a new meatball every 5 seconds
            if ((int) gameTime.TotalGameTime.TotalSeconds % 4 == 0)
            {
                if(meatballFlag) 
                {
                    meatballPositions.Add(new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth - MEATBALL_SIZE), random.Next(0, _graphics.PreferredBackBufferHeight - MEATBALL_SIZE)));
                    meatballFlag = false;
                }
            }
            else
                meatballFlag = true;

            //move the doggy:
            var kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Up))
                dogPosition.Y -= 4;

            if (kbState.IsKeyDown(Keys.Down))
                dogPosition.Y += 4;

            if (kbState.IsKeyDown(Keys.Left)) { 
                dogTexture = Content.Load<Texture2D>("dogL");
                dogPosition.X -= 4;
            }
            if (kbState.IsKeyDown(Keys.Right)) {
                dogTexture = Content.Load<Texture2D>("dogR");
                dogPosition.X += 4;
            }

            for (var i = 0; i < meatballPositions.Count; i++)
            {
                if(checkCollision(meatballPositions[i]))
                {
                    hunger--;
                    meatballPositions.RemoveAt(i);
                    i--;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (var i = 0; i < meatballPositions.Count; i++)
            {
                _spriteBatch.Draw(meatball, meatballPositions[i], Color.White);
            }

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

            //_spriteBatch.DrawString(font, $"Timer: {gameTime.TotalGameTime.TotalSeconds}", new Vector2(50, 50), Color.White);
                        
            if(hunger > 0)
                _spriteBatch.DrawString(font, $"Carlos Segundo tiene hambre! El qiere {hunger} mas albondigas.", new Vector2(50, 50), Color.White);
            else
                _spriteBatch.DrawString(font, "Wahoo, Carlos Segundo no tiene hambre ya!", new Vector2(50, 50), Color.White);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
