using DataFederationService.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System; // Added for ArgumentNullException

namespace DataFederationService.Services
{
    public class SpoonacularFederationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://api.spoonacular.com";

        public SpoonacularFederationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Retrieve the API key from configuration (appsettings.json)
            _apiKey = configuration["SpoonacularApi:ApiKey"];
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new ArgumentNullException("Spoonacular API Key is not configured in appsettings.json.");
            }
        }

        /// <summary>
        /// Searches for recipes on the Spoonacular API based on a provided query.
        /// </summary>
        /// <param name="query">The search query (e.g., "pasta").</param>
        /// <returns>A SpoonacularRecipeSearchResponse object containing search results.</returns>
        public async Task<SpoonacularRecipeSearchResponse> SearchRecipesAsync(string query)
        {
            // Construct the request URL for the Spoonacular API.
            var requestUrl = $"{_baseUrl}/recipes/complexSearch?query={query}&apiKey={_apiKey}";

            // Execute the HTTP GET request.
            var response = await _httpClient.GetAsync(requestUrl);

            // Ensure the HTTP request was successful (status code 2xx).
            response.EnsureSuccessStatusCode(); // Throws an exception if status code is not 2xx.

            // Read the response content as a string.
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into the defined model.
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<SpoonacularRecipeSearchResponse>(jsonResponse, options);

            return result;
        }
    }
}