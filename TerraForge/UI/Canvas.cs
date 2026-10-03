using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.UI;

public abstract class Canvas : IDisposable
{
    protected readonly int _vertexArray;
    protected readonly int _vertexBuffer;

    protected readonly Vector2i _canvasSize;
    protected readonly Vector2i _pivot;
    protected readonly Vector2i _offset;
    
    protected readonly Shader _shader;

    protected Canvas(Shader shader, Vector2i pivot, Vector2i offset)
    {
        _shader = shader;
        
        _pivot = pivot;
        _offset = offset;
        
        _vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArray);

        _vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
    }
    
    public abstract void Render();
    
    public abstract void Dispose();
}