using System.Runtime.InteropServices;
using FreeTypeSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using static FreeTypeSharp.FT;

namespace TerraForge.UI;

public unsafe class Font
{
    public uint FontSize { get; }
    public float LineHeight { get; }

    private FT_LibraryRec_* _library;
    private FT_FaceRec_* _face;
    private int _texture;

    private int _cellSize = 64;
    private int _atlasSize = 1024;

    private Dictionary<char, Glyph> _glyphs;
    private byte[] _atlas;

    private int _vao;
    private int _vbo;

    public Font(string path, uint size)
    {
        FontSize = size;
        
        FT_LibraryRec_* library = null;
        FT_Init_FreeType(&library);
        _library = library;

        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        FT_FaceRec_* face = null;
        var error = FT_New_Face(_library, (byte*)pathPtr, 0, &face);
        Marshal.FreeHGlobal(pathPtr);
        _face = face;

        
        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to load font: {error}");

        error = FT_Set_Pixel_Sizes(
            _face,
            0,
            size
        );

        if (error != FT_Error.FT_Err_Ok)
            throw new Exception($"Failed to set font size: {error}");
        
        LineHeight = _face->size->metrics.height.ToInt32() / 64f;
        
        _glyphs = new Dictionary<char, Glyph>();
        _atlas = new byte[_atlasSize * _atlasSize];

        int cellsPerRow = _atlasSize / _cellSize;

        for (char c = ' '; c <= '~'; c++)
        {
            uint glyphIndex = FT_Get_Char_Index(_face, c);

            if (glyphIndex == 0)
            {
                continue;
            }

            error = FT_Load_Glyph(
                _face,
                glyphIndex,
                FT_LOAD.FT_LOAD_DEFAULT
            );

            if (error != FT_Error.FT_Err_Ok)
                throw new Exception($"Failed to load glyph: {error}");

            error = FT_Render_Glyph(
                _face->glyph,
                FT_Render_Mode_.FT_RENDER_MODE_NORMAL
            );

            if (error != FT_Error.FT_Err_Ok)
                throw new Exception($"Failed to render glyph: {error}");

            int width = (int)_face->glyph->bitmap.width;
            int height = (int)_face->glyph->bitmap.rows;
            int pitch = _face->glyph->bitmap.pitch;

            int index = c - ' ';
            int column = index % cellsPerRow;
            int row = index / cellsPerRow;

            int offsetX = column * _cellSize;
            int offsetY = row * _cellSize;
            
            _glyphs[c] = new Glyph(new Vector2(width, height),
                new Vector2(_face->glyph->bitmap_left, _face->glyph->bitmap_top),
                _face->glyph->advance.x / 64.0f,
                new Vector2((float)offsetX / _atlasSize, (float)offsetY / _atlasSize),
                new Vector2((float)(offsetX + width) / _atlasSize,
                    (float)(offsetY + height) / _atlasSize));
            
            // Console.WriteLine($"Added glyph: '{c}'");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte value = _face->glyph->bitmap.buffer[y * pitch + x];

                    int atlasX = offsetX + x;
                    int atlasY = offsetY + (height - 1 - y);

                    _atlas[atlasY * _atlasSize + atlasX] = value;
                }
            }
        }

        _texture = GL.GenTexture();

        GL.BindTexture(TextureTarget.Texture2D, _texture);
        GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.R8,
            _atlasSize,
            _atlasSize,
            0,
            PixelFormat.Red,
            PixelType.UnsignedByte,
            _atlas
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
        
        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapS,
            (int)TextureWrapMode.ClampToEdge
        );

        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapT,
            (int)TextureWrapMode.ClampToEdge
        );
    }
    
    public void Bind()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, _texture);
    }

    public bool TryGetGlyph(char c, out Glyph glyph)
    {
        return _glyphs.TryGetValue(c, out glyph);
    }
    
    public void Dispose()
    {
        GL.DeleteTexture(_texture);
    }
}