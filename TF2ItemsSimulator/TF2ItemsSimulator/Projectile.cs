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
    abstract class Projectile
    {
        public static Texture2D DefaultSkin;

        public Vector2 Position;
        public Vector2 Speed;
        public Texture2D Skin;

        public Projectile(Vector2 SpawnLocation, Vector2 SpawnSpeed, Texture2D Model)
        {
            this.Position = SpawnLocation;
            this.Speed = SpawnSpeed;
            this.Skin = Model;
        }

        public event EventHandler<ProjectileCollisionArgs> OnCollision;

        protected virtual void OnCollisionHandle(ProjectileCollisionArgs e)
        {
            EventHandler<ProjectileCollisionArgs> handler = OnCollision;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            ProjectileCollisionArgs P = null;

            Position += Speed;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - Skin.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - Skin.Height;
            int MinY = 0;

            if (Position.X > MaxX || Position.X < MinX || Position.Y > MaxY || Position.Y < MinY)
            {
                P = new ProjectileCollisionArgs(this.Position);
            }

            if (P != null)
            {
                OnCollisionHandle(P);
            }
        }
    }

    class ProjectileCollisionArgs : EventArgs
    {
        public readonly Vector2 CollisionLocation;
        public ProjectileCollisionArgs(Vector2 location)
        {
            CollisionLocation = location;
        }
    }
}
