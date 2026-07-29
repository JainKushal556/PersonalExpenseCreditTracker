using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Borrow;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    class BorrowUI
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }

        // Create an object of the Business Logic Layer
        private BorrowBLL borrowBLL = new BorrowBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoLentUi()
        {
            borrowBLL.userId = userId;
            borrowBLL.lentId = lentId;
            borrowBLL.personId = personId;
            borrowBLL.paymentId = paymentId;
            borrowBLL.statusId = statusId;
            borrowBLL.amount = amount;
            borrowBLL.deadlineAt = deadlineAt;
            borrowBLL.description = description;

            // Call the BLL method for validation
            return borrowBLL.DataValidatorIntoLentBll();
        }
    }
}
