using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeFriendCalc
{
    public class Program
    {
        public static void Main()
        {
            var app = new PokeFriendCalculator();
            app.Run();
        }
    }

    public class PokeFriendCalculator
    {
        public void Run()
        {
            DisplayWelcomeMessage();

            do
            {
                Console.WriteLine();

                int generation = GetGeneration();
                int currentHappiness = GetCurrentHappiness(generation);
                int desiredHappiness = GetDesiredHappiness(generation, currentHappiness);
                int iterations = PromptInt("How many iterations? (default: 1000): ", 1000, 1, int.MaxValue);

                List<int> stepValues = SimulateHappinessSteps(generation, currentHappiness, desiredHappiness, iterations);
                DisplayResults(stepValues);

                Console.Write("\nWould you like to run calculator again? (y/n): ");
            }
            while (Console.ReadKey().Key == ConsoleKey.Y);
        }

        private void DisplayWelcomeMessage()
        {
            Console.WriteLine($"Welcome to PokéFriendCalc v0.99.0!\n");

            Console.WriteLine("Note: The step calculations for generations VI and VII might not be correct.");
            Console.WriteLine("Note: This program doesn't calculate steps for Let's Go, Pikachu! and Let's Go, Eevee! games.\n");

            Console.WriteLine("In the following prompts, if you want to proceed with a default value, just press the ENTER key.");
        }

        private int GetGeneration()
        {
            const int defaultGeneration = 4;
            return PromptInt($"Enter generation (1-9, default: {defaultGeneration}): ", defaultGeneration, 1, 9);
        }

        private int GetCurrentHappiness(int generation)
        {
            int defaultCurrentHappiness = generation switch
            {
                1 => 90,
                >= 2 and <= 7 => 70,
                _ => 50
            };

            int maxCurrentHappiness = (generation == 8) ? 158 : 254;

            return PromptInt($"Enter current happiness (default: {defaultCurrentHappiness}, max: {maxCurrentHappiness}): ", defaultCurrentHappiness, 0, maxCurrentHappiness);
        }

        private int GetDesiredHappiness(int generation, int currentHappiness)
        {
            int defaultDesiredHappiness = generation switch
            {
                1 => 255,
                >= 2 and <= 7 => 220,
                8 => 159,
                _ => 160
            };

            int maxDesiredHappiness = (generation == 8) ? 159 : 255;
            return PromptInt($"Enter desired happiness (default: {defaultDesiredHappiness}, max: {maxDesiredHappiness}): ",
                            defaultDesiredHappiness, currentHappiness + 1, maxDesiredHappiness);
        }

        private List<int> SimulateHappinessSteps(int generation, int currentHappiness, int desiredHappiness, int iterations)
        {
            var stepValues = new List<int>();
            var random = new Random();

            int requiredSteps = generation switch
            {
                1 or 5 => 255,
                2 => 512,
                _ => 128
            };

            for (int i = 0; i < iterations; i++)
            {
                int happiness = currentHappiness;
                int steps = 0;

                while (happiness < desiredHappiness)
                {
                    steps += requiredSteps;

                    if (random.Next() % 2 == 1)
                    {
                        if ((generation == 1 && happiness < 100) ||
                            (generation >= 5 && generation <= 7 && happiness < 200))
                        {
                            happiness += 2;
                        }
                        else
                        {
                            happiness++;
                        }
                    }
                }

                stepValues.Add(steps);
                Console.WriteLine($"Iteration {i + 1}, steps: {steps}");
            }

            return stepValues;
        }

        private void DisplayResults(List<int> stepValues)
        {
            Console.WriteLine($"\nAvg: {Math.Round(stepValues.Average())}, Min: {stepValues.Min()}, Max: {stepValues.Max()}");
        }

        private int PromptInt(string prompt, int defaultValue, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;

            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) return defaultValue;

                if (!int.TryParse(input, out result) || result < min || result > max)
                {
                    Console.WriteLine($"\nError: Please enter a number between {min} and {max}.");
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
