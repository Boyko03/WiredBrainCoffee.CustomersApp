using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomersViewModel(ICustomerDataProvider customerDataProvider) : INotifyPropertyChanged
    {
        private Customer? _selectedCustomer;
        public ObservableCollection<Customer> Customers { get; } = [];

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await customerDataProvider.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }
            }
        }

        public void Add()
        {
            var customer = new Customer() {FirstName = "New"};
            Customers.Add(customer);
            SelectedCustomer = customer;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
