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
    public partial class Window4 : Window
    {
        private string _username;
        private string _staffID;
        private mallparkingDataContext _lsDC;

        public Window4(string username, string staffID, mallparkingDataContext lsDC)
        {
            InitializeComponent();
            _username = username;
            _staffID = staffID;
            _lsDC = lsDC;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void TableSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedTable = selectedItem.Content.ToString();
                Console.WriteLine($"Selected table: {selectedTable}");
                DataGridView.Columns.Clear();
                DataGridView.AutoGenerateColumns = true;
                DataGridView.Visibility = Visibility.Visible;

                NewEmployeeForm.Visibility = Visibility.Collapsed;

                switch (selectedTable)
                {
                    case "Customer Table":
                        DataGridView.ItemsSource = _lsDC.Customers.ToList();
                        Console.WriteLine($"Customer count: {_lsDC.Customers.Count()}");
                        break;
                    case "ParkingArea Table":
                        DataGridView.ItemsSource = _lsDC.ParkingAreas.ToList();
                        Console.WriteLine($"ParkingArea count: {_lsDC.ParkingAreas.Count()}");
                        break;
                    case "Plate_Number Table":
                        DataGridView.ItemsSource = _lsDC.Plate_Numbers.ToList();
                        Console.WriteLine($"Plate_Number count: {_lsDC.Plate_Numbers.Count()}");
                        break;
                    case "Receipt Table":
                        DataGridView.ItemsSource = _lsDC.Receipts.ToList();
                        Console.WriteLine($"Receipt count: {_lsDC.Receipts.Count()}");
                        break;
                    case "Staff Table":
                        DataGridView.AutoGenerateColumns = false;
                        DataGridView.Columns.Add(new DataGridTextColumn { Header = "Staff ID", Binding = new System.Windows.Data.Binding("Staff_ID") });
                        DataGridView.Columns.Add(new DataGridTextColumn { Header = "Staff Name", Binding = new System.Windows.Data.Binding("Staff_Name") });
                        DataGridView.Columns.Add(new DataGridTextColumn { Header = "Staff Username", Binding = new System.Windows.Data.Binding("Staff_Username") });
                        DataGridView.Columns.Add(new DataGridTextColumn { Header = "Staff Role", Binding = new System.Windows.Data.Binding("Staff_Role") });
                        DataGridView.Columns.Add(new DataGridTextColumn { Header = "Staff Status", Binding = new System.Windows.Data.Binding("Staff_Status") });

                        var actionColumn = new DataGridTemplateColumn { Header = "Action" };
                        var template = new DataTemplate();
                        var buttonFactory = new FrameworkElementFactory(typeof(Button));
                        buttonFactory.SetValue(Button.ContentProperty, "Toggle Status");
                        buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ToggleStatusButton_Click));
                        buttonFactory.SetBinding(Button.TagProperty, new System.Windows.Data.Binding("Staff_ID"));
                        template.VisualTree = buttonFactory;
                        actionColumn.CellTemplate = template;

                        DataGridView.Columns.Add(actionColumn);

                        DataGridView.ItemsSource = _lsDC.Staffs.ToList();
                        Console.WriteLine($"Staff count: {_lsDC.Staffs.Count()}");
                        break;
                    default:
                        DataGridView.ItemsSource = null;
                        break;
                }
            }
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridView.Visibility = Visibility.Collapsed;
            NewEmployeeForm.Visibility = Visibility.Visible;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string staffName = StaffNameTextBox.Text;
            string staffUsername = StaffUsernameTextBox.Text;
            string staffPassword = StaffPasswordBox.Password;
            string staffRole = (StaffRoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(staffName) || string.IsNullOrWhiteSpace(staffUsername) || string.IsNullOrWhiteSpace(staffPassword) || string.IsNullOrWhiteSpace(staffRole))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string newStaffID = GenerateNewStaffID();

            var newStaff = new Staff
            {
                Staff_ID = newStaffID,
                Staff_Name = staffName,
                Staff_Username = staffUsername,
                Staff_Password = staffPassword,
                Staff_Role = staffRole,
                Staff_Status = "Active"
            };

            _lsDC.Staffs.InsertOnSubmit(newStaff);
            _lsDC.SubmitChanges();

            DataGridView.ItemsSource = _lsDC.Staffs.ToList();
            NewEmployeeForm.Visibility = Visibility.Collapsed;

            // Clear textboxes after saving
            ClearEmployeeForm();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmployeeForm.Visibility = Visibility.Collapsed;
            ClearEmployeeForm();
        }

        private void HomeLogsButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 homeWindow = new Window1(_username, _staffID, _lsDC);
            homeWindow.Show();
            this.Close();
        }
        private void ClearEmployeeForm()
        {
            StaffNameTextBox.Clear();
            StaffUsernameTextBox.Clear();
            StaffPasswordBox.Clear();
            StaffRoleComboBox.SelectedIndex = -1;
        }

        private string GenerateNewStaffID()
        {
            var lastStaff = _lsDC.Staffs.OrderByDescending(s => s.Staff_ID).FirstOrDefault();
            if (lastStaff == null)
            {
                return "ST001";
            }

            string lastStaffID = lastStaff.Staff_ID;
            int lastStaffNumber = int.Parse(lastStaffID.Substring(2));
            int newStaffNumber = lastStaffNumber + 1;
            return $"ST{newStaffNumber:D3}";
        }

        private void ToggleStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                string staffID = toggleButton.Tag.ToString();
                var staff = _lsDC.Staffs.FirstOrDefault(s => s.Staff_ID == staffID);

                if (staff != null)
                {
                    staff.Staff_Status = staff.Staff_Status == "Active" ? "Inactive" : "Active";
                    _lsDC.SubmitChanges();
                    DataGridView.ItemsSource = _lsDC.Staffs.ToList(); // Refresh the grid
                }
            }
        }
        private void Logoutlog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Show confirmation dialog
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Navigate back to MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close(); // Close the current Window4 instance
            }
        }
    }
}