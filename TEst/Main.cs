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
            meuMenu = new Menu("Example Menu", Color.Red);

            Item item1 = new Item("Example Item: Clipping Test ABCDEFGH", Color.LightBlue);
            Item item2 = new Item("Arrest Ped", Color.OrangeRed);
            Item item3 = new Item("Example Item: Clipping Test ABCDEFGH", Color.LightBlue);
            Item item4 = new Item("Arrest Ped", Color.OrangeRed);
            Item item5 = new Item("Example Item: Clipping Test ABCDEFGH", Color.LightBlue);
            Item item6 = new Item("Arrest Ped", Color.OrangeRed);

            meuMenu.Items.Add(item1);
            meuMenu.Items.Add(item2);
            meuMenu.Items.Add(item3);
            meuMenu.Items.Add(item4);
            meuMenu.Items.Add(item5);
            meuMenu.Items.Add(item6);

            Game.FrameRender += OnFrameRender;

            GameFiber.Hibernate();
        }
        private static void OnFrameRender(object sender, GraphicsEventArgs Graphics)
        {
            meuMenu.Process(Graphics);
        }
    }
}
