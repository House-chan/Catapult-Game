using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Catapult.GameObjects;
using System.Collections.Generic;

namespace Catapult
{
    class MainMenu
    {
        enum menu
        {
            MainMenu, StageSelect, Setting
        }
        Texture2D MenuBackground, Button, Ship, Soup;
        SpriteFont font;

        Vector2 fontPosition, shipPosition, soupPosition, buttonPosition;

        menu mainMenu;

        public int stage;

        float transparent;

        public MainMenu(Texture2D MenuBackground, Texture2D Button, Texture2D Ship, Texture2D Soup, SpriteFont font)
        {
            this.MenuBackground = MenuBackground;
            this.Button = Button;
            this.Ship = Ship;
            this.Soup = Soup;
            this.font = font;
            transparent = 0.0f;
            stage = 0;
            mainMenu = menu.MainMenu;
            resetMenu();
        }

        public void Update(GameTime gameTime)
        {
            switch (mainMenu)
            {
                case menu.MainMenu:
                    //Moving Animation
                    if (fontPosition.Y < 10)
                    {
                        fontPosition.Y += 1;
                    }
                    else
                    {
                        if(soupPosition.X < 300)
                        {
                            soupPosition.X += 10;
                        }
                        if(shipPosition.X > 800)
                        {
                            shipPosition.X -= 10;
                        }
                    }

                    //Start Button
                    //Hover
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= 475 && Singleton.Instance.CurrentMouse.Position.Y < 575) &&
                        Singleton.Instance.CurrentMouse.Position.X >= 630 && Singleton.Instance.CurrentMouse.Position.X < 975)
                    {
                        //Pressed
                        if(Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                        {
                            mainMenu = menu.StageSelect;
                        }
                    }
                        break;
                case menu.StageSelect:
                    stage = 1;
                    break;
                case menu.Setting:

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MenuBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(Ship, shipPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(Soup, soupPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(Button, buttonPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Testing", fontPosition, Color.White);
        }

        void resetMenu()
        {
            fontPosition = new Vector2(250, -250);
            buttonPosition = new Vector2(630, 475);
            soupPosition = new Vector2(-700, 600);
            shipPosition = new Vector2(1750, 0);
        }

    }
}
