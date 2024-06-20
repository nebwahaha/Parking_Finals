using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Threading;

namespace Parking_Finals
{
    public partial class Window2 : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private mallparkingDataContext _lsDC;
        private DispatcherTimer timer;
        private System.Drawing.Bitmap currentFrame; // Add this field to store the current frame

        public Window2(string username, string staffID)
        {
            InitializeComponent();
            InitializeCamera();
            _lsDC = new mallparkingDataContext(Properties.Settings.Default.mallparkingConnectionString);
            UsernameTextBox.Text = username;
            StaffIDTextBox.Text = staffID;
            PopulateComboBox();
            PopulateParkingTypeComboBox();

            this.Closed += Window2_Closed;

            // Initialize the timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimeTextBox.Text = DateTime.Now.ToString("G");
        }

        private void InitializeCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No video devices found.");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            currentFrame?.Dispose(); // Dispose the previous frame if exists
            currentFrame = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

            using (var bitmap = (System.Drawing.Bitmap)currentFrame.Clone())
            {
                Dispatcher.Invoke(() =>
                {
                    videoImage.Source = ConvertBitmapToBitmapImage(bitmap);
                });
            }
        }

        private BitmapImage ConvertBitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void PopulateComboBox()
        {
            var parkingAreas = from pa in _lsDC.ParkingAreas
                               select pa.ParkingArea_Location;

            ParkingAreaDrop.ItemsSource = parkingAreas.ToList();
        }

        private void PopulateParkingTypeComboBox()
        {
            List<string> parkingTypes = new List<string> { "Fix Rate", "Overnight Parking" };
            ParkingTypeDrop.ItemsSource = parkingTypes;
        }

        private void ParkingAreaDrop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParkingAreaDrop.SelectedItem != null)
            {
                string selectedLocation = ParkingAreaDrop.SelectedItem.ToString();

                var selectedParkingArea = (from pa in _lsDC.ParkingAreas
                                           where pa.ParkingArea_Location == selectedLocation
                                           select pa).FirstOrDefault();

                if (selectedParkingArea != null)
                {
                    ParkingCapacity.Text = selectedParkingArea.ParkingArea_Capacity.ToString();
                    AvailableSlot.Text = selectedParkingArea.Available_Slot.ToString();
                }
            }
        }

        private void Window2_Closed(object sender, EventArgs e)
        {
            StopCamera();
            timer.Stop();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
            timer.Stop();
            Application.Current.Shutdown();
        }

        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
                videoSource = null;
            }
            currentFrame?.Dispose();
        }

        private string GenerateCustomerID()
        {
            var lastCustomer = _lsDC.Customers
                                    .OrderByDescending(c => c.Customer_ID)
                                    .FirstOrDefault();
            int newIdNumber = 1;
            if (lastCustomer != null)
            {
                string lastId = lastCustomer.Customer_ID;
                newIdNumber = int.Parse(lastId.Substring(3)) + 1;
            }
            return "CID" + newIdNumber;
        }

        private void InsertPlateNumber(string plateNumber, string carPhotoPath)
        {
            try
            {
                var newPlateNumber = new Plate_Number
                {
                    Plate_Number1 = plateNumber,
                    Car_Photo = carPhotoPath
                };
                _lsDC.Plate_Numbers.InsertOnSubmit(newPlateNumber);
                _lsDC.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting plate number: {ex.Message}");
            }
        }

        private void InsertCustomer(string customerId, string plateNumber, string customerName, string contactNumber)
        {
            try
            {
                var newCustomer = new Customer
                {
                    Customer_ID = customerId,
                    Plate_Number = plateNumber,
                    Customer_Name = string.IsNullOrEmpty(customerName) ? null : customerName,
                    Contact_Number = string.IsNullOrEmpty(contactNumber) ? null : contactNumber
                };
                _lsDC.Customers.InsertOnSubmit(newCustomer);
                _lsDC.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting customer: {ex.Message}");
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string plateNumber = PlateNumberTextBox.Text;

            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                MessageBox.Show("Plate number is required.");
                return;
            }

            string customerId = GenerateCustomerID();
            string customerName = CustomerNameTextBox.Text;
            string contactNumber = ContactNumberTextBox.Text;

            string carPhotoPath = SaveCurrentFrame(plateNumber); // Save the current frame and get the file path

            if (carPhotoPath != null)
            {
                InsertPlateNumber(plateNumber, carPhotoPath);
                InsertCustomer(customerId, plateNumber, customerName, contactNumber);

                MessageBox.Show("Data saved successfully!");
            }
        }

        private string SaveCurrentFrame(string plateNumber)
        {
            try
            {
                if (currentFrame == null)
                {
                    MessageBox.Show("No image captured from the camera.");
                    return null;
                }

                // Specify the directory where you want to save the image
                string directoryPath = "C:\\Users\\Benjamin\\Documents\\CarPhoto"; // Edit this path as needed
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }

                string fileName = GetUniqueFileName(directoryPath, plateNumber);
                string filePath = System.IO.Path.Combine(directoryPath, fileName);

                currentFrame.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show($"Image saved to {filePath}");
                return filePath; // Return the file path
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image: {ex.Message}");
                return null;
            }
        }

        private string GetUniqueFileName(string directoryPath, string plateNumber)
        {
            string fileName = $"{plateNumber}.png";
            string filePath = System.IO.Path.Combine(directoryPath, fileName);
            int counter = 1;

            while (System.IO.File.Exists(filePath))
            {
                fileName = $"{plateNumber}_{counter}.png";
                filePath = System.IO.Path.Combine(directoryPath, fileName);
                counter++;
            }

            return fileName;
        }
    }
}
