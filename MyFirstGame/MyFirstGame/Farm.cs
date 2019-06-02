using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System.Collections.Generic;

namespace MyFirstGame
{
    public class Farm
    {
        public string seedType = null;
        public bool isSeeded = false;
        public Texture2D texture;
        public Texture2D emptyTexture;
        public Texture2D seededTexture;
        public Vector2 position = new Vector2(1000,150);
        public Rectangle hitbox;
        public Timer farmTimer = new Timer(15000);

        public Farm(Texture2D setEmptyTexture,Texture2D setSeededTexture)
        {
            emptyTexture = setEmptyTexture;
            seededTexture = setSeededTexture;
            texture = emptyTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            farmTimer.Elapsed += growSeed;
        }

        public Farm(Texture2D setEmptyTexture, Texture2D setSeededTexture, Vector2 setPosition)
        {
            emptyTexture = setEmptyTexture;
            seededTexture = setSeededTexture;
            texture = emptyTexture;
            position = setPosition;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            farmTimer.Elapsed += growSeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void growSeed(object sender, ElapsedEventArgs e)
        {
            this.texture = emptyTexture;
            bool onList = false;

            string vegatableName = seedType.Replace("Seed", "");
            foreach (Vegatable vg in Game1.vegatablesList)
            {
                if (vg.name == vegatableName)
                {
                    onList = true;
                    break;
                }
            }

            if (seedType == "cornSeed")
            {
                Game1.corn.count++;
                if(onList == false)
                {
                    Game1.vegatablesList.Add(Game1.corn);
                }
            }
            else if (seedType == "pumpkinSeed")
            {
                Game1.pumpkin.count++;
                if (onList == false)
                {
                    Game1.vegatablesList.Add(Game1.pumpkin);
                }
            }
                
            farmTimer.Stop();
            this.isSeeded = false;

        }

        public void Seed(ref List<Seed> seedList)
        {
            seedList[seedList.Count - 1].count -= 1;
            seedType = seedList[seedList.Count - 1].name;
            this.texture = seededTexture;
            this.isSeeded = true;
            this.farmTimer.Start();

            //return seedList[seedList.Count - 1].name;
        }
    }
}
