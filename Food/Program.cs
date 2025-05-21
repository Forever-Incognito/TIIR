using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpoonacularClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Spoonacular API key
            string apiKey = "16141d4db8be484db3cff2679e45b3d8";
            // Instance of the Spoonacular client
            var spoonacularClient = new SpoonacularClient(apiKey);

            // --- Search recipes ---
            Console.WriteLine("Enter an ingredient to search for recipes:");
            string ingredient = Console.ReadLine();

            if (!string.IsNullOrEmpty(ingredient))
            {
                Console.WriteLine($"Searching for recipes with ingredient: {ingredient}...");
                var recipes = await spoonacularClient.SearchRecipesByIngredient(ingredient);

                if (recipes != null && recipes.Count > 0)
                {
                    Console.WriteLine("\n--- Recipes Found ---");
                    // Define column widths
                    int idWidth = 10;
                    int titleWidth = 60; // Adjust as needed
                    int minRequiredWidth = idWidth + titleWidth + 7; // Min width for "| ID | Title |" and spaces

                    // Print header
                    Console.WriteLine(new string('-', minRequiredWidth));
                    Console.WriteLine($"| {"ID".PadRight(idWidth)} | {"Title".PadRight(titleWidth)} |");
                    Console.WriteLine(new string('-', minRequiredWidth));

                    // Print each recipe in a row
                    foreach (var recipe in recipes)
                    {
                        Console.WriteLine($"| {recipe.id.ToString().PadRight(idWidth)} | {recipe.title.PadRight(titleWidth)} |");
                    }
                    Console.WriteLine(new string('-', minRequiredWidth));
                    Console.WriteLine($"Total recipes found: {recipes.Count}");
                }
                else
                {
                    Console.WriteLine("No recipes found.");
                }
            }
            else
            {
                Console.WriteLine("Please enter an ingredient.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}