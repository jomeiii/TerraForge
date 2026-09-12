using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace TerraForge.Graphics;

public class Shader : IDisposable
{
    public int Handle { get; }

    private bool _disposed;

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexSource = File.ReadAllText(vertexPath);
        string fragmentSource = File.ReadAllText(fragmentPath);

        int vertexShader = CompileShader(
            ShaderType.VertexShader,
            vertexSource
        );

        int fragmentShader = CompileShader(
            ShaderType.FragmentShader,
            fragmentSource
        );

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        GL.LinkProgram(Handle);

        CheckLinkStatus(Handle);

        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private static int CompileShader(ShaderType type, string source)
    {
        int shader = GL.CreateShader(type);

        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(
            shader,
            ShaderParameter.CompileStatus,
            out int success
        );

        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(shader);
            GL.DeleteShader(shader);

            throw new Exception(
                $"Failed to compile {type} shader:\n{infoLog}"
            );
        }

        return shader;
    }

    private static void CheckLinkStatus(int program)
    {
        GL.GetProgram(
            program,
            GetProgramParameterName.LinkStatus,
            out int success
        );

        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(program);
            throw new Exception(
                $"Failed to link shader program:\n{infoLog}"
            );
        }
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void SetInt(string name, int value)
    {
        int location = GL.GetUniformLocation(Handle, name);

        if (location == -1)
        {
            throw new Exception(
                $"Uniform '{name}' was not found."
            );
        }

        GL.Uniform1(location, value);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        GL.DeleteProgram(Handle);

        _disposed = true;
    }

    ~Shader()
    {
        if (!_disposed)
        {
            Console.WriteLine(
                "GPU Resource leak! Did you forget to call Dispose()?"
            );
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SetMatrix4(string name, Matrix4 matrix)
    {
        GL.UseProgram(Handle);
        int location = GL.GetUniformLocation(Handle, name);
        GL.UniformMatrix4(location, true, ref matrix);
    }
}