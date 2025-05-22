using EtlServiceApp.Models; // Added this using directive to resolve "SourceBIngredientInfo not detected"
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EtlServiceApp.Services
{
    public class EtlService
    {
        // Simulated source data for Extract phase
        private static List<SourceARecipe> _sourceARecipes = new List<SourceARecipe>
        {
            new SourceARecipe { Id = 1, Title = "Simple Veggie Stir-fry", MainIngredients = new List<string> { "Broccoli", "Carrot", "Soy Sauce" } },
            new SourceARecipe { Id = 2, Title = "Chicken Pasta", MainIngredients = new List<string> { "Chicken", "Pasta", "Tomato" } },
            new SourceARecipe { Id = 3, Title = "Mushroom Soup", MainIngredients = new List<string> { "Mushroom", "Cream" } }
        };

        private static List<SourceBIngredientInfo> _sourceBIngredientInfo = new List<SourceBIngredientInfo>
        {
            new SourceBIngredientInfo { IngredientName = "Broccoli", Category = "Vegetable", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Carrot", Category = "Vegetable", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Chicken", Category = "Meat", IsVegetarian = false },
            new SourceBIngredientInfo { IngredientName = "Pasta", Category = "Grain", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Tomato", Category = "Fruit", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Mushroom", Category = "Fungi", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Cream", Category = "Dairy", IsVegetarian = true },
            new SourceBIngredientInfo { IngredientName = "Soy Sauce", Category = "Condiment", IsVegetarian = true }
        };

        // Target storage for consolidated data (Load phase)
        private static List<ConsolidatedRecipe> _consolidatedData = new List<ConsolidatedRecipe>();

        /// <summary>
        /// Retrieves the currently consolidated data.
        /// </summary>
        /// <returns>A list of consolidated recipes.</returns>
        public List<ConsolidatedRecipe> GetConsolidatedData()
        {
            return _consolidatedData;
        }

        /// <summary>
        /// Runs the ETL (Extract, Transform, Load) process.
        /// Extracts data from simulated sources, transforms it, and loads into consolidated storage.
        /// </summary>
        public async Task RunEtlProcess()
        {
            // Clear previous consolidated data for fresh run
            _consolidatedData.Clear();

            // --- EXTRACT & TRANSFORM ---
            // Data is "extracted" from static lists _sourceARecipes and _sourceBIngredientInfo.
            // Transformation happens during iteration and lookup.
            foreach (var recipeA in _sourceARecipes)
            {
                var consolidatedRecipe = new ConsolidatedRecipe
                {
                    RecipeId = recipeA.Id,
                    RecipeTitle = recipeA.Title,
                    Ingredients = new List<ConsolidatedIngredient>()
                };

                foreach (var mainIngredient in recipeA.MainIngredients)
                {
                    var ingredientInfoB = _sourceBIngredientInfo.FirstOrDefault(i => i.IngredientName == mainIngredient);

                    var consolidatedIngredient = new ConsolidatedIngredient
                    {
                        Name = mainIngredient,
                        Category = ingredientInfoB?.Category ?? "Unknown", // Transform: Add category from source B
                        IsVegetarian = ingredientInfoB?.IsVegetarian ?? false // Transform: Add vegetarian status
                    };
                    consolidatedRecipe.Ingredients.Add(consolidatedIngredient);
                }

                // Optional transformation: Filter out recipes containing "Chicken"
                // Uncomment to enable specific filtering logic
                // if (consolidatedRecipe.Ingredients.Any(i => i.Name == "Chicken"))
                // {
                //     continue; // Skip recipes with chicken (example of filtering)
                // }

                _consolidatedData.Add(consolidatedRecipe);
            }

            // --- LOAD ---
            // Data is "loaded" into the static _consolidatedData list.
            // In a real ETL, this would involve writing to a database, file, or external system.

            await Task.CompletedTask; // For asynchronous demonstration
        }
    }
}