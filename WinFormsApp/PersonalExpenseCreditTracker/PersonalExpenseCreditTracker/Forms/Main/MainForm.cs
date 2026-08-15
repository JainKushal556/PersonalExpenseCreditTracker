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
using PersonalExpenseCreditTracker.Modules.Note;
using PersonalExpenseCreditTracker.Modules.Profile;
using PersonalExpenseCreditTracker.Modules.Settings;
using PersonalExpenseCreditTracker.Modules.Task;
using PersonalExpenseCreditTracker.Modules.Settings.Person;
using PersonalExpenseCreditTracker.Modules.Settings.Category;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
using PersonalExpenseCreditTracker.Forms.Main;
using BLLayer.Common;
using System.IO;
using System.Drawing.Drawing2D;
namespace PersonalExpenseCreditTracker
{
    public partial class MainForm : Form
    {
        private Panel lastOpenedPage = null;

        //private Panel activeSettingSubMenu = null;
        private Panel previousSettingSubMenu = null; // <-- এই লাইনটি যোগ করুন


        // Sidebar Smooth Scroll er Target Position
        private int targetTop = 0;

        // Smooth Scroll Animation er jano Timer

        private Timer tmScroll = new Timer();

        //Contant page Load korer jano use kora hoye

        private ExpenseControl expenseControl;
        private DashboardControl dashboardControl;
        private LentControls lentControl;
        private CreditControl creditControl;
        private BorrowControls borrowControls;
        private ProfileControls profileControl;
        private NoteControl noteControl;
        private TaskControls taskControl;
        private AddPersonControls addPersonSControls;
        private ExpenseCategoryControls expenseCategoryControls;
        private CreditCategoryControls creditCategoryControls;
        // Dropdown Open/Close Status
        //Kon Menu present open ache ta Track korer jano

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

        private Panel activeExpenseSubMenu = null;
        private Panel activeCreditSubMenu = null;
        private Panel activeLentSubMenu = null;
        private Panel activeBorrowSubMenu = null;
        private Panel activeTaskSubMenu = null;
        private Panel activeNoteSubMenu = null;
        private Panel activeSettingSubMenu = null;

        private bool filterOpen = false;
        private bool dateOpen = false;
        private bool amountOpen = false;
        private bool categoryOpen = false;

        private bool creditFilterOpen = false;
        private bool creditDateOpen = false;
        private bool creditAmountOpen = false;
        private bool creditCategoryOpen = false;

        private bool lentFilterOpen = false;
        private bool lentDateOpen = false;
        private bool lentAmountOpen = false;
        private bool lentPersonOpen = false;
        private bool lentStatusOpen = false;
        private bool lentPaymentOpen = false;

        private bool borrowFilterOpen = false;
        private bool borrowDateOpen = false;
        private bool borrowAmountOpen = false;
        private bool borrowPersonOpen = false;
        private bool borrowStatusOpen = false;
        private bool borrowPaymentOpen = false;

        private bool taskFilterOpen = false;
        private bool taskDateOpen = false;
        private bool taskStatusOpen = false;
        private bool taskPriorityOpen = false;

        private bool noteFilterOpen = false;
        private bool noteDateOpen = false;
        private bool noteStatusOpen = false;
        private bool notePriorityOpen = false;

        public MainForm()
        {

            InitializeComponent();

            tmScroll.Interval = 10;
            tmScroll.Tick += tmScroll_Tick;

            pnlExpenseDropDown.Visible = false;
            tmSidebar.Interval = 15;
            tmSidebar.Tick += tmSidebar_Tick;

            pnlFilterContent.Visible = false;
           
            pnlCreditFilterContant.Visible = false;

            pnlLentFilterContant.Visible = false;
          
            pnlBorrowFilterContant.Visible = false;
          
            this.Resize += MainForm_Resize;

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; // 1 second
            timer1.Start();

            ShowCurrentDateTime();

            //lblDateTime.Text = DateTime.Now.ToString("dd MMM yyyy | hh:mm tt");
            CommonBllFunction.UpdateOverdueStatus();

            flowSidebar.Location = new Point(0, 0);
            flowSidebar.Width = pnlSideBar.ClientSize.Width;


            LoadSidebarUserProfile();
            MakeCircularPictureBox(picUserProfile);
            //ComboBoxCategory.SelectedIndex = 0;
            //cmbSubCategory.SelectedIndex = 0;

            //ComboBoxCreditCategory.SelectedIndex = 0;
            //ComboBoxCreditSubCategory.SelectedIndex = 0;

            //ComboBoxLentPerson.SelectedIndex = 0;
            //ComboBoxLentStatus.SelectedIndex = 0;
            //ComboBoxLentPayment.SelectedIndex = 0;

            //ComboBoxBorrowPerson.SelectedIndex = 0;
            //ComboBoxBorrowStatus.SelectedIndex = 0;
            //ComboBoxBorrowPayment.SelectedIndex = 0;

            //ComboBoxTaskStatus.SelectedIndex = 0;
            //ComboBoxTaskPriority.SelectedIndex = 0;

            //ComboBoxNoteStatus.SelectedIndex = 0;
            //ComboBoxNotePriority.SelectedIndex = 0;

           
      
            SetActiveMenu(pnlDashboard, true);

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

            ShowPage(pnlOverview);
            this.MinimumSize = new Size(1200, 700);

        }
        //Scroll Bar 
        // Mouse Wheel Scroll Control
        // Sidebar er upore Mouse Wheel ghorale Sidebar Scroll hobe

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;

            if (m.Msg == WM_MOUSEWHEEL)
            {
                Point mousePoint = PointToClient(Control.MousePosition);

                if (pnlSideBar.Bounds.Contains(mousePoint))
                {
                    int delta = (short)((m.WParam.ToInt32() >> 16) & 0xFFFF);

                    ScrollSidebar(delta);
                    return;
                }
            }

            base.WndProc(ref m);
        }
        // Sidebar Scroll Function
        // Mouse Wheel onujai Sidebar upore ba niche Scroll kore

        private void ScrollSidebar(int delta)
        {
            if (flowSidebar.Controls.Count == 0)
                return;

            int contentHeight = 0;

            foreach (Control c in flowSidebar.Controls)
            {
                if (c.Visible)
                    contentHeight = Math.Max(contentHeight, c.Bottom + c.Margin.Bottom);
            }

            if (contentHeight <= pnlSideBar.ClientSize.Height)
                return;

            int step = 60;

            if (delta > 0)
                targetTop += step;
            else
                targetTop -= step;

            int minTop = pnlSideBar.ClientSize.Height - contentHeight;

            if (targetTop > 0)
                targetTop = 0;

            if (targetTop < minTop)
                targetTop = minTop;

            tmScroll.Start();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            flowSidebar.Width = pnlSideBar.ClientSize.Width;

            RefreshSidebarScroll();
        }

        // Sidebar er Content Height onijai Scroll Position Update kore
        // jate Sidebar er Content upore ba niche otirikto Scroll na hoye

        private void RefreshSidebarScroll()
        {
            flowSidebar.PerformLayout();
            Application.DoEvents();

            int contentHeight = 0;

            foreach (Control c in flowSidebar.Controls)
            {
                if (c.Visible)
                {
                    contentHeight = Math.Max(contentHeight,
                                             c.Bottom + c.Margin.Bottom);
                }
            }

            int minTop = pnlSideBar.ClientSize.Height - contentHeight;

            if (minTop > 0)
            {
                flowSidebar.Top = 0;
            }
            else
            {
                if (flowSidebar.Top > 0)
                    flowSidebar.Top = 0;

                if (flowSidebar.Top < minTop)
                    flowSidebar.Top = minTop;
            }
        }

        // Smooth Scroll Animation
        // Timer choler time Sidebar slowly slowly Target Position e Move kore

        private void tmScroll_Tick(object sender, EventArgs e)
        {
            int speed = 6;

            if (flowSidebar.Top < targetTop)
            {
                flowSidebar.Top += speed;

                if (flowSidebar.Top > targetTop)
                    flowSidebar.Top = targetTop;
            }
            else if (flowSidebar.Top > targetTop)
            {
                flowSidebar.Top -= speed;

                if (flowSidebar.Top < targetTop)
                    flowSidebar.Top = targetTop;
            }

            if (flowSidebar.Top == targetTop)
                tmScroll.Stop();
        }

        // All Dropdown Close kore
        // New Menu Open Korer age ager Menu off kore dai
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
          



            // Reset Expense SubMenu
            if (activeExpenseSubMenu != null)
            {
                activeExpenseSubMenu.BackColor = Color.Transparent;
                activeExpenseSubMenu = null;
            }

            // Reset Credit SubMenu
            if (activeCreditSubMenu != null)
            {
                activeCreditSubMenu.BackColor = Color.Transparent;
                activeCreditSubMenu = null;
            }

            // Reset Lent SubMenu
            if (activeLentSubMenu != null)
            {
                activeLentSubMenu.BackColor = Color.Transparent;
                activeLentSubMenu = null;
            }

            // Reset Borrow SubMenu
            if (activeBorrowSubMenu != null)
            {
                activeBorrowSubMenu.BackColor = Color.Transparent;
                activeBorrowSubMenu = null;
            }

            // Reset Task SubMenu
            if (activeTaskSubMenu != null)
            {
                activeTaskSubMenu.BackColor = Color.Transparent;
                activeTaskSubMenu = null;
            }

            // Reset Note SubMenu
            if (activeNoteSubMenu != null)
            {
                activeNoteSubMenu.BackColor = Color.Transparent;
                activeNoteSubMenu = null;


            }

            // Reset Setting SubMenu
            if (activeSettingSubMenu != null)
            {
                activeSettingSubMenu.BackColor = Color.Transparent;
                activeSettingSubMenu = null;
            }

            flowSidebar.SuspendLayout();
            flowSidebar.ResumeLayout();
            flowSidebar.PerformLayout();

            flowSidebar.Top = 0;


        }


        private void ShowPage(Panel page)
        {
            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = false;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;
            pnlExpenseCategory.Visible = false;
            pnlCreditCategoryPage.Visible = false;

            pnlPersonAddPage.Visible = false;

            //pnlExpenseCategory.Visible = false;

            if (page != null)
            {
                page.Visible = true;
                lastOpenedPage = page;
                //MessageBox.Show(page.Name); 
            }
        }

        private void UpdateHeader(string title, string subtitle)
        {
            lblTitle.Text = title;
            lblSubtitle.Text = subtitle;

            lblSubtitle.Location = new Point(17, lblSubtitle.Location.Y);

            pnlTop.Visible = true;
        }

        // Active Menu Highlight kore
        private void SetActiveMenu(Panel panel, bool keepHighlight)
        {
            if (activePanel != null)
            {
                activePanel.BackColor = Color.FromArgb(15, 23, 42);
                ChangeMenuIcon(activePanel, false);
            }

            activePanel = panel;

            if (keepHighlight)
            {
                panel.BackColor = Color.FromArgb(59, 130, 246);
                ChangeMenuIcon(panel, true);
            }
        }

        // Active Menu Onujai Icon Change kore
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
            //else if (panel == pnlUserProfile)
                //picUserProfile.Image = active ? Properties.Resources.user : Properties.Resources.user;
        }
        //pnlDashboard all Function
        private void pnlDashboard_Click(object sender, EventArgs e)
        {
            UpdateHeader(
                     "Dashboard",
                    "Welcome back! Here's your financial overview.");

       
            SetActiveMenu(pnlDashboard, true);
            CloseAllDropDown();
            RefreshSidebarScroll();
            ExpandSidebar();

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

            dashboardControl.LoadDashboardSummary(Session.LogedInUser.GetUserId());

            ShowPage(pnlOverview);
            pnlTop.Visible = true;

            ExpandSidebar();

        }

        private void pnlDashboard_MouseEnter(object sender, EventArgs e)
        {
            //pnlDashboard.BackColor = Color.FromArgb(59, 130, 246);
            if (activePanel != pnlDashboard)
            {
                pnlDashboard.BackColor = Color.FromArgb(30, 41, 59);
                picDashboard.Image = Properties.Resources.home_white;
            }

        }

        private void pnlDashboard_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlDashboard)
            {
                pnlDashboard.BackColor = Color.FromArgb(15, 23, 42);
                picDashboard.Image = Properties.Resources.home7;
            }
        }


        private void pnlExpense_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlExpense || expenseOpen)
            {
                pnlExpense.BackColor = Color.FromArgb(30, 41, 59);
                picExpense.Image = Properties.Resources.wallet_filled_money_tool;
            }
        }

        private void pnlExpense_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlExpense || expenseOpen)
            {
                pnlExpense.BackColor = Color.FromArgb(15, 23, 42);
                picExpense.Image = Properties.Resources.wallet_filled_money_tool1;
            }
        }

        private void pnlExpense_Click(object sender, EventArgs e)
        {
            UpdateHeader(
                   "Expense",
                   "Track and manage your expenses");

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
            else if (expenseControl != null && !expenseControl.IsDisposed)
            {
                expenseControl.LoadExpenseData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlExpensePage);

            
            SetActiveMenu(pnlExpense, false);

            bool wasOpen = expenseOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
              
                pnlExpenseDropDown.Visible = true;
                picExpenseArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                expenseOpen = true;

                SetActiveExpenseSubMenu(pnlAllExpense);
            }
            else
            {
              
                pnlExpenseDropDown.Visible = false;
                picExpenseArrow.Image = Properties.Resources.down;
                expenseOpen = false;

                SetActiveMenu(pnlExpense, true);
            }

            RefreshSidebarScroll();
            ExpandSidebar();
            dateOpen = false;
            amountOpen = false;
        }


        public void OpenExpenseCategorySettings()
        {
            
            pnlSettingExpenseCategories_Click(null, null);
        }


        //pnlCredit all Function
        private void pnlCredit_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlCredit || creditOpen)
            {
                pnlCredit.BackColor = Color.FromArgb(30, 41, 59);
                picCredit.Image = Properties.Resources.credit_card_white;
            }
        }

        private void pnlCredit_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlCredit || creditOpen)
            {
                pnlCredit.BackColor = Color.FromArgb(15, 23, 42);
                picCredit.Image = Properties.Resources.credit_card;
            }
        }


        private void pnlCredit_Click(object sender, EventArgs e)
        {
            UpdateHeader(
             "Credit",
             "Track and manage your credit transactions");

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
            else if(creditControl != null && !creditControl.IsDisposed)
            {
                creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
            }
            ShowPage(pnlCreditPage);

          
            SetActiveMenu(pnlCredit, false);

            bool wasOpen = creditOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                
                pnlCreditDropDown.Visible = true;
                picCreditArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                creditOpen = true;

                SetActiveCreditSubMenu(pnlAllCredit);
            }
            else
            {
                
                pnlCreditDropDown.Visible = false;
                picCreditArrow.Image = Properties.Resources.down;
                creditOpen = false;

                SetActiveMenu(pnlCredit, true); 
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            creditDateOpen = false;
            creditAmountOpen = false;
            creditCategoryOpen = false;

            pnlCreditFilterContant.Visible = true;
        }


        //pnlLent all Function

        private void pnlLent_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlLent || lentOpen)
            {
                pnlLent.BackColor = Color.FromArgb(30, 41, 59);
                picLent.Image = Properties.Resources.payment_white;
            }
        }

        private void pnlLent_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlLent || lentOpen)
            {
                pnlLent.BackColor = Color.FromArgb(15, 23, 42);
                picLent.Image = Properties.Resources.payment_lent1;
            }
        }


        private void pnlLent_Click(object sender, EventArgs e)
        {
            UpdateHeader(
             "Lent",
             "Track and manage your lent transactions");

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

            ShowPage(pnlLentPage);

            bool wasOpen = lentOpen;

            SetActiveMenu(pnlLent, false); 
            CloseAllDropDown();

            if (!wasOpen)
            {
                
                pnlLentDropDown.Visible = true;
                lentOpen = true;
                picLentArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;

                SetActiveLentSubMenu(pnlAllLent);
            }
            else
            {
               
                pnlLentDropDown.Visible = false;
                lentOpen = false;
                picLentArrow.Image = Properties.Resources.down;

                SetActiveMenu(pnlLent, true); 
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            lentDateOpen = false;
            lentAmountOpen = false;
            lentPersonOpen = false;
            lentStatusOpen = false;
            lentPaymentOpen = false;
        }


        //pnlBorrow Function
        private void pnlBorrow_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlBorrow || borrowOpen)
            {
                pnlBorrow.BackColor = Color.FromArgb(30, 41, 59);
                picBorrow.Image = Properties.Resources.borrowing_white;
            }
        }

        private void pnlBorrow_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlBorrow || borrowOpen)
            {
                pnlBorrow.BackColor = Color.FromArgb(15, 23, 42);
                picBorrow.Image = Properties.Resources.borrowing;
            }
        }


        private void pnlBorrow_Click(object sender, EventArgs e)
        {
            UpdateHeader(
             "Borrow",
                "Track and manage money you have borrowed from others");

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

            ShowPage(pnlBorrowPage);

            // ১. আগের মেনুর ব্লু হাইলাইট ক্লিয়ার করবে
            SetActiveMenu(pnlBorrow, false);

            bool wasOpen = borrowOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                
                pnlBorrowDropDown.Visible = true;
                picBorrowArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                borrowOpen = true;

                SetActiveBorrowSubMenu(pnlAllBorrow);
            }
            else
            {
               
                pnlBorrowDropDown.Visible = false;
                picBorrowArrow.Image = Properties.Resources.down;
                borrowOpen = false;

                SetActiveMenu(pnlBorrow, true); 
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            borrowDateOpen = false;
            borrowAmountOpen = false;
            borrowPersonOpen = false;
            borrowStatusOpen = false;
            borrowPaymentOpen = false;
        }


        // pnlTasks all Function
        private void pnlTasks_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlTasks || taskOpen)
            {
                pnlTasks.BackColor = Color.FromArgb(30, 41, 59);
                picTasks.Image = Properties.Resources.task__white;
            }
        }

        private void pnlTasks_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlTasks || taskOpen)
            {
                pnlTasks.BackColor = Color.FromArgb(15, 23, 42);
                picTasks.Image = Properties.Resources.task;
            }
        }


        private void pnlTasks_Click(object sender, EventArgs e)
        {
            UpdateHeader(
                 "Tasks",
                 "Organize and track your tasks efficiently");

            if (taskControl == null || taskControl.IsDisposed)
            {
                taskControl = new TaskControls();

                taskControl.TopLevel = false;
                taskControl.FormBorderStyle = FormBorderStyle.None;
                taskControl.Dock = DockStyle.Fill;

                pnlTaskPage.Controls.Clear();

                pnlTaskPage.Controls.Add(taskControl);

                taskControl.Show();
            }

            ShowPage(pnlTaskPage);

            // ১. আগের মেনুর ব্লু হাইলাইট ক্লিয়ার করবে
            SetActiveMenu(pnlTasks, false);

            bool wasOpen = taskOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                
                pnlTaskDropDown.Visible = true;
                picTasksArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                taskOpen = true;

                SetActiveTaskSubMenu(pnlAllTask);
            }
            else
            {
                
                pnlTaskDropDown.Visible = false;
                picTasksArrow.Image = Properties.Resources.down;
                taskOpen = false;

                SetActiveMenu(pnlTasks, true); 
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            taskDateOpen = false;
            taskStatusOpen = false;
            taskPriorityOpen = false;

            pnlTaskFilterContant.Visible = true;
        }



        //Notes Function 
        private void pnlNotes_MouseEnter(object sender, EventArgs e)
        {
            if (activePanel != pnlNotes || notesOpen)
            {
                pnlNotes.BackColor = Color.FromArgb(30, 41, 59);
                picNotes.Image = Properties.Resources.pencil__2_;
            }
        }

        private void pnlNotes_MouseLeave(object sender, EventArgs e)
        {
            if (activePanel != pnlNotes || notesOpen)
            {
                pnlNotes.BackColor = Color.FromArgb(15, 23, 42);
                picNotes.Image = Properties.Resources.pencil;
            }
        }


        private void pnlNotes_Click(object sender, EventArgs e)
        {
            UpdateHeader(
                "Notes",
                "Capture your thoughts and keep everything organized");

            if (noteControl == null || noteControl.IsDisposed)
            {
                noteControl = new NoteControl();

                noteControl.TopLevel = false;
                noteControl.FormBorderStyle = FormBorderStyle.None;
                noteControl.Dock = DockStyle.Fill;

                pnlNotesPage.Controls.Clear();
                pnlNotesPage.Controls.Add(noteControl);

                noteControl.Show();
            }

            if (noteControl != null && !noteControl.IsDisposed)
            {
                noteControl.LoadNoteData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlNotesPage);

           
            SetActiveMenu(pnlNotes, false);

            bool wasOpen = notesOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                
                pnlNotesDropDown.Visible = true;
                picNotesArrow.Image = Properties.Resources.arrowhead_up;
                pnlTop.Visible = true;
                notesOpen = true;

                SetActiveNoteSubMenu(pnlAllNote);
            }
            else
            {
                
                pnlNotesDropDown.Visible = false;
                picNotesArrow.Image = Properties.Resources.down;
                notesOpen = false;

                SetActiveMenu(pnlNotes, true); 
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            noteDateOpen = false;
            noteStatusOpen = false;
            notePriorityOpen = false;

            pnlNoteFilterContant.Visible = true;
        }

        //Setting Function


        private void pnlSettings_MouseEnter(object sender, EventArgs e)
        {


            if (activePanel != pnlSettings)
            {
                pnlSettings.BackColor = Color.FromArgb(30, 41, 59);
                picSettings.Image = Properties.Resources.cogwheel__1_;
            }
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

            pnlTop.Visible = true;
            SetActiveMenu(pnlSettings, true);

            bool wasOpen = settingOpen;

            CloseAllDropDown();

            if (!wasOpen)
            {
                pnlSettingsDropDown.Visible = true;
                picSettingsArrow.Image = Properties.Resources.arrowhead_up;
                settingOpen = true;
            }
            else
            {
                pnlSettingsDropDown.Visible = false;
                picSettingsArrow.Image = Properties.Resources.down;
                //MessageBox.Show(lastOpenedPage == null ? "NULL" : lastOpenedPage.Name);
                UpdateHeader(
                     "Dashboard",
                    "Welcome back! Here's your financial overview.");
                ShowPage(pnlOverview);

                settingOpen = false;
            }


            RefreshSidebarScroll();
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
            UpdateHeader(
                "Profile",
                "Manage your personal profile and account settings");

            SetActiveMenu(pnlUserProfile, true);

            CloseAllDropDown();

            if (profileControl == null || profileControl.IsDisposed)
            {
                profileControl = new ProfileControls();

                profileControl.TopLevel = false;
                profileControl.FormBorderStyle = FormBorderStyle.None;
                profileControl.Dock = DockStyle.Fill;

                pnlProfilePage.Controls.Clear();

                pnlProfilePage.Controls.Add(profileControl);

                profileControl.Show();
            }

            if (profileControl != null && !profileControl.IsDisposed)
            {
                profileControl.LoadUserProfileData();
            }

            ShowPage(pnlProfilePage);

            ExpandSidebar();
        }

        private void MakeCircularPictureBox(PictureBox pictureBox)
        {
            // ১. ছবির ফ্রেমটি যেন নিখুঁত বর্গক্ষেত্র (Square) হয়
            int diameter = Math.Min(pictureBox.Width, pictureBox.Height);
            pictureBox.Width = diameter;
            pictureBox.Height = diameter;

            // ২. এবার গোল মাস্ক তৈরি করা
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, diameter - 1, diameter - 1);
            pictureBox.Region = new Region(path);

            // ৩. ছবি বিকৃত হওয়া এড়াতে Zoom ব্যবহার করা
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }


        public void LoadSidebarUserProfile()
        {
            int userID = Session.LogedInUser.GetUserId();

            DataTable dt = CommonUiFunction.RetrieveDataForGridView("spGetActiveUserDetails", userID);

            if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
            {
                DataRow row = dt.Rows[0];

                // Full Name
                lblUserName.Text = Convert.ToString(row["FullName"]);

                // Email
                lblEmail.Text = Convert.ToString(row["Email"]);

                // Profile Photo
                if (row["ProfilePhoto"] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row["ProfilePhoto"];

                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        picUserProfile.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // Default Image
                    picUserProfile.Image = Properties.Resources.user;
                }
            }
            else
            {
                lblUserName.Text = "";
                lblEmail.Text = "";
                picUserProfile.Image = Properties.Resources.user;
            }
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tmSidebar.Start();
        }
        //Side Bar all work
        // Sidebar Expand / Collapse Animation
        // Timer choler সময় Sidebar slowly slowly choto ba bora kora

        private void tmSidebar_Tick(object sender, EventArgs e)
        {
            if (isSidebarExpanded)
            {

                pnlMainSideBarSection.Width -= 20;
                // Width 180px ba tar kom hole all Text and Arrow Hide korbe
                if (pnlMainSideBarSection.Width <= 180)
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
                // Width 80px er niche gele Sidebar puropuri Collapse hobe

                if (pnlMainSideBarSection.Width < 80)
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
                pnlMainSideBarSection.Width += 20;
                CenterIcons(false);
                if (pnlMainSideBarSection.Width >= 80)
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
                if (pnlMainSideBarSection.Width >= 300)
                {
                    pnlMainSideBarSection.Width = 300;

                    CenterIcons(false);
                    //flowSidebar.PerformLayout();
                    //flowSidebar.Refresh();

                    tmSidebar.Stop();

                    isSidebarExpanded = true;
                }
            }
        }

        // Sidebar Collapse hole Icon Center kore
        // Expand hole Icon ager Position e firiye ane
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
            pic.Left = (pnlMainSideBarSection.Width - pic.Width) / 2;
        }



        //FilterExpense all event

        private void pnlAllExpense_MouseEnter(object sender, EventArgs e)
        {
            if (activeExpenseSubMenu != pnlAllExpense)
            {
                pnlAllExpense.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllExpense_MouseLeave(object sender, EventArgs e)
        {
            if (activeExpenseSubMenu != pnlAllExpense)
            {
                pnlAllExpense.BackColor = Color.Transparent;
            }
        }

        private void SetActiveExpenseSubMenu(Panel activePanel)
        {

            if (activeExpenseSubMenu != null)
            {
                activeExpenseSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeExpenseSubMenu = activePanel;
        }

        private void pnlAllExpense_Click(object sender, EventArgs e)
        {
            SetActiveExpenseSubMenu(pnlAllExpense);
        }

        private void pnlAddExpense_Click(object sender, EventArgs e)
        {
            SetActiveExpenseSubMenu(pnlAddExpense);

            AddExpenseControls expenseDetailsControl = new AddExpenseControls();
            expenseDetailsControl.FormClosed += Expense_FormClosed;
            expenseDetailsControl.Show();
        }
        private void Expense_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveExpenseSubMenu(pnlAllExpense);
            if (expenseControl != null && !expenseControl.IsDisposed)
            {
                expenseControl.LoadExpenseData(Session.LogedInUser.GetUserId());
            }
        }

        private void pnlAddExpense_MouseEnter(object sender, EventArgs e)
        {
            if (activeExpenseSubMenu != pnlAddExpense)
            {
                pnlAddExpense.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddExpense_MouseLeave(object sender, EventArgs e)
        {
            if (activeExpenseSubMenu != pnlAddExpense)
            {
                pnlAddExpense.BackColor = Color.Transparent;
            }
        }

       

        //Credit Filter all event

        private void pnlAllCredit_MouseEnter(object sender, EventArgs e)
        {
            if (activeCreditSubMenu != pnlAllCredit)
            {
                pnlAllCredit.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllCredit_MouseLeave(object sender, EventArgs e)
        {
            if (activeCreditSubMenu != pnlAllCredit)
            {
                pnlAllCredit.BackColor = Color.Transparent;
            }
        }


        private void SetActiveCreditSubMenu(Panel activePanel)
        {
            if (activeCreditSubMenu != null)
            {
                activeCreditSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeCreditSubMenu = activePanel;
        }

        private void pnlAllCredit_Click(object sender, EventArgs e)
        {
            SetActiveCreditSubMenu(pnlAllCredit);
        }

        private void pnlAddCredit_Click(object sender, EventArgs e)
        {
            SetActiveCreditSubMenu(pnlAddCredit);

            AddCreditControls creditDetailsControl = new AddCreditControls();

            creditDetailsControl.FormClosed += Credit_FormClosed;

            creditDetailsControl.Show();
        }
        private void Credit_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveCreditSubMenu(pnlAllCredit);

            if (creditControl != null && !creditControl.IsDisposed)
            {
                creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
            }

        }

        private void pnlAddCredit_MouseEnter(object sender, EventArgs e)
        {
            if (activeCreditSubMenu != pnlAddCredit)
            {
                pnlAddCredit.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddCredit_MouseLeave(object sender, EventArgs e)
        {
            if (activeCreditSubMenu != pnlAddCredit)
            {
                pnlAddCredit.BackColor = Color.Transparent;
            }
        }


        //Lent Start

        
      

        private void pnlAllLent_MouseEnter(object sender, EventArgs e)
        {
            if (activeLentSubMenu != pnlAllLent)
            {
                pnlAllLent.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllLent_MouseLeave(object sender, EventArgs e)
        {
            if (activeLentSubMenu != pnlAllLent)
            {
                pnlAllLent.BackColor = Color.Transparent;
            }
        }

        private void SetActiveLentSubMenu(Panel activePanel)
        {
            if (activeLentSubMenu != null)
            {
                activeLentSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeLentSubMenu = activePanel;
        }


        private void pnlAddLent_Click(object sender, EventArgs e)
        {
            SetActiveLentSubMenu(pnlAddLent);

            AddLentControls addLentControls = new AddLentControls();

            addLentControls.FormClosed += Lent_FormClosed;

            addLentControls.Show();
        }
        private void Lent_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveLentSubMenu(pnlAllLent);

            if (lentControl != null && !lentControl.IsDisposed)
            {
                lentControl.LoadLentData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlLentPage);
        }
        private void pnlAddLent_MouseEnter(object sender, EventArgs e)
        {
            if (activeLentSubMenu != pnlAddLent)
            {
                pnlAddLent.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddLent_MouseLeave(object sender, EventArgs e)
        {
            if (activeLentSubMenu != pnlAddLent)
            {
                pnlAddLent.BackColor = Color.Transparent;
            }
        }


        //Borrow Start

        private void pnlAllBorrow_Click(object sender, EventArgs e)
        {
            SetActiveBorrowSubMenu(pnlAllBorrow);
        }

        private void pnlAllBorrow_MouseEnter(object sender, EventArgs e)
        {
            if (activeBorrowSubMenu != pnlAllBorrow)
            {
                pnlAllBorrow.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllBorrow_MouseLeave(object sender, EventArgs e)
        {
            if (activeBorrowSubMenu != pnlAllBorrow)
            {
                pnlAllBorrow.BackColor = Color.Transparent;
            }
        }

        private void SetActiveBorrowSubMenu(Panel activePanel)
        {
            if (activeBorrowSubMenu != null)
            {
                activeBorrowSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeBorrowSubMenu = activePanel;
        }

        private void pnlAddBorrow_Click(object sender, EventArgs e)
        {
            SetActiveBorrowSubMenu(pnlAddBorrow);

            AddBorrowControls addBorrowControls = new AddBorrowControls();

            addBorrowControls.FormClosed += Borrow_FormClosed;

            addBorrowControls.Show();
        }
        private void Borrow_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveBorrowSubMenu(pnlAllBorrow);
            if (borrowControls != null && !borrowControls.IsDisposed)
            {
                borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlBorrowPage);

        }

        private void pnlAddBorrow_MouseEnter(object sender, EventArgs e)
        {
            if (activeBorrowSubMenu != pnlAddBorrow)
            {
                pnlAddBorrow.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddBorrow_MouseLeave(object sender, EventArgs e)
        {
            if (activeBorrowSubMenu != pnlAddBorrow)
            {
                pnlAddBorrow.BackColor = Color.Transparent;
            }
        }

        //Task filter all event

        private void pnlAllTask_Click(object sender, EventArgs e)
        {
            SetActiveTaskSubMenu(pnlAllTask);
        }

        private void pnlAllTask_MouseEnter(object sender, EventArgs e)
        {
            if (activeTaskSubMenu != pnlAllTask)
            {
                pnlAllTask.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllTask_MouseLeave(object sender, EventArgs e)
        {
            if (activeTaskSubMenu != pnlAllTask)
            {
                pnlAllTask.BackColor = Color.Transparent;
            }
        }

        private void SetActiveTaskSubMenu(Panel activePanel)
        {
            if (activeTaskSubMenu != null)
            {
                activeTaskSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeTaskSubMenu = activePanel;
        }

        private void pnlAddTask_Click(object sender, EventArgs e)
        {
            SetActiveTaskSubMenu(pnlAddTask);

            AddTaskControl addTaskControl = new AddTaskControl();

            addTaskControl.FormClosed += Task_FormClosed;

            addTaskControl.Show();
        }

        private void Task_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveTaskSubMenu(pnlAllTask);

            if (taskControl != null && !taskControl.IsDisposed)
            {
                taskControl.LoadTaskData(Session.LogedInUser.GetUserId());
            }
        }

        private void pnlAddTask_MouseEnter(object sender, EventArgs e)
        {
            if (activeTaskSubMenu != pnlAddTask)
            {
                pnlAddTask.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddTask_MouseLeave(object sender, EventArgs e)
        {
            if (activeTaskSubMenu != pnlAddTask)
            {
                pnlAddTask.BackColor = Color.Transparent;
            }
        }

        //Note filter all event

        private void pnlAllNote_Click(object sender, EventArgs e)
        {
            SetActiveNoteSubMenu(pnlAllNote);

            if (noteControl != null && !noteControl.IsDisposed)
            {
                noteControl.LoadNoteData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlNotesPage);
        }

        private void pnlAllNote_MouseEnter(object sender, EventArgs e)
        {
            if (activeNoteSubMenu != pnlAllNote)
            {
                pnlAllNote.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAllNote_MouseLeave(object sender, EventArgs e)
        {
            if (activeNoteSubMenu != pnlAllNote)
            {
                pnlAllNote.BackColor = Color.Transparent;
            }
        }

        private void SetActiveNoteSubMenu(Panel activePanel)
        {
            if (activeNoteSubMenu != null)
            {
                activeNoteSubMenu.BackColor = Color.Transparent;
            }

            activePanel.BackColor = Color.FromArgb(59, 130, 246);

            activeNoteSubMenu = activePanel;
        }

        private void pnlAddNote_Click(object sender, EventArgs e)
        {
            SetActiveNoteSubMenu(pnlAddNote);

            NoteAddDetailsControl noteAddDetailsControl = new NoteAddDetailsControl();

            noteAddDetailsControl.FormClosed += Note_FormClosed;

            noteAddDetailsControl.Show();
        }
        private void Note_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetActiveNoteSubMenu(pnlAllNote);

            if (noteControl != null && !noteControl.IsDisposed)
            {
                noteControl.LoadNoteData(Session.LogedInUser.GetUserId());
            }
        }

        private void pnlAddNote_MouseEnter(object sender, EventArgs e)
        {
            if (activeNoteSubMenu != pnlAddNote)
            {
                pnlAddNote.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlAddNote_MouseLeave(object sender, EventArgs e)
        {
            if (activeNoteSubMenu != pnlAddNote)
            {
                pnlAddNote.BackColor = Color.Transparent;
            }
        }

        private void SetActiveSettingSubMenu(Panel panel)
        {
            if (activeSettingSubMenu != null)
                activeSettingSubMenu.BackColor = Color.Transparent;

            pnlSettings.BackColor = Color.FromArgb(15, 23, 42);
            picSettings.Image = Properties.Resources.cogwheel;

            if (activePanel == pnlSettings)
                activePanel = null;

            panel.BackColor = Color.FromArgb(59, 130, 246);

            activeSettingSubMenu = panel;
        }

        private void pnlSettingExpenseCategories_Click(object sender, EventArgs e)
        {
            

            UpdateHeader(
                "Expense Category",
                "Manage expense categories and subcategories");

            ShowPage(pnlExpenseCategory);

            SetActiveSettingSubMenu(pnlSettingExpenseCategories);

            RefreshSidebarScroll();
            ExpandSidebar();
            if (expenseCategoryControls == null || expenseCategoryControls.IsDisposed)
            {
                expenseCategoryControls = new ExpenseCategoryControls();

                expenseCategoryControls.TopLevel = false;
                expenseCategoryControls.FormBorderStyle = FormBorderStyle.None;
                expenseCategoryControls.Dock = DockStyle.Fill;
                pnlTop.Visible = false;
                pnlExpenseCategory.Controls.Clear();

                pnlExpenseCategory.Controls.Add(expenseCategoryControls);

                expenseCategoryControls.Show();
            }

        }

        private void pnlSettingExpenseCategories_MouseEnter(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingExpenseCategories)
            {
                pnlSettingExpenseCategories.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlSettingExpenseCategories_MouseLeave(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingExpenseCategories)
            {
                pnlSettingExpenseCategories.BackColor = Color.Transparent;
            }
        }

        private void pnlSettingCreditCategories_Click(object sender, EventArgs e)
        {

            SetActiveSettingSubMenu(pnlSettingCreditCategories);


            UpdateHeader(
                "Credit Category",
                "Manage credit categories and subcategories");

            ShowPage(pnlCreditCategoryPage);

            SetActiveSettingSubMenu(pnlSettingCreditCategories);

            RefreshSidebarScroll();
            ExpandSidebar();

            if (creditCategoryControls == null || creditCategoryControls.IsDisposed)
            {
                creditCategoryControls = new CreditCategoryControls();

                creditCategoryControls.TopLevel = false;
                creditCategoryControls.FormBorderStyle = FormBorderStyle.None;
                creditCategoryControls.Dock = DockStyle.Fill;
                pnlTop.Visible = false;
                pnlCreditCategoryPage.Controls.Clear();

                pnlCreditCategoryPage.Controls.Add(creditCategoryControls);

                creditCategoryControls.Show();
            }
        }

        private void pnlSettingCreditCategories_MouseEnter(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingCreditCategories)
            {
                pnlSettingCreditCategories.BackColor = Color.FromArgb(30, 41, 59);
            }
        }



        private void pnlSettingCreditCategories_MouseLeave(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingCreditCategories)
            {
                pnlSettingCreditCategories.BackColor = Color.Transparent;
            }
        }

        private void pnlLogout_Click(object sender, EventArgs e)
        {
            
            previousSettingSubMenu = activeSettingSubMenu;

            SetActiveSettingSubMenu(pnlLogout);
            pnlTop.Visible = false;

            PersonLogOutControls personLogOutControls = new PersonLogOutControls();

            personLogOutControls.FormClosed += Logout_FormClosed;

            personLogOutControls.Show();

            RefreshSidebarScroll();
            ExpandSidebar();
        }

        private void Logout_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            if (previousSettingSubMenu != null)
            {
                SetActiveSettingSubMenu(previousSettingSubMenu);
            }
            else
            {
                SetActiveSettingSubMenu(pnlSettingExpenseCategories);
            }
        }


        private void pnlLogout_MouseEnter(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlLogout)
            {
                pnlLogout.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlLogout_MouseLeave(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlLogout)
            {
                pnlLogout.BackColor = Color.Transparent;
            }
        }


        private void pnlSideBar_Resize(object sender, EventArgs e)
        {
            flowSidebar.Width = pnlSideBar.ClientSize.Width;
            //UpdateThumb();
        }

        private void pnlSettingChangesPassword_Click(object sender, EventArgs e)
        {
            previousSettingSubMenu = activeSettingSubMenu;
            SetActiveSettingSubMenu(pnlSettingChangesPassword);

            ChangePasswordControls changePasswordControls = new ChangePasswordControls();
            changePasswordControls.FormClosed += ChangePassword_FormClosed; 
            changePasswordControls.Show();
            RefreshSidebarScroll();
            ExpandSidebar();
        }

        private void ChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (previousSettingSubMenu != null)
            {
                SetActiveSettingSubMenu(previousSettingSubMenu);
            }
            else
            {
                SetActiveSettingSubMenu(pnlSettingExpenseCategories);
            }
        }


        private void pnlSettingChangesPassword_MouseEnter(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingChangesPassword)
            {
                pnlSettingChangesPassword.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlSettingChangesPassword_MouseLeave(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingChangesPassword)
            {
                pnlSettingChangesPassword.BackColor = Color.Transparent;
            }
        }

        private void pnlSettingPersonAdd_Click(object sender, EventArgs e)
        {

            SetActiveSettingSubMenu(pnlSettingPersonAdd);

            UpdateHeader(
     "Add Person",
     "Save a person's details for use in Lent and Borrow modules");

            ShowPage(pnlPersonAddPage);

            if (addPersonSControls == null || addPersonSControls.IsDisposed)
            {
                addPersonSControls = new AddPersonControls();

                addPersonSControls.TopLevel = false;
                addPersonSControls.FormBorderStyle = FormBorderStyle.None;
                addPersonSControls.Dock = DockStyle.Fill;

                pnlPersonAddPage.Controls.Clear();
                pnlPersonAddPage.Controls.Add(addPersonSControls);

                addPersonSControls.Show();
            }

            RefreshSidebarScroll();
            ExpandSidebar();
        }

        private void pnlSettingPersonAdd_MouseEnter(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingPersonAdd)
            {
                pnlSettingPersonAdd.BackColor = Color.FromArgb(30, 41, 59);
            }
        }

        private void pnlSettingPersonAdd_MouseLeave(object sender, EventArgs e)
        {
            if (activeSettingSubMenu != pnlSettingPersonAdd)
            {
                pnlSettingPersonAdd.BackColor = Color.Transparent;
            }
        }

        private void pnlAllLent_Click(object sender, EventArgs e)
        {
            SetActiveLentSubMenu(pnlAllLent);
        }

        private void button1_Click(object sender, EventArgs e)
            {
                if (dashboardControl != null && !dashboardControl.IsDisposed)
                {
                    dashboardControl.pnlNotification.Visible = true;
                    dashboardControl.pnlNotification.BringToFront();

                    
                    if (dashboardControl.flowLayoutPanel5.Controls.Count == 0)
                    {
                        dashboardControl.LoadNotifications(Session.LogedInUser.GetUserId(), dashboardControl.pnlNotification, dashboardControl.flowLayoutPanel5);
                    }
                }
            }

        private Label lblHeaderBadge;

        public void UpdateNotificationBadge(int count)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { UpdateNotificationBadge(count); });
                return;
            }

            if (lblHeaderBadge == null)
            {
                lblHeaderBadge = new Label
                {
                    AutoSize = false,
                    Size = new Size(18, 18),
                    BackColor = Color.FromArgb(239, 68, 68), // Bright Red Badge
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Visible = false,
                    Cursor = Cursors.Hand
                };

                
                try
                {
                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddEllipse(0, 0, lblHeaderBadge.Width, lblHeaderBadge.Height);
                    lblHeaderBadge.Region = new Region(path);
                }
                catch { }

                panel5.Controls.Add(lblHeaderBadge);
                lblHeaderBadge.Location = new Point(button1.Right - 18, 4);
                lblHeaderBadge.Click += button1_Click;
            }

            if (count > 0)
            {
                lblHeaderBadge.Text = count > 99 ? "99+" : count.ToString();
                lblHeaderBadge.Visible = true;
                lblHeaderBadge.BringToFront();
            }
            else
            {
                lblHeaderBadge.Visible = false;
            }
        }


        private void ShowCurrentDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dd MMM yyyy | hh:mm:ss tt");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowCurrentDateTime();
        }



      

       
    }
}
