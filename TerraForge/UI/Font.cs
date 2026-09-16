using System.Runtime.InteropServices;
using FreeTypeSharp;
using OpenTK.Graphics.OpenGL4;
using static FreeTypeSharp.FT;

namespace TerraForge.UI;

public unsafe class Font
{
    private FT_LibraryRec_* _library;
    private FT_FaceRec_* _face;
    private int _texture;

    private Dictionary<char, Glyph> _glyphs = new();

    private int _cellSize = 64;
    private int _atlasSize = 1024;

    private int _vao;
    private int _vbo;

    public Font(string path, uint size)
    {
        FT_LibraryRec_* library = null;
        FT_Init_FreeType(&library);
        _library = library;

        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);

        FT_FaceRec_* face = null;
        var error = FT_New_Face(
            _library,
            (byte*)pathPtr,
            0,
            &face
        );

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

        // =========================
        // CREATE ATLAS
        // =========================

        byte[] atlas = new byte[
            _atlasSize * _atlasSize
        ];

        int cellsPerRow = _atlasSize / _cellSize;

        for (char c = ' '; c <= '~'; c++)
        {
            uint glyphIndex = FT_Get_Char_Index(
                _face,
                c
            );

            if (glyphIndex == 0)
                continue;

            error = FT_Load_Glyph(
                _face,
                glyphIndex,
                FT_LOAD.FT_LOAD_DEFAULT
            );

            if (error != FT_Error.FT_Err_Ok)
                throw new Exception(
                    $"Failed to load glyph '{c}': {error}"
                );

            error = FT_Render_Glyph(
                _face->glyph,
                FT_Render_Mode_.FT_RENDER_MODE_NORMAL
            );

            if (error != FT_Error.FT_Err_Ok)
                throw new Exception(
                    $"Failed to render glyph '{c}': {error}"
                );

            int width = (int)_face->glyph->bitmap.width;
            int height = (int)_face->glyph->bitmap.rows;
            int pitch = _face->glyph->bitmap.pitch;

            int index = c - ' ';

            int column = index % cellsPerRow;
            int row = index / cellsPerRow;

            int offsetX = column * _cellSize;
            int offsetY = row * _cellSize;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte value =
                        _face->glyph->bitmap.buffer[
                            y * pitch + x
                        ];

                    int atlasX = offsetX + x;
                    int atlasY = offsetY + y;

                    atlas[
                        atlasY * _atlasSize + atlasX
                    ] = value;
                }
            }
        }

        // =========================
        // UPLOAD ATLAS TO OPENGL
        // =========================

        _texture = GL.GenTexture();

        GL.BindTexture(
            TextureTarget.Texture2D,
            _texture
        );

        GL.PixelStore(
            PixelStoreParameter.UnpackAlignment,
            1
        );

        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.R8,
            _atlasSize,
            _atlasSize,
            0,
            PixelFormat.Red,
            PixelType.UnsignedByte,
            atlas
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

        // =========================
        // QUAD
        // =========================

        float[] vertices =
        {
            100f, 100f, 0f, 0f,
            100f + _cellSize, 100f, 1f, 0f,
            100f + _cellSize, 100f + _cellSize, 1f, 1f,

            100f + _cellSize, 100f + _cellSize, 1f, 1f,
            100f, 100f + _cellSize, 0f, 1f,
            100f, 100f, 0f, 0f
        };

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        GL.BindVertexArray(_vao);

        GL.BindBuffer(
            BufferTarget.ArrayBuffer,
            _vbo
        );

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            vertices.Length * sizeof(float),
            vertices,
            BufferUsageHint.StaticDraw
        );

        GL.VertexAttribPointer(
            0,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            0
        );

        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            2 * sizeof(float)
        );

        GL.EnableVertexAttribArray(1);
    }

    public void Draw()
    {
        GL.BindVertexArray(_vao);

        GL.ActiveTexture(TextureUnit.Texture0);

        GL.BindTexture(
            TextureTarget.Texture2D,
            _texture
        );

        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            6
        );
    }
}