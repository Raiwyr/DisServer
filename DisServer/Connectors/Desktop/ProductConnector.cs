using DatabaseController;
using DatabaseController.Models;
using DisServer.Connectors.Mobile;
using DisServer.Models.Desktop;
using Microsoft.EntityFrameworkCore;

namespace DisServer.Connectors.Desktop
{
    public class ProductConnector
    {
        private static readonly string FilesFolderPath = Environment.CurrentDirectory + "\\Files\\";

        public async Task<Product> GetProductAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var product = await context.Products
                    .Include(p => p.Indication)
                    .Include(p => p.Contraindication)
                    .Include(p => p.SideEffect)
                    .Include(p => p.Availability)
                    .Include(p => p.ProductType)
                    .Include(p => p.ReleaseForm)
                    .Include(p => p.Manufacturer)
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();

                if (product == null)
                    throw new Exception();

                return product;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductHeaderModel>> GetProductHeadersAsync()
        {
            try
            {
                using DataContext context = new();

                var products = await context.Products.Select(p =>
                    new ProductHeaderModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageName = p.ImageName ?? ""
                    }).ToListAsync();

                return products;
            }
            catch
            {
                throw;
            }
        }

        public async Task PostProductAsync(PostProductModel model)
        {
            try
            {
                using DataContext context = new();

                var indications = await context.Indications.Where(i => model.IndicationIds.Contains(i.Id)).ToListAsync();
                var contraindications = await context.Contraindications.Where(c => model.ContraindicationIds.Contains(c.Id)).ToListAsync();
                var sideEffect = await context.SideEffects.Where(c => model.SideEffectIds.Contains(c.Id)).ToListAsync();
                var productType = await context.ProductTypes.Where(p => p.Id == model.ProductTypeId).FirstOrDefaultAsync();
                var releaseForm = await context.ReleaseForms.Where(r => r.Id == model.ReleaseFormId).FirstOrDefaultAsync();
                var manufacturer = await context.Manufacturers.Where(r => r.Id == model.ManufacturerId).FirstOrDefaultAsync();

                if (
                    indications.Count > 0 &&
                    contraindications.Count > 0 &&
                    sideEffect.Count > 0 &&
                    productType != null &&
                    releaseForm != null &&
                    manufacturer != null
                    )
                {
                    Product product = new Product()
                    {
                        Name = model.Name,
                        ExpirationDate = DateTime.Now,
                        Dosage = model.Dosage,
                        Composition = model.Composition,
                        QuantityPackage = model.QuantityPackage,
                        Availability = new Availability()
                        {
                            Quantity = model.Quantity,
                            Price = model.Price
                        },
                        ProductType = productType,
                        ReleaseForm = releaseForm,
                        Manufacturer = manufacturer,
                        Indication = indications,
                        Contraindication = contraindications,
                        SideEffect = sideEffect
                    };

                    context.Products.Add(product);
                    await context.SaveChangesAsync();

                    if (model.Image.Length > 0)
                    {
                        int id = product.Id;

                        string ext = Path.GetExtension(model.ImageName);

                        var imageName = $"Product_{id}" + ext;

                        if (!Directory.Exists(FilesFolderPath))
                            Directory.CreateDirectory(FilesFolderPath);
                        File.WriteAllBytes(FilesFolderPath + imageName, model.Image);

                        product.ImageName = imageName;
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task PutProductAsync(int productId, PostProductModel model)
        {
            try
            {
                using DataContext context = new();

                var product = await context.Products
                    .Include(p => p.Availability)
                    .Include(p => p.ProductType)
                    .Include(p => p.ReleaseForm)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Indication)
                    .Include(p => p.Contraindication)
                    .Include(p => p.SideEffect)
                    .Where(p => p.Id == productId).FirstOrDefaultAsync();
                var indications = await context.Indications.Where(i => model.IndicationIds.Contains(i.Id)).ToListAsync();
                var contraindications = await context.Contraindications.Where(c => model.ContraindicationIds.Contains(c.Id)).ToListAsync();
                var sideEffect = await context.SideEffects.Where(c => model.SideEffectIds.Contains(c.Id)).ToListAsync();
                var productType = await context.ProductTypes.Where(p => p.Id == model.ProductTypeId).FirstOrDefaultAsync();
                var releaseForm = await context.ReleaseForms.Where(r => r.Id == model.ReleaseFormId).FirstOrDefaultAsync();
                var manufacturer = await context.Manufacturers.Where(r => r.Id == model.ManufacturerId).FirstOrDefaultAsync();

                if (
                    product != null &&
                    indications.Count > 0 &&
                    contraindications.Count > 0 &&
                    productType != null &&
                    releaseForm != null &&
                    manufacturer != null
                    )
                {
                    product.Name = model.Name;
                    product.Dosage = model.Dosage;
                    product.Composition = model.Composition;
                    product.QuantityPackage = model.QuantityPackage;
                    if (product.Availability != null)
                    {
                        product.Availability.Quantity = model.Quantity;
                        product.Availability.Price = model.Price;
                    }
                    if (product.ProductType != null)
                        product.ProductType = productType;
                    if (product.ReleaseForm != null)
                        product.ReleaseForm = releaseForm;
                    if (product.Manufacturer != null)
                        product.Manufacturer = manufacturer;
                    product.Indication = indications;
                    product.Contraindication = contraindications;
                    product.SideEffect = sideEffect;

                    await context.SaveChangesAsync();

                    if (model.Image.Length > 0)
                    {
                        int id = product.Id;

                        string ext = Path.GetExtension(model.ImageName);

                        var imageName = $"Product_{id}" + ext;

                        if (!Directory.Exists(FilesFolderPath))
                            Directory.CreateDirectory(FilesFolderPath);
                        File.WriteAllBytes(FilesFolderPath + imageName, model.Image);

                        product.ImageName = imageName;
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string searchParam)
        {
            try
            {
                if (string.IsNullOrEmpty(searchParam))
                {
                    return new List<Product>();
                }

                var listParams = searchParam.Split(' ');

                using DataContext context = new();

                if (listParams.Length > 1)
                {
                    var paramsForLikeSearch = new string[listParams.Length];
                    for (int i = 0; i < listParams.Length; i++)
                        paramsForLikeSearch[i] = string.Format($"%{listParams[i]}%");

                    var results = await context.Products
                        .LikeAny(paramsForLikeSearch).ToListAsync();
                    results = results.OrderByDescending(p => listParams.Where(l => p.Name.ToLower().Contains(l.ToLower())).ToList().Count()).ToList();

                    return results;
                }
                else
                {
                    var results = await context.Products
                        .Where(p => EF.Functions.Like(p.Name, $"%{searchParam}%")).ToListAsync();
                    return results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var product = await context.Products
                    .Include(p => p.Availability)
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();

                if (product == null)
                    throw new Exception();

                context.Products.Remove(product);

                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> GetImageBytes(string name)
        {
            try
            {
                string path = FilesFolderPath + name;

                byte[] imageByte = await System.IO.File.ReadAllBytesAsync(path);

                return imageByte;
            }
            catch
            {
                throw;
            }
        }
    }
}
