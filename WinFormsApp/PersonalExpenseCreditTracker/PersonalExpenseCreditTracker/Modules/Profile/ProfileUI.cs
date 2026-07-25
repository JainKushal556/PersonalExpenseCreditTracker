using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Profile;
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
        public byte[] photoData { get; set; }
        //Update Profile
        public bool UpdateUserProfileIntoProfUi()
        {
            ProfileBLL ProfBll = new ProfileBLL();
            ProfBll.userId = userId;
            ProfBll.fullName = fullName;
            ProfBll.email = email;
            ProfBll.phoneNumber = phoneNumber;
            ProfBll.address = address;
            ProfBll.dateOfBirth = dateOfBirth;

            return ProfBll.UpdateUserProfileIntoProfBll();
        }

        public bool UpdateProfilePhotoIntoProfUi()
        {
            ProfileBLL ProfBll = new ProfileBLL();
            ProfBll.userId = userId;
            ProfBll.photoData = photoData;

            return ProfBll.UpdateProfilePhotoIntoProfBll();
        }
    }
}
