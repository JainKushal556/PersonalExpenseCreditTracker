using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;

namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    public partial class AddPersonControls : Form
    {
        public AddPersonControls()
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

        private void AddPersonSControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlAddPersonDataGridView, 15);
            SetRadius(pnlAddPersonInput, 15);
            SetRadius(pnlIdia, 15);
            SetRadius(btnAddPersonInputSavePerson, 10);
            SetRadius(btnAddPersonInputClear, 10);

            //Place Holder Text
            txtAddPersonInputFullName.Text = "Enter full name";
            txtAddPersonInputPhoneNumber.Text = "Enter phone number";
            txtAddPersonInputAddress.Text = "Enter address";
            txtAddPersonSearchBar.Text = "Search by name or phone number ...";

            //Place Holder Color
            txtAddPersonInputFullName.ForeColor = Color.FromArgb(191, 192, 199);
            txtAddPersonInputPhoneNumber.ForeColor = Color.FromArgb(191, 192, 199);
            txtAddPersonInputAddress.ForeColor = Color.FromArgb(191, 192, 199);
            txtAddPersonSearchBar.ForeColor = Color.FromArgb(191, 192, 199);

            //Load Data On DataGridView
            DataSet dataset = GetDataSet();

            if (dataset != null)
            {
                DataTable Table1 = dataset.Tables[0];
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = Table1;
                dataGridViewAddPerson.DataSource = bindingSource1;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }

        }

        private DataSet GetDataSet()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection sqlConnection = null;
            DataSet dataset = null;
            try
            {
                sqlConnection = new SqlConnection(CS);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetAllPersons", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@UserId", 12);
                dataset = new DataSet();
                dataAdapter.Fill(dataset);
                return dataset;
            }
            catch (Exception)
            {
                return dataset;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // All Border Cornar Radius
        protected internal void SetRadius(Control control, int radius)
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

        private void pnlAddPersonDataGridView_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlAddPersonDataGridView, 15);
        }
        private void pnlAddPersonInput_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlAddPersonInput, 15);
        }
        private void pnlIdia_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlIdia, 15);
        }
        private void btnAddPersonInputSavePerson_Resize(object sender, EventArgs e)
        {
            SetRadius(btnAddPersonInputSavePerson, 10);
        }
        private void btnAddPersonInputClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnAddPersonInputClear, 10);
        }
        private void dataGridViewAddPerson_Resize(object sender, EventArgs e)
        {
            //SetRadius(dataGridViewAddPerson, 15);
        }
        


        // All Text Outside Border
        private void pnlAddPersonInputFullName_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputFullName.Text == "")
            {
                txtAddPersonInputFullName.Text = "Enter full name";
                txtAddPersonInputFullName.ForeColor = Color.FromArgb(191, 192, 199);
            }
            pnlAddPersonInputFullName.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputFullName_Enter(object sender, EventArgs e)
        {
            pnlAddPersonInputFullName.BorderStyle = BorderStyle.FixedSingle;
            if (txtAddPersonInputFullName.Text == "Enter full name")
            {
                txtAddPersonInputFullName.Text = "";
            }
            txtAddPersonInputFullName.ForeColor = Color.FromArgb(0, 0, 0);
        }
        private void pnlAddPersonInputPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputPhoneNumber.Text == "")
            {
                txtAddPersonInputPhoneNumber.Text = "Enter phone number";
                txtAddPersonInputPhoneNumber.ForeColor = Color.FromArgb(191, 192, 199);
            }
            pnlAddPersonInputPhoneNumber.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputPhoneNumber_Enter(object sender, EventArgs e)
        {
            pnlAddPersonInputPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
            if (txtAddPersonInputPhoneNumber.Text == "Enter phone number")
            {
                txtAddPersonInputPhoneNumber.Text = "";
            }
            txtAddPersonInputPhoneNumber.ForeColor = Color.FromArgb(0, 0, 0);
        }
        private void pnlAddPersonInputAddress_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputAddress.Text == "")
            {
                txtAddPersonInputAddress.Text = "Enter address";
                txtAddPersonInputAddress.ForeColor = Color.FromArgb(191, 192, 199);
            }
            pnlAddPersonInputAddress.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputAddress_Enter(object sender, EventArgs e)
        {
            if (txtAddPersonInputAddress.Text == "Enter address")
            {
                txtAddPersonInputAddress.Text = "";
            }
            txtAddPersonInputAddress.ForeColor = Color.FromArgb(0, 0, 0);
            pnlAddPersonInputAddress.BorderStyle = BorderStyle.FixedSingle;
        }
        private void txtAddPersonSearchBar_Enter(object sender, EventArgs e)
        {
            
            if (txtAddPersonSearchBar.Text == "Search by name or phone number ...")
            {
                txtAddPersonSearchBar.Text = "";
            }
            pnlAddPersonSearchBar.BorderStyle = BorderStyle.FixedSingle;
            txtAddPersonSearchBar.ForeColor = Color.FromArgb(0, 0, 0);
        }
        private void pnlAddPersonSearchBar_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonSearchBar.Text == "")
            {
                txtAddPersonSearchBar.Text = "Search by name or phone number ...";
                txtAddPersonSearchBar.ForeColor = Color.FromArgb(191, 192, 199);
            }
            pnlAddPersonSearchBar.BorderStyle = BorderStyle.None;
        }
        

        private void dataGridViewAddPerson_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewAddPerson.Columns["colAction"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Image img = Properties.Resources.pen__1_;

                int x = e.CellBounds.Left + (e.CellBounds.Width - 20) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - 20) / 2;

                e.Graphics.DrawImage(img, new Rectangle(x, y, 20, 20));

                e.Handled = true;
            }
        }
        private void dataGridViewAddPerson_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // SL Number
            for (int i = 0; i < dataGridViewAddPerson.Rows.Count; i++)
            {
                dataGridViewAddPerson.Rows[i].Cells["colSL"].Value = i + 1;
            }

            lblDataGridViewTotalPersonsNumber.Text =
                dataGridViewAddPerson.Rows.Count.ToString();

            // Remove selection
            dataGridViewAddPerson.ClearSelection();
            dataGridViewAddPerson.CurrentCell = null;
        }

        private void dataGridViewAddPerson_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridViewAddPerson.Columns["colAction"].Index)
            {
                EditPersons editPerson = new EditPersons(this);
                editPerson.Show();          // Opens the form
                // frm.ShowDialog(); // Opens it as a modal dialog
            }
        }

        private void btnAddPersonInputClear_Click(object sender, EventArgs e)
        {
            txtAddPersonInputFullName.Text = "Enter full name";
            txtAddPersonInputPhoneNumber.Text = "Enter phone number";
            txtAddPersonInputAddress.Text = "Enter address";
            txtAddPersonInputFullName.ForeColor = Color.FromArgb(191, 192, 199);
            txtAddPersonInputPhoneNumber.ForeColor = Color.FromArgb(191, 192, 199);
            txtAddPersonInputAddress.ForeColor = Color.FromArgb(191, 192, 199);
        }

        private void btnAddPersonInputSavePerson_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Person Details Save Successfully");
        }
    }
}
