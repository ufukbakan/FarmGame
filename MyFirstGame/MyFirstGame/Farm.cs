using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
namespace MyFirstGame
{
    public class Farm
    {
        public bool isSeeded = false;
        public Texture2D texture;
        public Texture2D emptyTexture;
        public Texture2D seededTexture;
        public Vector2 position = new Vector2(1000,150);
        public Rectangle hitbox;
        public Timer cornFarmTimer = new Timer(15000);

        public Farm(Texture2D setEmptyTexture,Texture2D setSeededTexture)
        {
            emptyTexture = setEmptyTexture;
            seededTexture = setSeededTexture;
            texture = emptyTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            cornFarmTimer.Elapsed += growCorn;
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
            cornFarmTimer.Elapsed += growCorn;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void growCorn(object sender, ElapsedEventArgs e)
        {
            this.texture = emptyTexture;
            Game1.corns++;
            Game1.cornString = "Corns: " + Game1.corns.ToString();
            cornFarmTimer.Stop();
            this.isSeeded = false;

        }

        public void Seed()
        {
            Game1.cornSeeds--;
            Game1.cornSeedsString = "Corn Seeds: " + Game1.cornSeeds.ToString();
            this.texture = seededTexture;
            this.isSeeded = true;
            this.cornFarmTimer.Start();
        }
    }
}
