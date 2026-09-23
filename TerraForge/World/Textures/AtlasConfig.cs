using OpenTK.Mathematics;

namespace TerraForge.World.Textures;

public class AtlasConfig
{
    public string Atlas { get; set; } = "";
    public int TileSize { get; set; }
    public int Columns { get; set; }
    public int Rows { get; set; }

    public Dictionary<BlockType, BlockTextureConfig> Blocks { get; set; } = new();

    public Vector4 GetUV(int tileX, int tileY)
    {
        float uMin = (float)tileX / Columns;
        float vMin = (float)tileY / Rows;

        float uMax = uMin + 1.0f / Columns;
        float vMax = vMin + 1.0f / Rows;

        return new Vector4(uMin, vMin, uMax, vMax);
    }
}