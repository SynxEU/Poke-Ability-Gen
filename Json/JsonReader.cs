using System.Text.Json;
using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Models.Preset;

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
                    GenerationNumber = elem.TryGetProperty("genNumber", out var genNumProp) 
                                 && genNumProp.TryGetInt32(out int genNumber) 
                        ? genNumber : 0,
                    GenerationName = elem.TryGetProperty("genName", out var genNameProp) 
                        ? genNameProp.GetString() 
                          ?? string.Empty : string.Empty,
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

    private const string PresetFilePath = "Json/presets.json";

    public static List<PresetEntity> LoadPresets(List<AbilityEntity> allAbilities)
    {
        if (!File.Exists(PresetFilePath))
            return new();

        string json = File.ReadAllText(PresetFilePath);
        var dto = JsonSerializer.Deserialize<FlattenPreset>(json);
        if (dto == null) return new();

        return new List<PresetEntity> {
            new PresetEntity
            {
                Name = dto.Name,
                Abilities = dto.BannedAbilityNames
                    .Select(name => allAbilities.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    .Where(a => a != null)
                    .Select(a => new BannedAbilities { Ability = a!, AbilityId = a!.Id })
                    .ToList()
            }
        };

    }


    public static void SavePresets(List<PresetEntity> presets)
    {
        var dtos = presets.Select(p => new FlattenPreset()
        {
            Name = p.Name,
            BannedAbilityNames = p.Abilities
                .Select(b => b.Ability?.Name ?? string.Empty)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList()
        }).ToList();

        string json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(PresetFilePath, json);
    }
}