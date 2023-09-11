using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Interfaces;

namespace Try2.ViewModels
{
    internal class CargoAddViewModel : ViewModel
    {

        public string Title { get; set; }

        private readonly IRepository<Unit> _Units;

        private readonly IRepository<Order> _Orders;

        public List<Unit> Units { get { return _Units.Items.ToList(); } }

        public List<Order> Orders { get { return _Orders.Items.ToList(); } }



        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        private string? _Amount;

        public string? Amount { get { return _Amount; } set => Set(ref _Amount, value); }

        private string? _Weight;

        public string? Weight { get { return _Weight; } set => Set(ref _Weight, value); }

        private Unit _Unit;

        public Unit Unit { get { return _Unit; } set => Set(ref _Unit, value); }


        private string? _Value;

        public string? Value { get { return _Value; } set => Set(ref _Value, value); }


        private Order _Order;

        public Order Order { get { return _Order; } set => Set(ref _Order, value); }


        public CargoAddViewModel()
        {

        }

        public CargoAddViewModel(IRepository<Unit> units, IRepository<Order> orders)
        {
            _Units = units;
            _Orders = orders;
            Title = "CargoAddView";
        }

        public CargoAddViewModel(Cargo cargo, IRepository<Unit> units, IRepository<Order> orders)
        {
            _Units = units;
            _Orders = orders;
            Title = "CargoEditView";


            Name = cargo.Name;
            Amount = cargo.Amount.ToString();
            Weight = cargo.Weight.ToString();
            Unit = cargo.Unit;
            Value = cargo.InsuranceValue.ToString();
            Order = cargo.Order;
        }
    }
}
