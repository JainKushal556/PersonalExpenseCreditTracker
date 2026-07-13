using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Modules.Expense;
using PersonalExpenseCreditTracker.Modules.Dashboard;
using PersonalExpenseCreditTracker.Modules.Lent;
using PersonalExpenseCreditTracker.Modules.Credit;
using PersonalExpenseCreditTracker.Modules.Borrow;
namespace PersonalExpenseCreditTracker
{
    public partial class MainForm : Form
    {
        private ExpenseControl expenseControl;
        private DashboardControl dashboardControl;
        private LentControls lentControl;
        private CreditControl creditControl;
        private BorrowControls borrowControls;
        private bool expenseOpen = false;
        private bool creditOpen = false;
        private bool lentOpen = false;
        private bool borrowOpen = false;
        private bool taskOpen = false;
        private bool notesOpen = false;
         private bool settingOpen = false;
         private Panel activePanel = null;
         private bool isSidebarExpanded = true;
         private const int SidebarExpandedWidth = 300;
         private const int SidebarCollapsedWidth = 70;
         //private Timer tmSidebar = new Timer();
        public MainForm()
        {
            InitializeComponent();
            pnlExpenseDropDown.Visible = false;
            tmSidebar.Interval = 15;
            tmSidebar.Tick += tmSidebar_Tick;
        }

        private void lblPersonalExpense_Click(object sender, EventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
          
            pnlOverview.Visible = true;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlDashboard);

            if (dashboardControl == null || dashboardControl.IsDisposed)
            {
                dashboardControl = new DashboardControl();

                dashboardControl.TopLevel = false;
                dashboardControl.FormBorderStyle = FormBorderStyle.None;
                dashboardControl.Dock = DockStyle.Fill;

                pnlOverview.Controls.Clear();
                pnlOverview.Controls.Add(dashboardControl);

                dashboardControl.Show();
            }
          
            this.MinimumSize = new Size(1200, 700);
        
        }

        private void CloseAllDropDown()
        {
            pnlExpenseDropDown.Visible = false;
            pnlCreditDropDown.Visible = false;
            pnlLentDropDown.Visible = false;
            pnlBorrowDropDown.Visible = false;
            pnlTaskDropDown.Visible = false;
            pnlNotesDropDown.Visible = false;
            pnlSettingsDropDown.Visible = false;
           
            expenseOpen = false;
            creditOpen = false;
            lentOpen = false;
            borrowOpen = false;
            taskOpen = false;
            notesOpen = false;
            settingOpen = false;

            picExpenseArrow.Image = Properties.Resources.down;
            picCreditArrow.Image = Properties.Resources.down;
            picLentArrow.Image = Properties.Resources.down;
            picBorrowArrow.Image = Properties.Resources.down;
            picTasksArrow.Image = Properties.Resources.down;
            picNotesArrow.Image = Properties.Resources.down;
            picSettingsArrow.Image = Properties.Resources.down;
        }

    //SetActive colour
        private void SetActiveMenu(Panel panel)
        {
            if (activePanel != null)
            {
                activePanel.BackColor = Color.FromArgb(15, 23, 42);

                ChangeMenuIcon(activePanel, false);
            }

            panel.BackColor = Color.FromArgb(59, 130, 246);

          
            ChangeMenuIcon(panel, true);

            activePanel = panel;
        }

    //Change Icone
        private void ChangeMenuIcon(Panel panel, bool active)
        {
            if (panel == pnlDashboard)
                picDashboard.Image = active ? Properties.Resources.home_white : Properties.Resources.home7;

            else if (panel == pnlExpense)
            {
                picExpense.Image = active ? Properties.Resources.wallet_filled_money_tool : Properties.Resources.wallet_filled_money_tool1;
            
            }

            else if (panel == pnlCredit)
                picCredit.Image = active ? Properties.Resources.credit_card_white : Properties.Resources.credit_card;

            else if (panel == pnlLent)
                picLent.Image = active ? Properties.Resources.payment_white : Properties.Resources.payment_lent1;

            else if (panel == pnlBorrow)
                picBorrow.Image = active ? Properties.Resources.borrowing_white : Properties.Resources.borrowing;

            else if (panel == pnlTasks)
                picTasks.Image = active ? Properties.Resources.task__white : Properties.Resources.task;

            else if (panel == pnlNotes)
                picNotes.Image = active ? Properties.Resources.pencil__2_ : Properties.Resources.pencil;

            else if (panel == pnlSettings)
                picSettings.Image = active ? Properties.Resources.cogwheel__1_ : Properties.Resources.cogwheel;
            else if (panel == pnlUserProfile)
                picUserProfile.Image = active ? Properties.Resources.user : Properties.Resources.user;
        }
     //pnlDashboard Function
        private void pnlDashboard_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Dashboard";
            lblSubtitle.Text = "Welcome back! Here's your financial overview.";

            SetActiveMenu(pnlDashboard);
            CloseAllDropDown();

            if (dashboardControl == null || dashboardControl.IsDisposed)
            {
                dashboardControl = new DashboardControl();

                dashboardControl.TopLevel = false;
                dashboardControl.FormBorderStyle = FormBorderStyle.None;
                dashboardControl.Dock = DockStyle.Fill;

                pnlOverview.Controls.Clear();
                pnlOverview.Controls.Add(dashboardControl);

                dashboardControl.Show();
            }

            pnlOverview.Visible = true;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;
            pnlTop.Visible = true;
            ExpandSidebar();
        }

        private void pnlDashboard_MouseEnter(object sender, EventArgs e)
        {
            pnlDashboard.BackColor = Color.FromArgb(59, 130, 246);
            picDashboard.Image = Properties.Resources.home_white;

        }

        private void pnlDashboard_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlDashboard)
            {
                pnlDashboard.BackColor = Color.FromArgb(15, 23, 42);
                picDashboard.Image = Properties.Resources.home7;
            }
        }


     //pnlExpense Function
        private void pnlExpense_MouseEnter(object sender, EventArgs e)
        {
            pnlExpense.BackColor = Color.FromArgb(59, 130, 246);
            picExpense.Image = Properties.Resources.wallet_filled_money_tool;
        }

        private void pnlExpense_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlExpense)
            {
                pnlExpense.BackColor = Color.FromArgb(15, 23, 42);
                picExpense.Image = Properties.Resources.wallet_filled_money_tool1;
            }
        }

        //private void pnlExpense_Click(object sender, EventArgs e)
        //{
        //    ExpenseControl expenseControl = new ExpenseControl();

        //    expenseControl.TopLevel = false;
        //    expenseControl.FormBorderStyle = FormBorderStyle.None;
        //    expenseControl.Dock = DockStyle.Fill;
        //    expenseControl.Location = new Point(0, 0);

        //    pnlExpensePage.Controls.Clear();
        //    pnlExpensePage.Controls.Add(expenseControl);

        //    expenseControl.BringToFront();
        //    expenseControl.Show();
            

        //    pnlOverview.Visible = false;
        //    pnlExpensePage.Visible = true;
        //    pnlCreditPage.Visible = false;
        //    pnlLentPage.Visible = false;
        //    pnlBorrowPage.Visible = false;
        //    pnlTaskPage.Visible = false;
        //    pnlNotesPage.Visible = false;
        //    pnlSettingPage.Visible = false;
        //    pnlProfilePage.Visible = false;
        //    SetActiveMenu(pnlExpense);
        //    bool wasOpen = expenseOpen;

        //    CloseAllDropDown();

        //    if (!wasOpen)
        //    {
        //        pnlExpenseDropDown.Visible = true;
        //        picExpenseArrow.Image = Properties.Resources.arrowhead_up;
        //        pnlTop.Visible = true;
        //        expenseOpen = true;
        //    }
        //    else
        //    {
        //        pnlExpenseDropDown.Visible = false;
        //        picExpenseArrow.Image = Properties.Resources.down;

        //        expenseOpen = false;
        //    }

        //    ExpandSidebar();
        //}

       private void pnlExpense_Click(object sender, EventArgs e)
    {
    lblTitle.Text = "Expense";
    lblSubtitle.Text = "Track and manage your expenses";
    lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);

    if (expenseControl == null || expenseControl.IsDisposed)
    {
        expenseControl = new ExpenseControl();

        expenseControl.TopLevel = false;
        expenseControl.FormBorderStyle = FormBorderStyle.None;
        expenseControl.Dock = DockStyle.Fill;

        pnlExpensePage.Controls.Clear();
        pnlExpensePage.Controls.Add(expenseControl);

        expenseControl.Show();
    }

    pnlOverview.Visible = false;
    pnlExpensePage.Visible = true;
    pnlCreditPage.Visible = false;
    pnlLentPage.Visible = false;
    pnlBorrowPage.Visible = false;
    pnlTaskPage.Visible = false;
    pnlNotesPage.Visible = false;
    pnlSettingPage.Visible = false;
    pnlProfilePage.Visible = false;

    SetActiveMenu(pnlExpense);

    bool wasOpen = expenseOpen;

    CloseAllDropDown();

    if (!wasOpen)
    {
        pnlExpenseDropDown.Visible = true;
        picExpenseArrow.Image = Properties.Resources.arrowhead_up;
        pnlTop.Visible = true;
        expenseOpen = true;
    }
    else
    {
        pnlExpenseDropDown.Visible = false;
        picExpenseArrow.Image = Properties.Resources.down;
        expenseOpen = false;
    }

    ExpandSidebar();
}


   //pnlCredit Function
        private void pnlCredit_MouseEnter(object sender, EventArgs e)
        {
            pnlCredit.BackColor = Color.FromArgb(59, 130, 246);
            picCredit.Image = Properties.Resources.credit_card_white;
        }

        private void pnlCredit_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlCredit)
            {
                pnlCredit.BackColor = Color.FromArgb(15, 23, 42);
                picCredit.Image = Properties.Resources.credit_card;
            }
        }

        private void pnlCredit_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Credit";
            lblSubtitle.Text = "Track and manage your credit transactions";
            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);

            if (creditControl == null || creditControl.IsDisposed)
            {
                creditControl = new CreditControl();

                creditControl.TopLevel = false;
                creditControl.FormBorderStyle = FormBorderStyle.None;
                creditControl.Dock = DockStyle.Fill;

                pnlCreditPage.Controls.Clear();
                pnlCreditPage.Controls.Add(creditControl);

                creditControl.Show();

            }

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = true;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;
            SetActiveMenu(pnlCredit);
            bool wasOpen = creditOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlCreditDropDown.Visible = true;
                creditOpen = true;
                picCreditArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
            }
            else
            {
                pnlCreditDropDown.Visible = false;
                creditOpen = false;
                picCreditArrow.Image = Properties.Resources.down;
            }
            ExpandSidebar();
        }

  //pnlLent Function

        private void pnlLent_MouseEnter(object sender, EventArgs e)
        {
            pnlLent.BackColor = Color.FromArgb(59, 130, 246);
            picLent.Image = Properties.Resources.payment_white;
        }

        private void pnlLent_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlLent)
            {
                pnlLent.BackColor = Color.FromArgb(15, 23, 42);
                picLent.Image = Properties.Resources.payment_lent1;
            }
        }

        private void pnlLent_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Lent";
            lblSubtitle.Text = "Track and manage money you have lent to others";
            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);
            if (lentControl == null || lentControl.IsDisposed)
            {
                lentControl = new LentControls();

                lentControl.TopLevel = false;
                lentControl.FormBorderStyle = FormBorderStyle.None;
                lentControl.Dock = DockStyle.Fill;
                pnlLentPage.Controls.Clear();
                pnlLentPage.Controls.Add(lentControl);
                lentControl.Show();

            }



            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = true;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlLent);
            bool wasOpen = lentOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlLentDropDown.Visible = true;
                picLentArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                lentOpen = true;
            }
            else
            {
                pnlLentDropDown.Visible = false;
                picLentArrow.Image = Properties.Resources.down;
                lentOpen = false;
            }

            ExpandSidebar();
        }

  //pnlBorrow Function
        private void pnlBorrow_MouseEnter(object sender, EventArgs e)
        {
            pnlBorrow.BackColor = Color.FromArgb(59, 130, 246);
            picBorrow.Image = Properties.Resources.borrowing_white;
        }

        private void pnlBorrow_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlBorrow)
            {
                pnlBorrow.BackColor = Color.FromArgb(15, 23, 42);
                picBorrow.Image = Properties.Resources.borrowing;
            }
        }

        private void pnlBorrow_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Borrow";
            lblSubtitle.Text = "Track and manage money you have borrowed from others";
            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);
            if (borrowControls == null || borrowControls.IsDisposed)
            {
                borrowControls = new BorrowControls();

                borrowControls.TopLevel = false;
                borrowControls.FormBorderStyle = FormBorderStyle.None;
                borrowControls.Dock = DockStyle.Fill;

                pnlBorrowPage.Controls.Clear();
                pnlBorrowPage.Controls.Add(borrowControls);

                borrowControls.Show();

            }

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = true;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlBorrow);
            bool wasOpen = borrowOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlBorrowDropDown.Visible = true;
                picBorrowArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                borrowOpen = true;
            }
            else
            {
                pnlBorrowDropDown.Visible = false;
                picBorrowArrow.Image = Properties.Resources.down;
                borrowOpen = false;
            }

            ExpandSidebar();
        }

 // pnlTasks Function
        private void pnlTasks_MouseEnter(object sender, EventArgs e)
        {
            pnlTasks.BackColor = Color.FromArgb(59, 130, 246);
            picTasks.Image = Properties.Resources.task__white;
        }

        private void pnlTasks_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlTasks)
            {
                pnlTasks.BackColor = Color.FromArgb(15, 23, 42);
                picTasks.Image = Properties.Resources.task;
            }
        }

        private void pnlTasks_Click(object sender, EventArgs e)
        {

            lblTitle.Text = "Tasks";
            lblSubtitle.Text = "Organize and track your tasks efficiently";
            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = true;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlTasks);
            bool wasOpen = taskOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlTaskDropDown.Visible = true;
                picTasksArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                taskOpen = true;
            }
            else
            {
                pnlTaskDropDown.Visible = false;
                picTasksArrow.Image = Properties.Resources.down;
                taskOpen = false;
            }

            ExpandSidebar();
        }


     //Notes Function 
        private void pnlNotes_MouseEnter(object sender, EventArgs e)
        {
            pnlNotes.BackColor = Color.FromArgb(59, 130, 246);
            picNotes.Image = Properties.Resources.pencil__2_;
        }

        private void pnlNotes_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlNotes)
            {
                pnlNotes.BackColor = Color.FromArgb(15, 23, 42);
                picNotes.Image = Properties.Resources.pencil;
            }
        }

        private void pnlNotes_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Notes";
            lblSubtitle.Text = "Capture your thoughts and keep everything organized";
            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = true;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlNotes);
            bool wasOpen = notesOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlNotesDropDown.Visible = true;
                picNotesArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                notesOpen = true;
            }
            else
            {
                pnlNotesDropDown.Visible = false;
                picNotesArrow.Image = Properties.Resources.down;
                notesOpen = false;
            }
            ExpandSidebar();
        }
    //Setting Function
        private void pnlSettings_MouseEnter(object sender, EventArgs e)
        {
            pnlSettings.BackColor = Color.FromArgb(59, 130, 246);
            picSettings.Image = Properties.Resources.cogwheel__1_;
        }

        private void pnlSettings_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlSettings)
            {
                pnlSettings.BackColor = Color.FromArgb(15, 23, 42);
                picSettings.Image = Properties.Resources.cogwheel;
            }
        }

        private void pnlSettings_Click(object sender, EventArgs e)
        {
            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = true;
            pnlProfilePage.Visible = false;

            SetActiveMenu(pnlSettings);
            bool wasOpen = settingOpen;

            CloseAllDropDown();
            if (!wasOpen)
            {
                pnlSettingsDropDown.Visible = true;
                picSettingsArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = false;
                settingOpen = true;
            }
            else
            {
                pnlSettingsDropDown.Visible = false;
                picSettingsArrow.Image = Properties.Resources.down;
                settingOpen = false;
            }
            ExpandSidebar();
        }

       
   //UserProfileFunction
        private void pnlUserProfile_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlUserProfile)
            {
                pnlUserProfile.BackColor = Color.FromArgb(59, 130, 246);
            }
        }

        private void pnlUserProfile_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlUserProfile)
            {
                pnlUserProfile.BackColor = Color.FromArgb(15, 23, 42);
            }
        }

        private void pnlUserProfile_Click(object sender, EventArgs e)
        {
            SetActiveMenu(pnlUserProfile);

            CloseAllDropDown();

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = true;

            ExpandSidebar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tmSidebar.Start();
        }
   //Side Bar increase and Decrease
        private void tmSidebar_Tick(object sender, EventArgs e)
        {
            if (isSidebarExpanded)
            {

                pnlSideBar.Width -= 20;
                if (pnlSideBar.Width <= 180)
                {
                    lblPersonalExpense.Visible = false;
                    lblManager.Visible = false;
                    lblDashboard.Visible = false;
                    lblExpense.Visible = false;
                    lblCredit.Visible = false;
                    lblLent.Visible = false;
                    lblBorrow.Visible = false;
                    lblTasks.Visible = false;
                    lblNotes.Visible = false;
                    lblSettings.Visible = false;

                    picExpenseArrow.Visible = false;
                    picCreditArrow.Visible = false;
                    picLentArrow.Visible = false;
                    picBorrowArrow.Visible = false;
                    picTasksArrow.Visible = false;
                    picNotesArrow.Visible = false;
                    picSettingsArrow.Visible = false;

                    lblUserName.Visible = false;
                    lblEmail.Visible = false;
                }
                if (pnlSideBar.Width < 80)
                {
                    CloseAllDropDown();
                    picExpenseArrow.Image = Properties.Resources.down;
                    picCreditArrow.Image = Properties.Resources.down;
                    picLentArrow.Image = Properties.Resources.down;
                    picBorrowArrow.Image = Properties.Resources.down;
                    picTasksArrow.Image = Properties.Resources.down;
                    picNotesArrow.Image = Properties.Resources.down;
                    picSettingsArrow.Image = Properties.Resources.down;
                    CenterIcons(true);

                    //flowSidebar.PerformLayout();
                    //flowSidebar.Refresh();
                    flowSidebar.AutoScroll = false;
                    tmSidebar.Stop();
                    isSidebarExpanded = false;
                }
               
            }
            else
            {
                pnlSideBar.Width += 20;
                CenterIcons(false);
                if (pnlSideBar.Width >= 80)
                {
                    lblPersonalExpense.Visible = true;
                    lblManager.Visible = true;
                    lblDashboard.Visible = true;
                    lblExpense.Visible = true;
                    lblCredit.Visible = true;
                    lblLent.Visible = true;
                    lblBorrow.Visible = true;
                    lblTasks.Visible = true;
                    lblNotes.Visible = true;
                    lblSettings.Visible = true;

                    picExpenseArrow.Visible = true;
                    picCreditArrow.Visible = true;
                    picLentArrow.Visible = true;
                    picBorrowArrow.Visible = true;
                    picTasksArrow.Visible = true;
                    picNotesArrow.Visible = true;
                    picSettingsArrow.Visible = true;
                    lblUserName.Visible = true;
                    lblEmail.Visible = true;
                    flowSidebar.AutoScroll = true; 
                }
                if (pnlSideBar.Width >= 300)
                {
                    pnlSideBar.Width = 300;

                    CenterIcons(false);
                    //flowSidebar.PerformLayout();
                    //flowSidebar.Refresh();

                    tmSidebar.Stop();
                    
                    isSidebarExpanded = true;
                }
            }
        }

   //Expende and Collapse time icone location
        private void CenterIcons(bool collapse)
        {
            if (collapse)
            {
                //CenterIcon(pictureBox1);

                CenterIcon(picDashboard);
                CenterIcon(picExpense);
                CenterIcon(picCredit);
                CenterIcon(picLent);
                CenterIcon(picBorrow);
                CenterIcon(picTasks);
                CenterIcon(picNotes);
                CenterIcon(picSettings);
                CenterIcon(picUserProfile);
            }
            else
            {
                picDashboard.Left = 20;
                picExpense.Left = 20;
                picCredit.Left = 20;
                picLent.Left = 20;
                picBorrow.Left = 20;
                picTasks.Left = 20;
                picNotes.Left = 20;
                picSettings.Left = 20;
                picUserProfile.Left = 20;

                pictureBox1.Left = 15;

               
            }
        }
        private void ExpandSidebar()
        {
            if (!isSidebarExpanded)
            {
                tmSidebar.Start();
            }
        }

        private void CenterIcon(PictureBox pic)
        {
            pic.Left = (pnlSideBar.Width - pic.Width) / 2;
        }

        private void pnlProfilePage_Paint(object sender, PaintEventArgs e)
        {

        }



     
        
       
    }
}

