using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TerraForge.Input;

public class Keyboard
{
    public Vector2 Movement { get; private set; }
    public bool IsJumped { get; private set; }
    
    private bool _wasSpaceDown;
    
    public Keyboard()
    {
        Movement = new Vector2(0, 0);
    }

    public void Update(KeyboardState input)
    {
        Vector2 movement = Vector2.Zero;

        bool spaceDown = input.IsKeyDown(Keys.Space);
        IsJumped = spaceDown && !_wasSpaceDown;
        _wasSpaceDown = spaceDown;

        if (!input.IsKeyDown(Keys.Space))
            IsJumped = false;

        if (input.IsKeyDown(Keys.W))
            movement.Y += 1;

        if (input.IsKeyDown(Keys.S))
            movement.Y -= 1;

        if (input.IsKeyDown(Keys.A))
            movement.X -= 1;

        if (input.IsKeyDown(Keys.D))
            movement.X += 1;

        if (movement.LengthSquared > 0)
            movement = movement.Normalized();

        Movement = movement;
    }
}