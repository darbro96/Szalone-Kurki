using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Szalone_Kurki
{
    class Background : MyObject
    {
        public Background(string fileName, int x, int y, int width, int height)
        {
            textureFile = fileName;
            this.width = width;
            this.height = height;
            rectangle = new Rectangle(x, y, width, height);
        }
    }
}