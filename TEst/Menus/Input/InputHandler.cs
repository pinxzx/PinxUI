using Rage;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinxUI.Menus.Input
{
    internal class InputHandler
    {
        public Menu Menu { get; internal set; }
        internal InputHandler(Menu Menu)
        {
            this.Menu = Menu;
        }

        public void ProcessInput() 
        {
            int maxItems = Menu.Items.Count - 1;
            int minItems = (Menu.Items.Count - 1) - (Menu.Items.Count - 1);
            if (Game.IsKeyDown(Keys.Up))
            {
                Menu.selectedItemIndex -= 1;
            }
            if (Game.IsKeyDown(Keys.Down))
            {
                Menu.selectedItemIndex += 1;
            }
        }

    }
}
