using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
using System.Data;
using System.Drawing;

namespace PersonalExpenseCreditTracker.Common
{
    public class CommonUiFunction
    {
        private static string[] NonHighlightableColumns =
        {
            "colDescription",
            "colReturnedAmount",
            "colRemainingAmount",
            "colDeadline"
        };
        public const decimal SqlAmountMax = 99999999.99m;
        private static string dateColumn = "";


        // Helper method to load a ComboBox with data (with UserID)
        public static void LoadInComboBox(string spName, int userId, string initialText, ComboBox comboBox)
        {
            DataTable dataTable = RetrieveListForComboBox(spName, userId);


            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }


        // Person
        public static void LoadInComboBox(string spName, int userId, string initialText, string addNewText, ComboBox comboBox)
        {
            DataTable dataTable = RetrieveListForComboBox(spName, userId);


            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            // Initial Row
            DataRow headerRow = dataTable.NewRow();
            headerRow[0] = 0;
            headerRow[1] = initialText;
            dataTable.Rows.InsertAt(headerRow, 0);

            // Add New Row
            DataRow addNewRow = dataTable.NewRow();
            addNewRow[0] = -99;
            addNewRow[1] = addNewText;
            dataTable.Rows.Add(addNewRow);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;

            comboBox.ForeColor = System.Drawing.Color.Gray;
        }


        // Helper method to load a ComboBox with data (with UserID)
        public static void LoadInComboBox(string spName, string initialText, ComboBox comboBox, string paramName, int paramValue)
        {
            DataTable dataTable = RetrieveListForComboBox(spName, paramName, paramValue);

            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;

            comboBox.SelectedIndex = 0;
        }



        // SubCategory last index add
        public static void LoadInComboBox(string spName, string initialText, string addNewText, ComboBox comboBox, string paramName, int paramValue)
        {
            DataTable dataTable = RetrieveListForComboBox(spName, paramName, paramValue);

            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            DataRow headerRow = dataTable.NewRow();
            headerRow[0] = 0;
            headerRow[1] = initialText;
            dataTable.Rows.InsertAt(headerRow, 0);

            DataRow addNewRow = dataTable.NewRow();
            addNewRow[0] = -99;
            addNewRow[1] = addNewText;
            dataTable.Rows.Add(addNewRow);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }



        // Helper method to load a ComboBox with data (without UserID)
        public static void LoadInComboBox(string spName, string initialText, ComboBox comboBox)
        {
            DataTable dataTable = RetrieveListForComboBox(spName);

            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }

        // Helper method to load a ComboBox with data (without UserID)
        public static void LoadInComboBox(string spName, string initialText, string lastText, ComboBox comboBox)
        {
            DataTable dataTable = RetrieveListForComboBox(spName);

            // 🟢 এখানে যোগ করবেন:
            if (dataTable == null || dataTable.Columns.Count < 2)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            }

            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            DataRow addNewRow = dataTable.NewRow();
            addNewRow[0] = -99;
            addNewRow[1] = lastText;
            dataTable.Rows.Add(addNewRow);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }


        // Retrieves list data for ComboBoxes from BLL layer (with UserID)
        public static DataTable RetrieveListForComboBox(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveListForComboBox(spName, userId);
            return dataTable;
        }

        // Retrieves list data for ComboBoxes from BLL layer (without UserID)
        public static DataTable RetrieveListForComboBox(string spName)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveListForComboBox(spName);
            return dataTable;
        }
        // Retrieves list data for ComboBoxes from BLL layer (without UserID)
        public static DataTable RetrieveListForComboBox(string spName, string paramName, int paramValue)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveListForComboBox(spName, paramName, paramValue);
            return dataTable;
        }

        // Retrieves filtered data by Status ID from BLL layer
        public static DataTable RetrieveFilteredDataByStatus(string spName, int userid, string paramName, int filterId)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveFilteredDataByStatus(spName, userid, paramName, filterId);
            return dataTable;
        }

        // Retrieves table data for GridView display from BLL layer (without UserID)
        public static DataTable RetrieveDataForGridView(string spName)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveDataForGridView(spName);
            return dataTable;
        }

        // Retrieves table data for GridView display from BLL layer (with UserID)
        public static DataTable RetrieveDataForGridView(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveDataForGridView(spName, userId);
            return dataTable;
        }

        // Retrieves filtered data by a range of two decimal values from BLL layer
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, decimal paramId1, string paramName2, decimal paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveDataByUserIdAndFilterId(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }

        // Retrieves filtered data by a range of two int values from BLL layer
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveDataByUserIdAndFilterId(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }

        public static DataTable RetrieveErrorCategoryDataIntoCategory(string spName, int userId, int paramId1, int paramId2, string paramName, string paramName1, string paramName2, string paramName3)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveErrorCategoryDataIntoCategory(spName, userId, paramId1, paramId2, paramName, paramName1, paramName2, paramName3);
            return dataTable;
        }

        // Retrieves filtered data by a date range from BLL layer
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveDataByUserIdAndFilterId(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }


        public static DataTable SearchDataInLentOrBorrow(DataTable masterTable, TextBox txtBox)
        {
            if (masterTable == null)
                return null;

            string search = txtBox.Text.Trim();

           
            if (string.IsNullOrWhiteSpace(search) || search == "Search...")
            {
                masterTable.DefaultView.RowFilter = "";
                return masterTable.DefaultView.ToTable();
            }

            search = search.Replace("'", "''");

            List<string> filters = new List<string>();

            if (masterTable.Columns.Contains("Amount"))
                filters.Add(
                    "Convert(Amount, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("ReturnedAmount"))
                filters.Add(
                    "Convert(ReturnedAmount, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("RemainingAmount"))
                filters.Add(
                    "Convert(RemainingAmount, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("LentAt"))
                filters.Add(
                    "Convert(LentAt, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("BorrowAt"))
                filters.Add(
                    "Convert(BorrowAt, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("DeadlineAt"))
                filters.Add(
                    "Convert(DeadlineAt, 'System.String') LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("PersonName"))
                filters.Add(
                    "PersonName LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("PaymentName"))
                filters.Add(
                    "PaymentName LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("StatusName"))
                filters.Add(
                    "StatusName LIKE '%" + search + "%'");

            if (filters.Count == 0)
                return masterTable.Clone();

            masterTable.DefaultView.RowFilter =
                string.Join(" OR ", filters.ToArray());

            return masterTable.DefaultView.ToTable();
        }

        //public static DataTable SearchDataInLentOrBorrow(DataTable masterTable, TextBox txtBox)
        //{
        //    string search = txtBox.Text.Trim().Replace("'", "''");
        //    if (masterTable == null) return null;
        //    if (string.IsNullOrWhiteSpace(search))
        //    {
        //        masterTable.DefaultView.RowFilter = "";
        //        return masterTable.DefaultView.ToTable();
        //    }
        //    if (masterTable.Columns.Contains("LentAt"))
        //        dateColumn = "LentAt";
        //    else
        //        dateColumn = "BorrowAt";
        //    masterTable.DefaultView.RowFilter = string.Format(
        //       "Convert(Amount, 'System.String') LIKE '%{0}%' OR " +
        //       "Convert({1}, 'System.String') LIKE '%{0}%' OR " +
        //       "PersonName LIKE '%{0}%' OR " +
        //       "PaymentName LIKE '%{0}%' OR " +
        //       "StatusName LIKE '%{0}%'",
        //       search, dateColumn);
        //    DataTable filteredTable = masterTable.DefaultView.ToTable();
        //    return filteredTable;
        //}

        public static DataTable SearchDataInExpenseOrCredit(DataTable masterTable, TextBox txtBox)
        {
            string search = txtBox.Text.Trim().Replace("'", "''");

            if (masterTable == null) return null;
            if (string.IsNullOrWhiteSpace(search))
            {
                masterTable.DefaultView.RowFilter = "";
                return masterTable.DefaultView.ToTable();
            }
            if (masterTable.Columns.Contains("ExpenseAt"))
                dateColumn = "ExpenseAt";
            else
                dateColumn = "CreditAt";

            

            masterTable.DefaultView.RowFilter = string.Format(
              "Convert(Amount, 'System.String') LIKE '%{0}%' OR " +
              "Convert({1}, 'System.String') LIKE '%{0}%' OR " +
              "CategoryName LIKE '%{0}%' OR " +
              "PaymentName LIKE '%{0}%' OR " +
              "SubCategoryName LIKE '%{0}%'",
               search, dateColumn);

         
            DataTable filteredTable = masterTable.DefaultView.ToTable();

            
            return filteredTable;
        }

        public static void HighlightSearch(DataGridView dgv, TextBox txtBox)
        {
            string search = txtBox.Text.Trim();

            // সব cell reset (Revert to column default styles)
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.Empty;
                    cell.Style.ForeColor = Color.Empty;
                }
            }

            // Search empty হলে কিছু করবে না
            if (string.IsNullOrWhiteSpace(search))
                return;

            // Match হওয়া cell highlight
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null &&
                        cell.Value.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cell.Style.BackColor = Color.FromArgb(232, 240, 253);
                        cell.Style.ForeColor = Color.FromArgb(25, 103, 210);
                    }
                }
                foreach (string NonHighlightableColumn in NonHighlightableColumns)
                {
                    if (dgv.Columns.Contains(NonHighlightableColumn))
                    {
                        row.Cells[NonHighlightableColumn].Style.BackColor = Color.Empty;
                        row.Cells[NonHighlightableColumn].Style.ForeColor = Color.Empty;
                    }
                }
            }
        }
        public static DataTable SearchDataInNote(DataTable masterTable, TextBox txtBox)
        {
            string search = txtBox.Text.Trim().Replace("'", "''");

            if (masterTable == null) return null;
            if (string.IsNullOrWhiteSpace(search))
            {
                masterTable.DefaultView.RowFilter = "";
                return masterTable.DefaultView.ToTable();
            }

            masterTable.DefaultView.RowFilter = string.Format(
                "NoteTitle LIKE '%{0}%' OR " +
                "NotePriorityName LIKE '%{0}%' OR " +
                "Convert(CreatedAt, 'System.String') LIKE '%{0}%'",
                search);

            DataTable filteredTable = masterTable.DefaultView.ToTable();
            return filteredTable;
        }


        public static DataTable SearchDataInTask(DataTable masterTable, TextBox txtBox)
        {
            if (masterTable == null)
                return null;

            string search = txtBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(search) || search == "Search...")
            {
                masterTable.DefaultView.RowFilter = "";
                return masterTable.DefaultView.ToTable();
            }

            search = search.Replace("'", "''");

            List<string> filters = new List<string>();

            if (masterTable.Columns.Contains("TaskTitle"))
                filters.Add("TaskTitle LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("PriorityName"))
                filters.Add("PriorityName LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("TaskStatusName"))
                filters.Add("TaskStatusName LIKE '%" + search + "%'");

            if (masterTable.Columns.Contains("CreatedAt"))
                filters.Add(
                    "Convert(CreatedAt, 'System.String') LIKE '%" +
                    search + "%'"
                );

            if (masterTable.Columns.Contains("Deadline"))
                filters.Add(
                    "Convert(Deadline, 'System.String') LIKE '%" +
                    search + "%'"
                );

            if (filters.Count == 0)
                return masterTable.Clone();

            masterTable.DefaultView.RowFilter =string.Join(" OR ", filters.ToArray());

            return masterTable.DefaultView.ToTable();
        }
        //public static DataTable SearchDataInTask(DataTable masterTable, TextBox txtBox)
        //{
        //    string search = txtBox.Text.Trim().Replace("'", "''");

        //    if (masterTable == null) return null;
        //    if (string.IsNullOrWhiteSpace(search))
        //    {
        //        masterTable.DefaultView.RowFilter = "";
        //        return masterTable.DefaultView.ToTable();
        //    }

        //    masterTable.DefaultView.RowFilter = string.Format(
        //        "TaskTitle LIKE '%{0}%' OR " +
        //        "TaskStatusName LIKE '%{0}%' OR " +
        //        "PriorityName LIKE '%{0}%' OR " +
        //        "Convert(CreatedAt, 'System.String') LIKE '%{0}%'",
        //        search);

        //    DataTable filteredTable = masterTable.DefaultView.ToTable();
        //    return filteredTable;
        //}

        public static DataTable SearchDataInPersons(DataTable masterTable, TextBox txtBox)
        {
            string search = txtBox.Text.Trim().Replace("'", "''");

            if (masterTable == null) return null;
            if (string.IsNullOrWhiteSpace(search))
            {
                masterTable.DefaultView.RowFilter = "";
                return masterTable.DefaultView.ToTable();
            }

            masterTable.DefaultView.RowFilter = string.Format(
                "PersonName LIKE '%{0}%' OR " +
                "PhoneNumber LIKE '%{0}%'",
                search);

            DataTable filteredTable = masterTable.DefaultView.ToTable();
            return filteredTable;
        }


        public static void SetComboBoxHeightAndOwnerDraw(ComboBox comboBox)
        {
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DrawItem += ComboBox_DrawItem;
        }
       
        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox combo = sender as ComboBox;
            if (combo == null) return;

            e.DrawBackground();

            Color textColor = Color.Black;
            if (e.Index == 0)
            {
                textColor = Color.Gray;
            }

            Brush textBrush = new SolidBrush(textColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textBrush = SystemBrushes.HighlightText;
            }

            string text = combo.GetItemText(combo.Items[e.Index]);
            
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Near;
                Rectangle rect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 2, e.Bounds.Height);
                e.Graphics.DrawString(text, combo.Font, textBrush, rect, sf);
            }

            e.DrawFocusRectangle();
        }

        public static void SetComboBoxHeightAndOwnerDraw1(ComboBox comboBox)
        {
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DrawItem -= ComboBox_DrawItem1;
            comboBox.DrawItem += ComboBox_DrawItem1;
        }

        private static void ComboBox_DrawItem1(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = sender as ComboBox;
            if (combo == null) return;

            e.DrawBackground();

            string text = combo.GetItemText(combo.Items[e.Index]);

            Color textColor = (e.Index == 0) ? Color.Gray : Color.Black;


            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textColor = SystemColors.HighlightText;
            }


            Rectangle rect = new Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                text,
                combo.Font,
                rect,
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );

            e.DrawFocusRectangle();
        }


    }
}
