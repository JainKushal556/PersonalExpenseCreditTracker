using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Authentication;
using System.Text.RegularExpressions;
namespace BLLayer.Authentication
{
    public class AuthBLL
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword {get;set;}

        AuthDAL authDAL = new AuthDAL();

        private bool ValidUserName()
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }
            if (userName.Length > 50)
            {
                return false;
            }
            return Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$");

        }
        private bool ValidEmail()
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            if (email.Length > 100)
            {
                return false;
            }

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool ValidPhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }

        private bool ValidPassword()
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            if (password.Length < 6)
            {
                return false;
            }
            return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$");
        }

        private bool ValidOldPassword()
        {
            if(string.IsNullOrWhiteSpace(oldPassword))
            {
                return false;
            }
            return true;
        }

        private bool ValidNewPassword()
        {
            if(string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }
            if(newPassword.Length<6)
            {
                return false;
            }
            if(newPassword==oldPassword)
            {
                return false;
            }
            return true;
        }
        //Register Page
        public bool InsertDataIntoAuthBll()
        {
            if (ValidUserName())
            {
                if (ValidEmail())
                {
                    if (ValidPhoneNumber())
                    {
                        if (ValidPassword())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Login Page
        public bool LoginDataIntoAuthBll()
        {
            if (ValidEmail())
            {
                if (ValidPassword())
                {
                    return true;
                }
            }
            return false;
        }

        //Forget Password
        public bool ForgetPasswordIntoAuthBll()
        {
            if (ValidEmail())
            {
                return true;
            }
            return false;
        }

        //Change Password
        public bool ChangePasswordIntoAuthBll()
        {
            if (ValidOldPassword())
            {
                if (ValidNewPassword())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
