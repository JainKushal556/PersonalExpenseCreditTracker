using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    public partial class PersonLogOutControls : Form
    {

        public PersonLogOutControls()
        {
            InitializeComponent();
            this.Resize += PersonLogOutControl_Resize;
            CenterPanel();
        }

        private void PersonLogOutControls_Load(object sender, EventArgs e)
        {

        }
        private void CenterPanel()
        {
            pnlContent.Location = new Point(
                (this.ClientSize.Width - pnlContent.Width) / 2,
                (this.ClientSize.Height - pnlContent.Height) / 2
            );
        }
        private void PersonLogOutControl_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            SettingsUi settingUi = new SettingsUi();

            settingUi.UserId =
                Session.LogedInUser.GetUserId();

            CommonValidator.ValidationResult validationResult =
                settingUi.LogoutUserIntoSettingsUi();

            switch (validationResult)
            {
                case CommonValidator.ValidationResult.Success:
                    Application.Exit();
                    this.Close();

                    break;

                default:

                    MessageBox.Show(
                        "Unable to logout. Please try again.",
                        "Logout",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
