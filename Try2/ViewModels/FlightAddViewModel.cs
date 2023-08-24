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
    internal class FlightAddViewModel: ViewModel
    {

        public string Title { get; set; }

        private readonly IRepository<Crew> _Crews;

        private readonly IRepository<Automobile> _Automobiles;

        public List<Crew> Crews
        {
            get
            {
                return _Crews.Items.ToList();
            }
        }

        public List<Automobile> Automobiles
        {
            get
            {
                return _Automobiles.Items.ToList();
            }
        }


        // private int _Id;

        //  public int Id { get { return _Id; } set => Set(ref _Id, value); }



        private DateTime _ArrivalDate;

        public DateTime ArrivalDate { get { return _ArrivalDate; } set => Set(ref _ArrivalDate, value); }

        private Crew _CrewId;

        public Crew CrewId { get { return _CrewId; } set => Set(ref _CrewId, value); }

        private Automobile _AutomobileId;

        public Automobile AutomobileId { get { return _AutomobileId; } set => Set(ref _AutomobileId, value); }


        public FlightAddViewModel()
        {

        }

        public FlightAddViewModel(IRepository<Crew> crews, IRepository<Automobile> automobiles)
        {
            _Crews = crews;
            _Automobiles = automobiles;
            Title = "FlightAddView";

        }

        public FlightAddViewModel(Flight flight, IRepository<Crew> crews, IRepository<Automobile> automobiles)
        {
            _Crews = crews;
            _Automobiles = automobiles;
            Title = "FlightEditView";

            ArrivalDate = flight.ArrivalDate;
            CrewId = flight.Crew;
            AutomobileId = flight.Automobile;

        }
    }
}
