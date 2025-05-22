namespace EtlServiceApp.Models
{
    public class SourceBIngredientInfo
    {
        public string IngredientName { get; set; }
        public string Category { get; set; } // Наприклад, "Fruit", "Vegetable", "Meat"
        public bool IsVegetarian { get; set; }
    }
}