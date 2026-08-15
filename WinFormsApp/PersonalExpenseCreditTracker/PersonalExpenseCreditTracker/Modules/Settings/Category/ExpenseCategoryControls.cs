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
        private int UserID = Session.LogedInUser.GetUserId();
        private List<int> expandedCategories = new List<int>();
        public ExpenseCategoryControls()
        {
            InitializeComponent();
            CreateColumns();
            StyleGrid();
            
            //tsmiEdit.Click += tsmiEdit_Click;
            //tsmiAddSubCategory.Click += tsmiAddSubCategory_Click;
            //tsmiDelete.Click += tsmiDelete_Click;
            
        }

        private void ExpenseCategoryControls_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSubCategories();
            FillGrid();
            dgvCategory.CellClick += dgvCategory_CellClick;
            dgvCategory.CellFormatting += dgvCategory_CellFormatting;

            lblTotalCategoryNumber.Text = categoryList.Count.ToString();

            lblTotalSubCategoryNumber.Text = subCategoryList.Count.ToString();

            int totalActive = categoryList.Count(c => c.IsActive) + subCategoryList.Count(s => s.IsActive);
            lblActiveNumber.Text = totalActive.ToString();

            int totalInactive = categoryList.Count(c => !c.IsActive) + subCategoryList.Count(s => !s.IsActive);
            lblInactiveNumber.Text = totalInactive.ToString();
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
            dgvCategory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvCategory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgvCategory.RowTemplate.Height = 40;
            //dgvCategory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Expand")
            {
                DataGridViewRow categoryRow = dgvCategory.Rows[e.RowIndex];

               
                if (categoryRow.Cells["Type"].Value == null || categoryRow.Cells["Type"].Value.ToString() != "Category")
                    return;

                string currentExpand = categoryRow.Cells["Expand"].Value != null ? categoryRow.Cells["Expand"].Value.ToString() : "";
                bool isExpanded = (currentExpand == "↓");

                
                categoryRow.Cells["Expand"].Value = isExpanded ? "→" : "↓";

               
                dgvCategory.CurrentCell = null;

               
                for (int i = e.RowIndex + 1; i < dgvCategory.Rows.Count; i++)
                {
                    DataGridViewRow nextRow = dgvCategory.Rows[i];

                    string rowType = nextRow.Cells["Type"].Value != null ? nextRow.Cells["Type"].Value.ToString() : "";
                    if (rowType == "Category")
                        break; 

                    nextRow.Visible = !isExpanded;
                }

                return;
            }

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Action")
            {
                Rectangle r = dgvCategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                bool isCategory = row.Cells["Type"].Value != null && row.Cells["Type"].Value.ToString() == "Category";

                tsmiAddSubCategory.Visible = isCategory;
                cmsCategoryAction.Tag = row;
                cmsCategoryAction.Show(dgvCategory, r.Left, r.Bottom);

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

        public void LoadCategories()
        {
            categoryList.Clear();
            using (SqlConnection con = new SqlConnection(ConnectionString))    
            {
                SqlCommand cmd = new SqlCommand("spGetActiveAndDeactiveExpenseCategoriesByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 0;
                while(dr.Read())
                {
                    ExpenseCategory c = new ExpenseCategory();
                    c.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    c.CategoryName =dr["CategoryName"].ToString();
                    c.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                    c.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    categoryList.Add(c);
                    i++;
                }
                lblTotalCategoryNumber.Text = Convert.ToString(i);
                dr.Close();
            }
        }

        public void LoadSubCategories()
        {
            subCategoryList.Clear();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetActiveAndDeactiveExpenseSubCategoriesByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    ExpenseSubCategory s = new ExpenseSubCategory();
                    s.SubCategoryID =Convert.ToInt32(dr["SubCategoryID"]);
                    s.CategoryID =Convert.ToInt32(dr["CategoryID"]);
                    s.SubCategoryName = dr["SubCategoryName"].ToString();
                    s.IsDefault =Convert.ToBoolean(dr["IsDefault"]);
                    s.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    subCategoryList.Add(s);
                    i++;
                }
                lblTotalSubCategoryNumber.Text = Convert.ToString(i);
                dr.Close();
            }
        }

        private void dgvCategory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // status Color
            if (dgvCategory.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value == null) return;

                switch (e.Value.ToString())
                {
                    case "Inactive":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10);
                        break;

                    case "Active":
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10);
                        break;
                }
            }
        }

        private void FillGrid()
        {
            int scrollIndex = dgvCategory.FirstDisplayedScrollingRowIndex;
            dgvCategory.Rows.Clear();

            foreach (ExpenseCategory cat in categoryList)
            {
                bool expanded = expandedCategories.Contains(cat.CategoryID);

                int row = dgvCategory.Rows.Add();
                dgvCategory.Rows[row].Tag = cat.CategoryID;
                dgvCategory.Rows[row].Cells["Expand"].Value = expanded ? "↓" : "→";
                dgvCategory.Rows[row].Cells["Name"].Value = cat.CategoryName;
                dgvCategory.Rows[row].Cells["Type"].Value = "Category";
                dgvCategory.Rows[row].Cells["Status"].Value = cat.IsActive ? "Active" : "Inactive";
                dgvCategory.Rows[row].Cells["Action"].Value = "⋮";

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
                        dgvCategory.Rows[subRow].DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);

                       
                        dgvCategory.Rows[subRow].Visible = expanded;
                    }
                }

                
            }

            if (scrollIndex >= 0 && scrollIndex < dgvCategory.Rows.Count)
            {
                dgvCategory.FirstDisplayedScrollingRowIndex = scrollIndex;
            }
        }


        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            ExpenseAddCategoryControls expenseAddCategoryControls = new ExpenseAddCategoryControls(this);
            if (expenseAddCategoryControls.ShowDialog() == DialogResult.OK)
            {
                LoadCategories();
                LoadSubCategories();
                FillGrid();
            }
        }

        private void tsmiAddSubCategory_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = cmsCategoryAction.Tag as DataGridViewRow;
            if (row == null) return;

            int categoryId = Convert.ToInt32(row.Tag);
            string categoryName = row.Cells["Name"].Value.ToString();

            ExpenseAddSubCategoryControls expenseAddSubCategoryControls = new ExpenseAddSubCategoryControls(categoryId, categoryName);
            if (expenseAddSubCategoryControls.ShowDialog() == DialogResult.OK)
            {
                LoadCategories();
                LoadSubCategories();
                FillGrid();
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = cmsCategoryAction.Tag as DataGridViewRow;
            if (row == null) return;
            int id = Convert.ToInt32(row.Tag); // CategoryID or SubCategoryID
            string rawName = row.Cells["Name"].Value.ToString();


            bool isSubCategory = row.Cells["Type"].Value.ToString() == "Sub Category";

            string cleanName = isSubCategory ? rawName.Replace("└", "").Trim() : rawName.Trim();

            ExpenseEditCategoryControls editForm = new ExpenseEditCategoryControls(id, cleanName, isSubCategory);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadCategories();
                LoadSubCategories();
                FillGrid();
            }
        }
    }
}
