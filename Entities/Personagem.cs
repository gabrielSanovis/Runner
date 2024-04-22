using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner


{
    public class Personagem
    {
        private Vector2 position; 
        private Vector2 velocity; 
        private bool isJumping;
        private const float JumpVelocity = -13f; 
        private const float Gravity = 0.7f; 
        private bool isDucking; 
        private Rectangle playerBounds;
        private GraphicsDevice graphicsDevice;
        private Texture2D[] runFrames;
        private int currentRunFrames;
        private Texture2D[] jumpFrames;
        private int currentJumpFrames;
        private float frameTime;
        private float timeElapsed;
        private float timeJumpElapsed;
        // Construtor
        public Personagem(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.isJumping = false;
            this.isDucking = false;
            this.frameTime = 0.1f;
            currentRunFrames = 0;
            currentJumpFrames = 0;
            timeElapsed = 0;
            timeJumpElapsed = 0;
        }

        public void Initialize()
        {
            position = new Vector2(100, runFrames[0].Height);
        }
        public void LoadContent(ContentManager content)
        {
            runFrames = new Texture2D[6];
            for (int i = 0; i < 6; i++)
            {
                runFrames[i] = content.Load<Texture2D>("Player/Run/player-run-" + (i + 1));
            }
            jumpFrames = new Texture2D[2];
            for (int i = 0; i < 2; i++)
            {
                jumpFrames[i] = content.Load<Texture2D>("Player/Jump/player-jump-" + (i + 1));
            }
        }

        // Método para atualizar o personagem
        public void Update(GameTime gameTime, bool gameOver)
        {
            if (position.Y >= Game1.ScreenHeight - runFrames[0].Height)
            {
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsed > frameTime)
                {
                    currentRunFrames = (currentRunFrames + 1) % runFrames.Length;
                    timeElapsed -= frameTime;
                }

                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentJumpFrames = 0;
            }
            else
            {
                timeJumpElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeJumpElapsed > 0.7f)
                {
                    currentRunFrames = 0;
                    currentJumpFrames = (currentJumpFrames + 1) % jumpFrames.Length;
                    timeJumpElapsed -= 0.7f;
                }
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isJumping && !isDucking)
            {
               
                if (position.Y >= Game1.ScreenHeight - runFrames[0].Height)
                {
                    timeElapsed = 0;
                    velocity.Y = gameOver ? 0 : JumpVelocity; 
                    isJumping = true; 
                }
            }
            else
            {
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
            position.Y = MathHelper.Clamp(position.Y, 0, Game1.ScreenHeight - runFrames[0].Height); // Ajuste de acordo com o tamanho da tela
            playerBounds = new Rectangle((int)position.X, (int)position.Y, runFrames[0].Width - 13, runFrames[0].Height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (position.Y >= Game1.ScreenHeight - runFrames[0].Height)
            {
                spriteBatch.Draw(runFrames[currentRunFrames], position, Color.White);
            }
            else
            {
                spriteBatch.Draw(jumpFrames[currentJumpFrames], position, Color.White);
            }
        }

        public bool CheckCollision(Rectangle obstacleBounds)
        {
            return playerBounds.Intersects(obstacleBounds);
        }
    }
}