using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;

namespace Try2.ViewModels
{
    internal class BankAddViewModel :ViewModel
    {
        public string Title { get; set; }


        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        public BankAddViewModel()
        {
            Title = "BankAddView";

        }

        public BankAddViewModel(Bank bank)
        {
            Title = "BankEditView";

            Name = bank.Name;

        }
    }
}
