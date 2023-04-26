using DatabaseController.Models;
using DisServer.Connectors;
using DisServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace DisServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserConnector connector;

        public UserController()
        {
            connector = new();
        }


        [HttpGet("auth")]
        public async Task<object> GetUserId(
            string login,
            string pass
            )
        {
            try
            {
                var userId = await connector.GetUserIdAsync(login, pass);
                string response = JsonConvert.SerializeObject(userId);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("info")]
        public async Task<object> GetUserInfo(int id)
        {
            try
            {
                var userInfo = await connector.GetUserInfoByIdAsync(id);
                string response = JsonConvert.SerializeObject(
                    new UserInfoModel() {
                        Id = userInfo.Id,
                        FullName = userInfo.FullName,
                        BirthDate = userInfo.BirthDate,
                        Phone = userInfo.Phone,
                        Gender = userInfo.Gender.Name
                    });
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("shopcart")]
        public async Task<object> PostProductToShopingCart(
            int userId
            )
        {
            try
            {
                var products = await connector.GetShopingCartAsync(userId);

                List<ProductHeaderModel> headers = products.Select(p => new ProductHeaderModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Availability.Price,
                    Assessment = p.Review.Count() > 0 ? p.Review.Sum(p => p.Assessment) / p.Review.Count() : 0,
                    Count = p.Availability.Quantity
                }).ToList();

                string response = JsonConvert.SerializeObject(headers);
                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("shopcart")]
        public async Task<object> PostProductToShopingCart(
            int userId,
            int productId
            )
        {
            try
            {
                await connector.AddProductToShopingCartAsync(userId, productId);
                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpPost("order")]
        public async Task<object> PostProductsToOrder(
            int userId,
            [FromBody] string products
            )
        {
            try
            {
                var productsToOrder = JsonConvert.DeserializeObject<List<ProductToOrderModel>>(products);

                await connector.AddProductsToOrderAsync(productsToOrder, userId);
                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("order/notcompleted")]
        public async Task<object> GetNotCompletedOrders(
            int userId
            )
        {
            try
            {
                var orders = await connector.GetOrdersAsync(userId, false);

                var orderModel = orders.Select(o => new OrderModel()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    GrandTotal = o.GrandTotal,
                    ProductModels = o.OrderProductInfos.Select(p => new OrderProductModel()
                    {
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

        [HttpGet("order/completed")]
        public async Task<object> GetCompletedOrders(
            int userId
            )
        {
            try
            {
                var orders = await connector.GetOrdersAsync(userId, true);

                var orderModel = orders.Select(o => new OrderModel()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    GrandTotal = o.GrandTotal,
                    ProductModels = o.OrderProductInfos.Select(p => new OrderProductModel()
                    {
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
    }
}
