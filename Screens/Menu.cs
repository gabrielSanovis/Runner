using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner;
public class Menu
{
    private SpriteFont font;
    private Vector2 textPosition;
    private string startText = "Clique para iniciar";
    private bool isClicked = false;

    public event EventHandler StartGameClicked;

    public Menu(SpriteFont font)
    {
        this.font = font;
        textPosition = new Vector2(100, 100); // Posição do texto do menu
    }

    public void Update()
    {
        // Verifica se o texto foi clicado
        if (!isClicked && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            isClicked = true;
            StartGameClicked?.Invoke(this, EventArgs.Empty); // Dispara o evento de início de jogo
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, startText, textPosition, Color.White);
    }
}