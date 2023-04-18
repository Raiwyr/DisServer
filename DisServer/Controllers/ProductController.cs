using DatabaseController.Models;
using DisServer.Connectors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductConnector connector;

        public ProductController()
        {
            connector = new();
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<object> GetAsync()
        {
            try
            {
                List<Product> products = await connector.GetProductsAsync();
                return JsonConvert.SerializeObject(products);
            }
            catch
            {
                return new ForbidResult();
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<object> GetAsync(int id)
        {
            try
            {
                Product? product = await connector.GetProductByIdAsync(id);
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch(Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("category/{id}")]
        public async Task<object> GetByCategoryIdAsync(int id)
        {
            try
            {
                List<Product> products = await connector.GetProductsByCategoryIdAsync(id);
                string response = JsonConvert.SerializeObject(products);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("search")]
        public async Task<object> SearchAsync(string param)
        {
            try
            {
                List<Product> product = await connector.SearchProductsAsync(param);
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();

            }
        }

                // POST api/<ProductController>
                [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
