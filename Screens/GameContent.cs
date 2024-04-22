using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner


{
    public class GameContent
    {
        private Texture2D obstacleTexture; // Textura do obstáculo
        private Texture2D moedaTexture; // Textura da moeda
        private Personagem _player; // Personagem
        private SpriteFont font;
        private Obstaculo[] obstacles; // Array de obstáculos
        private Moeda[] moedas; // Array de moedas
        private const int MaxMoedas = 30; // Número máximo de moedas na tela
        private const int MaxObstacles = 40; // Número máximo de obstáculos na tela
        private float obstacleSpawnTimer = 0f; // Timer para controlar a geração de obstáculos
        private float moedaSpawnTimer = 0f; // Timer para controlar a geração de moedas

        private int pontuacao = 0;
        private float globalSpeed = 3f;

        private bool gameOver = false;
        private GraphicsDevice graphicsDevice;
        // Construtor
        public GameContent(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            _player.Initialize();
        }
        public void LoadContent(ContentManager content)
        {
            _player = new Personagem(graphicsDevice);
            _player.LoadContent(content);
            obstacleTexture = content.Load<Texture2D>("obstaculo");
            obstacles = new Obstaculo[MaxObstacles];
            moedaTexture = content.Load<Texture2D>("moeda");
            moedas = new Moeda[MaxMoedas];
            font = content.Load<SpriteFont>("Fonts/arial24");
        }

        // Método para atualizar o personagem
        public void Update(GameTime gameTime)
        {

            globalSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
            obstacleSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Update(gameTime, gameOver);
            if (obstacleSpawnTimer > 1.5f) // Ajusta o tempo
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    if (obstacles[i] == null)
                    {
                        obstacles[i] = new Obstaculo(obstacleTexture, new Vector2(graphicsDevice.Viewport.Width + globalSpeed + obstacleTexture.Height + 50, graphicsDevice.Viewport.Height - obstacleTexture.Height), globalSpeed);
                        break;
                    }
                }
                obstacleSpawnTimer = 0f;
            }
            moedaSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moedaSpawnTimer > 1.5f)
            {
                for (int i = 0; i < moedas.Length; i++)
                {
                    if (moedas[i] == null)
                    {
                        moedas[i] = new Moeda(moedaTexture, new Vector2(graphicsDevice.Viewport.Width + globalSpeed + moedaTexture.Height + 50, graphicsDevice.Viewport.Height - 250), globalSpeed);
                        break;
                    }
                }
                moedaSpawnTimer = 0f;
            }

            foreach (Obstaculo obstacle in obstacles)
            {
                if (obstacle != null)
                {
                    obstacle.Update(gameTime, gameOver ? 0 : globalSpeed);
                    if (_player.CheckCollision(obstacle.GetBounds()))
                    {
                        gameOver = true;
                    }
                }
            }
            foreach (Moeda moeda in moedas)
            {
                if (moeda != null)
                {
                    moeda.Update(gameTime, gameOver ? 0 : globalSpeed);
                    if (_player.CheckCollision(moeda.GetBounds()) && moeda.IsVisible())
                    {
                        pontuacao++;
                        moeda.SetVisible(false);
                        // Outras ações quando o jogador colide com a moeda, como remover a moeda do jogo
                    }
                }
            }
        }

        // Método para desenhar o personagem
        public void Draw(SpriteBatch spriteBatch)
        {
            _player.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + pontuacao, new Vector2(10, 10), Color.Black);
            if (gameOver)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(graphicsDevice.Viewport.Width / 2 - 50, graphicsDevice.Viewport.Height / 2), Color.Black);
            }
            foreach (Moeda moeda in moedas)
            {
                if (moeda != null)
                {
                    if (moeda.IsVisible())
                    {
                        moeda.Draw(spriteBatch);

                    }
                }
            }
            foreach (Obstaculo obstacle in obstacles)
            {
                if (obstacle != null)
                {
                    obstacle.Draw(spriteBatch);
                }
            }
        }
    }
}