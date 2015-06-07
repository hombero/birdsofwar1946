using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame2
{
    class Menu
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        Rectangle menuFrame;
        Vector2 location;
        Color menuBorderColor;
        Color menuFillColor;
        Color menuTextColor;
        Texture2D menuIcon;
        Texture2D texMenuFill;
        Player player;
        char menuType;


        string menuTitle;
        string menuContent;
        

        bool isActive;

        public Menu(Vector2 location)
        {
            menuType = 'N';
            this.location = location;
        }

    
        
        public Menu(Rectangle menuFrame, Texture2D texMenuFill, Color menuTextColor, Player player, char menuType)
        {
            this.menuType = menuType;
            
            this.menuFrame = menuFrame;
           
            this.texMenuFill = texMenuFill;
            this.menuTextColor = menuTextColor;
            this.player = player;
            this.isActive = false;
        }
        
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (menuType != 'N')
            {
                batch.Draw(this.texMenuFill, menuFrame, Color.White);

                for (int i = 0; i < menuItems.Count; i++)
                {
                    menuItems[i].Draw(batch, gameTime, menuFrame);

                }
            }
            if (menuType == 'N')
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    menuItems[i].Draw(batch, gameTime, location);

                }

            }

        }
        public void AddMenuItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
            menuItems[0].IsSelected = true;
            

        }

        public void Update(GameTime gameTime, Viewport viewport)
        {

        }

        public void KeyPress(Keys key)
        {
            int tempSelected = 0;

            if (key == Keys.Down)
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (menuItems[i].IsSelected)
                    {
                        tempSelected = i;
                        break;
                    }
                }

                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (i > tempSelected && menuItems[i].IsSelectable)
                    {
                        menuItems[i].IsSelected = true;
                        menuItems[tempSelected].IsSelected = false;
                        break;

                    }
                }
            }
            if (key == Keys.Space)
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (menuItems[i].IsSelected)
                    {

                        if (menuType == 'L')
                        {
                            if (player.LevelPoints >= menuItems[i].Cost)
                            {
                                if (i == 0)
                                    player.Speed += .5f;
                                else if (i == 1)
                                    player.WeaponSpeed += 0.5f;
                                else if (i == 2)
                                {
                                    player.MaxHealth += 1;
                                    player.CurHealth += 1;
                                }
                                player.LevelPoints -= menuItems[i].Cost;
                            }

                        }



                        if (menuType == 'P')
                        {
                            if (player.Score >= menuItems[i].Cost)
                            {
                                if (i == 0)
                                    player.HasSpeedPowerup = true;
                                else if (i == 2)
                                    player.HasWeaponPowerup = true;
                                else if (i == 4)
                                    player.HasShieldPowerup = true;
                                player.Score -= menuItems[i].Cost;

                            }
                        }

                    }
                }
                
            }

            if (key == Keys.Up)
            {
                for (int i = menuItems.Count - 1; i >= 0; i--)
                {
                    if (menuItems[i].IsSelected)
                    {
                        tempSelected = i;
                        break;
                    }
                }

                for (int i = menuItems.Count - 1; i >= 0; i--)
                {
                    if (i < tempSelected && menuItems[i].IsSelectable)
                    {
                        menuItems[i].IsSelected = true;
                        menuItems[tempSelected].IsSelected = false;
                        break;

                    }
                }


            }
        
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }




    }
    class MenuItem
    {
        MenuContentType menuContentType;
        Vector2 contentLocation;
        Texture2D contentImage;
        string contentString;
        Color contentColor;
        SpriteFont contentFont;
        bool isSelected;
        bool isSelectable;
        int cost;


        public MenuItem(MenuContentType menuContentType, Vector2 contentLocation, Texture2D contentImage, SpriteFont contentFont, string contentString, Color contentColor, bool isSelectable, int cost)
        {
            this.menuContentType = menuContentType;
            this.contentLocation = contentLocation;
            this.contentImage = contentImage;
            this.contentFont = contentFont;
            this.contentString = contentString;
            this.contentColor = contentColor;
            this.isSelectable = isSelectable;
            this.cost = cost;

        }

        public void Draw(SpriteBatch batch, GameTime gameTime, Rectangle menuFrame)
        {
            if (menuContentType == MenuContentType.Texture2D)
                batch.Draw(contentImage, new Vector2(menuFrame.X + contentLocation.X, menuFrame.Y + contentLocation.Y), Color.White);
            if (menuContentType == MenuContentType.TextBlock)
                if (isSelected)
                batch.DrawString(contentFont, contentString, new Vector2(menuFrame.X + contentLocation.X, menuFrame.Y + contentLocation.Y), Color.CornflowerBlue);
                else
                batch.DrawString(contentFont, contentString, new Vector2(menuFrame.X + contentLocation.X, menuFrame.Y + contentLocation.Y), contentColor);

        }
        public void Draw(SpriteBatch batch, GameTime gameTime, Vector2 location)
        {
            if (menuContentType == MenuContentType.Texture2D)
                batch.Draw(contentImage,location, Color.White);
            if (menuContentType == MenuContentType.TextBlock)
                if (isSelected)
                    batch.DrawString(contentFont, contentString, location + contentLocation, Color.CornflowerBlue);
                else
                    batch.DrawString(contentFont, contentString, location + contentLocation, contentColor);
        }

        public void Update(GameTime gameTime, Viewport viewport)
        {
            

        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public bool IsSelectable
        {
            get { return isSelectable ; }
            set { isSelectable = value; }
        }

         public int Cost
        {
            get { return cost ; }
            set { cost = value; }
        }
            



    }
}
