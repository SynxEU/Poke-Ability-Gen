using System.Text.Json;
using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Json;

public static class JsonReader
{
    public static List<AbilityEntity> LoadAbilities(string filePath)
    {
        try
        {
            string rawJson = File.ReadAllText(filePath);
            using JsonDocument doc = JsonDocument.Parse(rawJson);

            return doc.RootElement.EnumerateArray()
                .Select(elem => new AbilityEntity
                {
                    Id = elem.TryGetProperty("id", out var idProp) 
                         && idProp.TryGetInt32(out int id) 
                        ? id : 0,
                    Name = elem.TryGetProperty("name", out var nameProp) 
                        ? nameProp.GetString() 
                          ?? string.Empty : string.Empty,
                    Generation = elem.TryGetProperty("gen", out var genProp) 
                                 && genProp.TryGetInt32(out int gen) 
                        ? gen : 0,
                    Desc = elem.TryGetProperty("desc", out var descProp) 
                        ? descProp.GetString() 
                          ?? string.Empty : string.Empty,
                })
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load abilities: {ex.Message}");
            return new List<AbilityEntity>();
        }
    }
}