using OpenTK.Graphics.OpenGL4;
using TerraForge.Graphics;

namespace TerraForge.World;

public class WorldRenderer
{
    private readonly Camera.Camera _camera;
    private readonly Shader _shader;
    
    public WorldRenderer(Camera.Camera camera)
    {
        _camera = camera;
        _shader = new Shader(
            "shader.vert",
            "shader.frag"
        );
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    public void Draw(World world)
    {
        _shader.Use();
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        world.Draw(_shader);
    }
}