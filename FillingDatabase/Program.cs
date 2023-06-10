using DatabaseController;
using DatabaseController.Models;
using DisServer.Services;
using DisServer.Services.VectorOptimization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
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
            //controller.AddGenders();
            //controller.AddUsers(10);
            //controller.AddWorkers();
            //controller.AddProducts(100, true);
            controller.AddReviewsById(1);
            Console.ReadKey();
        }
    }

    public class Controller
    {
        public void AddReviewsById(int id) {

            List<Review> reviews = new();

            reviews.Add(new Review()
            {
                Message = "Хороший товар",
                Assessment = 4,
                UserId = 1,
                UserName = "Александр",
                DateReview = DateTime.Now
            });
            reviews.Add(new Review()
            {
                Message = "Помогло",
                Assessment = 5,
                UserId = 1,
                UserName = "Артемий",
                DateReview = DateTime.Now
            });
            reviews.Add(new Review()
            {
                Message = "",
                Assessment = 4,
                UserId = 1,
                UserName = "Михаил",
                DateReview = DateTime.Now
            });
            reviews.Add(new Review()
            {
                Message = "Не помогло",
                Assessment = 2,
                UserId = 1,
                UserName = "Anonim",
                DateReview = DateTime.Now
            });
            reviews.Add(new Review()
            {
                Message = "Хорошо",
                Assessment = 5,
                UserId = 1,
                UserName = "Степан Лавкрафтович",
                DateReview = DateTime.Now
            });

            using DataContext data = new();

            var product = data.Products.Where(p => p.Id == id).FirstOrDefault();

            if (product != null) {
                product.Review = reviews;
            }

            data.SaveChanges();

            Console.WriteLine("Reviews added");
        }

        public void AddWorkers()
        {
            try
            {
                using DataContext data = new();

                data.Workers.Add(
                    new()
                    {
                        Login = "user",
                        Password = "user",
                        FullName = "Юзер юзерович",
                        isAdmin = false
                    });

                //data.Workers.Add(
                //    new()
                //    {
                //        Login = "admin",
                //        Password = "admin",
                //        FullName = "Админ админович",
                //    });

                data.SaveChanges();
                Console.WriteLine("Workers added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

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

        public void AddSideEffects(int count)
        {
            try
            {
                using DataContext data = new();

                for (int i = 1; i <= count; i++)
                    data.SideEffects.Add(new SideEffect() { Name = "Побочка" + i });

                data.SaveChanges();
                Console.WriteLine($"{count} SideEffects added");
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
                    data.ReleaseForms.Add(new ReleaseForm() { Name = "Форма_выпуска" + i });

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

                User userAdmin = new()
                {
                    Login = "admin",
                    Password = "admin"
                };

                data.Users.Add(userAdmin);

                UserInfo userInfoAdmin = new()
                {
                    Id = -1,
                    FullName = "Админ Админов Админович",
                    Gender = genders[random.Next(genders.Count)],
                    BirthDate = DateTime.Now,
                    Phone = "88888888888",
                    User = userAdmin
                };

                data.UserInfos.Add(userInfoAdmin);

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
                    AddSideEffects(30);
                    AddManufacturers(10);
                    AddProductTypes(10);
                    AddReleaseForms(10);
                }

                using DataContext data = new();

                var indications = data.Indications.ToList();
                var contraindications = data.Contraindications.ToList();
                var sideEffects = data.SideEffects.ToList();
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

                            reviews.Add(new Review()
                            {
                                Message = "Хорошо",
                                Assessment = random.Next(1, 6),
                                UserId = random.Next(1, 11),
                                UserName = "Anonim",
                                DateReview = DateTime.Now
                            });
                        }
                        else
                        {
                            reviews.Add(new Review()
                            {
                                Message = "Плохо",
                                Assessment = random.Next(1, 6),
                                UserId = random.Next(1, 11),
                                UserName = "Anonim",
                                DateReview = DateTime.Now
                            });
                         }
                    }

                    double verQuantityPackage = random.NextDouble();

                    data.Products.Add(new Product()
                    {
                        Name = $"Товар {i}",
                        ImageName = "Product_102.jpg",
                        Composition = "Состав",
                        Dosage = "Доза",
                        QuantityPackage = verQuantityPackage <= 0.3 ? 5 : (verQuantityPackage <= 0.6 ? 10 : 15),
                        ExpirationDate = DateTime.Now,
                        ProductType = productTypes[random.Next(productTypes.Count)],
                        ReleaseForm = releaseForms[random.Next(releaseForms.Count)],
                        Indication = indications.OrderBy(x => random.Next()).Take(5).ToList(),
                        Contraindication = contraindications.OrderBy(x => random.Next()).Take(5).ToList(),
                        SideEffect = sideEffects.OrderBy(x => random.Next()).Take(5).ToList(),
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