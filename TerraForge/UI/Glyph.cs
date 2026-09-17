using OpenTK.Mathematics;

namespace TerraForge.UI;

public struct Glyph
{
    public Vector2 Size { get; }
    public Vector2 Bearing { get; }
    public float Advance { get; } // offset from the origin
    
    public Vector2 UVMin { get; }
    public Vector2 UVMax { get; }

    public Glyph(Vector2 size, Vector2 bearing, float advance, Vector2 uvMin, Vector2 uvMax)
    {
        Size = size;
        Bearing = bearing;
        Advance = advance;
        
        UVMin = uvMin;
        UVMax = uvMax;
    }
}