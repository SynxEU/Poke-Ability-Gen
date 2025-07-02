using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Models;

public class BannedAbilities
{
    public int PresetId { get; set; }
    public int AbilityId { get; set; }
    
    public PresetEntity Preset { get; set; } = new PresetEntity();
    public AbilityEntity Ability { get; set; } = new AbilityEntity();
}