namespace EtlServiceApp.Models
{
    public class ConsolidatedRecipe
    {
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public List<ConsolidatedIngredient> Ingredients { get; set; }

        public ConsolidatedRecipe()
        {
            Ingredients = new List<ConsolidatedIngredient>();
        }
    }

    public class ConsolidatedIngredient
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsVegetarian { get; set; }
    }
}