using TerraForge.World.Textures.TextureFace;

namespace TerraForge.World.Textures;

public class BlockTextureConfig
{
    public FaceTextureConfig? All { get; set; }
    public FaceTextureConfig? Top { get; set; }
    public FaceTextureConfig? Bottom { get; set; }
    public FaceTextureConfig? Side { get; set; }
    
    public FaceTextureConfig? GetFace(BlockFace face)
    {
        return face switch
        {
            BlockFace.Top => Top ?? All,
            BlockFace.Bottom => Bottom ?? All,
            BlockFace.Left => Side ?? All,
            BlockFace.Right => Side ?? All,
            BlockFace.Front => Side ?? All,
            BlockFace.Back => Side ?? All,
            _ => null
        };
    }
}