# 🌀 Random Ability Generator

A simple C# console application that randomly generates a specified number of pokemon abilities from a JSON file, with support for banning specific ones.

## 📁 Project Structure

│ \
├── Ability/ \
│ ├── AbilityEntity.cs # The Ability model class \
│ └── AbilityGenerator.cs # Logic for filtering, randomizing, and displaying abilities \
│ \
├── Json/ \
│ ├── abilities.json # JSON file containing all ability data \
│ └── JsonReader.cs # Loads and parses the abilities JSON \
│ \
├── Program.cs # Entry point with main console logic \
└── RandomAbilityGenerator.csproj

## ▶️ How It Works

1. The program loads all abilities from `abilities.json`.
2. The user inputs how many abilities to roll.
3. The user may ban specific ability names.
4. The program:
   - Filters out banned abilities.
   - Randomly selects the requested number of unique abilities.
   - Animates a simple "rolling" effect.
   - Displays name, description, and generation info.

## 💾 JSON Format

The abilities are loaded from a `Json/abilities.json` file. Each ability should look like this:

```json
{
  "id": 1,
  "name": "Overgrow",
  "gen": 3,
  "desc": "Boosts Grass-type moves in a pinch.",
  "shortDesc": "Grass moves get stronger at low HP."
}
```

## 🔧 Setup Instructions
### ✅ Requirements
- .NET 9 SDK
- VS Code / JetBrains Rider / Visual Studio / CLI

### 📦 Build & Run
From project root:
```sh
dotnet build
dotnet run
```
Or press **Run** in your IDE (Rider/VS).

## TO:DO
- [ ] Add every pokemon ability to the JSON file ( Maybe find an API to make it lighter? )

## 🙋 Author
Made by **SynxEU** \
Created as a personal side project / experiment.
