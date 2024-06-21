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
        private mallparkingDataContext _lsDC; // Assuming this is your data context

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
                // Perform search by Plate_Number
                SearchByPlateNumber(plateNumber);
            }
            else if (!string.IsNullOrWhiteSpace(receiptId))
            {
                // Perform search by Receipt_ID
                SearchByReceiptID(receiptId);
            }
            else
            {
                MessageBox.Show("Please enter Plate Number or Receipt ID.");
            }
        }

        private void SearchByPlateNumber(string plateNumber)
        {
            // Find the Plate_Number record
            var plate = _lsDC.Plate_Numbers.FirstOrDefault(p => p.Plate_Number1 == plateNumber);
            if (plate != null)
            {
                // Retrieve the associated Customer
                var customer = _lsDC.Customers.FirstOrDefault(c => c.Plate_Number == plateNumber);
                if (customer != null)
                {
                    // Display customer details
                    DisplayCustomerDetails(customer, plateNumber);

                    // Find the Receipt associated with the customer
                    var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == customer.Receipt_ID);
                    if (receipt != null)
                    {
                        // Display receipt details
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
            // Find the Customer based on Receipt_ID
            var customer = _lsDC.Customers.FirstOrDefault(c => c.Receipt_ID == receiptId);
            if (customer != null)
            {
                // Find the Receipt based on Receipt_ID
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

            // Display receipt-specific fields
            TimeInTextBox.Text = receipt.Time_IN.ToString(); // Assuming Time_IN is a DateTime field
            ParkingAreaIDTextBox.Text = receipt.ParkingArea_ID.ToString(); // Assuming ParkingArea_ID is an integer
            ParkingStatusTextBox.Text = receipt.Parking_Status;

            // Set the current date and time in TimeOutTextBox
            TimeOutTextBox.Text = DateTime.Now.ToString();

            // Display car photo
            DisplayCarPhoto(customer.Plate_Number);
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
            TimeOutTextBox.Text = string.Empty; // Clear the TimeOutTextBox
            ParkingAreaIDTextBox.Text = string.Empty;
            ParkingStatusTextBox.Text = string.Empty;
            CarPhotoImage.Source = null; // Clear the image display
        }

        private void DisplayCarPhoto(string plateNumber)
        {
            // Find the Plate_Number record
            var plate = _lsDC.Plate_Numbers.FirstOrDefault(p => p.Plate_Number1 == plateNumber);
            if (plate != null)
            {
                // Display Car_Photo associated with the Plate_Number
                try
                {
                    // Check if the file exists using System.IO.File.Exists
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