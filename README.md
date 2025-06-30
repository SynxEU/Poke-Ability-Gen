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
   "name": "Stench",
   "gen": 3,
   "desc": "Has a 10% chance of making target Pok\u00e9mon flinch with each hit."
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

## 👀 How it looks:
#### Input:
![image](https://github.com/user-attachments/assets/056b85bd-4980-406c-8d03-4ad2fa4f3f68)

#### "As One" + Ability example:
![image](https://github.com/user-attachments/assets/07da9953-891d-4a36-95ec-a134c171870e)


## 📋 TO:DO
- [x] Add every Pokémon ability (I can find) to the JSON file

## ⛔ Issues/Missing abilities
If you have any issues with the program please put it under the [Issuess page](https://github.com/SynxEU/Poke-Ability-Gen/issues) \
If there is any missing abilities please contact me on discord: **synx_eu**

## 🙋 Author
Made by **SynxEU** \
Created as a personal side project / experiment.
