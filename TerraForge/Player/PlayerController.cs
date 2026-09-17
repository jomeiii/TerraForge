using OpenTK.Mathematics;
using TerraForge.Input;

namespace TerraForge.Player;

public class PlayerController
{
    private Keyboard _keyboard;
    private Player _player;

    private float _speed = 4;

    public PlayerController(Keyboard keyboard, Player player)
    {
        _keyboard = keyboard;
        _player = player;
    }

    public void Update(double deltaTime)
    {
        var front = new Vector3(_player.Camera.Front.X, 0, _player.Camera.Front.Z).Normalized();
        var right = Vector3.Cross(front, _player.Camera.Up).Normalized();
        Vector3 movement =
            front * _keyboard.Movement.Y +
            right * _keyboard.Movement.X;

        movement.Y = _keyboard.VerticalMovement;

        if (movement.LengthSquared > 0)
            movement = movement.Normalized();

        _player.Position += movement * _speed * (float)deltaTime;
        
        _player.Update();
    }
}