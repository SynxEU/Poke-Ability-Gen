using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Json;
using RandomAbilityGenerator.Models;

namespace RandomAbilityGenerator.Service;

public static class AbilityGenerator
{
    public static List<AbilityEntity> GetFilteredAbilities(List<AbilityEntity> allAbilities, PresetEntity? preset)
    {
        var banned = preset?.Abilities.Select(b => b.Ability?.Name.ToLower()).ToHashSet() ?? new();

        return allAbilities
            .Where(a => !banned.Contains(a.Name.ToLower()))
            .ToList();
    }

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