using DatabaseController.Models;
using DatabaseController;
using Microsoft.EntityFrameworkCore;

namespace DisServer.Connectors.Desktop
{
    public class ProductParameterConnector
    {
        #region indication
        public async Task<List<Indication>> GetIndicationsAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.Indications.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddIndicationAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new Indication()
                {
                    Name = title
                };

                context.Indications.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateIndicationAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Indications.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteIndicationAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Indications.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.Indications.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region contraindication
        public async Task<List<Contraindication>> GetContraindicationsAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.Contraindications.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddContraindicationsAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new Contraindication()
                {
                    Name = title
                };

                context.Contraindications.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateContraindicationsAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Contraindications.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteContraindicationsAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Contraindications.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.Contraindications.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region sideEffect
        public async Task<List<SideEffect>> GetSideEffectsAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.SideEffects.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddSideEffectsAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new SideEffect()
                {
                    Name = title
                };

                context.SideEffects.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateSideEffectsAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.SideEffects.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteSideEffectsAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.SideEffects.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.SideEffects.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region producttype
        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.ProductTypes.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddProductTypeAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new ProductType()
                {
                    Name = title
                };

                context.ProductTypes.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateProductTypeAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.ProductTypes.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteProductTypeAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.ProductTypes.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.ProductTypes.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region releaseform
        public async Task<List<ReleaseForm>> GetReleaseFormsAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.ReleaseForms.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddReleaseFormAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new ReleaseForm()
                {
                    Name = title
                };

                context.ReleaseForms.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateReleaseFormAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.ReleaseForms.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteReleaseFormAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.ReleaseForms.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.ReleaseForms.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region manufacturer
        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            try
            {
                using DataContext context = new();

                var models = await context.Manufacturers.ToListAsync();

                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddManufacturerAsync(string title)
        {
            try
            {
                using DataContext context = new();

                var model = new Manufacturer()
                {
                    Name = title
                };

                context.Manufacturers.Add(model);

                await context.SaveChangesAsync();

                return model.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateManufacturerAsync(int id, string title)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Manufacturers.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                model.Name = title;

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteManufacturerAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var model = await context.Manufacturers.Where(i => i.Id == id).FirstOrDefaultAsync();

                if (model == null)
                    return false;

                context.Manufacturers.Remove(model);

                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
