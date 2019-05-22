using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Szalone_Kurki
{
    class MySoundEffect
    {
        public SoundEffect soundEffect;
        public string fileName;
        public string myName;

        public MySoundEffect(string fileName, string myName)
        {
            this.fileName = fileName;
            this.myName = myName;
        }
    }
}