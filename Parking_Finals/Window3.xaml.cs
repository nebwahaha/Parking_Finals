using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Parking_Finals
{
    public partial class Window3 : Window
    {
        private string _username;
        private string _staffID;
        private mallparkingDataContext _lsDC;

        public Window3(string username, string staffID)
        {
            InitializeComponent();
            _username = username;
            _staffID = staffID;
            _lsDC = new mallparkingDataContext(Properties.Settings.Default.mallparkingConnectionString);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string plateNumber = PlateNumberTextBox.Text.Trim();
            string receiptId = ReceiptIDTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(plateNumber))
            {
                SearchByPlateNumber(plateNumber);
            }
            else if (!string.IsNullOrWhiteSpace(receiptId))
            {
                SearchByReceiptID(receiptId);
            }
            else
            {
                MessageBox.Show("Please enter Plate Number or Receipt ID.");
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void SearchByPlateNumber(string plateNumber)
        {
            var plate = _lsDC.Plate_Numbers.FirstOrDefault(p => p.Plate_Number1 == plateNumber);
            if (plate != null)
            {
                var customer = _lsDC.Customers.FirstOrDefault(c => c.Plate_Number == plateNumber);
                if (customer != null)
                {
                    DisplayCustomerDetails(customer, plateNumber);

                    var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == customer.Receipt_ID);
                    if (receipt != null)
                    {
                        DisplayReceiptDetails(customer, receipt);
                    }
                    else
                    {
                        MessageBox.Show($"Receipt for Customer with Plate Number '{plateNumber}' not found.");
                        ClearReceiptDetails();
                    }
                }
                else
                {
                    MessageBox.Show($"Customer with Plate Number '{plateNumber}' not found.");
                    ClearCustomerDetails();
                }
            }
            else
            {
                MessageBox.Show($"Plate Number '{plateNumber}' not found.");
                ClearCustomerDetails();
            }
        }

        private void SearchByReceiptID(string receiptId)
        {
            var customer = _lsDC.Customers.FirstOrDefault(c => c.Receipt_ID == receiptId);
            if (customer != null)
            {
                var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == receiptId);
                if (receipt != null)
                {
                    DisplayReceiptDetails(customer, receipt);
                }
                else
                {
                    MessageBox.Show($"Receipt with ID '{receiptId}' not found.");
                    ClearCustomerDetails();
                    ClearReceiptDetails();
                }
            }
            else
            {
                MessageBox.Show($"Customer with Receipt ID '{receiptId}' not found.");
                ClearCustomerDetails();
                ClearReceiptDetails();
            }
        }

        private void DisplayCustomerDetails(Customer customer, string plateNumber)
        {
            PlateNumberTextBox.Text = customer.Plate_Number;
            ReceiptIDTextBox.Text = customer.Receipt_ID;
            CustomerNameTextBox.Text = customer.Customer_Name;
            ContactNumberTextBox.Text = customer.Contact_Number;
        }

        private void DisplayReceiptDetails(Customer customer, Receipt receipt)
        {
            PlateNumberTextBox.Text = customer.Plate_Number;
            ReceiptIDTextBox.Text = customer.Receipt_ID;
            CustomerNameTextBox.Text = customer.Customer_Name;
            ContactNumberTextBox.Text = customer.Contact_Number;

            if (receipt.Time_IN.HasValue)
            {
                DateTime timeIn = receipt.Time_IN.Value;
                DateTime timeOut = DateTime.Now;
                TimeInTextBox.Text = timeIn.ToString();
                TimeOutTextBox.Text = timeOut.ToString();

                // Calculate parking duration
                TimeSpan duration = timeOut - timeIn;

                // Determine the total amount and update parking status
                decimal totalAmount;
                if (duration.TotalHours <= 12)
                {
                    totalAmount = 30m; // 30 PHP for up to 12 hours
                }
                else
                {
                    totalAmount = 300m; // 300 PHP for overnight parking
                    ParkingStatusTextBox.Text = "Overnight"; // Update parking status in the UI
                    receipt.Parking_Status = "Overnight"; // Update parking status in the receipt object
                    _lsDC.SubmitChanges(); // Save changes to the database
                }

                TotalAmount.Text = totalAmount.ToString("F2"); // Display total amount

                // Display ParkingArea_ID
                ParkingAreaIDTextBox.Text = receipt.ParkingArea_ID.ToString();

                DisplayCarPhoto(customer.Plate_Number);
            }
            else
            {
                MessageBox.Show("Time IN is not available.");
            }
        }

        private void PaymentInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(TotalAmount.Text, out decimal totalAmount) &&
                decimal.TryParse(PaymentInputTextBox.Text, out decimal paymentAmount))
            {
                decimal change = paymentAmount - totalAmount;
                ChangeTextBox.Text = change.ToString("F2");
            }
            else
            {
                ChangeTextBox.Text = string.Empty;
            }
        }

        private void ClearCustomerDetails()
        {
            CustomerNameTextBox.Text = string.Empty;
            ContactNumberTextBox.Text = string.Empty;
            ClearReceiptDetails();
        }

        private void ClearReceiptDetails()
        {
            TimeInTextBox.Text = string.Empty;
            TimeOutTextBox.Text = string.Empty;
            ParkingAreaIDTextBox.Text = string.Empty;
            ParkingStatusTextBox.Text = string.Empty;
            TotalAmount.Text = string.Empty;
            PaymentInputTextBox.Text = string.Empty; // Clear payment input
            ChangeTextBox.Text = string.Empty; // Clear change display
            CarPhotoImage.Source = null;
        }

        private void DisplayCarPhoto(string plateNumber)
        {
            var plate = _lsDC.Plate_Numbers.FirstOrDefault(p => p.Plate_Number1 == plateNumber);
            if (plate != null)
            {
                try
                {
                    if (System.IO.File.Exists(plate.Car_Photo))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(plate.Car_Photo);
                        bitmap.EndInit();
                        CarPhotoImage.Source = bitmap;
                    }
                    else
                    {
                        MessageBox.Show($"Car photo not found at '{plate.Car_Photo}'.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying car photo: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"Plate Number '{plateNumber}' not found.");
                ClearReceiptDetails();
            }
        }
    }
}