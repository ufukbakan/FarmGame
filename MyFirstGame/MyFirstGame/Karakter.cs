using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    class Karakter
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Vector2 position = new Vector2(200, 200);
        public float speed = 5;
        public Karakter(Texture2D setTexture)
        {
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public Karakter(Texture2D setTexture,Vector2 setPosition)
        {
            position = setPosition;
            texture = setTexture;
            hitbox.Width = texture.Width;
            hitbox.Height = texture.Height;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Update()
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (kstate.IsKeyDown(Keys.Down))
                position.Y += speed;
            if (kstate.IsKeyDown(Keys.Right))
                position.X += speed;
            if (kstate.IsKeyDown(Keys.Left))
                position.X -= speed;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
