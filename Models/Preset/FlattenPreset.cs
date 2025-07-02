namespace RandomAbilityGenerator.Models.Preset;

/// <summary>
/// A simplified data transfer object (DTO) used for serializing and deserializing preset data.
/// Stores the preset name and a flat list of banned ability names.
/// </summary>
public class FlattenPreset
{
    /// <summary>
    /// The name of the preset.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A list of banned ability names (as strings) associated with the preset.
    /// </summary>
    public List<string> BannedAbilityNames { get; set; } = new();
}