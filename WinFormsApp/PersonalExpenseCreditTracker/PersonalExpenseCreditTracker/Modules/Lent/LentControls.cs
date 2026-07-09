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


namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class LentControls : Form
    {
        public LentControls()
        {
            InitializeComponent();
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
            dataGridViewAllLent.Columns["colAction"].DisplayIndex = 10;
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
            catch (Exception ex)
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
        }

        private void dataGridViewAllLent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dataGridViewAllLent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


      }
    }
