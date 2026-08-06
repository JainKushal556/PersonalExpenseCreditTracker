using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Common
{
    // Helper class to display validation errors using ErrorProvider
    public class ErrorHelper
    {
        // Displays validation errors for ComboBox controls
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, ComboBox comboBox)
        {
            errorProvider.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(comboBox, -30); 
            //errorProvider.Clear();

            switch (result)
            {
                // Person is not selected
                case CommonValidator.ValidationResult.PersonInvalid:
                    
                    errorProvider.SetError(comboBox, "Please select a person.");
                    comboBox.Focus();
                    break;

                // Payment type is not selected
                case CommonValidator.ValidationResult.PaymentInvalid:

                    errorProvider.SetError(comboBox, "Please select payment type.");
                    comboBox.Focus();
                    break;
                // Status is not selected
                case CommonValidator.ValidationResult.StatusInvalid:
                    errorProvider.SetError(comboBox, "Please select status.");
                    comboBox.Focus();
                    break;
                case CommonValidator.ValidationResult.CategoryInvalid:
                    errorProvider.SetError(comboBox, "Please select a category.");
                    break;

                case CommonValidator.ValidationResult.SubCategoryInvalid:
                    errorProvider.SetError(comboBox, "Please select a sub category.");
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    errorProvider.SetError(comboBox, "Please select a priority.");
                    comboBox.Focus();
                    break;

            }
        }

        // Displays validation errors for TextBox controls
        public static void ShowValidationError(CommonValidator.ValidationResult result,  ErrorProvider errorProvider,TextBox textBox)
        {
            errorProvider.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(textBox, -30); 
            switch (result)
            {
                // Amount field is empty
                case CommonValidator.ValidationResult.AmountEmpty:
                    errorProvider.SetError(textBox, "Amount is required.");
                    textBox.Focus();
                    break;

                // Amount is not a valid number
                case CommonValidator.ValidationResult.AmountInvalid:
                    errorProvider.SetError(textBox, "Enter valid amount.");
                    textBox.Focus();
                    break;
                // Amount exceeds the allowed limit
                case CommonValidator.ValidationResult.AmountTooLarge:
                    errorProvider.SetError(textBox, "Amount is too large.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.MinimumAmountInvalid:
                    errorProvider.SetError(textBox, "Minimum amount invalid.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.MaximumAmountInvalid:
                    errorProvider.SetError(textBox, "Maximum amount invalid.");
                    textBox.Focus();
                    break;
                // Description is invalid
                case CommonValidator.ValidationResult.DescriptionInvalid:
                    errorProvider.SetError(textBox, "Description is invalid.");
                    textBox.Focus();
                    break;
                // Deadline is not selected or invalid
                case CommonValidator.ValidationResult.DeadlineInvalid:
                    errorProvider.SetError(textBox, "Select valid deadline.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.TaskTitleInvalid:
                    errorProvider.SetError(textBox, "Please enter a valid task title.");
                    textBox.Focus();
                    break;

                // Phone Number Invalid
                case CommonValidator.ValidationResult.PhoneInvalid:
                    errorProvider.SetError(textBox, "Please enter a valid phone number.");
                    textBox.Focus();
                    break;

                // Person Name Invalid
                case CommonValidator.ValidationResult.PersonNameInvalid:
                    errorProvider.SetError(textBox, "Please enter a valid person name");
                    textBox.Focus();
                    break;

                // Duplicate Phone Number
                case CommonValidator.ValidationResult.PhoneNumberAlreadyExists:
                    errorProvider.SetError(textBox, "Phone number already exists");
                    textBox.Focus();
                    break;

                // Amount field is empty
                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                    errorProvider.SetError(textBox, "Phone number is required.");
                    textBox.Focus();
                    break;

                // Amount field is empty
                case CommonValidator.ValidationResult.PersonNameEmpty:
                    errorProvider.SetError(textBox, "Person Name is required.");
                    textBox.Focus();
                    break;

                // Phone Number Invalid
                case CommonValidator.ValidationResult.PersonInvalid:
                    errorProvider.SetError(textBox, "Please enter a valid person.");
                    textBox.Focus();
                    break;
            }
        }
        // Displays validation errors for MonthCalendar controls
        public static void ShowValidationError(CommonValidator.ValidationResult result,  ErrorProvider errorProvider, MonthCalendar monthCalendar)
        {
            //errorProvider.Clear();
            errorProvider.SetIconAlignment(monthCalendar, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(monthCalendar, 10); 
            switch (result)
            {
                case CommonValidator.ValidationResult.DeadlineInvalid:
                    errorProvider.SetError(monthCalendar, "Select valid deadline.");
                    monthCalendar.Focus();
                    break;
                
            }
        }

         // Displays validation errors for Date
        public static void ShowValidationError(CommonValidator.ValidationResult result,ErrorProvider errorProvider,DateTimePicker dtp1, DateTimePicker dtp2)
        {
            errorProvider.SetIconAlignment(dtp1, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(dtp1, 10);

            errorProvider.SetIconAlignment(dtp2, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(dtp2, 10);

            switch (result)
            {
                case CommonValidator.ValidationResult.DateRangeInvalid:

                    errorProvider.SetError(dtp1, "From Date cannot be greater than To Date.");
                    errorProvider.SetError(dtp2, "To Date must be greater than or equal to From Date.");

                    dtp1.Focus();
                    dtp2.Focus();
                    break;
            }
        }

        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider,TextBox textBox1, TextBox textBox2)
        {
            errorProvider.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(textBox1, 10);

            errorProvider.SetIconAlignment(textBox2, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(textBox2, 10);

            switch (result)
            {
                case CommonValidator.ValidationResult.AmountRangeInvalid:

                    errorProvider.SetError(textBox2, "Invalid Maximum amount");
                    textBox1.Focus();

                    break;
            }
        }

    }
}
