using RandomAbilityGenerator.Json;

namespace RandomAbilityGenerator.Ability;

public static class AbilityGenerator
{
    public static List<AbilityEntity> GetFilteredAbilities(HashSet<string> bannedList)
        => JsonReader.LoadAbilities("Json/abilities.json")
            .Where(a => !bannedList.Contains(a.Name.ToLower()))
            .ToList();

    public static List<AbilityEntity> PickRandomAbilities(List<AbilityEntity> abilities, int count, Random rand)
    {
        List<AbilityEntity> uniqueByName = abilities
            .GroupBy(a => a.Name.ToLower())
            .Select(g => g.First())
            .ToList();

        return uniqueByName
            .OrderBy(_ => rand.Next())
            .Take(count)
            .ToList();
    }

    public static void PlayRollingAnimation(List<AbilityEntity> pool, Random rand)
    {
        for (int j = 0; j < 10; j++)
        {
            string text = $"Rolling: {pool[rand.Next(pool.Count)].Name}";

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text.PadRight(Console.WindowWidth - 1));
            Thread.Sleep(50);
        }

        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
    }
}