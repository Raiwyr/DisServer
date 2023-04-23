using DatabaseController;
using DatabaseController.Models;
using DisServer.Models;
using DisServer.Services.VectorOptimization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DisServer.Connectors
{
    public class ProductConnector
    {
        private readonly IVectorOptimization vectorOptimization;
        public ProductConnector()
        {
            vectorOptimization = new VectorOptimizationService();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                using DataContext context = new();
                return await context.Products.Include(p => p.Availability).Include(p => p.Review).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                using DataContext context = new();
                await context.SaveChangesAsync();
                var product = await context.Products.Where(p => p.Id == id)
                    .Include(p => p.Availability)
                    .Include(p => p.ProductType)
                    .Include(p => p.ReleaseForm)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Indication)
                    .Include(p => p.Contraindication)
                    .Include(p => p.Review)
                    .FirstOrDefaultAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
            try
            {
                using DataContext context = new();
                return await context.Products
                    .Where(p => p.ProductType.Id == id)
                    .Include(p => p.Availability)
                    .Include(p => p.Review)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id, FilterModel model)
        {
            try
            {
                //p.Locations.Any(l => searchIds.Contains(l.Id))
                using DataContext context = new();
                return await context.Products
                    .Include(p => p.Availability)
                    .Include(p => p.Review)
                    .Include(p => p.Indication)
                    .Where(p => p.ProductType.Id == id)
                    .Where(p => model.MinPrice != null ? p.Availability.Price >= model.MinPrice : true)
                    .Where(p => model.MaxPrice != null ? p.Availability.Price <= model.MaxPrice : true)
                    .Where(p => (model.IndicationsIds != null && model.IndicationsIds.Count() > 0) ? p.Indication.Any(l => model.IndicationsIds.Contains(l.Id)) : true)
                    .Where(p => (model.ManufacturersIds != null && model.ManufacturersIds.Count() > 0) ? model.ManufacturersIds.Contains(p.ManufacturerId) : true)
                    .Where(p => (model.QuantityPackage != null && model.QuantityPackage.Count() > 0) ? model.QuantityPackage.Contains(p.QuantityPackage) : true)
                    .Where(p => (model.ReleaseFormsIds != null && model.ReleaseFormsIds.Count() > 0) ? model.ReleaseFormsIds.Contains(p.ReleaseFormId) : true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        .Include(p => p.Availability)
                        .Include(p => p.Review)
                        .LikeAny(paramsForLikeSearch).ToListAsync();
                    results = results.OrderByDescending(p => listParams.Where(l => p.Name.ToLower().Contains(l.ToLower())).ToList().Count()).ToList();

                    return results;
                }
                else
                {
                    var results = await context.Products
                        .Include(p => p.Availability)
                        .Include(p => p.Review)
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

        public async Task<List<Product>> SearchProductsAsync(string searchParam, FilterModel model)
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
                        .Include(p => p.Availability)
                        .Include(p => p.Review)
                        .Include(p => p.Indication)
                        .LikeAny(paramsForLikeSearch)
                        .Where(p => model.MinPrice != null ? p.Availability.Price >= model.MinPrice : true)
                        .Where(p => model.MaxPrice != null ? p.Availability.Price <= model.MaxPrice : true)
                        .Where(p => (model.IndicationsIds != null && model.IndicationsIds.Count() > 0) ? p.Indication.Any(l => model.IndicationsIds.Contains(l.Id)) : true)
                        .Where(p => (model.ManufacturersIds != null && model.ManufacturersIds.Count() > 0) ? model.ManufacturersIds.Contains(p.ManufacturerId) : true)
                        .Where(p => (model.QuantityPackage != null && model.QuantityPackage.Count() > 0) ? model.QuantityPackage.Contains(p.QuantityPackage) : true)
                        .Where(p => (model.ReleaseFormsIds != null && model.ReleaseFormsIds.Count() > 0) ? model.ReleaseFormsIds.Contains(p.ReleaseFormId) : true)
                        .ToListAsync();
                    results = results.OrderByDescending(p => listParams.Where(l => p.Name.ToLower().Contains(l.ToLower())).ToList().Count()).ToList();

                    return results;
                }
                else
                {
                    var results = await context.Products
                        .Include(p => p.Availability)
                        .Include(p => p.Review)
                        .Include(p => p.Indication)
                        .Where(p => EF.Functions.Like(p.Name, $"%{searchParam}%"))
                        .Where(p => model.MinPrice != null ? p.Availability.Price >= model.MinPrice : true)
                        .Where(p => model.MaxPrice != null ? p.Availability.Price <= model.MaxPrice : true)
                        .Where(p => (model.IndicationsIds != null && model.IndicationsIds.Count() > 0) ? p.Indication.Any(l => model.IndicationsIds.Contains(l.Id)) : true)
                        .Where(p => (model.ManufacturersIds != null && model.ManufacturersIds.Count() > 0) ? model.ManufacturersIds.Contains(p.ManufacturerId) : true)
                        .Where(p => (model.QuantityPackage != null && model.QuantityPackage.Count() > 0) ? model.QuantityPackage.Contains(p.QuantityPackage) : true)
                        .Where(p => (model.ReleaseFormsIds != null && model.ReleaseFormsIds.Count() > 0) ? model.ReleaseFormsIds.Contains(p.ReleaseFormId) : true)
                        .ToListAsync();
                    return results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> SelectionProductAsync(SelectionParameterModel model)
        {
            try
            {
                using DataContext context = new();
                await context.SaveChangesAsync();
                var product = await context.Products
                    .Include(p => p.Indication)
                    .Include(p => p.Contraindication)
                    .Include(p => p.Availability)
                    .Include(p => p.Review)
                    .Where(p => p.Indication.Where(i => i.Id == model.IndicationId).Count() > 0)
                    .ToListAsync();

                if (model.Availability == false)
                    product = product.Where(p => p.Availability.Quantity > 0).ToList();

                Vector contraindicationVector = new() { Values = new() };
                Vector priseVector = new() { Values = new() };
                Vector assessmentVector = new() { Values = new() };
                Vector reviewsVector = new() { Values = new() };

                foreach (var item in product)
                {
                    if (model.ContraindicationIds != null && model.evaluationContraindication != null)
                        contraindicationVector.Values.Add(
                            item.Id,
                            item.Contraindication.Select(i => i.Id).Intersect(model.ContraindicationIds).Count());

                    if (model.PriseSort != null && model.evaluationPrise != null)
                        priseVector.Values.Add(item.Id, item.Availability.Price);

                    if (model.AssessmentSort != null && model.evaluationAssessment != null)
                    {
                        double assessment;

                        if (item.Review?.Any() ?? false)
                        {
                            assessment = (double)item.Review.Sum(r => r.Assessment) / item.Review.Count();
                        }
                        else
                            assessment = 0;

                        assessmentVector.Values.Add(
                            item.Id,
                            assessment);
                    }

                    if(model.ReviewsSort != null && model.evaluationReviews != null)
                        reviewsVector.Values.Add(
                            item.Id,
                            (item.Review?.Any() ?? false) ? item.Review.Count() : 0);
                }

                List<Vector> vectors = new();

                if (model.ContraindicationIds != null && model.evaluationContraindication != null)
                {
                    contraindicationVector.Weight = (int)model.evaluationContraindication;
                    contraindicationVector.NeedMaximize = false;
                    vectors.Add(contraindicationVector);
                }
                if (model.PriseSort != null && model.evaluationPrise != null)
                {
                    priseVector.Weight = (int)model.evaluationPrise;
                    priseVector.NeedMaximize = (bool)model.PriseSort;
                    vectors.Add(priseVector);
                }

                if (model.AssessmentSort != null && model.evaluationAssessment != null)
                {
                    assessmentVector.Weight = (int)model.evaluationAssessment;
                    assessmentVector.NeedMaximize = (bool)model.AssessmentSort;
                    vectors.Add(assessmentVector);
                }

                if (model.ReviewsSort != null && model.evaluationReviews != null)
                {
                    reviewsVector.Weight = (int)model.evaluationReviews;
                    reviewsVector.NeedMaximize = (bool)model.ReviewsSort;
                    vectors.Add(reviewsVector);
                }

                var resultsIds = vectorOptimization.Optimize(vectors);

                List<int> sortIds = resultsIds
                    .Select(d => new KeyValuePair<int, double>(d.Key, d.Value))
                    .OrderByDescending(v => v.Value)
                    .Select(k => k.Key).ToList();

                product = product.OrderBy(x => sortIds.IndexOf(x.Id)).ToList();

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<FilterParameterModel> GetFiltersAsync(List<int> productIds)
        {
            try
            {
                using DataContext context = new();

                var indications = await context.Indications.Include(i => i.Products).Where(i => i.Products.Any(p => productIds.Contains(p.Id))).ToListAsync();
                var releasForms = await context.ReleaseForms.Include(i => i.Products).Where(i => i.Products.Any(p => productIds.Contains(p.Id))).ToListAsync();
                var manufacturers = await context.Manufacturers.Include(i => i.Products).Where(i => i.Products.Any(p => productIds.Contains(p.Id))).ToListAsync();
                var quantityPackages = (await context.Products.Where(p => productIds.Contains(p.Id)).Select(p => p.QuantityPackage).Distinct().ToListAsync()).OrderBy(i => i).ToList();

                return new FilterParameterModel()
                {
                    Indications = indications.Select(r => new Parameter() { Id = r.Id, Name = r.Name }).ToList(),
                    ReleaseForms = releasForms.Select(r => new Parameter() { Id = r.Id, Name = r.Name }).ToList(),
                    Manufacturers = manufacturers.Select(r => new Parameter() { Id = r.Id, Name = r.Name }).ToList(),
                    QuantityPackage = quantityPackages
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    

    public static class Extensions
    {
        public static IQueryable<Product> LikeAny(this IQueryable<Product> products, params string[] words)
        {
            var parameter = Expression.Parameter(typeof(Product));

            var body = words.Select(word => Expression.Call(typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like),
                                                                                                    new[]
                                                                                                    {
                                                                                                        typeof(DbFunctions), typeof(string), typeof(string)
                                                                                                    }),
                                                            Expression.Constant(EF.Functions),
                                                            Expression.Property(parameter, typeof(Product).GetProperty(nameof(Product.Name))),
                                                            Expression.Constant(word)))
                            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.OrElse(current, call) : (Expression)call);

            return products.Where(Expression.Lambda<Func<Product, bool>>(body, parameter));
        }

        public static IQueryable<Product> LikeAll(this IQueryable<Product> products, params string[] words)
        {
            var parameter = Expression.Parameter(typeof(Product));

            var body = words.Select(word => Expression.Call(typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like),
                                                                                                    new[]
                                                                                                    {
                                                                                                        typeof(DbFunctions), typeof(string), typeof(string)
                                                                                                    }),
                                                            Expression.Constant(EF.Functions),
                                                            Expression.Property(parameter, typeof(Product).GetProperty(nameof(Product.Name))),
                                                            Expression.Constant(word)))
                            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.AndAlso(current, call) : (Expression)call);

            return products.Where(Expression.Lambda<Func<Product, bool>>(body, parameter));
        }
    }
}
