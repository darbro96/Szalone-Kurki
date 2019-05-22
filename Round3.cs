using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Szalone_Kurki
{
    class Round3
    {
        public string myName = "Round3";
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
        private int maxHenX = 0;
        private int xCoop = 0;
        private int xMoon = 0;
        private int xMoon2 = 0;
        private int xButtonOn = 0;
        private int yButtonOn = 0;
        private int widthButtonOn = 0;
        private int xSign = 0;

        private int ghostMoveCounter = 0;
        private int ghostMoveCounterMax = 50;

        private int changeMove = 0;
        private bool changeMoveRight = true;

        private bool isButtonOn = false;

        public Background background;
        public Hen hen;

        public List<MyObject> myObjects = new List<MyObject>();
        public List<Ground> groundList = new List<Ground>();
        public List<Board> boardList = new List<Board>();
        public List<MySong> songs = new List<MySong>();
        public List<MySoundEffect> soundEffects = new List<MySoundEffect>();
        public List<Ghost> ghostList = new List<Ghost>();

        private Rectangle help = new Rectangle(0, 0, 0, 0);
        private int dx = 0;
        //Odpowiada za efekt dreptania
        private int moveCounter = 0;
        private int maxMoveCounter = 30;

        private bool fail = false;

        private int roundTime = 90;
        private int time = 0;
        private int timeSec = 0;
        private int timeLeft = 0;

        private bool isPlayMainSound = false;

        public Round3(int width, int height)
        {
            this.width = width;
            this.height = height;
            dx = (width * 3) / 1080;
            if (dx < 1)
            {
                dx = 1;
            }
            background = new Background("tloDuchy", 0, 0, this.width, this.height);
            createListGround(this.width, this.height);
            loadObjects(this.width, this.height);
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
            int sumWidth = 0;
            for (int i = 0; i < (width * 8) / 10; i += w)
            {
                groundList.Add(new Ground("trawaCiemna", xPos, height - h, w, h, 0, true));
                xPos += w;
                sumWidth += w;
            }
            xPos += w;
            w = (300 * width) / 1920;
            h = (50 * height) / 1080;
            Board board = new Board("deska", xPos, this.height, w, h, 0);
            boardList.Add(board);
            xMoon = board.rectangle.X;

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            w = w + (w / 4);
            xPos = board.rectangle.X + board.rectangle.Width;
            xPos += w;
            for (int i = 0; i < sumWidth / 2; i += w)
            {
                groundList.Add(new Ground("trawaCiemna", xPos, height - (4 * h), w, h, 0, false));
                xPos += w;
            }
            ghostList.Add(new Ghost(xPos - (2 * w), height - (2 * h), ((400 * width) / 1920) / 3, ((400 * height) / 1080) / 3));

            xPos += w;
            w = (300 * width) / 1920;
            h = (50 * height) / 1080;
            Board board2 = new Board("deska", xPos, this.height, w, h, 0);
            boardList.Add(board2);

            Board board3 = new Board("deska", xPos, this.height / 2, w, h, 0);
            boardList.Add(board3);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            w = w + (w / 4);
            xPos = board2.rectangle.X + board2.rectangle.Width;
            xPos += w;
            for (int i = 0; i < sumWidth; i += w)
            {
                groundList.Add(new Ground("trawaCiemna", xPos, height - (2 * h), w, h, 0, false));
                if (i < sumWidth / 3)
                {
                    groundList.Add(new Ground("trawaCiemna", xPos, height - (7 * h), w, h, 0, false));
                }
                xPos += w;
            }
            xMoon2 = xPos;
            xPos += w;
            w = (300 * width) / 1920;
            h = (50 * height) / 1080;
            Board board4 = new Board("deska", xPos, this.height, w, h, 0);
            boardList.Add(board4);

            Board board5 = new Board("deska", xPos, this.height / 2, w, h, 0);
            boardList.Add(board5);

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            w = w + (w / 4);
            xPos = board4.rectangle.X + board4.rectangle.Width;
            xPos += w;
            for (int i = 0; i < sumWidth; i += w)
            {
                groundList.Add(new Ground("trawaCiemna", xPos, height - h, w, h, 0, false));
                if (i < sumWidth / 3)
                {
                    groundList.Add(new Ground("trawaCiemna", xPos, height - (7 * h), w, h, 0, false));
                }
                if (xButtonOn == 0 && i > sumWidth / 6)
                {
                    xButtonOn = xPos;
                    yButtonOn = height - (7 * h);
                    widthButtonOn = w;
                }
                xSign = xPos - w;
                xPos += w;
            }

            w = (300 * width) / 1920;
            h = (50 * height) / 1080;
            w = w * 2;
            Ground longBorder = new Ground("deska", xPos, yGround, w, h, 0, true);
            longBorder.myName = "longBorder";
            //longBorder.canTouch = false;
            groundList.Add(longBorder);
            xPos += w;

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            w = w / 3;
            h = h / 3;
            w = w + (w / 4);
            xPos = longBorder.rectangle.X + longBorder.rectangle.Width;
            for (int i = 0; i < sumWidth; i += w)
            {
                groundList.Add(new Ground("trawaCiemna", xPos, height - h, w, h, 0, false));
                if (xCoop == 0 && i >= sumWidth / 2)
                {
                    xCoop = xPos;
                }
                xPos += w;
            }
        }

        private void loadObjects(int width, int height)
        {
            int w = (300 * width) / 1920;
            int h = (300 * height) / 1080;

            MyObject moon = new MyObject("ksiezyc", xMoon, -h / 2, w, h, "moon", 0);
            myObjects.Add(moon);

            MyObject moon2 = new MyObject("ksiezyc", xMoon2, -h / 2, w, h, "moon", 0);
            myObjects.Add(moon2);

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

            w = (600 * width) / 1920;
            h = (600 * height) / 1080;
            MyObject chickenCoop = new MyObject("piramida", xCoop, (yGround - h), w, h, "coop", 0);
            myObjects.Add(chickenCoop);

            maxHenX = chickenCoop.rectangle.X - (chickenCoop.rectangle.Width) / 2;

            w = (300 * width) / 1920;
            h = (300 * height) / 1080;
            MyObject buttonOn = new MyObject("przyciskDuchyON", xButtonOn, yButtonOn - (yButtonOn / 10), widthButtonOn, widthButtonOn, "switchOn", 1);
            buttonOn.isDraw = false;
            myObjects.Add(buttonOn);
            MyObject buttonOff = new MyObject("przyciskDuchyOFF", xButtonOn, yButtonOn - (yButtonOn / 10), widthButtonOn, widthButtonOn, "switchOff", 1);
            buttonOff.isDraw = true;
            myObjects.Add(buttonOff);

            MyObject sign = new MyObject("znak", xSign, yGround - h, w, h, "sign", 0);
            myObjects.Add(sign);

            MySong mainSound = new MySong("bensound-psychedelic", "mainSound");
            songs.Add(mainSound);

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            w = (400 * width) / 1920;
            h = (400 * height) / 1080;
            w = w / 3;
            h = h / 3;
            MyObject ghostTex1 = new MyObject("duch", 0, 0, w, h, "ghostTex1", 0);
            ghostTex1.isDraw = false;
            myObjects.Add(ghostTex1);

            MyObject ghostTex2 = new MyObject("duch2", 0, 0, w, h, "ghostTex2", 0);
            ghostTex2.isDraw = false;
            myObjects.Add(ghostTex2);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.075f;
            //MediaPlayer.Play(song);
        }

        public void Update(TouchCollection touchPlaces)
        {
            if (!isPlayMainSound)
            {
                MediaPlayer.Play(songs.Find(o => o.myName.Equals("mainSound")).song);
                isPlayMainSound = true;
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
                //else
                //{
                //    canMove = false;
                //    fail = true;
                //    findByName("buttonAgain").isDraw = true;
                //    findByName("buttonMenu").isDraw = true;
                //}

                if (!isButtonOn)
                {
                    findByName("switchOff").isDraw = true;
                    findByName("switchOn").isDraw = false;
                    groundList.Find(g => g.myName.Equals("longBorder")).isDraw = false;
                    groundList.Find(g => g.myName.Equals("longBorder")).canTouch = false;
                }
                else
                {
                    findByName("switchOff").isDraw = false;
                    findByName("switchOn").isDraw = true;
                    groundList.Find(g => g.myName.Equals("longBorder")).isDraw = true;
                    groundList.Find(g => g.myName.Equals("longBorder")).canTouch = true;
                }

                foreach (TouchLocation touch in touchPlaces)
                {
                    Vector2 touchPosition = touch.Position;
                    if (touch.State == TouchLocationState.Moved)
                    {
                        if (isTouchButton(touchPosition, findByName("buttonR")))
                        {
                            moveCounter++;
                            right = true;
                            if (help.X <= maxHenX)
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
                                foreach (Board b in boardList)
                                {
                                    b.rectangle.X -= dx;
                                }
                                foreach (Ghost ghost in ghostList)
                                {
                                    ghost.rectangle.X -= dx;
                                }
                            }
                        }
                        if (isTouchButton(touchPosition, findByName("buttonLeft")))
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
                                    foreach (Board b in boardList)
                                    {
                                        b.rectangle.X += dx;
                                    }
                                    foreach (Ghost ghost in ghostList)
                                    {
                                        ghost.rectangle.X += dx;
                                    }
                                }
                            }
                        }
                    }
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        if (isTouchButton(touchPosition, findByName("buttonUp")))
                        {
                            jump = true;
                        }
                        if (isTouchButton(touchPosition, findByName("buttonMenu")))
                        {
                            myName = "Menu";
                        }
                    }
                }
                hen.position(hen.rectangle.Y - jumpCounter);
                moveBoard();
                jumping();
                if (right)
                {
                    int texNum;
                    if (!jump)
                    {
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
                foreach (Ground g in groundList)
                {
                    if (!g.canTouch)
                    {
                        if (hen.legsRectangle.X > g.rectangle.X && hen.legsRectangle.X < g.rectangle.X + g.rectangle.Width)
                        {
                            canMove = false;
                            fail = true;
                            findByName("buttonAgain").isDraw = true;
                            findByName("buttonMenu").isDraw = true;
                        }
                    }
                }
            }

            ghostMoveCounter++;
            if (ghostMoveCounter > ghostMoveCounterMax)
            {
                ghostMoveCounter = 0;
            }

            if (changeMoveRight)
            {
                if (changeMove < this.height / 100)
                {
                    if (ghostMoveCounter % 2 == 0)
                        changeMove++;
                }
                else
                {
                    changeMoveRight = false;
                }
            }
            else
            {
                if (changeMove > -this.height / 100)
                {
                    if (ghostMoveCounter % 2 == 0)
                        changeMove--;
                }
                else
                {
                    changeMoveRight = true;
                }
            }

            foreach (Ghost ghost in ghostList)
            {
                ghost.rectangle.X += changeMove;
                if (ghost.rectangle.Y > -ghost.rectangle.Height)
                {
                    ghost.rectangle.Y -= 2;
                }
                else
                {
                    ghost.rectangle.Y = this.height + ghost.rectangle.Height;
                }
                if (ghostMoveCounter < ghostMoveCounterMax / 2)
                {
                    //ghost.texture = findByName("ghostTex1").texture;
                    ghost.texture = ghost.texture1;
                }
                else
                {
                    //ghost.texture = findByName("ghostTex2").texture;
                    ghost.texture = ghost.texture2;
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

        private void moveBoard()
        {
            foreach (Board b in boardList)
            {
                if (b.rectangle.Y > -b.rectangle.Height)
                {
                    b.rectangle.Y -= (height / 500);
                    if ((height / 500) < 1)
                    {
                        b.rectangle.Y--;
                    }
                }
                else
                {
                    b.rectangle.Y = this.height;
                }
                if (b.rectangle.Intersects(hen.legsRectangle))
                {
                    hen.rectangle.Y = b.rectangle.Y - hen.rectangle.Height;
                }
            }
        }

        private void jumping()
        {
            if (jump)
            {
                if (jumpUp)
                {
                    jumpCounter++;
                    if (jumpCounter > 2.1 * (height / 100))
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
                    foreach (Board b in boardList)
                    {
                        if (b.rectangle.Intersects(hen.legsRectangle))
                        {
                            henY = b.rectangle.Y - hen.rectangle.Height;
                            jumpDown = false;
                            jumpUp = true;
                            jump = false;
                            jumpCounter = 0;
                            hen.rectangle.Y = henY;
                            break;
                        }
                    }
                    if (hen.legsRectangle.Intersects(findByName("switchOff").rectangle))
                    {
                        isButtonOn = true;
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
                    foreach (Board b in boardList)
                    {
                        if (b.rectangle.Intersects(hen.legsRectangle))
                        {
                            toDown = false;
                            break;
                        }
                    }
                    if (hen.legsRectangle.Intersects(findByName("switchOn").rectangle))
                    {
                        henY = findByName("switchOn").rectangle.Y - hen.rectangle.Height;
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
        }
    }
}