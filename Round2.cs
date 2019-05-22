using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class Round2 : RoundInterface
    {
        public bool isDraw = false;
        public string myName = "Round2";

        public int width = 0;
        public int height = 0;

        public List<Ground> groundList = new List<Ground>();
        public List<MyObject> myObjects = new List<MyObject>();
        public List<MySong> songs = new List<MySong>();
        public List<MySoundEffect> soundEffects = new List<MySoundEffect>();

        public Background background;

        public Hen hen;

        private int yGround = 0;

        private Rectangle help = new Rectangle(0, 0, 0, 0);

        private bool jump = false;
        private bool jumpUp = true;
        private bool jumpDown = false;
        private int jumpCounter = 0;
        private int henY = 0;
        private int henYStart = 0;
        private bool right = true;
        private bool podnoszenie = false;
        int chickenCounter = 0;

        private int xChicken1 = 0;
        private int yChicken1 = 0;
        private int xChicken2 = 0;
        private int yChicken2 = 0;

        private int iglyIndeks = 0;
        private int podnoszenieLicznik = 0;

        private bool canMove = true;

        private int moveCounter = 0;
        private int maxMoveCounter = 30;

        private int dx = 0;

        private bool dotknelaIgly = false;

        private int roundTime = 60;
        private int time = 0;
        private int timeSec = 0;
        private int timeLeft = 0;

        private bool isSoundPlay = false;
        private bool isPlayChoirVoice = false;

        public Round2(int width, int height)
        {
            this.width = width;
            this.height = height;
            createListGround(this.width, this.height);
            background = new Background("bialoNiebieskieNiebo", 0, 0, this.width, this.height);
            positionElements(this.width, this.height);
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

            for (int i = 0; i < 3; i++)
            {
                Ground ground = new Ground("igly0", xPos, height - h, w, h, 0, true);
                ground.canTouch = false;
                groundList.Add(ground);
                xPos += w;
            }
            int xPos2 = xPos;
            xChicken1 = xPos;
            yChicken1 = height - h;
            for (int i = 0; i < width * 2; i += w)
            {
                groundList.Add(new Ground("trawa", xPos, height - h, w, h, 0, true));
                xPos += w;
            }
            xChicken2 = xPos2;
            yChicken2 = height - (5 * h);
            //gorne
            for (int i = 0; i < (width * 3) / 4; i += w)
            {
                groundList.Add(new Ground("trawa", xPos2, height - (5 * h), w, h, 0, false));
                xPos2 += w;
            }
            // do wskoczenia na górę
            for (int i = 0; i < 10; i += w)
            {
                groundList.Add(new Ground("trawa", xPos2, height - (4 * h), w, h, 0, false));
                xPos2 += w;
            }
            for (int i = 0; i < 10; i += w)
            {
                groundList.Add(new Ground("trawa", xPos2, height - ((7 * h) / 2), w, h, 0, false));
                xPos2 += w;
            }
        }

        private void positionElements(int width, int height)
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

            MyObject tree = new MyObject();
            tree.myName = "tree";
            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            h = (600 * height) / 1080;
            w = (w * 2) / 3;
            h = (h * 2) / 3;
            tree.width = w;
            tree.height = h;
            tree.rectangle = new Rectangle(width / 10, yGround - h, w, h);
            tree.textureFile = "drzewo";
            tree.levelDraw = 0;
            myObjects.Add(tree);
            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            MyObject cloud = new MyObject("chmura", width / 20, height / 20, w, h, "cloud", 1);
            myObjects.Add(cloud);
            MyObject cloud2 = new MyObject("chmura", width * 2, height / 21, w, h, "cloud2", 1);
            myObjects.Add(cloud2);
            MyObject cloud3 = new MyObject("chmura", width / 2, height / 50, w, h, "cloud3", 1);
            myObjects.Add(cloud3);
            w = (w * 3) / 4;
            h = (h * 3) / 4;

            MyObject buttonR = new MyObject("PrzyciskPr", width - w, height - h, w, h, "buttonR", 1);
            myObjects.Add(buttonR);
            MyObject buttonLeft = new MyObject("PrzyciskL", width - (w * 2), height - h, w, h, "buttonLeft", 1);
            myObjects.Add(buttonLeft);
            MyObject buttonUp = new MyObject("PrzyciskW", 0, height - h, w, h, "buttonUp", 1);
            myObjects.Add(buttonUp);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            MyObject butRed = new MyObject("Cg", (width * 4) / 5, (yGround - h) + (h / 10), w, h, "butRed", 0);
            myObjects.Add(butRed);

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
            MyObject spiritTexture = new MyObject("kuraDuch", (width / 3), yGround - h, w, h, "spiritTexture", 0);
            spiritTexture.isDraw = false;
            myObjects.Add(spiritTexture);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            MyObject signallerRed = new MyObject("sygnalizacjaCzerwona", (width * 4) / 5, (yGround - h), w, h, "signallerRed", 0);
            myObjects.Add(signallerRed);
            MyObject signallerYellow = new MyObject("sygnalizacjaZolta", (width * 4) / 5, (yGround - h), w, h, "signallerYellow", 0);
            signallerYellow.isDraw = false;
            myObjects.Add(signallerYellow);
            MyObject signallerGreen = new MyObject("sygnalizacjaZielona", (width * 4) / 5, (yGround - h), w, h, "signallerGreen", 0);
            signallerGreen.isDraw = false;
            myObjects.Add(signallerGreen);

            MyObject igly1Tex = new MyObject("igly1", 0, 0, 0, 0, "igly1", 1);
            igly1Tex.isDraw = false;
            myObjects.Add(igly1Tex);
            MyObject igly2Tex = new MyObject("igly2", 0, 0, 0, 0, "igly2", 1);
            igly2Tex.isDraw = false;
            myObjects.Add(igly2Tex);
            MyObject igly0Tex = new MyObject("igly0", 0, 0, 0, 0, "igly0", 1);
            igly0Tex.isDraw = false;
            myObjects.Add(igly0Tex);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            MyObject chickenCoop = new MyObject("kurnik", (width * 2) + (hen.rectangle.X - help.X), (yGround - h), w, h, "coop", 0);
            myObjects.Add(chickenCoop);

            w = (295 * width) / 1920;
            h = (332 * height) / 1080;
            w = w / 5;
            h = h / 5;
            MyObject chicken = new MyObject("chicken", xChicken1, yGround - h, w, h, "chicken1", 0);
            myObjects.Add(chicken);
            MyObject chicken2 = new MyObject("chicken", xChicken2, yChicken2 - h, w, h, "chicken2", 0);
            myObjects.Add(chicken2);
            MyObject smallchicken1 = new MyObject("chicken", width / 100, height / 100, w, h, "smallChicken1", 0);
            smallchicken1.isDraw = false;
            myObjects.Add(smallchicken1);
            MyObject smallchicken2 = new MyObject("chicken", smallchicken1.rectangle.X + (2 * w), smallchicken1.rectangle.Y, w, h, "smallChicken2", 0);
            smallchicken2.isDraw = false;
            myObjects.Add(smallchicken2);
            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            MyObject mountain = new MyObject("gora", chickenCoop.rectangle.X + chickenCoop.rectangle.Width, yGround - h, w, h, "mountain", 0);
            myObjects.Add(mountain);

            MyObject dimness = new MyObject("PoziomAkcja", 0, 0, background.rectangle.Width, background.rectangle.Height, "dimness", 0);
            dimness.isDraw = false;
            myObjects.Add(dimness);

            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            w = w / 2;
            h = h / 2;
            w = (2 * w) / 3;
            h = (h * 2) / 3;
            MyObject buttonAgain = new MyObject("przyciskJeszczeRaz", width / 2, (3 * height) / 5, w, h, "buttonAgain", 1);
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

            MySong mainSound = new MySong("bensound-clearday", "mainSound");
            songs.Add(mainSound);

            MySong choir = new MySong("choir", "choir");
            songs.Add(choir);

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            MySoundEffect jumpSound = new MySoundEffect("tap", "jumpSound");
            soundEffects.Add(jumpSound);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.075f;
            //MediaPlayer.Play(song);
        }

        public void Update(TouchCollection touchPlaces)
        {
            if (!isSoundPlay)
            {
                MediaPlayer.Play(songs.Find(o => o.myName.Equals("mainSound")).song);
                isSoundPlay = true;
            }

            if (moveCounter > maxMoveCounter)
            {
                moveCounter = 0;
            }

            int num = myObjects.FindIndex(c => c.myName.Equals("cloud"));
            myObjects[num].rectangle.X++;
            num = myObjects.FindIndex(c => c.myName.Equals("cloud2"));
            myObjects[num].rectangle.X--;
            num = myObjects.FindIndex(c => c.myName.Equals("cloud3"));
            myObjects[num].rectangle.X++;
            num = myObjects.FindIndex(c => c.myName.Equals("buttonR"));
            int numUp = myObjects.FindIndex(c => c.myName.Equals("buttonUp"));
            int numL = myObjects.FindIndex(c => c.myName.Equals("buttonLeft"));
            int buttonNo = myObjects.FindIndex(c => c.myName.Equals("butRed"));
            int numAgain = myObjects.FindIndex(c => c.myName.Equals("buttonAgain"));
            int numMenu = myObjects.FindIndex(c => c.myName.Equals("buttonMenu"));
            int numNextRound = myObjects.FindIndex(c => c.myName.Equals("buttonNextRound"));
            int numCaptionLose = myObjects.FindIndex(c => c.myName.Equals("captionLose"));
            int numCaptionEndRound = myObjects.FindIndex(c => c.myName.Equals("captionEndRound"));

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
                    dotknelaIgly = true;
                    findByName("buttonAgain").isDraw = true;
                    findByName("buttonMenu").isDraw = true;
                }

                foreach (TouchLocation touch in touchPlaces)
                {
                    Vector2 touchPosition = touch.Position;
                    if (touch.State == TouchLocationState.Moved)
                    {
                        if (touchPosition.X > myObjects[num].rectangle.X && touchPosition.X < myObjects[num].rectangle.X + myObjects[num].rectangle.Width && touchPosition.Y > myObjects[num].rectangle.Y && touchPosition.Y < myObjects[num].rectangle.Y + myObjects[num].rectangle.Height)
                        {
                            moveCounter++;
                            right = true;
                            if (help.X <= (width * 2))
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
                        if (touchPosition.X > myObjects[numL].rectangle.X && touchPosition.X < myObjects[numL].rectangle.X + myObjects[numL].rectangle.Width && touchPosition.Y > myObjects[numL].rectangle.Y && touchPosition.Y < myObjects[numL].rectangle.Y + myObjects[numL].rectangle.Height)
                        {
                            moveCounter++;
                            right = false;
                            if (help.X >= 3)
                            {
                                bool stop = false;
                                foreach (Ground g in groundList)
                                {
                                    if (g.rectangle.Intersects(hen.rectangle) && (g.rectangle.Y + g.rectangle.Height) > hen.rectangle.Y)
                                    {
                                        foreach (Ground gr in groundList)
                                        {
                                            if (gr.rectangle.Intersects(hen.rectangle))
                                            {
                                                if (gr.rectangle.X != g.rectangle.X && gr.rectangle.Y != g.rectangle.Y)
                                                {
                                                    stop = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!stop)
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
                    }
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        if (touchPosition.X > myObjects[numUp].rectangle.X && touchPosition.X < myObjects[numUp].rectangle.X + myObjects[numUp].rectangle.Width && touchPosition.Y > myObjects[numUp].rectangle.Y && touchPosition.Y < myObjects[numUp].rectangle.Y + myObjects[numUp].rectangle.Height)
                        {
                            jump = true;
                            soundEffects.Find(o => o.myName.Equals("jumpSound")).soundEffect.CreateInstance().Volume = 1.0f;
                            soundEffects.Find(o => o.myName.Equals("jumpSound")).soundEffect.Play();
                        }
                        if (isTouchButton(touchPosition, myObjects[numMenu]) && myObjects[numMenu].isDraw)
                        {
                            myName = "Menu";
                        }
                    }
                }
                // hen.rectangle.Y = hen.rectangle.Y - jumpCounter;
                hen.position(hen.rectangle.Y - jumpCounter);
                if (jump)
                {
                    if (jumpUp)
                    {
                        jumpCounter++;
                        if (jumpCounter > 2.1 * (height / 100))
                        //if (jumpCounter > (hen.rectangle.Height))
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
                        if (hen.legsRectangle.Intersects(myObjects[buttonNo].rectangle))
                        {
                            if (!podnoszenie)
                                podnoszenie = true;
                            henY = myObjects[buttonNo].rectangle.Y - hen.rectangle.Height;

                        }
                        foreach (Ground g in groundList)
                        {
                            if (!g.isFloor)
                            {
                                //zmieniono
                                if (hen.legsRectangle.Intersects(g.rectangle))
                                {
                                    henY = g.rectangle.Y - hen.rectangle.Height;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (henY != henYStart)
                    {
                        bool toDown = true;
                        foreach (Ground g in groundList)
                        {
                            if (!g.isFloor)
                            {
                                //zmieniono
                                if (g.rectangle.Intersects(hen.legsRectangle))
                                {
                                    toDown = false;
                                    break;
                                }
                            }
                        }
                        if (hen.legsRectangle.Intersects(myObjects[buttonNo].rectangle))
                        {
                            toDown = false;
                        }
                        if (toDown)
                        {
                            if (henY != henYStart)
                            {
                                int dy = (height * 9) / 720;
                                if (dy < 1)
                                {
                                    dy = 1;
                                }
                                henY += dy;
                                if (henY == henYStart)
                                {
                                    toDown = false;
                                }
                                else if (henY > henYStart)
                                {
                                    henY = henYStart;
                                    toDown = false;
                                }
                                hen.rectangle.Y = henY;
                            }
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
                        //texNum = myObjects.FindIndex(c => c.myName.Equals("henL"));
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
                if (podnoszenie)
                {
                    podnoszenieLicznik++;
                    if (podnoszenieLicznik % 20 == 0)
                    {
                        iglyIndeks++;
                        foreach (Ground g in groundList)
                        {
                            if (g.textureFile.Contains("igly"))
                            {
                                g.texture = myObjects[myObjects.FindIndex(c => c.myName.Equals("igly" + iglyIndeks))].texture;
                                if (iglyIndeks == 2)
                                {
                                    g.canTouch = true;
                                }
                            }
                        }
                        if (myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerRed"))].isDraw)
                        {
                            myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerRed"))].isDraw = false;
                            myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerYellow"))].isDraw = true;

                        }
                        else if (myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerYellow"))].isDraw)
                        {
                            myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerYellow"))].isDraw = false;
                            myObjects[myObjects.FindIndex(c => c.myName.Equals("signallerGreen"))].isDraw = true;

                        }
                    }
                    if (iglyIndeks == 2)
                    {
                        podnoszenie = false;
                        podnoszenieLicznik = 0;
                        iglyIndeks = 0;
                    }
                }
                foreach (Ground g in groundList)
                {
                    if (!g.canTouch)
                    {
                        if (hen.legsRectangle.X > g.rectangle.X && hen.legsRectangle.X < g.rectangle.X + g.rectangle.Width)
                        {
                            canMove = false;
                            dotknelaIgly = true;
                            myObjects[numAgain].isDraw = true;
                            myObjects[numMenu].isDraw = true;
                        }
                    }
                }
            }
            else
            {
                findByName("dimness").rectangle.X = 0;
                findByName("dimness").rectangle.Y = 0;
                findByName("dimness").isDraw = true;
                if (dotknelaIgly)
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

                    if (timeLeft >= roundTime - 20)
                    {
                        findByName("firstStar").texture = findByName("goldenStar").texture;
                        findByName("secondStar").texture = findByName("goldenStar").texture;
                        findByName("thirdStar").texture = findByName("goldenStar").texture;
                    }
                    else if (timeLeft >= roundTime - 25)
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
                findByName("buttonMenu").isDraw = true;

                foreach (TouchLocation touch in touchPlaces)
                {
                    Vector2 touchPosition = touch.Position;
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        if (isTouchButton(touchPosition, myObjects[numAgain]) && myObjects[numAgain].isDraw)
                        {
                            myName = "ReloadRound2";
                        }
                        if (isTouchButton(touchPosition, myObjects[numMenu]) && myObjects[numMenu].isDraw)
                        {
                            myName = "Menu";
                        }
                        if (isTouchButton(touchPosition, myObjects[numNextRound]) && myObjects[numNextRound].isDraw)
                        {
                            myName = "Menu";
                        }
                    }
                }

            }
            catchChicken();
            isEndOfRound();
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

        private void isEndOfRound()
        {
            if (chickenCounter >= 2)
            {
                if (help.X >= (width * 2))
                {
                    canMove = false;
                }
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