using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.UI;

public class UI : IDisposable
{
    private readonly Shader _shader;
    private readonly Font _font;
    private readonly TextRenderer _textRenderer;

    public Font Font => _font;
    
    public UI(int width, int height)
    {
        _shader = new Shader(
            "ui.vert",
            "ui.frag"
        );

        _font = new Font(
            "Resources/Roboto-Bold.ttf",
            18
        );

        _textRenderer = new TextRenderer();

        _shader.Use();

        _shader.SetInt(
            "textTexture",
            0
        );

        SetProjection(width, height);
    }

    private void SetProjection(int width, int height)
    {
        _shader.SetMatrix4(
            "projection",
            Matrix4.CreateOrthographicOffCenter(
                0,
                width,
                height,
                0,
                -1,
                1
            )
        );
    }

    public void DrawText(string text, float x, float y, uint lineUpperCount, Color color)
    {
        _shader.Use();
        _shader.SetColor("textColor", color);
        _font.Bind();
        _textRenderer.Draw(text, x, _font.FontSize + y + _font.LineHeight * lineUpperCount, _font);
    }

    public void Dispose()
    {
        _font.Dispose();
        _textRenderer.Dispose();
        _shader.Dispose();
    }
}