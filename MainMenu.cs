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
        public enum menu
        {
            MainMenu, StageSelect, Setting
        }
        Texture2D MenuBackground, StartButton, Ship, Soup, Prop, BackButton, SettingButton, ExitButton, StageBackground;
        Texture2D font;
        Texture2D[] StageButton;
        Vector2 fontPosition, shipPosition, soupPosition, startButtonPosition, propPosition, exitButtonPosition, settingButtonPosition;
        Vector2[] ButtonStage = new Vector2[5];
        Vector2 BackPosition;
        public menu page;
        public bool hit, exit;
        public int stage;

        float transparent;

        public MainMenu(Texture2D MenuBackground, Texture2D StartButton, Texture2D SettingButton, Texture2D ExitButton, Texture2D Ship, Texture2D Soup, Texture2D font, Texture2D prop, Texture2D BackButton, Texture2D StageBackground, Texture2D[] StageButton)
        {
            this.MenuBackground = MenuBackground;
            this.StartButton = StartButton;
            this.SettingButton = SettingButton;
            this.ExitButton = ExitButton;
            this.BackButton = BackButton;
            this.StageBackground = StageBackground;
            this.StageButton = StageButton;
            this.Ship = Ship;
            this.Soup = Soup;
            this.font = font;
            Prop = prop;
            page = menu.MainMenu;
            startButtonPosition = new Vector2(575, 400);
            exitButtonPosition = new Vector2(650, 675);
            settingButtonPosition = new Vector2(620, 550);
            resetMenu();
            ButtonStage[0] = new Vector2(155, 165);
            ButtonStage[1] = new Vector2(675, 165);
            ButtonStage[2] = new Vector2(1195, 165);
            ButtonStage[3] = new Vector2(405, 540);
            ButtonStage[4] = new Vector2(945, 540);
            BackPosition = new Vector2(45, 35);
        }

        public void Update(GameTime gameTime)
        {
            switch (page)
            {
                case menu.MainMenu:
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Moving Animation
                    if (fontPosition.Y < 10)
                    {
                        fontPosition.Y += 2;
                    }
                    else if(!hit)
                    {
                        if(soupPosition.X < 300)
                        {
                            soupPosition.X += 25;
                        }
                        if(shipPosition.X > 800)
                        {
                            shipPosition.X -= 25;
                        }
                        if(soupPosition.X >= 300 && shipPosition.X <= 800)
                        {
                            hit = true;
                        }
                    }
                    else
                    {
                        if (soupPosition.X > -0)
                        {
                            soupPosition.X -= 5;
                        }
                        if (shipPosition.X < 1075)
                        {
                            shipPosition.X += 5;
                        }
                        if (transparent < 1)
                        {
                            transparent += 0.01f;
                        }
                        if (propPosition.X < 50)
                        {
                            propPosition.X += 5;
                        }
                        //Start Button
                        //Hover
                        if ((Singleton.Instance.CurrentMouse.Position.Y >= startButtonPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < startButtonPosition.Y + StartButton.Height) &&
                            (Singleton.Instance.CurrentMouse.Position.X >= startButtonPosition.X && Singleton.Instance.CurrentMouse.Position.X < startButtonPosition.X + StartButton.Width))
                        {
                            //Pressed
                            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                            {
                                resetMenu();
                                page = menu.StageSelect;
                            }
                        }
                        //Setting Button
                        else if ((Singleton.Instance.CurrentMouse.Position.Y >= settingButtonPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < settingButtonPosition.Y + SettingButton.Height) &&
                            (Singleton.Instance.CurrentMouse.Position.X >= settingButtonPosition.X && Singleton.Instance.CurrentMouse.Position.X < settingButtonPosition.X + SettingButton.Width))
                        {
                            //Pressed
                            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                            {
                                page = menu.Setting;
                            }
                        }
                        //Exit Button
                        else if ((Singleton.Instance.CurrentMouse.Position.Y >= exitButtonPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < exitButtonPosition.Y + ExitButton.Height) &&
                            (Singleton.Instance.CurrentMouse.Position.X >= exitButtonPosition.X && Singleton.Instance.CurrentMouse.Position.X < exitButtonPosition.X + ExitButton.Width))
                        {
                            //Pressed
                            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                            {
                                exit = true;
                            }
                        }
                    }
                    if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                    {
                        fontPosition.Y = 10;
                        soupPosition.X = 0;
                        shipPosition.X = 1075;
                        transparent = 1f;
                        propPosition.X = 50;
                        hit = true;
                    }
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    break;
                case menu.StageSelect:
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Back
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= BackPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < BackPosition.Y + BackButton.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= BackPosition.X && Singleton.Instance.CurrentMouse.Position.X < BackPosition.X + BackButton.Width))
                    {
                        //Pressed
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            resetMenu();
                            page = menu.MainMenu;
                        }
                    }
                    //Stage 1
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[0].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[0].Y + StageButton[0].Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[0].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[0].X + StageButton[0].Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 1;
                        }
                    }
                    //Stage 2
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[1].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[1].Y + StageButton[1].Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[1].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[1].X + StageButton[1].Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 2;
                        }
                    }
                    //Stage 3
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[2].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[2].Y + StageButton[2].Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[2].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[2].X + StageButton[2].Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 3;
                        }
                    }
                    //Stage 4
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[3].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[3].Y + StageButton[3].Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[3].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[3].X + StageButton[3].Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 4;
                        }
                    }
                    //Stage 5
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= ButtonStage[4].Y && Singleton.Instance.CurrentMouse.Position.Y < ButtonStage[4].Y + StageButton[4].Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= ButtonStage[4].X && Singleton.Instance.CurrentMouse.Position.X < ButtonStage[4].X + StageButton[4].Width))
                    {
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            stage = 5;
                        }
                    }
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    break;
                case menu.Setting:
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Back
                    if ((Singleton.Instance.CurrentMouse.Position.Y >= BackPosition.Y && Singleton.Instance.CurrentMouse.Position.Y < BackPosition.Y + BackButton.Height) &&
                        (Singleton.Instance.CurrentMouse.Position.X >= BackPosition.X && Singleton.Instance.CurrentMouse.Position.X < BackPosition.X + BackButton.Width))
                    {
                        //Pressed
                        if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
                        {
                            resetMenu();
                            page = menu.MainMenu;
                        }
                    }
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (page)
            {
                case menu.MainMenu:
                    spriteBatch.Draw(MenuBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Ship, shipPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Soup, soupPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Prop, propPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StartButton, startButtonPosition, null, Color.White * transparent, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(SettingButton, settingButtonPosition, null, Color.White * transparent, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(ExitButton, exitButtonPosition, null, Color.White * transparent, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(font, fontPosition, Color.Yellow);
                    break;
                case menu.StageSelect:
                    spriteBatch.Draw(StageBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(BackButton, BackPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StageButton[0], ButtonStage[0], null, Color.White * 0.6f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StageButton[1], ButtonStage[1], null, Color.White * 0.6f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StageButton[2], ButtonStage[2], null, Color.White * 0.6f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StageButton[3], ButtonStage[3], null, Color.White * 0.6f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StageButton[4], ButtonStage[4], null, Color.White * 0.6f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    break;
                case menu.Setting:
                    spriteBatch.Draw(MenuBackground, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(BackButton, BackPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Ship, shipPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Soup, soupPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                    spriteBatch.Draw(font, fontPosition, Color.Yellow);
                    break;
            }
        }

        public void resetMenu()
        {
            fontPosition = new Vector2(120, -450);
            soupPosition = new Vector2(-700, 400);
            shipPosition = new Vector2(1750, 100);
            propPosition = new Vector2(-500, 50);
            transparent = 0.0f;
            hit = false;
            stage = 0;
        }

    }
}
