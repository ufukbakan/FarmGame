using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    class Bazaar
    {
        public Texture2D texture;
        public Vector2 position = new Vector2(50, 50);
        public Rectangle hitbox;
        
        public Bazaar(Texture2D setTexture)
        {
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public Bazaar(Texture2D setTexture, Vector2 setPosition)
        {
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            position = setPosition;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
