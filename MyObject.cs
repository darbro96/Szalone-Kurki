using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Szalone_Kurki
{
    class MyObject
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Color color = Color.White;

        public string textureFile;
        public int width;
        public int height;
        public string myName = "";

        public int levelDraw = 1;
        public bool isDraw = true;
        public bool canTouch = true;
        public bool isMove = true;

        public MyObject(string fileName, int x, int y, int width, int height, string myName, int levelDraw)
        {
            textureFile = fileName;
            this.width = width;
            this.height = height;
            this.myName = myName;
            this.levelDraw = levelDraw;
            rectangle = new Rectangle(x, y, width, height);
        }

        public MyObject(string fileName, int x, int y, int width, int height, string myName, int levelDraw, bool isMove)
        {
            textureFile = fileName;
            this.width = width;
            this.height = height;
            this.myName = myName;
            this.levelDraw = levelDraw;
            rectangle = new Rectangle(x, y, width, height);
            this.isMove = isMove;
        }

        public MyObject()
        {

        }
    }
}