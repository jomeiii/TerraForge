using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.World;

public class World
{
    private readonly Cube[] _cubes;

    public World(Mesh mesh, Texture texture)
    {
        _cubes = new Cube[10 * 10 * 10];

        int index = 0;

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    _cubes[index++] = new Cube(
                        mesh,
                        texture,
                        new Vector3(x, y, z)
                    );
                }
            }
        }
    }

    public void Draw(Shader shader)
    {
        foreach (Cube cube in _cubes)
        {
            shader.SetMatrix4(
                "model",
                cube.GetModelMatrix()
            );

            cube.Draw();
        }
    }
}