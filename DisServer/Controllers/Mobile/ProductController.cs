using DatabaseController.Models;
using DisServer.Connectors.Mobile;
using DisServer.Models.Mobile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Mobile
{
    [Route("api/mobile/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductConnector connector;

        public ProductController()
        {
            connector = new();
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
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("category")]
        public async Task<object> GetByCategoryIdAsync(
            int id,
            [FromBody] string filter)
        {
            try
            {
                FilterModel? filterModel;
                try { filterModel = JsonConvert.DeserializeObject<FilterModel>(filter); }
                catch { filterModel = null; }

                List<Product> products = filterModel == null ? await connector.GetProductsByCategoryIdAsync(id) : await connector.GetProductsByCategoryIdAsync(id, filterModel);

                List<ProductHeaderModel> headers = products.Select(p => new ProductHeaderModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Availability.Price,
                    Assessment = p.Review.Count() > 0 ? p.Review.Sum(p => p.Assessment) / p.Review.Count() : 0,
                    Count = p.Availability.Quantity,
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

        [HttpPost("search")]
        public async Task<object> SearchAsync(
            string param,
            [FromBody] string filter)
        {
            try
            {
                FilterModel? filterModel;
                try { filterModel = JsonConvert.DeserializeObject<FilterModel>(filter); }
                catch { filterModel = null; }

                List<Product> products = filterModel == null ? await connector.SearchProductsAsync(param) : await connector.SearchProductsAsync(param, filterModel);

                List<ProductHeaderModel> headers = products.Select(p => new ProductHeaderModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Availability.Price,
                    Assessment = p.Review.Count() > 0 ? p.Review.Sum(p => p.Assessment) / p.Review.Count() : 0,
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

        [HttpPost("filter")]
        public async Task<object> GetFiltersAsync([FromBody] string ids)
        {
            try
            {
                List<int>? listIds = JsonConvert.DeserializeObject<List<int>>(ids);

                if (listIds == null)
                    throw new Exception();

                var filterParameters = await connector.GetFiltersAsync(listIds);

                string response = JsonConvert.SerializeObject(filterParameters);
                return response;
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

                string response = JsonConvert.SerializeObject(Convert.ToBase64String(imageByte));

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
    }
}
