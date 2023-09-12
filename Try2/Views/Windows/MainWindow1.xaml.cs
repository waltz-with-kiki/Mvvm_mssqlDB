using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Try2.Context;
using Try2.Interfaces;
using Try2.ViewModels;
using Try2.Hash;
using System.Globalization;

namespace Try2.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {

        User CurrentUser { get; set; }

        private readonly IRepository<User> _UserRep;


        public MainWindow1(User user, IRepository<User> users)
        {
            CurrentUser = user;

           // Loaded += MainWindow_Loaded;

            InitializeComponent();
            _UserRep = users;
            InitializeUser(user);

           
        }

        // Выход за mvvm (failed to do realize other solution)
        private void InitializeUser(User user)
        {
            OrdersGrid.IsEnabled = user.Right.R;
            PhysClientsGrid.IsEnabled = user.Right.R;
            LegalClientsGrid.IsEnabled = user.Right.R;
            FlightsGrid.IsEnabled = user.Right.R;
            CrewsGrid.IsEnabled = user.Right.R;
            DriversGrid.IsEnabled = user.Right.R;
            AutomobilesGrid.IsEnabled = user.Right.R;
            BrandsGrid.IsEnabled = user.Right.R;
            Checking_AccountsGrid.IsEnabled = user.Right.R;
            BanksGrid.IsEnabled = user.Right.R;
            UnitsGrid.IsEnabled = user.Right.R;
            CargosGrid.IsEnabled = user.Right.R;

            TextOrderSearch.IsEnabled = user.Right.R;
            SearchOrder.IsEnabled = user.Right.R;
            SearchOrderSelect.IsEnabled = user.Right.R;
            TextSearchFlight.IsEnabled = user.Right.R;
            SearchFlight.IsEnabled = user.Right.R;
            FlightSearchSelect.IsEnabled = user.Right.R;
            TextSearchDriver.IsEnabled = user.Right.R;
            SearchDriver.IsEnabled = user.Right.R;
            DriverSearchSelect.IsEnabled = user.Right.R;
            TextSearchAutomobile.IsEnabled = user.Right.R;
            SearchAutomobile.IsEnabled = user.Right.R;
            TextSearchCheck.IsEnabled = user.Right.R;
            SearchCheck.IsEnabled = user.Right.R;
            CheckSearchSelect.IsEnabled = user.Right.R;
            TextSearchCargo.IsEnabled = user.Right.R;
            SearchCargo.IsEnabled = user.Right.R;
            CheckSearchCargo.IsEnabled = user.Right.R;

            Import.IsEnabled = user.Right.W;
            Export.IsEnabled = user.Right.W;

            AddOrder.IsEnabled = user.Right.W;
            AddPhysClient.IsEnabled = user.Right.W;
            AddLegalClient.IsEnabled = user.Right.W;
            AddFlight.IsEnabled = user.Right.W;
            AddCrew.IsEnabled = user.Right.W;
            AddDriver.IsEnabled = user.Right.W;
            AddAutomobile.IsEnabled = user.Right.W;
            AddBrand.IsEnabled = user.Right.W;
            AddCheck.IsEnabled = user.Right.W;
            AddBank.IsEnabled = user.Right.W;
            AddUnit.IsEnabled = user.Right.W;
            AddCargo.IsEnabled = user.Right.W;

            RemoveOrder.IsEnabled = user.Right.D;
            RemovePhysClient.IsEnabled = user.Right.D;
            RemoveLegalClient.IsEnabled = user.Right.D;
            RemoveFlight.IsEnabled = user.Right.D;
            RemoveCrew.IsEnabled = user.Right.D;
            RemoveDriver.IsEnabled = user.Right.D;
            RemoveAutomobile.IsEnabled = user.Right.D;
            RemoveBrand.IsEnabled = user.Right.D;
            RemoveCheck.IsEnabled = user.Right.D;
            RemoveBank.IsEnabled = user.Right.D;
            RemoveUnit.IsEnabled = user.Right.D;
            RemoveCargo.IsEnabled = user.Right.D;

            EditOrder.IsEnabled = user.Right.E;
            EditPhysClient.IsEnabled = user.Right.E;
            EditLegalClient.IsEnabled = user.Right.E;
            EditFlight.IsEnabled = user.Right.E;
            EditCrew.IsEnabled = user.Right.E;
            EditDriver.IsEnabled = user.Right.E;
            EditAutomobile.IsEnabled = user.Right.E;
            EditBrand.IsEnabled = user.Right.E;
            EditCheck.IsEnabled = user.Right.E;
            EditBank.IsEnabled = user.Right.E;
            EditUnit.IsEnabled = user.Right.E;
            EditCargo.IsEnabled = user.Right.E;

        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            if (md5.hashPassword(OldPassword.Password) == CurrentUser.Password && NewPassword.Password.Length > 3 && NewPassword.Password == PassRepeat.Password && NewPassword.Password != OldPassword.Password)
            {
                CurrentUser.Password = md5.hashPassword(NewPassword.Password);
                //CurrentUser.Password
                _UserRep.Update(CurrentUser);
                MessageBox.Show("Пароль успешно изменён");
                OldPassword.Clear();
                NewPassword.Clear();
                PassRepeat.Clear();
            }
            else
            {
                MessageBox.Show("Не получилось изменить пароль", "Ошибка");
            }
        }

    }

}
