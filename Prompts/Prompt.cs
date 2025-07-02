using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Json;
using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Service;
using Spectre.Console;

namespace RandomAbilityGenerator.Prompts;

public static class Prompt
{
    public static void PromptCreatePreset(List<AbilityEntity> allAbilities)
    {
        string name = AnsiConsole.Ask<string>("[green]Enter a name for the new preset:[/]");
        string input = AnsiConsole.Ask<string>("[yellow]Enter banned abilities separated by commas:[/]");

        var bannedNames = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(n => n.ToLower()).ToList();

        var bannedEntities = allAbilities
            .Where(a => bannedNames.Contains(a.Name.ToLower()))
            .Select(a => new BannedAbilities { AbilityId = a.Id, Ability = a })
            .ToList();

        var newPreset = new PresetEntity { Name = name, Abilities = bannedEntities };

        var presets = JsonReader.LoadPresets(allAbilities);
        presets.Add(newPreset);
        JsonReader.SavePresets(presets);

        AnsiConsole.MarkupLine($"[green]Preset '{name}' saved.[/]");
        AnsiConsole.MarkupLine("[grey]Press Enter to continue...[/]");
        Console.ReadLine();
    }

    public static PresetEntity? PromptSelectPreset(List<AbilityEntity> allAbilities)
    {
        var presets = JsonReader.LoadPresets(allAbilities);
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

    public static bool AskYesNo(string question)
    {
        return AnsiConsole.Confirm(question);
    }

    public static HashSet<string> PromptBannedAbilities()
    {
        string input = AnsiConsole.Ask<string>("[yellow]Enter abilities to ban (comma-separated):[/]");
        return input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => s.ToLower()).ToHashSet();
    }

    public static int PromptRollCount()
    {
        return AnsiConsole.Ask<int>("[green]How many abilities would you like to roll?[/]");
    }

    public static void ShowAbilities(List<AbilityEntity> chosen)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold green]Rolled Abilities[/]").RuleStyle("grey").Centered());

        foreach (var ability in chosen)
        {
            AnsiConsole.MarkupLine($"[blue]{ability.Name}[/]");
            AnsiConsole.MarkupLine($"   [grey]Description: {ability.Desc}[/]");
            AnsiConsole.MarkupLine($"   [grey]Released in generation: {ability.GenerationNumber} ({ability.GenerationName})[/]");
            AnsiConsole.MarkupLine("");
        }
    }

    public static void Notify(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
        AnsiConsole.MarkupLine("[grey]Press Enter to continue...[/]");
        Console.ReadLine();
    }
}
