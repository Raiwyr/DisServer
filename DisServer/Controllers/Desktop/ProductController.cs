using DatabaseController.Models;
using DisServer.Connectors.Desktop;
using DisServer.Models.Desktop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Desktop
{
    [Route("api/desktop/product")]
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
                List<ProductHeaderModel> product = await connector.GetProductHeadersAsync();
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch (Exception ex)
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
                Product? product = await connector.GetProductAsync(id);
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
        public async Task<object> PostAsync(
            [FromBody] string model
            )
        {
            try
            {
                var product = JsonConvert.DeserializeObject<PostProductModel>(model);

                if (product == null)
                    return false;

                await connector.PostProductAsync(product);

                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut]
        public async Task<object> PutAsync(
            int id,
            [FromBody] string model
            )
        {
            try
            {
                var product = JsonConvert.DeserializeObject<PostProductModel>(model);

                if (product == null)
                    return false;

                await connector.PutProductAsync(id, product);

                return true;
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
                List<Product> products = await connector.SearchProductsAsync(param);

                List<ProductHeaderModel> headers = products.Select(p => new ProductHeaderModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageName = p.ImageName ?? ""
                }).ToList();

                string response = JsonConvert.SerializeObject(headers);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();

            }
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteProductAsync(int id)
        {
            try
            {
                await connector.DeleteProductAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }


        [HttpGet("image/{name}")]
        public async Task<object> GetImageAsync(string name)
        {
            try
            {
                byte[] imageByte = await connector.GetImageBytes(name);

                string response = JsonConvert.SerializeObject(imageByte.ToList());

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        //// PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public async Task<object> PutAsync(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public async Task<object> DeleteAsync(int id)
        //{
        //}
    }
}
