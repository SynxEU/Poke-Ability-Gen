using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Models;

public class BannedAbilities
{
    public int PresetId { get; set; }
    public int AbilityId { get; set; }
    
    public PresetEntity Preset { get; set; }
    public AbilityEntity Ability { get; set; }
}