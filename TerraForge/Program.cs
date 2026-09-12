using System.Data;

namespace TerraForge;

public class Program
{
    static void Main(string[] args)
    {
        using (Game game = new Game(1280, 720, "TerraForge"))
        {
            game.Run();
        }
    }
}