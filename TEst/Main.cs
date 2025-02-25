using Rage;
using System.Drawing;
using TEst.Menus;

[assembly: Rage.Attributes.Plugin("My First Plugin", Description = "This is my first plugin.", Author = "MyName")]

namespace MyFirstPlugin
{
    public static class EntryPoint
    {
        private static PedStopMenu pedStopMenu;
        public static void Main()
        {
            

            pedStopMenu = new PedStopMenu();
            SeparatorItem Separator = new SeparatorItem("Separator Dynamic Bla Bla", Color.FromArgb(150, Color.Black), Color.White);
            MenuItem RequestPedCheckItem = new MenuItem("Request Ped Check to Dispatch", Color.FromArgb(100, Color.Black), Color.LightBlue);
            SeparatorItem Separator2 = new SeparatorItem("Separator", Color.FromArgb(150, Color.Black), Color.White);
            MenuItem TesteItem = new MenuItem("Search Ped", Color.FromArgb(100, Color.Black), Color.LightBlue);
            SeparatorItem Separator3 = new SeparatorItem("Separator", Color.FromArgb(150, Color.Black), Color.White);
            MenuItem Teste2Item = new MenuItem("Arrest Ped", Color.FromArgb(100, Color.Black), Color.Red);
            MenuItem Teste3Item = new MenuItem("Release Ped", Color.FromArgb(100, Color.Black), Color.Orange);

            pedStopMenu.AddItem(Separator);
            pedStopMenu.AddItem(RequestPedCheckItem);
            pedStopMenu.AddItem(Separator2);
            pedStopMenu.AddItem(TesteItem);
            pedStopMenu.AddItem(Separator3);
            pedStopMenu.AddItem(TesteItem);
            pedStopMenu.AddItem(Teste2Item);


            Game.FrameRender += OnFrameRender;
            GameFiber.Hibernate();
        }
        private static void OnFrameRender(object sender, GraphicsEventArgs Graphics)
        {
            pedStopMenu.DrawMenu(Graphics); 
        }
    }
}
