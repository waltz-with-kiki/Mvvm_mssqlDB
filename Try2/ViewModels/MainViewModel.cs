using FluentValidation;
using FluentValidation.Results;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Try2.Context;
using Try2.Interfaces;
using Try2.Services.Interfaces;
using Excel = Microsoft.Office.Interop.Excel;


namespace Try2.ViewModels
{



    public class MainViewModel : ViewModel
    {

        public string Title { get; set; } = "Грузовые автоперевозки";


       /* private bool _isDataLoaded = false;
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set
            {
                _isDataLoaded = value;
                
                OnPropertyChanged(nameof(IsDataLoaded));
            }
        }
       */
        private bool _isDataLoaded = false;

        public bool isDataLoaded { get { return _isDataLoaded; } set => Set(ref _isDataLoaded, value); }

        public string SearchTextOrder { get; set; }

        public string SearchTextFlight { get; set; }

        public string SearchTextDriver { get; set; }

        public string SearchTextAutomobile { get; set; }

        public string SearchTextCheck { get; set; }

        public string SearchTextCargo { get; set; }


        private readonly IRepository<Client> _ClientsRep;

        private readonly IRepository<Order> _OrdersRep;

        private readonly IRepository<Bank> _BanksRep;
        private readonly IRepository<Checking_account> _Checking_accountsRep;
        private readonly IRepository<Crew> _CrewsRep;
        private readonly IRepository<Brand> _BrandsRep;
        private readonly IRepository<Automobile> _AutomobilesRep;
        private readonly IRepository<Driver> _DriversRep;
        private readonly IRepository<Flight> _FlightsRep;
        private readonly IRepository<Unit> _UnitsRep;
        private readonly IRepository<Cargo> _CargosRep;

        private readonly IUserDialog _UserDialog;

        /*   public List<Order> Orders { get
               {
                   return _OrdersRep.Items.ToList();
               } }
           */
        /*  public List<Client> PhysClients { get
              {
                  return _ClientsRep.Items.Where(c => c.IsPhysical == true).Select(c => c).ToList();
              } }

          public List<Client> LegalClients { get
              {


                  return _ClientsRep.Items.Where(c => c.IsPhysical == false).ToList();
              } }
        */

        private Order _SelectedOrder;

        //Замена OnPropertyChanged
        public Order SelectedOrder { get { return _SelectedOrder; } set => Set(ref _SelectedOrder, value); }

        private Checking_account _SelectedChecking_account;

        public Checking_account SelectedChecking_account { get { return _SelectedChecking_account; } set => Set(ref _SelectedChecking_account, value); }

        private Client _SelectedPhysClient;

        public Client SelectedPhysClient { get { return _SelectedPhysClient; } set => Set(ref _SelectedPhysClient, value); }

        private Client _SelectedLegalClient;

        public Client SelectedLegalClient { get { return _SelectedLegalClient; } set => Set(ref _SelectedLegalClient, value); }

        private Flight _SelectedFlight;

        public Flight SelectedFlight { get { return _SelectedFlight; } set => Set(ref _SelectedFlight, value); }


        private Crew _SelectedCrew;

        public Crew SelectedCrew { get { return _SelectedCrew; } set => Set(ref _SelectedCrew, value); }

        private Automobile _SelectedAutomobile;

        public Automobile SelectedAutomobile { get { return _SelectedAutomobile; } set => Set(ref _SelectedAutomobile, value); }


        private Brand _SelectedBrand;

        public Brand SelectedBrand { get { return _SelectedBrand; } set => Set(ref _SelectedBrand, value); }



        public Driver SelectedDriver { get { return _SelectedDriver; } set => Set(ref _SelectedDriver, value); }

        private Driver _SelectedDriver;

        private Bank _SelectedBank;

        public Bank SelectedBank { get { return _SelectedBank; } set => Set(ref _SelectedBank, value); }

        private Unit _SelectedUnit;

        public Unit SelectedUnit { get { return _SelectedUnit; } set => Set(ref _SelectedUnit, value); }


        private Cargo _SelectedCargo;

        public Cargo SelectedCargo { get { return _SelectedCargo; } set => Set(ref _SelectedCargo, value); }







        private ObservableCollection<Bank> _Banks;


        private CollectionViewSource _BanksViewSource;

        public ICollectionView BanksView => _BanksViewSource?.View;

        public ObservableCollection<Bank> Banks
        {
            get => _Banks;
            set
            {
                if (Set(ref _Banks, value))
                {
                    _BanksViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _BanksViewSource.View.Refresh();

                    OnPropertyChanged(nameof(BanksView));
                }
            }
        }


        private ObservableCollection<Checking_account> _Checking_Accounts;


        private CollectionViewSource _Checking_AccountsViewSource;

        public ICollectionView Checking_AccountsView => _Checking_AccountsViewSource?.View;

        public ObservableCollection<Checking_account> Checking_Accounts
        {
            get => _Checking_Accounts;
            set
            {
                if (Set(ref _Checking_Accounts, value))
                {
                    _Checking_AccountsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _Checking_AccountsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(Checking_AccountsView));
                }
            }
        }

        private ObservableCollection<Crew> _Crews;


        private CollectionViewSource _CrewsViewSource;

        public ICollectionView CrewsView => _CrewsViewSource?.View;

        public ObservableCollection<Crew> Crews
        {
            get => _Crews;
            set
            {
                if (Set(ref _Crews, value))
                {
                    _CrewsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _CrewsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(CrewsView));
                }
            }
        }


        private ObservableCollection<Brand> _Brands;


        private CollectionViewSource _BrandsViewSource;

        public ICollectionView BrandsView => _BrandsViewSource?.View;

        public ObservableCollection<Brand> Brands
        {
            get => _Brands;
            set
            {
                if (Set(ref _Brands, value))
                {
                    _BrandsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _BrandsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(BrandsView));
                }
            }
        }


        private ObservableCollection<Automobile> _Automobiles;


        private CollectionViewSource _AutomobilesViewSource;

        public ICollectionView AutomobilesView => _AutomobilesViewSource?.View;

        public ObservableCollection<Automobile> Automobiles
        {
            get => _Automobiles;
            set
            {
                if (Set(ref _Automobiles, value))
                {
                    _AutomobilesViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _AutomobilesViewSource.View.Refresh();

                    OnPropertyChanged(nameof(AutomobilesView));
                }
            }
        }




        private ObservableCollection<Driver> _Drivers;


        private CollectionViewSource _DriversViewSource;

        public ICollectionView DriversView => _DriversViewSource?.View;

        public ObservableCollection<Driver> Drivers
        {
            get => _Drivers;
            set
            {
                if (Set(ref _Drivers, value))
                {
                    _DriversViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _DriversViewSource.View.Refresh();

                    OnPropertyChanged(nameof(DriversView));
                }
            }
        }

        private ObservableCollection<Flight> _Flights;


        private CollectionViewSource _FlightsViewSource;

        public ICollectionView FlightsView => _FlightsViewSource?.View;

        public ObservableCollection<Flight> Flights
        {
            get => _Flights;
            set
            {
                if (Set(ref _Flights, value))
                {
                    _FlightsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _FlightsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(FlightsView));
                }
            }
        }

        private ObservableCollection<Unit> _Units;


        private CollectionViewSource _UnitsViewSource;

        public ICollectionView UnitsView => _UnitsViewSource?.View;

        public ObservableCollection<Unit> Units
        {
            get => _Units;
            set
            {
                if (Set(ref _Units, value))
                {
                    _UnitsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _UnitsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(UnitsView));
                }
            }
        }

        private ObservableCollection<Cargo> _Cargos;


        private CollectionViewSource _CargosViewSource;

        public ICollectionView CargosView => _CargosViewSource?.View;

        public ObservableCollection<Cargo> Cargos
        {
            get => _Cargos;
            set
            {
                if (Set(ref _Cargos, value))
                {
                    _CargosViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };


                    _CargosViewSource.View.Refresh();

                    OnPropertyChanged(nameof(CargosView));
                }
            }
        }


        private ObservableCollection<Client> _LegalClients;


        private CollectionViewSource _LegalClientsViewSource;

        public ICollectionView LegalClientsView => _LegalClientsViewSource?.View;

        public ObservableCollection<Client> LegalClients
        {
            get => _LegalClients;
            set
            {
                if (Set(ref _LegalClients, value))
                {
                    _LegalClientsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };

                    // _ClientsViewSource.Filter += OnClientsFilter;
                    _LegalClientsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(LegalClientsView));
                }
            }
        }



        private ObservableCollection<Client> _PhysClients;


        private CollectionViewSource _PhysClientsViewSource;

        public ICollectionView PhysClientsView => _PhysClientsViewSource?.View;

        public ObservableCollection<Client> PhysClients
        {
            get => _PhysClients;
            set
            {
                if (Set(ref _PhysClients, value))
                {
                    _PhysClientsViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };

                    // _ClientsViewSource.Filter += OnClientsFilter;
                    _PhysClientsViewSource.View.Refresh();

                    OnPropertyChanged(nameof(PhysClientsView));
                }
            }
        }


        private ObservableCollection<Order> _Orders;


        private CollectionViewSource _OrdersViewSource;

        public ICollectionView OrdersView => _OrdersViewSource?.View;

        public ObservableCollection<Order> Orders
        {
            get => _Orders;
            set
            {
                if (Set(ref _Orders, value))
                {
                    _OrdersViewSource = new CollectionViewSource
                    {
                        Source = value,
                    };

                    // _OrdersViewSource.Filter += OnOrdersFilter;
                    _OrdersViewSource.View.Refresh();

                    OnPropertyChanged(nameof(OrdersView));
                }
            }
        }
        // public static ObservableCollection<Bank> Sort(ObservableCollection<Bank> Banks) => Banks.OrderBy(x => x.Name).ToObservableCollection();





        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute(object arg) => true;

        private async void OnLoadDataCommandExecuted(object obj)
        {
            //Orders = new ObservableCollection<Order>(_Orders.ToArray());
            //Orders = _OrdersRep.Items.ToArray().ToObservableCollection();\

            await LoadBanks();

            await LoadChecking_Accounts();

            await LoadPhysClients();

            await LoadLegalClients();

            await LoadCrews();

            await LoadDrivers();

            await LoadBrands();

            await LoadAutomobiles();

            await LoadFlights();

            await LoadUnits();

            await LoadOrders();

            await LoadCargos();

           // Task.WaitAll(); 

            isDataLoaded = true;
            // Orders = await new ObservableCollection<Order>(await _OrdersRep.Items.ToArrayAsync());
        }

        private async Task LoadOrders() {

            Orders = new ObservableCollection<Order>(await _OrdersRep.Items.ToArrayAsync());
            _OrdersViewSource.Source = Orders;
        }

        private async Task LoadBanks() {

            Banks = new ObservableCollection<Bank>(await _BanksRep.Items.ToArrayAsync());
            _BanksViewSource.Source = Banks;
        }

        private async Task LoadChecking_Accounts() {

            Checking_Accounts = new ObservableCollection<Checking_account>(await _Checking_accountsRep.Items.ToArrayAsync());
            _Checking_AccountsViewSource.Source = Checking_Accounts;
        }

        private async Task LoadPhysClients() {

            PhysClients = new ObservableCollection<Client>(await _ClientsRep.Items.Where(x => x.IsPhysical == true).ToArrayAsync());
            _PhysClientsViewSource.Source = PhysClients;
        }

        private async Task LoadLegalClients() {

            LegalClients = new ObservableCollection<Client>(await _ClientsRep.Items.Where(x => x.IsPhysical == false).ToArrayAsync());
            _LegalClientsViewSource.Source = LegalClients;
        }

        private async Task LoadCrews() {

            Crews = new ObservableCollection<Crew>(await _CrewsRep.Items.ToArrayAsync());
            _CrewsViewSource.Source = Crews;
        }
        private async Task LoadAutomobiles() {

            Automobiles = new ObservableCollection<Automobile>(await _AutomobilesRep.Items.ToArrayAsync());
            _AutomobilesViewSource.Source = Automobiles;
        }
        private async Task LoadBrands() {

            Brands = new ObservableCollection<Brand>(await _BrandsRep.Items.ToArrayAsync());
            _BrandsViewSource.Source = Brands;
        }
        private async Task LoadDrivers() {

            Drivers = new ObservableCollection<Driver>(await _DriversRep.Items.ToArrayAsync());
            _DriversViewSource.Source = Drivers;

        }

        private async Task LoadFlights() {

            Flights = new ObservableCollection<Flight>(await _FlightsRep.Items.ToArrayAsync());
            _FlightsViewSource.Source = Flights;
        }

        private async Task LoadUnits() {

            Units = new ObservableCollection<Unit>(await _UnitsRep.Items.ToArrayAsync());
            _UnitsViewSource.Source = Units;
        }

        private async Task LoadCargos() {

            Cargos = new ObservableCollection<Cargo>(await _CargosRep.Items.ToArrayAsync());
            _CargosViewSource.Source = Cargos;
        }


        private ICommand _AddNewOrder;

        public ICommand AddNewOrder => _AddNewOrder
           ??= new RelayCommand(OnAddNewOrderCommandExecuted, CanAddNewOrderCommandExecute);

        private bool CanAddNewOrderCommandExecute(object arg) => true;

        private void OnAddNewOrderCommandExecuted(object obj)
        {
            var new_order = new Order();

            if (!_UserDialog.Add(new_order))
                return;

            Orders.Add(_OrdersRep.Add(new_order));

            SelectedOrder = new_order;
        }

        private ICommand _EditOrder;

        public ICommand EditOrder => _EditOrder
           ??= new RelayCommand(OnEditOrderCommandExecuted, CanEditOrderCommandExecute);

        private bool CanEditOrderCommandExecute(object arg) => SelectedOrder != null;

        private void OnEditOrderCommandExecuted(object obj)
        {
            var new_order = SelectedOrder;

            if (!_UserDialog.Edit(new_order))
                return;

            Orders.Remove(SelectedOrder);

            Orders.Add(new_order);

            _OrdersRep.Update(new_order);

            LoadCargos();

            SelectedOrder = new_order;

        }



        private ICommand _AddNewPhysClient;

        public ICommand AddNewPhysClient => _AddNewPhysClient
           ??= new RelayCommand(OnAddNewPhysClientCommandExecuted, CanAddNewPhysClientCommandExecute);

        private bool CanAddNewPhysClientCommandExecute(object arg) => true;

        private void OnAddNewPhysClientCommandExecuted(object obj)
        {
            var new_client = new Client();

            new_client.IsPhysical = true;

            if (!_UserDialog.Add(new_client))
                return;

            PhysClients.Add(_ClientsRep.Add(new_client));

            SelectedPhysClient = new_client;
        }

        private ICommand _EditPhysClient;

        public ICommand EditPhysClient => _EditPhysClient
           ??= new RelayCommand(OnEditPhysClientCommandExecuted, CanEditPhysClientCommandExecute);

        private bool CanEditPhysClientCommandExecute(object arg) => SelectedPhysClient != null;

        private void OnEditPhysClientCommandExecuted(object obj)
        {
            var new_physclient = SelectedPhysClient;

            if (!_UserDialog.Edit(new_physclient))
                return;


            PhysClients.Remove(SelectedPhysClient);

            PhysClients.Add(new_physclient);

            _ClientsRep.Update(new_physclient);

            LoadOrders();

            SelectedPhysClient = new_physclient;

        }



        private ICommand _AddNewLegalClient;

        public ICommand AddNewLegalClient => _AddNewLegalClient
           ??= new RelayCommand(OnAddNewLegalClientCommandExecuted, CanAddNewLegalClientCommandExecute);

        private bool CanAddNewLegalClientCommandExecute(object arg) => true;

        private void OnAddNewLegalClientCommandExecuted(object obj)
        {
            var new_client = new Client();

            new_client.IsPhysical = false;

            if (!_UserDialog.Add(new_client))
                return;

            LegalClients.Add(_ClientsRep.Add(new_client));

            SelectedLegalClient = new_client;
        }

        private ICommand _EditLegalClient;

        public ICommand EditLegalClient => _EditLegalClient
           ??= new RelayCommand(OnEditLegalClientCommandExecuted, CanEditLegalClientCommandExecute);

        private bool CanEditLegalClientCommandExecute(object arg) => SelectedLegalClient != null;

        private void OnEditLegalClientCommandExecuted(object obj)
        {
            var new_legalclient = SelectedLegalClient;

            if (!_UserDialog.Edit(new_legalclient))
                return;


            LegalClients.Remove(SelectedLegalClient);

            LegalClients.Add(new_legalclient);

            _ClientsRep.Update(new_legalclient);

            LoadOrders();

            SelectedLegalClient = new_legalclient;

        }


        private ICommand _AddNewFlight;

        public ICommand AddNewFlight => _AddNewFlight
           ??= new RelayCommand(OnAddNewFlightCommandExecuted, CanAddNewFlightCommandExecute);

        private bool CanAddNewFlightCommandExecute(object arg) => true;

        private void OnAddNewFlightCommandExecuted(object obj)
        {
            var new_flight = new Flight();

            if (!_UserDialog.Add(new_flight))
                return;

            Flights.Add(_FlightsRep.Add(new_flight));

            SelectedFlight = new_flight;
        }

        private ICommand _EditFlight;

        public ICommand EditFlight => _EditFlight
           ??= new RelayCommand(OnEditFlightCommandExecuted, CanEditFlightCommandExecute);

        private bool CanEditFlightCommandExecute(object arg) => SelectedFlight != null;

        private void OnEditFlightCommandExecuted(object obj)
        {
            var new_flight = SelectedFlight;

            if (!_UserDialog.Edit(new_flight))
                return;

            Flights.Remove(SelectedFlight);

            Flights.Add(new_flight);

            _FlightsRep.Update(new_flight);

            LoadOrders();

            SelectedFlight = new_flight;

        }



        private ICommand _AddNewDriver;

        public ICommand AddNewDriver => _AddNewDriver
           ??= new RelayCommand(OnAddNewDriverCommandExecuted, CanAddNewDriverCommandExecute);

        private bool CanAddNewDriverCommandExecute(object arg) => true;

        private void OnAddNewDriverCommandExecuted(object obj)
        {
            var new_driver = new Driver();

            if (!_UserDialog.Add(new_driver))
                return;

            Drivers.Add(_DriversRep.Add(new_driver));

            SelectedDriver = new_driver;
        }

        private ICommand _EditDriver;

        public ICommand EditDriver => _EditDriver
           ??= new RelayCommand(OnEditDriverCommandExecuted, CanEditDriverCommandExecute);

        private bool CanEditDriverCommandExecute(object arg) => SelectedDriver != null;

        private void OnEditDriverCommandExecuted(object obj)
        {
            var new_driver = SelectedDriver;

            if (!_UserDialog.Edit(new_driver))
                return;

            Drivers.Remove(SelectedDriver);

            Drivers.Add(new_driver);

            _DriversRep.Update(new_driver);

            SelectedDriver = new_driver;

        }


        private ICommand _AddNewAutomobile;

        public ICommand AddNewAutomobile => _AddNewAutomobile
           ??= new RelayCommand(OnAddNewAutomobileCommandExecuted, CanAddNewAutomobileCommandExecute);

        private bool CanAddNewAutomobileCommandExecute(object arg) => true;

        private void OnAddNewAutomobileCommandExecuted(object obj)
        {
            var new_automobile = new Automobile();

            if (!_UserDialog.Add(new_automobile))
                return;

            Automobiles.Add(_AutomobilesRep.Add(new_automobile));

            SelectedAutomobile = new_automobile;
        }

        private ICommand _EditAutomobile;

        public ICommand EditAutomobile => _EditAutomobile
           ??= new RelayCommand(OnEditAutomobileCommandExecuted, CanEditAutomobileCommandExecute);

        private bool CanEditAutomobileCommandExecute(object arg) => SelectedAutomobile != null;

        private void OnEditAutomobileCommandExecuted(object obj)
        {
            var new_automobile = SelectedAutomobile;

            if (!_UserDialog.Edit(new_automobile))
                return;

            Automobiles.Remove(SelectedAutomobile);

            Automobiles.Add(new_automobile);

            _AutomobilesRep.Update(new_automobile);

            LoadFlights();

            SelectedAutomobile = new_automobile;

        }


        private ICommand _AddNewChecking_Account;

        public ICommand AddNewChecking_Account => _AddNewChecking_Account
           ??= new RelayCommand(OnAddNewChecking_AccountCommandExecuted, CanAddNewChecking_AccountCommandExecute);

        private bool CanAddNewChecking_AccountCommandExecute(object arg) => true;

        private void OnAddNewChecking_AccountCommandExecuted(object obj)
        {
            var new_checking_account = new Checking_account();

            if (!_UserDialog.Add(new_checking_account))
                return;

            Checking_Accounts.Add(_Checking_accountsRep.Add(new_checking_account));

            SelectedChecking_account = new_checking_account;
        }

        private ICommand _EditChecking_Account;

        public ICommand EditChecking_Account => _EditChecking_Account
           ??= new RelayCommand(OnEditChecking_AccountCommandExecuted, CanEditChecking_AccountCommandExecute);

        private bool CanEditChecking_AccountCommandExecute(object arg) => SelectedChecking_account != null;

        private void OnEditChecking_AccountCommandExecuted(object obj)
        {
            var new_checking_account = SelectedChecking_account;

            if (!_UserDialog.Edit(new_checking_account))
                return;

            Checking_Accounts.Remove(SelectedChecking_account);

            Checking_Accounts.Add(new_checking_account);

            _Checking_accountsRep.Update(new_checking_account);

            LoadLegalClients();

            SelectedChecking_account = new_checking_account;

        }


        private ICommand _AddNewBank;

        public ICommand AddNewBank => _AddNewBank
           ??= new RelayCommand(OnAddNewBankCommandExecuted, CanAddNewBankCommandExecute);

        private bool CanAddNewBankCommandExecute(object arg) => true;

        private void OnAddNewBankCommandExecuted(object obj)
        {
            var new_bank = new Bank();

            if (!_UserDialog.Add(new_bank))
                return;


            Banks.Add(_BanksRep.Add(new_bank));

            SelectedBank = new_bank;
        }

        private ICommand _EditBank;

        public ICommand EditBank => _EditBank
           ??= new RelayCommand(OnEditBankCommandExecuted, CanEditBankCommandExecute);

        private bool CanEditBankCommandExecute(object arg) => SelectedBank != null;

        private void OnEditBankCommandExecuted(object obj)
        {

            var new_bank = SelectedBank;

            if (!_UserDialog.Edit(new_bank))
                return;


            Banks.Remove(SelectedBank);

            Banks.Add(new_bank);

            _BanksRep.Update(new_bank);

            LoadChecking_Accounts();

            SelectedBank = new_bank;

        }


        private ICommand _AddNewUnit;

        public ICommand AddNewUnit => _AddNewUnit
           ??= new RelayCommand(OnAddNewUnitCommandExecuted, CanAddNewUnitCommandExecute);

        private bool CanAddNewUnitCommandExecute(object arg) => true;

        private void OnAddNewUnitCommandExecuted(object obj)
        {
            var new_unit = new Unit();

            if (!_UserDialog.Add(new_unit))
                return;

            Units.Add(_UnitsRep.Add(new_unit));

            SelectedUnit = new_unit;
        }

        private ICommand _EditUnit;

        public ICommand EditUnit => _EditUnit
           ??= new RelayCommand(OnEditUnitCommandExecuted, CanEditUnitCommandExecute);

        private bool CanEditUnitCommandExecute(object arg) => SelectedUnit != null;

        private void OnEditUnitCommandExecuted(object obj)
        {
            var new_unit = SelectedUnit;

            if (!_UserDialog.Edit(new_unit))
                return;

            Units.Remove(SelectedUnit);

            Units.Add(new_unit);

            _UnitsRep.Update(new_unit);

            LoadCargos();

            SelectedUnit = new_unit;

        }


        private ICommand _AddNewCargo;

        public ICommand AddNewCargo => _AddNewCargo
           ??= new RelayCommand(OnAddNewCargoCommandExecuted, CanAddNewCargoCommandExecute);

        private bool CanAddNewCargoCommandExecute(object arg) => true;

        private void OnAddNewCargoCommandExecuted(object obj)
        {
            var new_cargo = new Cargo();

            if (!_UserDialog.Add(new_cargo))
                return;

            Cargos.Add(_CargosRep.Add(new_cargo));

            LoadOrders();

            SelectedCargo = new_cargo;
        }

        private ICommand _EditCargo;

        public ICommand EditCargo => _EditCargo
           ??= new RelayCommand(OnEditCargoCommandExecuted, CanEditCargoCommandExecute);

        private bool CanEditCargoCommandExecute(object arg) => SelectedCargo != null;

        private void OnEditCargoCommandExecuted(object obj)
        {
            var new_cargo = SelectedCargo;

            if (!_UserDialog.Edit(new_cargo))
                return;

            Cargos.Remove(SelectedCargo);

            Cargos.Add(new_cargo);

            _CargosRep.Update(new_cargo);

            SelectedCargo = new_cargo;

        }


        private ICommand _AddNewBrand;

        public ICommand AddNewBrand => _AddNewBrand
           ??= new RelayCommand(OnAddNewBrandCommandExecuted, CanAddNewBrandCommandExecute);

        private bool CanAddNewBrandCommandExecute(object arg) => true;

        private void OnAddNewBrandCommandExecuted(object obj)
        {
            var new_brand = new Brand();

            if (!_UserDialog.Add(new_brand))
                return;

            Brands.Add(_BrandsRep.Add(new_brand));

            SelectedBrand = new_brand;
        }

        private ICommand _EditBrand;

        public ICommand EditBrand => _EditBrand
           ??= new RelayCommand(OnEditBrandCommandExecuted, CanEditBrandCommandExecute);

        private bool CanEditBrandCommandExecute(object arg) => SelectedBrand != null;

        private void OnEditBrandCommandExecuted(object obj)
        {
            var new_brand = SelectedBrand;

            if (!_UserDialog.Edit(new_brand))
                return;

            Brands.Remove(SelectedBrand);

            Brands.Add(new_brand);

            _BrandsRep.Update(new_brand);

            //Automobiles = new ObservableCollection<Automobile>(_AutomobilesRep.Items.ToArray());
            // _AutomobilesViewSource.Source = Automobiles;
            //_AutomobilesViewSource.View.Refresh();
            LoadAutomobiles();

            SelectedBrand = new_brand;

        }




        private ICommand _AddNewCrew;

        public ICommand AddNewCrew => _AddNewCrew
           ??= new RelayCommand(OnAddNewCrewCommandExecuted, CanAddNewCrewCommandExecute);

        private bool CanAddNewCrewCommandExecute(object arg) => true;

        private void OnAddNewCrewCommandExecuted(object obj)
        {
            var new_crew = new Crew();

            if (!_UserDialog.Add(new_crew))
                return;

            _Crews.Add(_CrewsRep.Add(new_crew));

            SelectedCrew = new_crew;
        }


        private ICommand _EditCrew;

        public ICommand EditCrew => _EditCrew
           ??= new RelayCommand(OnEditCrewCommandExecuted, CanEditCrewCommandExecute);

        private bool CanEditCrewCommandExecute(object arg) => SelectedCrew != null;

        private void OnEditCrewCommandExecuted(object obj)
        {
            var new_crew = SelectedCrew;

            if (!_UserDialog.Edit(new_crew))
                return;

            Crews.Remove(SelectedCrew);

            Crews.Add(new_crew);

            _CrewsRep.Update(new_crew);

            LoadFlights();

            LoadDrivers();

            SelectedCrew = new_crew;

        }



        private ICommand _DeleteChecking_account;

        public ICommand DeleteChecking_account => _DeleteChecking_account
            ??= new RelayCommand(OnDeleteChecking_accountCommandExecuted, CanDeleteChecking_accountCommandExecute);

        private bool CanDeleteChecking_accountCommandExecute(object arg) => SelectedChecking_account != null;

        private void OnDeleteChecking_accountCommandExecuted(object obj)
        {
            var checking_account_to_remove = SelectedChecking_account;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить счет: {checking_account_to_remove.Check}?", "Удаление счета"))
                return;

            _Checking_accountsRep.Remove(checking_account_to_remove.Id);

            Checking_Accounts.Remove(checking_account_to_remove);
            if (ReferenceEquals(SelectedChecking_account, checking_account_to_remove))
                SelectedChecking_account = null;

            LoadLegalClients();
        }


        private ICommand _DeleteOrder;

        public ICommand DeleteOrder => _DeleteOrder
            ??= new RelayCommand(OnDeleteOrderCommandExecuted, CanDeleteOrderCommandExecute);

        private bool CanDeleteOrderCommandExecute(object arg) => SelectedOrder != null;

        private void OnDeleteOrderCommandExecuted(object obj)
        {
            var order_to_remove = SelectedOrder;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить заказ номер {order_to_remove.Id}?", "Удаление заказа"))
                return;

            _OrdersRep.Remove(order_to_remove.Id);

            Orders.Remove(order_to_remove);
            if (ReferenceEquals(SelectedOrder, order_to_remove))
                SelectedOrder = null;

            LoadCargos();

        }

        private ICommand _DeletePhysClient;

        public ICommand DeletePhysClient => _DeletePhysClient
            ??= new RelayCommand(OnDeletePhysClientCommandExecuted, CanDeletePhysClientCommandExecute);

        private bool CanDeletePhysClientCommandExecute(object arg) => SelectedPhysClient != null;

        private void OnDeletePhysClientCommandExecuted(object obj)
        {
            var physclient_to_remove = SelectedPhysClient;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить клиента: {physclient_to_remove.Name} {physclient_to_remove.Surname} ?", "Удаление клиента"))
                return;

            _ClientsRep.Remove(physclient_to_remove.Id);

            PhysClients.Remove(physclient_to_remove);
            if (ReferenceEquals(SelectedOrder, physclient_to_remove))
                SelectedPhysClient = null;

            LoadOrders();
        }

        private ICommand _DeleteLegalClient;

        public ICommand DeleteLegalClient => _DeleteLegalClient
            ??= new RelayCommand(OnDeleteLegalClientCommandExecuted, CanDeleteLegalClientCommandExecute);

        private bool CanDeleteLegalClientCommandExecute(object arg) => SelectedLegalClient != null;

        private void OnDeleteLegalClientCommandExecuted(object obj)
        {
            var legalclient_to_remove = SelectedLegalClient;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить клиента: {legalclient_to_remove.Name} {legalclient_to_remove.Surname} ?", "Удаление клиента"))
                return;

            _ClientsRep.Remove(legalclient_to_remove.Id);

            LegalClients.Remove(legalclient_to_remove);
            if (ReferenceEquals(SelectedLegalClient, legalclient_to_remove))
                SelectedLegalClient = null;

            LoadOrders();
        }

        private ICommand _DeleteFlight;

        public ICommand DeleteFlight => _DeleteFlight
            ??= new RelayCommand(OnDeleteFlightCommandExecuted, CanDeleteFlightCommandExecute);

        private bool CanDeleteFlightCommandExecute(object arg) => SelectedFlight != null;

        private void OnDeleteFlightCommandExecuted(object obj)
        {
            var flight_to_remove = SelectedFlight;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить рейс: {flight_to_remove.Id} ?", "Удаление рейса"))
                return;

            _FlightsRep.Remove(flight_to_remove.Id);

            Flights.Remove(flight_to_remove);
            if (ReferenceEquals(SelectedFlight, flight_to_remove))
                SelectedFlight = null;

            LoadOrders();
        }

        private ICommand _DeleteCrew;

        public ICommand DeleteCrew => _DeleteCrew
            ??= new RelayCommand(OnDeleteCrewCommandExecuted, CanDeleteCrewCommandExecute);

        private bool CanDeleteCrewCommandExecute(object arg) => SelectedCrew != null;

        private void OnDeleteCrewCommandExecuted(object obj)
        {
            var crew_to_remove = SelectedCrew;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить экипаж номер: {crew_to_remove.Id} ?", "Удаление экипажа"))
                return;

            _CrewsRep.Remove(crew_to_remove.Id);

            Crews.Remove(crew_to_remove);
            if (ReferenceEquals(SelectedCrew, crew_to_remove))
                SelectedCrew = null;

            LoadFlights();

            LoadDrivers();
        }

        private ICommand _DeleteDriver;

        public ICommand DeleteDriver => _DeleteDriver
            ??= new RelayCommand(OnDeleteDriverCommandExecuted, CanDeleteDriverCommandExecute);

        private bool CanDeleteDriverCommandExecute(object arg) => SelectedDriver != null;

        private void OnDeleteDriverCommandExecuted(object obj)
        {
            var driver_to_remove = SelectedDriver;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить водителя: {driver_to_remove.Name} {driver_to_remove.Surname} ?", "Удаление водителя"))
                return;

            _DriversRep.Remove(driver_to_remove.Id);

            Drivers.Remove(driver_to_remove);
            if (ReferenceEquals(SelectedDriver, driver_to_remove))
                SelectedDriver = null;
        }

        private ICommand _DeleteAutomobile;

        public ICommand DeleteAutomobile => _DeleteAutomobile
            ??= new RelayCommand(OnDeleteAutomobileCommandExecuted, CanDeleteAutomobileCommandExecute);

        private bool CanDeleteAutomobileCommandExecute(object arg) => SelectedAutomobile != null;

        private void OnDeleteAutomobileCommandExecuted(object obj)
        {
            var automobile_to_remove = SelectedAutomobile;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить автомобиль номер: {automobile_to_remove.Id} ?", "Удаление автомобиля"))
                return;

            _AutomobilesRep.Remove(automobile_to_remove.Id);

            Automobiles.Remove(automobile_to_remove);
            if (ReferenceEquals(SelectedAutomobile, automobile_to_remove))
                SelectedAutomobile = null;

            LoadFlights();
        }

        private ICommand _DeleteBrand;

        public ICommand DeleteBrand => _DeleteBrand
            ??= new RelayCommand(OnDeleteBrandCommandExecuted, CanDeleteBrandCommandExecute);

        private bool CanDeleteBrandCommandExecute(object arg) => SelectedBrand != null;

        private void OnDeleteBrandCommandExecuted(object obj)
        {
            var brand_to_remove = SelectedBrand;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить бренд: {brand_to_remove.Name} ?", "Удаление бренда"))
                return;

            _BrandsRep.Remove(brand_to_remove.Id);

            Brands.Remove(brand_to_remove);
            if (ReferenceEquals(SelectedBrand, brand_to_remove))
                SelectedBrand = null;

            LoadAutomobiles();
        }

        private ICommand _DeleteBank;

        public ICommand DeleteBank => _DeleteBank
            ??= new RelayCommand(OnDeleteBankCommandExecuted, CanDeleteBankCommandExecute);

        private bool CanDeleteBankCommandExecute(object arg) => SelectedBank != null;

        private void OnDeleteBankCommandExecuted(object obj)
        {
            var bank_to_remove = SelectedBank;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить банк: {bank_to_remove.Name} ?", "Удаление банка"))
                return;

            _BanksRep.Remove(bank_to_remove.Id);

            Banks.Remove(bank_to_remove);
            if (ReferenceEquals(SelectedBank, bank_to_remove))
                SelectedBank = null;

            LoadChecking_Accounts();
        }

        private ICommand _DeleteUnit;

        public ICommand DeleteUnit => _DeleteUnit
            ??= new RelayCommand(OnDeleteUnitCommandExecuted, CanDeleteUnitCommandExecute);

        private bool CanDeleteUnitCommandExecute(object arg) => SelectedUnit != null;

        private void OnDeleteUnitCommandExecuted(object obj)
        {
            var unit_to_remove = SelectedUnit;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить единицу измерения: {unit_to_remove.Name} ?", "Удаление единицы измерения"))
                return;

            _UnitsRep.Remove(unit_to_remove.Id);

            Units.Remove(unit_to_remove);
            if (ReferenceEquals(SelectedUnit, unit_to_remove))
                SelectedUnit = null;

            LoadCargos();
        }

        private ICommand _DeleteCargo;

        public ICommand DeleteCargo => _DeleteCargo
            ??= new RelayCommand(OnDeleteCargoCommandExecuted, CanDeleteCargoCommandExecute);

        private bool CanDeleteCargoCommandExecute(object arg) => SelectedCargo != null;

        private void OnDeleteCargoCommandExecuted(object obj)
        {
            var cargo_to_remove = SelectedCargo;

            if (!_UserDialog.ConfirmWarning($"Вы хотите удалить груз: {cargo_to_remove.Id} ?", "Удаление груза"))
                return;

            _CargosRep.Remove(cargo_to_remove.Id);

            Cargos.Remove(cargo_to_remove);
            if (ReferenceEquals(SelectedCargo, cargo_to_remove))
                SelectedCargo = null;
        }


        

        private ICommand _SearchOrder;

        public ICommand SearchOrder => _SearchOrder
           ??= new RelayCommand(OnSearchOrderCommandExecuted, CanSearchOrderCommandExecute);

        private bool CanSearchOrderCommandExecute(object arg) => true;

        private void OnSearchOrderCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextOrder != null && SearchTextOrder.Trim() != "")
            {

                if (OrderSearchSelect == "По заказу")
                {
                    Orders = new ObservableCollection<Order>(_OrdersRep.Items.Where(x => (x.Id).ToString() == SearchTextOrder).ToArray());
                    _OrdersViewSource.Source = Orders;
                }

                if (OrderSearchSelect == "По клиенту")
                {

                    Orders = new ObservableCollection<Order>(_OrdersRep.Items.Where(x => (x.Client.Id).ToString() == SearchTextOrder).ToArray());
                    _OrdersViewSource.Source = Orders;

                }

                if (OrderSearchSelect == "По номеру рейса")
                {
                    Orders = new ObservableCollection<Order>(_OrdersRep.Items.Where(x => (x.Flight.Id).ToString() == SearchTextOrder).ToArray());
                    _OrdersViewSource.Source = Orders;
                }
            }
            else
            {
                LoadOrders();
            }

        }

    

        private ICommand _SearchFlight;

        public ICommand SearchFlight => _SearchFlight
           ??= new RelayCommand(OnSearchFlightCommandExecuted, CanSearchFlightCommandExecute);

        private bool CanSearchFlightCommandExecute(object arg) => true;

        private void OnSearchFlightCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextFlight != null && SearchTextFlight.Trim() != "")
            {

                if (FlightSearchSelect == "По номеру")
                {
                    Flights = new ObservableCollection<Flight>(_FlightsRep.Items.Where(x => (x.Id).ToString() == SearchTextFlight).ToArray());
                    _FlightsViewSource.Source = Flights;
                }

                if (FlightSearchSelect == "По экипажу")
                {

                    Flights = new ObservableCollection<Flight>(_FlightsRep.Items.Where(x => (x.Crew.Id).ToString() == SearchTextFlight).ToArray());
                    _FlightsViewSource.Source = Flights;
                }

                if (FlightSearchSelect == "По номеру автомобиля")
                {
                    Flights = new ObservableCollection<Flight>(_FlightsRep.Items.Where(x => (x.Automobile.Id).ToString() == SearchTextFlight).ToArray());
                    _FlightsViewSource.Source = Flights;
                }

            }
            else
            {
                LoadFlights();
            }

        }


        private ICommand _SearchDriver;

        public ICommand SearchDriver => _SearchDriver
           ??= new RelayCommand(OnSearchDriverCommandExecuted, CanSearchDriverCommandExecute);

        private bool CanSearchDriverCommandExecute(object arg) => true;

        private void OnSearchDriverCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextDriver != null && SearchTextDriver.Trim() != "")
            {

                if (DriverSearchSelect == "По номеру экипажа")
                {
                    Drivers = new ObservableCollection<Driver>(_DriversRep.Items.Where(x => (x.Crew.Id).ToString() == SearchTextDriver).ToArray());
                    _DriversViewSource.Source = Drivers;
                }

                if (DriverSearchSelect == "По категории")
                {

                    Drivers = new ObservableCollection<Driver>(_DriversRep.Items.Where(x => x.Category.Name == SearchTextDriver).ToArray());
                    _DriversViewSource.Source = Drivers;
                }

                if (DriverSearchSelect == "По классности")
                {
                    Drivers = new ObservableCollection<Driver>(_DriversRep.Items.Where(x => x.Class.Name == SearchTextDriver).ToArray());
                    _DriversViewSource.Source = Drivers;
                }

            }
            else
            {
                LoadDrivers();
            }

        }

        private ICommand _SearchAutomobile;

        public ICommand SearchAutomobile => _SearchAutomobile
           ??= new RelayCommand(OnSearchAutomobileCommandExecuted, CanSearchAutomobileCommandExecute);

        private bool CanSearchAutomobileCommandExecute(object arg) => true;

        private void OnSearchAutomobileCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextAutomobile != null && SearchTextAutomobile.Trim() != "")
            {
                Automobiles = new ObservableCollection<Automobile>(_AutomobilesRep.Items.Where(x => x.Brand.Name.Contains(SearchTextAutomobile)).ToArray());
                _AutomobilesViewSource.Source = Automobiles;

            }
            else
            {
                LoadAutomobiles();
            }

        }

        private ICommand _SearchCheck;

        public ICommand SearchCheck => _SearchCheck
           ??= new RelayCommand(OnSearchCheckCommandExecuted, CanSearchCheckCommandExecute);

        private bool CanSearchCheckCommandExecute(object arg) => true;

        private void OnSearchCheckCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextCheck != null && SearchTextCheck.Trim() != "")
            {

                if (CheckSearchSelect == "По номеру")
                {
                    Checking_Accounts = new ObservableCollection<Checking_account>(_Checking_accountsRep.Items.Where(x => x.Check.Contains(SearchTextCheck)).ToArray());
                    _Checking_AccountsViewSource.Source = Checking_Accounts;
                }

                if (CheckSearchSelect == "По банку")
                {
                    Checking_Accounts = new ObservableCollection<Checking_account>(_Checking_accountsRep.Items.Where(x => x.Bank.Name.Contains(SearchTextCheck)).ToArray());
                    _Checking_AccountsViewSource.Source = Checking_Accounts;
                }

            }
            else
            {
                LoadChecking_Accounts();
            }

        }


        private ICommand _SearchCargo;

        public ICommand SearchCargo => _SearchCargo
           ??= new RelayCommand(OnSearchCargoCommandExecuted, CanSearchCargoCommandExecute);

        private bool CanSearchCargoCommandExecute(object arg) => true;

        private void OnSearchCargoCommandExecuted(object obj)
        {

            //CollectionViewSource collectionView = _OrdersViewSource;

            // List<Order> SearchOrders = Orders.ToList();

            //List<Order> SearchOrders = _OrdersRep.Items.ToList();

            if (SearchTextCargo != null && SearchTextCargo.Trim() != "")
            {

                if (CargoSearchSelect == "По единицам измерения")
                {
                    Cargos = new ObservableCollection<Cargo>(_CargosRep.Items.Where(x => x.Unit.Name.Contains(SearchTextCargo)).ToArray());
                    _CargosViewSource.Source = Cargos;
                }

                if (CargoSearchSelect == "По названию")
                {
                    Cargos = new ObservableCollection<Cargo>(_CargosRep.Items.Where(x => x.Name.Contains(SearchTextCargo)).ToArray());
                    _CargosViewSource.Source = Cargos;
                }

                if (CargoSearchSelect == "По заказу")
                {
                    Cargos = new ObservableCollection<Cargo>(_CargosRep.Items.Where(x => (x.Order.Id).ToString() == SearchTextCargo).ToArray());
                    _CargosViewSource.Source = Cargos;
                }

            }
            else
            {
                LoadCargos();
            }

        }


        private ICommand _ExcelImport;

        public ICommand ExcelImport => _ExcelImport
           ??= new RelayCommand(OnExcelImportCommandExecuted, CanExcelImportCommandExecute);

        private bool CanExcelImportCommandExecute(object arg) => true;

        private void OnExcelImportCommandExecuted(object obj)
        {
            string filepath = $"{Environment.CurrentDirectory}\\new_file.xlsx";


            if (!_UserDialog.ConfirmWarning($"Вы хотити импортировать: {filepath}: ?", "Ипмпорт"))
            {
                return;
            }
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;
                //List<Order> orders = new List<Order>();
                int rCnt;
                int cCnt;
                int rw = 0;
                int cl = 0;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(filepath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;
                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    string[] str = new string[cl + 1];
                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {

                        str[cCnt] = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2.ToString();

                    }
                    //string[] cargos = str[8].Split(" ");
                    //ICollection<Cargo> collection = new List<Cargo>();
                    //foreach (var item in cargos)
                    //{
                      //  collection.Add(new Cargo { Id = int.Parse(item) });
                    //}
                    Order order = new Order { Client = _ClientsRep.Items.Where(x => x.Id == int.Parse(str[1])).FirstOrDefault(), OrderData = DateTime.ParseExact(str[2], "dd/MM/yyyy", null), LoadingAddress = str[3], UnloadingAddress = str[4], RouteLength = str[5], OrderCost = Convert.ToDecimal(str[6]), Flight = _FlightsRep.Items.Where(x => x.Id == int.Parse(str[7])).FirstOrDefault()};
                    // if((_ClientsRep.Items.Any(x => x.Id == order.Client.Id)) && (_FlightsRep.Items.Any(x => x.Id == order.Flight.Id))
                    Orders.Add(_OrdersRep.Add(order));
                    // orders.Add(order);          
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            

        }


        private ICommand _ExcelExport;

        public ICommand ExcelExport => _ExcelExport
           ??= new RelayCommand(OnExcelExportCommandExecuted, CanExcelExportCommandExecute);

        private bool CanExcelExportCommandExecute(object arg) => true;

        private void OnExcelExportCommandExecuted(object obj)
        {
            if (!_UserDialog.ConfirmWarning("Точно хотите экспортировать базу данных?", "Экпорт")) { return; }

            string fileName = "DbExport";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);

            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel установлен неправильно!!");
                return;
            }
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Excel.Sheets worksheets = xlWorkBook.Worksheets;

            var xlCargos = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlCargos.Name = "Cargos";
            xlCargos.Cells[1, 1] = "Номер груза";
            xlCargos.Cells[1, 2] = "Назначение";
            xlCargos.Cells[1, 3] = "Количесво";
            xlCargos.Cells[1, 4] = "Вес";
            xlCargos.Cells[1, 5] = "Единицы измерения";
            xlCargos.Cells[1, 6] = "Страховая стоимость";
            xlCargos.Cells[1, 7] = "Заказ";

            int row = 2;

            foreach(var item in _CargosRep.Items)
            {
                xlCargos.Cells[row, 1] = item.Id;
                xlCargos.Cells[row, 2] = item.Name;
                xlCargos.Cells[row, 3] = item.Amount;
                xlCargos.Cells[row, 4] = item.Weight;
                xlCargos.Cells[row, 5] = item.Unit.ToString();
                xlCargos.Cells[row, 6] = item.InsuranceValue;
                xlCargos.Cells[row, 7] = item.Order.ToString();
                row++;
            }

            var xlUnits = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlUnits.Name = "Units";
            xlUnits.Cells[1, 1] = "Название";
            
            row = 2;

            foreach (var item in _UnitsRep.Items)
            {
                xlUnits.Cells[row, 1] = item.Name;
                row++;
            }

            var xlBanks = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlBanks.Name = "Banks";
            xlBanks.Cells[1, 1] = "Название";

            row = 2;

            foreach (var item in _BanksRep.Items)
            {
                xlBanks.Cells[row, 1] = item.Name;
                row++;
            }

            var xlChecks = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlChecks.Name = "Checks";
            xlChecks.Cells[1, 1] = "Номер";
            xlChecks.Cells[1, 2] = "Банк";

            row = 2;

            foreach (var item in _Checking_accountsRep.Items)
            {
                xlChecks.Cells[row, 1] = item.Check;
                xlChecks.Cells[row, 2] = item.Bank.ToString();
                row++;
            }

            var xlBrands = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlBrands.Name = "Brands";
            xlBrands.Cells[1, 1] = "Название";

            row = 2;

            foreach (var item in _BrandsRep.Items)
            {
                xlBrands.Cells[row, 1] = item.Name;
                row++;
            }

            var xlAutomobiles = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlAutomobiles.Name = "Automobiles";
            xlAutomobiles.Cells[1, 1] = "Номер";
            xlAutomobiles.Cells[1, 2] = "Название";
            xlAutomobiles.Cells[1, 3] = "Гос.Номер";
            xlAutomobiles.Cells[1, 4] = "Бренд";
            xlAutomobiles.Cells[1, 5] = "Грузоподъемность";
            xlAutomobiles.Cells[1, 6] = "Назначение";
            xlAutomobiles.Cells[1, 7] = "Год выпуска";
            xlAutomobiles.Cells[1, 8] = "Год ремонта";
            xlAutomobiles.Cells[1, 9] = "Пробег";

            row = 2;

            foreach (var item in _AutomobilesRep.Items)
            {
                xlAutomobiles.Cells[row, 1] = item.Id;
                xlAutomobiles.Cells[row, 2] = item.Name;
                xlAutomobiles.Cells[row, 3] = item.GosNumber;
                xlAutomobiles.Cells[row, 4] = item.Brand.ToString();
                xlAutomobiles.Cells[row, 5] = item.LoadCapacity;
                xlAutomobiles.Cells[row, 6] = item.Purpose;
                xlAutomobiles.Cells[row, 7] = item.YearOfIssue;
                xlAutomobiles.Cells[row, 8] = item.YearOfRepair;
                xlAutomobiles.Cells[row, 9] = item.Mileage;
                row++;
            }

            var xlDrivers = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlDrivers.Name = "Drivers";
            xlDrivers.Cells[1, 1] = "Имя";
            xlDrivers.Cells[1, 2] = "Фамилия";
            xlDrivers.Cells[1, 3] = "Номер экипажа";
            xlDrivers.Cells[1, 4] = "Дата рождения";
            xlDrivers.Cells[1, 5] = "Стаж";
            xlDrivers.Cells[1, 6] = "Категория";
            xlDrivers.Cells[1, 7] = "Классность";
     

            row = 2;

            foreach (var item in _DriversRep.Items)
            {
                xlDrivers.Cells[row, 1] = item.Name;
                xlDrivers.Cells[row, 2] = item.Surname;
                xlDrivers.Cells[row, 3] = item.Crew.ToString();
                xlDrivers.Cells[row, 4] = item.YearOfBirth;
                xlDrivers.Cells[row, 5] = item.WorkExperience;
                xlDrivers.Cells[row, 6] = item.Category.ToString();
                xlDrivers.Cells[row, 7] = item.Class.ToString();
                row++;
            }

            var xlCrews = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlCrews.Name = "Crews";
            xlCrews.Cells[1, 1] = "Номер";
            xlCrews.Cells[1, 2] = "Название";


            row = 2;

            foreach (var item in _CrewsRep.Items)
            {
                xlCrews.Cells[row, 1] = item.Id;
                xlCrews.Cells[row, 2] = item.Name;
                
                row++;
            }

            var xlFlights = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlFlights.Name = "Flights";
            xlFlights.Cells[1, 1] = "Номер";
            xlFlights.Cells[1, 2] = "Время прибытия";
            xlFlights.Cells[1, 3] = "Экипаж";
            xlFlights.Cells[1, 4] = "Номер Автомобиля";


            row = 2;

            foreach (var item in _FlightsRep.Items)
            {
                xlFlights.Cells[row, 1] = item.Id;
                xlFlights.Cells[row, 2] = item.ArrivalDate;
                xlFlights.Cells[row, 3] = item.Crew.ToString();
                xlFlights.Cells[row, 4] = item.Automobile.ToString();

                row++;
            }

            var xlLegalClients = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlLegalClients.Name = "LegalClients";
            xlLegalClients.Cells[1, 1] = "Номер";
            xlLegalClients.Cells[1, 2] = "Имя";
            xlLegalClients.Cells[1, 3] = "Фамилия";
            xlLegalClients.Cells[1, 4] = "Руководитель";
            xlLegalClients.Cells[1, 5] = "Номер";
            xlLegalClients.Cells[1, 6] = "Юридический адрес";
            xlLegalClients.Cells[1, 7] = "Расчетный счет";
            xlLegalClients.Cells[1, 8] = "Инн";


            row = 2;

            //var qw = _ClientsRep.Items.Where(x => x.IsPhysical == false);

            foreach (var item in LegalClients)
            {
                xlLegalClients.Cells[row, 1] = item.Id;
                xlLegalClients.Cells[row, 2] = item.Name;
                xlLegalClients.Cells[row, 3] = item.Surname;
                xlLegalClients.Cells[row, 4] = item.LegalPersonName;
                xlLegalClients.Cells[row, 5] = item.PhoneNumber;
                xlLegalClients.Cells[row, 6] = item.LegalAdress;
                xlLegalClients.Cells[row, 7] = item.Checking_Account.ToString();
                xlLegalClients.Cells[row, 8] = item.Inn;
                row++;
            }

            var xlPhysClients = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlPhysClients.Name = "PhysClients";
            xlPhysClients.Cells[1, 1] = "Номер";
            xlPhysClients.Cells[1, 2] = "Имя";
            xlPhysClients.Cells[1, 3] = "Фамилия";
            xlPhysClients.Cells[1, 4] = "Серия и номер пасспорта";
            xlPhysClients.Cells[1, 5] = "Номер телефона";
            xlPhysClients.Cells[1, 6] = "Дата получения";
            xlPhysClients.Cells[1, 7] = "Кем выдан";


            row = 2;

            //var wq = _ClientsRep.Items.Where(x => x.IsPhysical == true);

            foreach (var item in PhysClients)
            {
                xlPhysClients.Cells[row, 1] = item.Id;
                xlPhysClients.Cells[row, 2] = item.Name;
                xlPhysClients.Cells[row, 3] = item.Surname;
                xlPhysClients.Cells[row, 4] = item.SeriesAndNumberPass;
                xlPhysClients.Cells[row, 5] = item.PhoneNumber;
                xlPhysClients.Cells[row, 6] = item.DataOfIssue;
                xlPhysClients.Cells[row, 7] = item.IssuedBy;
                row++;
            }


            var xlOrders = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlOrders.Name = "Orders";
            xlOrders.Cells[1, 1] = "Номер заказа";
            xlOrders.Cells[1, 2] = "Номер клиента";
            xlOrders.Cells[1, 3] = "Дата заказа";
            xlOrders.Cells[1, 4] = "Пункт погрузки";
            xlOrders.Cells[1, 5] = "Пункт разгрузки";
            xlOrders.Cells[1, 6] = "Длина маршрута";
            xlOrders.Cells[1, 7] = "Стоимость заказа";
            xlOrders.Cells[1, 8] = "Номер рейса";
            xlOrders.Cells[1, 9] = "Перечень груза";

            row = 2;
            foreach(var item in _OrdersRep.Items)
            {
                xlOrders.Cells[row, 1] = item.Id;
                xlOrders.Cells[row, 2] = item.Client.ToString();
                xlOrders.Cells[row, 3] = item.OrderData;
                xlOrders.Cells[row, 4] = item.LoadingAddress;
                xlOrders.Cells[row, 5] = item.UnloadingAddress;
                xlOrders.Cells[row, 6] = item.RouteLength;
                xlOrders.Cells[row, 7] = item.OrderCost;
                xlOrders.Cells[row, 8] = item.Flight.ToString();
                StringBuilder cargos = new StringBuilder();
                if (item.Cargos != null)
                {
                    foreach (var cargo in item.Cargos)
                    {
                        cargos.Append(cargo.Id).Append(" ");
                    }
                }
                xlOrders.Cells[row, 9] = cargos.ToString().Trim();
                    
                row++;
            }


            xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            MessageBox.Show("Файл excel был создан, вы можете найти его в загрузках");
           


        }







        private string _OrderSearchSelect;

        public string OrderSearchSelect { get { return _OrderSearchSelect; } set => Set(ref _OrderSearchSelect, value); }


        private string _FlightSearchSelect;

        public string FlightSearchSelect { get { return _FlightSearchSelect; } set => Set(ref _FlightSearchSelect, value); }

        private string _DriverSearchSelect;

        public string DriverSearchSelect { get { return _DriverSearchSelect; } set => Set(ref _DriverSearchSelect, value); }

        private string _CargoSearchSelect;

        private string _CheckSearchSelect;

        public string CheckSearchSelect { get { return _CheckSearchSelect; } set => Set(ref _CheckSearchSelect, value); }

        public string CargoSearchSelect { get { return _CargoSearchSelect; } set => Set(ref _CargoSearchSelect, value); }

        public List<string> OrderSearchList
        {
            get
            {

               return new List<string> {
                "По заказу",
            "По клиенту",
            "По номеру рейса"};
            }
        }
            

        public List<string> FlightSearchList 
        {
            get
            {
                return new List<string>
                {
                    "По номеру",
                    "По экипажу",
                    "По номеру автомобиля"
                };
            }
        }

        public List<string> DriverSearchList 
        {
            get
            {
                return new List<string>{
            "По номеру экипажа",
            "По категории",
            "По классности"
                };
                }
        }

        

        public List<string> CargoSearchList 
        {
            get
            {
                return new List<string>{
            "По единицам измерения",
            "По названию",
            "По заказу"
                };
                }
        }

        public List<string> CheckSearchList
        {
            get
            {
                return new List<string>{
            "По номеру",
            "По банку"
                };
            }
        }

        



        public MainViewModel(IRepository<Bank> Banks, 
                             IRepository<Checking_account> Checking_accounts, 
                             IRepository<Crew> Crews, 
                             IRepository <Brand> Brands, 
                             IRepository<Automobile> Automobiles,
                             IRepository<Driver> Drivers,
                             IRepository<Flight> Flights,
                             IRepository<Cargo> Cargos,
                             IRepository<Unit> Units,
                             IRepository<Client> Clients, 
                             IRepository<Order> Orders, 
                             IUserDialog userDialog)
        {
            _ClientsRep = Clients;
            _OrdersRep = Orders;
            _BanksRep = Banks;
            _Checking_accountsRep = Checking_accounts;
            _CrewsRep = Crews;
            _BrandsRep = Brands;
            _AutomobilesRep = Automobiles;
            _DriversRep = Drivers;
            _FlightsRep = Flights;
            _UnitsRep = Units;
            _CargosRep = Cargos;
            _UserDialog = userDialog;
        }



        


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
