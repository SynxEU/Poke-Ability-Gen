using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Json;
using RandomAbilityGenerator.Models;
using RandomAbilityGenerator.Service;
using RandomAbilityGenerator.Prompts;
using Spectre.Console;

namespace RandomAbilityGenerator;

static class Program
{
    static void Main()
    {
        Console.Title = "Ability Generator";

        var allAbilities = JsonReader.LoadAbilities("Json/abilities.json");

        while (true)
        {
            AnsiConsole.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Choose an option:[/]")
                    .AddChoices("Roll Abilities", "Create Preset", "Exit")
            );

            if (option == "Create Preset")
            {
                Prompt.PromptCreatePreset(allAbilities);
                continue;
            }

            if (option == "Exit")
                break;

            int rollCount = Prompt.PromptRollCount();

            PresetEntity? preset = null;
            if (Prompt.AskYesNo("Use a preset?"))
            {
                preset = Prompt.PromptSelectPreset(allAbilities);
                if (preset == null)
                {
                    Prompt.Notify("Preset selection cancelled.");
                    continue;
                }
                if (preset != null)
                {
                    bool confirmed = Presets.ConfirmBannedAbilities(preset);
                    if (!confirmed)
                    {
                        Prompt.Notify("Preset confirmation declined.");
                        continue;
                    }
                }

            }

            List<AbilityEntity> availableAbilities = AbilityGenerator.GetFilteredAbilities(allAbilities, preset);

            if (availableAbilities.Count == 0)
            {
                Prompt.Notify("No abilities available to roll from due to bans.");
                continue;
            }

            List<AbilityEntity> chosenAbilities = AbilityGenerator.PickRandomAbilities(availableAbilities, rollCount, new Random());

            if (rollCount > chosenAbilities.Count)
            {
                Prompt.Notify($"You requested {rollCount} unique abilities, but only {chosenAbilities.Count} are available.");
                continue;
            }

            Prompt.ShowAbilities(chosenAbilities);

            if (!Prompt.AskYesNo("Want to return to the main menu?"))
                break;
        }

        AnsiConsole.MarkupLine("[grey]Press Enter to exit...[/]");
        Console.ReadLine();
    }
}
