using System.Text.Json;
using System.Text.Json.Serialization;

namespace TerraForge.World.Textures.Atlas;

public static class AtlasLoader
{
    public static AtlasConfig Load(string path)
    {
        string json = File.ReadAllText(path);

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<AtlasConfig>(
            json,
            options
        ) ?? throw new Exception("Failed to load atlas config.");
    }
}