namespace TerraForge.Graphics;

public struct Color
{
    public float R { get; }
    public float G { get; }
    public float B { get; }
    public float A { get; }

    public static Color Red => new Color(1f, 0f, 0f);
    public static Color Green => new Color(0f, 1f, 0f);
    public static Color Blue => new Color(0f, 0f, 1f);
    public static Color White => new Color(1f, 1f, 1f);
    public static Color Black => new Color(0f, 0f, 0f);
    public static Color Yellow => new Color(1f, 1f, 0f);
    
    public Color(float r, float g, float b, float a = 1f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(uint r, uint g, uint b, float a = 1)
    {
        R = (float)r / 256;
        G = (float)g / 256;
        B = (float)b / 256;
        A = a;
    }

    public static Color FromHex(string hex)
    {
        if (hex[0] == '#') hex = hex.Substring(1);

        string red = hex.Substring(0, 2);
        string green = hex.Substring(2, 2);
        string blue = hex.Substring(4, 2);

        int r = (Convert.ToInt32(red[0]) * 16 + Convert.ToInt32(red[1])) / 256;
        int g = (Convert.ToInt32(green[0]) * 16 + Convert.ToInt32(green[1])) / 256;
        int b = (Convert.ToInt32(blue[0]) * 16 + Convert.ToInt32(blue[1])) / 256;

        return new Color(r, g, b);
    }
}