using Spectre.Console;
using RandomAbilityGenerator.Models.Preset;

namespace RandomAbilityGenerator.Service;

/// <summary>
/// Provides utility methods for interacting with and validating ability presets,
/// including user confirmation of banned abilities.
/// </summary>
public class Presets
{
    /// <summary>
    /// Displays the banned abilities for the given preset and asks the user to confirm.
    /// </summary>
    /// <param name="preset">The preset to show.</param>
    /// <returns>True if user confirms, false otherwise.</returns>
    public static bool ConfirmBannedAbilities(PresetEntity preset)
    {
        if (preset == null)
        {
            AnsiConsole.MarkupLine("[red]Preset is null.[/]");
            return false;
        }

        if (preset.Abilities == null || preset.Abilities.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]This preset has no banned abilities.[/]");
        }
        else
        {
            Table table = new Table();
            table.AddColumn("[u]Banned Abilities[/]");

            foreach (var banned in preset.Abilities.Where(b => b.Ability != null))
            {
                table.AddRow(banned.Ability!.Name);
            }

            AnsiConsole.Write(table);
        }

        return AnsiConsole.Confirm("Is this okay?");
    }
}