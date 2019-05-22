using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class Board : MyObject
    {
        public Board(string name, int x, int y, int width, int height, int levelDraw)
        {
            textureFile = name;
            this.width = width;
            this.height = height;
            this.levelDraw = levelDraw;
            rectangle = new Rectangle(x, y, width, height);
            this.levelDraw = 0;
        }
    }
}