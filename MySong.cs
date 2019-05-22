using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Szalone_Kurki
{
    class MySong
    {
        public Song song;
        public string fileName;
        public string myName;

        public MySong(string fileName, string myName)
        {
            this.fileName = fileName;
            this.myName = myName;
        }
    }
}