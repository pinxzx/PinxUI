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

namespace PinxUI.Menus.Items
{
    class Item : IItem
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }

        public float Width {  get; set; } 
        public float Height { get; set; }

        public string Text { get; set; } 
        public Color TextColor { get; set; } 

        public Item(string Text, Color Color)
        {
            this.Text = Text;
            this.TextColor = Color;
        }

    }
}
