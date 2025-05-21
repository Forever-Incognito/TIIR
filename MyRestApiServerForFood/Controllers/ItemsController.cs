using Microsoft.AspNetCore.Mvc;
using MyRestApiServerForFood;
using System.Collections.Generic;
using System.Linq;

namespace MyRestApiServerForFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Attribute indicating this is an API controller
    public class ItemsController : ControllerBase
    {
        // Storing data in memory for simplicity
        private static List<Item> _items = new List<Item>
        {
            new Item { Id = 1, Name = "Apple", Description = "A red fruit" },
            new Item { Id = 2, Name = "Banana", Description = "A yellow fruit" }
        };
        private static int _nextId = 3; // For generating unique IDs

        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Ok(_items); // Returns a list of all items with 200 OK status
        }

        // GET: api/items/5
        [HttpGet("{id}")] // Route for getting an item by ID
        public ActionResult<Item> GetItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id); // Finds the item by ID

            if (item == null)
            {
                return NotFound(); // If item is not found, returns 404 Not Found
            }

            return Ok(item); // Returns the found item with 200 OK status
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> CreateItem(Item item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("Name is required."); // Validates input data
            }

            item.Id = _nextId++; // Assigns a new unique ID
            _items.Add(item); // Adds the item to the list
            // Returns the created item with 201 CreatedAtAction status
            // CreatedAtAction also provides a URL to retrieve the created resource
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // PUT: api/items/5
        [HttpPut("{id}")] // Route for updating an item by ID
        public IActionResult UpdateItem(int id, Item updatedItem)
        {
            if (id != updatedItem.Id)
            {
                return BadRequest("ID mismatch."); // Checks if the ID in the route matches the ID in the request body
            }

            var existingItem = _items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound(); // If item is not found, returns 404 Not Found
            }

            // Updates properties of the existing item
            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;

            return NoContent(); // Returns 204 No Content, as the resource was successfully updated
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")] // Route for deleting an item by ID
        public IActionResult DeleteItem(int id)
        {
            var itemToRemove = _items.FirstOrDefault(i => i.Id == id);
            if (itemToRemove == null)
            {
                return NotFound(); // If item is not found, returns 404 Not Found
            }

            _items.Remove(itemToRemove); // Removes the item from the list

            return NoContent(); // Returns 204 No Content, as the resource was successfully deleted
        }
    }
}