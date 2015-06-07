// (c) Andrew Smith, 2007
// This document is released under the BSD license free for use, modification or incorporation.
// Please see license.txt for more information.
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{
    class ProgressBar
    {
        float progress; // percent of the progress bar, represented as a float. ex: 0.0 = 0%, 0.5 = 50%, 0.75 = 75%, etc.                        
        Texture2D texFrame; // texture to be used for the frame of the progress bar (aka an empty progress bar)
        Texture2D texTickFull; // texture to be used to show progress inside the frame, if progress is at 0.5, this image will be shown 50% filling the progress frame
        Vector2 origin; // the X,Y location of the bottom left corner of the progress bar
        bool isActive; // = true if the progress bar is active and displayed, false if inactive and hidden
        byte xOffset; // size in px between the top of the frame and top of the progress texture start 
        byte yOffset; // size in px between the left of the frame, and left of the progress texture start

        public ProgressBar(Texture2D texFrame, Texture2D texTickFull, Vector2 origin, byte xOffset, byte yOffset)
        {
            this.texFrame = texFrame;
            this.texTickFull = texTickFull;
            this.origin = origin;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            isActive = true;
        }
        
        public void Update(GameTime gameTime)
        {
            // put an update method here because it was suggested to have an Update and Draw method in each class
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isActive) // if the progress bar is active, 
            {
                spriteBatch.Draw(texFrame, origin, Color.White); // draw the empty frame at origin,
                spriteBatch.Draw(texTickFull, new Rectangle((int)origin.X + xOffset, (int)origin.Y + yOffset, (int)(progress * texTickFull.Width), texTickFull.Height), Color.White); // draw the progress filler based on progress (the rectangle is sized according to current progress, and texture filled inside)
            }
        }

        public float Progress
        {
            get { return progress; }
            set { progress = value; }
        }
    }
}
