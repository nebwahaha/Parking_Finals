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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private mallparkingDataContext _lsDC = null;
        private string username = "";
        private bool loginlog = false;
        private string _staffID = "";

        public MainWindow()
        {
            InitializeComponent();
            _lsDC = new mallparkingDataContext(Properties.Settings.Default.mallparkingConnectionString);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginlog = false;

            if (txtbusername.Text.Length > 0 && txtbpass.Password.Length > 0)
            {
                var mallparking = from s in _lsDC.Staffs
                                  where s.Staff_Username == txtbusername.Text
                                  select s;
                if (mallparking.Count() == 1)
                {
                    foreach (var login in mallparking)
                    {
                        if (login.Staff_Password == txtbpass.Password)
                        {
                            loginlog = true;
                            username = login.Staff_Name;
                            _staffID = login.Staff_ID;
                        }
                    }
                }
            }
            if (loginlog)
            {
                MessageBox.Show($"Success! Welcome {username}");
                Window1 window1 = new Window1(username, _staffID, _lsDC);
                window1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username and password are incorrect");
            }
        }

        private void txtbusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtbpass.Focus();
            }
        }

        private void txtbpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
