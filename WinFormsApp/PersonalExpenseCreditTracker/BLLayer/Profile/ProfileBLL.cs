using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Profile;
using System.Text.RegularExpressions;
namespace BLLayer.Profile
{
   public class ProfileBLL
    {
        public int userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public byte[] photoData { get; set; }
       //update profile
        public bool UpdateUserProfileIntoProfBll()
        {
            if (ValidateFullName())
            {
                if (ValidateEmail())
                {
                    if (ValidatePhoneNumber())
                    {
                        if (ValidateAddress())
                        {
                            if (ValidateDateOfBirth())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

       //Update profile Photo
        public bool UpdateProfilePhotoIntoProfBll()
        {
            if (ValidatePhotoData())
            {
                return true;
            }
            return false;
        }

        private bool ValidatePhotoData()
        {
            if (photoData == null)
            {
                return false;
            }
            if (photoData.Length > 2 * 1024 * 1024)
            {
                return false;
            }
            return true;
        }
       private bool ValidateFullName()
       {
           if(string.IsNullOrWhiteSpace(fullName))
           {
               return false;
           }
           if(fullName.Length>100)
           {
               return false;
           }
           return true;
       }
        private bool ValidateEmail()
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
        private bool ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }
        private bool ValidateAddress()
        {
            if (address.Length > 200)
            {
                return false;
            }
            return true;
        }
        private bool ValidateDateOfBirth()
        {
            if (dateOfBirth > DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
