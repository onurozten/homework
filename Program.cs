using System;
using System.Collections.Generic;
using System.Linq;
using HomeWork.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            IHost host = hostBuilder.Build();

            var a = DateTime.Now.AddDays(-5);

           
            using (IServiceScope scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                if (!context.Countries.Any())
                {
                    var c1=new Country
                    {
                        Name = "Türkiye",
                        WeekEndDays = "6,7",
                        CurrencyCode = "TL",
                        PenaltyAmount = 5,
                        CountryOffDays = new List<CountryOffDay>
                        {
                            new CountryOffDay
                            {
                                Name = "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı",
                                Day = 23,
                                Month = 4
                            },
                            new CountryOffDay
                            {
                                Name = "19 Atatürk'ü Anma Gençlik ve Spor Bayramı",
                                Day = 23,
                                Month = 4
                            },
                            new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 1. Günü",
                                Day = 24,
                                Month = 5
                            },new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 2. Günü",
                                Day = 25,
                                Month = 5
                            },
                            new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 3. Günü",
                                Day = 26,
                                Month = 5
                            },
                        }
                    };

                    

                    var c2=new Country
                    {
                        Name = "Birleşik Arap Emirlikleri",
                        WeekEndDays = "5,6",
                        CurrencyCode = "UAD",
                        PenaltyAmount = 6,
                        CountryOffDays = new List<CountryOffDay>
                        {
                            new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 1. Günü",
                                Day = 24,
                                Month = 5
                            },new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 2. Günü",
                                Day = 25,
                                Month = 5
                            },
                            new CountryOffDay
                            {
                                Name = "Ramazan Bayramı 3. Günü",
                                Day = 26,
                                Month = 5
                            },

                        }
                    };

                    context.Countries.Add(c1);
                    context.Countries.Add(c2);
                    context.SaveChanges();
                    
                    if (!context.BookItems.Any())
                    {
                        context.BookItems.Add(new BookItem
                        {
                            Country = c1,
                            BookName = "C# 8.0 in a Nutshell",
                            CheckOutDate = new DateTime(2020, 5, 19),
                            ReturnDate = new DateTime(2020, 5, 30),
                        });
                        context.BookItems.Add(new BookItem
                        {
                            Country = c1,
                            BookName = "Professional Git",
                            CheckOutDate = new DateTime(2020, 4, 18),
                            ReturnDate = new DateTime(2020, 5, 2),
                        });

                        context.BookItems.Add(new BookItem
                        {
                            Country = c2,
                            BookName = "Clean Architecture",
                            CheckOutDate = new DateTime(2020, 5, 9),
                            ReturnDate = new DateTime(2020, 5, 28),
                        });
                        context.SaveChanges();

                    }

                }

            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
