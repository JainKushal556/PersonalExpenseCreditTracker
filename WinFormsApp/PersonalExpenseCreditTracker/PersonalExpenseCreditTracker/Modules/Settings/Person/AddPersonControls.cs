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
            int userID = 11;
            SetRadius(pnlAddPersonDataGridView, 15);
            SetRadius(pnlAddPersonInput, 15);
            SetRadius(pnlIdia, 15);
            //SetRadius(btnAddPersonInputSavePerson, 10);
            //SetRadius(btnAddPersonInputClear, 10);

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
            DataSet dataset = GetDataSet(userID);

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

        private DataSet GetDataSet(int userID)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection sqlConnection = null;
            DataSet dataset = null;
            try
            {
                sqlConnection = new SqlConnection(CS);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetAllPersons", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@UserId", userID);
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

        //private void pnlAddPersonInputFullName_Resize(object sender, EventArgs e)
        //{
        //    SetRadius(pnlIdia, 15);
        //}
        //private void btnAddPersonInputSavePerson_Resize(object sender, EventArgs e)
        //{
        //    SetRadius(btnAddPersonInputSavePerson, 10);
        //}
        //private void btnAddPersonInputClear_Resize(object sender, EventArgs e)
        //{
        //    SetRadius(btnAddPersonInputClear, 10);
        //}
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

            // Header
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

            // Action column
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewAddPerson.Columns["colAction"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Image img = Properties.Resources.pen__1_;

                int iconSize = 23;

                int x = e.CellBounds.Left + (e.CellBounds.Width - 20) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - 20) / 2;

                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                e.Graphics.DrawImage(img, new Rectangle(x, y, iconSize, iconSize));

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

        //private void dataGridViewAddPerson_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
          
        //}
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

        private void txtAddPersonInputFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlAddPersonInputFullName_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlAddPersonInput.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void txtAddPersonInputPhoneNumber_TextChanged(object sender, EventArgs e)
        {

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

        //private void pnlAddPersonInput_Paint(object sender, PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(
        //        e.Graphics,
        //        pnlAddPersonInput.ClientRectangle,
        //        ColorTranslator.FromHtml("#E7ECF3"),
        //        ButtonBorderStyle.Solid);
        //}

        //private void panel2_Paint(object sender, PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(
        //       e.Graphics,
        //       panel2.ClientRectangle,
        //       ColorTranslator.FromHtml("#E7ECF3"),
        //       ButtonBorderStyle.Solid);
        //}


    }
}
