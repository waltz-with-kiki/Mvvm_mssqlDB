using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Interfaces;

namespace Try2.ViewModels
{
    internal class AutomobileAddViewModel: ViewModel
    {

        public string Title { get; set; }


        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        private string _GosNumber;

        public string GosNumber { get { return _GosNumber; } set => Set(ref _GosNumber, value); }


        private Brand _Brand;

        public Brand Brand { get { return _Brand; } set => Set(ref _Brand, value); }

        private string _LoadCapacity;

        public string LoadCapacity { get { return _LoadCapacity; } set => Set(ref _LoadCapacity, value); }


    public string Error => null;

        private string _Purpose;

        public string Purpose { get { return _Purpose; } set => Set(ref _Purpose, value); }

        private DateTime _YearOfIssue;

        public DateTime YearOfIssue { get { return _YearOfIssue; } set => Set(ref _YearOfIssue, value); }

        private DateTime _YearOfRepair;

        public DateTime YearOfRepair { get { return _YearOfRepair; } set => Set(ref _YearOfRepair, value); }

        private string _Millage;

        public string Millage { get { return _Millage; } set => Set(ref _Millage, value); }



        private readonly IRepository<Brand> _Brands;

        public List<Brand> Brands { get
            {
                return _Brands.Items.ToList();
            } }

        public AutomobileAddViewModel()
        {

        }

        public AutomobileAddViewModel(IRepository<Brand> brands)
        {
            Title = "AutomobileAddView";
            _Brands = brands;
        }


        public AutomobileAddViewModel(Automobile automobile, IRepository<Brand> brands)
        {
            Title = "AutomobileEditView";

            _Brands = brands;

            Name = automobile.Name;
            GosNumber = automobile.GosNumber;
            Brand = automobile.Brand;
            LoadCapacity = automobile.LoadCapacity.ToString();
            Purpose = automobile.Purpose;
            YearOfIssue = automobile.YearOfIssue;
            YearOfRepair = automobile.YearOfRepair;
            Millage = automobile.Mileage.ToString();

        }



    }
}
