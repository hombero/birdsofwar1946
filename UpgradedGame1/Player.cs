using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{
    class Player
    {        
        float weaponElapsedTime; 
        float speedElapsedTime; 
        float shieldElapsedTime; 
        ProjectileManager projectileManager;

        Viewport globalViewport;
        Vector2 curLocation;
        int xOffset;
        bool isActive;
        float curRotation;
        float curScale;
        float weaponSpeed = 3;

        int levelPoints;
        
        int curHealth = 2;
        int maxHealth = 2;
        int score = 0;
        float speed = 4;
        int extraSpeed = 0;
        int level = 1;
        int exp = 0;
        bool hasSpeedPowerup;
        bool hasWeaponPowerup;
        bool hasShieldPowerup;
        bool shieldActive;
        
        public Player(Vector2 iniLocation, float iniScale, float iniRotation, Viewport globalViewport, ProjectileManager projectileManager)
        {
            this.globalViewport = globalViewport;
            this.curLocation = iniLocation;
            this.curScale = iniScale;
            this.curRotation = iniRotation;
            this.projectileManager = projectileManager;
            this.isActive = true;
        }
       
        public void Fire()
        {          
            if (!hasWeaponPowerup)
            projectileManager.StartBullet('P', curLocation, xOffset, weaponSpeed, compassDir.North);

        if (hasWeaponPowerup)
        {
            projectileManager.StartBullet('P', curLocation, xOffset - 10, weaponSpeed + 5, compassDir.North);
            projectileManager.StartBullet('P', curLocation, xOffset + 10, weaponSpeed + 5, compassDir.North);
        }
        }

        public void Move(compassDir direction)
        {
            if (hasSpeedPowerup)
                extraSpeed = 4;
            else
                extraSpeed = 0;
                


            if (direction.Equals(compassDir.North))
            {
                curLocation.Y -= (speed + extraSpeed);
            }
            else if (direction.Equals(compassDir.South))
            {
                curLocation.Y += (speed + extraSpeed);
            }
            else if (direction.Equals(compassDir.East))
            {
                curLocation.X += (speed + extraSpeed);
            }
            else if (direction.Equals(compassDir.West))
            {
                curLocation.X -= (speed + extraSpeed);
            }
        }
        
        public Vector2 CurLocation
        {
            get { return curLocation; }
            set { }// not allowed 
        }
        public int XOffset
        {
            get { return xOffset; }
            set { xOffset = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }
        public bool HasSpeedPowerup
        {
            get { return hasSpeedPowerup; }
            set { 
                hasSpeedPowerup = value;
                speedElapsedTime = 0;


            }
        }
        public bool HasWeaponPowerup
        {
            get { return hasWeaponPowerup; }
            set { hasWeaponPowerup = value; }
        }
        public bool HasShieldPowerup
        {
            get { return hasShieldPowerup; }
            set { hasShieldPowerup = value; }
        }
        public bool ShieldActive
        {
            get { return shieldActive; }
            set { shieldActive = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public float WeaponSpeed
        {
            get { return weaponSpeed; }
            set { weaponSpeed = value; }        
        }
        public int CurHealth
        {
            get { return curHealth; }
            set { curHealth = value; }
        }
        public int LevelPoints
        {
            get { return levelPoints; }
            set { levelPoints = value; }
        }
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public void Update(GameTime gameTime)
        {
            if (hasSpeedPowerup)
            {
                speedElapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (speedElapsedTime > 20000)
                {
                    hasSpeedPowerup = false;
                    speedElapsedTime = 0;

                }
                    

            }
            if (hasWeaponPowerup)
            {
                weaponElapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (weaponElapsedTime > 20000)
                {
                    hasWeaponPowerup = false;
                    weaponElapsedTime = 0;
                }

            }

            if (hasShieldPowerup)
            {
                if (shieldActive)
                    shieldElapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;

                if (shieldElapsedTime > 5000)
                {
                    shieldActive = false;
                    hasShieldPowerup = false;
                    shieldElapsedTime = 0;
                }


            }


            
        }
    }
    
}
