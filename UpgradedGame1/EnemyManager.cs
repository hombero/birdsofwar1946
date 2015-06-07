using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{   
    class EnemyManager
    {
        int xOffset;
        TimeSpan elapsedTime = TimeSpan.Zero;
        List<Enemy> enemies;
        ProjectileManager projectileManager;
        Random myRandomNumberGenerator = new Random();
        
        public EnemyManager(byte maxEnemies, ProjectileManager projectileManager)
        {
            xOffset = -33; // DOESN'T WORK PROPERLY, XNA BUG?
            this.projectileManager = projectileManager;
            enemies = new List<Enemy>(maxEnemies);
            
            for (int i = 0; i < maxEnemies; i++) 
                enemies.Add(new Enemy(projectileManager));            
        }
        
        public void Update(GameTime gameTime, Viewport viewport, Vector2 hintedPlayerLoc)
        {
            UpdateViaAI(gameTime, viewport, hintedPlayerLoc);
        }

        public void UpdateViaAI(GameTime gameTime, Viewport viewport, Vector2 playerLoc)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            
            for (int i = 0; i < enemies.Count; i++)
            {
                int tempRand = myRandomNumberGenerator.Next(0, 60);                
                
                if (enemies[i].IsActive)
                {
                    if (enemies[i].CurPosition.Y < viewport.Height)
                    {
                        if (tempRand == 1)
                        {
                            projectileManager.StartBullet('E', enemies[i].CurPosition, -24, 5, compassDir.South);
                        }
                        if (elapsedTime > TimeSpan.FromSeconds(.2))
                        {
                            if (enemies[i].CurPosition.X < playerLoc.X)
                            {
                                enemies[i].Move(compassDir.East, 2);
                            }
                            else
                            {
                                enemies[i].Move(compassDir.West, 2);
                                if (tempRand == 1)
                                    projectileManager.StartBullet('E', enemies[i].CurPosition, -24, 5, compassDir.South);
                            }
                        }
                        enemies[i].Move(compassDir.South, 1);
                    }
                    else
                    {
                        enemies[i].IsActive = false;
                    }                   
                }                
            }

            if (elapsedTime > TimeSpan.FromSeconds(.2))
            {
                elapsedTime = TimeSpan.Zero;
            }          
        }
        
        public void StartEnemy(Vector2 location, compassDir iniDirection, byte speed)
        {
            int randomNum = myRandomNumberGenerator.Next(0, 3);
            for (int i = 0; i < enemies.Count; i++)
                 if (!enemies[i].IsActive)
                 {
                     
                     enemies[i].TexIndex = randomNum;
                     enemies[i].CurPosition = location;
                     enemies[i].Direction = iniDirection;
                     enemies[i].Speed = speed;                     
                     enemies[i].IsActive = true;
                     break;
                 }
        }

        public void Draw(SpriteBatch batch, List<AnimatedTexture> enemyTex, GameTime gameTime)
        {
            
            for (int i = 0; i < enemies.Count; i++)
                if (enemies[i].IsActive)
                {
                    
                    enemyTex[enemies[i].TexIndex].DrawFrame(batch, enemies[i].CurPosition);                   
                }         
        }

        public int Count
        {
            get
            {
                int tempCount = 0;
                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i].IsActive)
                    {
                        tempCount += 1;
                    }
                return tempCount; }
            set {  } // not allowed
        }

        public int XOffset
        {
            get { return xOffset; }
            set { xOffset = value; }
        }

        public List<Enemy> EnemyList
        {
            get { return enemies; }
            set { enemies = value; }
        }
    }
}
