using System.Numerics;
using TerraForge.World.Textures;
using TerraForge.World.Textures.Atlas;
using TerraForge.World.Textures.TextureFace;

namespace TerraForge.World.Chunk;

public static class ChunkMeshCreator
{
    private static int FloatsPerVertex = 16;

    public static ChunkMesh CreateChunkMesh(Chunk chunk, AtlasConfig atlas)
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
                        AddLeftFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Left), atlas);
                    }

                    // Right
                    if (x == Chunk.Size - 1 || chunk.Blocks[x + 1, y, z].Type == BlockType.Air)
                    {
                        AddRightFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Right), atlas);
                    }

                    // Bottom
                    if (y == 0 || chunk.Blocks[x, y - 1, z].Type == BlockType.Air)
                    {
                        AddBottomFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Bottom), atlas);
                    }

                    // Top
                    if (y == Chunk.Size - 1 || chunk.Blocks[x, y + 1, z].Type == BlockType.Air)
                    {
                        AddTopFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Top), atlas);
                    }

                    // Front
                    if (z == 0 || chunk.Blocks[x, y, z - 1].Type == BlockType.Air)
                    {
                        AddFrontFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Front), atlas);
                    }

                    // Back
                    if (z == Chunk.Size - 1 || chunk.Blocks[x, y, z + 1].Type == BlockType.Air)
                    {
                        AddBackFace(vertices, indexes, x, y, z,
                            GetMaterial(atlas, block.Type, BlockFace.Back), atlas);
                    }
                }
            }
        }

        return new ChunkMesh(vertices.ToArray(), indexes.ToArray());
    }

    private static FaceTextureConfig GetMaterial(AtlasConfig atlas, BlockType blockType, BlockFace face)
    {
        BlockTextureConfig block = atlas.Blocks[blockType];

        FaceTextureConfig? material = face switch
        {
            BlockFace.Top => block.Top,
            BlockFace.Bottom => block.Bottom,
            _ => block.Side
        };

        return material ?? block.All
            ?? throw new InvalidOperationException(
                $"No texture configuration for {blockType} face {face}.");
    }

    private static void AddVertex(List<float> vertices, float x, float y, float z, float u, float v,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        vertices.AddRange([x, y, z, u, v]);

        AddUV(vertices, texture.GetBaseUV(atlas));
        AddUV(vertices, texture.GetOverlayUV(atlas));
        AddColor(vertices, texture.GetColorUV());
    }

    private static void AddUV(List<float> vertices, Vector4? uv)
    {
        Vector4 value = uv ?? Vector4.Zero;
        vertices.AddRange([value.X, value.Y, value.Z, value.W]);
    }

    private static void AddColor(List<float> vertices, Vector3 color)
    {
        vertices.AddRange([color.X, color.Y, color.Z]);
    }

    private static void AddQuadIndexes(List<uint> indexes, uint startIndex)
    {
        indexes.AddRange([
            startIndex,
            startIndex + 1,
            startIndex + 2,

            startIndex + 2,
            startIndex + 3,
            startIndex
        ]);
    }

    private static void AddLeftFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x - 0.5f, y + 1, z - 0.5f, 0, 1, texture, atlas);
        AddVertex(vertices, x - 0.5f, y + 1, z + 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x - 0.5f, y, z + 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x - 0.5f, y, z - 0.5f, 0, 0, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }

    private static void AddRightFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x + 0.5f, y + 1, z + 0.5f, 0, 1, texture, atlas);
        AddVertex(vertices, x + 0.5f, y + 1, z - 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z - 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z + 0.5f, 0, 0, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }

    private static void AddBottomFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x - 0.5f, y, z - 0.5f, 0, 0, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z - 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z + 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x - 0.5f, y, z + 0.5f, 0, 1, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }

    private static void AddTopFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x - 0.5f, y + 1, z - 0.5f, 0, 0, texture, atlas);
        AddVertex(vertices, x - 0.5f, y + 1, z + 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x + 0.5f, y + 1, z + 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x + 0.5f, y + 1, z - 0.5f, 0, 1, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }

    private static void AddFrontFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x - 0.5f, y + 1, z - 0.5f, 0, 1, texture, atlas);
        AddVertex(vertices, x + 0.5f, y + 1, z - 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z - 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x - 0.5f, y, z - 0.5f, 0, 0, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }

    private static void AddBackFace(List<float> vertices, List<uint> indexes, int x, int y, int z,
        FaceTextureConfig texture, AtlasConfig atlas)
    {
        uint startIndex = (uint)(vertices.Count / FloatsPerVertex);

        AddVertex(vertices, x + 0.5f, y + 1, z + 0.5f, 0, 1, texture, atlas);
        AddVertex(vertices, x - 0.5f, y + 1, z + 0.5f, 1, 1, texture, atlas);
        AddVertex(vertices, x - 0.5f, y, z + 0.5f, 1, 0, texture, atlas);
        AddVertex(vertices, x + 0.5f, y, z + 0.5f, 0, 0, texture, atlas);

        AddQuadIndexes(indexes, startIndex);
    }
}