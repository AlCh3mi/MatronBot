using System;
using MatronBot.ApexLegends.Map.Data;

namespace MatronBot.ApexLegends.Map {
    public static class ConsoleExtension {
        
        public static void NavigateWithConsole(this MapInfo mapInfo) {
            Console.WriteLine("MapInfo Console Explorer");
            var quit = false;
            do {
                Console.WriteLine();
                Console.WriteLine($"1: {GameMode.BattleRoyale}");
                Console.WriteLine($"2: {GameMode.Arenas}");
                Console.WriteLine($"3: {GameMode.Ranked}");
                Console.WriteLine("Anything else to quit.");
                if (int.TryParse(Console.ReadLine(), out var modeInt)) {
                    switch (modeInt) {
                        case 1: //Display Battle Royale Info
                            Console.WriteLine("Battle Royale:");
                            MapExplorer(mapInfo.Modes[GameMode.BattleRoyale]);
                            break;
                        case 2: //Display Arenas Info
                            Console.WriteLine("Arena's:");
                            MapExplorer(mapInfo.Modes[GameMode.Arenas]);
                            break;
                        case 3:
                            Console.WriteLine("Ranked:");
                            Console.WriteLine($"{mapInfo.Modes[GameMode.Ranked]}");
                            break;
                        default:
                            Console.WriteLine("Invalid option, Quitting.");
                            quit = true;
                            break;
                    }
                }
            } while (!quit);
        }

        static void MapExplorer(GameMode mode) {
            while (true) {
                Console.WriteLine("\n1. Current");
                Console.WriteLine("2. Next");
                Console.WriteLine();
                Console.WriteLine("anything else to return.");
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var input)) {
                    if (input == 1)
                        ConsoleMapInfo(mode.Current);
                    else if (input == 2)
                        ConsoleMapInfo(mode.Next);
                    else {
                        Console.WriteLine("Invalid Entry. returning..");
                        break;
                    }
                }
                else Console.WriteLine("Invalid Entry. Please try again.");
            }
        }

        static void ConsoleMapInfo(Data.Map map) {
            Console.WriteLine($"Current         : {map.Name}");
            Console.WriteLine($"Start           : {map.ReadableDateStart}");
            Console.WriteLine($"End             : {map.ReadableDateEnd}");
            Console.WriteLine($"Duration        : {map.DurationInMinutes}");
            Console.WriteLine($"Mins Remaining  : {map.RemainingMins}");
        }
    }
}