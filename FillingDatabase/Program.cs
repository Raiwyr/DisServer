using DatabaseController;
using DatabaseController.Models;
using DisServer.Services;
using DisServer.Services.VectorOptimization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FillingDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new();
            using (DataContext context = new())
            {
                context.Database.EnsureDeleted();
            }
            controller.AddGenders();
            controller.AddUsers(10);
            controller.AddProducts(100, true);
            Console.ReadKey();
        }
    }

    public class Controller
    {
        public void AddGenders()
        {
            try
            {
                using DataContext data = new();

                List<Gender> genders = new List<Gender>
                {
                new Gender() { Name = "Male" },
                new Gender() { Name = "Female" }
                };
                genders.ForEach(n => data.Genders.Add(n));

                data.SaveChanges();
                Console.WriteLine("Genders added");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddPaymentTypes()
        {
            try
            {
                using DataContext data = new();

                List<PaymentType> genders = new List<PaymentType>
                {
                new PaymentType() { Name = "Cash" },
                new PaymentType() { Name = "Card" }
                };
                genders.ForEach(n => data.PaymentTypes.Add(n));
                
                data.SaveChanges();
                Console.WriteLine("PaymentTypes added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public void AddIndications(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.Indications.Add(new Indication() { Name = "Показание" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} Indications added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddContraindications(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.Contraindications.Add(new Contraindication() { Name = "Противопоказание" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} Contraindications added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddManufacturers(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.Manufacturers.Add(new Manufacturer() { Name = "Производитель" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} Manufacturers added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddProductTypes(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.ProductTypes.Add(new ProductType() { Name = "Тип_продукта" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} ProductTypes added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddReleaseForms(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.ReleaseForms.Add(new ReleaseForm() { Name = "Тип_продукта" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} ReleaseForms added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public void AddUsers(int count)
        {
            try
            {
                Random random = new ();

                using DataContext data = new();

                var genders = data.Genders.ToList();

                for (int i = 1; i <= count; i++)
                {
                    User user = new()
                    {
                        Login = "User" + i,
                        Password = "pass"
                    };

                    data.Users.Add(user);

                    UserInfo userInfo = new()
                    {
                        Id = -1,
                        FullName = "Full Name",
                        Gender = genders[random.Next(genders.Count)],
                        BirthDate = DateTime.Now,
                        Phone = "88888888888",
                        User = user
                    };

                    data.UserInfos.Add(userInfo);
                }

                data.SaveChanges();
                Console.WriteLine($"{count} Users added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddProducts(int count, bool addDependentЕables = false)
        {
            try
            {
                Random random = new();
                if (addDependentЕables)
                {
                    AddIndications(30);
                    AddContraindications(30);
                    AddManufacturers(10);
                    AddProductTypes(10);
                    AddReleaseForms(10);
                }

                using DataContext data = new();

                var indications = data.Indications.ToList();
                var contraindications = data.Contraindications.ToList();
                var manufacturers = data.Manufacturers.ToList();
                var productTypes = data.ProductTypes.ToList();
                var releaseForms = data.ReleaseForms.ToList();

                for (int i = 1; i <= count; i++)
                {
                    List<Review> reviews = new();
                    var countComments = random.Next(5, 11);
                    var countAssessments = random.Next(5, 31);
                    for (int j = 0; j < countComments; j++)
                    {
                        if (random.NextDouble() >= 0.5)
                        {

                            reviews.Add(new Review() { Message = "Хорошо", Assessment = random.Next(1, 6), UserId = random.Next(1, 11) });
                        }
                        else
                        {
                            reviews.Add(new Review() { Message = "Плохо", Assessment = random.Next(1, 6), UserId = random.Next(1, 11) });
                        }
                    }

                    data.Products.Add(new Product()
                    {
                        Name = $"Товар {i}",
                        Composition = "Состав",
                        Dosage = "Доза",
                        ExpirationDate = DateTime.Now,
                        ProductType = productTypes[random.Next(productTypes.Count)],
                        ReleaseForm = releaseForms[random.Next(releaseForms.Count)],
                        Indication = indications.OrderBy(x => random.Next()).Take(5).ToList(),
                        Contraindication = contraindications.OrderBy(x => random.Next()).Take(5).ToList(),
                        Manufacturer = manufacturers[random.Next(manufacturers.Count)],
                        Availability = new()
                        {
                            Quantity = random.Next(10, 101),
                            Price = random.Next(100, 1001)
                        },
                        Review = reviews
                    });
                }

                data.SaveChanges();
                Console.WriteLine($"{count} Products added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}