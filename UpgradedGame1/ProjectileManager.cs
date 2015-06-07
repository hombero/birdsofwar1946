using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
namespace WindowsGame2
{
    class ProjectileManager
    {
       
        List<Bullet> bullets;
        SoundBank soundBank;
        
        public ProjectileManager(byte maxBullets, SoundBank soundBank)
        {
            this.soundBank = soundBank;
            bullets = new List<Bullet>(maxBullets);
            for (int i = 0; i < maxBullets; i++) 
                bullets.Add(new Bullet());            
        }

        public void Update(GameTime gameTime, Viewport viewport)
        {
            UpdateBullets(viewport);
        }
        
        public void UpdateBullets(Viewport viewport)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].IsActive)
                {
                    if (bullets[i].Direction.Equals(compassDir.North)) 
                    {
                        if (bullets[i].CurPosition.Y < viewport.Height && bullets[i].CurPosition.Y > viewport.Y)
                        {
                            bullets[i].CurPosition -= new Vector2(0, bullets[i].Speed);
                        }
                       else
                       {
                            bullets[i].IsActive = false;
                       }
                    }
                    if (bullets[i].Direction.Equals(compassDir.South)) {
                        if (bullets[i].CurPosition.Y < viewport.Height && bullets[i].CurPosition.Y > viewport.Y)
                        {
                            bullets[i].CurPosition += new Vector2(0, bullets[i].Speed);
                        }
                        else
                        {
                            bullets[i].IsActive = false;
                        }
                    }                    
                }
            }
        }

        public void Draw(SpriteBatch batch, Texture2D bulletTex, Texture2D enemyBulletTex, GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; i++)
                if (bullets[i].IsActive)
                {
                    if (bullets[i].Shooter == 'E')
                        batch.Draw(enemyBulletTex, new Rectangle((int)bullets[i].CurPosition.X, (int)bullets[i].CurPosition.Y, enemyBulletTex.Width, enemyBulletTex.Height), null, Color.White, 0.0f, new Vector2(bulletTex.Width / 2, bulletTex.Height / 2), SpriteEffects.None, 0.0f);
                    else
                        batch.Draw(bulletTex, bullets[i].CurPosition, Color.White);
                }
        }
            
        public void StartBullet(char shooter, Vector2 location, int locationXOffset, float speed, compassDir direction)
        {
            for (int i = 0; i < bullets.Count; i++) 
             if (!bullets[i].IsActive) 
             {
                 Vector2 tempLocation = new Vector2((location.X) - locationXOffset, location.Y);
                 bullets[i].CurPosition = tempLocation;
                 bullets[i].Speed = speed;
                 bullets[i].Shooter = shooter;
                 bullets[i].IsActive = true;
                 bullets[i].Direction = direction;
                 //if (shooter == 'E')
                     //soundBank.PlayCue("enemy_gunshot");
                 break;
                         
             }   
        }
        public int Count
        {   get
            {
                int tempCount = 0;
                for (int i = 0; i < bullets.Count; i++)
                    if (bullets[i].IsActive)
                    {
                        tempCount++;
                    }
                return tempCount;
            }
            set { } // not allowed
        }

        public List<Bullet> ProjectileList
        {
            get { return bullets; }
            set { bullets = value; }
        }
    }
}
