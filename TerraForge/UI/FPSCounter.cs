namespace TerraForge.UI;

public class FpsCounter
{
    private double _timer;
    private int _frames;

    public float FPS
    {
        get => double.ConvertToInteger<int>(_frames / _timer);
    }

    public void Update(double deltaTime)
    {
        _timer += deltaTime;
        _frames++;

        if (_timer >= 1.0)
        {
            _timer = 0;
            _frames = 0;
        }
    }
}