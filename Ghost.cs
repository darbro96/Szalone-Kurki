using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Szalone_Kurki
{
    class Ghost : MyObject
    {
        public Texture2D texture1;
        public Texture2D texture2;
        public string textureFile2;

        public Ghost(int x, int y, int width, int height)
        {
            this.textureFile = "duch";
            this.textureFile2 = "duch2";
            this.width = width;
            this.height = height;
            this.rectangle = new Rectangle(x, y, width, height);
        }
    }
}