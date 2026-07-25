using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Authentication;
namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public class AuthUI
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

        AuthBLL authBll = new AuthBLL();
        //Register Page
        public bool InsertDataIntoAuthUi()
        {
            
            authBll.userName = userName;
            authBll.email = email;
            authBll.phoneNumber = phoneNumber;
            authBll.password = password;
            return authBll.InsertDataIntoAuthBll();
        }

        //login page
        public bool LoginDataIntoAuthUi()
        {
            authBll.email = email;
            authBll.password = password;
            return authBll.LoginDataIntoAuthBll();
        }
        //Forget Password
        public bool ForgetPasswordIntoAuthUi()
        {
            authBll.email = email;
            return authBll.ForgetPasswordIntoAuthBll();
        }

        public bool ChangePasswordIntoAuthUi()
        {
            authBll.userId = userId;
            authBll.oldPassword = oldPassword;
            authBll.newPassword = newPassword;
            return authBll.ChangePasswordIntoAuthBll();
        }

        internal bool InsertDataIntoAuthUi(AuthUI authUi)
        {
            throw new NotImplementedException();
        }

        internal bool LoginDataIntoAuthUi(AuthUI authUi)
        {
            throw new NotImplementedException();
        }
    }
}
