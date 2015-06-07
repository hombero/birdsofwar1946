using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
namespace WindowsGame2
{
    public class AnimatedTexture
    {
        private int framecount;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int Frame;
        private float TotalElapsed;       
        private bool Paused;
        public float Rotation, Scale, Depth;
        public Vector2 Origin;

        public AnimatedTexture(Vector2 Origin, float Rotation, float Scale, float Depth)
        {
            this.Origin = Origin;
            this.Rotation = Rotation;
            this.Scale = Scale;
            this.Depth = Depth;            
        }

        public void Load(GraphicsDevice device, ContentManager content, string asset, int FrameCount, int FramesPerSec)
        {
            framecount = FrameCount;
            myTexture = content.Load<Texture2D>("CPL/" + asset);
            TimePerFrame = (float)1 / FramesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }
        
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
            {
                return;
            }
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;                
            }
        }
        
        public void DrawFrame(SpriteBatch Batch, Vector2 screenpos)
        {
            DrawFrame(Batch, Frame, screenpos);
        }
        public void DrawFrame(SpriteBatch Batch, int Frame, Vector2 screenpos)
        {
            int FrameWidth = myTexture.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * Frame, 0, FrameWidth, myTexture.Height);
            Batch.Draw(myTexture, screenpos, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);            
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }      
        public Texture2D MyTexture
        {
            get { return myTexture; }
            set { } // not allowed
        }
        public int Framecount
        {
            get { return framecount; }
            set { } // not allowed

        }
           
    }
}
