using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;

namespace Try2.ViewModels
{
    internal class PhysClientAddViewModel : ViewModel

    {

        public string Title { get; set; }


        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        private string _Surname;

        public string Surname { get { return _Surname; } set => Set(ref _Surname, value); }

        private string _SerialAndNumber;

        public string SerialAndNumber { get { return _SerialAndNumber; } set => Set(ref _SerialAndNumber, value); }

        private string _PhoneNumber;

        public string PhoneNumber { get { return _PhoneNumber; } set => Set(ref _PhoneNumber, value); }

        private DateTime _DataOfIssue;

        public DateTime DataOfIssue { get { return _DataOfIssue; } set => Set(ref _DataOfIssue, value); }

        private string _IssuedBy;

        public string IssuedBy { get { return _IssuedBy; } set => Set(ref _IssuedBy, value); }

        public PhysClientAddViewModel()
        {
            Title = "PhysClientAddView";

        }

        public PhysClientAddViewModel(Client client)
        {
            Title = "PhysClientEditView";


            Name = client.Name;
            Surname = client.Surname;
            SerialAndNumber = client.SeriesAndNumberPass;
            PhoneNumber = client.PhoneNumber;
            DataOfIssue = client.DataOfIssue;
            IssuedBy = client.IssuedBy;


        }

    }
}
