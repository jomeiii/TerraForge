using OpenTK.Mathematics;

namespace TerraForge.Physics;

// Axis-Aligned Bounding Box
public struct AABB
{
    public Vector3 Min;
    public Vector3 Max;
    
    public AABB(Vector3 position, Vector3 size)
    {
        Min = new Vector3(
            position.X - size.X / 2,
            position.Y,
            position.Z - size.Z / 2
        );

        Max = new Vector3(
            position.X + size.X / 2,
            position.Y + size.Y,
            position.Z + size.Z / 2
        );
    }

    public bool Intersects(AABB other)
    {
        return Max.X >= other.Min.X && Min.X <= other.Max.X &&
               Max.Y >= other.Min.Y && Min.Y <= other.Max.Y &&
               Max.Z >= other.Min.Z && Min.Z <= other.Max.Z;
    }    
}