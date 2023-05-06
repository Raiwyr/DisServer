using DatabaseController.Models;
using DisServer.Connectors.Desktop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Desktop
{
    [Route("api/desktop/product/parameter")]
    [ApiController]
    public class ProductParameterController : ControllerBase
    {
        private readonly ProductParameterConnector connector;

        public ProductParameterController()
        {
            connector = new();
        }

        #region indication

        // GET: api/<ProductParameterController>
        [HttpGet("indications")]
        public async Task<object> GetIndicationsAsync()
        {
            try
            {
                List<Indication> indications = await connector.GetIndicationsAsync();
                string response = JsonConvert.SerializeObject(indications);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("indications")]
        public async Task<object> PostIndicationAsync(string title)
        {
            try
            {
                return await connector.AddIndicationAsync(title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut("indications")]
        public async Task<object> PutIndicationsAsync(
            int id,
            string title
            )
        {
            try
            {
                return await connector.UpdateIndicationAsync(id, title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("indications")]
        public async Task<object> DeleteIndicationsAsync(int id)
        {
            try
            {
                return await connector.DeleteIndicationAsync(id);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
        #endregion

        #region contraindications
        [HttpGet("contraindications")]
        public async Task<object> GetContraindicationsAsync()
        {
            try
            {
                List<Contraindication> contraindications = await connector.GetContraindicationsAsync();
                string response = JsonConvert.SerializeObject(contraindications);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("contraindications")]
        public async Task<object> PostContraindicationAsync(string title)
        {
            try
            {
                return await connector.AddContraindicationsAsync(title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut("contraindications")]
        public async Task<object> PutContraindicationAsync(
            int id,
            string title
            )
        {
            try
            {
                return await connector.UpdateContraindicationsAsync(id, title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("contraindications")]
        public async Task<object> DeleteContraindicationAsync(int id)
        {
            try
            {
                return await connector.DeleteContraindicationsAsync(id);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
        #endregion

        #region releaseforms
        [HttpGet("releaseforms")]
        public async Task<object> GetReleaseFormsAsync()
        {
            try
            {
                List<ReleaseForm> releaseForms = await connector.GetReleaseFormsAsync();
                string response = JsonConvert.SerializeObject(releaseForms);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("releaseforms")]
        public async Task<object> PostReleaseFormAsync(string title)
        {
            try
            {
                return await connector.AddReleaseFormAsync(title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut("releaseforms")]
        public async Task<object> PutReleaseFormAsync(
            int id,
            string title
            )
        {
            try
            {
                return await connector.UpdateReleaseFormAsync(id, title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("releaseforms")]
        public async Task<object> DeleteReleaseFormAsync(int id)
        {
            try
            {
                return await connector.DeleteReleaseFormAsync(id);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
        #endregion

        #region producttypes
        [HttpGet("producttypes")]
        public async Task<object> GetProductTypesAsync()
        {
            try
            {
                List<ProductType> productTypes = await connector.GetProductTypesAsync();
                string response = JsonConvert.SerializeObject(productTypes);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("producttypes")]
        public async Task<object> PostProductTypeAsync(string title)
        {
            try
            {
                return await connector.AddProductTypeAsync(title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut("producttypes")]
        public async Task<object> PutProductTypeAsync(
            int id,
            string title
            )
        {
            try
            {
                return await connector.UpdateProductTypeAsync(id, title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("producttypes")]
        public async Task<object> DeleteProductTypeAsync(int id)
        {
            try
            {
                return await connector.DeleteProductTypeAsync(id);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
        #endregion

        #region manufacturers
        [HttpGet("manufacturers")]
        public async Task<object> GetManufacturersAsync()
        {
            try
            {
                List<Manufacturer> manufacturers = await connector.GetManufacturersAsync();
                string response = JsonConvert.SerializeObject(manufacturers);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("manufacturers")]
        public async Task<object> PostManufacturerAsync(string title)
        {
            try
            {
                return await connector.AddManufacturerAsync(title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPut("manufacturers")]
        public async Task<object> PutManufacturerAsync(
            int id,
            string title
            )
        {
            try
            {
                return await connector.UpdateManufacturerAsync(id, title);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("manufacturers")]
        public async Task<object> DeleteManufacturerAsync(int id)
        {
            try
            {
                return await connector.DeleteManufacturerAsync(id);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
        #endregion
    }
}
