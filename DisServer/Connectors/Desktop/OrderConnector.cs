using DatabaseController.Models;
using DatabaseController;
using DisServer.Enums;
using Microsoft.EntityFrameworkCore;

namespace DisServer.Connectors.Desktop
{
    public class OrderConnector
    {
        public async Task<List<Order>> GetOrdersAsync()
        {
            try
            {
                using DataContext context = new();
                var orders = await context.Orders.ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            try
            {
                using DataContext context = new();
                var order = await context.Orders
                    .Where(o => o.Id == id)
                        .Include(o => o.Products)
                            .ThenInclude(p => p.Availability)
                    .FirstOrDefaultAsync();

                if (order == null)
                    throw new Exception();

                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            try
            {
                using DataContext context = new();
                var order = await context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();

                if (order == null)
                    throw new Exception();

                order.OrderStatus = status;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
