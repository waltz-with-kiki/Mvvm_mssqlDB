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
    internal class DriverAddViewModel :ViewModel
    {

        public string Title { get; set; }

        private readonly IRepository<Crew> _Crews;

        private readonly IRepository<DriverCategory> _Categories;

        private readonly IRepository<DriverClass> _Classes;

        public List<Crew> Crews
        {
            get
            {
                return _Crews.Items.ToList();
            }
        }

        public List<DriverCategory> Categories
        {
            get
            {
                return _Categories.Items.ToList();
            }
        }

        public List<DriverClass> Classes
        {
            get
            {
                return _Classes.Items.ToList();
            }
        }



        private string _Name;

        public string Name { get { return _Name; } set => Set(ref _Name, value); }

        private string _Surname;

        public string Surname { get { return _Surname; } set => Set(ref _Surname, value);}



        private Crew _CrewId;

        public Crew CrewId { get { return _CrewId; } set => Set(ref _CrewId, value); }

        private DateTime _YearOfBirth;

        public DateTime YearOfBirth { get { return _YearOfBirth; } set => Set(ref _YearOfBirth, value); }

        private string _WorkExperience;
        
        public string WorkExperience { get { return _WorkExperience; } set => Set(ref _WorkExperience, value); }

        private DriverCategory _CategoryId;

        public DriverCategory CategoryId { get { return _CategoryId; } set => Set(ref _CategoryId, value); }

        private DriverClass _ClassId;

        public DriverClass ClassId { get { return _ClassId; } set => Set(ref _ClassId, value); }

        public DriverAddViewModel()
        {

        }

        public DriverAddViewModel(IRepository<Crew> crews, IRepository<DriverCategory> categories, IRepository<DriverClass> classes)
        {
            _Crews = crews;
            _Categories = categories;
            _Classes = classes;

            Title = "DriverAddView";
        }

        public DriverAddViewModel(Driver driver, IRepository<Crew> crews, IRepository<DriverCategory> categories, IRepository<DriverClass> classes)
        {
            _Crews = crews;
            _Categories = categories;
            _Classes = classes;
            Title = "DriverEditView";

            Name = driver.Name;
            Surname = driver.Surname;
            CrewId = driver.Crew;
            YearOfBirth = driver.YearOfBirth;
            WorkExperience = driver.WorkExperience;
            CategoryId = driver.Category;
            ClassId = driver.Class;

        }

    }
}
