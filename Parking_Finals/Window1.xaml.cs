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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string _username;
        private string _staffID;
        private mallparkingDataContext _lsDC;

        public Window1(string username, string staffID, mallparkingDataContext lsDC)
        {
            InitializeComponent();
            _username = username;
            _staffID = staffID;
            _lsDC = lsDC;
        }

        private void EntranceBoothButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(_username, _staffID);
            window2.Show();
            this.Close();
        }
    }
}
