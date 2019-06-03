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
        static public Seed watermelonSeed;
        static public Vegatable corn;
        static public Vegatable pumpkin;
        static public Vegatable watermelon;
        Random randomGenerator = new Random();

        List<Seed> seedsList = new List<Seed>();
        public static List<Vegatable> vegatablesList = new List<Vegatable>();
        List<sellSeed> signsList = new List<sellSeed>();

        string seedString = "";
        static uint lastCoin = 1;
        sellSeed cornSign;
        sellSeed pumpkinSign;
        sellSeed watermelonSign;
        public static string vegatablesString = "";
        Bazaar pazar;
        List<Farm> farmList = new List<Farm>();

        Dictionary<uint, Coin> CoinList = new Dictionary<uint, Coin>();
        int money = 4;
        string moneyString;

        KeyboardState newState;
        KeyboardState oldState;

        public static Texture2D goLeftTexture;
        public static Texture2D goRightTexture;
        public static Texture2D goUpTexture;


        public void createCoin(object sender, ElapsedEventArgs e)
        {
            var coinTexture = Content.Load<Texture2D>("coin");
            int x = randomGenerator.Next(200, 900);
            int y = randomGenerator.Next(200, 550);
            CoinList[lastCoin] = new Coin(coinTexture,new Vector2(x,y));
            lastCoin++;
            coinTimer.Stop();
        }


        
        public Game1()
        {
            coinTimer.Elapsed += createCoin;
            coinTimer.Start();
            corn = new Vegatable("corn");
            corn.cost = 5;
            pumpkin = new Vegatable("pumpkin");
            pumpkin.cost = 22;
            watermelon = new Vegatable("watermelon");
            watermelon.cost = 100;
            cornSeed = new Seed("cornSeed");
            cornSeed.price = 3;
            pumpkinSeed = new Seed("pumpkinSeed");
            pumpkinSeed.price = 15;
            watermelonSeed = new Seed("watermelonSeed");
            watermelonSeed.price = 66;
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
            var chTexture = Content.Load<Texture2D>("girlChar");
            goLeftTexture = Content.Load<Texture2D>("girlGoLeft");
            goRightTexture = Content.Load<Texture2D>("girlGoRight");
            goUpTexture = Content.Load<Texture2D>("girlGoUp");
            var cornSignTexture = Content.Load<Texture2D>("sellCorn");
            var pumpkinSignTexture = Content.Load<Texture2D>("sellPumpkin");
            var watermelonSignTexture = Content.Load<Texture2D>("sellWatermelon");
            var farmTexture = Content.Load<Texture2D>("farm");
            var seededFarmTexture = Content.Load<Texture2D>("farmSeeded");
            var bazaarTexture = Content.Load<Texture2D>("bazaar");
            cornSign = new sellSeed(cornSignTexture,ref cornSeed);
            pumpkinSign = new sellSeed(pumpkinSignTexture, new Vector2(214, 450), ref pumpkinSeed);
            watermelonSign = new sellSeed(watermelonSignTexture, new Vector2(278, 450), ref watermelonSeed);
            signsList.Add(cornSign);
            signsList.Add(pumpkinSign);
            signsList.Add(watermelonSign);
            Karakter = new Karakter(chTexture);
            farmList.Add(new Farm(farmTexture, seededFarmTexture));
            farmList.Add(new Farm(farmTexture, seededFarmTexture, new Vector2(904,150)));
            farmList.Add(new Farm(farmTexture, seededFarmTexture, new Vector2(808,150)));
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
                money += 2;
                moneyString = money + " $";
                CoinList.Remove(lastCoin - 1);
                coinTimer.Start();
            }

            foreach(sellSeed sign in signsList)
            {
                if (Karakter.hitbox.Intersects(sign.hitbox) && money >= sign.seedType.price && Keyboard.GetState().IsKeyDown(Keys.E) && sign.purchasable == true)
                {
                    sign.purchasable = false;
                    newState = Keyboard.GetState();
                    bool onList = false;
                    money -= sign.seedType.price;
                    sign.seedType.count++;
                    moneyString = money + " $";

                    foreach (Seed seed in seedsList)
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

                    //Thread.Sleep(200);

                }

                if (Karakter.hitbox.Intersects(sign.hitbox) && Keyboard.GetState().IsKeyUp(Keys.E) && sign.purchasable == false)
                {
                    sign.purchasable = true;
                }
            }

            foreach (Seed seed in seedsList)
            {
                if (seed.count <= 0)
                {
                    seedsList.Remove(seed);
                    break;
                }
            }

            seedString = "";
            foreach (Seed seed in seedsList)
            {
                seedString += seed.listName + ": " + seed.count.ToString() + "\n";
                //seedString += "\n";
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
                if (vg.count <= 0)
                {
                    vegatablesList.Remove(vg);
                    break;
                }
            }

            vegatablesString = "";
            foreach (Vegatable vg in vegatablesList)
            {
                vegatablesString += char.ToUpper(vg.name[0]) + vg.name.Substring(1) + ": " + vg.count.ToString() + "\n";
            }


            if (vegatablesList.Count > 0 && Karakter.hitbox.Intersects(pazar.hitbox))
            {
                money += vegatablesList[vegatablesList.Count - 1].cost;
                vegatablesList[vegatablesList.Count - 1].count--;
                moneyString = money.ToString() + " $";
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
            


            spriteBatch.DrawString(font, moneyString, new Vector2(1000, 0), Color.Black);
            spriteBatch.DrawString(font, vegatablesString, new Vector2(890,0), Color.Black);
            spriteBatch.DrawString(font, seedString, new Vector2(745, 0), Color.Black);
            foreach(sellSeed sign in signsList)
            {
                sign.Draw(spriteBatch,font);
            }
            foreach(Farm farm in farmList)
            {
                farm.Draw(spriteBatch);
                if (Karakter.hitbox.Intersects(farm.hitbox) && farm.isSeeded == false)
                {
                    if (seedsList.Count > 0)
                        spriteBatch.DrawString(font, "Press E to seed", new Vector2(450, 450), Color.Black);
                    else
                        spriteBatch.DrawString(font, "You have no seed\nAfter your seeds grown up don't forget to go to bazaar", new Vector2(450, 450), Color.Black);
                }

            }
            pazar.Draw(spriteBatch);

            if (CoinList.Count != 0)
                CoinList[lastCoin - 1].Draw(spriteBatch);

            Karakter.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
