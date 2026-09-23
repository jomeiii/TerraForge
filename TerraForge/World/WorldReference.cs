using TerraForge.Graphics;
using TerraForge.World.Textures;

namespace TerraForge.World;

public class WorldReference
{
    public Mesh CubeMesh { get; }
    public TextureAtlas BlockAtlas { get; }


    public WorldReference()
    {
        Console.WriteLine("WorldReference created");

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