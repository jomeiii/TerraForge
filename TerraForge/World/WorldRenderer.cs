using TerraForge.Graphics;

namespace TerraForge.World;

public class WorldRenderer
{
    private readonly Camera.Camera _camera;
    private readonly Shader _shader;
    
    public WorldRenderer(Camera.Camera camera, Shader shader)
    {
        _camera = camera;
        _shader = shader;
    }

    public void Draw(World world)
    {
        _shader.Use();
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        world.Draw(_shader);
    }
}