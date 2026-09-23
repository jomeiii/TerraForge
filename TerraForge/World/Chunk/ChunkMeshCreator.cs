namespace TerraForge.World.Chunk;

public static class ChunkMeshCreator
{
    public static ChunkMesh CreateChunkMesh(Chunk chunk)
    {
        List<float> vertices = new();
        List<uint> indexes = new();

        for (int x = 0; x < Chunk.Size; x++)
        {
            for (int y = 0; y < Chunk.Size; y++)
            {
                for (int z = 0; z < Chunk.Size; z++)
                {
                    var block = chunk.Blocks[x, y, z];

                    // Air blocks don't have any faces
                    if (block.Type == BlockType.Air)
                    {
                        continue;
                    }

                    // Left
                    if (x == 0 || chunk.Blocks[x - 1, y, z].Type == BlockType.Air)
                    {
                        AddLeftFace(vertices, indexes, x, y, z);
                    }

                    // Right
                    if (x == Chunk.Size - 1 || chunk.Blocks[x + 1, y, z].Type == BlockType.Air)
                    {
                        AddRightFace(vertices, indexes, x, y, z);
                    }

                    // Bottom
                    if (y == 0 || chunk.Blocks[x, y - 1, z].Type == BlockType.Air)
                    {
                        AddBottomFace(vertices, indexes, x, y, z);
                    }

                    // Top
                    if (y == Chunk.Size - 1 || chunk.Blocks[x, y + 1, z].Type == BlockType.Air)
                    {
                        AddTopFace(vertices, indexes, x, y, z);
                    }

                    // Front
                    if (z == 0 || chunk.Blocks[x, y, z - 1].Type == BlockType.Air)
                    {
                        AddFrontFace(vertices, indexes, x, y, z);
                    }

                    // Back
                    if (z == Chunk.Size - 1 || chunk.Blocks[x, y, z + 1].Type == BlockType.Air)
                    {
                        AddBackFace(vertices, indexes, x, y, z);
                    }
                }
            }
        }

        return new ChunkMesh(vertices.ToArray(), indexes.ToArray());
    }

    private static void AddLeftFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x - 0.5f, y + 1, z - 0.5f, 0, 0,
            x - 0.5f, y + 1, z + 0.5f, 1, 0,
            x - 0.5f, y,     z + 0.5f, 1, 1,
            x - 0.5f, y,     z - 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddRightFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x + 0.5f, y + 1, z + 0.5f, 0, 0,
            x + 0.5f, y + 1, z - 0.5f, 1, 0,
            x + 0.5f, y,     z - 0.5f, 1, 1,
            x + 0.5f, y,     z + 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddBottomFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x - 0.5f, y, z - 0.5f, 0, 0,
            x + 0.5f, y, z - 0.5f, 1, 0,
            x + 0.5f, y, z + 0.5f, 1, 1,
            x - 0.5f, y, z + 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddTopFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x - 0.5f, y + 1, z - 0.5f, 0, 0,
            x - 0.5f, y + 1, z + 0.5f, 1, 0,
            x + 0.5f, y + 1, z + 0.5f, 1, 1,
            x + 0.5f, y + 1, z - 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddFrontFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x - 0.5f, y + 1, z - 0.5f, 0, 0,
            x + 0.5f, y + 1, z - 0.5f, 1, 0,
            x + 0.5f, y,     z - 0.5f, 1, 1,
            x - 0.5f, y,     z - 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddBackFace(List<float> vertices, List<uint> indexes, int x, int y, int z)
    {
        uint startIndex = (uint)(vertices.Count / 5);

        vertices.AddRange([
            x + 0.5f, y + 1, z + 0.5f, 0, 0,
            x - 0.5f, y + 1, z + 0.5f, 1, 0,
            x - 0.5f, y,     z + 0.5f, 1, 1,
            x + 0.5f, y,     z + 0.5f, 0, 1
        ]);

        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }
}