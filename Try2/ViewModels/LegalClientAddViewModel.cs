using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Interfaces;
using MathCore.WPF.ViewModels;

namespace Try2.ViewModels
{
    internal class LegalClientAddViewModel :ViewModel
    {

        public string Title { get; set; }

        private readonly IRepository<Checking_account> _Checking_Accounts;

        public List<Checking_account> Checking_Accounts { get
            {
                return _Checking_Accounts.Items.ToList();
            } }


        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        private string _Surname;

        public string Surname { get { return _Surname; } set => Set(ref _Surname, value); }

        private string _LegalPersonName;

        public string LegalPersonName { get { return _LegalPersonName; } set => Set(ref _LegalPersonName, value); }

        private string _PhoneNumber;

        public string PhoneNumber { get { return _PhoneNumber; } set => Set(ref _PhoneNumber, value); }

        private string _LegalAdress;

        public string LegalAdress { get { return _LegalAdress; } set => Set(ref _LegalAdress, value); }

        private Checking_account _Checking_account;

        public Checking_account Checking_account { get { return _Checking_account; } set => Set(ref _Checking_account, value); }

        private string _Inn;

        public string Inn { get { return _Inn; } set => Set(ref _Inn, value); }

        public LegalClientAddViewModel()
        {

        }

        public LegalClientAddViewModel(IRepository<Checking_account> checking_accounts)
        {
            _Checking_Accounts = checking_accounts;
            Title = "LegalClientAddView";

        }

        public LegalClientAddViewModel(Client client, IRepository<Checking_account> checking_accounts)
        {
            _Checking_Accounts = checking_accounts;
            Title = "LegalClientEditView";

            Name = client.Name;
            Surname = client.Surname;
            LegalPersonName = client.LegalPersonName;
            PhoneNumber = client.PhoneNumber;
            LegalAdress = client.LegalAdress;
            Checking_account = client.Checking_Account;
            Inn = client.Inn;

        }
    }
}
