using DatabaseController.Models;
using DisServer.Connectors.Mobile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Mobile
{
    [Route("api/mobile/product/parameters")]
    [ApiController]
    public class ProductParameterController : ControllerBase
    {
        private readonly ProductParameterConnector connector;

        public ProductParameterController()
        {
            connector = new();
        }

        [HttpGet("indication")]
        public async Task<object> GetIndicationAsync()
        {
            try
            {
                List<Indication> product = await connector.GetIndicationsAsync();
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("contraindication")]
        public async Task<object> GetContraindicationAsync()
        {
            try
            {
                List<Contraindication> product = await connector.GetContraindicationsAsync();
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("sideeffect")]
        public async Task<object> GetSideEffectAsync()
        {
            try
            {
                List<SideEffect> sideEffect = await connector.GetSideEffectsAsync();
                string response = JsonConvert.SerializeObject(sideEffect);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("category")]
        public async Task<object> GetCategoryAsync()
        {
            try
            {
                List<ProductType> product = await connector.GetCategoriesAsync();
                string response = JsonConvert.SerializeObject(product);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
    }
}
