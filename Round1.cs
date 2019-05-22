using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Threading;

namespace Szalone_Kurki
{
    class Round1 : RoundInterface
    {
        public string myName = "Round1";
        public bool isDraw = false;

        public int width;
        public int height;
        private int yGround = 0;
        private bool canMove = true;
        private int henY = 0;
        private int henYStart = 0;
        private bool right = true;
        private bool jump = false;
        private int jumpCounter = 0;
        private bool jumpUp = true;
        private bool jumpDown = false;
        private int helpX = 0;
        private int xChicken = 0;
        private int chickenCounter = 0;

        public Background background;
        public Hen hen;

        public List<MyObject> myObjects = new List<MyObject>();
        public List<Ground> groundList = new List<Ground>();
        public List<MySong> songs = new List<MySong>();
        public List<MySoundEffect> soundEffects = new List<MySoundEffect>();

        private Rectangle help = new Rectangle(0, 0, 0, 0);
        private int dx = 0;
        //Odpowiada za efekt dreptania
        private int moveCounter = 0;
        private int maxMoveCounter = 30;

        private bool wpadlaDoWody = false;

        private int roundTime = 60;
        private int time = 0;
        private int timeSec = 0;
        private int timeLeft = 0;

        private bool isPlayChoirVoice = false;
        private bool isPlayMain = false;

        public Round1(int width, int height)
        {
            this.width = width;
            this.height = height;
            background = new Background("bialoNiebieskieNiebo", 0, 0, this.width, this.height);
            createListGround(this.width, this.height);
            loadObjects(this.width, this.height);
            dx = (width * 3) / 1080;
            if (dx < 1)
            {
                dx = 1;
            }
        }

        private void createListGround(int width, int height)
        {
            int w = (300 * width) / 1920;
            int h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            w = w + (w / 4);
            int xPos = 0;
            yGround = height - h;
            for (int i = 0; i < (width * 9) / 10; i += w)
            {
                groundList.Add(new Ground("trawa", xPos, height - h, w, h, 0, true));
                xPos += w;
            }
            int xPos2 = xPos;
            for (int i = 0; i < 1; i++)
            {
                Ground ground = new Ground("woda0", xPos, height - h, w, h, 0, true);
                ground.canTouch = false;
                groundList.Add(ground);
                xPos += w;
            }
            for (int i = 0; i < 2; i++)
            {
                groundList.Add(new Ground("trawa", xPos, height - h, w, h, 0, true));
                xChicken = xPos;
                xPos += w;
            }
            for (int i = 0; i < 1; i++)
            {
                Ground ground = new Ground("woda0", xPos, height - h, w, h, 0, true);
                ground.canTouch = false;
                groundList.Add(ground);
                xPos += w;
                helpX = xPos;
            }
            for (int i = 0; i < width * 2; i += w)
            {
                groundList.Add(new Ground("trawa", xPos, height - h, w, h, 0, true));
                xPos += w;
            }
        }

        private void loadObjects(int width, int height)
        {
            int w = (300 * width) / 1920;
            int h = (300 * height) / 1080;
            w = w / 2;
            h = h / 2;

            for (int i = 0; i < 10; i++)
            {
                MyObject num = new MyObject(i.ToString(), 0, 0, w, h, i.ToString(), 0, false);
                num.isDraw = false;
                myObjects.Add(num);
            }

            MyObject digitOfTens = new MyObject("1", width - (2 * w), 0, w, h, "digitOfTens", 1, false);
            myObjects.Add(digitOfTens);

            MyObject digitOfUnity = new MyObject("1", width - w, 0, w, h, "digitOfUnity", 1, false);
            myObjects.Add(digitOfUnity);

            w = (400 * width) / 1920;
            h = (400 * height) / 1080;
            w = w / 3;
            h = h / 3;
            hen = new Hen((width / 3), yGround - h, w, h, "kura");

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = (w * 3) / 4;
            h = (h * 3) / 4;
            MyObject buttonR = new MyObject("PrzyciskPr", width - w, height - h, w, h, "buttonR", 1);
            myObjects.Add(buttonR);
            MyObject buttonLeft = new MyObject("PrzyciskL", width - (w * 2), height - h, w, h, "buttonLeft", 1);
            myObjects.Add(buttonLeft);
            MyObject buttonUp = new MyObject("PrzyciskW", 0, height - h, w, h, "buttonUp", 1);
            myObjects.Add(buttonUp);

            w = (400 * width) / 1920;
            h = (400 * height) / 1080;
            w = w / 3;
            h = h / 3;

            MyObject henR = new MyObject("kura", (width / 3), yGround - h, w, h, "henR", 0);
            henR.isDraw = false;
            myObjects.Add(henR);
            hen.rectangle = henR.rectangle;
            henY = hen.rectangle.Y;
            henYStart = henY;
            MyObject henL = new MyObject("kuraL", (width / 3), yGround - h, w, h, "henL", 0);
            henL.isDraw = false;
            myObjects.Add(henL);
            MyObject henRU = new MyObject("kuraPG", (width / 3), yGround - h, w, h, "henRU", 0);
            henRU.isDraw = false;
            myObjects.Add(henRU);
            MyObject henLU = new MyObject("kuraLG", (width / 3), yGround - h, w, h, "henLU", 0);
            henLU.isDraw = false;
            myObjects.Add(henLU);
            MyObject henL1 = new MyObject("kuraL_D_0", (width / 3), yGround - h, w, h, "henL1", 0);
            henL1.isDraw = false;
            myObjects.Add(henL1);
            MyObject henL2 = new MyObject("kuraL_D_1", (width / 3), yGround - h, w, h, "henL2", 0);
            henL2.isDraw = false;
            myObjects.Add(henL2);
            MyObject henR1 = new MyObject("kuraP_D_0", (width / 3), yGround - h, w, h, "henR1", 0);
            henR1.isDraw = false;
            myObjects.Add(henR1);
            MyObject henR2 = new MyObject("kuraP_D_1", (width / 3), yGround - h, w, h, "henR2", 0);
            henR2.isDraw = false;
            myObjects.Add(henR2);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            MyObject chickenCoop = new MyObject("kurnik", (width * 3) / 2 + (hen.rectangle.X - help.X), (yGround - h), w, h, "coop", 0);
            myObjects.Add(chickenCoop);

            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            MyObject tree = new MyObject("drzewo", width / 10, yGround - h, w, h, "tree", 0);
            myObjects.Add(tree);
            MyObject tree2 = new MyObject("drzewo", (helpX + chickenCoop.rectangle.X) / 2, yGround - h, w, h, "tree", 0);
            myObjects.Add(tree2);


            w = (295 * width) / 1920;
            h = (332 * height) / 1080;
            w = w / 5;
            h = h / 5;
            MyObject chicken = new MyObject("chicken", xChicken + w, yGround - h, w, h, "chicken1", 0);
            myObjects.Add(chicken);
            MyObject chicken2 = new MyObject("chicken", tree2.rectangle.X, yGround - h, w, h, "chicken2", 0);
            myObjects.Add(chicken2);
            MyObject smallchicken1 = new MyObject("chicken", width / 100, height / 100, w, h, "smallChicken1", 0);
            smallchicken1.isDraw = false;
            myObjects.Add(smallchicken1);
            MyObject smallchicken2 = new MyObject("chicken", smallchicken1.rectangle.X + (2 * w), smallchicken1.rectangle.Y, w, h, "smallChicken2", 0);
            smallchicken2.isDraw = false;
            myObjects.Add(smallchicken2);

            w = (400 * width) / 1920;
            h = (400 * height) / 1080;
            w = w / 3;
            h = h / 3;
            MyObject spiritTexture = new MyObject("kuraDuch", (width / 3), yGround - h, w, h, "spiritTexture", 0);
            spiritTexture.isDraw = false;
            myObjects.Add(spiritTexture);

            MyObject dimness = new MyObject("PoziomAkcja", 0, 0, background.rectangle.Width, background.rectangle.Height, "dimness", 0);
            dimness.isDraw = false;
            myObjects.Add(dimness);

            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            w = w / 2;
            h = h / 2;
            w = (2 * w) / 3;
            h = (h * 2) / 3;
            MyObject buttonAgain = new MyObject("przyciskJeszczeRaz", width / 2, (3 * height) / 5, w, h, "buttonAgain", 0);
            buttonAgain.isDraw = false;
            myObjects.Add(buttonAgain);
            MyObject buttonMenu = new MyObject("przyciskStop", buttonAgain.rectangle.X - (3 * buttonAgain.rectangle.Width) / 2, (3 * height) / 5, w, h, "buttonMenu", 1);
            buttonMenu.isDraw = false;
            myObjects.Add(buttonMenu);
            MyObject buttonNextRound = new MyObject("przyciskDalejPoziom", buttonAgain.rectangle.X, buttonAgain.rectangle.Y, w, h, "buttonNextRound", 1);
            buttonNextRound.isDraw = false;
            myObjects.Add(buttonNextRound);

            w = (900 * width) / 1920;
            h = (600 * height) / 1080;
            MyObject captionLose = new MyObject("przegrana", 0, height / 10, w, h, "captionLose", 1, false);
            captionLose.isDraw = false;
            captionLose.rectangle.X = (width / 2) - (w / 2);
            myObjects.Add(captionLose);
            MyObject captionEndRound = new MyObject("poziomUkonczony", (width / 2) - (w / 2), height / 10, w, h, "captionEndRound", 1, false);
            captionEndRound.isDraw = false;
            myObjects.Add(captionEndRound);

            w = (100 * width) / 1920;
            h = (100 * height) / 1080;
            MyObject goldenStar = new MyObject("gwiazdkaZlota", 0, 0, w, h, "goldenStar", 1, false);
            goldenStar.isDraw = false;
            myObjects.Add(goldenStar);

            MyObject greyStar = new MyObject("gwiazdkaSzara.svg", 0, 0, w, h, "greyStar", 1, false);
            greyStar.isDraw = false;
            myObjects.Add(greyStar);

            MyObject secondStar = new MyObject("gwiazdkaZlota", (width / 2) - (w / 2), height / 20, w, h, "secondStar", 1, false);
            secondStar.isDraw = false;
            myObjects.Add(secondStar);

            MyObject firstStar = new MyObject("gwiazdkaZlota", secondStar.rectangle.X - w, height / 20, w, h, "firstStar", 1, false);
            firstStar.isDraw = false;
            myObjects.Add(firstStar);

            MyObject thirdStar = new MyObject("gwiazdkaZlota", secondStar.rectangle.X + w, height / 20, w, h, "thirdStar", 1, false);
            thirdStar.isDraw = false;
            myObjects.Add(thirdStar);

            //MySong jumpVoice = new MySong("tap", "jumpVoice");
            //songs.Add(jumpVoice);

            MySong choir = new MySong("choir", "choir");
            songs.Add(choir);

            MySong mainSound = new MySong("bensound-hey", "mainSound");
            songs.Add(mainSound);

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            MySoundEffect jumpSound = new MySoundEffect("tap", "jumpSound");
            soundEffects.Add(jumpSound);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            
            if(isPlayChoirVoice)
            {
                MediaPlayer.Volume=1.0f;
            }
            else
            {
                MediaPlayer.Volume = 0.075f;
            }
            //MediaPlayer.Play(song);
        }

       

    public void Update(TouchCollection touchPlaces)
        {
            if (!isPlayMain)
            {
                MediaPlayer.Play(songs.Find(o => o.myName.Equals("mainSound")).song);
                isPlayMain = true;
            }

            if (moveCounter > maxMoveCounter)
            {
                moveCounter = 0;
            }

            if (canMove)
            {
                time++;
                timeSec = time / 60;
                timeLeft = roundTime - timeSec;
                if (timeLeft >= 0)
                {
                    findByName("digitOfTens").texture = findByName((timeLeft / 10).ToString()).texture;
                    findByName("digitOfUnity").texture = findByName((timeLeft % 10).ToString()).texture;
                }
                else
                {
                    canMove = false;
                    wpadlaDoWody = true;
                    findByName("buttonAgain").isDraw = true;
                    findByName("buttonMenu").isDraw = true;
                }

                foreach (TouchLocation touch in touchPlaces)
                {
                    Vector2 touchPosition = touch.Position;
                    if (touch.State == TouchLocationState.Moved)
                    {
                        if (isTouchButton(touchPosition, findByName("buttonR")) && findByName("buttonR").isDraw)
                        {
                            moveCounter++;
                            right = true;
                            if (help.X <= (width * 3) / 2)
                            {

                                help.X += dx;
                                foreach (Ground ground in groundList)
                                {
                                    ground.rectangle.X -= dx;
                                }
                                foreach (MyObject o in myObjects)
                                {
                                    if (!o.myName.Contains("button") && !o.myName.Contains("smallChicken") && o.isMove)
                                    {
                                        o.rectangle.X -= dx;
                                    }
                                }
                            }
                        }
                        if (isTouchButton(touchPosition, findByName("buttonLeft")) && findByName("buttonLeft").isDraw)
                        {
                            moveCounter++;
                            right = false;
                            if (help.X >= 3)
                            {
                                help.X -= dx;
                                foreach (Ground ground in groundList)
                                {
                                    ground.rectangle.X += dx;
                                }
                                foreach (MyObject o in myObjects)
                                {
                                    if (!o.myName.Contains("button") && !o.myName.Contains("smallChicken") && o.isMove)
                                    {
                                        o.rectangle.X += dx;
                                    }
                                }
                            }
                        }
                    }
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        if (isTouchButton(touchPosition, findByName("buttonUp")) && findByName("buttonUp").isDraw)
                        {
                            jump = true;
                            soundEffects.Find(o => o.myName.Equals("jumpSound")).soundEffect.CreateInstance().Volume = 1.0f;
                            soundEffects.Find(o => o.myName.Equals("jumpSound")).soundEffect.Play();
                        }
                        if (isTouchButton(touchPosition, findByName("buttonMenu")) && findByName("buttonMenu").isDraw)
                        {
                            myName = "Menu";
                            MediaPlayer.Stop();
                        }
                    }
                }
                if (right)
                {
                    int texNum;
                    if (!jump)
                    {
                        //texNum = myObjects.FindIndex(c => c.myName.Equals("henR"));
                        if (moveCounter < (maxMoveCounter / 2))
                        {
                            texNum = myObjects.FindIndex(c => c.myName.Equals("henR1"));
                        }
                        else
                        {
                            texNum = myObjects.FindIndex(c => c.myName.Equals("henR2"));
                        }
                        hen.texture = myObjects[texNum].texture;
                    }
                    else
                    {
                        texNum = myObjects.FindIndex(c => c.myName.Equals("henRU"));
                        hen.texture = myObjects[texNum].texture;
                    }
                }
                else
                {
                    int texNum;
                    if (!jump)
                    {
                        if (moveCounter < (maxMoveCounter / 2))
                        {
                            texNum = myObjects.FindIndex(c => c.myName.Equals("henL1"));
                        }
                        else
                        {
                            texNum = myObjects.FindIndex(c => c.myName.Equals("henL2"));
                        }
                        hen.texture = myObjects[texNum].texture;
                    }
                    else
                    {
                        texNum = myObjects.FindIndex(c => c.myName.Equals("henLU"));
                        hen.texture = myObjects[texNum].texture;
                    }
                }

                hen.rectangle.Y = hen.rectangle.Y - jumpCounter;
                if (jump)
                {
                    if (jumpUp)
                    {
                        jumpCounter++;
                        // if (jumpCounter > 2.1 * (height / 100))
                        if (jumpCounter > (hen.rectangle.Height / 5))
                        {
                            jumpUp = false;
                            jumpDown = true;
                        }
                    }
                    if (jumpDown)
                    {
                        jumpCounter--;
                        if (hen.rectangle.Y >= henY)
                        {
                            jumpDown = false;
                            jumpUp = true;
                            jump = false;
                            jumpCounter = 0;
                            hen.rectangle.Y = henY;
                        }
                    }
                }

                foreach (Ground g in groundList)
                {
                    if (!g.canTouch)
                    {
                        if (hen.rectangle.Y == henYStart)
                        {
                            if (hen.legsRectangle.X > g.rectangle.X && hen.legsRectangle.X < g.rectangle.X + g.rectangle.Width)
                            {
                                canMove = false;
                                wpadlaDoWody = true;
                                findByName("buttonAgain").isDraw = true;
                                findByName("buttonMenu").isDraw = true;
                            }
                        }
                    }
                }

            }
            else
            {
                findByName("dimness").rectangle.X = 0;
                findByName("dimness").rectangle.Y = 0;
                findByName("dimness").isDraw = true;
                if (wpadlaDoWody)
                {
                    hen.texture = findByName("spiritTexture").texture;
                    hen.rectangle.Y--;
                    findByName("captionLose").isDraw = true;
                    if (!isPlayChoirVoice)
                    {
                        MediaPlayer.Play(songs.Find(o => o.myName.Equals("choir")).song);
                        isPlayChoirVoice = true;
                    }
                }
                else
                {
                    findByName("buttonNextRound").isDraw = true;
                    findByName("captionEndRound").isDraw = true;

                    if (timeLeft >= roundTime - 15)
                    {
                        findByName("firstStar").texture = findByName("goldenStar").texture;
                        findByName("secondStar").texture = findByName("goldenStar").texture;
                        findByName("thirdStar").texture = findByName("goldenStar").texture;
                    }
                    else if (timeLeft >= roundTime - 20)
                    {
                        findByName("firstStar").texture = findByName("goldenStar").texture;
                        findByName("secondStar").texture = findByName("goldenStar").texture;
                        findByName("thirdStar").texture = findByName("greyStar").texture;
                    }
                    else
                    {
                        findByName("firstStar").texture = findByName("goldenStar").texture;
                        findByName("secondStar").texture = findByName("greyStar").texture;
                        findByName("thirdStar").texture = findByName("greyStar").texture;
                    }
                    findByName("firstStar").isDraw = true;
                    findByName("secondStar").isDraw = true;
                    findByName("thirdStar").isDraw = true;
                }
                findByName("buttonR").isDraw = false;
                findByName("buttonLeft").isDraw = false;
                findByName("buttonUp").isDraw = false;

                foreach (TouchLocation touch in touchPlaces)
                {
                    Vector2 touchPosition = touch.Position;
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        if (isTouchButton(touchPosition, findByName("buttonAgain")) && findByName("buttonAgain").isDraw)
                        {
                            myName = "ReloadRound1";
                            MediaPlayer.Stop();
                        }
                        if (isTouchButton(touchPosition, findByName("buttonMenu")) && findByName("buttonMenu").isDraw)
                        {
                            myName = "Menu";
                            MediaPlayer.Stop();
                        }
                        if (isTouchButton(touchPosition, findByName("buttonNextRound")) && findByName("buttonNextRound").isDraw)
                        {
                            myName = "Round2";
                            MediaPlayer.Stop();
                        }
                    }
                }
            }

            catchChicken();
            if (isEndOfRound())
            {
                findByName("buttonMenu").isDraw = true;
                canMove = false;
            }
        }

        private void catchChicken()
        {
            foreach (MyObject o in myObjects)
            {
                if (o.myName.Contains("chicken"))
                {
                    if (o.rectangle.Intersects(hen.rectangle))
                    {
                        if (o.isDraw)
                        {
                            o.isDraw = false;
                            chickenCounter++;
                            myObjects[myObjects.FindIndex(c => c.myName.Equals("smallChicken" + chickenCounter))].isDraw = true;
                            break;
                        }
                    }
                }
            }
        }

        private bool isEndOfRound()
        {
            if (chickenCounter >= 2)
            {
                if (help.X >= (width * 3) / 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool isTouchButton(Vector2 touchPosition, MyObject myObject)
        {
            if (touchPosition.X > myObject.rectangle.X && touchPosition.X < myObject.rectangle.X + myObject.rectangle.Width && touchPosition.Y > myObject.rectangle.Y && touchPosition.Y < myObject.rectangle.Y + myObject.rectangle.Height)
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
    }
}