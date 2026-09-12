using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.World.Blocks;

public class Stone : Cube
{
    private readonly Texture _texture;
    
    public Stone(Mesh mesh, Texture texture, Vector3 position) : base(mesh, position)
    {
        _texture = texture;
    }

    public override void Draw(Shader shader)
    {
        shader.SetBool("useBlockColor", false);
        _texture.Use();
        base.Draw(shader);
    }
}