using TerraForge.Graphics;

namespace TerraForge.World;

public class WorldReference
{
    public Mesh CubeMesh { get; }
    public Texture StoneTexture { get; }
    public Texture SideTexture { get; }
    public Texture BottomDirtTexture { get; }
    public Texture TopDirtTexture { get; }
    public Texture SideOverlayTexture { get; }
    public Colormap GrassColorMap { get; }

    public WorldReference()
    {
        GrassColorMap = new Colormap(
            "Resources/minecraft/textures/colormap/grass.png"
        );

        SideTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_side.png"
        );

        SideOverlayTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_side_overlay.png"
        );

        BottomDirtTexture = new Texture(
            "Resources/minecraft/textures/block/dirt.png"
        );

        TopDirtTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_top.png"
        );

        StoneTexture = new Texture(
            "Resources/minecraft/textures/block/cobblestone.png"
        );

        CubeMesh = TerraForge.World.CubeMesh.Create();
    }

    public void Dispose()
    {
        CubeMesh.Dispose();
        StoneTexture.Dispose();
        SideTexture.Dispose();
        BottomDirtTexture.Dispose();
        TopDirtTexture.Dispose();
        SideOverlayTexture.Dispose();
        BottomDirtTexture.Dispose();
    }
}