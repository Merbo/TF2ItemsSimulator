using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TF2ItemsSimulator
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        public static readonly int MAXPLAYERS = 32;

        public static Texture2D Soldier;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Player> Players = new List<Player>(MAXPLAYERS);

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Soldier = Content.Load<Texture2D>("Soldier");
            Projectile.DefaultSkin = Content.Load<Texture2D>("Projectile");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState gState = GamePad.GetState(PlayerIndex.One);
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            // Allows the game to exit
            if (gState.Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (Players.Count == 0 && mState.MiddleButton == ButtonState.Pressed)
            {
                Player LocalPlayer = new Player(true, new Vector2(0, 0), Soldier, Team.Blu, new PlayerClass(PlayerClass.Class.Medic));
                Players.Add(LocalPlayer);
            }

            foreach (Player P in Players)
            {
                P.Update(gameTime, gState, kState, mState, graphics);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
            foreach (Player P in Players)
            {
                P.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
