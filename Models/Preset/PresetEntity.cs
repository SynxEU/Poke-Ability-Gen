using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Models;

public class PresetEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<BannedAbilities> Abilities { get; set; } = new List<BannedAbilities>();
}