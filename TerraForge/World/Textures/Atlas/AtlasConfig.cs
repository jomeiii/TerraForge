using OpenTK.Mathematics;

namespace TerraForge.World.Textures.Atlas;

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
        float uMax = (float)(tileX + 1) / Columns;

        float vMin = 1.0f - (float)(tileY + 1) / Rows;
        float vMax = 1.0f - (float)(tileY) / Rows;

        return new Vector4(uMin, vMin, uMax, vMax);
    }
}