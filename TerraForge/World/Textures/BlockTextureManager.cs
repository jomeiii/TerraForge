using TerraForge.Graphics;

namespace TerraForge.World.Textures;

public static class BlockTextureManager
{
    private static Texture _grassTop;
    private static Texture _grassSide;
    private static Texture _dirt;
    private static Texture _stone;

    static BlockTextureManager()
    {
        _grassTop = new Texture("Resources/Textures/grass_top.png");
        _grassSide = new Texture("Resources/Textures/grass_side.png");
        _dirt = new Texture("Resources/Textures/dirt.png");
        _stone = new Texture("Resources/Textures/stone.png");
    }
}