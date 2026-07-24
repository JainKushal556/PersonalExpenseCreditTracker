using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Common
{
    public class ErrorHelper
    {
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, ComboBox comboBox)
        {
            errorProvider.Clear();

            switch (result)
            {
                case CommonValidator.ValidationResult.PersonInvalid:
                    errorProvider.SetError(comboBox, "Please select a person.");
                    comboBox.Focus();
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    errorProvider.SetError(comboBox, "Please select payment type.");
                    comboBox.Focus();
                    break;

                case CommonValidator.ValidationResult.StatusInvalid:
                    errorProvider.SetError(comboBox, "Please select status.");
                    comboBox.Focus();
                    break;
               
            }
        }

        public static void ShowValidationError(CommonValidator.ValidationResult result,  ErrorProvider errorProvider,TextBox textBox)
        {
            errorProvider.Clear();

            switch (result)
            {
                case CommonValidator.ValidationResult.AmountEmpty:
                    errorProvider.SetError(textBox, "Amount is required.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.AmountInvalid:
                    errorProvider.SetError(textBox, "Enter valid amount.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.AmountTooLarge:
                    errorProvider.SetError(textBox, "Amount is too large.");
                    textBox.Focus();
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    errorProvider.SetError(textBox, "Description is invalid.");
                    textBox.Focus();
                    break;
            }
        }

        public static void ShowValidationError(CommonValidator.ValidationResult result,  ErrorProvider errorProvider, MonthCalendar monthCalendar)
        {
            errorProvider.Clear();

            switch (result)
            {
                case CommonValidator.ValidationResult.DeadlineInvalid:
                    errorProvider.SetError(monthCalendar, "Select valid deadline.");
                    monthCalendar.Focus();
                    break;
                
            }
        }
    }
}
