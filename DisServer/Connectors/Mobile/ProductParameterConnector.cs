using DatabaseController.Models;
using DatabaseController;
using Microsoft.EntityFrameworkCore;

namespace DisServer.Connectors.Mobile
{
    public class ProductParameterConnector
    {
        public async Task<List<Indication>> GetIndicationsAsync()
        {
            try
            {
                using DataContext context = new();
                return await context.Indications.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Contraindication>> GetContraindicationsAsync()
        {
            try
            {
                using DataContext context = new();
                return await context.Contraindications.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<SideEffect>> GetSideEffectsAsync()
        {
            try
            {
                using DataContext context = new();
                return await context.SideEffects.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<ProductType>> GetCategoriesAsync()
        {
            try
            {
                using DataContext context = new();
                return await context.ProductTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
