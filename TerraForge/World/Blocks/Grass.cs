using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.World.Blocks;

public class Grass : Cube
{
    private readonly Texture _topTexture;
    private readonly Texture _sideTexture;
    private readonly Texture _bottomTexture;
    private readonly Texture _sideOverlayTexture;

    private readonly Colormap _colormap;

    public Grass(Mesh mesh,
        Texture topTexture,
        Texture sideTexture,
        Texture bottomTexture,
        Texture sideOverlay,
        Vector3 position,
        Colormap colormap) : base(mesh, position)
    {
        _topTexture = topTexture;
        _sideTexture = sideTexture;
        _bottomTexture = bottomTexture;
        _sideOverlayTexture = sideOverlay;
        _colormap = colormap;
    }

    public override void Draw(Shader shader)
    {
        Vector3 grassColor = _colormap.GetColor(
            0.9f,
            0.8f
        );

        shader.SetBool("useBlockColor", false);
        _sideTexture.Use();
        Mesh.Draw(24, 0);

        GL.DepthFunc(DepthFunction.Equal);
        GL.DepthMask(false);

        shader.SetBool("useBlockColor", true);
        shader.SetVector3("blockColor", grassColor);
        _sideOverlayTexture.Use();
        Mesh.Draw(24, 0);

        GL.DepthMask(true);
        GL.DepthFunc(DepthFunction.Less);

        _topTexture.Use();
        Mesh.Draw(6, 24);

        shader.SetBool("useBlockColor", false);
        _bottomTexture.Use();
        Mesh.Draw(6, 30);
    }
}