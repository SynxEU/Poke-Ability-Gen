using RandomAbilityGenerator.Json;

namespace RandomAbilityGenerator.Ability;

public static class AbilityGenerator
{
    public static List<AbilityEntity> GetFilteredAbilities(HashSet<string> bannedList)
        => JsonReader.LoadAbilities("Json/abilities.json")
            .Where(a => !bannedList.Contains(a.Name.ToLower()))
            .ToList();

    public static List<AbilityEntity> PickRandomAbilities(List<AbilityEntity> abilities, int count, Random rand)
        => abilities
            .GroupBy(a => a.Name.ToLower())
            .Select(g => g.First())
            .OrderBy(_ => rand.Next())
            .Take(count)
            .ToList();


    public static void PlayRollingAnimation(List<AbilityEntity> pool, int count, Random rand)
    {
        int currentLine = Console.CursorTop;

        for (int j = 11; j > 1; j--)
        {
            string name = pool[rand.Next(pool.Count)].Name;
            string text = $"Ability #{count+1}: {name}".PadRight(Console.WindowWidth - 1);

            Console.SetCursorPosition(0, currentLine);
            Console.Write(text);

            Thread.Sleep(1000 / j);
        }

        Console.SetCursorPosition(0, currentLine);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, currentLine);
    }

}