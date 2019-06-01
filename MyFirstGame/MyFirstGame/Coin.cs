using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    public class Coin
    {
        public Texture2D texture;
        public Vector2 position = new Vector2(900,500);
        public Rectangle hitbox;

        public Coin(Texture2D setTexture)
        {
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public Coin(Texture2D setTexture, Vector2 setPosition)
        {
            texture = setTexture;
            position = setPosition;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            
        }
    }
}
