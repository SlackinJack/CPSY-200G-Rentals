using Microsoft.Maui.Storage;
using Rentals.Classes;
using System;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rentals.Pages {
    public partial class MainPage : ContentPage {
        private List<Customer> customerList = new List<Customer>();
        private List<Equipment> equipmentList = new List<Equipment>();
        private List<Rental> rentalList = new List<Rental>();
        private List<Category> categoryList = new List<Category>();

        public MainPage() {
            InitializeComponent();

            // init customers
            using var streamCustomers = FileSystem.OpenAppPackageFileAsync("customers.csv").Result;
            String contentCustomers = new StreamReader(streamCustomers).ReadToEnd();
            foreach (String line in contentCustomers.Split('\n')) {
                if (!String.IsNullOrWhiteSpace(line)) {
                    String[] columns = line.Split(',');
                    Int32 customerId = Int32.Parse(columns[0]);
                    String customerLastName = columns[1];
                    String customerFirstName = columns[2];
                    String customerPhone = columns[3];
                    String customerEmail = columns[4];
                    String customerNotes = "";
                    this.customerList.Add(new Customer(customerId, customerLastName, customerFirstName, customerPhone, customerEmail, customerNotes));
                }
            }

            using var streamEquipment = FileSystem.OpenAppPackageFileAsync("equipment.csv").Result;
            String contentEquipment = new StreamReader(streamEquipment).ReadToEnd();
            foreach (String line in contentEquipment.Split('\n')) {
                if (!String.IsNullOrWhiteSpace(line)) {
                    String[] columns = line.Split(',');
                    Int32 equipmentId = Int32.Parse(columns[0]);
                    Int32 equipmentCategory = Int32.Parse(columns[1]);
                    String equipmentName = columns[2];
                    String equipmentDescription = columns[3];
                    Double equipmentCost = Double.Parse(columns[4]);
                    this.equipmentList.Add(new Equipment(equipmentId, equipmentCategory, equipmentName, equipmentDescription, equipmentCost));
                }
            }

            using var streamRentals = FileSystem.OpenAppPackageFileAsync("rentals.csv").Result;
            String contentRentals = new StreamReader(streamRentals).ReadToEnd();
            foreach (String line in contentRentals.Split('\n')) {
                if (!String.IsNullOrWhiteSpace(line)) {
                    String[] columns = line.Split(',');
                    Int32 rentalId = Int32.Parse(columns[0]);
                    DateTime rentalDate = DateTime.Parse(columns[1]);
                    Int32 rentalCustomerId = Int32.Parse(columns[2]);
                    Int32 rentalEquipmentId = Int32.Parse(columns[3]);
                    DateTime rentalActualDate = DateTime.Parse(columns[4]);
                    DateTime rentalReturnDate = DateTime.Parse(columns[5]);
                    Double rentalCost = Double.Parse(columns[6]);
                    this.rentalList.Add(new Rental(rentalId, rentalDate, rentalCustomerId, rentalEquipmentId, rentalActualDate, rentalReturnDate, rentalCost));
                }
            }

            using var streamCategories = FileSystem.OpenAppPackageFileAsync("categories.csv").Result;
            String contentCategories = new StreamReader(streamCategories).ReadToEnd();
            foreach (String line in contentCategories.Split('\n')) {
                if (!String.IsNullOrWhiteSpace(line)) {
                    String[] columns = line.Split(',');
                    Int32 categoryId = Int32.Parse(columns[0]);
                    String categoryName = columns[1];
                    this.categoryList.Add(new Category(categoryId, categoryName));
                }
            }

            this.updatePickers();
        }

        private void onAddEquipmentButtonClicked(System.Object sender, System.EventArgs e) {
            if (String.IsNullOrEmpty(this.equipmentId.Text) ||
                String.IsNullOrEmpty(this.equipmentName.Text) ||
                String.IsNullOrEmpty(this.equipmentDescription.Text) ||
                String.IsNullOrEmpty(this.equipmentCost.Text) ||
                this.equipmentCategoryPicker.SelectedItem == null) {
                this.DisplayAlert("Error", "Please fill out the equipment form.", "oops");
            } else {
                Int32 equipmentId = Int32.Parse(this.equipmentId.Text);
                String equipmentName = this.equipmentName.Text;
                String equipmentDescription = this.equipmentDescription.Text;
                Int32 equipmentCategory = Int32.Parse(this.equipmentCategoryPicker.SelectedItem.ToString().Split(",")[0]);
                Double equipmentCost = Double.Parse(this.equipmentCost.Text);
                Equipment newEquipment = new Equipment(equipmentId, equipmentCategory, equipmentName, equipmentDescription, equipmentCost);
                this.equipmentList.Add(newEquipment);
                this.DisplayAlert("Success", "Added equipment.", "noice");
                this.resetForm();
            }
        }

        private void onDeleteEquipmentButtonClicked(System.Object sender, System.EventArgs e) {
            if (this.equipmentPicker.SelectedItem == null) {
                this.DisplayAlert("Error", "Please select an equipment to delete.", "oops");
            } else {
                Int32 equipmentId = Int32.Parse(this.equipmentPicker.SelectedItem.ToString().Split(",")[0]);
                List<Equipment> matchedEquipment = new List<Equipment>();
                foreach (Equipment equipment in this.equipmentList) {
                    if (equipment.getEquipmentId() == equipmentId) {
                        matchedEquipment.Add(equipment);
                    }
                }

                matchedEquipment.ForEach((e) => { this.equipmentList.Remove(e); });
                this.DisplayAlert("Success", "Deleted the selected equipment.", "noice");
                this.resetForm();
            }
        }

        private void onAddCustomerButtonClicked(System.Object sender, System.EventArgs e) {
            if (String.IsNullOrEmpty(this.customerId.Text) ||
               String.IsNullOrEmpty(this.customerLastName.Text) ||
               String.IsNullOrEmpty(this.customerFirstName.Text) ||
               String.IsNullOrEmpty(this.customerPhone.Text) ||
               String.IsNullOrEmpty(this.customerEmail.Text)) {
               this.DisplayAlert("Error", "Please fill out the customer form.", "oops");
            } else {
                Int32 customerId = Int32.Parse(this.customerId.Text);
                String customerLastName = this.customerLastName.Text;
                String customerFirstName = this.customerFirstName.Text;
                String customerPhone = this.customerPhone.Text;
                String customerEmail = this.customerEmail.Text;
                String customerNotes = this.customerNotes.Text;
                Customer newCustomer = new Customer(customerId, customerLastName, customerFirstName, customerPhone, customerEmail, customerNotes);
                this.customerList.Add(newCustomer);
                this.resetForm();
                this.DisplayAlert("Success", "Added customer.", "noice");
            }
        }

        private void onCreateRentalButtonClicked(System.Object sender, System.EventArgs e) {
            if (this.customerPicker.SelectedItem == null ||
               this.equipmentPicker.SelectedItem == null) {
               this.DisplayAlert("Error", "Please select a customer and an equipment.", "oops");
            } else {
                Int32 customerId = Int32.Parse(this.customerPicker.SelectedItem.ToString().Split(",")[0]);
                Int32 equipmentId = Int32.Parse(this.equipmentPicker.SelectedItem.ToString().Split(",")[0]);
                Int32 rentalId = new Random().Next(1000, 9999); ;
                DateTime rentalDate = DateTime.Now;
                DateTime rentalActualDate = DateTime.Parse(this.rentalActualDatePicker.Date.ToString());
                DateTime rentalReturnDate = DateTime.Parse(this.rentalReturnDatePicker.Date.ToString());
                Double rentalCostFinal = Double.Parse(this.equipmentPicker.SelectedItem.ToString().Split(",")[4]);
                Rental newRental = new Rental(rentalId, rentalDate, customerId, equipmentId, rentalActualDate, rentalReturnDate, rentalCostFinal);
                this.rentalList.Add(newRental);
                this.DisplayAlert("Success", "Added rental.", "noice");
                this.resetForm();
            }
        }


        private void onResetButtonClicked(System.Object sender, System.EventArgs e) {
            this.resetForm();
        }

        private void updatePickers() {
            this.customerPicker.Items.Clear();
            foreach (Customer c in this.customerList) {
                this.customerPicker.Items.Add(c.ToString());
            }

            this.equipmentPicker.Items.Clear();
            foreach (Equipment e in this.equipmentList) {
                this.equipmentPicker.Items.Add(e.ToString());
            }

            this.equipmentCategoryPicker.Items.Clear();
            foreach (Category c in this.categoryList) {
                this.equipmentCategoryPicker.Items.Add(c.ToString());
            }

            this.rentalPicker.Items.Clear();
            foreach (Rental r in this.rentalList) {
                this.rentalPicker.Items.Add(r.ToString());
            }
        }

        private void resetForm() {
            foreach (Element element in this.GetVisualTreeDescendants()) {
                if (element.GetType() == typeof(Entry)) {
                    ((Entry)element).Text = "";
                }

                if (element.GetType() == typeof(Picker)) {
                    ((Picker)element).SelectedIndex = 0;
                    ((Picker)element).Items.Clear();
                }
            }

            this.updatePickers();
        }
    }
}
