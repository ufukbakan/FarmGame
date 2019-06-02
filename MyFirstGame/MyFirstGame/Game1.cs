using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class Game1 : Game
    {
        public static Karakter Karakter;
        SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Timer coinTimer = new Timer(10000);
        static public Seed cornSeed;
        static public Seed pumpkinSeed;
        static public Vegatable corn;
        static public Vegatable pumpkin;

        List<Seed> seedsList = new List<Seed>();
        public static List<Vegatable> vegatablesList = new List<Vegatable>();
        List<sellSeed> signsList = new List<sellSeed>();

        string seedString = "";
        static uint lastCoin = 1;
        sellSeed cornSign;
        sellSeed pumpkinSign;
        //public static int cornSeeds = 0;
        //public static string cornSeedsString;
        //public static int corns = 0;
        public static string vegatablesString = "";
        Bazaar pazar;
        List<Farm> farmList = new List<Farm>();

        Dictionary<uint, Coin> CoinList = new Dictionary<uint, Coin>();
        int money = 10;
        string moneyString;

        public void createCoin(object sender, ElapsedEventArgs e)
        {
            var coinTexture = Content.Load<Texture2D>("coin");
            CoinList[lastCoin] = new Coin(coinTexture);
            lastCoin++;
            coinTimer.Stop();
        }


        
        public Game1()
        {
            coinTimer.Elapsed += createCoin;
            coinTimer.Start();
            corn = new Vegatable("corn");
            cornSeed = new Seed("cornSeed");
            cornSeed.price = 5;
            pumpkinSeed = new Seed("pumpkinSeed");
            pumpkinSeed.price = 25;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1100;
            Content.RootDirectory = "Content";
            moneyString = money.ToString() + " $";
            foreach(Vegatable vg in vegatablesList)
            {
                vegatablesString = vg.name + ": " + vg.count.ToString() +"\n";
            }

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
            var chTexture = Content.Load<Texture2D>("blackman");
            var cornSignTexture = Content.Load<Texture2D>("sellCorn");
            var pumpkinSignTexture = Content.Load<Texture2D>("sellPumpkin");
            var farmTexture = Content.Load<Texture2D>("farm");
            var seededFarmTexture = Content.Load<Texture2D>("farmSeeded");
            var bazaarTexture = Content.Load<Texture2D>("bazaar");
            cornSign = new sellSeed(cornSignTexture,ref cornSeed);
            pumpkinSign = new sellSeed(pumpkinSignTexture, new Vector2(214, 450), ref pumpkinSeed);
            signsList.Add(cornSign);
            signsList.Add(pumpkinSign);
            Karakter = new Karakter(chTexture);
            farmList.Add(new Farm(farmTexture, seededFarmTexture));
            farmList.Add(new Farm(farmTexture, seededFarmTexture, new Vector2(1000,182)));
            farmList.Add(new Farm(farmTexture, seededFarmTexture, new Vector2(1000,118)));
            pazar = new Bazaar(bazaarTexture);
            font = Content.Load<SpriteFont>("Money");


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Karakter.Update();
            if (CoinList.Count != 0 && Karakter.hitbox.Intersects(CoinList[lastCoin - 1].hitbox))
            {
                money += 10;
                moneyString = money + " $";
                CoinList.Remove(lastCoin - 1);
                coinTimer.Start();
            }

            foreach(sellSeed sign in signsList)
            {
                if(Karakter.hitbox.Intersects(sign.hitbox) && money >= sign.seedType.price && Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    bool onList = false;
                    money -= sign.seedType.price;
                    sign.seedType.count++;
                    moneyString = money + " $";

                    foreach(Seed seed in seedsList)
                    {
                        if(seed.name == sign.seedType.name)
                        {
                            onList = true;
                            break;
                        }
                    }
                    if(onList == false)
                    {
                        seedsList.Add(sign.seedType);
                    }

                    Thread.Sleep(200);

                }
            }

            foreach (Seed seed in seedsList)
            {
                if (seed.count == 0)
                {
                    seedsList.Remove(seed);
                    break;
                }
            }

            seedString = "";
            foreach (Seed seed in seedsList)
            {
                seedString += seed.listName + " " + seed.count.ToString();
                seedString += "\n";
            }

            foreach (Farm farm in farmList)
            {
                if (seedsList.Count > 0 && farm.isSeeded == false && Karakter.hitbox.Intersects(farm.hitbox) && Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    farm.Seed(ref seedsList);
                }

            }

            foreach (Vegatable vg in vegatablesList)
            {
                if (vg.count == 0)
                    vegatablesList.Remove(vg);
            }

            vegatablesString = "";
            foreach (Vegatable vg in vegatablesList)
            {
                vegatablesString = vg.name + ": " + vg.count.ToString() + "\n";
            }


            if (corn.count > 0 && Karakter.hitbox.Intersects(pazar.hitbox))
            {

                /*corn.count--;
                cornString = "Corns: " + corn.count.ToString();
                money += 50;
                moneyString = money + " $";*/
            }

            
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            if(CoinList.Count != 0)
                CoinList[lastCoin -1].Draw(spriteBatch);

            spriteBatch.DrawString(font, moneyString, new Vector2(990, 0), Color.Black);
            spriteBatch.DrawString(font, vegatablesString, new Vector2(890,0), Color.Black);
            spriteBatch.DrawString(font, seedString, new Vector2(790, 0), Color.Black);
            foreach(sellSeed sign in signsList)
            {
                sign.Draw(spriteBatch,font);
            }
            foreach(Farm farm in farmList)
            {
                farm.Draw(spriteBatch);
                if (Karakter.hitbox.Intersects(farm.hitbox))
                {
                    spriteBatch.DrawString(font, "Press E to seed", new Vector2(450, 450), Color.Black);
                }
            }
            pazar.Draw(spriteBatch);

            /*if(cornSign.hitbox.Intersects(Karakter.hitbox))
            {
                spriteBatch.DrawString(font, "Press E to buy corn seed for 10$", new Vector2(450, 450), Color.Black);
            }*/
            

            Karakter.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
