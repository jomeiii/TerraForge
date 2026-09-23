using OpenTK.Graphics.OpenGL4;
using TerraForge.Graphics;
using TerraForge.World.Chunk;

namespace TerraForge.World;

public class WorldRenderer
{
    private readonly Camera.Camera _camera;
    private readonly Shader _shader;
    
    private readonly ChunkRenderer _chunkRenderer;
    
    public WorldRenderer(Camera.Camera camera)
    {
        _camera = camera;
        _shader = new Shader(
            "shader.vert",
            "shader.frag"
        );

        _chunkRenderer = new ChunkRenderer();
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    public void Draw(World world)
    {
        _shader.Use();
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        
        foreach (Chunk.Chunk chunk in world.Chunks)
        {
            _chunkRenderer.Draw(chunk);
        }
    }
}