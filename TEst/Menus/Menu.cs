using System;
using Rage;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PinxUI.Menus.Items;
using PinxUI.Fonts;
using System.Windows.Forms;
using PinxUI.Menus.Input;
namespace PinxUI.Menus
{
    public class Menu
    {

        public bool Active = true;

        public Menu menu;

        // View Configs
        public float PositionX = 10f;
        public float PositionY = 40f;

        public float Width { get; private set; }
        public float Height { get; private set; }

        // Configs
        public string Title { get; set; }
        public Color Color { get; set; }

        // Textures
        private Texture MainFrameTexture;
        private Texture ItemTexture;
        private Texture ItemSelectedTexture;

        // Rectangles
        private RectangleF _mainFrameRect;
        private RectangleF _itemFrameRect;

        // Items
        public List<IItem> Items { get; private set; } = new List<IItem>();

        // Font
        public string Font;
        // FontSizes
        private float _titleFontSize = 40f;
        private float _fontSizes = 24f;

        // Items Offset
        private float itemOffsetY;

        public int selectedItemIndex = 0;

        // Input Handler
        InputHandler InputHandler;

        public Menu(string Title, Color TitleColor, string Font)
        {
            InputHandler = new InputHandler(this);

            this.Title = Title;
            this.Color = TitleColor;
            this.Font = Font;

            Width = Game.Resolution.Width * 0.2f;
            Height = Game.Resolution.Height * 0.5f;

            MainFrameTexture = LoadTexture("SolidFrame");
            ItemTexture = LoadTexture("ItemFrame");
            ItemSelectedTexture = LoadTexture("ItemSelectedFrame");

            if (MainFrameTexture == null)
            {
                Game.LogTrivial("Failed to load MainFrameTexture.");
            }

            if (ItemTexture == null)
            {
                Game.LogTrivial("Failed to load ItemTexture.");
            }
            FontFamily[] fontFamilies = FontFamily.Families;

            // Imprime o nome de cada família de fontes
            foreach (FontFamily font in fontFamilies)
            {
            Game.LogTrivial (font.Name);
            }
        }

        /// <summary>
        /// Draw the Menu, need to be called every Tick.
        /// </summary>
        public void Process(GraphicsEventArgs GraphicsArgs)
        {
            if (Active)
            {
                // input
                InputHandler.ProcessInput();
                //
                var e = GraphicsArgs.Graphics;

                _mainFrameRect = new RectangleF(PositionX, PositionY, Width, Height);

                // Check if MainFrameTexture is loaded
                if (MainFrameTexture != null)
                {
                    e.DrawTexture(MainFrameTexture, _mainFrameRect);
                    // Draw the Menu Title
                    SizeF _measureTextSize = Rage.Graphics.MeasureText(Title, Font, _titleFontSize); // Measure the text 
                    PointF _textPointF = new PointF(PositionX + (Width / 2) - (_measureTextSize.Width/2), PositionY + 10f);
                    e.DrawText(Title, Font, _titleFontSize, _textPointF, Color, _mainFrameRect);
                }
                else
                {
                    Game.LogTrivial("MainFrameTexture is not loaded.");
                }

                // Draw Items
                itemOffsetY = PositionY * 1.8f;
                for (int i = 0; i < Items.Count; i++)
                {
                    Items[i].Width = Width * 0.9f;
                    Items[i].Height = Height * 0.08f;
                    Items[i].PositionX = PositionX + ((Width / 2) - (Items[i].Width / 2));
                    Items[i].PositionY = PositionY + itemOffsetY;

                    _itemFrameRect = new RectangleF(Items[i].PositionX, Items[i].PositionY, Items[i].Width, Items[i].Height);

                    if (_itemFrameRect.IntersectsWith(_mainFrameRect))
                    {
                        // Check if ItemTexture is loaded
                        if (ItemTexture != null && ItemSelectedTexture != null)
                        {
                            if (Items.IndexOf(Items[i]) == selectedItemIndex) 
                            {
                                e.DrawTexture(ItemSelectedTexture, _itemFrameRect);
                            }
                            else
                            {
                                e.DrawTexture(ItemTexture, _itemFrameRect);
                            }
                            
                        }
                        else
                        {
                            Game.LogTrivial("ItemTexture is not loaded.");
                        }

                        // Items Text
                        SizeF _textMeasure = Rage.Graphics.MeasureText(Items[i].Text, Items[i].Font, _fontSizes); // Measure the text 
                        float textOffsetY = Items[i].PositionY + ((Items[i].Height / 2f) - (_textMeasure.Height)); // Centralize the text Y position. Not accurate,,,
                        e.DrawText(Items[i].Text, Items[i].Font, _fontSizes, new PointF(Items[i].PositionX + 5f, textOffsetY), Items[i].TextColor, _itemFrameRect);
                        itemOffsetY += Items[i].Height * 1.1f;
                    }
                }
            }
        }

        /// <summary>
        /// Disable specific game controls.
        /// </summary>
        private void DisableControls()
        {

        }

        /// <summary>
        /// Add a item to the Menu.
        /// </summary>
        public void AddItem(IItem item)
        {
            if (item != null)
            {
                Items.Add(item);
            }
            else
            {
                Game.LogTrivial("Attempted to add a null item to the menu.");
            }
        }

        private Texture LoadTexture(string Texture)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream($"PinxUI.Menus.Frames.MainFrames.{Texture}.png"))
                {
                    if (stream != null)
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            string tempPath = Path.Combine(Path.GetTempPath(), $"{Texture}_{Guid.NewGuid()}.png");
                            image.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);
                            Texture texture = Game.CreateTextureFromFile(tempPath);
                            GameFiber.StartNew(() =>
                            {
                                GameFiber.Wait(5000);
                                try
                                {
                                    File.Delete(tempPath);
                                }
                                catch (Exception e)
                                {
                                    Game.LogTrivial("Error while deleting temporary image: " + e);
                                }
                            });
                            return texture;
                        }
                    }
                    else
                    {
                        Game.LogTrivial($"Resource stream is null for texture {Texture}");
                    }
                }
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"Exception in LoadTexture for texture {Texture}: {ex}");
            }
            return null;
        }
    }
}