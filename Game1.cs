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
    private GameContent gameContent; // Personagem
    private Menu menu;
    private SpriteFont font;
    private string CurrentContent = "menu";

    private Random random; // Gerador de números aleatórios
    public static int ScreenHeight { get; private set; }


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
        base.Initialize();
        ScreenHeight = GraphicsDevice.Viewport.Height;
        gameContent.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        gameContent = new GameContent(GraphicsDevice);
        gameContent.LoadContent(Content);

        font = Content.Load<SpriteFont>("Fonts/arial24"); // Carrega a fonte para o texto do menu
        menu = new Menu(font);
        menu = new Menu(font);
        menu.StartGameClicked += StartGame;
    }

    private void StartGame(object sender, EventArgs e)
    {
        CurrentContent = "game";
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
        menu.Update();
        gameContent.Update(gameTime);

        // Gera novos obstáculos

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        switch (CurrentContent)
        {
            case "menu":
                menu.Draw(spriteBatch);
                break;
            case "game":
                gameContent.Draw(spriteBatch);
                break;
            default:
                menu.Draw(spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}