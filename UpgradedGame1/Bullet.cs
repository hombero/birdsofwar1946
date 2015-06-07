using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{
    class Bullet
    {
        private Vector2 curPosition;
        private float speed;
        private bool isActive;
        char shooter;
        compassDir direction;
       
        public Bullet()
        {
            isActive = false;
        }

        public Vector2 CurPosition 
        {
            get { return curPosition; }
            set { curPosition = value; }
        }
        public char Shooter
        {
            get { return shooter; }
            set { shooter = value; }
        }
        public float Speed 
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool IsActive 
        {
            get { return isActive; } 
            set { isActive = value; }
        }
        public compassDir Direction
        {
            get { return direction; }
            set { direction = value; }
        }         
    }
}



