using Rage;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System;
using System.Media;

namespace PinxUI.Menus.Input
{
    internal class InputHandler
    {
        public Menu Menu { get; internal set; }

        // Sound
        private ISoundOut soundOut;

        internal InputHandler(Menu Menu)
        {
            this.Menu = Menu;
        }

        public void ProcessInput() 
        {
            // Disable Phone when Input is being processed.
            Game.DisableControlAction(0, GameControl.Phone, true);
            //
            int maxItems = Menu.Items.Count - 1;
            int minItems = (Menu.Items.Count - 1) - (Menu.Items.Count - 1);
            if (Game.IsKeyDown(Keys.Up))
            {
                Menu.selectedItemIndex -= 1;
                PlaySound("changedItem");
            }
            if (Game.IsKeyDown(Keys.Down))
            {
                Menu.selectedItemIndex += 1;
                PlaySound("changedItem");
            }
            if (Menu.selectedItemIndex < minItems) { Menu.selectedItemIndex = maxItems; }
            else if (Menu.selectedItemIndex > maxItems) { Menu.selectedItemIndex = minItems; }
        }


        private void PlaySound(string audioName)
        {
            soundOut?.Stop();
            soundOut?.Dispose();
            soundOut = null;
            
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("PinxUI.Menus.Sounds."+audioName+".wav"))
            {
                SoundPlayer player = new SoundPlayer(stream);
                player.Play();

            }

                
        }

    }
}
