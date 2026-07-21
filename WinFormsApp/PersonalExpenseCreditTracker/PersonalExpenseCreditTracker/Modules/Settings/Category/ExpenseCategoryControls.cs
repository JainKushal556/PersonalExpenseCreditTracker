using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public partial class ExpenseCategoryControls : Form
    {
        private List<ExpenseCategory> categoryList = new List<ExpenseCategory>();
        private List<ExpenseSubCategory> subCategoryList = new List<ExpenseSubCategory>();
        private  string ConnectionString =ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private int UserID = 11;
        private List<int> expandedCategories = new List<int>();
        public ExpenseCategoryControls()
        {
            InitializeComponent();
            CreateColumns();
            StyleGrid();
            dgvCategory.CellClick += dgvCategory_CellClick;
            //tsmiEdit.Click += tsmiEdit_Click;
            //tsmiAddSubCategory.Click += tsmiAddSubCategory_Click;
            //tsmiDelete.Click += tsmiDelete_Click;
            
        }

        private void ExpenseCategoryControls_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSubCategories();
            FillGrid();
        }

        private void CreateColumns()
        {
            dgvCategory.Columns.Clear();

            DataGridViewTextBoxColumn colExpand = new DataGridViewTextBoxColumn();
            colExpand.Name = "Expand";
            colExpand.HeaderText = "";
            colExpand.FillWeight = 5;
            colExpand.MinimumWidth = 35;
            colExpand.ReadOnly = true;
            colExpand.SortMode = DataGridViewColumnSortMode.NotSortable;
            colExpand.DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
            colExpand.DefaultCellStyle.Font =new Font("Segoe UI Symbol", 11F, FontStyle.Bold);
       

            // Category/Subcategory
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.Name = "Name";
            colName.HeaderText = "Category / Sub Category";
            colName.FillWeight = 55;

            // Type
            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "Type";
            colType.HeaderText = "Type";
            colType.FillWeight = 20;
            

            // Status
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "Status";
            colStatus.HeaderText = "Status";
            colStatus.FillWeight = 12;
            colStatus.MinimumWidth = 20;

            // Action
            DataGridViewTextBoxColumn colAction = new DataGridViewTextBoxColumn();
            colAction.Name = "Action";
            colAction.HeaderText = "Action";
            colAction.FillWeight = 8;
            colAction.MinimumWidth =80;
          
            

            colAction.DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
            colAction.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colAction.DefaultCellStyle.ForeColor =Color.RoyalBlue;

            dgvCategory.Columns.Add(colExpand);
            dgvCategory.Columns.Add(colName);
            dgvCategory.Columns.Add(colType);    
            dgvCategory.Columns.Add(colStatus);   
            dgvCategory.Columns.Add(colAction);    
        }

        private void StyleGrid()
        {
            dgvCategory.BorderStyle = BorderStyle.None;
            dgvCategory.BackgroundColor = Color.White;
            dgvCategory.GridColor = Color.FromArgb(235, 235, 235);
            dgvCategory.ColumnHeadersHeightSizeMode =DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategory.ColumnHeadersHeight = 45;  
            dgvCategory.ColumnHeadersDefaultCellStyle.Padding =new Padding(0, 8, 0, 8); 
            dgvCategory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCategory.ColumnHeadersDefaultCellStyle.BackColor =Color.FromArgb(248, 248, 252);
            dgvCategory.ColumnHeadersDefaultCellStyle.ForeColor =Color.FromArgb(55, 55, 55);
            dgvCategory.ColumnHeadersDefaultCellStyle.Font =new Font("Segoe UI", 10, FontStyle.Bold);
            
            dgvCategory.DefaultCellStyle.Font =new Font("Segoe UI", 10);
            dgvCategory.DefaultCellStyle.SelectionBackColor =Color.FromArgb(236, 233, 255);
            dgvCategory.DefaultCellStyle.SelectionForeColor =Color.Black;
            dgvCategory.RowTemplate.Height = 40;
            dgvCategory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            dgvCategory.Columns["Type"].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
            dgvCategory.Columns["Name"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCategory.Columns["Action"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            dgvCategory.Columns["Status"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
           

             //Header Alignment
            dgvCategory.ColumnHeadersDefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleLeft;

            // Cell Alignment
            dgvCategory.Columns["Expand"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
            dgvCategory.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Type"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Status"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Action"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Action"].DefaultCellStyle.ForeColor =Color.RoyalBlue;
            dgvCategory.Columns["Action"].DefaultCellStyle.Font =new Font("Segoe UI", 9, FontStyle.Bold);

        }


        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Expand")
            {
                int categoryID = Convert.ToInt32(dgvCategory.Rows[e.RowIndex].Tag);

                if (expandedCategories.Contains(categoryID))
                    expandedCategories.Remove(categoryID);
                else
                    expandedCategories.Add(categoryID);

                FillGrid();
                return;
            }

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Action")
            {
                Rectangle r = dgvCategory.GetCellDisplayRectangle(e.ColumnIndex,e.RowIndex,true);
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                bool isCategory =row.Cells["Type"].Value.ToString() == "Category";

                tsmiAddSubCategory.Visible = isCategory;
                cmsCategoryAction.Tag = row;
                cmsCategoryAction.Show(
                    dgvCategory,
                    r.Left,
                    r.Bottom);

                return;
            }
        }

        public class ExpenseCategory
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public bool IsDefault { get; set; }
            public bool IsActive { get; set; }
        }

        public class ExpenseSubCategory
        {
            public int SubCategoryID { get; set; }
            public int CategoryID { get; set; }
            public string SubCategoryName { get; set; }
            public bool IsDefault { get; set; }
            public bool IsActive { get; set; }
        }

        private void LoadCategories()
           {
                  categoryList.Clear();
                  using (SqlConnection con = new SqlConnection(ConnectionString))    
                  {
                       SqlCommand cmd =new SqlCommand("spGetExpenseCategoriesByUserID", con);
                               cmd.CommandType = CommandType.StoredProcedure;
                               cmd.Parameters.AddWithValue("@UserID", UserID);
                               con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        while(dr.Read())
                             {
                                ExpenseCategory c = new ExpenseCategory();
                                 c.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                                 c.CategoryName =dr["CategoryName"].ToString();
                                 c.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                                 c.IsActive =Convert.ToBoolean(dr["IsActive"]);
                                 categoryList.Add(c);
                            }
                           dr.Close();
                              }
                }

        private void LoadSubCategories()
        {
            subCategoryList.Clear();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd =new SqlCommand("spGetExpenseSubCategoriesByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ExpenseSubCategory s = new ExpenseSubCategory();
                    s.SubCategoryID =Convert.ToInt32(dr["SubCategoryID"]);
                    s.CategoryID =Convert.ToInt32(dr["CategoryID"]);
                    s.SubCategoryName = dr["SubCategoryName"].ToString();
                    s.IsDefault =Convert.ToBoolean(dr["IsDefault"]);
                    s.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    subCategoryList.Add(s);
                }

                dr.Close();
            }
        }

        private void FillGrid()
        {
            dgvCategory.Rows.Clear();

            foreach (ExpenseCategory cat in categoryList)
            {
                bool expanded = expandedCategories.Contains(cat.CategoryID);

                int row = dgvCategory.Rows.Add();
                dgvCategory.Rows[row].Tag = cat.CategoryID;
                dgvCategory.Rows[row].Cells["Expand"].Value = expanded ? "∨" : ">";
                dgvCategory.Rows[row].Cells["Name"].Value = cat.CategoryName;
                dgvCategory.Rows[row].Cells["Type"].Value = "Category";
                dgvCategory.Rows[row].Cells["Status"].Value = cat.IsActive ? "Active" : "Inactive";
                dgvCategory.Rows[row].Cells["Action"].Value = "⋮";

                if (expanded)
                {
                    foreach (ExpenseSubCategory sub in subCategoryList)
                    {
                        if (sub.CategoryID == cat.CategoryID)
                        {
                            int subRow = dgvCategory.Rows.Add();
                            dgvCategory.Rows[subRow].Tag = sub.SubCategoryID;
                            dgvCategory.Rows[subRow].Cells["Expand"].Value = "";
                            dgvCategory.Rows[subRow].Cells["Name"].Value = "      └ " + sub.SubCategoryName;
                            dgvCategory.Rows[subRow].Cells["Type"].Value = "Sub Category";
                            dgvCategory.Rows[subRow].Cells["Status"].Value = sub.IsActive ? "Active" : "Inactive";
                            dgvCategory.Rows[subRow].Cells["Action"].Value = "⋮";

                            dgvCategory.Rows[subRow].DefaultCellStyle.BackColor =
                                Color.FromArgb(248, 250, 255);
                        }
                    }
                }
            }
        }
        //private void tsmiEdit_Click(object sender, EventArgs e)
        //{
        //    DataGridViewRow row = (DataGridViewRow)cmsCategoryAction.Tag;
        //    MessageBox.Show("Edit");
        //}
        //private void tsmiAddSubCategory_Click(object sender, EventArgs e)
        //{
        //    DataGridViewRow row = (DataGridViewRow)cmsCategoryAction.Tag;
        //    MessageBox.Show("Add Sub Category");
        //}
        //private void tsmiDelete_Click(object sender, EventArgs e)
        //{
        //    DataGridViewRow row = (DataGridViewRow)cmsCategoryAction.Tag;
        //    MessageBox.Show("Delete");
        //}
    }
}
