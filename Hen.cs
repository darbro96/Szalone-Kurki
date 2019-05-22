using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Szalone_Kurki
{
    class Hen : MyObject
    {
        public Rectangle legsRectangle;
        public Hen(int x, int y, int width, int height, string fileName)
        {
            rectangle = new Rectangle(x, y, width, height);
            this.textureFile = fileName;
            double dx = 0.3625 * (double)rectangle.Width;
            this.legsRectangle = new Rectangle(rectangle.X+(int)dx, rectangle.Y, (rectangle.Width * 2876) / 10000, rectangle.Height);

        }

        public void position(int y)
        {
            this.rectangle.Y = y;
            legsRectangle.Y = this.rectangle.Y;
        }
    }
}