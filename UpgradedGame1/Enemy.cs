using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{
    class Enemy
    {        
        TimeSpan elapsedSinceLastUpdate;
        ProjectileManager projectileManager;
        private Vector2 curPosition;
        private compassDir direction;
        private int speed;
        private bool isActive;
        int health;
        int texIndex;

        public Enemy(ProjectileManager projectileManager)
        {
            this.projectileManager = projectileManager;
            isActive = false;
           
        }        

        public void Move(compassDir direction)
        {
            if (direction.Equals(compassDir.North))
            {
                curPosition.Y -= speed;
            }
            else if (direction.Equals(compassDir.South))
            {
                curPosition.Y += speed;
            }
            else if (direction.Equals(compassDir.East))
            {
                curPosition.X += speed;
            }
            else if (direction.Equals(compassDir.West))
            {
                curPosition.X -= speed;
            }
        }
        public void Move(compassDir direction, byte speed)
        {
            if (direction.Equals(compassDir.North))
            {
                curPosition.Y -= speed;
            }
            else if (direction.Equals(compassDir.South))
            {
                curPosition.Y += speed;
            }
            else if (direction.Equals(compassDir.East))
            {
                curPosition.X += speed;
            }
            else if (direction.Equals(compassDir.West))
            {
                curPosition.X -= speed;
            }
        }
        
        public Vector2 CurPosition
        {
            get { return curPosition; }
            set { curPosition = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public TimeSpan ElapsedSinceLastUpdate
        {
            get { return elapsedSinceLastUpdate; }
            set { elapsedSinceLastUpdate = value; }
        }
        public compassDir Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public int TexIndex
        {
            get { return texIndex; }
            set { texIndex = value; }
        }
        
    }
}