using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    public class Obstaculo
    {
        private Texture2D sprite; // Textura do obstáculo
        private Vector2 position; // Posição do obstáculo
        private float speed; // Velocidade do obstáculo
        private bool isVisible = true;

        // Construtor
        public Obstaculo(Texture2D sprite, Vector2 position, float speed)
        {
            this.sprite = sprite;
            this.position = position;
            this.speed = speed;
        }

        // Método para atualizar o obstáculo
        public void Update(GameTime gameTime, float globalSpeed)
        {
            // Movimentar o obstáculo para a esquerda com uma velocidade constante

            // Movimentar o obstáculo para a esquerda com uma velocidade crescente
            position.X -= globalSpeed;
        }

        // Método para desenhar o obstáculo
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        // Método para obter o retângulo de colisão do obstáculo
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