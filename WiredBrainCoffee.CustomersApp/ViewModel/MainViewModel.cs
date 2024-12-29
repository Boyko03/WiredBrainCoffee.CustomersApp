using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly CustomersViewModel _customersViewModel;
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(CustomersViewModel customersViewModel)
        {
            _customersViewModel = customersViewModel;
            SelectedViewModel = _customersViewModel;
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public override async Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
    }
}
