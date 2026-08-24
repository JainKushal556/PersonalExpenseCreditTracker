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
    public partial class CreditCategoryControls : Form
    {
        private List<CreditCategory> categoryList = new List<CreditCategory>();
        private List<CreditSubCategory> subCategoryList = new List<CreditSubCategory>();
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private int UserID = Session.LogedInUser.GetUserId();
        private List<int> expandedCategories = new List<int>();
        private int selectedSubCategoryID = 0;
        public CreditCategoryControls()
        {
            InitializeComponent();
            CreateColumns();
            StyleGrid();
            dgvCategory.CellClick += dgvCategory_CellClick;
            //tsmiEdit.Click += tsmiEdit_Click;
            //tsmiAddSubCategory.Click += tsmiAddSubCategory_Click;
            //tsmiDelete.Click += tsmiDelete_Click;

        }

        private void CreditCategoryControls_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSubCategories();
            FillGrid();

            lblTotalCategoryNumber.Text = categoryList.Count.ToString();

            lblTotalSubCategoryNumber.Text = subCategoryList.Count.ToString();

            int totalActive = categoryList.Count(c => c.IsActive) + subCategoryList.Count(s => s.IsActive);
            lblActiveNumber.Text = totalActive.ToString();

            int totalInactive = categoryList.Count(c => !c.IsActive) + subCategoryList.Count(s => !s.IsActive);
            lblInactiveNumber.Text = totalInactive.ToString();

            tsmiEdit.Image = Properties.Resources.pen;
            tsmiAddSubCategory.Image = Properties.Resources.plus__2_;

            cmsCategoryAction.ImageScalingSize = new Size(20, 20);
            cmsCategoryAction.AutoSize = true;

            foreach (ToolStripItem item in cmsCategoryAction.Items)
            {
                item.AutoSize = true;
            }
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
            colExpand.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colExpand.DefaultCellStyle.Font = new Font("Segoe UI Symbol", 11F, FontStyle.Bold);


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
            colAction.MinimumWidth = 80;



            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAction.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colAction.DefaultCellStyle.ForeColor = Color.RoyalBlue;

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
            dgvCategory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategory.ColumnHeadersHeight = 45;
            dgvCategory.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 8, 0, 8);
            dgvCategory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCategory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 252);
            dgvCategory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(55, 55, 55);
            dgvCategory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvCategory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCategory.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvCategory.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCategory.RowTemplate.Height = 40;
            //dgvCategory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            dgvCategory.Columns["Type"].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
            dgvCategory.Columns["Name"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCategory.Columns["Action"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            dgvCategory.Columns["Status"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);


            //Header Alignment
            dgvCategory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Cell Alignment
            dgvCategory.Columns["Expand"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCategory.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategory.Columns["Action"].DefaultCellStyle.ForeColor = Color.RoyalBlue;
            dgvCategory.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Enable double buffering to eliminate grid flickering
            System.Reflection.PropertyInfo pi = typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(dgvCategory, true, null);
            }
        }


        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            // Skip Action column (it opens context menu)
            if (dgvCategory.Columns[e.ColumnIndex].Name == "Action")
            {
                Rectangle r = dgvCategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                bool isCategory = row.Cells["Type"].Value.ToString() == "Category";

                tsmiAddSubCategory.Visible = isCategory;
                cmsCategoryAction.Tag = row;

                // Rebuild items so size fits only visible items
                cmsCategoryAction.Items.Clear();
                tsmiEdit.AutoSize = true;
                cmsCategoryAction.Items.Add(tsmiEdit);
                if (isCategory)
                {
                    tsmiAddSubCategory.AutoSize = true;
                    cmsCategoryAction.Items.Add(tsmiAddSubCategory);
                }

                cmsCategoryAction.AutoSize = true;
                cmsCategoryAction.Show(
                    dgvCategory,
                    new Point(r.Right, r.Bottom),
                    ToolStripDropDownDirection.BelowLeft);

                return;
            }

            DataGridViewRow clickedRow = dgvCategory.Rows[e.RowIndex];
            string type = clickedRow.Cells["Type"].Value != null ? clickedRow.Cells["Type"].Value.ToString() : "";

            if (type == "Category")
            {
                int catId = Convert.ToInt32(clickedRow.Tag);
                bool isExpanded = expandedCategories.Contains(catId);

                // Accordion: collapse all, then expand clicked one
                expandedCategories.Clear();
                if (!isExpanded)
                {
                    expandedCategories.Add(catId);
                    // Highlight the first subcategory by default when category expands
                    var firstSub = subCategoryList.FirstOrDefault(s => s.CategoryID == catId);
                    selectedSubCategoryID = firstSub != null ? firstSub.SubCategoryID : 0;
                }
                else
                {
                    selectedSubCategoryID = 0;
                }

                FillGrid();
            }
            else if (type == "Sub Category")
            {
                int subId = Convert.ToInt32(clickedRow.Tag);
                if (selectedSubCategoryID == subId) return;

                selectedSubCategoryID = subId;

                // Update row colors in-place without reloading the entire grid
                foreach (DataGridViewRow row in dgvCategory.Rows)
                {
                    string rType = row.Cells["Type"].Value != null ? row.Cells["Type"].Value.ToString() : "";
                    if (rType == "Sub Category")
                    {
                        int rSubId = Convert.ToInt32(row.Tag);
                        if (rSubId == selectedSubCategoryID)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
                            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 242, 255);
                            row.DefaultCellStyle.SelectionForeColor = Color.Black;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.Black;
                        }
                    }
                }
                dgvCategory.ClearSelection();
            }
        }

        public class CreditCategory
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public bool IsDefault { get; set; }
            public bool IsActive { get; set; }
        }

        public class CreditSubCategory
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
                SqlCommand cmd = new SqlCommand("spGetActiveAndDeactiveCreditCategoriesByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    CreditCategory c = new CreditCategory();
                    c.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    c.CategoryName = dr["CategoryName"].ToString();
                    c.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                    c.IsActive = Convert.ToBoolean(dr["IsActive"]);
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
                SqlCommand cmd = new SqlCommand("spGetActiveAndDeactiveCreditSubCategoriesByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    CreditSubCategory s = new CreditSubCategory();
                    s.SubCategoryID = Convert.ToInt32(dr["SubCategoryID"]);
                    s.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    s.SubCategoryName = dr["SubCategoryName"].ToString();
                    s.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                    s.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    subCategoryList.Add(s);
                    i++;
                }
                lblTotalSubCategoryNumber.Text = Convert.ToString(i);
                dr.Close();
            }
        }

        private void FillGrid()
        {
            int scrollIndex = dgvCategory.FirstDisplayedScrollingRowIndex;
            dgvCategory.Rows.Clear();

            foreach (CreditCategory cat in categoryList)
            {
                bool expanded = expandedCategories.Contains(cat.CategoryID);

                int row = dgvCategory.Rows.Add();
                dgvCategory.Rows[row].Tag = cat.CategoryID;
                dgvCategory.Rows[row].Cells["Expand"].Value = expanded ? "↓" : "→";
                dgvCategory.Rows[row].Cells["Name"].Value = cat.CategoryName;
                dgvCategory.Rows[row].Cells["Type"].Value = "Category";
                dgvCategory.Rows[row].Cells["Status"].Value = cat.IsActive ? "Active" : "Inactive";
                dgvCategory.Rows[row].Cells["Action"].Value = "⋮";

                // Category row remains white (no blue highlight)
                dgvCategory.Rows[row].DefaultCellStyle.BackColor = Color.White;
                dgvCategory.Rows[row].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvCategory.Rows[row].DefaultCellStyle.SelectionForeColor = Color.Black;

                if (expanded)
                {
                    dgvCategory.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                    foreach (CreditSubCategory sub in subCategoryList)
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

                            bool isSubSelected = (sub.SubCategoryID == selectedSubCategoryID);
                            if (isSubSelected)
                            {
                                dgvCategory.Rows[subRow].DefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
                                dgvCategory.Rows[subRow].DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 242, 255);
                                dgvCategory.Rows[subRow].DefaultCellStyle.SelectionForeColor = Color.Black;
                            }
                            else
                            {
                                dgvCategory.Rows[subRow].DefaultCellStyle.BackColor = Color.White;
                                dgvCategory.Rows[subRow].DefaultCellStyle.SelectionBackColor = Color.White;
                                dgvCategory.Rows[subRow].DefaultCellStyle.SelectionForeColor = Color.Black;
                            }
                        }
                    }
                }
            }

            dgvCategory.ClearSelection();

            if (scrollIndex >= 0 && scrollIndex < dgvCategory.Rows.Count)
            {
                dgvCategory.FirstDisplayedScrollingRowIndex = scrollIndex;
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {

            CreditAddCategoryControls creditAddCategoryControls = new CreditAddCategoryControls(this);
            if (creditAddCategoryControls.ShowDialog() == DialogResult.OK)
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

            CreditAddSubCategoryControls creditAddSubCategoryControls = new CreditAddSubCategoryControls(categoryId, categoryName);
            if (creditAddSubCategoryControls.ShowDialog() == DialogResult.OK)
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

            CreditEditCategoryControls editForm = new CreditEditCategoryControls(id, cleanName, isSubCategory);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Grid Reload/Refresh
                LoadCategories();
                LoadSubCategories();
                FillGrid();
            }
        }

        public void ResetCategoryView()
        {
            if (cmsCategoryAction != null && cmsCategoryAction.Visible)
            {
                cmsCategoryAction.Close();
            }

            bool needsRefresh = (expandedCategories != null && expandedCategories.Count > 0) || selectedSubCategoryID != 0;

            if (expandedCategories != null)
            {
                expandedCategories.Clear();
            }
            selectedSubCategoryID = 0;

            if (needsRefresh)
            {
                FillGrid();
            }
        }
    }
}
