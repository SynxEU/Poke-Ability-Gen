namespace RandomAbilityGenerator.Models.Preset;

public class FlattenPreset
{
    public string Name { get; set; } = string.Empty;
    public List<string> BannedAbilityNames { get; set; } = new();
}