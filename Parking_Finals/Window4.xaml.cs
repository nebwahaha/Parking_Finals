using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Media.Imaging;

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

        private Button _currentlyViewedButton;


        private void TableSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                // Hide photo displays when changing tables
                ORCRDisplay.Visibility = Visibility.Collapsed;
                PhotoDisplay.Visibility = Visibility.Collapsed;

                string selectedTable = selectedItem.Content.ToString();
                Console.WriteLine($"Selected table: {selectedTable}");
                DataGridView.Columns.Clear();
                DataGridView.AutoGenerateColumns = false;
                DataGridView.Visibility = Visibility.Visible;

                NewEmployeeForm.Visibility = Visibility.Collapsed;

                switch (selectedTable)
                {
                    case "Customer Table":
                        SetDataGridColumns(typeof(Customer));
                        var customerViewColumn = new DataGridTemplateColumn { Header = "Action" };
                        var customerTemplate = new DataTemplate();
                        var customerButtonFactory = new FrameworkElementFactory(typeof(Button));
                        customerButtonFactory.SetValue(Button.ContentProperty, "View");
                        customerButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ViewORCRButton_Click));
                        customerButtonFactory.SetBinding(Button.TagProperty, new Binding("OR_CR"));
                        customerTemplate.VisualTree = customerButtonFactory;
                        customerViewColumn.CellTemplate = customerTemplate;

                        DataGridView.Columns.Add(customerViewColumn);
                        DataGridView.ItemsSource = _lsDC.Customers.ToList();
                        Console.WriteLine($"Customer count: {_lsDC.Customers.Count()}");
                        break;
                    case "ParkingArea Table":
                        SetDataGridColumns(typeof(ParkingArea));
                        DataGridView.ItemsSource = _lsDC.ParkingAreas.ToList();
                        Console.WriteLine($"ParkingArea count: {_lsDC.ParkingAreas.Count()}");
                        break;
                    case "Plate_Number Table":
                        SetDataGridColumns(typeof(Plate_Number));
                        var plateNumberViewColumn = new DataGridTemplateColumn { Header = "Action" };
                        var plateNumberTemplate = new DataTemplate();
                        var plateNumberButtonFactory = new FrameworkElementFactory(typeof(Button));
                        plateNumberButtonFactory.SetValue(Button.ContentProperty, "View");
                        plateNumberButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ViewPhotoButton_Click));
                        plateNumberButtonFactory.SetBinding(Button.TagProperty, new Binding("Car_Photo"));
                        plateNumberTemplate.VisualTree = plateNumberButtonFactory;
                        plateNumberViewColumn.CellTemplate = plateNumberTemplate;

                        DataGridView.Columns.Add(plateNumberViewColumn);
                        DataGridView.ItemsSource = _lsDC.Plate_Numbers.ToList();
                        Console.WriteLine($"Plate_Number count: {_lsDC.Plate_Numbers.Count()}");
                        break;
                    case "Receipt Table":
                        SetDataGridColumns(typeof(Receipt));
                        DataGridView.ItemsSource = _lsDC.Receipts.ToList();
                        Console.WriteLine($"Receipt count: {_lsDC.Receipts.Count()}");
                        break;
                    case "Staff Table":
                        SetDataGridColumns(typeof(Staff));
                        var staffActionColumn = new DataGridTemplateColumn { Header = "Action" };
                        var staffTemplate = new DataTemplate();
                        var staffButtonFactory = new FrameworkElementFactory(typeof(Button));
                        staffButtonFactory.SetValue(Button.ContentProperty, "Toggle Status");
                        staffButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ToggleStatusButton_Click));
                        staffButtonFactory.SetBinding(Button.TagProperty, new Binding("Staff_ID"));
                        staffTemplate.VisualTree = staffButtonFactory;
                        staffActionColumn.CellTemplate = staffTemplate;

                        DataGridView.Columns.Add(staffActionColumn);
                        DataGridView.ItemsSource = _lsDC.Staffs.ToList();
                        Console.WriteLine($"Staff count: {_lsDC.Staffs.Count()}");
                        break;
                    default:
                        DataGridView.ItemsSource = null;
                        break;
                }
            }
        }


        private void ViewORCRButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button viewButton)
            {
                // Close the currently viewed photo and reset its button
                if (_currentlyViewedButton != null && _currentlyViewedButton != viewButton)
                {
                    ORCRDisplay.Visibility = Visibility.Collapsed;
                    _currentlyViewedButton.Content = "View";
                }

                string photoUri = viewButton.Tag?.ToString();

                if (viewButton.Content.ToString() == "View")
                {
                    if (!string.IsNullOrWhiteSpace(photoUri))
                    {
                        try
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(photoUri, UriKind.RelativeOrAbsolute));
                            ORCRDisplay.Source = bitmap;
                            ORCRDisplay.Visibility = Visibility.Visible;
                            viewButton.Content = "Close";
                            _currentlyViewedButton = viewButton;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to display the photo. Please check the photo path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no photo for this entry.", "No Photo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (viewButton.Content.ToString() == "Close")
                {
                    ORCRDisplay.Visibility = Visibility.Collapsed;
                    viewButton.Content = "View";
                    _currentlyViewedButton = null;
                }
            }
        }


        private void SetDataGridColumns(Type entityType)
        {
            foreach (PropertyInfo property in entityType.GetProperties())
            {
                // Exclude certain properties for the Customer table
                if (entityType == typeof(Customer) && (property.Name == "Plate_Number1" || property.Name == "Receipt"))
                {
                    continue;
                }
                if (entityType == typeof(ParkingArea) && (property.Name == "Receipts"))
                {
                    continue;
                }
                if (entityType == typeof(Plate_Number) && (property.Name == "Customers"))
                {
                    continue;
                }
                if (entityType == typeof(Receipt) && (property.Name == "Customers" || property.Name == "ParkingArea" || property.Name == "Staff"))
                {
                    continue;
                }
                if (entityType == typeof(Staff) && (property.Name == "Receipts"))
                {  
                    continue; 
                }

                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = property.Name,
                    Binding = new Binding(property.Name)
                };
                DataGridView.Columns.Add(column);
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
                    DataGridView.ItemsSource = _lsDC.Staffs.ToList();
                }
            }
        }

        private void ViewPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button viewButton)
            {
                // Close the currently viewed photo and reset its button
                if (_currentlyViewedButton != null && _currentlyViewedButton != viewButton)
                {
                    PhotoDisplay.Visibility = Visibility.Collapsed;
                    _currentlyViewedButton.Content = "View";
                }

                string photoUri = viewButton.Tag?.ToString();

                if (viewButton.Content.ToString() == "View")
                {
                    if (!string.IsNullOrWhiteSpace(photoUri))
                    {
                        try
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(photoUri, UriKind.RelativeOrAbsolute));
                            PhotoDisplay.Source = bitmap;
                            PhotoDisplay.Visibility = Visibility.Visible;
                            viewButton.Content = "Close";
                            _currentlyViewedButton = viewButton;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to display the photo. Please check the photo path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no photo for this entry.", "No Photo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (viewButton.Content.ToString() == "Close")
                {
                    PhotoDisplay.Visibility = Visibility.Collapsed;
                    viewButton.Content = "View";
                    _currentlyViewedButton = null;
                }
            }
        }



        private void Logoutlog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }
}
