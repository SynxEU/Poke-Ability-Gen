using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Models.Preset;

namespace RandomAbilityGenerator.Service;

/// <summary>
/// Provides core functionality for working with abilities, including filtering banned abilities
/// based on a preset and selecting a random subset of unique abilities.
/// </summary>
public static class AbilityGenerator
{   
    /// <summary>
    /// Filters out abilities that are banned in the specified preset.
    /// Returns a list of abilities excluding those banned by the preset.
    /// </summary>
    /// <param name="allAbilities">The full list of abilities to filter from.</param>
    /// <param name="preset">The preset containing banned abilities, or null to apply no filter.</param>
    /// <returns>A filtered list of <see cref="AbilityEntity"/> excluding banned abilities.</returns>
    public static List<AbilityEntity> GetFilteredAbilities(List<AbilityEntity> allAbilities, PresetEntity? preset)
    {
        HashSet<string?> banned = preset?.Abilities.Select(b => b.Ability?.Name.ToLower()).ToHashSet() ?? new();

        return allAbilities
            .Where(a => !banned.Contains(a.Name.ToLower()))
            .ToList();
    }
    
    /// <summary>
    /// Randomly selects a specified number of unique abilities from the provided list.
    /// Ensures no duplicate ability names are selected.
    /// </summary>
    /// <param name="abilities">The list of abilities to select from.</param>
    /// <param name="count">The number of abilities to randomly pick.</param>
    /// <param name="rand">An instance of <see cref="Random"/> used for randomness.</param>
    /// <returns>A list of randomly selected unique <see cref="AbilityEntity"/> objects.</returns>
    public static List<AbilityEntity> PickRandomAbilities(List<AbilityEntity> abilities, int count, Random rand)
    {
        return abilities
            .GroupBy(a => a.Name.ToLower())
            .Select(g => g.First())
            .OrderBy(_ => rand.Next())
            .Take(count)
            .ToList();
    }
}