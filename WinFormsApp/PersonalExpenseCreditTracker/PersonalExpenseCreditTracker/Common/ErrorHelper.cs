using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Common
{
    // Helper class to display validation error text directly below controls (Without ErrorProvider icon)
    public class ErrorHelper
    {
        // Displays validation errors for ComboBox controls
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, ComboBox comboBox)
        {
            string message = "";
            switch (result)
            {
                case CommonValidator.ValidationResult.PersonInvalid:
                    message = "* Please select a person.";
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    message = "* Please select a payment type.";
                    break;

                case CommonValidator.ValidationResult.StatusInvalid:
                    message = "* Please select a status.";
                    break;

                case CommonValidator.ValidationResult.CategoryInvalid:
                    message = "* Please select a category.";
                    break;

                case CommonValidator.ValidationResult.SubCategoryInvalid:
                    message = "* Please select a sub category.";
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    message = "* Please select a priority.";
                    break;

                case CommonValidator.ValidationResult.GenderInvalid:
                    message = "* Please select a gender.";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ShowErrorBelowControl(comboBox, message);
                comboBox.Focus();
            }
        }

        // Displays validation errors for TextBox controls
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, TextBox textBox)
        {
            string message = "";
            switch (result)
            {
                case CommonValidator.ValidationResult.AmountEmpty:
                    message = "* Amount is required.";
                    break;

                case CommonValidator.ValidationResult.AmountInvalid:
                    message = "* Enter a valid amount.";
                    break;

                case CommonValidator.ValidationResult.AmountTooLarge:
                    message = "* Amount is too large.";
                    break;

                case CommonValidator.ValidationResult.MinimumAmountInvalid:
                    message = "* Enter a valid minimum amount.";
                    break;

                case CommonValidator.ValidationResult.MaximumAmountInvalid:
                    message = "* Enter a valid maximum amount.";
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    message = "* Description is required.";
                    break;

                case CommonValidator.ValidationResult.DescriptionTooShort:
                    message = "* Description must contain at least 5 characters.";
                    break;

                case CommonValidator.ValidationResult.DescriptionTooLong:
                    message = "* Description cannot exceed 150 characters.";
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    message = "* Please select a valid deadline.";
                    break;

                case CommonValidator.ValidationResult.TaskTitleInvalid:
                    message = "* Please enter a valid task title.";
                    break;

                case CommonValidator.ValidationResult.FullNameInvalid:
                    message = "* Please enter a valid full name.";
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    message = "* Please enter a valid email address.";
                    break;

                case CommonValidator.ValidationResult.PhoneInvalid:
                    message = "* Please enter a valid phone number.";
                    break;

                case CommonValidator.ValidationResult.AddressInvalid:
                    message = "* Please enter a valid address.";
                    break;

                case CommonValidator.ValidationResult.DateOfBirthInvalid:
                    message = "* Please enter a valid date of birth.";
                    break;

                case CommonValidator.ValidationResult.NoteTitleInvalid:
                    message = "* Please enter a valid note title.";
                    break;

                case CommonValidator.ValidationResult.PersonNameInvalid:
                    message = "* Please enter a valid person name.";
                    break;

                case CommonValidator.ValidationResult.PersonNameEmpty:
                    message = "* Person name is required.";
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                    message = "* Phone number is required.";
                    break;

                case CommonValidator.ValidationResult.PhoneNumberAlreadyExists:
                    message = "* Phone number already exists.";
                    break;

                case CommonValidator.ValidationResult.PersonInvalid:
                    message = "* Please enter a valid person.";
                    break;

                // Password field is empty
                case CommonValidator.ValidationResult.CurrentPasswordEmpty:
                case CommonValidator.ValidationResult.NewPasswordEmpty:
                case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
                    errorProvider.SetError(textBox, "Password is required.");
                    textBox.Focus();
                    break;

                // CurrentPassword And NewPassword Same
                case CommonValidator.ValidationResult.CurrentAndNewPasswordSame:
                    errorProvider.SetError(textBox, "Your current password and new password are same.");
                    textBox.Focus();
                    break;

                // Not Match Password
                case CommonValidator.ValidationResult.NotMatchPassword:
                    errorProvider.SetError(textBox, "Password doesn't match.");
                    textBox.Focus();
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ShowErrorBelowControl(textBox, message);
                textBox.Focus();
            }
        }

        // Displays validation errors for MonthCalendar controls
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, MonthCalendar monthCalendar)
        {
            string message = "";
            switch (result)
            {
                case CommonValidator.ValidationResult.DeadlineInvalid:
                    message = "* Select valid deadline.";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ShowErrorBelowControl(monthCalendar, message);
                monthCalendar.Focus();
            }
        }

        // Displays validation errors for Date
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, DateTimePicker dtp1, DateTimePicker dtp2)
        {
            switch (result)
            {
                case CommonValidator.ValidationResult.DateRangeInvalid:
                    HideErrorForControl(dtp1);
                    ShowErrorBelowControl(dtp2,"* From Date is greater than To Date.");
                    dtp1.Focus();
                    break;
            }
        }


        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, TextBox textBox1, TextBox textBox2)
        {
            switch (result)
            {
                case CommonValidator.ValidationResult.AmountRangeInvalid:
                    string msg = "* Minimum amount should be less than or equal to maximum amount.";
                    ShowErrorBelowControl(textBox2, msg);
                    textBox2.Focus();
                    break;
            }
        }

        // Displays validation errors for RichTextBox controls
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, RichTextBox richTextBox)
        {
            string message = "";
            switch (result)
            {
                case CommonValidator.ValidationResult.DescriptionInvalid:
                    message = "* Please enter a valid description.";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ShowErrorBelowControl(richTextBox, message);
                richTextBox.Focus();
            }
        }

        // Displays validation errors for Label controls (Color, Priority)
        public static void ShowValidationError(CommonValidator.ValidationResult result, ErrorProvider errorProvider, Label label)
        {
            string message = "";
            switch (result)
            {
                case CommonValidator.ValidationResult.ColorInvalid:
                    message = "* Please select a color.";
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    message = "* Please select a priority.";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ShowErrorBelowControl(label, message);
            }
        }

        public static void ShowErrorBelowControl(Control control, string message)
        {
            if (control == null) return;

          
            Control targetControl = control;
            if (control.Parent != null && control.Parent.Height < 50 && !(control.Parent is Form))
            {
                targetControl = control.Parent;
            }

            Control parent = targetControl.Parent;
            if (parent == null) return;

            string labelName = "lblErr_" + control.Name;

            Label errLabel = parent.Controls.Find(labelName, false).FirstOrDefault() as Label;

            if (errLabel == null)
            {
                errLabel = new Label();
                errLabel.Name = labelName;
                errLabel.ForeColor = Color.Red;
                errLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
                errLabel.AutoSize = true;
                parent.Controls.Add(errLabel);
            }

       
            errLabel.Location = new Point(targetControl.Left, targetControl.Bottom + 2);
            errLabel.BringToFront();
            errLabel.Text = message;
            errLabel.Visible = true;
        }

 
        public static void HideErrorForControl(Control control)
        {
            if (control == null) return;

            Control targetControl = control;
            if (control.Parent != null && control.Parent.Height < 50 && !(control.Parent is Form))
            {
                targetControl = control.Parent;
            }

            Control parent = targetControl.Parent;
            if (parent != null)
            {
                string labelName = "lblErr_" + control.Name;
                Control errLabel = parent.Controls.Find(labelName, true).FirstOrDefault();
                if (errLabel != null)
                {
                    errLabel.Visible = false;
                }
            }
        }

      
        public static void ClearAllErrors(Control parent)
        {
            if (parent == null) return;
            foreach (Control c in parent.Controls)
            {
                if (c.Name != null && c.Name.StartsWith("lblErr_"))
                {
                    c.Visible = false;
                }
                if (c.HasChildren)
                {
                    ClearAllErrors(c);
                }
            }
        }


    }
}
