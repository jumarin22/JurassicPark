using System;
using System.Collections.Generic;
using System.Linq;

namespace JurassicPark
{
    class Program
    {
        // Create a class to represent your dinosaurs.
        class Dinosaur
        {
            public string Name { get; set; }
            public string DietType { get; set; }
            public string WhenAcquired { get; set; }
            public int Weight { get; set; }
            public int EnclosureNumber { get; set; }

            public string Description()
            {
                return $"Name: {Name}, Diet Type: {DietType}, Weight: {Weight}, Enclosure Number: {EnclosureNumber}, Acquired: {WhenAcquired}";
            }
        }

        static void Main(string[] args)
        {
            // Keep track of your dinosaurs in a List<Dinosaur>.
            var dinos = new List<Dinosaur>();

            //When the console application runs, it should let the user choose one of the following actions:
            Console.WriteLine("Welcome to Jurassic Park");

            var userChoice = "zzz";
            var keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("(V)iew, (A)dd, (R)emove, (T)ransfer, (S)ummary, or (Q)uit");
                userChoice = Console.ReadLine().ToLower();
                switch (userChoice)
                {
                    case "v" or "view":
                        ViewDinos(dinos);
                        break;
                    case "a" or "add":
                        AddDino(dinos);
                        break;
                    case "r" or "remove":
                        RemoveDino(dinos);
                        break;
                    case "t" or "transfer":
                        TransferDino(dinos);
                        break;
                    case "s" or "summary":
                        Summary(dinos);
                        break;
                    case "q" or "quit":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Sorry, I didn't understand.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private static int PromptForInt(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            return int.Parse(userInput);
        }

        private static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            return userInput;
        }

        private static void Quit(bool keepRunning)
        {
            // Quit
            // This will stop the program
            keepRunning = false;
        }

        private static void Summary(List<Dinosaur> dinos)
        {
            // Summary
            // This command will display the number of carnivores and the number of herbivores.

            var carny = dinos.Where(dino => dino.DietType == "carnivore").Count();
            var herby = dinos.Where(dino => dino.DietType == "herbivore").Count();

            Console.WriteLine($"There are {carny} Carnivores and {herby} Herbivores.");
        }

        private static void TransferDino(List<Dinosaur> dinos)
        {
            // Transfer
            // This command will prompt the user for a dinosaur name and a new EnclosureNumber and update that dino's information.

            var name = PromptForString("What is the name of the Dinosaur that you want to Transfer? ");
            int index = dinos.FindIndex(dino => name == dino.Name);

            if (index == -1)
                Console.WriteLine($"Could not find {name} in the List!");
            else
            {
                var newEnclosure = PromptForInt($"What number Enclosure do you want to transfer {name} to? ");
                dinos[index].EnclosureNumber = newEnclosure;
                Console.WriteLine($"Transferred {name} to Enclosure {newEnclosure}.");
            }
        }

        private static void RemoveDino(List<Dinosaur> dinos)
        {
            // Remove
            // This command will prompt the user for a dinosaur name then find and delete the dinosaur with that name.

            var name = PromptForString("What is the name of the Dinosaur that you want to Remove? ");
            int index = dinos.FindIndex(dino => name == dino.Name);

            if (index == -1)
                Console.WriteLine($"Could not find {name} in the List!");
            else
            {
                Console.WriteLine($"{name} Removed.");
                dinos.RemoveAt(index);
            }
        }

        private static void AddDino(List<Dinosaur> dinos)
        {
            // Add
            // This command will let the user type in the information for a dinosaur and add it to the list.
            // Prompt for the Name, Diet Type, Weight and Enclosure Number, but the WhenAcquired must be supplied by the code.

            Console.WriteLine("Adding new Dinosaur...");

            var name = PromptForString("What is the Dinosaur's name? ");
            var dietType = "";

            var incorrectDietInput = true;
            while (incorrectDietInput)
            {
                dietType = PromptForString("What is the Dinosaur's Diet Type? (C)arnivore or (H)erbivore? ").ToLower();
                if (dietType == "c" || dietType == "carnivore")
                {
                    dietType = "carnivore";
                    incorrectDietInput = false;
                }
                else if (dietType == "h" || dietType == "herbivores")
                {
                    dietType = "herbivore";
                    incorrectDietInput = false;
                }
                else
                    Console.WriteLine("Sorry, I didn't understand.");
            }

            var weight = PromptForInt("What is the Dinosaur's weight? ");
            var enclosureNumber = PromptForInt("What is the Enclosure Number? ");
            var whenAcquired = DateTime.Now.ToString("yyyy.MM.dd @ HH:mm:ss");

            var newDino = new Dinosaur();

            newDino.Name = name;
            newDino.DietType = dietType;
            newDino.Weight = weight;
            newDino.EnclosureNumber = enclosureNumber;
            newDino.WhenAcquired = whenAcquired;

            dinos.Add(newDino);
        }

        private static void ViewDinos(List<Dinosaur> dinos)
        {
            // View
            // This command will show the all the dinosaurs in the list,
            // ordered by WhenAcquired. If there aren't any dinosaurs in the park then print out a message that there aren't any.

            if (dinos.Count > 0)
            {
                var sortedDinos = dinos.OrderBy(dino => dino.WhenAcquired);
                foreach (var dino in dinos)
                    Console.WriteLine(dino.Description());
            }
            else
                Console.WriteLine("There are no Dinosaurs in the List!");
        }
    }
}
