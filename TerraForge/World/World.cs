using OpenTK.Mathematics;
using TerraForge.Graphics;
using TerraForge.World.Blocks;

namespace TerraForge.World;

public class World
{
    private readonly Cube[] _cubes;
    private readonly Shader _shader;

    public World(Mesh mesh, Texture texture, Texture sideTexture, Texture bottonTexture, Texture topTexture,
        Texture sideOverlay ,Shader shader, Colormap colormap)
    {
        _shader = shader;

        _cubes = new Cube[2];
        Stone stone = new Stone(mesh, texture, new Vector3(3, -1, 3));
        
        Grass grass = new Grass(mesh, topTexture, sideTexture, bottonTexture,sideOverlay, new Vector3(0, -1, 2), colormap);
        
        _cubes[0] = stone;
        _cubes[1] = grass;
    }

    public void Draw(Shader shader)
    {
        foreach (Cube cube in _cubes)
        {
            shader.SetMatrix4(
                "model",
                cube.GetModelMatrix()
            );

            cube.Draw(_shader);
        }
    }
}