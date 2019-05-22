using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class StartView
    {
        public string myName = "startView";
        public bool isDraw = true;

        public int width;
        public int height;

        public Background background;

        public List<MyObject> myObjects = new List<MyObject>();

        private int noBackground = 0;
        private int counter = 0;

        public StartView(int width, int height)
        {
            this.width = width;
            this.height = height;
            background = new Background("startGra0", 0, 0, this.width, this.height);
            loadObjects(width,height);
        }

        private void loadObjects(int width, int height)
        {
            MyObject background0 = new MyObject("startGra0", 0, 0, width, height, "background0", 1);
            background0.isDraw = false;
            MyObject background1 = new MyObject("startGra1", 0, 0, width, height, "background1", 1);
            background1.isDraw = false;
            myObjects.Add(background0);
            myObjects.Add(background1);
        }

        public void Update(TouchCollection touchPlaces)
        {
            background.texture = myObjects[myObjects.FindIndex(o=>o.myName.Equals("background"+noBackground))].texture;

            counter++;
            if (counter == 20)
            {
                noBackground = 1;
            }
            if (counter >= 40)
            {
                noBackground = 0;
                counter = 0;
            }

            foreach (TouchLocation touch in touchPlaces)
            {
                Vector2 touchPosition = touch.Position;
                if (touch.State == TouchLocationState.Pressed)
                {
                    //isDraw = false;
                    myName = "Menu";
                }
            }
;        }
    }
}