using DatabaseController;
using DatabaseController.Models;
using DisServer.Enums;
using DisServer.Models.Mobile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq;

namespace DisServer.Connectors.Mobile
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
                await context.SaveChangesAsync();
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
                if (user?.Products != null)
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

        public async Task AddProductsToOrderAsync(List<ProductToOrderModel> productsToOrder, int userId)
        {

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

                await context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Order>> GetOrdersAsync(int userId, bool getCompletedOrders)
        {
            try
            {
                using DataContext context = new();
                var user = await context.Users
                    .Include(u => u.Orders)
                        .ThenInclude(o => o.Products)
                            .ThenInclude(p => p.Availability)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                List<Order> orders = new();

                if (getCompletedOrders)
                    orders = user?.Orders.Where(o => o.OrderStatus == OrderStatus.Completed).ToList() ?? new();
                else
                    orders = user?.Orders.Where(o => o.OrderStatus != OrderStatus.Completed).ToList() ?? new();

                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task PostReview(ReviewModel review)
        {
            try
            {
                using DataContext context = new();
                var user = await context.Users.Where(u => u.Id == review.UserId).FirstOrDefaultAsync();
                var product = await context.Products.Include(p => p.Review).Where(p => p.Id == review.ProductId).FirstOrDefaultAsync();

                if (user == null || product == null)
                    throw new Exception();

                product.Review.Add(new()
                {
                    UserId = review.UserId,
                    Assessment = review.Assessment,
                    Message = review.Message,
                    DateReview = review.DateReview,
                    UserName = review.UserName
                });

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            try
            {
                using DataContext context = new();

                var genders = await context.Genders.ToListAsync();

                return genders;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateUserInfoAsync(UserInfoModel userInfoModel)
        {
            try
            {
                using DataContext context = new();

                var gender = await context.Genders.Where(g => g.Id == userInfoModel.Gender.Id).FirstOrDefaultAsync();
                var userInfo = await context.UserInfos.Include(u => u.Gender).Where(u => u.Id == userInfoModel.Id).FirstOrDefaultAsync();

                if (userInfo != null && gender != null)
                {
                    userInfo.FullName = userInfoModel.FullName;
                    userInfo.BirthDate = userInfoModel.BirthDate;
                    userInfo.Phone = userInfoModel.Phone;
                    userInfo.Gender = gender;

                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddUserAsync(RegistrationModel registrationModel)
        {
            try
            {
                using DataContext context = new();

                var gender = await context.Genders.Where(g => g.Id == registrationModel.GenderId).FirstOrDefaultAsync();

                if (gender == null)
                    throw new Exception();

                var userInfo = new UserInfo()
                {
                    FullName = registrationModel.FullName,
                    BirthDate = registrationModel.BirthDate,
                    Phone = registrationModel.Phone,
                    Gender = gender
                };

                var user = new User()
                {
                    Login = registrationModel.Username,
                    Password = registrationModel.Password,
                    UserInfo = userInfo
                };

                context.Users.Add(user);

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
