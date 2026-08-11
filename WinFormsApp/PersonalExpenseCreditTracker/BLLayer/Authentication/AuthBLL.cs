using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using DALayer.Common;
using DALayer.Authentication;
using System.Text.RegularExpressions;
namespace BLLayer.Authentication
{
    public class AuthBLL
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword {get;set;}
        public string confirmPassword { get; set; }
        public string message { get; set; }

        private AuthDAL authDal=new AuthDAL();


        // Stores the validation result
        CommonValidator.ValidationResult result;


        // Validates all user input before saving the data
        public CommonValidator.ValidationResult DataValidatorIntoAuthBll()
        {
            //UserName Validation
            result = CommonValidator.ValidateUserName(userName);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Email Validation
            result = CommonValidator.ValidateEmail(email);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Validation Phone Number
            result = CommonValidator.ValidatePhoneNumber(phoneNumber);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }
            //Validation Password
            result = CommonValidator.ValidatePassword(password);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Validation Confirm Password
            result = CommonValidator.ValidateConfirmPassword(password,confirmPassword);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            authDal.userName = userName;
            authDal.email = email;
            authDal.phoneNumber = phoneNumber;
            authDal.password = password;

            if (authDal.SaveRegisterToDb())
            {
                this.userId = authDal.userId;
                this.message = authDal.message;
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                this.userId = authDal.userId;
                this.message = authDal.message;
                return CommonValidator.ValidationResult.StoreProcedureError;
            }

        }

        public CommonValidator.ValidationResult LoginUserDataValidator()
        {
            //Email Empty Validation
            result = CommonValidator.ValidateLoginEmail(email);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            
            //Password Empty Validation
            result = CommonValidator.ValidateLoginPassword(password);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            authDal.email = email;
            authDal.password = password;

            //call Login Stored Procedure
            if (authDal.LoginUserToDb())
            {
                this.userId = authDal.userId;
                this.message = authDal.message;

                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                this.userId = authDal.userId;
                this.message = authDal.message;
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }

    
    }
}
