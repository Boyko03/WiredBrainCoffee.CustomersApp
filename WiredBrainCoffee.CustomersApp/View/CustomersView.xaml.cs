﻿using System.Windows;
using System.Windows.Controls;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.View
{
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        private readonly CustomersViewModel _viewModel;

        public CustomersView()
        {
            InitializeComponent();

            _viewModel = new CustomersViewModel(new CustomerDataProvider());
            DataContext = _viewModel;
            Loaded += CustomersView_Loaded;
        }

        private async void CustomersView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void BtnMoveNavigation_OnClick(object sender, RoutedEventArgs e)
        {
            var column = Grid.GetColumn(customerListGrid);

            var newColumn = column == 0 ? 2 : 0;
            Grid.SetColumn(customerListGrid, newColumn);
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Add();
        }
    }
}