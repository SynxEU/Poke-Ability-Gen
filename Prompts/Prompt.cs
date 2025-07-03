using RandomAbilityGenerator.Json;
using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Models.Preset;
using Spectre.Console;

namespace RandomAbilityGenerator.Prompts;

/// <summary>
/// Provides console-based user prompts for interacting with presets and abilities.
/// Handles creation, selection, and display of presets and abilities using Spectre.Console.
/// </summary>
public static class Prompt
{
    /// <summary>
    /// Prompts the user to create a new preset by entering its name and a list of banned abilities.
    /// Saves the new preset to the presets JSON file.
    /// </summary>
    /// <param name="allAbilities">The complete list of available abilities.</param>
    public static void PromptCreatePreset(List<AbilityEntity> allAbilities)
    {
        string name = AnsiConsole.Ask<string>("[green]Enter a name for the new preset:[/]");
        string input = AnsiConsole.Ask<string>("[yellow]Enter banned abilities separated by commas:[/]");

        List<string> bannedNames = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(n => n.ToLower()).ToList();

        List<BannedAbilities> bannedEntities = allAbilities
            .Where(a => bannedNames.Contains(a.Name.ToLower()))
            .Select(a => new BannedAbilities { AbilityId = a.Id, Ability = a })
            .ToList();

        PresetEntity newPreset = new PresetEntity { Name = name, Abilities = bannedEntities };

        List<PresetEntity> presets = JsonReader.LoadPresets(allAbilities);
        presets.Add(newPreset);
        JsonReader.SavePresets(presets);

        AnsiConsole.MarkupLine($"[green]Preset '{name}' saved.[/]");
        AnsiConsole.MarkupLine("[grey]Press Enter to continue...[/]");
        Console.ReadLine();
    }

    /// <summary>
    /// Prompts the user to select a preset from the list of loaded presets.
    /// </summary>
    /// <param name="allAbilities">The complete list of available abilities.</param>
    /// <returns>The selected <see cref="PresetEntity"/> or null if no presets exist.</returns>
    public static PresetEntity? PromptSelectPreset(List<AbilityEntity> allAbilities)
    {
        List<PresetEntity> presets = JsonReader.LoadPresets(allAbilities);
        if (!presets.Any())
        {
            AnsiConsole.MarkupLine("[red]No presets found.[/]");
            return null;
        }

        return AnsiConsole.Prompt(
            new SelectionPrompt<PresetEntity>()
                .Title("[yellow]Select a preset:[/]")
                .AddChoices(presets)
                .UseConverter(p => p.Name));
    }

    /// <summary>
    /// Asks the user a yes/no question and returns the answer as a boolean.
    /// </summary>
    /// <param name="question">The yes/no question to ask.</param>
    /// <param name="defaultAnswer">The default answer.</param>
    /// <returns>True if the user answers yes; otherwise, false.</returns>
    public static bool AskYesNo(string question, bool defaultAnswer = true)
    {
        return AnsiConsole.Confirm(question, defaultAnswer);
    }

    /// <summary>
    /// Prompts the user to enter the number of abilities they want to roll.
    /// </summary>
    /// <returns>The number of abilities to roll.</returns>
    public static int PromptRollCount()
    {
        return AnsiConsole.Ask<int>("[green]How many abilities would you like to roll?[/]");
    }

    /// <summary>
    /// Displays the list of chosen abilities with their details to the console.
    /// </summary>
    /// <param name="chosen">The list of abilities to display.</param>
    public static void ShowAbilities(List<AbilityEntity> chosen)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold green]Rolled Abilities[/]").RuleStyle("grey").Centered());

        foreach (AbilityEntity ability in chosen)
        {
            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine($"[blue]{ability.Name}[/]");
            AnsiConsole.MarkupLine($"   [grey]Description: {ability.Desc}[/]");
            AnsiConsole.MarkupLine($"   [grey]Released in generation: {ability.GenerationNumber} ({ability.GenerationName})[/]");
            AnsiConsole.MarkupLine("");
        }
    }

    /// <summary>
    /// Displays a notification message and waits for the user to press Enter.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void Notify(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
        AnsiConsole.MarkupLine("[grey]Press Enter to continue...[/]");
        Console.ReadLine();
    }
}
