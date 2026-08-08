using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using DALayer.Settings.Person;
using System.Data;
using DALayer.Common;

namespace BLLayer.Settings.Persons
{
    public class PersonsBLL
    {
        public int userId { get; set; }
        public int personId { get; set; }
        public string personName { get; set; }
        public string personNumber { get; set; }
        public string address { get; set; }

        CommonValidator.ValidationResult result;

        private PersonDAL personDAL = new PersonDAL();

        public CommonValidator.ValidationResult DataValidatorIntoPersonBll()
        {
            // Person Name Validation
            result = CommonValidator.ValidationPersonName(personName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Person Phone Number Validation
            result = CommonValidator.ValidatePhoneNumber(personNumber);
            if (result != CommonValidator.ValidationResult.Success)
                return result;


            personDAL.userId = userId;
            personDAL.personId = personId;
            personDAL.personName = personName;
            personDAL.personNumber = personNumber;
            personDAL.address = address;

            if (personDAL.FindDuplicatePhoneNumberIntoDB())
            {
                return CommonValidator.ValidationResult.PhoneNumberAlreadyExists;
            }

            if (personDAL.SavePersonToDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult EditPersonValidator()
        {
            // PersonID Validation
            result = CommonValidator.ValidatePerson(personId);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Person Name Validation
            result = CommonValidator.ValidationPersonName(personName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Person Phone Number Validation
            result = CommonValidator.ValidatePhoneNumber(personNumber);
            if (result != CommonValidator.ValidationResult.Success)
                return result;


            personDAL.userId = userId;
            personDAL.personId = personId;
            personDAL.personName = personName;
            personDAL.personNumber = personNumber;
            personDAL.address = address;

            if (personDAL.FindDuplicatePhoneNumberIntoDB())
            {
                return CommonValidator.ValidationResult.PhoneNumberAlreadyExists;
            }

            if (personDAL.UpdatePersonToDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }
    }
}
