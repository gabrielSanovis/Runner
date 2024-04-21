using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner


{
    public class Personagem
    {
        private Texture2D sprite; // Textura do personagem
        private Vector2 position; // Posição do personagem
        private Vector2 velocity; // Velocidade do personagem
        private bool isJumping; // Indica se o personagem está pulando
        private const float JumpVelocity = -13f; // Velocidade inicial do pulo
        private const float Gravity = 0.7f; // Gravidade aplicada ao personagem
        private bool isDucking; // Indica se o personagem está agachado
        private Rectangle playerBounds;
        // Construtor
        public Personagem(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
            this.velocity = Vector2.Zero;
            this.isJumping = false;
            this.isDucking = false;
        }

        // Método para atualizar o personagem
        public void Update(GameTime gameTime)
        {
            // Verificar entrada do jogador para pular
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !isJumping && !isDucking)
            {
                // Verificar se o personagem está no chão
                if (position.Y >= Game1.ScreenHeight - sprite.Height)
                {
                    velocity.Y = JumpVelocity; // Aplicar a velocidade do pulo
                    isJumping = true; // Indicar que o personagem está pulando
                }
                isJumping = false;
            }

            // Verificar entrada do jogador para agachar
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                isDucking = true;
                // Ajuste a altura do personagem ao se agachar
                // Por exemplo: diminuir a altura do retângulo de colisão
            }
            else
            {
                isDucking = false;
                // Resetar a altura do personagem ao se levantar
                // Por exemplo: restaurar a altura do retângulo de colisão
            }

            // Aplicar gravidade ao personagem
            velocity.Y += 0.5f; // Ajuste a gravidade de acordo com o necessário
            position += velocity;
            // Limitar a posição do personagem para mantê-lo na tela
            position.Y = MathHelper.Clamp(position.Y, 0, Game1.ScreenHeight - sprite.Height); // Ajuste de acordo com o tamanho da tela
            playerBounds = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);

        }

        // Método para desenhar o personagem
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        // Método para verificar colisão com obstáculos (a ser implementado)
        public bool CheckCollision(Rectangle obstacleBounds)
        {
            // Console.WriteLine(playerBounds.Y + playerBounds.Height); 
            // if((playerBounds.Y + playerBounds.Height) < obstacleBounds.Y) {
            //     Console.WriteLine("isTrue"); 
            // }
            // Console.WriteLine("if ({0}) < {1}", playerBounds.Y + playerBounds.Height, obstacleBounds.Y); 
            //Calcula o hit X
            // if (obstacleBounds.X <= (playerBounds.X + playerBounds.Width) && obstacleBounds.X >= playerBounds.X)
            // {
            //     Console.WriteLine("isTrue"); 
            // }
            return false;
            // Implemente a lógica para verificar colisões com os obstáculos aqui
            // Retorna true se houver colisão, false caso contrário
        }
    }
}