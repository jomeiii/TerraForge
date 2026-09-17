using OpenTK.Mathematics;
using TerraForge.World.Blocks;

namespace TerraForge.World;

public class World
{
    private readonly Cube[] _cubes;

    public Cube[] Cubes => _cubes;

    public World(WorldReference worldReference)
    {
        _cubes = new Cube[2];

        Stone stone = new Stone(
            worldReference.CubeMesh,
            worldReference.StoneTexture,
            new Vector3(3, -1, 3)
        );

        Grass grass = new Grass(
            worldReference.CubeMesh,
            worldReference.TopDirtTexture,
            worldReference.SideTexture,
            worldReference.BottomDirtTexture,
            worldReference.SideOverlayTexture,
            new Vector3(0, -1, 2),
            worldReference.GrassColorMap
        );

        _cubes[0] = stone;
        _cubes[1] = grass;
    }
}