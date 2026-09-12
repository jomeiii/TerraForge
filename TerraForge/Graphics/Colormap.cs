using OpenTK.Mathematics;
using StbImageSharp;

namespace TerraForge.Graphics;

public class Colormap
{
    private readonly ImageResult _image;

    public Colormap(string path)
    {
        string fullPath = Path.Combine(
            AppContext.BaseDirectory,
            path
        );

        using FileStream stream = File.OpenRead(fullPath);

        _image = ImageResult.FromStream(
            stream,
            ColorComponents.RedGreenBlue
        );
    }

    public Vector3 GetColor(float temperature, float downfall)
    {
        temperature = Math.Clamp(temperature, 0f, 1f);
        downfall = Math.Clamp(downfall, 0f, 1f);

        downfall *= temperature;

        int x = (int)((1f - temperature) * 255f);
        int y = (int)((1f - downfall) * 255f);

        int index = (y * _image.Width + x) * 3;

        byte r = _image.Data[index];
        byte g = _image.Data[index + 1];
        byte b = _image.Data[index + 2];

        return new Vector3(
            r / 255f,
            g / 255f,
            b / 255f
        );
    }
}