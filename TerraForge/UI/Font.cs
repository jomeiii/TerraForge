using System.Runtime.InteropServices;
using FreeTypeSharp;
using static FreeTypeSharp.FT;

namespace TerraForge.UI;

public unsafe class Font
{
    private FT_LibraryRec_* _library;
    private FT_FaceRec_* _face;
    private int _texture;

    public Font(string path, uint size)
    {
        FT_LibraryRec_* library = null;
        FT_Init_FreeType(&library);
        _library = library;

        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        FT_FaceRec_* face = null;
        var error = FT_New_Face(_library, (byte*)pathPtr, 0, &face);
        _face = face;
        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to load font: {error}");
        
        error = FT_Set_Pixel_Sizes(_face, 0, size);
        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to set font size: {error}");
        
        uint glyphIndex = FT_Get_Char_Index(_face, 'A');
        if (glyphIndex == 0)
            throw new Exception("Glyph not found");
        
        error = FT_Load_Glyph(_face, glyphIndex, FT_LOAD.FT_LOAD_DEFAULT);
        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to load glyph: {error}");
        
        error = FT_Render_Glyph(_face->glyph, FT_Render_Mode_.FT_RENDER_MODE_NORMAL);
        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to render glyph: {error}");
    }
}