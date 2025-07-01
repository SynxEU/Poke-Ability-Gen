using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Prompts;

namespace RandomAbilityGenerator;

static class Program
{
    static void Main()
    {
        Console.Title = "Ability Generator";

        while (true)
        {
            int rollCount = Prompt.PromptRollCount();

            List<AbilityEntity> availableAbilities = AbilityGenerator.GetFilteredAbilities(Prompt.PromptBannedAbilities());

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

            Prompt.ShowAbilities(availableAbilities, chosenAbilities);

            if (!Prompt.AskYesNo("Do you want to roll again? (y/n): "))
                break;
        }

        Console.Clear();
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}
