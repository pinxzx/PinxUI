using Rage;
using System.Drawing;
using PinxUI.Menus;
using PinxUI.Menus.Items;

[assembly: Rage.Attributes.Plugin("Testing", Description = "This is my first plugin.", Author = "MyName")]

namespace PinxUI
{
    public static class EntryPoint
    {

        // JUST FOR INGAME TEST

        private static Menu meuMenu;
        public static void Main()
        {
            meuMenu = new Menu("Boas", Color.Red);

            Item item1 = new Item("Search Ped", Color.LightBlue);

            Item item2 = new Item("Arrest Ped", Color.OrangeRed);

            meuMenu.Items.Add(item1);
            meuMenu.Items.Add(item2);

            Game.FrameRender += OnFrameRender;

            GameFiber.Hibernate();
        }
        private static void OnFrameRender(object sender, GraphicsEventArgs Graphics)
        {
            meuMenu.Process(Graphics);
        }
    }
}
