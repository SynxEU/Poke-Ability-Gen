using RandomAbilityGenerator.Models;

namespace RandomAbilityGenerator.Ability;

public class AbilityEntity
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public int GenerationNumber { get; set; } = 3;
    public string GenerationName { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    
    public List<BannedAbilities>? BannedAbilities { get; set; }
}