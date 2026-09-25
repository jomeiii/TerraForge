using System.Numerics;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World.Textures.TextureFace;

public class FaceTextureConfig
{
    public int[]? Base { get; set; }
    public int[]? Overlay { get; set; }
    public float[]? Color { get; set; }

    public Vector4? GetBaseUV(AtlasConfig atlas)
    {
        return Base == null ? null : (Vector4?)atlas.GetUV(Base[0], Base[1]);
    }

    public Vector4? GetOverlayUV(AtlasConfig atlas)
    {
        return Overlay == null ? null : (Vector4?)atlas.GetUV(Overlay[0], Overlay[1]);
    }

    public Vector3 GetColorUV()
    {
        return Color == null ? Vector3.Zero : new Vector3(Color[0], Color[1], Color[2]);
    }
}