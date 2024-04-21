using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Runner;

namespace Runner;

public class Game1 : Game
{
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D characterTexture; // Textura do personagem
        private Texture2D obstacleTexture; // Textura do obstáculo
        private Personagem player; // Personagem
        private Obstaculo[] obstacles; // Array de obstáculos
        private const int MaxObstacles = 40; // Número máximo de obstáculos na tela
        private float obstacleSpawnTimer = 0f; // Timer para controlar a geração de obstáculos
        private Random random; // Gerador de números aleatórios
        public static int ScreenHeight { get; private set; }
        private float globalSpeed = 3f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1024; // Ajuste conforme necessário
            graphics.PreferredBackBufferHeight = 768; // Ajuste conforme necessário
            random = new Random();
        }

        protected override void Initialize()
        {
            ScreenHeight = GraphicsDevice.Viewport.Height;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterTexture = Content.Load<Texture2D>("quadrado"); // Carrega a textura do personagem
            obstacleTexture = Content.Load<Texture2D>("obstaculo"); // Carrega a textura do obstáculo
            player = new Personagem(characterTexture, new Vector2(100, GraphicsDevice.Viewport.Height - characterTexture.Height)); // Cria o personagem
            obstacles = new Obstaculo[MaxObstacles]; // Inicializa o array de obstáculos
        }

        protected override void UnloadContent()
        {
            // Descarrega conteúdos da memória
        }

        protected override void Update(GameTime gameTime)
        {
            // Fecha o jogo se pressionar Esc
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Atualiza o jogador
            player.Update(gameTime);

            globalSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;

            // Gera novos obstáculos
            obstacleSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (obstacleSpawnTimer > 1.5f ) // Ajusta o tempo
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    if (obstacles[i] == null)
                    {
                        obstacles[i] = new Obstaculo(obstacleTexture, new Vector2(GraphicsDevice.Viewport.Width + globalSpeed + obstacleTexture.Height + 50 , GraphicsDevice.Viewport.Height - obstacleTexture.Height), globalSpeed); // Ajusta a posição e velocidade conforme necessário
                        break;
                    }
                }
                obstacleSpawnTimer = 0f;
            }

            // Atualiza os obstáculos e verifica colisões
            foreach (Obstaculo obstacle in obstacles)
            {
                if (obstacle != null)
                {
                    obstacle.Update(gameTime, globalSpeed);
                    if (player.CheckCollision(obstacle.GetBounds()))
                    {
                        // O jogador perdeu, implemente a lógica apropriada aqui
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Desenha o jogador
            player.Draw(spriteBatch);

            // Desenha os obstáculos
            foreach (Obstaculo obstacle in obstacles)
            {
                if (obstacle != null)
                {
                    obstacle.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }