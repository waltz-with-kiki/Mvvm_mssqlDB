using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Hash;

namespace Try2.Data
{
     class DbInitializer
    {
        private readonly UsersContext _db;
        private readonly ILogger<DbInitializer> _logger;
        
        


        public DbInitializer(UsersContext db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();

            _logger.LogInformation("Добавление миграции...");
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation($"Добавление миграции выполнилось за {timer.ElapsedMilliseconds} мс...");

            if (await _db.Users.AnyAsync()) return;

            await InitializeRights();
            await InitializeUsers();
            await InitializeBanks();
            await InitializeChecking_Accounts();
            await InitializeClients();
            await InitializeCrews();
            await InitializeBrands();
            await InitializeAutomobiles();
            await InitializeDriverCategories();
            await InitializeDriverClasses();
            await InitializeDrivers();
            await InitializeFlights();
            await InitializeOrders();
            await InitializeUnits();
            await InitializeCargos();

            _logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private Right[] Rights;

        private async Task InitializeRights()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Прав...");
            Rights = new Right[]{
                            new Right { Name = "Полный", D = true, E = true, R = true, W = true },
                            new Right { Name = "Просмотр", R = true},
                            new Right { Name = "Базовый", R = false, W = false, E = false, D = false },
                            };

            await _db.Right.AddRangeAsync(Rights);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Прав выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private User[] Users;

        private async Task InitializeUsers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Пользователей...");

            Users = new User[]{
                           new User
                    {
                        Name = "Admin",
                        Password = md5.hashPassword("1234"),
                        Right = Rights[0]
                    },
                    new User
                    {
                        Name = "User",
                        Password = md5.hashPassword("1234"),
                        Right = Rights[1]
                    },
                    new User
                    {
                        Name = "Nicholas P",
                        Password = md5.hashPassword("12345"),
                        Right = Rights[1]
                    }
            };

            await _db.Users.AddRangeAsync(Users);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Пользователей выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _BankCount = 10;

        private Bank[] _Banks;

        private async Task InitializeBanks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация банков...");

            _Banks = new Bank[_BankCount];
            for (var i = 0; i < _BankCount; i++)
                _Banks[i] = new Bank { Name = $"Банк {i + 1}" };

            await _db.Banks.AddRangeAsync(_Banks);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация банков выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _Checking_AccountCount = 105;

        private Checking_account[] _Checking_Accounts;

        private async Task InitializeChecking_Accounts()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация банков...");

            var rnd = new Random();

            _Checking_Accounts = Enumerable.Range(1, _Checking_AccountCount)
                .Select(i => new Checking_account
                {
                    Check = $"{4062326883030312456 + i}7",
                    Bank = rnd.NextItem(_Banks)

                }).ToArray();

            await _db.Checking_Accounts.AddRangeAsync(_Checking_Accounts);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация счетов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _ClientsCount = 250;

        private Client[] _Clients;

        private bool[] TrueOrFalse = { true, false }; 

        private async Task InitializeClients()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация банков...");

            var rnd = new Random();

            _Clients = new Client[_ClientsCount];
            for (var i = 0; i < _ClientsCount; i++)
            {
                int code = rnd.Next(920, 989);
                int num = rnd.Next(100, 999);
                int num2 = rnd.Next(10, 99);
                int num3 = rnd.Next(10, 99);

                int pas1 = rnd.Next(10, 100);
                int pas2 = rnd.Next(10, 100);
                int pas3 = rnd.Next(100000, 999999);
                
                var PhOrNot = rnd.NextItem(TrueOrFalse);
                if (PhOrNot== true)
                {
                    _Clients[i] = new Client
                    {
                        IsPhysical = true,
                        Name = $"Клиент {i + 1}",
                        Surname = $"{rnd.NextItem(Surnames.SurnamesClients)}",
                        PhoneNumber = "+7" + code.ToString() + num.ToString() + num2.ToString() + num3.ToString(),
                        SeriesAndNumberPass = $"{pas1}{pas2}{pas3}",
                        DataOfIssue = new DateTime(rnd.Next(2023, 2024), rnd.Next(1, 13), rnd.Next(1, 29)),
                        IssuedBy = $"Сотрудник {rnd.Next(10, 101)}"
                    };

                }
                else
                {
                    _Clients[i] = new Client
                    {
                        IsPhysical = false,
                        Name = $"Клиент {i + 1}",
                        Surname = $"{rnd.NextItem(Surnames.SurnamesClients)}",
                        PhoneNumber = "+7" + code.ToString() + num.ToString() + num2.ToString() + num3.ToString(),
                        LegalPersonName = $"Сотрудник банка {rnd.Next(1, 101)}",
                        LegalAdress = $"Адрес {rnd.Next(1, 11)}",
                        Checking_Account = rnd.NextItem(_Checking_Accounts),
                        Inn = $"{rnd.Next(100000000, 999999999)}841"
                    };
                }
            }

            await _db.Clients.AddRangeAsync(_Clients);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация счетов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _CrewCount = 20;

        private Crew[] _Crews;
        private async Task InitializeCrews()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация груза...");

            _Crews = new Crew[_CrewCount];
            for (var i = 0; i < _CrewCount; i++)
                _Crews[i] = new Crew { Name = $"Экипаж {i + 1}"};

            await _db.Crews.AddRangeAsync(_Crews);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация категорий выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _BrandCount = 30;

        private Brand[] _Brands;
        private async Task InitializeBrands()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация брендов...");

            _Brands = new Brand[_BrandCount];
            for (var i = 0; i < _BrandCount; i++)
                _Brands[i] = new Brand { Name = $"Бренд {i + 1}" };

            await _db.Brands.AddRangeAsync(_Brands);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация брендов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _AutomobileCount = 75;

        private Automobile[] _Automobiles;
        private async Task InitializeAutomobiles()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация автомобилей...");

            var rnd = new Random();

            _Automobiles = Enumerable.Range(1, _AutomobileCount).Select(
                i => new Automobile
                {
                    Name = $"Машина {i}",
                    GosNumber = $"{(char)rnd.Next('A', 'Z' + 1)}{(char)rnd.Next('A', 'Z' + 1)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{(char)rnd.Next('A', 'Z' + 1)}{(char)rnd.Next('A', 'Z' + 1)}",
                    Brand = rnd.NextItem(_Brands),
                    LoadCapacity = rnd.Next(1, 29),
                    Purpose = $"Назначение {rnd.Next(1,11)}",
                    YearOfIssue = new DateTime(rnd.Next(2005, 2024), rnd.Next(1, 13), rnd.Next(1, 29)),
                    Mileage = rnd.Next(1000, 150000)
                    
                }
                ).ToArray();


            await _db.Automobiles.AddRangeAsync(_Automobiles);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация автомобилей выполнена за {0} мс", timer.ElapsedMilliseconds);
        }


        private DriverCategory[] _DriverCategories;

        private async Task InitializeDriverCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация категорий измерения...");

            _DriverCategories = new DriverCategory[] {

                new DriverCategory { Name = "A" },
                new DriverCategory { Name = "B" },
                new DriverCategory { Name = "C"},
                new DriverCategory { Name = "D"},
                new DriverCategory { Name = "M"}
            };

            await _db.DriverCategories.AddRangeAsync(_DriverCategories);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация категорий выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

       
        private DriverClass[] _DriverClasses;

        private async Task InitializeDriverClasses()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация классов измерения...");

            _DriverClasses = new DriverClass[] {

                new DriverClass { Name = "1" },
                new DriverClass { Name = "2" },
                new DriverClass { Name = "3" }
            };

            await _db.DriverClasses.AddRangeAsync(_DriverClasses);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация классов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _DriverCount = 75;

        private Driver[] _Drivers;
        private async Task InitializeDrivers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация водителей...");

            var rnd = new Random();

            _Drivers = Enumerable.Range(1, _DriverCount).Select(
                i => new Driver
                {
                    Name = $"Водитель {i}",
                    Crew = rnd.NextItem(_Crews),
                    Surname = $"{rnd.NextItem(Surnames.SurnamesClients)}",
                    YearOfBirth = new DateTime(rnd.Next(1975, 2004), rnd.Next(1, 13), rnd.Next(1, 29)),
                    WorkExperience = $"{rnd.Next(1,10)}",
                    Category = rnd.NextItem(_DriverCategories),
                    Class = rnd.NextItem(_DriverClasses)

                }
                ).ToArray();


            await _db.Drivers.AddRangeAsync(_Drivers);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация водителей выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _FlightCount = 50;

        private Flight[] _Flights;
        private async Task InitializeFlights()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация рейсов...");

            var rnd = new Random();

            _Flights = Enumerable.Range(1, _FlightCount).Select(
                i => new Flight
                {
                    ArrivalDate = new DateTime(rnd.Next(2023, 2025), rnd.Next(1, 13), rnd.Next(1, 29)),
                    Automobile = rnd.NextItem(_Automobiles),
                    Crew = rnd.NextItem(_Crews)
                }
                ).ToArray();


            await _db.Flights.AddRangeAsync(_Flights);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация рейсов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _OrderCount = 400;

        private Order[] _Orders;
        private async Task InitializeOrders()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация заказов...");

            var rnd = new Random();

            _Orders = Enumerable.Range(1, _OrderCount).Select(
                i => new Order
                {
                    OrderData = new DateTime(rnd.Next(2015, 2024), rnd.Next(1, 13), rnd.Next(1, 29)),
                    Client = rnd.NextItem(_Clients),
                    LoadingAddress = $"Адрес загрузки {rnd.Next(1,401)}",
                    UnloadingAddress = $"Адрес разгрузки {rnd.Next(1, 401)}",
                    RouteLength = rnd.Next(250, 100000),
                    OrderCost = (decimal) rnd.NextDouble() * 100000 + 1000,
                    Flight = rnd.NextItem(_Flights)

                }
                ).ToArray();


            await _db.Orders.AddRangeAsync(_Orders);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация заказов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }


        private Unit[] _Units;

        private async Task InitializeUnits()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация единиц измерения...");

            _Units = new Unit[] {

                new Unit { Name = "Грамм" },
                new Unit { Name = "Килограмм" },
                new Unit { Name = "Литров"}
            };

            await _db.Units.AddRangeAsync(_Units);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация единиц измерения выполнена за {0} мс", timer.ElapsedMilliseconds);
        }


        private const int _CargoCount = 500;

        private Cargo[] _Cargos;
        private async Task InitializeCargos()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация груза...");

            var rnd = new Random();

            _Cargos = Enumerable.Range(1, _CargoCount).Select(
                i => new Cargo
                {
                    Name = $"Груз {i}",
                    Unit = rnd.NextItem(_Units),
                    Amount = rnd.Next(1, 1000),
                    Weight = rnd.Next(1, 27000),
                    Order = rnd.NextItem(_Orders),
                    InsuranceValue = (decimal) rnd.NextDouble() * 40000 + 2000
                }
                ).ToArray();

            await _db.Cargos.AddRangeAsync(_Cargos);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация грузов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }



    }
}
