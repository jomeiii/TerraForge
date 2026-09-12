using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace TerraForge.Graphics;

public class Texture : IDisposable
{
    public int Handle { get; }

    public Texture(string path)
    {
        Handle = GL.GenTexture();

        StbImage.stbi_set_flip_vertically_on_load(1);

        using FileStream stream = File.OpenRead(path);

        ImageResult image = ImageResult.FromStream(
            stream,
            ColorComponents.RedGreenBlueAlpha
        );

        Use();

        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba,
            image.Width,
            image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            image.Data
        );

        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Nearest
        );

        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Nearest
        );
    }

    public void Use(
        TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(
            TextureTarget.Texture2D,
            Handle
        );
    }

    public void Dispose()
    {
        GL.DeleteTexture(Handle);
    }
}