using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Try2.Context;
using Try2.Interfaces;
using Try2.Services.Interfaces;
using Try2.ViewModels;
using Try2.Views.Windows;

namespace Try2.Services
{
    internal class UserDialogService : IUserDialog
    {
        
        public ValidationResult results;

        private readonly IRepository<Client> _Clients;

        private readonly IRepository<Flight> _Flights;

        private readonly IRepository<Order> _Orders;
        private readonly IRepository<Bank> _Banks;
        private readonly IRepository<Checking_account> _Checking_accounts;
        private readonly IRepository<Crew> _Crews;
        private readonly IRepository<Brand> _Brands;
        private readonly IRepository<Automobile> _Automobiles;
        private readonly IRepository<Driver> _Drivers;
        private readonly IRepository<Unit> _Units;
        private readonly IRepository<Cargo> _Cargos;
        private readonly IRepository<DriverCategory> _Categories;
        private readonly IRepository<DriverClass> _Classes;

        

        public bool Add<T>(T item)
        {

            switch (item)
            {


                case Order order:
                    var order_editor_model = new OrderEditorViewModel(_Clients, _Flights);
                    var order_editor_window = new OrderEditorView
                    {
                        DataContext = order_editor_model
                    };

                    if (order_editor_window.ShowDialog() != true) return false;

                    OrderValidator OrderValidator = new OrderValidator();

                    results = OrderValidator.Validate(order_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    order.OrderData = order_editor_model.OrderDate;
                    order.Client = order_editor_model.ClientId;
                    order.LoadingAddress = order_editor_model.LoadingAddress;
                    order.UnloadingAddress = order_editor_model.UploadingAddress;
                    order.RouteLength = Convert.ToInt32(order_editor_model.RouteLength);
                    order.OrderCost = Convert.ToInt32(order_editor_model.Cost);
                    order.Flight = order_editor_model.Flight;


                    return true;

                case Client client:
                    if (client.IsPhysical == true)
                    {
                        var client_editor_model = new PhysClientAddViewModel();
                        var client_editor_window = new PhysClientAddView
                        {
                            DataContext = client_editor_model
                        };

                        if (client_editor_window.ShowDialog() != true) return false;

                        PhysValidator PhysValidator = new PhysValidator();

                        results = PhysValidator.Validate(client_editor_model);

                        if (!results.IsValid)
                        {
                            MessageBox.Show(results.ToString(), "Ошибка добавления");
                            return false;
                        }

                        client.Name = client_editor_model.Name;
                        client.Surname = client_editor_model.Surname;
                        client.SeriesAndNumberPass = client_editor_model.SerialAndNumber;
                        client.PhoneNumber = client_editor_model.PhoneNumber;
                        client.DataOfIssue = client_editor_model.DataOfIssue;
                        client.IssuedBy = client_editor_model.IssuedBy;

                    }

                    else
                    {

                        var client_editor_model = new LegalClientAddViewModel(_Checking_accounts);
                        var client_editor_window = new LegalClientAddView
                        {
                            DataContext = client_editor_model
                        };

                        if (client_editor_window.ShowDialog() != true) return false;

                        LegalClientValidator LegalValidator = new LegalClientValidator();

                        results = LegalValidator.Validate(client_editor_model);

                        if (!results.IsValid)
                        {
                            MessageBox.Show(results.ToString(), "Ошибка добавления");
                            return false;
                        }

                        client.Name = client_editor_model.Name;
                        client.Surname = client_editor_model.Surname;
                        client.LegalPersonName = client_editor_model.LegalPersonName;
                        client.PhoneNumber = client_editor_model.PhoneNumber;
                        client.LegalAdress = client_editor_model.LegalAdress;
                        client.Checking_Account = client_editor_model.Checking_account;
                        client.Inn = client_editor_model.Inn;



                    }

                    return true;

                case Flight flight:

                    var flight_editor_model = new FlightAddViewModel(_Crews, _Automobiles);
                    var flight_editor_window = new FlightAddView
                    {
                        DataContext = flight_editor_model
                    };

                    if (flight_editor_window.ShowDialog() != true) return false;

                    FlightValidator FlightValidator = new FlightValidator();

                    results = FlightValidator.Validate(flight_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    //flight.Id = flight_editor_model.Id;
                    flight.ArrivalDate = flight_editor_model.ArrivalDate;
                    flight.Crew = flight_editor_model.CrewId;
                    flight.Automobile = flight_editor_model.AutomobileId;


                    return true;

                case Driver driver:

                    var driver_editor_model = new DriverAddViewModel(_Crews, _Categories, _Classes);
                    var driver_editor_window = new DriverAddView
                    {
                        DataContext = driver_editor_model
                    };

                    if (driver_editor_window.ShowDialog() != true) return false;

                    DriverValidator DriverValidator = new DriverValidator();

                    results = DriverValidator.Validate(driver_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    driver.Name = driver_editor_model.Name;
                    driver.Surname = driver_editor_model.Surname;
                    driver.Crew = driver_editor_model.CrewId;
                    driver.YearOfBirth = driver_editor_model.YearOfBirth;
                    driver.WorkExperience = driver_editor_model.WorkExperience;
                    driver.Category = driver_editor_model.CategoryId;
                    driver.Class = driver_editor_model.ClassId;

                    return true;

                case Crew crew:
                    var crew_editor_model = new CrewAddViewModel();
                    var crew_editor_window = new CrewAddView
                    {
                        DataContext = crew_editor_model
                    };

                    if (crew_editor_window.ShowDialog() != true) return false;

                    CrewValidator CrewValidator = new CrewValidator();

                    results = CrewValidator.Validate(crew_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    crew.Name = crew_editor_model.Name;

                    return true;

                case Automobile automobile:
                    var automobile_editor_model = new AutomobileAddViewModel(_Brands);
                    var automobile_editor_window = new AutomobileAddView
                    {
                        DataContext = automobile_editor_model
                    };

                    if (automobile_editor_window.ShowDialog() != true) return false;

                    AutomobileValidator AutomobileValidator = new AutomobileValidator();

                    results = AutomobileValidator.Validate(automobile_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    automobile.Name = automobile_editor_model.Name;
                    automobile.GosNumber = automobile_editor_model.GosNumber;
                    automobile.Brand = automobile_editor_model.Brand;
                    automobile.LoadCapacity = Convert.ToInt32(automobile_editor_model.LoadCapacity);
                    automobile.Purpose = automobile_editor_model.Purpose;
                    automobile.YearOfIssue = automobile_editor_model.YearOfIssue;
                    automobile.YearOfRepair = automobile_editor_model.YearOfRepair;
                    automobile.Mileage = Convert.ToInt32(automobile_editor_model.Millage);


                    return true;

                case Brand brand:
                    var brand_editor_model = new BrandAddViewModel();
                    var brand_editor_window = new BrandAddView
                    {
                        DataContext = brand_editor_model
                    };

                    if (brand_editor_window.ShowDialog() != true) return false;

                    BrandValidator BrandValidator = new BrandValidator();

                    results = BrandValidator.Validate(brand_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    brand.Name = brand_editor_model.Name;

                    return true;

                case Checking_account checking_account:
                    var checking_account_editor_model = new Cheking_AccountAddViewModel(_Banks, _Checking_accounts);
                    var checking_account_editor_window = new Checking_AccountAddView
                    {
                        DataContext = checking_account_editor_model
                    };

                    if (checking_account_editor_window.ShowDialog() != true) return false;

                    Checking_accountValidator Checking_accountValidator = new Checking_accountValidator();

                    results = Checking_accountValidator.Validate(checking_account_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    checking_account.Check = checking_account_editor_model.Check;
                    checking_account.Bank = checking_account_editor_model.Bank;

                    return true;

                case Bank bank:
                    var bank_editor_model = new BankAddViewModel();
                    var bank_editor_window = new BankAddView
                    {
                        DataContext = bank_editor_model
                    };

                    if (bank_editor_window.ShowDialog() != true) return false;

                    BankValidator BankValidator = new BankValidator();

                    results = BankValidator.Validate(bank_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    bank.Name = bank_editor_model.Name;

                    return true;

                case Unit unit:
                    var unit_editor_model = new UnitAddViewModel();
                    var unit_editor_window = new UnitAddView
                    {
                        DataContext = unit_editor_model
                    };
                    if (unit_editor_window.ShowDialog() != true) return false;

                    UnitValidator UnitValidator = new UnitValidator();

                    results = UnitValidator.Validate(unit_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    unit.Name = unit_editor_model.Name;

                    return true;

                case Cargo cargo:
                    var cargo_editor_model = new CargoAddViewModel(_Units, _Orders);
                    var cargo_editor_window = new CargoAddView
                    {
                        DataContext = cargo_editor_model
                    };
                    if (cargo_editor_window.ShowDialog() != true) return false;

                    CargoValidator CargoValidator = new CargoValidator();

                    results = CargoValidator.Validate(cargo_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка добавления");
                        return false;
                    }

                    cargo.Name = cargo_editor_model.Name;
                    cargo.Amount = Convert.ToInt32(cargo_editor_model.Amount);
                    cargo.Weight = Convert.ToInt32(cargo_editor_model.Weight);
                    cargo.Unit = cargo_editor_model.Unit;
                    cargo.InsuranceValue = Convert.ToInt32(cargo_editor_model.Value);
                    cargo.Order = cargo_editor_model.Order;


                    return true;

                default:
                    return false;
            }
            //var order_editor_model = new OrderEditorViewModel(order);

        }




        public UserDialogService(IRepository<Bank> Banks,
                         IRepository<Checking_account> Checking_accounts,
                         IRepository<Crew> Crews,
                         IRepository<Brand> Brands,
                         IRepository<Automobile> Automobiles,
                         IRepository<Driver> Drivers,
                         IRepository<Flight> Flights,
                         IRepository<Cargo> Cargos,
                         IRepository<Unit> Units,
                         IRepository<Client> Clients,
                         IRepository<DriverCategory> Categories,
                         IRepository<DriverClass> Classes,
                         IRepository<Order> orders
                         )
        {
            _Clients = Clients;
            _Flights = Flights;
            _Banks = Banks;
            _Automobiles = Automobiles;
            _Checking_accounts = Checking_accounts;
            _Crews = Crews;
            _Brands = Brands;
            _Drivers = Drivers;
            _Cargos = Cargos;
            _Units = Units;
            _Categories = Categories;
            _Classes = Classes;
            _Orders = orders;
        }

        public bool ConfirmInformation(string Information, string Caption) => MessageBox
           .Show(
                Information, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Information)
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(
                Warning, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(
                Error, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Error)
                == MessageBoxResult.Yes;

        public bool Edit<T>(T item)
        {
            switch (item)
            {


                case Order order:
                    var order_editor_model = new OrderEditorViewModel(order, _Clients, _Flights);
                    var order_editor_window = new OrderEditorView
                    {
                        DataContext = order_editor_model
                    };

                    if (order_editor_window.ShowDialog() != true) return false;

                    OrderValidator OrderValidator = new OrderValidator();

                    results = OrderValidator.Validate(order_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    order.OrderData = order_editor_model.OrderDate;
                    order.Client = order_editor_model.ClientId;
                    order.LoadingAddress = order_editor_model.LoadingAddress;
                    order.UnloadingAddress = order_editor_model.UploadingAddress;
                    order.RouteLength = Convert.ToDecimal(order_editor_model.RouteLength);
                    order.OrderCost = Convert.ToDecimal(order_editor_model.Cost);
                    order.Flight = order_editor_model.Flight;


                    return true;

                case Client client:
                    if (client.IsPhysical == true)
                    {
                        var client_editor_model = new PhysClientAddViewModel(client);
                        var client_editor_window = new PhysClientAddView
                        {
                            DataContext = client_editor_model
                        };

                        if (client_editor_window.ShowDialog() != true) return false;

                        PhysValidator PhysValidator = new PhysValidator();

                        results = PhysValidator.Validate(client_editor_model);

                        if (!results.IsValid)
                        {
                            MessageBox.Show(results.ToString(), "Ошибка редактировния");
                            return false;
                        }

                        client.Name = client_editor_model.Name;
                        client.Surname = client_editor_model.Surname;
                        client.SeriesAndNumberPass = client_editor_model.SerialAndNumber;
                        client.PhoneNumber = client_editor_model.PhoneNumber;
                        client.DataOfIssue = client_editor_model.DataOfIssue;
                        client.IssuedBy = client_editor_model.IssuedBy;

                    }

                    else
                    {

                        var client_editor_model = new LegalClientAddViewModel(client, _Checking_accounts);
                        var client_editor_window = new LegalClientAddView
                        {
                            DataContext = client_editor_model
                        };

                        if (client_editor_window.ShowDialog() != true) return false;


                        LegalClientValidator LegalValidator = new LegalClientValidator();

                        results = LegalValidator.Validate(client_editor_model);

                        if (!results.IsValid)
                        {
                            MessageBox.Show(results.ToString(), "Ошибка редактировния");
                            return false;
                        }

                        client.Name = client_editor_model.Name;
                        client.Surname = client_editor_model.Surname;
                        client.LegalPersonName = client_editor_model.LegalPersonName;
                        client.PhoneNumber = client_editor_model.PhoneNumber;
                        client.LegalAdress = client_editor_model.LegalAdress;
                        client.Checking_Account = client_editor_model.Checking_account;
                        client.Inn = client_editor_model.Inn;



                    }

                    return true;

                case Flight flight:

                    var flight_editor_model = new FlightAddViewModel(flight, _Crews, _Automobiles);
                    var flight_editor_window = new FlightAddView
                    {
                        DataContext = flight_editor_model
                    };

                    if (flight_editor_window.ShowDialog() != true) return false;

                    FlightValidator FlightValidator = new FlightValidator();

                    results = FlightValidator.Validate(flight_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }


                    //flight.Id = flight_editor_model.Id;
                    flight.ArrivalDate = flight_editor_model.ArrivalDate;
                    flight.Crew = flight_editor_model.CrewId;
                    flight.Automobile = flight_editor_model.AutomobileId;


                    return true;

                case Driver driver:

                    var driver_editor_model = new DriverAddViewModel(driver, _Crews, _Categories, _Classes);
                    var driver_editor_window = new DriverAddView
                    {
                        DataContext = driver_editor_model
                    };

                    if (driver_editor_window.ShowDialog() != true) return false;

                    DriverValidator DriverValidator = new DriverValidator();

                    results = DriverValidator.Validate(driver_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    driver.Name = driver_editor_model.Name;
                    driver.Surname = driver_editor_model.Surname;
                    driver.Crew = driver_editor_model.CrewId;
                    driver.YearOfBirth = driver_editor_model.YearOfBirth;
                    driver.WorkExperience = driver_editor_model.WorkExperience;
                    driver.Category = driver_editor_model.CategoryId;
                    driver.Class = driver_editor_model.ClassId;

                    return true;

                case Crew crew:
                    var crew_editor_model = new CrewAddViewModel(crew);
                    var crew_editor_window = new CrewAddView
                    {
                        DataContext = crew_editor_model
                    };

                    if (crew_editor_window.ShowDialog() != true) return false;

                    CrewValidator CrewValidator = new CrewValidator();

                    results = CrewValidator.Validate(crew_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    crew.Name = crew_editor_model.Name;

                    return true;

                case Automobile automobile:
                    var automobile_editor_model = new AutomobileAddViewModel(automobile, _Brands);
                    var automobile_editor_window = new AutomobileAddView
                    {
                        DataContext = automobile_editor_model
                    };

                    if (automobile_editor_window.ShowDialog() != true) return false;



                    AutomobileValidator AutomobileValidator = new AutomobileValidator();

                    results = AutomobileValidator.Validate(automobile_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    automobile.Name = automobile_editor_model.Name;
                    automobile.GosNumber = automobile_editor_model.GosNumber;
                    automobile.Brand = automobile_editor_model.Brand;
                    automobile.LoadCapacity = Convert.ToInt32(automobile_editor_model.LoadCapacity);
                    automobile.Purpose = automobile_editor_model.Purpose;
                    automobile.YearOfIssue = automobile_editor_model.YearOfIssue;
                    automobile.YearOfRepair = automobile_editor_model.YearOfRepair;
                    automobile.Mileage = Convert.ToInt32(automobile_editor_model.Millage);


                    return true;

                case Brand brand:
                    var brand_editor_model = new BrandAddViewModel(brand);
                    var brand_editor_window = new BrandAddView
                    {
                        DataContext = brand_editor_model
                    };

                    if (brand_editor_window.ShowDialog() != true) return false;

                    BrandValidator BrandValidator = new BrandValidator();

                    results = BrandValidator.Validate(brand_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }


                    brand.Name = brand_editor_model.Name;

                    return true;

                case Checking_account checking_account:
                    var checking_account_editor_model = new Cheking_AccountAddViewModel(checking_account ,_Banks, _Checking_accounts);
                    var checking_account_editor_window = new Checking_AccountAddView
                    {
                        DataContext = checking_account_editor_model
                    };

                    if (checking_account_editor_window.ShowDialog() != true) return false;

                    Checking_accountValidator Checking_accountValidator = new Checking_accountValidator();

                    results = Checking_accountValidator.Validate(checking_account_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }


                    checking_account.Check = checking_account_editor_model.Check;
                    checking_account.Bank = checking_account_editor_model.Bank;

                    return true;

                case Bank bank:
                    var bank_editor_model = new BankAddViewModel(bank);
                    var bank_editor_window = new BankAddView
                    {
                        DataContext = bank_editor_model
                    };

                    if (bank_editor_window.ShowDialog() != true) return false;


                    BankValidator BankValidator = new BankValidator();

                    results = BankValidator.Validate(bank_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    bank.Name = bank_editor_model.Name;


                    return true;

                case Unit unit:
                    var unit_editor_model = new UnitAddViewModel(unit);
                    var unit_editor_window = new UnitAddView
                    {
                        DataContext = unit_editor_model
                    };
                    if (unit_editor_window.ShowDialog() != true) return false;

                    UnitValidator UnitValidator = new UnitValidator();

                    results = UnitValidator.Validate(unit_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }


                    unit.Name = unit_editor_model.Name;

                    return true;

                case Cargo cargo:
                    var cargo_editor_model = new CargoAddViewModel(cargo, _Units, _Orders);
                    var cargo_editor_window = new CargoAddView
                    {
                        DataContext = cargo_editor_model
                    };
                    if (cargo_editor_window.ShowDialog() != true) return false;

                    CargoValidator CargoValidator = new CargoValidator();

                    results = CargoValidator.Validate(cargo_editor_model);

                    if (!results.IsValid)
                    {
                        MessageBox.Show(results.ToString(), "Ошибка редактировния");
                        return false;
                    }

                    cargo.Name = cargo_editor_model.Name;
                    cargo.Amount = Convert.ToInt32(cargo_editor_model.Amount);
                    cargo.Weight = Convert.ToInt32(cargo_editor_model.Weight);
                    cargo.Unit = cargo_editor_model.Unit;
                    cargo.InsuranceValue = Convert.ToDecimal(cargo_editor_model.Value);
                    cargo.Order = cargo_editor_model.Order;


                    return true;

                default:
                    return false;
            }
        }


        public class CargoValidator : AbstractValidator<CargoAddViewModel>
        {
            public CargoValidator()
            {
                BigInteger res;
                Decimal s;


                RuleFor(cargo => cargo.Name).NotEmpty().MinimumLength(3);
                RuleFor(cargo => cargo.Amount).NotEmpty();
                RuleFor(cargo => cargo.Amount).Must(amount => BigInteger.TryParse(amount, out res)).WithMessage("Количество должно быть числом");
                RuleFor(cargo => cargo.Weight).NotEmpty();
                RuleFor(cargo => cargo.Weight).Must(weight => BigInteger.TryParse(weight, out res)).WithMessage("Масса должна быть числом");
                RuleFor(cargo => cargo.Unit.Name).NotEmpty().When(cargo => cargo.Unit != null);
                RuleFor(cargo => cargo.Value).NotEmpty();
                RuleFor(cargo => cargo.Value).Must(value => Decimal.TryParse(value, out s)).WithMessage("Стоимость должна быть числом, следует использовать запятую, для не целых чисел");
                RuleFor(cargo => cargo.Order).NotEmpty();

                //RuleFor(automobile => automobile.Brand.Name).NotEmpty().When(automobile => automobile.Brand != null);
            }
        }

        public class BankValidator : AbstractValidator<BankAddViewModel>
        {
            public BankValidator()
            {
                RuleFor(bank => bank.Name).NotEmpty().MinimumLength(3);
            }
        }

        public class UnitValidator : AbstractValidator<UnitAddViewModel>
        {
            public UnitValidator()
            {
                RuleFor(unit => unit.Name).NotEmpty().MinimumLength(3);
            }
        }

        public class Checking_accountValidator : AbstractValidator<Cheking_AccountAddViewModel>
        {
            public Checking_accountValidator()
            {
                BigInteger res;
                RuleFor(check => check.Check).NotEmpty().Length(20);
                RuleFor(check => check.Check).Must(check => BigInteger.TryParse(check, out res)).WithMessage("Неверный формат числа");
                RuleFor(check => check.Bank.Name).NotEmpty().When(check => check.Bank != null);
            }
        }

        public class BrandValidator : AbstractValidator<BrandAddViewModel>
        {
            public BrandValidator()
            {
                RuleFor(brand => brand.Name).NotEmpty().MinimumLength(3);
            }
        }

        public class AutomobileValidator : AbstractValidator<AutomobileAddViewModel>
        {
            public AutomobileValidator()
            {
                BigInteger res;
                RuleFor(automobile => automobile.Name).NotEmpty().MinimumLength(2);
                RuleFor(automobile => automobile.GosNumber).NotEmpty().Length(8,9);
                RuleFor(automobile => automobile.Brand.Name).NotEmpty().When(automobile => automobile.Brand != null);
                RuleFor(automobile => automobile.LoadCapacity).NotEmpty();
                RuleFor(automobile => automobile.LoadCapacity).Must(load => BigInteger.TryParse(load, out res)).WithMessage("Грузоподъемность должна быть числом");
                //.When(automobile => ulong.TryParse(automobile.LoadCapacity, out result));
                RuleFor(automobile => automobile.Purpose).NotEmpty().ToFormattedString();
                RuleFor(automobile => automobile.YearOfIssue).NotEmpty();

               // RuleFor(automobile => automobile.YearOfIssue).Must(year => BigInteger.TryParse(year, out res)).WithMessage("Неверный формат года выпуска");
                RuleFor(automobile => automobile.Millage).NotEmpty();
                RuleFor(automobile => automobile.Millage).Must(mill => BigInteger.TryParse(mill, out res)).WithMessage("Пробег должен быть числом");
            }
        }

        public class DriverValidator : AbstractValidator<DriverAddViewModel>
        {
            public DriverValidator()
            {
                RuleFor(driver => driver.Name).NotEmpty().MinimumLength(3);
                RuleFor(driver => driver.Surname).NotEmpty().MinimumLength(3);
                RuleFor(driver => driver.CrewId).NotEmpty();
                RuleFor(driver => driver.YearOfBirth).NotEmpty().When(driver => driver.YearOfBirth.Year > 1960);
                RuleFor(driver => driver.WorkExperience).NotEmpty();
                RuleFor(driver => driver.Categories).NotEmpty();
                RuleFor(driver => driver.ClassId).NotEmpty();
            }
        }

        public class CrewValidator : AbstractValidator<CrewAddViewModel>
        {
            public CrewValidator()
            {
                RuleFor(crew => crew.Name).NotEmpty().MinimumLength(3);
            }
        }

        public class FlightValidator : AbstractValidator<FlightAddViewModel>
        {
            public FlightValidator()
            {
                RuleFor(flight => flight.ArrivalDate).NotEmpty();
                RuleFor(flight => flight.ArrivalDate).Must(flight => (flight.Year >= DateTime.Now.Year)).WithMessage("Дата имеет странное значение");
                RuleFor(flight => flight.CrewId).NotEmpty();
                RuleFor(flight => flight.AutomobileId).NotEmpty();
            }
        }

        public class LegalClientValidator : AbstractValidator<LegalClientAddViewModel>
        {
            public LegalClientValidator()
            {
                Regex regexTelephone = new Regex(@"^\+?\d{11}$");
                ulong result;

                RuleFor(legalclient => legalclient.Name).NotEmpty().MinimumLength(3);
                RuleFor(legalclient => legalclient.Surname).NotEmpty().MinimumLength(3);
                RuleFor(legalclient => legalclient.LegalPersonName).NotEmpty().MinimumLength(3);
                RuleFor(legalclient => legalclient.PhoneNumber).Length(11,12).Must(number => regexTelephone.Match(number).Success).WithMessage("Неверный формат номера телефона, пример: (+79994212121 или 89994212121)");
                RuleFor(legalclient => legalclient.LegalAdress).NotEmpty().MinimumLength(3);
                RuleFor(legalclient => legalclient.Checking_account).NotEmpty();
                RuleFor(legalclient => legalclient.Inn).NotEmpty().Length(12);
                RuleFor(legalclient => legalclient.Inn).Must(inn => ulong.TryParse(inn, out result)).WithMessage("Неверный формат ИНН, ввод должен содержать 12 цифр");


            }
        }



        public class PhysValidator : AbstractValidator<PhysClientAddViewModel>
        {
            public PhysValidator()
            {
                Regex regexTelephone = new Regex(@"^[+]{0,1}[0-9]{11}");

                ulong result;
                RuleFor(physclient => physclient.Name).NotEmpty().MinimumLength(3);
                RuleFor(physclient => physclient.Surname).NotEmpty().MinimumLength(3);
                RuleFor(physclient => physclient.SerialAndNumber).NotEmpty().Length(10);
                RuleFor(physclient => physclient.SerialAndNumber).Must(serial => ulong.TryParse(serial, out result)).WithMessage("Неверный формат пасспортных данных, пример: (2255123678)");
                RuleFor(physclient => physclient.PhoneNumber).Must(number => regexTelephone.Match(number).Success).WithMessage("Неверный формат номера телефона, пример: (+79994212121)");
                RuleFor(physclient => physclient.DataOfIssue).Must(date => (date.Year >= 1900)).WithMessage("Неверный формат времени");
                RuleFor(physclient => physclient.IssuedBy).NotEmpty().MinimumLength(3);

            }
        }

        public class OrderValidator : AbstractValidator<OrderEditorViewModel>
        {
            public OrderValidator()
            {
                //BigInteger res;
                //Double s;
                Decimal s;

                RuleFor(order => order.ClientId).NotEmpty();
                RuleFor(order => order.OrderDate).NotEmpty();
                RuleFor(order => order.OrderDate).Must(date => date.Year >= 2015).WithMessage("Неверный формат времени");
                RuleFor(order => order.LoadingAddress).NotEmpty().MinimumLength(3);
                RuleFor(order => order.UploadingAddress).NotEmpty().MinimumLength(3);
                RuleFor(order => order.RouteLength).NotEmpty();
                RuleFor(order => order.RouteLength).Must(length => Decimal.TryParse(length, out s)).WithMessage("Длина должна быть числом, следует использовать запятую, для не целых чисел");
                //RuleFor(order => order.RouteLength).Must(length => ulong.TryParse(length, out result)).WithMessage("Неверный формат длины");
                RuleFor(order => order.Cost).NotEmpty();
                RuleFor(order => order.Cost).Must(cost => Decimal.TryParse(cost, out s)).WithMessage("Цена должна быть числом, следует использовать запятую, для не целых чисел");
                RuleFor(order => order.Flight).NotEmpty();
            }
        }






    }
}

