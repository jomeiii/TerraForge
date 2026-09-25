using TerraForge.Graphics;

namespace TerraForge.World.Textures.Atlas;

public class TextureAtlas
{
    public Texture Texture { get; }
    public AtlasConfig Config { get; }

    public TextureAtlas(Texture texture, AtlasConfig config)
    {
        Texture = texture;
        Config = config;
    }
}