using DatabaseController.Models;
using DisServer.Connectors.Mobile;
using DisServer.Models.Mobile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Mobile
{
    [Route("api/mobile/selection")]
    [ApiController]
    public class SelectionController : ControllerBase
    {
        private readonly ProductConnector connector;

        public SelectionController()
        {
            connector = new();
        }

        // GET api/<SelectionController>/5
        [HttpPost]
        public async Task<object> Get([FromBody] string model)
        {
            try
            {
                var selectionModel = JsonConvert.DeserializeObject<SelectionParameterModel>(model);

                List<Product> seletedProducts = await connector.SelectionProductAsync(selectionModel);

                List<ProductHeaderModel> headers = seletedProducts.Select(p => new ProductHeaderModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Availability.Price,
                    Assessment = p.Review.Count() > 0 ? p.Review.Sum(p => p.Assessment) / p.Review.Count() : 0
                }).ToList();

                string response = JsonConvert.SerializeObject(headers);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();

            }
        }
    }
}
