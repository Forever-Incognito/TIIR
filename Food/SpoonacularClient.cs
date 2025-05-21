using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpoonacularClientApp
{
    public class Recipe
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class SpoonacularClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        // Constructor to initialize the client with an API key
        public SpoonacularClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        // Asynchronously searches for recipes by a given ingredient
        public async Task<List<Recipe>> SearchRecipesByIngredient(string ingredient)
        {
            // Construct the API URL for searching recipes by ingredients
            string apiUrl = $"https://api.spoonacular.com/recipes/findByIngredients?ingredients={ingredient}&number=5&apiKey={_apiKey}"; // Limit to 5 recipes for example

            try
            {
                // Send a GET request to the Spoonacular API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                // Ensure the request was successful (status code in the 200 range)
                response.EnsureSuccessStatusCode();
                // Read the response body as a JSON string
                string responseBody = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON response into a list of Recipe objects
                var recipes = JsonSerializer.Deserialize<List<Recipe>>(responseBody);
                // Return the list of recipes
                return recipes;
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request errors
                Console.WriteLine($"HTTP request error: {e.Message}");
                return null;
            }
            catch (JsonException e)
            {
                // Handle JSON parsing errors
                Console.WriteLine($"JSON processing error: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                // Handle any other unexpected errors
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
                return null;
            }
        }
    }
}