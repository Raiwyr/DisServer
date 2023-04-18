using DatabaseController.Models;
using DisServer.Connectors;
using DisServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers
{
    [Route("api/selection")]
    [ApiController]
    public class SelectionController : ControllerBase
    {
        private readonly ProductConnector connector;

        public SelectionController()
        {
            connector = new();
        }

        // GET api/<SelectionController>/5
        [HttpGet]
        public async Task<object> Get([FromHeader] string model)
        {
            try
            {
                var selectionModel = JsonConvert.DeserializeObject<SelectionParameterModel>(model);

                List<Product> seletedProducts = await connector.SelectionProductAsync(selectionModel);

                string response = JsonConvert.SerializeObject(seletedProducts);

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();

            }
        }
    }
}
