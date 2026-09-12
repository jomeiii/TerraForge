using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.World;

public class Cube
{
    public Mesh Mesh { get; }

    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }
    
    public Cube(Mesh mesh, Vector3 position)
    {
        Mesh = mesh;
        Position = position;
        Rotation = Vector3.Zero;
        Scale = Vector3.One;
    }

    public Matrix4 GetModelMatrix()
    {
        Matrix4 model =
            Matrix4.CreateScale(Scale);

        model *= Matrix4.CreateRotationX(
            MathHelper.DegreesToRadians(Rotation.X)
        );

        model *= Matrix4.CreateRotationY(
            MathHelper.DegreesToRadians(Rotation.Y)
        );

        model *= Matrix4.CreateRotationZ(
            MathHelper.DegreesToRadians(Rotation.Z)
        );

        model *= Matrix4.CreateTranslation(Position);

        return model;
    }

    public virtual void Draw(Shader shader)
    {
        Mesh.Draw(Mesh.IndexCount, 0);
    }
}