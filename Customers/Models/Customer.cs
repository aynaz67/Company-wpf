
using Customers.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Customers.Models
{
    public class Customer : INotifyPropertyChanged,INotifyDataErrorInfo
    {
        public Customer(){
            PropertyChangedWithValidation += (propName, value) => ValidateProperty(propName, value);
        }
        public int ID { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    PropertyChangedWithValidation?.Invoke(nameof(Name), value);
                }
            }
        }
        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                    PropertyChangedWithValidation?.Invoke(nameof(Age), value);
                }
            }
        }
        private string _postCode;
        public string PostCode
        {
            get { return _postCode; }
            set
            {
                if (_postCode != value)
                {
                    _postCode = value;
                    OnPropertyChanged(nameof(PostCode));
                    PropertyChangedWithValidation?.Invoke(nameof(PostCode), value);
                }
            }
        }
        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
                    PropertyChangedWithValidation?.Invoke(nameof(Height), value);
                }
            }
        }
        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged(nameof(Width));
                   
                }
            }
        }
        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                if (_length != value)
                {
                    _length = value;
                    OnPropertyChanged(nameof(Length));
                    PropertyChangedWithValidation?.Invoke(nameof(Length), value);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
      
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action<string, object>? PropertyChangedWithValidation;

        // INotifyDataErrorInfo
        private readonly Dictionary<string, List<string>> _errors = new();
        public bool HasErrors => _errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && _errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }
        public void ValidateProperty(string propertyName, object value)
        {
            var errors = CustomerValidation.Validate(propertyName, value);

            if (errors.Any())
                _errors[propertyName] = errors;
            else
                _errors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
