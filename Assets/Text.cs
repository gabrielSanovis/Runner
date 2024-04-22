using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    public class Text
    {
        private SpriteFont font; // Textura da moeda
        private Vector2 position; // Posição da moeda

        private string updateText;

        public Text(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
        }       
        public void Update(GameTime gameTime, string text)
        {
            updateText = text;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, updateText, position, Color.Black);
        }
    }
}

