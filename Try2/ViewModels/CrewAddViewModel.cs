using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using MathCore.WPF.ViewModels;

namespace Try2.ViewModels
{
    internal class CrewAddViewModel: ViewModel
    {

        public string Title { get; set; }

        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        public CrewAddViewModel()
        {
            Title = "CrewAddView";

        }

        public CrewAddViewModel(Crew crew)
        {
            Title = "CrewEditView";

            Name = crew.Name;

        }

    }
}
