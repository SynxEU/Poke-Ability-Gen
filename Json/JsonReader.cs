using System.Text.Json;
using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Json;

public static class JsonReader
{
    public static List<AbilityEntity> LoadAbilities(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        string rawJson = File.ReadAllText(filePath);
        using JsonDocument doc = JsonDocument.Parse(rawJson);

        List<AbilityEntity> abilities = doc.RootElement.EnumerateArray()
            .Select(elem => new AbilityEntity
            {
                Id = elem.GetProperty("id").GetInt32(),
                Name = elem.TryGetProperty("name", out JsonElement nameProp)
                    ? nameProp.GetString() ?? string.Empty
                    : string.Empty,
                Generation = elem.GetProperty("gen").GetInt32(),
                Desc = elem.TryGetProperty("desc", out JsonElement descProp)
                    ? descProp.GetString() ?? string.Empty
                    : string.Empty,
            })
            .ToList();

        return abilities;
    }
}