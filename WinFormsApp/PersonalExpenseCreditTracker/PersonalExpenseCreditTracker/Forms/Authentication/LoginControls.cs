using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Forms.Main;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Authentication;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class LoginControls : Form
    {
        bool isPasswordVisible = true;

        internal protected int UserId { get; set; }

        private Panel pnlLoginErrorAlert;
        private Label lblLoginErrorText;
        private PictureBox picLoginErrorIcon;

        public LoginControls()
        {
            InitializeComponent();
        }

        
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        // Free GDI object
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        // Radius Corner of These Panels
        private void SetRadius(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            IntPtr hrgn = CreateRoundRectRgn(
                0,
                0,
                control.Width + 1,
                control.Height + 1,
                radius,
                radius);

            Region region = Region.FromHrgn(hrgn);

            if (control.Region != null)
                control.Region.Dispose();

            control.Region = region;

            DeleteObject(hrgn);
        }
        
        private void LoginControls_Load(object sender, EventArgs e)
        {
            
            SetRadius(pnlLoginDataInput, 20);
            SetRadius(btnLogin, 17);

            picEye.Image = Properties.Resources.open_eye__2_;

            txtLoginEmail.Text = "Enter Email Address";
            txtLoginPassword.Text = "Enter Password";

            txtLoginEmail.ForeColor = Color.Gray;
            txtLoginPassword.ForeColor = Color.Gray;
            this.ActiveControl = pnlLoginDataInput;

            txtLoginEmail.TextChanged += txtLoginEmail_TextChanged;
            txtLoginPassword.TextChanged += txtLoginPassword_TextChanged;
        }
     
        private void ResetLoginForm()
        {
            
            txtLoginEmail.Text = "Enter Email Address";
            txtLoginEmail.ForeColor = Color.Gray;

        
            txtLoginPassword.Text = "Enter Password";
            txtLoginPassword.ForeColor = Color.Gray;
            txtLoginPassword.UseSystemPasswordChar = false;
            isPasswordVisible = true;
            picEye.Image = Properties.Resources.open_eye__2_;

            ErrorHelper.ClearAllErrors(pnlLoginDataInput);
            HideLoginError();

            this.ActiveControl = pnlLoginDataInput;
        }


        private Panel pnlCenterLoading = null;
        private Label lblLoadingText = null;

        private void ShowCenterLoading(bool isShow, string message = "Logging in, please wait...")
        {
            if (isShow)
            {
                if (pnlCenterLoading == null)
                {
                    pnlCenterLoading = new Panel
                    {
                        Size = new Size(280, 80),
                        BackColor = Color.FromArgb(15, 23, 42),
                        BorderStyle = BorderStyle.None
                    };

                    lblLoadingText = new Label
                    {
                        Text = "⏳  " + message,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    pnlCenterLoading.Controls.Add(lblLoadingText);
                    this.Controls.Add(pnlCenterLoading);
                }

                lblLoadingText.Text = "⏳  " + message;
                pnlCenterLoading.Location = new Point(
                    (this.ClientSize.Width - pnlCenterLoading.Width) / 2,
                    (this.ClientSize.Height - pnlCenterLoading.Height) / 2
                );

                SetRadius(pnlCenterLoading, 16);
                pnlCenterLoading.BringToFront();
                pnlCenterLoading.Visible = true;
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
            }
            else
            {
                if (pnlCenterLoading != null)
                {
                    pnlCenterLoading.Visible = false;
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ErrorHelper.ClearAllErrors(pnlLoginDataInput);
            HideLoginError();

            ShowCenterLoading(true, "Logging in, please wait...");

            try
            {
                AuthUI authUI = new AuthUI();
                AuthBLL authBLL = new AuthBLL();
                string errorMsg;

                authUI.email = (txtLoginEmail.Text == "Enter Email Address") ? "" : txtLoginEmail.Text.Trim();
                authUI.password = (txtLoginPassword.Text == "Enter Password") ? "" : txtLoginPassword.Text;

                CommonValidator.ValidationResult result = authUI.LoginDataIntoAuthUI();

                switch (result)
                {
                    case CommonValidator.ValidationResult.Success:
                        UserId = authBLL.GetUserIdFromDB();
                        Session.LogedInUser.SetUserId(UserId);

                        MainForm mainForm = new MainForm();
                        this.Hide();
                        mainForm.ShowDialog();
                        this.Close();
                        break;

                    case CommonValidator.ValidationResult.EmailInvalid:
                        ErrorHelper.ShowValidationError(result, errorProvider1, txtLoginEmail);
                        break;

                    case CommonValidator.ValidationResult.NewPasswordEmpty:
                        ErrorHelper.ShowErrorBelowControl(txtLoginPassword, "* Password is required.");
                        txtLoginPassword.Focus();
                        break;

                    case CommonValidator.ValidationResult.StoreProcedureError:
                        errorMsg = authUI.GetErrorMsgForLogin();
                        ShowLoginError(string.IsNullOrWhiteSpace(errorMsg) ? "Invalid email or password. Please try again." : errorMsg);
                        txtLoginPassword.Focus();
                        break;
                }
            }
            finally
            {
                ShowCenterLoading(false);
            }
        }


        private void picEye_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            txtLoginPassword.UseSystemPasswordChar = !isPasswordVisible;

            if (isPasswordVisible)
            {
                picEye.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye.Image = Properties.Resources.eye;
            }
        }

        private void txtLoginEmail_Enter(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "Enter Email Address")
            {
                txtLoginEmail.Text = "";
                txtLoginEmail.ForeColor = Color.Black;
            }
        }

        private void txtLoginEmail_Leave(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "")
            {
                txtLoginEmail.Text = "Enter Email Address";
                txtLoginEmail.ForeColor = Color.Gray;
            }
        }

        private void txtLoginPassword_Enter(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "Enter Password")
            {
                txtLoginPassword.Text = "";
                txtLoginPassword.ForeColor = Color.Black;
            }
        }

        private void txtLoginPassword_Leave(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "")
            {
                txtLoginPassword.Text = "Enter Password";
                txtLoginPassword.ForeColor = Color.Gray;
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            ResetLoginForm(); 
            ForgotPasswordControls forgotPasswordControls = new ForgotPasswordControls();
            forgotPasswordControls.ShowDialog();
            ResetLoginForm(); 
        }


        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            ResetLoginForm(); 
            this.Hide();

            RegistrationControls registrationControls = new RegistrationControls();
            registrationControls.ShowDialog();

            ResetLoginForm(); 
            this.Show();
        }


        private void txtLoginEmail_TextChanged(object sender, EventArgs e)
        {
            HideLoginError(); 
            if (txtLoginEmail.Text != "Enter Email Address" && !string.IsNullOrWhiteSpace(txtLoginEmail.Text))
            {
                ErrorHelper.HideErrorForControl(txtLoginEmail);
            }
        }


        private void txtLoginPassword_TextChanged(object sender, EventArgs e)
        {
            HideLoginError(); 
            if (txtLoginPassword.Text != "Enter Password" && !string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtLoginPassword);
            }
        }



        // Login বাটনের উপরে আইকনসহ সুন্দর এরর বক্স তৈরি ও প্রদর্শনের মেথড
        private void ShowLoginError(string message)
        {
            if (pnlLoginErrorAlert == null)
            {
               
                pnlLoginErrorAlert = new Panel();
                pnlLoginErrorAlert.Name = "pnlLoginErrorAlert";
                pnlLoginErrorAlert.Size = new Size(btnLogin.Width, 38);
                pnlLoginErrorAlert.Location = new Point(btnLogin.Left, btnLogin.Top - 46); 
                pnlLoginErrorAlert.BackColor = Color.FromArgb(254, 242, 242);

             
                picLoginErrorIcon = new PictureBox();
                picLoginErrorIcon.Size = new Size(18, 18);
                picLoginErrorIcon.Location = new Point(10, 10);
                picLoginErrorIcon.SizeMode = PictureBoxSizeMode.Zoom;
                picLoginErrorIcon.Image = Properties.Resources.info__3_; 

                // ৩. এরর মেসেজ লেবেল
                lblLoginErrorText = new Label();
                lblLoginErrorText.Font = new Font("Segoe UI Semibold", 8.75F, FontStyle.Bold);
                lblLoginErrorText.ForeColor = Color.FromArgb(220, 38, 38); 
                lblLoginErrorText.AutoSize = false;
                lblLoginErrorText.TextAlign = ContentAlignment.MiddleLeft;
                lblLoginErrorText.Location = new Point(34, 0);
                lblLoginErrorText.Size = new Size(pnlLoginErrorAlert.Width - 38, 38);

                pnlLoginErrorAlert.Controls.Add(picLoginErrorIcon);
                pnlLoginErrorAlert.Controls.Add(lblLoginErrorText);
                pnlLoginDataInput.Controls.Add(pnlLoginErrorAlert);


                SetRadius(pnlLoginErrorAlert, 10); 
            }

            lblLoginErrorText.Text = message;
            pnlLoginErrorAlert.Visible = true;
            pnlLoginErrorAlert.BringToFront();
        }

        private void HideLoginError()
        {
            if (pnlLoginErrorAlert != null)
            {
                pnlLoginErrorAlert.Visible = false;
            }
        }





    }
}
