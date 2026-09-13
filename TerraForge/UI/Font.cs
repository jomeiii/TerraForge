using Typography.OpenFont;

namespace TerraForge.UI;

public class Font
{
    public Typeface Typeface { get; }

    public Font(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        OpenFontReader reader = new OpenFontReader();
        Typeface Typeface = reader.Read(fs);
    }
}