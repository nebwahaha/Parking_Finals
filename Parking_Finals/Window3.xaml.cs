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
using Microsoft.Win32;

namespace Parking_Finals
{
    public partial class Window3 : Window
    {
        private string _username;
        private string _staffID;
        private mallparkingDataContext _lsDC;
        private bool _isLostTicket = false;


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

        private void LostTicketButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpeg;*.png)|*.jpeg;*.png";

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string selectedFileName = openFileDialog.FileName;
                UpdateCustomerORCR(selectedFileName);

                // Set the lost ticket flag
                _isLostTicket = true;

                // Update the total amount with the lost ticket fee
                if (decimal.TryParse(TotalAmount.Text, out decimal totalAmount))
                {
                    totalAmount += 200m; // Add 200 pesos for lost ticket fee
                    TotalAmount.Text = totalAmount.ToString("F2");
                }
            }
        }


        private void UpdateCustomerORCR(string filePath)
        {
            // Assuming the PlateNumberTextBox contains the plate number of the current customer
            string plateNumber = PlateNumberTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(plateNumber))
            {
                var customer = _lsDC.Customers.FirstOrDefault(c => c.Plate_Number == plateNumber);
                if (customer != null)
                {
                    customer.OR_CR = filePath;
                    _lsDC.SubmitChanges();
                    MessageBox.Show("Photo path saved successfully.");
                }
                else
                {
                    MessageBox.Show("Customer not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a Plate Number.");
            }
        }

        private void PayCashButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(TotalAmount.Text, out decimal totalAmount) &&
                decimal.TryParse(PaymentInputTextBox.Text, out decimal paymentAmount))
            {
                decimal change = paymentAmount - totalAmount;
                if (change >= 0)
                {
                    var receiptId = ReceiptIDTextBox.Text.Trim();
                    var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == receiptId);
                    if (receipt != null)
                    {
                        receipt.Payment_Method = "Cash";
                        receipt.Payment_Status = "Paid";
                        receipt.Total_Amount = totalAmount;
                        receipt.Change_Amount = change;
                        receipt.Staff_ID = _staffID;
                        receipt.Time_Out = DateTime.Now;

                        var parkingArea = _lsDC.ParkingAreas.FirstOrDefault(p => p.ParkingArea_ID == receipt.ParkingArea_ID);
                        if (parkingArea != null && parkingArea.Available_Slot < 100)
                        {
                            parkingArea.Available_Slot = Math.Min(parkingArea.Available_Slot + 1, 100);
                        }

                        _lsDC.SubmitChanges();
                        MessageBox.Show("Cash payment successful.");

                        // Clear the input fields after payment
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Receipt not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient payment amount.");
                }
            }
            else
            {
                MessageBox.Show("Invalid payment amount.");
            }
        }

        private void PayGCashButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(TotalAmount.Text, out decimal totalAmount))
            {
                var receiptId = ReceiptIDTextBox.Text.Trim();
                var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == receiptId);
                if (receipt != null)
                {
                    receipt.Payment_Method = "GCash";
                    receipt.Payment_Status = "Paid";
                    receipt.Total_Amount = totalAmount;
                    receipt.Change_Amount = 0m;
                    receipt.Staff_ID = _staffID;
                    receipt.Time_Out = DateTime.Now;

                    var parkingArea = _lsDC.ParkingAreas.FirstOrDefault(p => p.ParkingArea_ID == receipt.ParkingArea_ID);
                    if (parkingArea != null && parkingArea.Available_Slot < 100)
                    {
                        parkingArea.Available_Slot = Math.Min(parkingArea.Available_Slot + 1, 100);
                    }

                    _lsDC.SubmitChanges();
                    MessageBox.Show("GCash payment successful.");

                    // Clear the input fields after payment
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Receipt not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid total amount.");
            }
        }

        private void SearchByPlateNumber(string plateNumber)
        {
            var plates = _lsDC.Plate_Numbers.Where(p => p.Plate_Number1.StartsWith(plateNumber)).ToList();

            if (plates.Count == 1)
            {
                var customer = _lsDC.Customers.FirstOrDefault(c => c.Plate_Number == plates[0].Plate_Number1);
                if (customer != null)
                {
                    var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == customer.Receipt_ID && r.Payment_Status != "Paid");
                    if (receipt != null)
                    {
                        DisplayCustomerDetails(customer, plates[0].Plate_Number1);
                        DisplayReceiptDetails(customer, receipt);
                    }
                    else
                    {
                        MessageBox.Show($"No unpaid receipt found for Customer with Plate Number '{plateNumber}'.");
                        ClearCustomerDetails();
                        ClearReceiptDetails();
                    }
                }
                else
                {
                    MessageBox.Show($"Customer with Plate Number '{plateNumber}' not found.");
                    ClearCustomerDetails();
                }
            }
            else if (plates.Count > 1)
            {
                MessageBox.Show($"Multiple entries found for Plate Number '{plateNumber}'. Please search by Receipt ID instead.");
                ClearCustomerDetails();
                PlateNumberTextBox.Clear();
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
                var receipt = _lsDC.Receipts.FirstOrDefault(r => r.Receipt_ID == receiptId && r.Payment_Status != "Paid");
                if (receipt != null)
                {
                    DisplayReceiptDetails(customer, receipt);
                }
                else
                {
                    MessageBox.Show($"No unpaid receipt found with ID '{receiptId}'.");
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
            _isLostTicket = false; // Reset the lost ticket flag

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

        private void ClearInputFields()
        {
            PlateNumberTextBox.Clear();
            ReceiptIDTextBox.Clear();
            CustomerNameTextBox.Clear();
            ContactNumberTextBox.Clear();
            TimeInTextBox.Clear();
            TimeOutTextBox.Clear();
            ParkingAreaIDTextBox.Clear();
            ParkingStatusTextBox.Clear();
            TotalAmount.Clear();
            PaymentInputTextBox.Clear();
            ChangeTextBox.Clear();
            CarPhotoImage.Source = null;
        }

    }
}