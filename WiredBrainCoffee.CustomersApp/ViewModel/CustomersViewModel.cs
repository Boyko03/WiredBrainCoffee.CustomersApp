using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomersViewModel(ICustomerDataProvider customerDataProvider) : ViewModelBase
    {
        private CustomerItemViewModel? _selectedCustomer;
        private int _navigationColumn;
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = [];

        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }
        public int NavigationColumn
        {
            get => _navigationColumn;
            set
            {
                _navigationColumn = value;
                RaisePropertyChanged();
            }
        }

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
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        internal void Add()
        {
            var customer = new Customer() {FirstName = "New"};
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        internal void MoveNavigation()
        {
            NavigationColumn = NavigationColumn == 0 ? 2 : 0;
        }
    }
}
