using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AzureSampleDataAccessLayer.Repositories;
using AzureSampleDataAccessLayer.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureSampleWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ProductRepository _repository;

        public ProductsController(ApplicationDBContext context)
        {
            _context = context;
            _repository = new ProductRepository(context);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                
                if (product == null) 
                    return NotFound(new { Message = $"Product with ID {id} not found." });
                else
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET api/products/productNumber
        [HttpGet("ProductNumber/{productNumber}")]
        public async Task<ActionResult<Product>> GetByProductNumber(string productNumber)
        {
            try
            {
                var product = await _repository.GetByProductNumberAsync(productNumber);
                
                if (product == null) 
                    return NotFound(new { Message = $"Product with Product Number {productNumber} not found." });
                else
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET api/search?<name?>&<category?>&
        [HttpGet("Search")]
        public async Task<ActionResult<Product>> SearchProduct([FromQuery] string name = null, [FromQuery] string category = null, [FromQuery] bool inStockOnly = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(category))
                    return BadRequest(new { Message = "Please provide either name or product category to search." });
                
                var products = await _repository.SearchAsync(name, category, inStockOnly);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
