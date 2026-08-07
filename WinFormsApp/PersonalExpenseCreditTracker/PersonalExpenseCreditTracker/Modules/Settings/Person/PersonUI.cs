using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using BLLayer.Settings.Persons;
using System.Data;

namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    class PersonUI
    {
        public int userId { get; set; }
        public int personId { get; set; }
        public string personName { get; set; }
        public string personNumber { get; set; }
        public string address { get; set; }

        private PersonsBLL personsBLL = new PersonsBLL();

        public CommonValidator.ValidationResult InsertDataIntoPersonUi()
        {
            personsBLL.userId = userId;
            personsBLL.personId = personId;
            personsBLL.personName = personName;
            personsBLL.personNumber = personNumber;
            personsBLL.address = address;

            return personsBLL.DataValidatorIntoPersonBll();
        }

        public CommonValidator.ValidationResult UpdateDataIntoPersonUi()
        {
            personsBLL.userId = userId;
            personsBLL.personId = personId;
            personsBLL.personName = personName;
            personsBLL.personNumber = personNumber;
            personsBLL.address = address;

            return personsBLL.EditPersonValidator();
        }
    }
}