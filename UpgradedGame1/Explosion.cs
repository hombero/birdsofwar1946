using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace WindowsGame2
{
    class Explosion
    {
        Vector2 curPosition;
        byte curFrame;
        bool isActive;       

        public Explosion()
        {
          
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }       
        public Vector2 CurPosition
        {
            get { return curPosition; }
            set { curPosition = value; }
        }
        public byte CurFrame
        {
            get { return curFrame; }
            set { curFrame = value; }
        }        
    }
}
