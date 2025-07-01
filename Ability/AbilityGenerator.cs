namespace RandomAbilityGenerator.Ability;

public static class AbilityGenerator
{
    public static List<AbilityEntity> GetFilteredAbilities(List<AbilityEntity> allAbilities, HashSet<string> bannedList)
    {
        return allAbilities
            .Where(a => !bannedList.Contains(a.Name.ToLower()))
            .ToList();
    }

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
            int animIndex = rand.Next(pool.Count);
            string text = $"Rolling: {pool[animIndex].Name}";

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text.PadRight(Console.WindowWidth - 1));
            Thread.Sleep(50);
        }

        ClearLine();
    }

    private static void ClearLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
    }
}