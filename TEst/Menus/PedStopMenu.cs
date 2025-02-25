using System;
using Rage;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TEst.Menus
{
    public class PedStopMenu 
    {
        private static float _menuPosX = 0 + Game.Resolution.Width * 0.01f;
        private static float _menuPosY = 0 + Game.Resolution.Height * 0.01f;

        private static float _menuWidth = Game.Resolution.Width * 0.2f;
        private static float _menuHeight = Game.Resolution.Height*0.6f;

        internal static bool _menuVisible = true;

        internal static Texture _mainFrame;

        // Items
        internal static List<object> menuItems = new List<object>();

        internal void DrawMenu(GraphicsEventArgs Graphics)
        {
            if (_menuVisible)
            {
                _mainFrame = Game.CreateTextureFromFile(@"plugins\test\Frames\MainFrame.png");
                Graphics.Graphics.DrawTexture(_mainFrame, new RectangleF(_menuPosX, _menuPosY, _menuWidth, _menuHeight));

                float currentPosY = _menuPosY + 120;

                foreach (object item in menuItems)
                {
                    if (item is MenuItem menuItem)
                    {
                        currentPosY = DrawMenuItem(menuItem, currentPosY, Graphics);
                    }
                    else if (item is SeparatorItem separatorItem) 
                    {
                        currentPosY = DrawSeparator(separatorItem, currentPosY, Graphics);
                    }
                }
            }
        }

        private float DrawMenuItem(MenuItem item, float currentPosY, GraphicsEventArgs Graphics)
        {
            Game.LogTrivial(menuItems.IndexOf(item).ToString());
            item.PosX = _menuPosX * 1.5f;
            item.PosY = currentPosY;
            item.Width = _menuWidth * 0.96f;
            item.Height = _menuHeight * 0.05f;
            Graphics.Graphics.DrawRectangle(new RectangleF(item.PosX, item.PosY, item.Width, item.Height), item.Color);
            Graphics.Graphics.DrawText(item.Title, "Arial", 18f, new PointF(item.PosX * 1.15f, item.PosY), item.TextColor);
            currentPosY += item.Height + 2;
            return currentPosY;
        }
        private float DrawSeparator(SeparatorItem item, float currentPosY, GraphicsEventArgs Graphics)
        {
            Game.LogTrivial(menuItems.IndexOf(item).ToString());
            item.PosX = _menuPosX * 1.5f;
            item.PosY = currentPosY;
            item.Width = _menuWidth * 0.96f;
            item.Height = _menuHeight * 0.05f;
            item.TextPosX = item.PosX + _menuWidth * 0.5f + (item.Title.Length * -4.5f);
            RectangleF Label = new RectangleF(item.PosX, item.PosY, item.Width, item.Height);
            Graphics.Graphics.DrawRectangle(Label, item.Color);
            Graphics.Graphics.DrawText(item.Title, "Arial", 18f, new PointF(item.TextPosX, item.PosY), item.TextColor, Label);
            currentPosY += item.Height + 2;
            return currentPosY;
        }

        public void AddItem(object Item)
        {
            menuItems.Add(Item);
        }
    }

    public class MenuItem
    {

        internal float PosX { get; set; }
        internal float PosY { get; set; }
        internal float Width { get; set; }
        internal float Height { get; set; }
        // Text
        internal float TextPosX;

        internal float TextPosY;

        internal string Title { get; private set; }
        internal Color Color { get; set; }
        internal Color TextColor { get; set; }

        internal Font TextFont;

        internal bool isSeparator { get; set; }

        internal MenuItem(string Title, Color Color, Color TextColor)
        {
            this.Title = Title;
            this.Color = Color;
            this.TextColor = TextColor;
        }

    }
    public class SeparatorItem
    {

        internal float PosX { get; set; }
        internal float PosY { get; set; }
        internal float Width { get; set; }
        internal float Height { get; set; }
        // Text
        internal float TextPosX;
        internal float TextPosY;

        internal string Title { get; private set; }
        internal Color Color { get; set; }
        internal Color TextColor { get; set; }

        internal SeparatorItem(string Title, Color Color, Color TextColor)
        {
            this.Title = Title;
            this.Color = Color;
            this.TextColor = TextColor;
        }

    }
}
