using OpenTK.Mathematics;

namespace TerraForge.UI;

public class Glyph
{
    public Vector2 Size { get; }
    public Vector2 Bearing { get; }
    public float Advance { get; }

    public Glyph(
        Vector2 size,
        Vector2 bearing,
        float advance
    )
    {
        Size = size;
        Bearing = bearing;
        Advance = advance;
    }
}