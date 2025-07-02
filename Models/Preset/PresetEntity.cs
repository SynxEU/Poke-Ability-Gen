namespace RandomAbilityGenerator.Models.Preset;

/// <summary>
/// Represents a preset configuration that defines a group of banned abilities.
/// </summary>
public class PresetEntity
{
    /// <summary>
    /// The unique identifier for the preset.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the preset.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The list of abilities that are banned by this preset.
    /// </summary>
    public List<BannedAbilities> Abilities { get; set; } = new List<BannedAbilities>();
}