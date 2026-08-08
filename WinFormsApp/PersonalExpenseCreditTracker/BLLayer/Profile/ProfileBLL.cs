using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Profile;
using System.Text.RegularExpressions;
using BLLayer.Common;
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
        public int genderId { get; set; }
        public byte[] photoData { get; set; }

      private  ProfileDAL profileDAL = new ProfileDAL();

        //// Update Profile
      public CommonValidator.ValidationResult UpdateUserProfileIntoProfBll()
      {
          CommonValidator.ValidationResult result;

          result = CommonValidator.ValidateFullName(fullName);
          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

          result = CommonValidator.ValidateDateOfBirth(dateOfBirth);
          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

          result = CommonValidator.ValidateEmail(email);
          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

          result = CommonValidator.ValidatePhoneNumber(phoneNumber);
          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

          result = CommonValidator.ValidateGender(genderId);

          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

          result = CommonValidator.ValidateAddress(address);
          if (result != CommonValidator.ValidationResult.Success)
          {
              return result;
          }

         

          profileDAL.userId = userId;
          profileDAL.fullName = fullName;
          profileDAL.email = email;
          profileDAL.phoneNumber = phoneNumber;
          profileDAL.address = address;
          profileDAL.dateOfBirth = dateOfBirth;
          profileDAL.genderId = genderId;

          if (profileDAL.UpdateUserProfileToDb())
          {
              return CommonValidator.ValidationResult.Success;
          }
          else
          {

              return CommonValidator.ValidationResult.StoreProcedureError;
          }
      }

        // Update Profile Photo
        public CommonValidator.ValidationResult UpdateProfilePhotoIntoProfBll()
        {
            CommonValidator.ValidationResult result;

            result = CommonValidator.ValidatePhotoData(photoData);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

          

            profileDAL.userId = userId;
            profileDAL.photoData = photoData;

            if (profileDAL.UpdateProfilePhotoToDb())
                return CommonValidator.ValidationResult.Success;

            return CommonValidator.ValidationResult.StoreProcedureError;
        }
       

    }
}
