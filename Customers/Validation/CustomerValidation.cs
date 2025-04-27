using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public static class CustomerValidation
    {
        public static List<string> Validate(string propertyName, object value)
        {
            List<string> errors = new();

            switch (propertyName)
            {
                case "Name":
                    var name = value as string;
                    if (string.IsNullOrWhiteSpace(name))
                        errors.Add("Name is required.");
                    else if (name.Length > 50)
                        errors.Add("Name must be 50 characters or fewer.");
                    break;

                case "Age":
                    if (int.TryParse(value?.ToString(), out int age))
                    {
                        if (age < 0 || age > 110)
                            errors.Add("Age must be between 0 and 110.");
                    }
                    else
                    {
                        errors.Add("Age must be a valid number.");
                    }
                    break;

                case "PostCode":
                    var postCode = value as string;
                    if (string.IsNullOrWhiteSpace(postCode))
                        errors.Add("Post code is required.");
                    else if (!Regex.IsMatch(postCode, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
                        errors.Add("Post code must contain both letters and numbers.");
                    break;

                case "Height":
                    if (value is double height)
                    {
                        if (height < 0 || height > 2.5)
                            errors.Add("Height must be between 0 and 2.50 meters.");
                        else if (Math.Round(height, 2) != height)
                            errors.Add("Height must have only two decimal places.");
                    }
                    break;
            }

            return errors;
        }

    }
}
