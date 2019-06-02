using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    class sellSeed
    {
        public Seed seedType;
        public Texture2D texture;
        public Rectangle hitbox;
        public Vector2 position = new Vector2(150, 450);

        public sellSeed(Texture2D setTexture, ref Seed setSeedType)
        {
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            seedType = setSeedType;
        }

        public sellSeed(Texture2D setTexture, Vector2 setPosition, ref Seed setSeedType)
        {
            texture = setTexture;
            position = setPosition;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            seedType = setSeedType;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(texture, position, Color.White);

            if (Game1.Karakter.hitbox.Intersects(this.hitbox))
            {
                spriteBatch.DrawString(font, $"Press E to buy {seedType.listName} seed for {seedType.price}$", new Vector2(450, 450), Color.Black);
            }
        }

    }
}
