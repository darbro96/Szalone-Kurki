using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class Menu
    {
        public string myName = "Menu";
        public bool isDraw = false;

        public int width;
        public int height;

        private bool isPlay = false;

        public Background background;
        public List<MyObject> myObjects = new List<MyObject>();
        public List<RoundIcon> icons = new List<RoundIcon>();
        public List<MySong> songs = new List<MySong>();

        private int counter = 0;
        private int noBackground = 0;

        public Menu(int width, int height)
        {
            this.width = width;
            this.height = height;
            background = new Background("menuTlo0", 0, 0, this.width, this.height);
            loadObjects(this.width, this.height);
        }

        private void loadObjects(int width, int height)
        {
            MyObject tlo0 = new MyObject("menuTlo0", 0, 0, width, height, "background0", 0);
            tlo0.isDraw = false;
            myObjects.Add(tlo0);
            MyObject tlo1 = new MyObject("menuTlo1", 0, 0, width, height, "background1", 0);
            tlo1.isDraw = false;
            myObjects.Add(tlo1);
            MyObject tlo2 = new MyObject("menuTlo2", 0, 0, width, height, "background2", 0);
            tlo2.isDraw = false;
            myObjects.Add(tlo2);

            RoundIcon roundIcon = new RoundIcon("gwiazdki0", 0, 0, width, height, "Round1", "button1");
            icons.Add(roundIcon);
            RoundIcon roundIcon2 = new RoundIcon("gwiazdki0", roundIcon.width, 0, width, height, "Round2", "button2");
            icons.Add(roundIcon2);
            RoundIcon roundIcon3 = new RoundIcon("gwiazdki0", roundIcon.width * 2, 0, width, height, "Round3", "button3");
            icons.Add(roundIcon3);

            MySong mainSound = new MySong("bensound-funnysong", "mainSound");
            songs.Add(mainSound);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.075f;
            MediaPlayer.Play(songs.Find(o => o.myName.Equals("mainSound")).song);
        }

        public void Update(TouchCollection touchPlaces, GameTime gameTime)
        {

            if (!isPlay)
            {
                MediaPlayer.Play(songs.Find(o => o.myName.Equals("mainSound")).song);
                //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
                isPlay = true;
            }


            background.texture = findByName("background" + noBackground).texture;

            counter++;
            if (counter < 20)
            {
                noBackground = 1;
            }
            if (counter >= 40)
            {
                noBackground = 2;
            }
            if (counter >= 60)
            {
                noBackground = 0;
                counter = 0;
            }

            foreach (TouchLocation touch in touchPlaces)
            {
                Vector2 touchPosition = touch.Position;
                if (touch.State == TouchLocationState.Pressed)
                {
                    if (isTouchButton(touchPosition, findIconByName("button1")))
                    {
                        myName = "Round1";
                        MediaPlayer.Stop();
                        isPlay = false;
                    }
                    if (isTouchButton(touchPosition, findIconByName("button2")))
                    {
                        myName = "Round2";
                        MediaPlayer.Stop();
                        isPlay = false;
                    }
                    if (isTouchButton(touchPosition, findIconByName("button3")))
                    {
                        myName = "Round3";
                        MediaPlayer.Stop();
                        isPlay = false; 
                    }
                }
            }

        }

        private bool isTouchButton(Vector2 touchPosition, RoundIcon icon)
        {
            if (touchPosition.X > icon.rectangle.X && touchPosition.X < icon.rectangle.X + icon.rectangle.Width && touchPosition.Y > icon.rectangle.Y && touchPosition.Y < icon.rectangle.Y + icon.rectangle.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private MyObject findByName(string name)
        {
            return myObjects.Find(o => o.myName.Equals(name));
        }

        private RoundIcon findIconByName(string name)
        {
            return icons.Find(o => o.myName.Equals(name));
        }
    }
}