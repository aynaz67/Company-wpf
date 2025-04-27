using Customers.Models;
using Customers.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Customers
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Window
    {
        private readonly CustomerFormViewModel _viewModel;

        public Customer Customer => _viewModel.Customer;

        public CustomersPage(Customer? customer = null)
        {
            InitializeComponent();

            _viewModel = new CustomerFormViewModel(customer);
            _viewModel.RequestClose += () => this.Close();
            _viewModel.RequestSave += () => this.DialogResult = true;

            DataContext = _viewModel;
        }
    }
}
