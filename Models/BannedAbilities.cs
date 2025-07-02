using RandomAbilityGenerator.Models.Preset;

namespace RandomAbilityGenerator.Models;

/// <summary>
/// Represents a banned ability within a preset. 
/// Links a specific ability to a preset that excludes it.
/// </summary>
public class BannedAbilities
{
    /// <summary>
    /// The ID of the preset that bans the ability.
    /// </summary>
    public int PresetId { get; set; }

    /// <summary>
    /// The ID of the banned ability.
    /// </summary>
    public int AbilityId { get; set; }

    /// <summary>
    /// The preset entity that this ban is associated with.
    /// </summary>
    public PresetEntity Preset { get; set; } = new PresetEntity();

    /// <summary>
    /// The ability entity that is banned.
    /// </summary>
    public AbilityEntity Ability { get; set; } = new AbilityEntity();
}