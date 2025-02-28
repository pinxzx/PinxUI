using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinxUI.Menus.Items
{
    public interface IItem
    {
        float PositionX { get; set; }
        float PositionY { get; set; }

        float Width { get; set; }
        float Height { get; set; }

        string Text { get; set; }
        Color TextColor { get; set; }
}
}
