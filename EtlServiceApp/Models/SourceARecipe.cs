namespace EtlServiceApp.Models
{
    public class SourceARecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> MainIngredients { get; set; }
    }
}