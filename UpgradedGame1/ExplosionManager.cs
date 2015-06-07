using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{
    class ExplosionManager
    {
        List<Explosion> explosions;

        public ExplosionManager(byte maxExplosions)
        {
            explosions = new List<Explosion>(maxExplosions);
            for (int i = 0; i < maxExplosions; i++)
            {
                explosions.Add(new Explosion());
            }
        }
        
        public void Update(GameTime gameTime, Viewport viewport)
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                if (explosions[i].IsActive)
                {
                    explosions[i].CurFrame += 1;
                }
            }
        }

        public void Draw(SpriteBatch batch, AnimatedTexture explosionTex, GameTime gameTime)
        {
            for (int i = 0; i < explosions.Count; i++)
                if (explosions[i].IsActive)
                {
                    explosionTex.DrawFrame(batch, new Vector2(explosions[i].CurPosition.X - (explosionTex.MyTexture.Width / explosionTex.Framecount) / 2 + 25, explosions[i].CurPosition.Y - 50));
                    
                    if (explosions[i].CurFrame > 25)
                    {
                        explosions[i].IsActive = false;
                    }                   
                }
        }

        public void StartExplosion(Vector2 location)
        {
            for (int i = 0; i < explosions.Count; i++)
                if (!explosions[i].IsActive)
                {
                    explosions[i].CurPosition = location;
                    explosions[i].IsActive = true;
                    explosions[i].CurFrame = 1;
                    break;
                }
        }
    }
}
