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
using BLLayer.Settings;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
using Excel = Microsoft.Office.Interop.Excel;
using PersonalExpenseCreditTracker.Helpers;

namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    public partial class AddPersonControls : Form
    {
        private DataTable AllLentData = new DataTable();
        private DataTable masterData = new DataTable();

        public string LastAddedPersonName { get; set; }

        public AddPersonControls()
        {
            InitializeComponent();
            StylePersonGrid();
            typeof(DataGridView).InvokeMember(
                 "DoubleBuffered",
                 System.Reflection.BindingFlags.NonPublic |
                 System.Reflection.BindingFlags.Instance |
                 System.Reflection.BindingFlags.SetProperty,
                 null,
                 dataGridViewAddPerson,
                 new object[] { true });
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
            dataGridViewAddPerson.CellPainting += dataGridViewAddPerson_CellPainting;
            //int userID = 11;
            SetRadius(pnlAddPersonDataGridView, 15);
            SetRadius(pnlAddPersonInput, 15);
            SetRadius(pnlIdia, 15);
            SetRadius(btnAddPersonInputSavePerson, 5);
            SetRadius(btnAddPersonInputClear, 5);

            //Place Holder Text
            txtAddPersonInputFullName.Text = "Enter Full Name";
            txtAddPersonInputPhoneNumber.Text = "Enter Phone Number";
            txtAddPersonInputAddress.Text = "Enter Address";
            txtAddPersonSearchBar.Text = "Search by name or phone number ...";

            //Place Holder Color
            txtAddPersonInputFullName.ForeColor = Color.Gray;
            txtAddPersonInputPhoneNumber.ForeColor = Color.Gray;
            txtAddPersonInputAddress.ForeColor = Color.Gray;
            txtAddPersonSearchBar.ForeColor = Color.FromArgb(191, 192, 199);

            //Load Data On DataGridView
            LoadData();

        }

        public void LoadData()
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllPersons", Session.LogedInUser.GetUserId());

            if (dataTable != null)
            {
                masterData = dataTable.Copy();

                // Newest person first
                if (dataTable.Columns.Contains("PersonID"))
                {
                    dataTable.DefaultView.Sort = "PersonID DESC";
                    dataTable = dataTable.DefaultView.ToTable();
                }

                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = dataTable;
                dataGridViewAddPerson.DataSource = bindingSource1;
            }
            else
            {
                MessageBox.Show("No Data Found");
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

        // All Text Outside Border
        private void pnlAddPersonInputFullName_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputFullName.Text == "")
            {
                txtAddPersonInputFullName.Text = "Enter Full Name";
                txtAddPersonInputFullName.ForeColor = Color.Gray;
            }
            pnlAddPersonInputFullName.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputFullName_Enter(object sender, EventArgs e)
        {
            pnlAddPersonInputFullName.BorderStyle = BorderStyle.FixedSingle;
            if (txtAddPersonInputFullName.Text == "Enter Full Name")
            {
                txtAddPersonInputFullName.Text = "";
            }
            txtAddPersonInputFullName.ForeColor = Color.Black;
        }
        private void pnlAddPersonInputPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputPhoneNumber.Text == "")
            {
                txtAddPersonInputPhoneNumber.Text = "Enter Phone Number";
                txtAddPersonInputPhoneNumber.ForeColor = Color.Gray;
            }
            pnlAddPersonInputPhoneNumber.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputPhoneNumber_Enter(object sender, EventArgs e)
        {
            pnlAddPersonInputPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
            if (txtAddPersonInputPhoneNumber.Text == "Enter Phone Number")
            {
                txtAddPersonInputPhoneNumber.Text = "";
            }
            txtAddPersonInputPhoneNumber.ForeColor = Color.Black;
        }
        private void pnlAddPersonInputAddress_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonInputAddress.Text == "")
            {
                txtAddPersonInputAddress.Text = "Enter Address";
                txtAddPersonInputAddress.ForeColor = Color.Gray;
            }
            pnlAddPersonInputAddress.BorderStyle = BorderStyle.None;
        }
        private void txtAddPersonInputAddress_Enter(object sender, EventArgs e)
        {
            if (txtAddPersonInputAddress.Text == "Enter Address")
            {
                txtAddPersonInputAddress.Text = "";
            }
            txtAddPersonInputAddress.ForeColor = Color.Black;
            pnlAddPersonInputAddress.BorderStyle = BorderStyle.FixedSingle;
        }
        private void txtAddPersonSearchBar_Enter(object sender, EventArgs e)
        {
            if (txtAddPersonSearchBar.Text == "Search by name or phone number ...")
            {
                txtAddPersonSearchBar.Text = "";
            }
            pnlAddPersonSearchBar.BorderStyle = BorderStyle.FixedSingle;
            txtAddPersonSearchBar.ForeColor = Color.Black;
        }
        private void pnlAddPersonSearchBar_Leave(object sender, EventArgs e)
        {
            if (txtAddPersonSearchBar.Text == "")
            {
                txtAddPersonSearchBar.Text = "Search by name or phone number ...";
                txtAddPersonSearchBar.ForeColor = Color.Gray;
            }
            pnlAddPersonSearchBar.BorderStyle = BorderStyle.None;
        }

        private void dataGridViewAddPerson_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
            if (e.RowIndex == -1)
            {
                switch (dataGridViewAddPerson.Columns[e.ColumnIndex].Name)
                {
                    case "colName":
                        DrawHeader(e, Properties.Resources.PersonIcon__2_, "Name");
                        break;
                    case "colPhoneNumber":
                        DrawHeader(e, Properties.Resources.phone, "Phone");
                        break;
                    case "colAddress":
                        DrawHeader(e, Properties.Resources.address_location, "Address");
                        break;
                    case "colAction":
                        DrawHeader(e, Properties.Resources.Action, "Action");
                        break;
                    case "colSL":
                        DrawHeader(e, Properties.Resources.SL, "SL");
                        break;
                }
                return;
            }

            if (e.RowIndex >= 0)
            {
               
                if (e.ColumnIndex == dataGridViewAddPerson.Columns["colAction"].Index)
                {
                    e.PaintBackground(e.CellBounds, true);

                    Image img = Properties.Resources.pen__1_;
                    int iconSize = 23;
                    int x = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    e.Graphics.DrawImage(img, new Rectangle(x, y, iconSize, iconSize));
                }
                else
                {
                    e.Paint(e.CellBounds, e.PaintParts & ~DataGridViewPaintParts.Focus);
                }


                int lineY = e.CellBounds.Y + e.CellBounds.Height - 1;
                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawLine(pen,
                        e.CellBounds.Left,
                        lineY,
                        e.CellBounds.Right,
                        lineY);
                }

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

            //first row selected
            if (dataGridViewAddPerson.Rows.Count > 0)
            {
                dataGridViewAddPerson.ClearSelection();
                dataGridViewAddPerson.CurrentCell =
                    dataGridViewAddPerson.Rows[0].Cells["colName"];
                dataGridViewAddPerson.Rows[0].Selected = true;
            }

            
        }

        private void dataGridViewAddPerson_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridViewAddPerson.Columns["colAction"].Index)
            {
                EditPersons editPerson = new EditPersons(this);

                editPerson.personID = Convert.ToInt32(dataGridViewAddPerson.Rows[e.RowIndex].Cells["colPersonID"].Value);
                editPerson.PersonName = dataGridViewAddPerson.Rows[e.RowIndex].Cells["colName"].Value.ToString();
                editPerson.PhoneNumber = dataGridViewAddPerson.Rows[e.RowIndex].Cells["colPhoneNumber"].Value.ToString();
                editPerson.Address = dataGridViewAddPerson.Rows[e.RowIndex].Cells["colAddress"].Value.ToString();

                editPerson.Show();          // Opens the form

                LoadData();
            }
        }

        private void btnAddPersonInputClear_Click(object sender, EventArgs e)
        {
            txtAddPersonInputFullName.Text = "Enter Full Name";
            txtAddPersonInputPhoneNumber.Text = "Enter Phone Number";
            txtAddPersonInputAddress.Text = "Enter Address";
            txtAddPersonInputFullName.ForeColor = Color.Gray;
            txtAddPersonInputPhoneNumber.ForeColor = Color.Gray;
            txtAddPersonInputAddress.ForeColor = Color.Gray;
        }

        private void btnAddPersonInputSavePerson_Click(object sender, EventArgs e)
        {
            //Clear All Previous Validation
            errorProvider1.Clear();

            PersonUI personUI = new PersonUI();

            personUI.userId = Session.LogedInUser.GetUserId();
            personUI.personId = -1;
            personUI.personName = (txtAddPersonInputFullName.Text == "Enter Full Name")? "" : txtAddPersonInputFullName.Text;
            personUI.personNumber = (txtAddPersonInputPhoneNumber.Text == "Enter Phone Number") ? "" : txtAddPersonInputPhoneNumber.Text;
            personUI.address = (txtAddPersonInputAddress.Text == "Enter Address") ? "" : txtAddPersonInputAddress.Text;

            CommonValidator.ValidationResult result = personUI.InsertDataIntoPersonUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Person Details Save Successfully");
                    //LoadData();
                     LastAddedPersonName = txtAddPersonInputFullName.Text.Trim();
                    this.DialogResult = DialogResult.OK;
    
                     LoadData();
                     
                    break;

                case CommonValidator.ValidationResult.PersonNameEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddPersonInputFullName);
                    break;

                case CommonValidator.ValidationResult.PersonNameInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddPersonInputFullName);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddPersonInputPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.PhoneInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddPersonInputPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberAlreadyExists:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddPersonInputPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Person Not Saved");
                    break;
            }
        }

        private void StylePersonGrid()
        {
           
            colPersonID.DataPropertyName = "PersonID";
            colName.DataPropertyName = "PersonName";
            colPhoneNumber.DataPropertyName = "PhoneNumber";
            colAddress.DataPropertyName = "Address";

            //Column Style
            dataGridViewAddPerson.AllowUserToOrderColumns = false;
            dataGridViewAddPerson.AutoGenerateColumns = false;
            dataGridViewAddPerson.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dataGridViewAddPerson.EnableHeadersVisualStyles = false;
            dataGridViewAddPerson.ColumnHeadersHeight = 45;
            dataGridViewAddPerson.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewAddPerson.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dataGridViewAddPerson.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewAddPerson.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dataGridViewAddPerson.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dataGridViewAddPerson.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

           
            
            colAction.DefaultCellStyle.BackColor = Color.White;
            colAddress.DefaultCellStyle.BackColor = Color.White;
            colPersonID.DefaultCellStyle.BackColor = Color.White;
            colPhoneNumber.DefaultCellStyle.BackColor = Color.White;
            colName.DefaultCellStyle.BackColor = Color.White;
            colSL.DefaultCellStyle.BackColor = Color.White;
            
            colAction.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colAddress.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPersonID.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPhoneNumber.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            colSL.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colName.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            //Row Style
            dataGridViewAddPerson.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dataGridViewAddPerson.DefaultCellStyle.BackColor = Color.White;
            dataGridViewAddPerson.DefaultCellStyle.ForeColor = Color.Black;
            //dataGridViewAddPerson.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dataGridViewAddPerson.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            //dataGridViewAddPerson.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewAddPerson.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewAddPerson.RowTemplate.Height = 40;
            dataGridViewAddPerson.RowHeadersVisible = false;
            dataGridViewAddPerson.MultiSelect = false;
            dataGridViewAddPerson.ReadOnly = true;
            dataGridViewAddPerson.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Border style
            dataGridViewAddPerson.BorderStyle = BorderStyle.None;
           dataGridViewAddPerson.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           dataGridViewAddPerson.GridColor = Color.FromArgb(230, 230, 230);
          

            //cell Alignment
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPersonID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPhoneNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSL.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSL.Resizable = DataGridViewTriState.False;
            colAction.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAction.Resizable = DataGridViewTriState.False;
        }

        private void DrawHeader(DataGridViewCellPaintingEventArgs e, Image icon, string text)
        {
            e.Paint(e.CellBounds,
                DataGridViewPaintParts.Background |
                DataGridViewPaintParts.Border);

            int iconSize = 16;
            int spacing = 6;

            SizeF textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);

            int totalWidth = iconSize + spacing + (int)textSize.Width;

            int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
            int iconY = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(icon, startX, iconY, iconSize, iconSize);

            using (Brush brush = new SolidBrush(Color.FromArgb(80, 60, 180)))
            {
                e.Graphics.DrawString(
                    text,
                    e.CellStyle.Font,
                    brush,
                    startX + iconSize + spacing,
                    e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2);
            }

            e.Handled = true;
        }

        private void pnlAddPersonInputFullName_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlAddPersonInput.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }
        private void pnlAddPersonInputPhoneNumber_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlAddPersonInput.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }
        private void pnlAddPersonInputAddress_Paint(object sender, PaintEventArgs e)
        {

            ControlPaint.DrawBorder(
                e.Graphics,
                pnlAddPersonInput.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }
        private void pnlAddPersonSearchBar_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlAddPersonInput.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void btnAddPersonInputClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnAddPersonInputClear, 5);
        }
        private void btnAddPersonInputSavePerson_Resize(object sender, EventArgs e)
        {
            SetRadius(btnAddPersonInputSavePerson, 5);
        }
        private void AddPersonControls_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void txtAddPersonSearchBar_TextChanged(object sender, EventArgs e)
        {
            if (txtAddPersonSearchBar.Text == "Search by name or phone number ...") return;
            DataTable filtered = Common.CommonUiFunction.SearchDataInPersons(masterData, txtAddPersonSearchBar);
            if (filtered != null)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = filtered;
                dataGridViewAddPerson.DataSource = bs;
                Common.CommonUiFunction.HighlightSearch(dataGridViewAddPerson, txtAddPersonSearchBar);
            }
        }
        private void txtAddPersonInputFullName_TextChanged(object sender, EventArgs e)
        {
            if (txtAddPersonInputFullName.Text != "Enter Full Name" && !string.IsNullOrWhiteSpace(txtAddPersonInputFullName.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddPersonInputFullName);
            }
        }
        private void txtAddPersonInputPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtAddPersonInputPhoneNumber.Text != "Enter Phone Number" && !string.IsNullOrWhiteSpace(txtAddPersonInputPhoneNumber.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddPersonInputPhoneNumber);
            }
        }
        private void txtAddPersonInputAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtAddPersonInputAddress.Text != "Enter Address" && !string.IsNullOrWhiteSpace(txtAddPersonInputAddress.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddPersonInputAddress);
            }
        }

        private void brnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (masterData == null || masterData.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save Person Excel File";
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName =
                    "Person_" +
                    DateTime.Now.ToString("ddMMyyyy_HHmmss") +
                    ".xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                var progress = new ExportProgressHelper();
                progress.Show(this, "Exporting Person Data...");

                try
                {
                    progress.SetProgress(10);

                    ExportPersonToExcel(
                        masterData,
                        saveDialog.FileName);

                    progress.SetProgress(100);
                }
                catch (Exception ex)
                {
                    progress.Close();
                    MessageBox.Show(
                        "Export failed.\n\n" + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void ExportPersonToExcel(DataTable dataTable, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add(
                    Excel.XlWBATemplate.xlWBATWorksheet);

                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Persons";

                // Column Names
                for (int col = 0;
                     col < dataTable.Columns.Count;
                     col++)
                {
                    worksheet.Cells[1, col + 1] =
                        GetPersonExportColumnName(
                            dataTable.Columns[col].ColumnName);
                }

                // Data
                for (int row = 0;
                     row < dataTable.Rows.Count;
                     row++)
                {
                    for (int col = 0;
                         col < dataTable.Columns.Count;
                         col++)
                    {
                        if (dataTable.Rows[row][col] != DBNull.Value)
                        {
                            worksheet.Cells[row + 2, col + 1] =
                                dataTable.Rows[row][col].ToString();
                        }
                    }
                }

                // Header bold
                Excel.Range headerRange =
                    worksheet.Range[
                        worksheet.Cells[1, 1],
                        worksheet.Cells[
                            1,
                            dataTable.Columns.Count]];

                headerRange.Font.Bold = true;

                // Auto fit columns
                worksheet.Columns.AutoFit();

                // SAVE EXCEL FILE
                workbook.SaveAs(
                    filePath,
                    Excel.XlFileFormat.xlOpenXMLWorkbook);

                // Close Excel without opening it
                workbook.Close(false);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel export failed.\n\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (workbook != null)
                {
                    try
                    {
                        workbook.Close(false);
                    }
                    catch { }
                }

                if (excelApp != null)
                {
                    try
                    {
                        excelApp.Quit();
                    }
                    catch { }
                }
            }
            finally
            {
                // Release COM objects
                if (worksheet != null)
                    Marshal.ReleaseComObject(worksheet);

                if (workbook != null)
                    Marshal.ReleaseComObject(workbook);

                if (excelApp != null)
                    Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private string GetPersonExportColumnName(string columnName)
        {
            switch (columnName)
            {
            
                case "PersonID":
                    return "SL";

                case "PersonName":
                    return "Person Name";

                case "PhoneNumber":
                    return "Phone Number";

                case "Address":
                    return "Address";

                default:
                    return columnName;
            }
        }

        
    }
}
