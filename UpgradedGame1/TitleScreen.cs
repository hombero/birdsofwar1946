using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class TitleScreen
    {
       
        

        bool isActive;
        
        
        public TitleScreen(SpriteFont ComicSans)
        {
                       
            
           


        }

        public void Update(GameTime gameTime)
        {
            

        }

        public void Draw(GameTime gameTime, SpriteBatch batch, SpriteFont font, List<Texture2D> titleTextures)
        {
            batch.Draw(titleTextures[(int)TitleTextureList.birdsLogo], new Vector2(170, 50), Color.WhiteSmoke);
            
            batch.DrawString(font, "(N)ew Game", new Vector2(340, 350), Color.WhiteSmoke);
            batch.Draw(titleTextures[(int)TitleTextureList.ButtonA],new Vector2(300, 350),null, Color.White, 0f,new Vector2(0,0),.4f,SpriteEffects.None, .0f);
            batch.DrawString(font, "(I)nstructions", new Vector2(340, 400), Color.WhiteSmoke);
            batch.Draw(titleTextures[(int)TitleTextureList.ButtonY], new Vector2(300, 400), null, Color.White, 0f, new Vector2(0, 0), .4f, SpriteEffects.None, .0f);
            batch.DrawString(font, "(Q)uit", new Vector2(340, 450), Color.WhiteSmoke);
            batch.Draw(titleTextures[(int)TitleTextureList.ButtonB], new Vector2(300, 450), null, Color.White, 0f, new Vector2(0, 0), .4f, SpriteEffects.None, .0f);


        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
     
        
        
                
    }
}
