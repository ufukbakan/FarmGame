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
    public class Karakter
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Texture2D idleTexture;
        public Vector2 position = new Vector2(200, 200);
        public float speed = 2;

        public Karakter(Texture2D setTexture)
        {
            idleTexture = setTexture;
            texture = idleTexture;
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
            {
                this.texture = Game1.goUpTexture;
                position.Y -= speed;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                this.texture = idleTexture;
                position.Y += speed;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                this.texture = Game1.goRightTexture;
                position.X += speed;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                this.texture = Game1.goLeftTexture;
                position.X -= speed;
            }
            if(kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Up))
            {
                this.texture = idleTexture;
            }
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
