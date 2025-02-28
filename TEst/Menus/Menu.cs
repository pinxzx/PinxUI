using System;
using Rage;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.CodeDom;
using PinxUI.Menus.Items;

namespace PinxUI.Menus
{
    public class Menu
    {
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

        // Rectangles
        private RectangleF _mainFrameRect;
        private RectangleF _itemFrameRect;

        // Items
        public List<IItem> Items { get; private set; } = new List<IItem>();

        public Menu(string Title, Color TitleColor)
        {
            this.Title = Title;
            this.Color = TitleColor;

            Width = Game.Resolution.Width * 0.2f;
            Height = Game.Resolution.Height * 0.5f;

            MainFrameTexture = LoadTexture("SolidFrame");
            ItemTexture = LoadTexture("ItemFrame");

            if (MainFrameTexture == null)
            {
                Game.LogTrivial("Failed to load MainFrameTexture.");
            }

            if (ItemTexture == null)
            {
                Game.LogTrivial("Failed to load ItemTexture.");
            }
        }

        /// <summary>
        /// Draw the Menu, need to be called every Tick.
        /// </summary>
        public void Process(GraphicsEventArgs GraphicsArgs)
        {
            var e = GraphicsArgs.Graphics;

            _mainFrameRect = new RectangleF(PositionX, PositionY, Width, Height);

            // Check if MainFrameTexture is loaded
            if (MainFrameTexture != null)
            {
                e.DrawTexture(MainFrameTexture, _mainFrameRect);
            }
            else
            {
                Game.LogTrivial("MainFrameTexture is not loaded.");
            }

            // Draw Items
            float itemOffsetY = 30f;
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].PositionX = PositionX;
                Items[i].PositionY = PositionY + itemOffsetY;
                Items[i].Width = Width;
                Items[i].Height = Height * 0.1f;

                float textOffsetY = Items[i].PositionY + (Items[i].Height / 2) - 24f;

                Game.DisplayNotification(i.ToString());

                // Check if ItemTexture is loaded
                if (ItemTexture != null)
                {
                    e.DrawTexture(ItemTexture, new RectangleF(Items[i].PositionX, Items[i].PositionY, Items[i].Width, Items[i].Height));
                }
                else
                {
                    Game.LogTrivial("ItemTexture is not loaded.");
                }

                e.DrawText(Items[i].Text, "Arial", 24f, new PointF(Items[i].PositionX, textOffsetY), Items[i].TextColor);
                itemOffsetY += Items[i].Height * 1.1f;
            }
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