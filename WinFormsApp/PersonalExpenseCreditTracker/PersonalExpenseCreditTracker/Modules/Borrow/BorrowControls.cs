using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class BorrowControls : Form
    {
        public BorrowControls()
        {
            InitializeComponent();

            dataGridViewAllBorrow.AutoGenerateColumns = false;

            ApplyRoundCorners();

            this.Resize += BorrowControls_Resize;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);


        private void BorrowControls_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void ApplyRoundCorners()
        {
            pnlBorrowTotalBorrowed.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlBorrowTotalBorrowed.Width,
                    pnlBorrowTotalBorrowed.Height,
                    10,
                    10));

            pnlRepaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    10,
                    10));

            pnlActiveBorrowings.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlActiveBorrowings.Width,
                    pnlActiveBorrowings.Height,
                    10,
                    10));

            pnlRepaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    10,
                    10));
        }

        private void BorrowControls_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();

            DataSet dataset = GetDataSet();
            if (dataset != null)
            {
                DataTable Table1 = dataset.Tables[0];
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = Table1;
                dataGridViewAllBorrow.DataSource = bindingSource1;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }

            AddActionImages();

            dataGridViewAllBorrow.Columns["colAction"].DisplayIndex = dataGridViewAllBorrow.Columns.Count - 1;

            //foreach (DataGridViewColumn col in dataGridViewAllLent.Columns)
            //{
            //    MessageBox.Show(col.Name + " = " + col.DisplayIndex);
            //}
        }


        private DataSet GetDataSet()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection sqlConnection = null;
            DataSet dataset = null;
            try
            {
                sqlConnection = new SqlConnection(CS);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetAllBorrow", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@UserId", 11);
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

        private void AddActionImages()
        {
            foreach (DataGridViewRow row in dataGridViewAllBorrow.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["colAction"].Value = Properties.Resources.menu;
                }
            }

            dataGridViewAllBorrow.Invalidate();
        }

        private void pnlRepaidAmount_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
