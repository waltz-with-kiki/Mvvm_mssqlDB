using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Try2.Context;
using Try2.Interfaces;

namespace Try2.ViewModels
{
    internal class Cheking_AccountAddViewModel : ViewModel
    {
        public string Title { get; set; }

        private readonly IRepository<Bank> _Banks;

        private readonly IRepository<Checking_account> _Accounts;

        private string _Check;



        public string Check { get { return _Check; } set => Set(ref _Check, value); }

        private Bank _Bank;

        public Bank Bank { get { return _Bank; } set => Set(ref _Bank, value); }

        public List<Bank> Banks { get
            {
                return _Banks.Items.ToList();
            } }


        private ICommand _CreateNewAccount;

        public ICommand CreateNewAccount => _CreateNewAccount
           ??= new RelayCommand(OnCreateNewAccountCommandExecuted, CanCreateNewAccountCommandExecute);

        private bool CanCreateNewAccountCommandExecute(object arg) => true;

        /// <summary>Логика выполнения - Добавление новой книги</summary>
        private void OnCreateNewAccountCommandExecuted(object obj)
        {
            var rnd = new Random();
            
                Check = rnd.NextInt64(1000000000, 9999999999).ToString()+rnd.NextInt64(1000000000, 9999999999).ToString();            
        }

        public Cheking_AccountAddViewModel()
        {

        }

        public Cheking_AccountAddViewModel(IRepository<Bank> banks, IRepository<Checking_account> accounts)
        {
            _Banks = banks;
            _Accounts = accounts;
            Title = "Checking_AccountAddView";

        }

        public Cheking_AccountAddViewModel(Checking_account checking_Account ,IRepository<Bank> banks, IRepository<Checking_account> accounts)
        {
            _Banks = banks;
            _Accounts = accounts;
            Title = "Checking_AccountEditView";


            Check = checking_Account.Check;
            Bank = checking_Account.Bank;

        }

    }
}
