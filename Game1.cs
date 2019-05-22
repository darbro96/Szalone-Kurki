using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Szalone_Kurki
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int width;
        int height;

        StartView startView;
        Round1 round1;
        Round2 round2;
        Round3 round3;
        Menu menu;

        Texture2D testowy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            startView = new StartView(width, height);
            startView.background.texture = Content.Load<Texture2D>("img/" + startView.background.textureFile);
            foreach (MyObject o in startView.myObjects)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }

            menu = new Menu(width, height);
            menu.background.texture = Content.Load<Texture2D>("img/" + menu.background.textureFile);
            foreach (MyObject o in menu.myObjects)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }
            foreach (RoundIcon o in menu.icons)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }
            foreach(MySong ms in menu.songs)
            {
                ms.song= Content.Load<Song>("sounds/"+ms.fileName);
            }

            loadTexturesRound1();

            loadTexturesRound2();

            loadTexturesRound3();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here
            var timer = (int)gameTime.ElapsedGameTime.TotalSeconds;

            TouchCollection touchPlaces = TouchPanel.GetState();

            if (startView.isDraw)
            {
                startView.Update(touchPlaces);
                if (!startView.myName.Equals("startView"))
                {
                    setView(startView.myName);
                    startView.myName = "startView";
                }
            }
            if (menu.isDraw)
            {
                menu.Update(touchPlaces,gameTime);
                if (!menu.myName.Equals("Menu"))
                {
                    setView(menu.myName);
                    menu.myName = "Menu";
                }
            }
            if (round1.isDraw)
            {
                round1.Update(touchPlaces);
                if (!round1.myName.Equals("Round1"))
                {
                    setView(round1.myName);
                    round1.myName = "Round1";
                }
            }
            if (round2.isDraw)
            {
                round2.Update(touchPlaces);
                if (!round2.myName.Equals("Round2"))
                {
                    setView(round2.myName);
                    round2.myName = "Round2";
                }
            }
            if (round3.isDraw)
            {
                round3.Update(touchPlaces);
                if (!round3.myName.Equals("Round3"))
                {
                    setView(round3.myName);
                    round3.myName = "Round3";
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            drawStartView();
            drawMenu();
            drawRound1();
            drawRound2();
            drawRound3();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void setView(string view)
        {
            switch (view)
            {
                case "startView":
                    round1.isDraw = false;
                    round2.isDraw = false;
                    menu.isDraw = false;
                    round3.isDraw = false;
                    startView.isDraw = true;
                    break;
                case "Round1":
                    loadTexturesRound1();
                    startView.isDraw = false;
                    round2.isDraw = false;
                    menu.isDraw = false;
                    round3.isDraw = false;
                    round1.isDraw = true;
                    break;
                case "ReloadRound1":
                    round1.isDraw = false;
                    loadTexturesRound1();
                    setView("Round1");
                    round1.isDraw = true;
                    break;
                case "Round2":
                    loadTexturesRound2();
                    startView.isDraw = false;
                    round1.isDraw = false;
                    menu.isDraw = false;
                    round2.isDraw = true;
                    round3.isDraw = false;
                    break;
                case "ReloadRound2":
                    round2.isDraw = false;
                    loadTexturesRound2();
                    setView("Round2");
                    round2.isDraw = true;
                    break;
                case "Menu":
                    round1.isDraw = false;
                    round2.isDraw = false;
                    startView.isDraw = false;
                    round3.isDraw = false;
                    menu.isDraw = true;
                    break;
                case "Round3":
                    loadTexturesRound3();
                    round1.isDraw = false;
                    round2.isDraw = false;
                    startView.isDraw = false;
                    menu.isDraw = false;
                    round3.isDraw = true;
                    break;
                case "ReloadRound3":
                    round3.isDraw = false;
                    loadTexturesRound3();
                    setView("Round3");
                    round3.isDraw = true;
                    break;
                default:
                    round1.isDraw = false;
                    round2.isDraw = false;
                    round3.isDraw = false;
                    menu.isDraw = false;
                    startView.isDraw = true;
                    break;
            }
        }

        private void drawStartView()
        {
            if (startView.isDraw)
            {
                spriteBatch.Draw(startView.background.texture, startView.background.rectangle, Color.White);
                foreach (MyObject o in startView.myObjects)
                {
                    if (o.isDraw)
                    {
                        spriteBatch.Draw(o.texture, o.rectangle, null, o.color, 0, Vector2.Zero, SpriteEffects.None, o.levelDraw);
                    }
                }
            }
        }

        private void drawRound1()
        {
            if (round1.isDraw)
            {
                spriteBatch.Draw(round1.background.texture, round1.background.rectangle, Color.White);


                foreach (MyObject o in round1.myObjects)
                {
                    if (o.isDraw)
                    {
                        spriteBatch.Draw(o.texture, o.rectangle, null, o.color, 0, Vector2.Zero, SpriteEffects.None, o.levelDraw);
                    }
                }
                foreach (Ground g in round1.groundList)
                {
                    spriteBatch.Draw(g.texture, g.rectangle, null, g.color, 0, Vector2.Zero, SpriteEffects.None, g.levelDraw);
                }
                spriteBatch.Draw(round1.hen.texture, round1.hen.rectangle, Color.White);
            }
        }

        private void drawRound2()
        {
            if (round2.isDraw)
            {
                spriteBatch.Draw(round2.background.texture, round2.background.rectangle, round2.background.color);


                foreach (Ground ground in round2.groundList)
                {
                    spriteBatch.Draw(ground.texture, ground.rectangle, null, ground.color, 0, Vector2.Zero, SpriteEffects.None, ground.levelDraw);
                }
                foreach (MyObject o in round2.myObjects)
                {
                    if (o.isDraw)
                    {
                        spriteBatch.Draw(o.texture, o.rectangle, null, o.color, 0, Vector2.Zero, SpriteEffects.None, o.levelDraw);
                    }
                }
                spriteBatch.Draw(round2.hen.texture, round2.hen.rectangle, null, round1.hen.color, 0, Vector2.Zero, SpriteEffects.None, 1);
            }

        }

        private void drawRound3()
        {
            if (round3.isDraw)
            {
                spriteBatch.Draw(round3.background.texture, round3.background.rectangle, round3.background.color);


                foreach (Ground ground in round3.groundList)
                {
                    if(ground.isDraw)
                    {
                        spriteBatch.Draw(ground.texture, ground.rectangle, null, ground.color, 0, Vector2.Zero, SpriteEffects.None, ground.levelDraw);
                    }
                }
                foreach(Board b in round3.boardList)
                {
                    spriteBatch.Draw(b.texture, b.rectangle, null, b.color, 0, Vector2.Zero, SpriteEffects.None, b.levelDraw);
                }
                foreach (MyObject o in round3.myObjects)
                {
                    if (o.isDraw)
                    {
                        spriteBatch.Draw(o.texture, o.rectangle, null, o.color, 0, Vector2.Zero, SpriteEffects.None, o.levelDraw);
                    }
                }
                foreach(Ghost ghost in round3.ghostList)
                {
                    spriteBatch.Draw(ghost.texture, ghost.rectangle, null, ghost.color, 0, Vector2.Zero, SpriteEffects.None, ghost.levelDraw);
                }
                spriteBatch.Draw(round3.hen.texture, round3.hen.rectangle, null, round3.hen.color, 0, Vector2.Zero, SpriteEffects.None, 1);
            }

        }

        private void drawMenu()
        {
            if (menu.isDraw)
            {
                spriteBatch.Draw(menu.background.texture, menu.background.rectangle, menu.background.color);

                foreach (RoundIcon ri in menu.icons)
                {
                    spriteBatch.Draw(ri.texture, ri.iconRectangle, ri.color);
                }
            }
        }

        private void loadTexturesRound1()
        {
            round1 = new Round1(width, height);
            round1.background.texture = Content.Load<Texture2D>("img/" + round1.background.textureFile);
            round1.hen.texture = Content.Load<Texture2D>("img/" + round1.hen.textureFile);
            foreach (MyObject o in round1.myObjects)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }
            foreach (Ground g in round1.groundList)
            {
                g.texture = Content.Load<Texture2D>("img/" + g.textureFile);
            }
            foreach (MySong ms in round1.songs)
            {
                ms.song = Content.Load<Song>("sounds/" + ms.fileName);
            }
            foreach (MySoundEffect ms in round1.soundEffects)
            {
                ms.soundEffect = Content.Load<SoundEffect>("sounds/" + ms.fileName);
            }
        }

        private void loadTexturesRound2()
        {
            round2 = new Round2(width, height);
            round2.background.texture = Content.Load<Texture2D>("img/" + round2.background.textureFile);
            round2.hen.texture = Content.Load<Texture2D>("img/" + round2.hen.textureFile);
            testowy = Content.Load<Texture2D>("img/tloDuchy");
            foreach (Ground ground in round2.groundList)
            {
                ground.texture = Content.Load<Texture2D>("img/" + ground.textureFile);
            }
            foreach (MyObject o in round2.myObjects)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }
            foreach (MySong ms in round2.songs)
            {
                ms.song = Content.Load<Song>("sounds/" + ms.fileName);
            }
            foreach (MySoundEffect ms in round2.soundEffects)
            {
                ms.soundEffect = Content.Load<SoundEffect>("sounds/" + ms.fileName);
            }
        }

        private void loadTexturesRound3()
        {
            round3 = new Round3(width, height);
            round3.background.texture = Content.Load<Texture2D>("img/" + round3.background.textureFile);
            round3.hen.texture = Content.Load<Texture2D>("img/" + round3.hen.textureFile);
            testowy = Content.Load<Texture2D>("img/tloDuchy");
            foreach (Ground ground in round3.groundList)
            {
                ground.texture = Content.Load<Texture2D>("img/" + ground.textureFile);
            }
            foreach (Board b in round3.boardList)
            {
                b.texture = Content.Load<Texture2D>("img/" + b.textureFile);
            }
            foreach (MyObject o in round3.myObjects)
            {
                o.texture = Content.Load<Texture2D>("img/" + o.textureFile);
            }
            foreach (MySong ms in round3.songs)
            {
                ms.song = Content.Load<Song>("sounds/" + ms.fileName);
            }
            foreach (MySoundEffect ms in round3.soundEffects)
            {
                ms.soundEffect = Content.Load<SoundEffect>("sounds/" + ms.fileName);
            }
            foreach(Ghost ghost in round3.ghostList)
            {
                ghost.texture= Content.Load<Texture2D>("img/" + ghost.textureFile);
                ghost.texture1 = ghost.texture;
                ghost.texture2 = Content.Load<Texture2D>("img/" + ghost.textureFile2);
            }
        }
    }
}
