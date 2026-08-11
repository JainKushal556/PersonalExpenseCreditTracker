using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using BLLayer.Authentication;
namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public class AuthUI
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string message { get; set; }

        // Create an object of the Business Logic Layer
        AuthBLL authBll = new AuthBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoAuthUi()
        {
            authBll.userName = userName;
            authBll.email = email;
            authBll.phoneNumber = phoneNumber;
            authBll.password = password;
            authBll.confirmPassword = confirmPassword;

            CommonValidator.ValidationResult result = authBll.DataValidatorIntoAuthBll();
            this.userId = authBll.userId;
            this.message = authBll.message;

            return result;
        }

        //Login Page
        public CommonValidator.ValidationResult LoginUserIntoAuthUi()
        {
            authBll.email = email;
            authBll.password = password;

            CommonValidator.ValidationResult result = authBll.LoginUserDataValidator();
            //if(result==CommonValidator.ValidationResult.Success)
            //{
                this.userId=authBll.userId;
                this.message=authBll.message;
            //}
            return result;
        }

    }
}
