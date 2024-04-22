using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    public class Moeda
    {
        private Texture2D sprite; // Textura da moeda
        private Vector2 position; // Posição da moeda
        private float speed; // Velocidade da moeda
        private bool isVisible = true;

        public Moeda(Texture2D sprite, Vector2 position, float speed)
        {
            this.sprite = sprite;
            this.position = position;
            this.speed = speed;
        }       
        public void Update(GameTime gameTime, float globalSpeed)
        {
            position.X -= globalSpeed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }
        public bool IsVisible()
        {
            return isVisible;
        }

        public void SetVisible(bool visible)
        {
            isVisible = visible;
        }

    }
}

