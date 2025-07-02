using RandomAbilityGenerator.Models;

namespace RandomAbilityGenerator.Models;

/// <summary>
/// Represents an ability.
/// Includes metadata such as generation information and description.
/// </summary>
public class AbilityEntity
{
    /// <summary>
    /// The unique identifier of the ability.
    /// </summary>
    public int Id { get; set; } = 0;

    /// <summary>
    /// The name of the ability.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The generation number in which the ability was introduced.
    /// </summary>
    public int GenerationNumber { get; set; } = 3;

    /// <summary>
    /// The name of the generation in which the ability was introduced.
    /// </summary>
    public string GenerationName { get; set; } = string.Empty;

    /// <summary>
    /// A textual description of what the ability does.
    /// </summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>
    /// A list of presets that have banned this ability.
    /// </summary>
    public List<BannedAbilities>? BannedAbilities { get; set; }
}