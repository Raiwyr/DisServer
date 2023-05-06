using DisServer.Connectors.Desktop;
using DisServer.Enums;
using DisServer.Models.Desktop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Desktop
{
    [Route("api/desktop/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderConnector connector;

        public OrderController()
        {
            connector = new();
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<object> GetOrders()
        {
            try
            {
                var orders = await connector.GetOrdersAsync();

                var orderModel = orders.Select(o => new OrderModel()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    GrandTotal = o.GrandTotal,
                    ProductModels = o.OrderProductInfos.Select(p => new OrderProductModel()
                    {
                        Id =p.ProductId,
                        Name = p.Product.Name,
                        Price = p.Price,
                        Count = p.ProductQuantity
                    }).ToList()
                });

                var response = JsonConvert.SerializeObject(orderModel);

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("status")]
        public async Task<object> GetOrderStatuses()
        {
            try
            {
                List<string> statuses = new()
                {
                    OrderStatus.Done,
                    OrderStatus.Completed,
                    OrderStatus.InProcessing,
                    OrderStatus.BeingCollected
                };

                var response = JsonConvert.SerializeObject(statuses);

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<object> PutOrderStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case OrderStatus.Done: return await connector.UpdateOrderStatusAsync(id, OrderStatus.Done);
                    case OrderStatus.Completed: return await connector.UpdateOrderStatusAsync(id, OrderStatus.Completed);
                    case OrderStatus.InProcessing: return await connector.UpdateOrderStatusAsync(id, OrderStatus.InProcessing);
                    case OrderStatus.BeingCollected: return await connector.UpdateOrderStatusAsync(id, OrderStatus.BeingCollected);
                    default: return false;
                }
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
    }
}
