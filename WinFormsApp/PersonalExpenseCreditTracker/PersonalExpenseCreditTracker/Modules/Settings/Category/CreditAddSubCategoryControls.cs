using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public partial class CreditAddSubCategoryControls : Form
    {
        public CreditAddSubCategoryControls()
        {
            InitializeComponent();
            this.Resize += CreditAddSubCategoryControls_Resize;
            CenterPanel();
            this.txtSubCategory.Enter += new System.EventHandler(this.txtSubCategory_Enter);
            this.txtSubCategory.Leave += new System.EventHandler(this.txtSubCategory_Leave);
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CenterPanel()
        {
            pnlContent.Location = new Point(
                (this.ClientSize.Width - pnlContent.Width) / 2,
                (this.ClientSize.Height - pnlContent.Height) / 2
            );
        }
        private void CreditAddSubCategoryControls_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void CreditAddSubCategoryControls_Load(object sender, EventArgs e)
        {
            txtSubCategory.Text = "  Enter Subcategory name";
            txtSubCategory.ForeColor = Color.FromArgb(45, 45, 45);
          
        }
        private void txtSubCategory_Enter(object sender, EventArgs e)
        {
            if (txtSubCategory.Text == "  Enter Subcategory name")
            {
                txtSubCategory.Text = "";
                txtSubCategory.ForeColor = Color.FromArgb(45, 45, 45);
            }
        }
        private void txtSubCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubCategory.Text))
            {
                txtSubCategory.Text = "  Enter Subcategory name";
                txtSubCategory.ForeColor = Color.FromArgb(150, 150, 150);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
