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
    internal class OrderEditorViewModel :ViewModel
    {

        public string Title { get; set; }

        private readonly IRepository<Client> _Clients;

        private readonly IRepository<Flight> _Flights;

        public List<Client> CClients {
            get
            {
                return _Clients.Items.ToList();
            }
        }

        public List<Flight> CFlights
        {
            get
            {
                return _Flights.Items.ToList();
            }
        }


        public int OrderId { get; set; }

        private DateTime _OrderDate;

        public DateTime OrderDate { get { return _OrderDate; } set => Set(ref _OrderDate, value); }

        private Client _ClientId;

        public Client ClientId { get { return _ClientId; } set => Set(ref _ClientId, value); }

        private string _LoadingAddress;

        public string LoadingAddress { get { return _LoadingAddress; } set => Set(ref _LoadingAddress, value); }


        private string _UploadingAddress;

        public string UploadingAddress { get { return _UploadingAddress; } set => Set(ref _UploadingAddress, value); }

        private string _RouteLength;

        public string RouteLength { get { return _RouteLength; } set => Set(ref _RouteLength, value); }

        private decimal _Cost;

        public decimal Cost { get { return _Cost; } set => Set(ref _Cost, value); }

        private Flight _Flight;

        public Flight Flight { get { return _Flight; } set => Set(ref _Flight, value); }



        public OrderEditorViewModel()
        {
            
        }


        public OrderEditorViewModel(IRepository<Client> Clients, IRepository<Flight> Flights)
        {
            _Clients = Clients;
            _Flights = Flights;
            Title = "OrderAddView";
            OrderDate = DateTime.Now;
            
        }

        public OrderEditorViewModel(Order order, IRepository<Client> Clients, IRepository<Flight> Flights)
        {
            _Clients = Clients;
            _Flights = Flights;
            Title = "OrderEditView";

            OrderId = order.Id;
            OrderDate = order.OrderData;
            ClientId = order.Client;
            LoadingAddress = order.LoadingAddress;
            UploadingAddress = order.UnloadingAddress;
            RouteLength = order.RouteLength;
            Cost = order.OrderCost;
            Flight = order.Flight;
        }


    }
}
