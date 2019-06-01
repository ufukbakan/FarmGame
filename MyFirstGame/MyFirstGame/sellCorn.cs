using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    class sellCorn
    {
        public Texture2D texture;
        public Rectangle hitbox;
        public Vector2 position = new Vector2(150, 450);

        public sellCorn(Texture2D setTexture)
        {
            texture = setTexture;
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
