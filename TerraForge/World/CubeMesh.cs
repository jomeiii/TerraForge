using TerraForge.Graphics;

namespace TerraForge.World;

public static class CubeMesh
{
    public static Mesh Create()
    {
        float[] vertices =
        {
            // FRONT: A B B1 A1
            // 0
            -0.5f, -0.5f,  0.5f,  0f, 0f, // A
            // 1
            0.5f, -0.5f,  0.5f,  1f, 0f, // B
            // 2
            0.5f,  0.5f,  0.5f,  1f, 1f, // B1
            // 3
            -0.5f,  0.5f,  0.5f,  0f, 1f, // A1


            // BACK: D C C1 D1
            // 4
            -0.5f, -0.5f, -0.5f,  0f, 0f, // D
            // 5
            0.5f, -0.5f, -0.5f,  1f, 0f, // C
            // 6
            0.5f,  0.5f, -0.5f,  1f, 1f, // C1
            // 7
            -0.5f,  0.5f, -0.5f,  0f, 1f, // D1


            // LEFT: D A A1 D1
            // 8
            -0.5f, -0.5f, -0.5f,  0f, 0f, // D
            // 9
            -0.5f, -0.5f,  0.5f,  1f, 0f, // A
            // 10
            -0.5f,  0.5f,  0.5f,  1f, 1f, // A1
            // 11
            -0.5f,  0.5f, -0.5f,  0f, 1f, // D1


            // RIGHT: B C C1 B1
            // 12
            0.5f, -0.5f,  0.5f,  0f, 0f, // B
            // 13
            0.5f, -0.5f, -0.5f,  1f, 0f, // C
            // 14
            0.5f,  0.5f, -0.5f,  1f, 1f, // C1
            // 15
            0.5f,  0.5f,  0.5f,  0f, 1f, // B1


            // TOP: A1 B1 C1 D1
            // 16
            -0.5f,  0.5f,  0.5f,  0f, 0f, // A1
            // 17
            0.5f,  0.5f,  0.5f,  1f, 0f, // B1
            // 18
            0.5f,  0.5f, -0.5f,  1f, 1f, // C1
            // 19
            -0.5f,  0.5f, -0.5f,  0f, 1f, // D1


            // BOTTOM: D C B A
            // 20
            -0.5f, -0.5f, -0.5f,  0f, 0f, // D
            // 21
            0.5f, -0.5f, -0.5f,  1f, 0f, // C
            // 22
            0.5f, -0.5f,  0.5f,  1f, 1f, // B
            // 23
            -0.5f, -0.5f,  0.5f,  0f, 1f  // A
        };

        uint[] indices =
        {
            // FRONT
            0, 1, 2,
            0, 2, 3,

            // BACK
            4, 5, 6,
            4, 6, 7,

            // LEFT
            8, 9, 10,
            8, 10, 11,

            // RIGHT
            12, 13, 14,
            12, 14, 15,

            // TOP
            16, 17, 18,
            16, 18, 19,

            // BOTTOM
            20, 21, 22,
            20, 22, 23
        };

        return new Mesh(vertices, indices);
    }
}