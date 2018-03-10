using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;

namespace SLS
{
    class ESP
    {
        public static bool Enabled = false;
        public static float Range = 5000f;

        public static void DoDrawPost()
        {
            if (!Enabled)
                return;

            var ply = Globals.GetLocalPlayer();

            Terraria.GameInput.PlayerInput.SetZoom_World();
            int num = Main.screenWidth;
            int num2 = Main.screenHeight;
            Vector2 vector = Main.screenPosition;
            Terraria.GameInput.PlayerInput.SetZoom_UI();
            float uIScale = Main.UIScale;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.myPlayer != i && !Main.player[i].dead)
                {

                    if (ply.team != 0 && Main.player[i].team == ply.team)
                        continue;

                    string text = Main.player[i].name;
                    if (Main.player[i].statLife < Main.player[i].statLifeMax2)
                    {
                        text = string.Concat(new object[]
				{
					text,
					": ",
					Main.player[i].statLife,
					"/",
					Main.player[i].statLifeMax2
				});
                    }
                    Vector2 vector2 = Main.fontMouseText.MeasureString(text);
                    float num3 = 0f;
                    if (Main.player[i].chatOverhead.timeLeft > 0)
                    {
                        num3 = -vector2.Y;
                    }
                    Vector2 vector3 = new Vector2((float)(num / 2) + vector.X, (float)(num2 / 2) + vector.Y);
                    Vector2 vector4 = Main.player[i].position;
                    vector4 += (vector4 - vector3) * (Main.GameViewMatrix.Zoom - Vector2.One);
                    float num4 = 0f;
                    float num5 = (float)Main.mouseTextColor / 255f;
                    Color color = new Color((int)((byte)((float)Main.teamColor[Main.player[i].team].R * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].G * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].B * num5)), (int)Main.mouseTextColor);
                    float num6 = vector4.X + (float)(Main.player[i].width / 2) - vector3.X;
                    float num7 = vector4.Y - vector2.Y - 2f + num3 - vector3.Y;
                    float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));

                    if ((num8 /16f * 2f) > Range)
                        continue;

                    int num9 = num2;
                    if (num2 > num)
                    {
                        num9 = num;
                    }
                    num9 = num9 / 2 - 30;
                    if (num9 < 100)
                    {
                        num9 = 100;
                    }
                    if (num8 < (float)num9)
                    {
                        vector2.X = vector4.X + (float)(Main.player[i].width / 2) - vector2.X / 2f - vector.X;
                        vector2.Y = vector4.Y - vector2.Y - 2f + num3 - vector.Y;
                    }
                    else
                    {
                        num4 = num8;
                        num8 = (float)num9 / num8;
                        vector2.X = (float)(num / 2) + num6 * num8 - vector2.X / 2f;
                        vector2.Y = (float)(num2 / 2) + num7 * num8;
                    }
                    if (Main.player[Main.myPlayer].gravDir == -1f)
                    {
                        vector2.Y = (float)num2 - vector2.Y;
                    }
                    vector2 *= 1f / uIScale;
                    Vector2 vector5 = Main.fontMouseText.MeasureString(text);
                    vector2 += vector5 * (1f - uIScale) / 4f;
                    if (num4 > 0f)
                    {
                        string textValue = Terraria.Localization.Language.GetTextValue("GameUI.PlayerDistance", (int)(num4 / 16f * 2f));
                        Vector2 vector6 = Main.fontMouseText.MeasureString(textValue);
                        vector6.X = vector2.X + vector5.X / 2f - vector6.X / 2f;
                        vector6.Y = vector2.Y + vector5.Y / 2f - vector6.Y / 2f - 20f;
                        Main.spriteBatch.DrawString(Main.fontMouseText, textValue, new Vector2(vector6.X - 2f, vector6.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, textValue, new Vector2(vector6.X + 2f, vector6.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, textValue, new Vector2(vector6.X, vector6.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, textValue, new Vector2(vector6.X, vector6.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, textValue, vector6, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                    Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(vector2.X - 2f, vector2.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(vector2.X + 2f, vector2.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(vector2.X, vector2.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(vector2.X, vector2.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.DrawString(Main.fontMouseText, text, vector2, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
            }
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "esp" && cmd != "e")
                return false;

            if(args.Length < 1)
            {
                Enabled = !Enabled;
                Globals.Print("ESP {0}", Enabled ? "Enabled" : "Disabled");
                return true;
            }

            switch(args[0])
            {
                case "range":
                case "r":
                    var range = 0f;
                    if(!args[1].TryConvert(out range))
                    {
                        Globals.Print("Invalid float value!");
                        return true;
                    }
                    Range = range;
                    Globals.Print("Range set to {0}", range);
                    break;

                default:
                    Globals.Print("Argument not found!");
                    break;
            }

            return true;
        }
    }
}
