using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class RoundIcon : MyObject
    {
        public Rectangle iconRectangle;
        public string toRound;

        public RoundIcon(string fileName, int x, int y, int width, int height, string round, string buttonName)
        {
            textureFile = fileName;
            this.rectangle = new Rectangle(x, y, (270 * width) / 1080, (260 * width) / 1080);
            this.width = this.rectangle.Width; ;
            this.height = this.rectangle.Height;
            iconRectangle = new Rectangle(x+(35 * this.rectangle.Width) / 270, y+(30 * this.rectangle.Height) / 270, (200 * width) / 1080, (200 * width) / 1080);
            toRound = round;
            myName = buttonName;
        }
    }
}