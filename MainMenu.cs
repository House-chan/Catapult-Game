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
        Vector2[] ButtonStage = new Vector2[5];
        Vector2 BackButton;
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
            stage = 0;
            mainMenu = menu.MainMenu;
            resetMenu();
            ButtonStage[0] = new Vector2(155, 165);
            ButtonStage[1] = new Vector2(675, 165);
            ButtonStage[2] = new Vector2(1195, 165);
            ButtonStage[3] = new Vector2(405, 540);
            ButtonStage[4] = new Vector2(945, 540);
            BackButton = new Vector2(45, 35);
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
                        if(transparent < 1)
                        {
                            transparent += 0.01f;
                        }
                    }

                    //Start Button
                    //Hover
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= buttonPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < buttonPosition.Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= buttonPosition.X && Singleton.Instance.CurrentMouse.Position.X < buttonPosition.X + Button.Width))
                    {
                        //Pressed
                        if(Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            resetMenu();
                            mainMenu = menu.StageSelect;
                        }
                    }
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    break;
                case menu.StageSelect:
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Back
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= BackButton.Y && Singleton.Instance.CurrentMouse.Position.Y < BackButton.Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= BackButton.X && Singleton.Instance.CurrentMouse.Position.X < BackButton.X + Button.Width))
                    {
                        //Pressed
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            mainMenu = menu.MainMenu;
                        }
                    }
                    //Stage 1
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[0].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[0].Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[0].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[0].X + Button.Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 1;
                        }
                    }
                    //Stage 2
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[1].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[1].Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[1].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[1].X + Button.Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 2;
                        }
                    }
                    //Stage 3
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[2].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[2].Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[2].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[2].X + Button.Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 3;
                        }
                    }
                    //Stage 4
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[3].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[3].Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[3].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[3].X + Button.Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 4;
                        }
                    }
                    //Stage 5
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[4].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[4].Y + Button.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[4].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[4].X + Button.Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 5;
                        }
                    }
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    break;
                case menu.Setting:

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (mainMenu)
            {
                case menu.MainMenu:
                    spriteBatch.Draw(MenuBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Ship, shipPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Soup, soupPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, buttonPosition, null, Color.White * transparent, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Testing", fontPosition, Color.White);
                    break;
                case menu.StageSelect:
                    spriteBatch.Draw(MenuBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Ship, shipPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Soup, soupPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, ButtonStage[0], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, ButtonStage[1], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, ButtonStage[2], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, ButtonStage[3], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Button, ButtonStage[4], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    break;
                case menu.Setting:
                    break;
            }
        }

        void resetMenu()
        {
            fontPosition = new Vector2(250, -250);
            buttonPosition = new Vector2(630, 475);
            soupPosition = new Vector2(-700, 600);
            shipPosition = new Vector2(1750, 0);
            transparent = 0.0f;
        }

    }
}
