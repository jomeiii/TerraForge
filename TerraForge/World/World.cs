using FreeTypeSharp;
using OpenTK.Mathematics;
using TerraForge.World.Blocks;

namespace TerraForge.World;

public class World
{
    private readonly Cube[] _cubes;

    public Cube[] Cubes => _cubes;

    public World(WorldReference worldReference)
    {
        _cubes = new Cube[10 * 10 + 3];
        int count = 0;
        
        for (int x = -5; x < 5; x++)
        {
            for (int z = -5; z < 5; z++)
            {
                Grass grass = new Grass(
                    worldReference.CubeMesh,
                    worldReference.TopDirtTexture,
                    worldReference.SideTexture,
                    worldReference.BottomDirtTexture,
                    worldReference.SideOverlayTexture,
                    new Vector3(x, -1, z),
                    worldReference.GrassColorMap
                );
                
                _cubes[count] = grass;
                count++;
            }
        }

        for (int x = 2; x < 5; x++)
        {
            Stone stone = new Stone(worldReference.CubeMesh,
                worldReference.StoneTexture,
                new Vector3(x, 0, 0));
            
            _cubes[count] = stone;
            count++;
        }
    }
}