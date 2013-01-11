using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TF2ItemsSimulator
{
    class Player
    {
        public enum Directions
        {
            Up,
            Left,
            Right,
            Down,
        }

        public readonly bool IsControlled;

        public Vector2 PlayerSpeed;
        public Vector2 PlayerPosition;
        public Texture2D PlayerSkin;
        public Directions PlayerFaceDirection;
        public Team PlayerTeam;
        public Color PlayerColor;
        public PlayerClass PlayerClass;
        public List<Weapon> PlayerWeapons;

        public int PlayerHealth;


        public Rectangle PlayerBoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)PlayerPosition.X,
                    (int)PlayerPosition.Y,
                    PlayerSkin.Width,
                    PlayerSkin.Height);
            }
        }

        public Player(bool isControlled, Vector2 SpawnPoint, Texture2D playerSkin, Team team, PlayerClass c)
        {
            this.IsControlled = isControlled;
            this.PlayerSpeed = new Vector2();
            this.PlayerPosition = SpawnPoint;
            this.PlayerSkin = playerSkin;
            this.PlayerFaceDirection = Directions.Down;
            this.PlayerTeam = team;
            this.PlayerColor = (team == Team.Spectator) ? Color.LightGray : (team == Team.Blu) ? Color.Blue : Color.Red;
            this.PlayerClass = c;
            this.PlayerHealth = 150;
            this.PlayerWeapons = new List<Weapon>();
        }

        public void Update(GameTime gameTime, GamePadState gState, KeyboardState kState, MouseState mState, GraphicsDeviceManager graphics)
        {
            if (IsControlled)
            {
                if (kState.IsKeyDown(Keys.W))
                {
                    PlayerSpeed.Y += -1;
                    PlayerFaceDirection = Directions.Up;
                }
                if (kState.IsKeyDown(Keys.A))
                {
                    PlayerSpeed.X += -1;
                    PlayerFaceDirection = Directions.Left;
                }
                if (kState.IsKeyDown(Keys.S))
                {
                    PlayerSpeed.Y += 1;
                    PlayerFaceDirection = Directions.Down;
                }
                if (kState.IsKeyDown(Keys.D))
                {
                    PlayerSpeed.X += 1;
                    PlayerFaceDirection = Directions.Right;
                }

                if (kState.IsKeyUp(Keys.W) && kState.IsKeyUp(Keys.S))
                {
                    if (PlayerSpeed.Y > 3)
                        PlayerSpeed.Y -= 3;
                    else
                        PlayerSpeed.Y = 0;
                }
                if (kState.IsKeyUp(Keys.A) && kState.IsKeyUp(Keys.D))
                {
                    if (PlayerSpeed.X > 3)
                        PlayerSpeed.X -= 3;
                    else
                        PlayerSpeed.X = 0;
                }
            }

            //Max speeds
            if (PlayerSpeed.X > 25)
                PlayerSpeed.X = 25;
            if (PlayerSpeed.Y > 25)
                PlayerSpeed.Y = 25;

            PlayerPosition += PlayerSpeed;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - PlayerSkin.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - PlayerSkin.Height;
            int MinY = 0;

            if (PlayerPosition.X > MaxX)
            {
                PlayerPosition.X = MaxX;
                PlayerSpeed.X *= -0.25f;
            }
            if (PlayerPosition.X < MinX)
            {
                PlayerPosition.X = MinX;
                PlayerSpeed.X *= -0.25f;
            }
            if (PlayerPosition.Y > MaxY)
            {
                PlayerPosition.Y = MaxY;
                PlayerSpeed.Y *= -0.25f;
            }
            if (PlayerPosition.Y < MinY)
            {
                PlayerPosition.Y = MinY;
                PlayerSpeed.Y *= -0.25f;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.PlayerSkin, this.PlayerPosition, this.PlayerColor);
        }
    }
}
