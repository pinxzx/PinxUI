using Rage;
using System.Drawing;
using PinxUI.Menus;
using PinxUI.Menus.Items;
using PinxUI.Fonts;

[assembly: Rage.Attributes.Plugin("Testing", Description = "This is my first plugin.", Author = "MyName")]

namespace PinxUI
{
    public static class EntryPoint
    {

        // JUST FOR INGAME TEST

        private static Menu meuMenu;
        public static void Main()
        {
            meuMenu = new Menu("My Menu", Color.White, FontFamilies.Calibri);

            Item item1 = new Item("My Option 1", Color.LightBlue, FontFamilies.Calibri);
            Item item2 = new Item("My Option 2", Color.Green, FontFamilies.Calibri);
            Item item3 = new Item("My Option 3", Color.Yellow, FontFamilies.Calibri);
            Item item4 = new Item("My Option 4", Color.White, FontFamilies.Calibri);
            Item item5 = new Item("My Option 5", Color.Black, FontFamilies.Calibri);
            Item item6 = new Item("My Option 6", Color.Gray, FontFamilies.Calibri);

            meuMenu.Items.Add(item1);
            meuMenu.Items.Add(item2);
            meuMenu.Items.Add(item3);
            meuMenu.Items.Add(item4);
            meuMenu.Items.Add(item5);
            meuMenu.Items.Add(item6);
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));
            meuMenu.Items.Add(new Item("My Option >6", Color.Gray, FontFamilies.Calibri));

            Game.FrameRender += OnFrameRender;

            while (true)
            {
                GameFiber.Yield();
                if (Game.IsKeyDown(System.Windows.Forms.Keys.T))
                {
                    meuMenu.Active = !meuMenu.Active;
                }
            }

            GameFiber.Hibernate();
        }
        private static void OnFrameRender(object sender, GraphicsEventArgs Graphics)
        {
            meuMenu.Process(Graphics);
            
        }
    }
}
