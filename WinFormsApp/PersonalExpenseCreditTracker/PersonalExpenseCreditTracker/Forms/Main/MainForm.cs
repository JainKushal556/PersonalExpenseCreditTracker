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
namespace PersonalExpenseCreditTracker
{
    public partial class MainForm : Form
    {
        private Panel lastOpenedPage = null;

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
            pnlDateBody.Visible = false;
            pnlAmountBody.Visible = false;
            pnlCategoryBody.Visible = false;

            pnlCreditFilterContant.Visible = false;
            pnlCreditDateBody.Visible = false;
            pnlCreditAmountBody.Visible = false;
            pnlCreditCategoryBody.Visible = false;

            pnlLentFilterContant.Visible = false;
            pnlLentDateBody.Visible = false;
            pnlLentAmountBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentStatusBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            pnlBorrowFilterContant.Visible = false;
            pnlBorrowDateBody.Visible = false;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            this.Resize += MainForm_Resize;

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            flowSidebar.Location = new Point(0, 0);
            flowSidebar.Width = pnlSideBar.ClientSize.Width;
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

            ComboBoxCreditSubCategory.Visible = false;
            lblCreditSelectSubCategory.Visible = false;


            ShowPage(pnlOverview);
            btnClearAll.Visible = false;
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
            picFilterHeader.Image = Properties.Resources.down;



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
            else if (panel == pnlUserProfile)
                picUserProfile.Image = active ? Properties.Resources.user : Properties.Resources.user;
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

            ShowPage(pnlOverview);
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
            pnlExpense.BackColor = Color.FromArgb(15, 23, 42);
            picExpense.Image = Properties.Resources.wallet_filled_money_tool1;
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
            }
            RefreshSidebarScroll();
            ExpandSidebar();
            dateOpen = false;
            amountOpen = false;
            categoryOpen = false;

            pnlDateBody.Visible = false;
            pnlAmountBody.Visible = false;
            pnlCategoryBody.Visible = false;

            pnlFilterContent.Visible = true;

            pnlDateHeader.Visible = false;
            pnlAmountHeader.Visible = false;
            pnlCategoryHeader.Visible = false;

            picFilterByDateArrow.Image = Properties.Resources.down;
            picFilterByAmountArrow.Image = Properties.Resources.down;
            picFilterByCategoryArrow.Image = Properties.Resources.down;


        }


        //pnlCredit all Function
        private void pnlCredit_MouseEnter(object sender, EventArgs e)
        {
            pnlCredit.BackColor = Color.FromArgb(59, 130, 246);
            picCredit.Image = Properties.Resources.credit_card_white;
        }

        private void pnlCredit_MouseLeave(object sender, EventArgs e)
        {
            pnlCredit.BackColor = Color.FromArgb(15, 23, 42);
            picCredit.Image = Properties.Resources.credit_card;
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
            }
            RefreshSidebarScroll();
            ExpandSidebar();

            creditDateOpen = false;
            creditAmountOpen = false;
            creditCategoryOpen = false;

            pnlCreditDateBody.Visible = false;
            pnlCreditAmountBody.Visible = false;
            pnlCreditCategoryBody.Visible = false;

            pnlCreditFilterContant.Visible = true;

            pnlCreditDateHeader.Visible = false;
            pnlCreditAmountHeader.Visible = false;
            pnlCreditCategoryHeader.Visible = false;

            picCreditFilterByDateArrow.Image = Properties.Resources.down;
            picCreditFilterByAmountArrow.Image = Properties.Resources.down;
            picCreditFilterByCategoryArrow.Image = Properties.Resources.down;
        }

        //pnlLent all Function

        private void pnlLent_MouseEnter(object sender, EventArgs e)
        {
            pnlLent.BackColor = Color.FromArgb(59, 130, 246);
            picLent.Image = Properties.Resources.payment_white;
        }

        private void pnlLent_MouseLeave(object sender, EventArgs e)
        {
            pnlLent.BackColor = Color.FromArgb(15, 23, 42);
            picLent.Image = Properties.Resources.payment_lent1;
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

            CloseAllDropDown();
            SetActiveMenu(pnlLent, false);

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
            }

            RefreshSidebarScroll();
            ExpandSidebar();

            lentDateOpen = false;
            lentAmountOpen = false;
            lentPersonOpen = false;
            lentStatusOpen = false;
            lentPaymentOpen = false;

            pnlLentDateBody.Visible = false;
            pnlLentAmountBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentStatusBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            pnlLentFilterContant.Visible = true;

            pnlLentDateHeader.Visible = false;
            pnlLentAmountHeader.Visible = false;
            pnlLentPersonHeader.Visible = false;
            pnlLentStatusHeader.Visible = false;
            pnlLentPaymentHeader.Visible = false;

            picLentFilterByDateArrow.Image = Properties.Resources.down;
            picLentFilterByAmountArrow.Image = Properties.Resources.down;
            picLentFilterByPersonArrow.Image = Properties.Resources.down;
            picLentFilterByStatusArrow.Image = Properties.Resources.down;
            picLentFilterByPaymentArrow.Image = Properties.Resources.down;
        }

        //pnlBorrow Function
        private void pnlBorrow_MouseEnter(object sender, EventArgs e)
        {
            pnlBorrow.BackColor = Color.FromArgb(59, 130, 246);
            picBorrow.Image = Properties.Resources.borrowing_white;
        }

        private void pnlBorrow_MouseLeave(object sender, EventArgs e)
        {
            pnlBorrow.BackColor = Color.FromArgb(15, 23, 42);
            picBorrow.Image = Properties.Resources.borrowing;
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
            }

            RefreshSidebarScroll();
            ExpandSidebar();


            borrowDateOpen = false;
            borrowAmountOpen = false;
            borrowPersonOpen = false;
            borrowStatusOpen = false;
            borrowPaymentOpen = false;

            pnlBorrowDateBody.Visible = false;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            pnlBorrowFilterContant.Visible = true;

            pnlBorrowDateHeader.Visible = false;
            pnlBorrowAmountHeader.Visible = false;
            pnlBorrowPersonHeader.Visible = false;
            pnlBorrowStatusHeader.Visible = false;
            pnlBorrowPaymentHeader.Visible = false;

            picBorrowFilterByDateArrow.Image = Properties.Resources.down;
            picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
            picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
            picBorrowFilterByStatusArrow.Image = Properties.Resources.down;
            picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;
        }

        // pnlTasks all Function
        private void pnlTasks_MouseEnter(object sender, EventArgs e)
        {
            pnlTasks.BackColor = Color.FromArgb(59, 130, 246);
            picTasks.Image = Properties.Resources.task__white;
        }

        private void pnlTasks_MouseLeave(object sender, EventArgs e)
        {
            pnlTasks.BackColor = Color.FromArgb(15, 23, 42);
            picTasks.Image = Properties.Resources.task;
        }

        private void pnlTasks_Click(object sender, EventArgs e)
        {

            UpdateHeader(
                 "Tasks",
                 "Organize and track your tasks efficiently");

            SetActiveMenu(pnlTasks, false);

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
            }

            ShowPage(pnlTaskPage);

            RefreshSidebarScroll();
            ExpandSidebar();



            taskDateOpen = false;
            taskStatusOpen = false;
            taskPriorityOpen = false;

            pnlTaskDateBody.Visible = false;
            pnlTaskStatusBody.Visible = false;
            pnlTaskPriorityBody.Visible = false;

            pnlTaskFilterContant.Visible = true;

            pnlTaskDateHeader.Visible = false;
            pnlTaskStatusHeader.Visible = false;
            pnlTaskPriorityHeader.Visible = false;

            picTaskFilterByDateArrow.Image = Properties.Resources.down;
            picTaskFilterByStatusArrow.Image = Properties.Resources.down;
            picTaskFilterByPriorityArrow.Image = Properties.Resources.down;
        }


        //Notes Function 
        private void pnlNotes_MouseEnter(object sender, EventArgs e)
        {
            pnlNotes.BackColor = Color.FromArgb(59, 130, 246);
            picNotes.Image = Properties.Resources.pencil__2_;
        }

        private void pnlNotes_MouseLeave(object sender, EventArgs e)
        {
            pnlNotes.BackColor = Color.FromArgb(15, 23, 42);
            picNotes.Image = Properties.Resources.pencil;
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

            pnlOverview.Visible = false;
            pnlExpensePage.Visible = false;
            pnlCreditPage.Visible = false;
            pnlLentPage.Visible = false;
            pnlBorrowPage.Visible = false;
            pnlTaskPage.Visible = false;
            pnlNotesPage.Visible = true;
            pnlSettingPage.Visible = false;
            pnlProfilePage.Visible = false;

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
            }

            ShowPage(pnlNotesPage);

            RefreshSidebarScroll();
            ExpandSidebar();


            noteDateOpen = false;
            noteStatusOpen = false;
            notePriorityOpen = false;

            pnlNoteDateBody.Visible = false;
            pnlNoteStatusBody.Visible = false;
            pnlNotePriorityBody.Visible = false;

            pnlNoteFilterContant.Visible = true;

            pnlNoteDateHeader.Visible = false;
            pnlNoteStatusHeader.Visible = false;
            pnlNotePriorityHeader.Visible = false;

            picNoteFilterByDateArrow.Image = Properties.Resources.down;
            picTaskFilterByNoteArrow.Image = Properties.Resources.down;


            picNotePriorityArrow.Image = Properties.Resources.down;
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

            ShowPage(pnlProfilePage);

            ExpandSidebar();
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

        private void pnlFilterHeader_Click(object sender, EventArgs e)
        {
            filterOpen = !filterOpen;

            pnlFilterContent.Visible = filterOpen;
            pnlDateHeader.Visible = filterOpen;
            pnlAmountHeader.Visible = filterOpen;
            pnlCategoryHeader.Visible = filterOpen;

            picFilterHeader.Image = filterOpen ? Properties.Resources.arrowhead_up : Properties.Resources.down;

            if (!filterOpen)
            {
                dateOpen = false;
                amountOpen = false;
                categoryOpen = false;

                pnlDateBody.Visible = false;
                pnlAmountBody.Visible = false;
                pnlCategoryBody.Visible = false;

                picFilterByDateArrow.Image = Properties.Resources.down;
                picFilterByAmountArrow.Image = Properties.Resources.down;
                picFilterByCategoryArrow.Image = Properties.Resources.down;
            }


            flowSidebar.Top = 0;
        }

        private void pnlDateHeader_Click(object sender, EventArgs e)
        {
            dateOpen = !dateOpen;

            amountOpen = false;
            categoryOpen = false;

            pnlDateBody.Visible = dateOpen;
            pnlAmountBody.Visible = false;
            pnlCategoryBody.Visible = false;

            picFilterByDateArrow.Image = dateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picFilterByAmountArrow.Image = Properties.Resources.down;
            picFilterByCategoryArrow.Image = Properties.Resources.down;
            if (!dateOpen)
            {
                rbThisMonth.Checked = false;
                rbLast7Days.Checked = false;
                rbThisYear.Checked = false;
                rbCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        private void pnlAmountHeader_Click(object sender, EventArgs e)
        {
            amountOpen = !amountOpen;

            dateOpen = false;
            categoryOpen = false;

            pnlAmountBody.Visible = amountOpen;
            pnlDateBody.Visible = false;
            pnlCategoryBody.Visible = false;

            picFilterByAmountArrow.Image = amountOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picFilterByDateArrow.Image = Properties.Resources.down;
            picFilterByCategoryArrow.Image = Properties.Resources.down;

            flowSidebar.Top = 0;
        }

        private void pnlCategoryHeader_Click(object sender, EventArgs e)
        {
            categoryOpen = !categoryOpen;

            dateOpen = false;
            amountOpen = false;

            pnlCategoryBody.Visible = categoryOpen;
            pnlDateBody.Visible = false;
            pnlAmountBody.Visible = false;

            picFilterByCategoryArrow.Image = categoryOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picFilterByDateArrow.Image = Properties.Resources.down;
            picFilterByAmountArrow.Image = Properties.Resources.down;
            UpdateClearAllButton();

            flowSidebar.Top = 0;

        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomDatePanel();
        }

        private void UpdateCustomDatePanel()
        {
            pnlCustomDate.Visible = rbCustom.Checked;

            if (rbCustom.Checked)
                pnlDateBody.Height = 280;
            else
                pnlDateBody.Height = 120;
        }

        private void rbThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomDatePanel();
            UpdateClearAllButton();
        }

        private void rbLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomDatePanel();
            UpdateClearAllButton();
        }

        private void rbThisYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomDatePanel();
            UpdateClearAllButton();
        }

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

        private void UpdateClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbThisMonth.Checked ||
                rbLast7Days.Checked ||
                rbThisYear.Checked ||
                rbCustom.Checked)
            {
                hasFilter = true;
            }

            // Amount
            if (txtMinAmount.Text.Trim() != "" ||
                txtMaxAmount.Text.Trim() != "")
            {
                hasFilter = true;
            }

            // Category
            if (ComboBoxCategory.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Sub Category
            if (cmbSubCategory.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnClearAll.Visible = hasFilter;
        }

        private void txtMixAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateClearAllButton();
        }

        private void txtMaxAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateClearAllButton();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            rbThisMonth.Checked = false;
            rbLast7Days.Checked = false;
            rbThisYear.Checked = false;
            rbCustom.Checked = false;

            txtMinAmount.Clear();
            txtMaxAmount.Clear();

            ComboBoxCategory.SelectedIndex = 0;
            cmbSubCategory.SelectedIndex = 0;

            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            btnClearAll.Visible = false;
        }

        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateClearAllButton();

        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateClearAllButton();
        }

        //Credit Filter all event

        private void pnlFilterCreditHeader_Click(object sender, EventArgs e)
        {
            creditFilterOpen = !creditFilterOpen;

            pnlCreditFilterContant.Visible = creditFilterOpen;
            pnlCreditDateHeader.Visible = creditFilterOpen;
            pnlCreditAmountHeader.Visible = creditFilterOpen;
            pnlCreditCategoryHeader.Visible = creditFilterOpen;

            picFilterCreditHeaderArrow.Image = creditFilterOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            if (!creditFilterOpen)
            {
                creditDateOpen = false;
                creditAmountOpen = false;
                creditCategoryOpen = false;

                pnlCreditDateBody.Visible = false;
                pnlCreditAmountBody.Visible = false;
                pnlCreditCategoryBody.Visible = false;

                picCreditFilterByDateArrow.Image = Properties.Resources.down;
                picCreditFilterByAmountArrow.Image = Properties.Resources.down;
                picCreditFilterByCategoryArrow.Image = Properties.Resources.down;
            }

            flowSidebar.Top = 0;
        }

        private void pnlCreditDateHeader_Click(object sender, EventArgs e)
        {
            creditDateOpen = !creditDateOpen;

            creditAmountOpen = false;
            creditCategoryOpen = false;

            pnlCreditDateBody.Visible = creditDateOpen;
            pnlCreditAmountBody.Visible = false;
            pnlCreditCategoryBody.Visible = false;

            picCreditFilterByDateArrow.Image = creditDateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picCreditFilterByAmountArrow.Image = Properties.Resources.down;
            picCreditFilterByCategoryArrow.Image = Properties.Resources.down;

            if (!creditDateOpen)
            {
                rbCreditThisMonth.Checked = false;
                rbCreditLast7Days.Checked = false;
                rbCreditThisYear.Checked = false;
                rbCreditCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        private void pnlCreditAmountHeader_Click(object sender, EventArgs e)
        {
            creditAmountOpen = !creditAmountOpen;

            creditDateOpen = false;
            creditCategoryOpen = false;

            pnlCreditAmountBody.Visible = creditAmountOpen;
            pnlCreditDateBody.Visible = false;
            pnlCreditCategoryBody.Visible = false;

            picCreditFilterByAmountArrow.Image = creditAmountOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picCreditFilterByDateArrow.Image = Properties.Resources.down;
            picCreditFilterByCategoryArrow.Image = Properties.Resources.down;

            flowSidebar.Top = 0;
        }

        private void pnlCreditCategoryHeader_Click(object sender, EventArgs e)
        {
            creditCategoryOpen = !creditCategoryOpen;

            creditDateOpen = false;
            creditAmountOpen = false;

            pnlCreditCategoryBody.Visible = creditCategoryOpen;
            pnlCreditDateBody.Visible = false;
            pnlCreditAmountBody.Visible = false;

            picCreditFilterByCategoryArrow.Image = creditCategoryOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picCreditFilterByDateArrow.Image = Properties.Resources.down;
            picCreditFilterByAmountArrow.Image = Properties.Resources.down;

            UpdateCreditClearAllButton();

            flowSidebar.Top = 0;

            CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", ComboBoxCreditCategory);

        }

        private void btnCreditClearAll_Click(object sender, EventArgs e)
        {
            // Date Filter
            rbCreditThisMonth.Checked = false;
            rbCreditLast7Days.Checked = false;
            rbCreditThisYear.Checked = false;
            rbCreditCustom.Checked = false;

            // Amount Filter
            txtCreditMinAmount.Clear();
            txtCreditMaxAmount.Clear();

            // Category Filter
            if (ComboBoxCreditCategory.Items.Count > 0)
                ComboBoxCreditCategory.SelectedIndex = 0;

            if (ComboBoxCreditSubCategory.Items.Count > 0)
                ComboBoxCreditSubCategory.SelectedIndex = 0;

            // Date Picker
            dtpCreditFromDate.Value = DateTime.Today;
            dtpCreditToDate.Value = DateTime.Today;

            // Hide SubCategory
            ComboBoxCreditSubCategory.Visible = false;
            lblCreditSelectSubCategory.Visible = false;

            // Hide Clear Button
            btnCreditClearAll.Visible = false;

            // Reload Data
            creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
        }

        private void UpdateCreditClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbCreditThisMonth.Checked ||
                rbCreditLast7Days.Checked ||
                rbCreditThisYear.Checked ||
                rbCreditCustom.Checked)
            {
                hasFilter = true;
            }

            // Amount
            if (txtCreditMinAmount.Text.Trim() != "" ||
                txtCreditMaxAmount.Text.Trim() != "")
            {
                hasFilter = true;
            }

            // Category
            if (ComboBoxCreditCategory.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Sub Category
            if (ComboBoxCreditSubCategory.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnCreditClearAll.Visible = hasFilter;
        }

        private void rbCreditCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCreditCustomDatePanel();
            UpdateCreditClearAllButton();
        }

        private void UpdateCreditCustomDatePanel()
        {
            pnlCreditCustomDate.Visible = rbCreditCustom.Checked;

            if (rbCreditCustom.Checked)
                pnlCreditDateBody.Height = 280;
            else
                pnlCreditDateBody.Height = 120;
        }

        private void rbCreditThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbCreditThisMonth.Checked) return;
            UpdateCreditCustomDatePanel();
            UpdateCreditClearAllButton();

            if (creditControl != null && !creditControl.IsDisposed)
            {


                DateTime firstDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                 1);

                DateTime lastDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                creditControl.LoadFilteredCreditData("spFilterCreditByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", firstDayOfMonth, "@ToDate", lastDayOfMonth);

            }
        }

        private void rbCreditLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbCreditLast7Days.Checked) return;
            UpdateCreditCustomDatePanel();
            UpdateCreditClearAllButton();

            DateTime fromDate = DateTime.Today.AddDays(-6);
            DateTime toDate = DateTime.Today;

            creditControl.LoadFilteredCreditData("spFilterCreditByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);
        }

        private void rbCreditThisYear_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbCreditThisYear.Checked) return;
            UpdateCreditCustomDatePanel();
            UpdateCreditClearAllButton();

            DateTime fromDate = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime toDate = new DateTime(DateTime.Today.Year, 12, 31);

            creditControl.LoadFilteredCreditData("spFilterCreditByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);
        }

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



        private void txtCreditMaxAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();
        }

        private void ComboBoxCreditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();

            if (ComboBoxCreditCategory.SelectedIndex <= 0)
            {
                ComboBoxCreditSubCategory.Visible = false;
                lblCreditSelectSubCategory.Visible = false;

                //creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                return;
            }

            ComboBoxCreditSubCategory.Visible = true;
            lblCreditSelectSubCategory.Visible = true;

            if (ComboBoxCreditCategory.SelectedValue == null)
                return;

            if (ComboBoxCreditCategory.SelectedValue is DataRowView)
                return;

            int categoryId = Convert.ToInt32(ComboBoxCreditCategory.SelectedValue);

            // Load Sub Category
            CommonUiFunction.LoadInComboBox(
                "spGetCreditSubCategoryByCategoryID",
                "Select SubCategory",
                ComboBoxCreditSubCategory,
                "@CategoryID",
                categoryId);

            // Filter Credit Data
            if (!creditControl.LoadFilteredCreditData(
                    "spFilterCreditByCategory",
                    "@CategoryID",
                    categoryId,
                    categoryId))
            {
                ComboBoxCreditSubCategory.SelectedIndex = 0;
            }
        }

        private void ComboBoxCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();

            if (creditControl == null || creditControl.IsDisposed)
                return;

            if (ComboBoxCreditSubCategory.SelectedIndex <= 0)
            {
                creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                return;
            }

            if (ComboBoxCreditCategory.SelectedValue == null ||
                ComboBoxCreditCategory.SelectedValue is DataRowView)
                return;

            if (ComboBoxCreditSubCategory.SelectedValue == null ||
                ComboBoxCreditSubCategory.SelectedValue is DataRowView)
                return;

            int categoryId = Convert.ToInt32(ComboBoxCreditCategory.SelectedValue);
            int subCategoryId = Convert.ToInt32(ComboBoxCreditSubCategory.SelectedValue);

            if (!creditControl.LoadFilteredCreditData(
                    "spFilterCreditByCategoryAndSubCategory",
                    Session.LogedInUser.GetUserId(),
                    "@CategoryID",
                    categoryId,
                    "@SubCategoryID",
                    subCategoryId))
            {
                ComboBoxCreditSubCategory.SelectedIndex = 0;
            }


        }
        //Lent Start

        private void pnlFilterLentHeader_Click(object sender, EventArgs e)
        {
            lentFilterOpen = !lentFilterOpen;

            pnlLentFilterContant.Visible = lentFilterOpen;
            pnlLentDateHeader.Visible = lentFilterOpen;
            pnlLentAmountHeader.Visible = lentFilterOpen;
            pnlLentPersonHeader.Visible = lentFilterOpen;
            pnlLentStatusHeader.Visible = lentFilterOpen;
            pnlLentPaymentHeader.Visible = lentFilterOpen;

            picFilterLentHeaderArrow.Image = lentFilterOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            if (!lentFilterOpen)
            {
                lentDateOpen = false;
                lentAmountOpen = false;
                lentPersonOpen = false;
                lentStatusOpen = false;
                lentPaymentOpen = false;

                pnlLentDateBody.Visible = false;
                pnlLentAmountBody.Visible = false;
                pnlLentPersonBody.Visible = false;
                pnlLentStatusBody.Visible = false;
                pnlLentPaymentBody.Visible = false;

                picLentFilterByDateArrow.Image = Properties.Resources.down;
                picLentFilterByAmountArrow.Image = Properties.Resources.down;
                picLentFilterByPersonArrow.Image = Properties.Resources.down;
                picLentFilterByStatusArrow.Image = Properties.Resources.down;
                picLentFilterByPaymentArrow.Image = Properties.Resources.down;
            }

            flowSidebar.Top = 0;
        }
        private void pnlLentDateHeader_Click(object sender, EventArgs e)
        {
            lentDateOpen = !lentDateOpen;

            lentAmountOpen = false;
            lentPersonOpen = false;
            lentStatusOpen = false;
            lentPaymentOpen = false;

            pnlLentDateBody.Visible = lentDateOpen;
            pnlLentAmountBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentStatusBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            picLentFilterByDateArrow.Image = lentDateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picLentFilterByAmountArrow.Image = Properties.Resources.down;
            picLentFilterByPersonArrow.Image = Properties.Resources.down;
            picLentFilterByStatusArrow.Image = Properties.Resources.down;
            picLentFilterByPaymentArrow.Image = Properties.Resources.down;

            if (!lentDateOpen)
            {
                rbLentThisMonth.Checked = false;
                rbLentLast7Days.Checked = false;
                rbLentThisYear.Checked = false;
                rbLentCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        private void pnlLentAmountHeader_Click(object sender, EventArgs e)
        {
            lentAmountOpen = !lentAmountOpen;

            lentDateOpen = false;
            lentPersonOpen = false;
            lentStatusOpen = false;
            lentPaymentOpen = false;

            pnlLentAmountBody.Visible = lentAmountOpen;
            pnlLentDateBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentStatusBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            picLentFilterByAmountArrow.Image = lentAmountOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picLentFilterByDateArrow.Image = Properties.Resources.down;
            picLentFilterByPersonArrow.Image = Properties.Resources.down;
            picLentFilterByStatusArrow.Image = Properties.Resources.down;
            picLentFilterByPaymentArrow.Image = Properties.Resources.down;

            flowSidebar.Top = 0;
        }

        private void pnlLentPersonHeader_Click(object sender, EventArgs e)
        {
            lentPersonOpen = !lentPersonOpen;

            lentDateOpen = false;
            lentAmountOpen = false;
            lentStatusOpen = false;
            lentPaymentOpen = false;

            pnlLentPersonBody.Visible = lentPersonOpen;
            pnlLentDateBody.Visible = false;
            pnlLentAmountBody.Visible = false;
            pnlLentStatusBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            picLentFilterByPersonArrow.Image = lentPersonOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picLentFilterByDateArrow.Image = Properties.Resources.down;
            picLentFilterByAmountArrow.Image = Properties.Resources.down;
            picLentFilterByStatusArrow.Image = Properties.Resources.down;
            picLentFilterByPaymentArrow.Image = Properties.Resources.down;

            UpdateLentClearAllButton();

            flowSidebar.Top = 0;

            //load status from db on click to Person drop down 
            Common.CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", ComboBoxLentPerson);


        }

        private void pnlLentStatusHeader_Click(object sender, EventArgs e)
        {
            lentStatusOpen = !lentStatusOpen;

            lentDateOpen = false;
            lentAmountOpen = false;
            lentPersonOpen = false;
            lentPaymentOpen = false;

            pnlLentStatusBody.Visible = lentStatusOpen;
            pnlLentDateBody.Visible = false;
            pnlLentAmountBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentPaymentBody.Visible = false;

            picLentFilterByStatusArrow.Image = lentStatusOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picLentFilterByDateArrow.Image = Properties.Resources.down;
            picLentFilterByAmountArrow.Image = Properties.Resources.down;
            picLentFilterByPersonArrow.Image = Properties.Resources.down;
            picLentFilterByPaymentArrow.Image = Properties.Resources.down;

            UpdateLentClearAllButton();
            flowSidebar.Top = 0;
            //load status from db on click to status drop down 
            Common.CommonUiFunction.LoadInComboBox("spGetAllLentBorrowStatus", "Select Status", ComboBoxLentStatus);

        }

        private void pnlLentPaymentHeader_Click(object sender, EventArgs e)
        {
            lentPaymentOpen = !lentPaymentOpen;

            lentDateOpen = false;
            lentAmountOpen = false;
            lentPersonOpen = false;
            lentStatusOpen = false;

            pnlLentPaymentBody.Visible = lentPaymentOpen;
            pnlLentDateBody.Visible = false;
            pnlLentAmountBody.Visible = false;
            pnlLentPersonBody.Visible = false;
            pnlLentStatusBody.Visible = false;

            picLentFilterByPaymentArrow.Image = lentPaymentOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picLentFilterByDateArrow.Image = Properties.Resources.down;
            picLentFilterByAmountArrow.Image = Properties.Resources.down;
            picLentFilterByPersonArrow.Image = Properties.Resources.down;
            picLentFilterByStatusArrow.Image = Properties.Resources.down;

            UpdateLentClearAllButton();

            flowSidebar.Top = 0;

            //load status from db on click to status drop down 
            Common.CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select PaymentType", ComboBoxLentPayment);
        }

        private void btnLentClearAll_Click(object sender, EventArgs e)
        {
            rbLentThisMonth.Checked = false;
            rbLentLast7Days.Checked = false;
            rbLentThisYear.Checked = false;
            rbLentCustom.Checked = false;

            txtLentMinAmount.Clear();
            txtLentMaxAmount.Clear();

            //ComboBoxLentPerson.SelectedIndex = 0;
            //ComboBoxLentStatus.SelectedIndex = 0;
            //ComboBoxLentPayment.SelectedIndex = 0;

            dtpLentFromDate.Value = DateTime.Today;
            dtpLentToDate.Value = DateTime.Today;

            btnLentClearAll.Visible = false;

            lentControl.LoadLentData(Session.LogedInUser.GetUserId());
        }

        private void UpdateLentClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbLentThisMonth.Checked ||
                rbLentLast7Days.Checked ||
                rbLentThisYear.Checked ||
                rbLentCustom.Checked)
            {
                hasFilter = true;
            }

            // Amount
            if (txtLentMinAmount.Text.Trim() != "" ||
                txtLentMaxAmount.Text.Trim() != "")
            {
                hasFilter = true;
            }

            // Person
            if (ComboBoxLentPerson.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Status
            if (ComboBoxLentStatus.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Payment Method
            if (ComboBoxLentPayment.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnLentClearAll.Visible = hasFilter;
        }

        private void rbLentCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLentCustomDatePanel();
            UpdateLentClearAllButton();
        }

        private void UpdateLentCustomDatePanel()
        {
            pnlLentCustomDate.Visible = rbLentCustom.Checked;

            if (rbLentCustom.Checked)
                pnlLentDateBody.Height = 280;
            else
                pnlLentDateBody.Height = 120;
        }

        private void rbLentThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLentCustomDatePanel();
            UpdateLentClearAllButton();

            if (lentControl != null && !lentControl.IsDisposed)
            {


                DateTime firstDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                 1);

                DateTime lastDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                lentControl.LoadFilteredLentData("spFilterLentByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", firstDayOfMonth, "@ToDate", lastDayOfMonth);

            }




        }

        private void rbLentLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLentCustomDatePanel();
            UpdateLentClearAllButton();

            DateTime fromDate = DateTime.Today.AddDays(-6);
            DateTime toDate = DateTime.Today;

            lentControl.LoadFilteredLentData("spFilterLentByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);

        }

        private void rbLentThisYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLentCustomDatePanel();
            UpdateLentClearAllButton();

            DateTime fromDate = new DateTime(DateTime.Today.Year, 1, 1);

            DateTime toDate = new DateTime(DateTime.Today.Year, 12, 31);
            lentControl.LoadFilteredLentData("spFilterLentByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);

        }

        private void txtLentMinAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();

        }

        private void txtLentMaxAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();
        }

        private void ComboBoxLentPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();

            if (lentControl != null && !lentControl.IsDisposed)
            {
                if (ComboBoxLentPerson.SelectedIndex > 0)
                {
                    int filterId = Convert.ToInt32(ComboBoxLentPerson.SelectedValue);
                    //MessageBox.Show(filterId.ToString());
                    if (!lentControl.LoadFilteredLentData("spFilterLentByPerson", "@PersonID", filterId))
                    {
                        ComboBoxLentPerson.SelectedIndex = 0;
                    }
                }
                else
                {
                    lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void ComboBoxLentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();
            if (lentControl != null && !lentControl.IsDisposed)
            {
                if (ComboBoxLentStatus.SelectedIndex > 0)
                {
                    int statusId = Convert.ToInt32(ComboBoxLentStatus.SelectedValue);
                    if (!lentControl.LoadFilteredLentData("spFilterLentByStatus", "@StatusID", statusId))
                    {
                        ComboBoxLentStatus.SelectedIndex = 0;
                    }
                }
                else
                {
                    lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                }
            }
            //MessageBox.Show(ComboBoxLentStatus.SelectedIndex.ToString());

        }

        private void ComboBoxLentPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();
        }
        private void pnlAllLent_Click(object sender, EventArgs e)
        {
            SetActiveLentSubMenu(pnlAllLent);

            if (lentControl != null && !lentControl.IsDisposed)
            {
                lentControl.LoadLentData(Session.LogedInUser.GetUserId());
            }

            ShowPage(pnlLentPage);
        }

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

        private void pnlFilterBorrowHeader_Click(object sender, EventArgs e)
        {
            borrowFilterOpen = !borrowFilterOpen;

            pnlBorrowFilterContant.Visible = borrowFilterOpen;
            pnlBorrowDateHeader.Visible = borrowFilterOpen;
            pnlBorrowAmountHeader.Visible = borrowFilterOpen;
            pnlBorrowPersonHeader.Visible = borrowFilterOpen;
            pnlBorrowStatusHeader.Visible = borrowFilterOpen;
            pnlBorrowPaymentHeader.Visible = borrowFilterOpen;

            picFilterBorrowHeaderArrow.Image = borrowFilterOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            if (!borrowFilterOpen)
            {
                borrowDateOpen = false;
                borrowAmountOpen = false;
                borrowPersonOpen = false;
                borrowStatusOpen = false;
                borrowPaymentOpen = false;

                pnlBorrowDateBody.Visible = false;
                pnlBorrowAmountBody.Visible = false;
                pnlBorrowPersonBody.Visible = false;
                pnlBorrowStatusBody.Visible = false;
                pnlBorrowPaymentBody.Visible = false;

                picBorrowFilterByDateArrow.Image = Properties.Resources.down;
                picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
                picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
                picBorrowFilterByStatusArrow.Image = Properties.Resources.down;
                picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;
            }

            flowSidebar.Top = 0;
        }

        private void pnlBorrowDateHeader_Click(object sender, EventArgs e)
        {
            borrowDateOpen = !borrowDateOpen;

            borrowAmountOpen = false;
            borrowPersonOpen = false;
            borrowStatusOpen = false;
            borrowPaymentOpen = false;

            pnlBorrowDateBody.Visible = borrowDateOpen;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            picBorrowFilterByDateArrow.Image = borrowDateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
            picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
            picBorrowFilterByStatusArrow.Image = Properties.Resources.down;
            picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;

            if (!borrowDateOpen)
            {
                rbBorrowThisMonth.Checked = false;
                rbBorrowLast7Days.Checked = false;
                rbBorrowThisYear.Checked = false;
                rbBorrowCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        private void pnlBorrowAmountHeader_Click(object sender, EventArgs e)
        {
            borrowAmountOpen = !borrowAmountOpen;

            borrowDateOpen = false;
            borrowPersonOpen = false;
            borrowStatusOpen = false;
            borrowPaymentOpen = false;

            pnlBorrowAmountBody.Visible = borrowAmountOpen;
            pnlBorrowDateBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            picBorrowFilterByAmountArrow.Image = borrowAmountOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picBorrowFilterByDateArrow.Image = Properties.Resources.down;
            picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
            picBorrowFilterByStatusArrow.Image = Properties.Resources.down;
            picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;

            flowSidebar.Top = 0;
        }

        private void pnlBorrowPersonHeader_Click(object sender, EventArgs e)
        {
            borrowPersonOpen = !borrowPersonOpen;

            borrowDateOpen = false;
            borrowAmountOpen = false;
            borrowStatusOpen = false;
            borrowPaymentOpen = false;

            pnlBorrowPersonBody.Visible = borrowPersonOpen;
            pnlBorrowDateBody.Visible = false;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            picBorrowFilterByPersonArrow.Image = borrowPersonOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picBorrowFilterByDateArrow.Image = Properties.Resources.down;
            picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
            picBorrowFilterByStatusArrow.Image = Properties.Resources.down;
            picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;

            UpdateBorrowClearAllButton();

            flowSidebar.Top = 0;

            Common.CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", ComboBoxBorrowPerson);
        }

        private void pnlBorrowStatusHeader_Click(object sender, EventArgs e)
        {
            borrowStatusOpen = !borrowStatusOpen;

            borrowDateOpen = false;
            borrowAmountOpen = false;
            borrowPersonOpen = false;
            borrowPaymentOpen = false;

            pnlBorrowStatusBody.Visible = borrowStatusOpen;
            pnlBorrowDateBody.Visible = false;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowPaymentBody.Visible = false;

            picBorrowFilterByStatusArrow.Image = borrowStatusOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picBorrowFilterByDateArrow.Image = Properties.Resources.down;
            picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
            picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
            picBorrowFilterBySPaymentArrow.Image = Properties.Resources.down;

            UpdateBorrowClearAllButton();

            flowSidebar.Top = 0;
            Common.CommonUiFunction.LoadInComboBox("spGetAllLentBorrowStatus", "Select Status", ComboBoxBorrowStatus);
        }

        private void pnlBorrowPaymentHeader_Click(object sender, EventArgs e)
        {
            borrowPaymentOpen = !borrowPaymentOpen;

            borrowDateOpen = false;
            borrowAmountOpen = false;
            borrowPersonOpen = false;
            borrowStatusOpen = false;

            pnlBorrowPaymentBody.Visible = borrowPaymentOpen;
            pnlBorrowDateBody.Visible = false;
            pnlBorrowAmountBody.Visible = false;
            pnlBorrowPersonBody.Visible = false;
            pnlBorrowStatusBody.Visible = false;

            picBorrowFilterBySPaymentArrow.Image = borrowPaymentOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picBorrowFilterByDateArrow.Image = Properties.Resources.down;
            picBorrowFilterByAmountArrow.Image = Properties.Resources.down;
            picBorrowFilterByPersonArrow.Image = Properties.Resources.down;
            picBorrowFilterByStatusArrow.Image = Properties.Resources.down;

            UpdateBorrowClearAllButton();

            flowSidebar.Top = 0;
            Common.CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select PaymentType", ComboBoxBorrowPayment);
        }

        private void btnBorrowClearAll_Click(object sender, EventArgs e)
        {
            rbBorrowThisMonth.Checked = false;
            rbBorrowLast7Days.Checked = false;
            rbBorrowThisYear.Checked = false;
            rbBorrowCustom.Checked = false;

            txtBorrowMinAmount.Clear();
            txtBorrowMaxAmount.Clear();

            //ComboBoxBorrowPerson.SelectedIndex = 0;
            //ComboBoxBorrowStatus.SelectedIndex = 0;
            //ComboBoxBorrowPayment.SelectedIndex = 0;

            dtpBorrowFromDate.Value = DateTime.Today;
            dtpBorrowToDate.Value = DateTime.Today;

            btnBorrowClearAll.Visible = false;

            this.borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void UpdateBorrowClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbBorrowThisMonth.Checked ||
                rbBorrowLast7Days.Checked ||
                rbBorrowThisYear.Checked ||
                rbBorrowCustom.Checked)
            {
                hasFilter = true;
            }

            // Amount
            if (txtBorrowMinAmount.Text.Trim() != "" ||
                txtBorrowMaxAmount.Text.Trim() != "")
            {
                hasFilter = true;
            }

            // Person
            if (ComboBoxBorrowPerson.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Status
            if (ComboBoxBorrowStatus.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Payment Method
            if (ComboBoxBorrowPayment.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnBorrowClearAll.Visible = hasFilter;
        }

        private void rbBorrowCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBorrowCustomDatePanel();
            UpdateBorrowClearAllButton();
        }

        private void UpdateBorrowCustomDatePanel()
        {
            pnlBorrowCustomDate.Visible = rbBorrowCustom.Checked;

            if (rbBorrowCustom.Checked)
                pnlBorrowDateBody.Height = 280;
            else
                pnlBorrowDateBody.Height = 120;
        }

        private void rbBorrowThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBorrowCustomDatePanel();
            UpdateBorrowClearAllButton();




            if (borrowControls != null && !borrowControls.IsDisposed)
            {


                DateTime firstDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                 1);

                DateTime lastDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                //MessageBox.Show(firstDayOfMonth.ToString());
                //MessageBox.Show(lastDayOfMonth.ToString());


                borrowControls.LoadFilteredBorrowData("spFilterBorrowByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", firstDayOfMonth, "@ToDate", lastDayOfMonth);

            }
            //LentUi.retriveDataByUserIdAndFilterIdAtUi("spFilterLentByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", "@ToDate");
        }

        private void rbBorrowLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBorrowCustomDatePanel();
            UpdateBorrowClearAllButton();

            DateTime fromDate = DateTime.Today.AddDays(-6);
            DateTime toDate = DateTime.Today;

            borrowControls.LoadFilteredBorrowData("spFilterBorrowByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);

        }

        private void rbBorrowThisYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBorrowCustomDatePanel();
            UpdateBorrowClearAllButton();

            DateTime fromDate = new DateTime(DateTime.Today.Year, 1, 1);

            DateTime toDate = new DateTime(DateTime.Today.Year, 12, 31);
            borrowControls.LoadFilteredBorrowData("spFilterBorrowByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);

        }

        private void txtBorrowMinAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateBorrowClearAllButton();
        }

        private void txtBorrowMaxAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateBorrowClearAllButton();
        }

        private void ComboBoxBorrowPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBorrowClearAllButton();



            if (borrowControls != null && !borrowControls.IsDisposed)
            {

                if (ComboBoxBorrowPerson.SelectedIndex > 0)
                {
                    int filterId = Convert.ToInt32(ComboBoxBorrowPerson.SelectedValue);
                    //MessageBox.Show(filterId.ToString());
                    if (!borrowControls.LoadFilteredBorrowtData("spFilterBorrowByPerson", "@PersonID", filterId))
                    {
                        ComboBoxBorrowPerson.SelectedIndex = 0;
                    }
                }
                else
                {
                    borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void ComboBoxBorrowStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBorrowClearAllButton();

            if (borrowControls != null && !borrowControls.IsDisposed)
            {

                if (ComboBoxBorrowStatus.SelectedIndex > 0)
                {
                    int statusId = Convert.ToInt32(ComboBoxBorrowStatus.SelectedValue);

                    if (!borrowControls.LoadFilteredBorrowtData("spFilterBorrowByStatus", "@StatusID", statusId))
                    {
                        ComboBoxBorrowStatus.SelectedIndex = 0;
                    }
                }
                else
                {

                    borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void ComboBoxBorrowPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBorrowClearAllButton();
            if (borrowControls != null && !borrowControls.IsDisposed)


                if (ComboBoxBorrowPayment.SelectedIndex > 0)
                {
                    int filterId = Convert.ToInt32(ComboBoxBorrowPayment.SelectedValue);
                    //MessageBox.Show(filterId.ToString());
                    if (!borrowControls.LoadFilteredBorrowtData("spFilterBorrowByPaymentMethod", "@PaymentID", filterId))
                    {
                        ComboBoxBorrowPayment.SelectedIndex = 0;
                    }
                }
                else
                {
                    borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                }
        }



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
        private void pnlFilterTaskHeader_Click(object sender, EventArgs e)
        {
            taskFilterOpen = !taskFilterOpen;

            pnlTaskFilterContant.Visible = taskFilterOpen;
            pnlTaskDateHeader.Visible = taskFilterOpen;
            pnlTaskStatusHeader.Visible = taskFilterOpen;
            pnlTaskPriorityHeader.Visible = taskFilterOpen;

            picFilterTaskHeaderArrow.Image = taskFilterOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            if (!taskFilterOpen)
            {
                taskDateOpen = false;
                taskStatusOpen = false;
                taskPriorityOpen = false;

                pnlTaskDateBody.Visible = false;
                pnlTaskStatusBody.Visible = false;
                pnlTaskPriorityBody.Visible = false;

                picTaskFilterByDateArrow.Image = Properties.Resources.down;
                picTaskFilterByStatusArrow.Image = Properties.Resources.down;
                picTaskFilterByPriorityArrow.Image = Properties.Resources.down;
            }
            flowSidebar.Top = 0;
        }

        private void pnlTaskDateHeader_Click(object sender, EventArgs e)
        {
            taskDateOpen = !taskDateOpen;

            taskStatusOpen = false;
            taskPriorityOpen = false;

            pnlTaskDateBody.Visible = taskDateOpen;
            pnlTaskStatusBody.Visible = false;
            pnlTaskPriorityBody.Visible = false;

            picTaskFilterByDateArrow.Image = taskDateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picTaskFilterByStatusArrow.Image = Properties.Resources.down;
            picTaskFilterByPriorityArrow.Image = Properties.Resources.down;

            if (!taskDateOpen)
            {
                rbTaskThisMonth.Checked = false;
                rbTaskLast7Days.Checked = false;
                rbTaskThisYear.Checked = false;
                rbTaskCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        private void pnlTaskStatusHeader_Click(object sender, EventArgs e)
        {
            taskStatusOpen = !taskStatusOpen;

            taskDateOpen = false;
            taskPriorityOpen = false;

            pnlTaskStatusBody.Visible = taskStatusOpen;
            pnlTaskDateBody.Visible = false;
            pnlTaskPriorityBody.Visible = false;

            picTaskFilterByStatusArrow.Image = taskStatusOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picTaskFilterByDateArrow.Image = Properties.Resources.down;
            picTaskFilterByPriorityArrow.Image = Properties.Resources.down;

            UpdateTaskClearAllButton();

            flowSidebar.Top = 0;

            //load status from db on click to Person drop down 
            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", ComboBoxTaskStatus);
        }

        private void pnlTaskPriorityHeader_Click(object sender, EventArgs e)
        {
            taskPriorityOpen = !taskPriorityOpen;

            taskDateOpen = false;
            taskStatusOpen = false;

            pnlTaskPriorityBody.Visible = taskPriorityOpen;
            pnlTaskDateBody.Visible = false;
            pnlTaskStatusBody.Visible = false;

            picTaskFilterByPriorityArrow.Image = taskPriorityOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picTaskFilterByDateArrow.Image = Properties.Resources.down;
            picTaskFilterByStatusArrow.Image = Properties.Resources.down;

            UpdateTaskClearAllButton();

            flowSidebar.Top = 0;

            //load status from db on click to Person drop down 
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select Priority", ComboBoxTaskPriority);
        }

        private void btnTaskClearAll_Click(object sender, EventArgs e)
        {
            rbTaskThisMonth.Checked = false;
            rbTaskLast7Days.Checked = false;
            rbTaskThisYear.Checked = false;
            rbTaskCustom.Checked = false;

            ComboBoxTaskStatus.SelectedIndex = 0;
            ComboBoxTaskPriority.SelectedIndex = 0;

            dtpTaskFromDate.Value = DateTime.Today;
            dtpTaskToDate.Value = DateTime.Today;

            btnTaskClearAll.Visible = false;
        }

        private void UpdateTaskClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbTaskThisMonth.Checked ||
                rbTaskLast7Days.Checked ||
                rbTaskThisYear.Checked ||
                rbTaskCustom.Checked)
            {
                hasFilter = true;
            }

            // Status
            if (ComboBoxTaskStatus.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Priority
            if (ComboBoxTaskPriority.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnTaskClearAll.Visible = hasFilter;
        }

        private void rbTaskCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTaskCustomDatePanel();
            UpdateTaskClearAllButton();
        }

        private void UpdateTaskCustomDatePanel()
        {
            pnlTaskCustomDate.Visible = rbTaskCustom.Checked;

            if (rbTaskCustom.Checked)
                pnlTaskDateBody.Height = 280;
            else
                pnlTaskDateBody.Height = 120;
        }

        private void rbTaskThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTaskCustomDatePanel();
            UpdateTaskClearAllButton();

            if (taskControl != null && !taskControl.IsDisposed)
            {


                DateTime firstDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                 1);

                DateTime lastDayOfMonth = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                taskControl.LoadFilteredTaskData("spGetTasksBetweenDates", Session.LogedInUser.GetUserId(), "@FromDate", firstDayOfMonth, "@ToDate", lastDayOfMonth);

            }

        }

        private void rbTaskLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTaskCustomDatePanel();
            UpdateTaskClearAllButton();

            DateTime fromDate = DateTime.Today.AddDays(-6);
            DateTime toDate = DateTime.Today;

            taskControl.LoadFilteredTaskData("spGetTasksBetweenDates", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);
        }

        private void rbTaskThisYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTaskCustomDatePanel();
            UpdateTaskClearAllButton();

            DateTime fromDate = new DateTime(DateTime.Today.Year, 1, 1);

            DateTime toDate = new DateTime(DateTime.Today.Year, 12, 31);
            taskControl.LoadFilteredTaskData("spGetTasksBetweenDates", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate);
        }

        private void ComboBoxTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTaskClearAllButton();
            if (taskControl != null && !taskControl.IsDisposed)
            {
                if (ComboBoxTaskStatus.SelectedIndex > 0)
                {
                    int statusId = Convert.ToInt32(ComboBoxTaskStatus.SelectedValue);
                    if (!taskControl.LoadFilteredTaskData("spFilterTasksByStatus", "@TaskStatusID", statusId))
                    {
                        ComboBoxTaskStatus.SelectedIndex = 0;
                    }
                }
                else
                {
                    taskControl.LoadTaskData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void ComboBoxTaskPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTaskClearAllButton();

            if (taskControl != null && !taskControl.IsDisposed)
            {
                if (ComboBoxTaskPriority.SelectedIndex > 0)
                {
                    int priorityId = Convert.ToInt32(ComboBoxTaskPriority.SelectedValue);
                    if (!taskControl.LoadFilteredTaskData("spFilterTasksByPriority", "@PriorityID", priorityId))
                    {
                        ComboBoxTaskPriority.SelectedIndex = 0;
                    }
                }
                else
                {
                    taskControl.LoadTaskData(Session.LogedInUser.GetUserId());
                }
            }
        }

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

        private void pnlFilterNoteHeader_Click(object sender, EventArgs e)
        {
            noteFilterOpen = !noteFilterOpen;

            pnlNoteFilterContant.Visible = noteFilterOpen;
            pnlNoteDateHeader.Visible = noteFilterOpen;
            pnlNoteStatusHeader.Visible = noteFilterOpen;
            pnlNotePriorityHeader.Visible = noteFilterOpen;

            picFilterNoteHeaderArrow.Image = noteFilterOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            if (!noteFilterOpen)
            {
                noteDateOpen = false;
                noteStatusOpen = false;
                notePriorityOpen = false;

                pnlNoteDateBody.Visible = false;
                pnlNoteStatusBody.Visible = false;
                pnlNotePriorityBody.Visible = false;

                picNoteFilterByDateArrow.Image = Properties.Resources.down;
                picTaskFilterByNoteArrow.Image = Properties.Resources.down;
                picNotePriorityArrow.Image = Properties.Resources.down;
            }

            flowSidebar.Top = 0;
        }

        private void pnlNoteDateHeader_Click(object sender, EventArgs e)
        {
            noteDateOpen = !noteDateOpen;

            noteStatusOpen = false;
            notePriorityOpen = false;

            pnlNoteDateBody.Visible = noteDateOpen;
            pnlNoteStatusBody.Visible = false;
            pnlNotePriorityBody.Visible = false;

            picNoteFilterByDateArrow.Image = noteDateOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picTaskFilterByNoteArrow.Image = Properties.Resources.down;
            picNotePriorityArrow.Image = Properties.Resources.down;

            if (!noteDateOpen)
            {
                rbNoteThisMonth.Checked = false;
                rbNoteLast7Days.Checked = false;
                rbNoteThisYear.Checked = false;
                rbNoteCustom.Checked = false;
            }

            flowSidebar.Top = 0;
        }

        //statusHeader
        private void pnlNoteStatusHeader_Click(object sender, EventArgs e)
        {
            noteStatusOpen = !noteStatusOpen;

            noteDateOpen = false;
            notePriorityOpen = false;

            pnlNoteStatusBody.Visible = noteStatusOpen;
            pnlNoteDateBody.Visible = false;
            pnlNotePriorityBody.Visible = false;

            picTaskFilterByNoteArrow.Image = noteStatusOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picNoteFilterByDateArrow.Image = Properties.Resources.down;
            picNotePriorityArrow.Image = Properties.Resources.down;

            UpdateNoteClearAllButton();

            flowSidebar.Top = 0;
        }

        private void pnlNotePriorityHeader_Click(object sender, EventArgs e)
        {
            notePriorityOpen = !notePriorityOpen;

            noteDateOpen = false;
            noteStatusOpen = false;

            pnlNotePriorityBody.Visible = notePriorityOpen;
            pnlNoteDateBody.Visible = false;
            pnlNoteStatusBody.Visible = false;

            picNotePriorityArrow.Image = notePriorityOpen
                ? Properties.Resources.arrowhead_up
                : Properties.Resources.down;

            picNoteFilterByDateArrow.Image = Properties.Resources.down;
            picTaskFilterByNoteArrow.Image = Properties.Resources.down;

            UpdateNoteClearAllButton();

            flowSidebar.Top = 0;
        }

        private void btnNoteClearAll_Click(object sender, EventArgs e)
        {
            rbNoteThisMonth.Checked = false;
            rbNoteLast7Days.Checked = false;
            rbNoteThisYear.Checked = false;
            rbNoteCustom.Checked = false;

            ComboBoxNoteStatus.SelectedIndex = 0;
            ComboBoxNotePriority.SelectedIndex = 0;

            dtpNoteFromDate.Value = DateTime.Today;
            dtpNoteToDate.Value = DateTime.Today;

            btnNoteClearAll.Visible = false;
        }

        private void UpdateNoteClearAllButton()
        {
            bool hasFilter = false;

            // Date
            if (rbNoteThisMonth.Checked ||
                rbNoteLast7Days.Checked ||
                rbNoteThisYear.Checked ||
                rbNoteCustom.Checked)
            {
                hasFilter = true;
            }

            // Status
            if (ComboBoxNoteStatus.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            // Priority
            if (ComboBoxNotePriority.SelectedIndex > 0)
            {
                hasFilter = true;
            }

            btnNoteClearAll.Visible = hasFilter;
        }

        private void rbNoteCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNoteCustomDatePanel();
            UpdateNoteClearAllButton();
        }

        private void UpdateNoteCustomDatePanel()
        {
            pnlNoteCustomDate.Visible = rbNoteCustom.Checked;

            if (rbNoteCustom.Checked)
                pnlNoteDateBody.Height = 280;
            else
                pnlNoteDateBody.Height = 120;
        }

        private void rbNoteThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNoteCustomDatePanel();
            UpdateNoteClearAllButton();
        }

        private void rbNoteLast7Days_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNoteCustomDatePanel();
            UpdateNoteClearAllButton();
        }

        private void rbNoteThisYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNoteCustomDatePanel();
            UpdateNoteClearAllButton();
        }

        private void ComboBoxNoteStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNoteClearAllButton();
        }

        private void ComboBoxNotePriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNoteClearAllButton();
        }

        private void pnlAllNote_Click(object sender, EventArgs e)
        {
            SetActiveNoteSubMenu(pnlAllNote);
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
            SetActiveSettingSubMenu(pnlLogout);
            pnlTop.Visible = false;


            PersonLogOutControls personLogOutControls = new PersonLogOutControls();
            personLogOutControls.Show();

            RefreshSidebarScroll();
            ExpandSidebar();


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
            SetActiveSettingSubMenu(pnlSettingChangesPassword);

            ChangePasswordControls changePasswordControls = new ChangePasswordControls();
            changePasswordControls.Show();
            RefreshSidebarScroll();
            ExpandSidebar();
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

        public void RefreshStatusDropDown(int index)
        {
            ComboBoxLentStatus.SelectedIndex = index;
        }

        private void ComboBoxLentPayment_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateLentClearAllButton();

            if (lentControl != null && !lentControl.IsDisposed)
            {
                if (ComboBoxLentPayment.SelectedIndex > 0)
                {
                    int filterId = Convert.ToInt32(ComboBoxLentPayment.SelectedValue);
                    //MessageBox.Show(filterId.ToString());
                    if (!lentControl.LoadFilteredLentData("spFilterLentByPaymentMethod", "@PaymentID", filterId))
                    {
                        ComboBoxLentPayment.SelectedIndex = 0;
                    }
                }
                else
                {
                    lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                }
            }

        }

        private void btnLentApplyDateFilter_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
            errorProvider1.Clear();

            MainUI mainUi = new MainUI();
            mainUi.fromDate = dtpLentFromDate.Value.Date;
            mainUi.toDate = dtpLentToDate.Value.Date;


            CommonValidator.ValidationResult result = mainUi.InsertDateDataIntoMainUi();

            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    if (lentControl != null && !lentControl.IsDisposed)
                    {
                        if (!lentControl.LoadFilteredLentData("spFilterLentByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", dtpLentFromDate.Value.Date, "@ToDate", dtpLentToDate.Value.Date))
                        {
                            lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                            //MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                    }
                    //MessageBox.Show("Apply successfully!");

                    break;
                case CommonValidator.ValidationResult.DateRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, dtpLentFromDate, dtpLentToDate);
                    break;

            }

        }



        private void btnLentApplyAmountFilter_Click(object sender, EventArgs e)
        {
            //Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            MainUI mainUi = new MainUI();
            // Assign values from the form controls to the object

            mainUi.minAmount = txtLentMinAmount.Text;
            mainUi.maxAmount = txtLentMaxAmount.Text;

            CommonValidator.ValidationResult result = mainUi.InsertAmountDataIntoMainUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    if (lentControl != null && !lentControl.IsDisposed)
                    {
                        if (!lentControl.LoadFilteredLentData("spFilterLentByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", Convert.ToDecimal(txtLentMinAmount.Text), "@MaxAmount", Convert.ToDecimal(txtLentMaxAmount.Text)))
                        {
                            lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                            MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        lentControl.LoadLentData(Session.LogedInUser.GetUserId());
                    }

                    //this.Close();
                    break;

                case CommonValidator.ValidationResult.MinimumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentMinAmount);
                    break;

                case CommonValidator.ValidationResult.MaximumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentMaxAmount);
                    break;

                case CommonValidator.ValidationResult.AmountRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentMinAmount, txtLentMaxAmount);
                    break;
            }
        }

        private void btnBorrowApplyDateFilter_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
            errorProvider1.Clear();

            MainUI mainUi = new MainUI();
            mainUi.fromDate = dtpBorrowFromDate.Value.Date;
            mainUi.toDate = dtpBorrowToDate.Value.Date;

            CommonValidator.ValidationResult result = mainUi.InsertDateDataIntoMainUi();

            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    if (borrowControls != null && !borrowControls.IsDisposed)
                    {
                        if (!borrowControls.LoadFilteredBorrowData("spFilterBorrowByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", mainUi.fromDate, "@ToDate", mainUi.toDate))
                        {
                            borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                            //MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                    }
                    //MessageBox.Show("Apply successfully!");

                    break;
                case CommonValidator.ValidationResult.DateRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, dtpLentFromDate, dtpLentToDate);
                    break;

            }

        }

        private void btnBorrowApplyAmountFilter_Click(object sender, EventArgs e)
        {
            //Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            MainUI mainUi = new MainUI();
            // Assign values from the form controls to the object

            mainUi.minAmount = txtBorrowMinAmount.Text;
            mainUi.maxAmount = txtBorrowMaxAmount.Text;

            CommonValidator.ValidationResult result = mainUi.InsertAmountDataIntoMainUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    if (borrowControls != null && !borrowControls.IsDisposed)
                    {

                        if (!borrowControls.LoadFilteredBorowData("spFilterBorrowByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", Convert.ToDecimal(txtBorrowMinAmount.Text), "@MaxAmount", Convert.ToDecimal(txtBorrowMaxAmount.Text)))
                        {
                            borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                            MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        borrowControls.LoadBorrowData(Session.LogedInUser.GetUserId());
                    }

                    //this.Close();
                    break;

                case CommonValidator.ValidationResult.MinimumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowMinAmount);
                    break;

                case CommonValidator.ValidationResult.MaximumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowMaxAmount);
                    break;

                case CommonValidator.ValidationResult.AmountRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowMinAmount, txtBorrowMaxAmount);
                    break;
            }
        }

        private void pnlLent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpLentFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCreditApplyDateFilter_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
            errorProvider1.Clear();

            MainUI mainUi = new MainUI();
            mainUi.fromDate = dtpCreditFromDate.Value.Date;
            mainUi.toDate = dtpCreditToDate.Value.Date;


            CommonValidator.ValidationResult result = mainUi.InsertDateDataIntoMainUi();

            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    if (creditControl != null && !creditControl.IsDisposed)
                    {
                        if (!creditControl.LoadFilteredCreditData("spFilterCreditByDateRange", Session.LogedInUser.GetUserId(), "@FromDate", dtpCreditFromDate.Value.Date, "@ToDate", dtpCreditToDate.Value.Date))
                        {
                            creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                            //MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                    }
                    //MessageBox.Show("Apply successfully!");

                    break;
                case CommonValidator.ValidationResult.DateRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, dtpCreditFromDate, dtpCreditToDate);
                    break;

            }
        }

        private void dtpLentToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCreditApplyAmountFilter_Click(object sender, EventArgs e)
        {
            //Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            MainUI mainUi = new MainUI();
            // Assign values from the form controls to the object

            mainUi.minAmount = txtCreditMinAmount.Text;
            mainUi.maxAmount = txtCreditMaxAmount.Text;

            CommonValidator.ValidationResult result = mainUi.InsertAmountDataIntoMainUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    if (creditControl != null && !creditControl.IsDisposed)
                    {
                        if (!creditControl.LoadFilteredCreditData("spFilterCreditByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", Convert.ToDecimal(txtCreditMinAmount.Text), "@MaxAmount", Convert.ToDecimal(txtCreditMaxAmount.Text)))
                        {
                            creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                            MessageBox.Show("No Specific Record Exist!");
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!");
                        }

                    }
                    else
                    {
                        creditControl.LoadCreditData(Session.LogedInUser.GetUserId());
                    }

                    //this.Close();
                    break;

                case CommonValidator.ValidationResult.MinimumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCreditMinAmount);
                    break;

                case CommonValidator.ValidationResult.MaximumAmountInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCreditMaxAmount);
                    break;

                case CommonValidator.ValidationResult.AmountRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCreditMaxAmount, txtCreditMaxAmount);
                    break;
            }
        }

        private void txtMinAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();
        }

        private void txtCreditMinAmount_TextChanged(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();
        }

        private void txtCreditMaxAmount_TextChanged_1(object sender, EventArgs e)
        {
            UpdateCreditClearAllButton();
        }

        private void btnApplyDateFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnTaskApplyDateFilter_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
            errorProvider1.Clear();

            DateTime fromDate = dtpTaskFromDate.Value.Date;


            DateTime toDate = dtpTaskToDate.Value.Date.AddDays(1).AddTicks(-1);

            MainUI mainUi = new MainUI();
            mainUi.fromDate = fromDate;
            mainUi.toDate = toDate;

            CommonValidator.ValidationResult result = mainUi.InsertDateDataIntoMainUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    if (taskControl != null && !taskControl.IsDisposed)
                    {
                        if (!taskControl.LoadFilteredTaskData("spGetTasksBetweenCreatedDates", Session.LogedInUser.GetUserId(), "@FromDate", fromDate, "@ToDate", toDate))
                        {
                            MessageBox.Show("No record exists for the selected date range!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Apply successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;

                case CommonValidator.ValidationResult.DateRangeInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, dtpTaskFromDate, dtpTaskToDate);
                    break;
            }
        }

        private void pnlAddLent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlProfilePage_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

