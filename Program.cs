using RandomAbilityGenerator.Ability;
using RandomAbilityGenerator.Json;

namespace RandomAbilityGenerator;

static class Program
{
    static void Main()
    {
        Console.Title = "Ability Generator";

        while (true)
        {
            Console.Clear();
            Console.Write("Enter number of abilities to roll: ");
            if (!int.TryParse(Console.ReadLine(), out int rollCount) || rollCount < 1)
            {
                Console.WriteLine("Invalid number. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("Enter banned abilities separated by commas (or leave empty):");
            string bannedInput = Console.ReadLine() ?? "";
            HashSet<string> bannedList = bannedInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(name => name.ToLower())
                .ToHashSet();
            
            List<AbilityEntity> availableAbilities = AbilityGenerator.GetFilteredAbilities(JsonReader.LoadAbilities("Json/abilities.json"), bannedList);

            if (availableAbilities.Count == 0)
            {
                Console.WriteLine("No abilities available to roll from due to bans.");
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
                continue;
            }

            List<AbilityEntity> chosenAbilities = AbilityGenerator.PickRandomAbilities(availableAbilities, rollCount, new Random());

            if (rollCount > chosenAbilities.Count)
            {
                Console.WriteLine($"You requested {rollCount} unique abilities, but only {chosenAbilities.Count} are available.");
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
                continue;
            }

            Console.Clear();
            for (int i = 0; i < chosenAbilities.Count; i++)
            {
                AbilityGenerator.PlayRollingAnimation(availableAbilities, new Random());

                var ability = chosenAbilities[i];
                Console.WriteLine($"Ability #{i + 1}: {ability.Name}");
                Console.WriteLine($"    Description: {ability.Desc}");
                Console.WriteLine($"    Released in Generation: {ability.Generation}\n");
            }

            Console.Write("Do you want to roll again? (y/n): ");
            string? again = Console.ReadLine()?.Trim().ToLower();
            if (again != "y" && again != "yes")
                break;
        }

        Console.Clear();
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}