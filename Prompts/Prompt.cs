using RandomAbilityGenerator.Ability;

namespace RandomAbilityGenerator.Prompts;

public class Prompt
{
    public static int PromptRollCount()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("Enter number of abilities to roll: ");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                return count;

            Console.WriteLine("Invalid number. Press Enter to try again.");
            Console.ReadLine();
        }
    }

    public static HashSet<string> PromptBannedAbilities()
    {
        Console.WriteLine("Enter banned abilities separated by commas (or leave empty):");
        string input = Console.ReadLine() ?? "";
        return input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(name => name.ToLower())
            .ToHashSet();
    }

    public static void ShowAbilities(List<AbilityEntity> pool, List<AbilityEntity> chosen)
    {
        Console.Clear();

        for (int i = 0; i < chosen.Count; i++)
        {
            AbilityGenerator.PlayRollingAnimation(pool, i, new Random());

            AbilityEntity ability = chosen[i];
            Console.WriteLine($"Ability #{i + 1}: {ability.Name}");
            Console.WriteLine($"    Description: {ability.Desc}");
            Console.WriteLine($"    Released in Generation: {ability.Generation}\n");
        }
    }

    public static void Notify(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Press Enter to try again...");
        Console.ReadLine();
    }

    public static bool AskYesNo(string prompt)
    {
        Console.Write(prompt);
        string? response = Console.ReadLine()?.Trim().ToLower();
        return response == "y" || response == "yes";
    }
}