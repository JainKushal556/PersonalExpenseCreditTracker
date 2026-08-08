using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Profile;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Modules.Profile
{
    public class ProfileUI
    {
        public int userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int genderId { get; set; }
        public byte[] photoData { get; set; }

        private ProfileBLL profBLL = new ProfileBLL();

        // Update Profile
        public CommonValidator.ValidationResult UpdateUserProfileIntoProfUi()
        {


            profBLL.userId = userId;
            profBLL.fullName = fullName;
            profBLL.email = email;
            profBLL.phoneNumber = phoneNumber;
            profBLL.address = address;
            profBLL.dateOfBirth = dateOfBirth;
            profBLL.genderId = genderId;

            return profBLL.UpdateUserProfileIntoProfBll();
        }

        // Update Profile Photo
        public CommonValidator.ValidationResult UpdateProfilePhotoIntoProfUi()
        {
  
            profBLL.userId = userId;
            profBLL.photoData = photoData;

            return profBLL.UpdateProfilePhotoIntoProfBll();
        }
    }
}
