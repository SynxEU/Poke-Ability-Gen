using System.Text.Json;
using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Models.Preset;

namespace RandomAbilityGenerator.Json;

/// <summary>
/// Provides functionality to load and save abilities and presets from/to JSON files.
/// Handles deserialization of ability data and preset configurations, with error handling.
/// </summary>
public static class JsonReader
{
    private const string PresetFilePath = "Json/presets.json";
    
    /// <summary>
    /// Loads a list of abilities from a JSON file at the specified path.
    /// Parses ability properties such as Id, Name, GenerationNumber, GenerationName, and Description.
    /// Returns an empty list if the file cannot be read or parsed.
    /// </summary>
    /// <param name="filePath">The path to the JSON file containing ability data.</param>
    /// <returns>A list of <see cref="AbilityEntity"/> objects parsed from the JSON file.</returns>
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

    /// <summary>
    /// Loads presets from a predefined JSON file path.
    /// Maps banned ability names from the preset JSON to actual <see cref="AbilityEntity"/> instances.
    /// Returns an empty list if the file does not exist or deserialization fails.
    /// </summary>
    /// <param name="allAbilities">The complete list of all available abilities.</param>
    /// <returns>A list of <see cref="PresetEntity"/> objects with associated banned abilities.</returns>
    public static List<PresetEntity> LoadPresets(List<AbilityEntity> allAbilities)
    {
        try
        {
            if (!File.Exists(PresetFilePath))
                return new();

            string json = File.ReadAllText(PresetFilePath);
            FlattenPreset dto = JsonSerializer.Deserialize<FlattenPreset>(json);
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
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load presets: {ex.Message}");
            return new List<PresetEntity>();
        }
    }

    /// <summary>
    /// Saves the given list of presets to the predefined JSON file path.
    /// Serializes presets by flattening banned abilities to their names.
    /// Throws an exception if saving fails.
    /// </summary>
    /// <param name="presets">The list of presets to save.</param>
    public static void SavePresets(List<PresetEntity> presets)
    {
        try
        {
            List<FlattenPreset> dtos = presets.Select(p => new FlattenPreset()
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
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save presets: {ex.Message}");
            throw;
        }
    }
}