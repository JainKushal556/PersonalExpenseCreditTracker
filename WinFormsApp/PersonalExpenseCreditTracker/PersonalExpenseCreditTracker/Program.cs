using System;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Forms.Authentication;
using BLLayer.Authentication;

namespace PersonalExpenseCreditTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AuthBLL authBll = new AuthBLL();
            int activeUserId = authBll.GetUserIdFromDB();

            //If user already login
            if (activeUserId > 0)
            {
                Session.LogedInUser.SetUserId(activeUserId);
                Application.Run(new MainForm());
            }
            else  // If User Not login
            {
                LoginControls loginForm = new LoginControls();

                Application.Run(new LoginControls());
            }
        }
    }
}