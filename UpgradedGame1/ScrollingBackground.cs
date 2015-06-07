using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame2
{    
    class ScrollingBackground
    {
        public static Random myRandomNumberGenerator = new Random();
        List<TerrainBlock> terrainBlocks;
        List<Cloud> clouds;
        List<Texture2D> texCloudsList;
        List<Texture2D> texTerrainList;
        private Vector2 screenpos, origin, texturesize;
        private Texture2D mytexture;
        private int screenheight;
        private Viewport viewport;

        public ScrollingBackground(Viewport viewport, List<Texture2D> TexTerrainList, List<Texture2D> texCloudsList)
        {
            this.viewport = viewport;            
            this.texTerrainList = TexTerrainList;
            this.texCloudsList = texCloudsList;
            terrainBlocks = new List<TerrainBlock>(20);
            
            clouds = new List<Cloud>(5);

            for (int i = 0; i <terrainBlocks.Capacity; i++)
            {
                terrainBlocks.Add(new TerrainBlock(viewport));
            }   
            for (int i = 0; i < clouds.Capacity; i++)
            {
                clouds.Add(new Cloud(viewport));
            }          
        }

        public void Load(GraphicsDevice Device, Texture2D BackgroundTexture)
        {            
            mytexture = BackgroundTexture;
            screenheight = Device.Viewport.Height;
            int screenwidth = Device.Viewport.Width;
            // Set the origin so that we're drawing from the 
            // center of the top edge.
            origin = new Vector2(mytexture.Width / 2, 0);
            // Set the screen position to the center of the screen.
            screenpos = new Vector2(screenwidth / 2, screenheight / 2);
            // Offset to draw the second texture, when necessary.
            texturesize = new Vector2(0, mytexture.Height);
        }        
      
        public void Update(float deltaY)
        {
            screenpos.Y += deltaY;
            screenpos.Y = screenpos.Y % mytexture.Height;
            for (int i = 0; i < terrainBlocks.Count; i++)
            {
                if (terrainBlocks[i].IsActive)
                {
                    if (terrainBlocks[i].Location.Y > viewport.Height)
                    {
                        terrainBlocks[i].IsActive = false;
                    }
                    terrainBlocks[i].Location = new Vector2(terrainBlocks[i].Location.X, terrainBlocks[i].Location.Y + deltaY);
                }
            }
            for (int i = 0; i < clouds.Count; i++)
            {
                if (clouds[i].IsActive)
                {
                    if (clouds[i].Location.Y > viewport.Height)
                    {
                        clouds[i].IsActive = false;
                    }
                    clouds[i].Location = new Vector2(clouds[i].Location.X, clouds[i].Location.Y + deltaY);
                }
            }
             GenerateRandomTerrain();
             GenerateRandomClouds();
        }
        
        public void Draw(SpriteBatch Batch)
        {
            // Draw the texture, if it is still onscreen.
            if (screenpos.Y < screenheight)
            {
                Batch.Draw(mytexture, screenpos, null, Color.White, 0, origin, 1, SpriteEffects.None, 0f);
            }
            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            Batch.Draw(mytexture, screenpos - texturesize , null,
                 Color.White, 0, origin, 1, SpriteEffects.None, 0f);
           
            
            for (int i = 0; i < terrainBlocks.Count; i++)
            {
                if (terrainBlocks[i].IsActive)
                {                   
                    Batch.Draw(texTerrainList[terrainBlocks[i].Type], terrainBlocks[i].Location, null, Color.White,0.0f,new Vector2(0,0), terrainBlocks[i].Size,SpriteEffects.None, 1.0f);
                }
            }
            for (int i = 0; i < clouds.Count; i++)
            {
                if (clouds[i].IsActive)
                {                   
                    Batch.Draw(texCloudsList[clouds[i].Type], clouds[i].Location, null, Color.White,0.0f,new Vector2(0,0), clouds[i].Size,SpriteEffects.None, 1.0f);
                }
            }
        }

        public void GenerateRandomTerrain()
        {
            int randomNum = myRandomNumberGenerator.Next(0, 200);
            if (randomNum == 1) // random chance to create a new terrain tile
            {
                for (int i = 0; i < terrainBlocks.Count; i++)
                {
                    if (!terrainBlocks[i].IsActive)
                    {
                        int tempRand;
                        double fTempRand = myRandomNumberGenerator.NextDouble() + 1.0;
                                        
                        tempRand = myRandomNumberGenerator.Next(0, 3);
                        terrainBlocks[i].Type = (short)tempRand;
                        tempRand = myRandomNumberGenerator.Next(0, viewport.Width);
                        terrainBlocks[i].Location = new Vector2(tempRand, -100);                        
                        terrainBlocks[i].Size = (float)fTempRand;
                        terrainBlocks[i].IsActive = true; 
                        break;           
                    }
                }
            }
        }

        public void GenerateRandomClouds()
        {
            int randomNum = myRandomNumberGenerator.Next(0, 200);
            if (randomNum == 1) // random chance to create a new cloud
            {
                for (int i = 0; i < clouds.Count; i++)
                {
                    if (!clouds[i].IsActive)
                    {
                        int tempRand;
                        double fTempRand = myRandomNumberGenerator.NextDouble() + 0.5;
                                        
                        tempRand = myRandomNumberGenerator.Next(0, 7);
                        clouds[i].Type = (short)tempRand;
                        tempRand = myRandomNumberGenerator.Next(0, viewport.Width);
                        clouds[i].Location = new Vector2(tempRand, -400);                        
                        clouds[i].Size = (float)fTempRand;
                        clouds[i].IsActive = true; 
                        break;           
                    }
                }
            }
        }
    }
    
    class TerrainBlock
    {
        short type;
        Vector2 location;
        float size;
        bool isActive;
        Viewport viewport;      

        public TerrainBlock(Viewport viewport)
        {
            this.viewport = viewport;         
        }
        public short Type
        {
            get { return type; }
            set { type = value; }
        }       
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }   
        public float Size
        {
            get { return size; }
            set { size = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }
    class Cloud
    {
        short type;
        Vector2 location;
        float size;
        bool isActive;
        Viewport viewport;

        public Cloud(Viewport viewport)
        {
            this.viewport = viewport;
        }
        public short Type
        {
            get { return type; }
            set { type = value; }
        }
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }
        public float Size
        {
            get { return size; }
            set { size = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }
}
