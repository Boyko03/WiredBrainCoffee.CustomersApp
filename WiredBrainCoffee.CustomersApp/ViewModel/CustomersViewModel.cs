using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Command;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomerDataProvider _customerDataProvider;
        private CustomerItemViewModel? _selectedCustomer;
        private ENavigationSide _navigationSide;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand MoveNavigationCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public ObservableCollection<CustomerItemViewModel> Customers { get; } = [];

        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsCustomerSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsCustomerSelected => SelectedCustomer is not null;

        public ENavigationSide NavigationSide
        {
            get => _navigationSide;
            set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customerDataProvider.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        private void Add(object? parameter)
        {
            var customer = new Customer() {FirstName = "New"};
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        private void MoveNavigation(object? parameter)
        {
            NavigationSide = NavigationSide == ENavigationSide.Left
                ? ENavigationSide.Right
                : ENavigationSide.Left;
        }

        private void Delete(object? parameter)
        {
            if (SelectedCustomer is null) return;

            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
    }
    public enum ENavigationSide
    {
        Left,
        Right
    }
}
