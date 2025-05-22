namespace DataFederationService.Models
{
    public class SpoonacularRecipeSearchResponse
    {
        public List<SpoonacularRecipe> Results { get; set; }
        public int Offset { get; set; }
        public int Number { get; set; }
        public int TotalResults { get; set; }
    }

    public class SpoonacularRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
    }
}