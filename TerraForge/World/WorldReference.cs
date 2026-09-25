using TerraForge.Graphics;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World;

public class WorldReference
{
    public Mesh CubeMesh { get; }
    public TextureAtlas BlockAtlas { get; }


    public WorldReference()
    {
        BlockAtlas = new TextureAtlas(
            new Texture("Resources/main/blocks.png"),
            AtlasLoader.Load("Resources/main/blocks.json")
        );

        CubeMesh = TerraForge.World.CubeMesh.Create();
    }

    public void Dispose()
    {
        CubeMesh.Dispose();
        BlockAtlas.Texture.Dispose();
    }
}