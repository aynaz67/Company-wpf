using Customers.Command;
using Customers.Models;
using Customers.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Customers.ViewModels
{
    class CustomerFormViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
                _customer.PropertyChangedWithValidation -= ValidateProperty;
                _customer.PropertyChangedWithValidation += ValidateProperty;
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action? RequestClose;
        public event Action? RequestSave;

        public CustomerFormViewModel(Customer? customer = null)
        {
            Customer = customer ?? new Customer { ID = new Random().Next(1000, 9999) };
            ValidateAllProperties();
            SaveCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            CancelCommand = new RelayCommand(Cancel);
        }
        private void ValidateAllProperties()
        {
            ValidateProperty(nameof(Customer.Name), Customer.Name);
            ValidateProperty(nameof(Customer.Age), Customer.Age);
            ValidateProperty(nameof(Customer.PostCode), Customer.PostCode);
            ValidateProperty(nameof(Customer.Height), Customer.Height);
            ValidateProperty(nameof(Customer.Length), Customer.Length);
            ValidateProperty(nameof(Customer.Width), Customer.Width);
        }


        private void SaveCustomer()
        {
            RequestSave?.Invoke();
            RequestClose?.Invoke();
        }

        private bool CanSaveCustomer()
        {
            return !HasErrors;
        }

        private void Cancel()
        {
            RequestClose?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Validation
        private readonly Dictionary<string, List<string>> _errors = new();
        public bool HasErrors => _errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public void ValidateProperty(string propertyName, object value)
        {
            var errors = CustomerValidation.Validate(propertyName, value);

            if (errors.Any())
                _errors[propertyName] = errors;
            else
                _errors.Remove(propertyName);

            OnErrorsChanged(propertyName);

            (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }


        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}

