using DatabaseController;
using DatabaseController.Models;
using DisServer.Enums;
using DisServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DisServer.Connectors
{
    public class UserConnector
    {
        public async Task<int?> GetUserIdAsync(string login, string password)
        {
            try
            {
                using DataContext context = new();
                var user = await context.Users.Where(u => u.Login == login).FirstOrDefaultAsync();
                return user?.Password == password ? user.Id : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            try
            {
                using DataContext context = new();
                var user = await context.UserInfos.Include(u => u.Gender).Where(u => u.Id == id).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddProductToShopingCartAsync(int userId, int productId)
        {
            try
            {
                using DataContext context = new();
                var user = await context.Users.Include(u => u.Products).Where(u => u.Id == userId).FirstOrDefaultAsync();
                var product = await context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
                user.Products.Add(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetShopingCartAsync(int UserId)
        {
            try
            {
                using DataContext context = new();
                var user = await context.Users
                    .Include(u => u.Products)
                    .ThenInclude(p => p.Availability)
                    .Include(u => u.Products)
                    .ThenInclude(p => p.Review)
                    .Where(u => u.Id == UserId).FirstOrDefaultAsync();
                if(user?.Products != null)
                    return user.Products.ToList();
                else
                    return new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddProductsToOrderAsync(List<ProductToOrderModel> productsToOrder, int userId) {

            using DataContext context = new();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                
                var listProductIds = productsToOrder.Select(p => p.Id).ToList();
                var products = await context.Products.Include(p => p.Availability).Where(p => listProductIds.Contains(p.Id)).ToListAsync();
                var user = await context.Users
                    .Include(u => u.Orders)
                    .Include(p => p.Products)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                int grandTotal = 0;
                foreach (var product in products)
                {
                    grandTotal += product.Availability.Price * productsToOrder.Where(p => p.Id == product.Id).First().Count;
                }

                Order order = new()
                {
                    OrderDate = DateTime.Now,
                    OrderStatus = OrderStatus.InProcessing,
                    GrandTotal = grandTotal
                };

                user.Orders.Add(order);
                order.OrderProductInfos = new();
                foreach (var product in products)
                {
                    order.OrderProductInfos.Add(new OrderProductInfo()
                    {
                        Product = product,
                        ProductQuantity = productsToOrder.Where(p => p.Id == product.Id).First().Count,
                        Price = product.Availability.Price
                    });
                }

                foreach (var product in products)
                    user.Products.Remove(product);

                context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Order>> GetOrdersAsync(int userId, bool getCompletedOrders) {
            try {
                using DataContext context = new();
                var user = await context.Users
                    .Include(u => u.Orders)
                        .ThenInclude(o => o.Products)
                            .ThenInclude(p => p.Availability)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                List<Order> orders = new();

                if(getCompletedOrders)
                    orders = user?.Orders.Where(o => o.OrderStatus == OrderStatus.Completed).ToList() ?? new();
                else
                    orders = user?.Orders.Where(o => o.OrderStatus != OrderStatus.Completed).ToList() ?? new();

                return orders;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
