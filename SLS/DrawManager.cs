using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

using Microsoft.Xna.Framework;

using ReLogic.Graphics;

namespace SLS
{
    class DrawManager
    {
        static bool _Drawing = false;

        public static bool Drawing
        {
            get
            {
                return _Drawing;
            }
        }

        public static void StartDrawing()
        {
            Main.spriteBatch.Begin();
            _Drawing = true;
        }

        public static void EndDrawing()
        {
            Main.spriteBatch.End();
            _Drawing = false;

            lastModuleText = new Vector2(Main.screenWidth, Main.screenHeight);
            lastModuleTextSize = Vector2.Zero;

            if (/*Main.invasionType != 0 && Main.invasionProgress != -1 && */Main.invasionProgressAlpha > 0f) //Higher the Module list to not overlap with invasion progress
                lastModuleText.Y -= 100;
        }

        public static void DrawString(string text, Color color, Vector2 position)
        {
            Main.spriteBatch.DrawString(Main.fontMouseText, text, position, color);
        }

        static Vector2 lastModuleText;
        static Vector2 lastModuleTextSize;

        public static void AddEnabledModule(string text, Color color)
        {
            lastModuleTextSize = Main.fontMouseText.MeasureString(text);
            lastModuleText.Y -= lastModuleTextSize.Y;
            lastModuleText.X = Main.screenWidth - lastModuleTextSize.X;
            DrawString(text, color, lastModuleText);
        }
    }
}
