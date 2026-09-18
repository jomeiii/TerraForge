using OpenTK.Mathematics;
using TerraForge.Input;

namespace TerraForge.Player;

public class PlayerController
{
    private Keyboard _keyboard;
    private Player _player;

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

        if (_keyboard.IsJumped) _player.Jump();
        
        if (movement.LengthSquared > 0)
            movement = movement.Normalized();

        movement *= (float)(_player.Speed * deltaTime);
        _player.Movement = movement;
        
        _player.Update();
    }
}