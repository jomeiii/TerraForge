namespace TerraForge.UI;

public class FpsCounter
{
    private double _timer;
    private int _frames;

    public double FPS { get; private set; }

    public void Update(double deltaTime)
    {
        _timer += deltaTime;
        _frames++;

        if (_timer >= 1.0)
        {
            FPS = _frames / _timer;

            _timer = 0;
            _frames = 0;
        }

        Console.WriteLine($"FPS: {FPS}");
    }
}