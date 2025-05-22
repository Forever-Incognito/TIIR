using DataFederationService.Models;
using DataFederationService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System; // Added for general Exception class
using System.Net.Http; // Already there, but good to ensure it's explicitly for HttpRequestException

namespace DataFederationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FederationController : ControllerBase
    {
        private readonly SpoonacularFederationService _federationService;
        private readonly ILogger<FederationController> _logger; // Logger for error tracing

        public FederationController(SpoonacularFederationService federationService, ILogger<FederationController> logger)
        {
            _federationService = federationService;
            _logger = logger;
        }

        /// <summary>
        /// Dynamically retrieves recipes from Spoonacular API based on a search query.
        /// This method acts as a data federation point, fetching data in real-time from an external source.
        /// </summary>
        /// <param name="query">The search term for recipes (e.g., "pasta", "pizza").</param>
        /// <returns>A list of recipes from Spoonacular or an error response.</returns>
        [HttpGet("recipes")]
        public async Task<ActionResult<SpoonacularRecipeSearchResponse>> GetRecipes(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            try
            {
                // Delegate the request to the federation service to fetch data from Spoonacular.
                var recipes = await _federationService.SearchRecipesAsync(query);
                return Ok(recipes); // Return the fetched data to the client.
            }
            catch (HttpRequestException ex)
            {
                // Log the exception for debugging purposes.
                _logger.LogError(ex, "Error calling Spoonacular API for query: {Query}", query);
                // Return a 500 Internal Server Error with a client-friendly message.
                return StatusCode(500, $"Error retrieving data from external source: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions.
                _logger.LogError(ex, "An unexpected error occurred while processing recipes for query: {Query}", query);
                // Return a generic 500 Internal Server Error.
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}