using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Szalone_Kurki
{
    class Ground : MyObject
    {
        public bool isFloor = true;
        public bool isMoving = false;

        public Ground(string name, int x, int y, int width, int height, int levelDraw, bool isFloor)
        {
            textureFile = name;
            this.width = width;
            this.height = height;
            this.levelDraw = levelDraw;
            this.isFloor = isFloor;
            rectangle = new Rectangle(x, y, width, height);
            this.levelDraw = 0;
        }
    }
}