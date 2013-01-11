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
    abstract class Weapon
    {
        /// <summary>
        /// Weapon's attributes.
        /// </summary>
        public List<Attribute> Attributes;

        /// <summary>
        /// Base damage before attributes.
        /// </summary>
        public int Damage;
         
        /// <summary>
        /// Fire rate in milliseconds.
        /// </summary>
        public int FireRate;

        /// <summary>
        /// Clip size.
        /// </summary>
        public int ClipSize;

        /// <summary>
        /// Reserve Ammo Size.
        /// </summary>
        public int ReserveSize;

        /// <summary>
        /// Texture of the weapon.
        /// </summary>
        public Texture2D Skin;

        /// <summary>
        /// Projectile fired. Should be instantiated as :Projectile(null, null, Projectile.DefaultSkin)
        /// </summary>
        public Projectile FiredProjectile;

        /// <summary>
        /// The amount of projectiles fired.
        /// </summary>
        public int ProjectileCount;

        /// <summary>
        /// The speed of the fired projectile.
        /// </summary>
        public float FiredProjectileSpeed;

        /// <summary>
        /// The last time the weapon was fired.
        /// </summary>
        public DateTime LastFired;

        /// <summary>
        /// Weapon's owner.
        /// </summary>
        public Player Owner;

        public Weapon(Player owner, Texture2D skin, Projectile firedprojectile, int damage = 5, int firerate = 0750, int clipsize = 4, int reservesize = 32, int projectilecount = 8, float projectilespeed = 1.5f)
        {
            this.Attributes = new List<Attribute>(16);
            this.Owner = owner;
            this.Skin = skin;
            this.FiredProjectile = firedprojectile;
            this.Damage = damage;
            this.FireRate = firerate;
            this.ClipSize = clipsize;
            this.ReserveSize = reservesize;
            this.ProjectileCount = projectilecount;
            this.FiredProjectileSpeed = projectilespeed;
            this.LastFired = DateTime.Now;
        }

        public bool CanFire(DateTime LastFiredTime)
        {
            TimeSpan Difference = DateTime.Now - LastFiredTime;

            long TotalMilliseconds = (long)Difference.TotalMilliseconds;
            if (TotalMilliseconds >= this.FireRate)
                return true;
            return false;
        }

        /// <summary>
        /// Fires the weapon.
        /// </summary>
        public virtual void Fire()
        {
            if (!CanFire(this.LastFired))
                return;
            for (int i = 1; i <= ProjectileCount; i++)
            {
                Projectile P = FiredProjectile;
                P.Position = Owner.PlayerPosition;
                switch (Owner.PlayerFaceDirection)
                {
                    case Player.Directions.Down:
                        P.Speed = new Vector2(0f, -1 * FiredProjectileSpeed);
                        break;
                    case Player.Directions.Left:
                        P.Speed = new Vector2(-1 * FiredProjectileSpeed, 0f);
                        break;
                    case Player.Directions.Right:
                        P.Speed = new Vector2(FiredProjectileSpeed, 0f);
                        break;
                    case Player.Directions.Up:
                        P.Speed = new Vector2(0f, FiredProjectileSpeed);
                        break;
                }

                P.OnCollision += OnCollidePre;
            }
            this.LastFired = DateTime.Now;
        }

        private void OnCollidePre(object sender, ProjectileCollisionArgs e)
        {
            OnCollide((Projectile)sender, e);
        }

        public abstract void OnCollide(Projectile P, ProjectileCollisionArgs e);
    }
}
