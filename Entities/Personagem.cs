using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner


{
    public class Personagem
    {
        private Vector2 position; // Posição do personagem
        private Vector2 velocity; // Velocidade do personagem
        private bool isJumping; // Indica se o personagem está pulando
        private const float JumpVelocity = -13f; // Velocidade inicial do pulo
        private const float Gravity = 0.7f; // Gravidade aplicada ao personagem
        private bool isDucking; // Indica se o personagem está agachado
        private Rectangle playerBounds;
        private Texture2D characterTexture; 
        private GraphicsDevice graphicsDevice;
        // Construtor
        public Personagem(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.isJumping = false;
            this.isDucking = false;
        }

        public void Initialize () 
        {
            position = new Vector2(100, graphicsDevice.Viewport.Height - characterTexture.Height);
        }
        public void LoadContent(ContentManager content)
        {
            characterTexture = content.Load<Texture2D>("quadrado");
        }

        // Método para atualizar o personagem
        public void Update(GameTime gameTime, bool gameOver)
        {
            // Verificar entrada do jogador para pular
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isJumping && !isDucking)
            {
                // Verificar se o personagem está no chão
                if (position.Y >= Game1.ScreenHeight - characterTexture.Height)
                {
                    velocity.Y = gameOver ? 0 : JumpVelocity; // Aplicar a velocidade do pulo
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
            position.Y = MathHelper.Clamp(position.Y, 0, Game1.ScreenHeight - characterTexture.Height); // Ajuste de acordo com o tamanho da tela
            playerBounds = new Rectangle((int)position.X, (int)position.Y, characterTexture.Width, characterTexture.Height);

        }

        // Método para desenhar o personagem
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterTexture, position, Color.White);
        }

        // Método para verificar colisão com obstáculos (a ser implementado)
        public bool CheckCollision(Rectangle obstacleBounds)
        {
            return playerBounds.Intersects(obstacleBounds);
            // Implemente a lógica para verificar colisões com os obstáculos aqui
            // Retorna true se houver colisão, false caso contrário
        }
    }
}