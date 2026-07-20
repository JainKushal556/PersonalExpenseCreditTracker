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

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class LentControls : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);


        public LentControls()
        {
            InitializeComponent();

            dataGridViewAllLent.AutoGenerateColumns = false;

            ApplyRoundCorners();

            this.Resize += LentControls_Resize;
        }
        private void LentControls_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }
        private void ApplyRoundCorners()
        {
            panelTotalLent.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalLent.Width,
                    panelTotalLent.Height,
                    10,
                    10));

            panelTotalRepaid.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalRepaid.Width,
                    panelTotalRepaid.Height,
                    10,
                    10));

            panelTotalDue.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalDue.Width,
                    panelTotalDue.Height,
                    10,
                    10));

            panelTotalTransaction.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalTransaction.Width,
                    panelTotalTransaction.Height,
                    10,
                    10));

            panelExportReport.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelExportReport.Width,
                    panelExportReport.Height,
                    10,
                    10));
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {

        }

        private void btnExportReport_MouseEnter(object sender, EventArgs e)
        {
            btnExportReport.BackColor = Color.FromArgb(0, 0, 240);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LentControls_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();
           
            DataSet dataset = GetDataSet();
            if (dataset != null)
            {
                DataTable Table1 = dataset.Tables[0];
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = Table1;
                dataGridViewAllLent.DataSource = bindingSource1;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }

            AddActionImages();
            //dataGridViewAllLent.Columns["colAction"].DisplayIndex = 9;

            dataGridViewAllLent.Columns["colAction"].DisplayIndex =
    dataGridViewAllLent.Columns.Count - 1;

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
                SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetAllLent", sqlConnection);
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
            foreach (DataGridViewRow row in dataGridViewAllLent.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["colAction"].Value = Properties.Resources.menu;
                }
            }

            dataGridViewAllLent.Invalidate();
        }

        private void dataGridViewAllLent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dataGridViewAllLent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


      }
    }
