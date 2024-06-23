using Parking_Finals;
using System;
using System.Collections.Generic;
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

namespace Parking_Finals
{
    public partial class Window1 : Window
    {
        private string _username;
        private string _staffID;
        private mallparkingDataContext _lsDC;
        private string _staffRole;

        public Window1(string username, string staffID, mallparkingDataContext lsDC)
        {
            InitializeComponent();
            _username = username;
            _staffID = staffID;
            _lsDC = lsDC;

            _staffRole = GetStaffRole(_staffID);

            // Set the visibility of the Admin button based on the role
            AdminButton.Visibility = _staffRole == "Admin" ? Visibility.Visible : Visibility.Collapsed;
        }

        private string GetStaffRole(string staffID)
        {
            // Fetch the role of the staff based on staffID from the database
            var staff = _lsDC.Staffs.SingleOrDefault(s => s.Staff_ID == staffID);
            return staff?.Staff_Role;
        }

        private void EntranceBoothButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(_username, _staffID);
            window2.Show();
            this.Close();
        }

        private void ExitBoothButton_Click(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3(_username, _staffID);
            window3.Show();
            this.Close();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4(_username, _staffID, _lsDC);
            window4.Show();
            this.Close();
        }
    }
}
