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
        public int borrowId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public string paidAmount { get; set; }
        public string remainingAmount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }
        public string returnAmount { get; set; }
        public DateTime returnDate {get;set;}

        // Create an object of the Business Logic Layer
        private BorrowBLL borrowBLL = new BorrowBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoLentUi()
        {
            borrowBLL.userId = userId;
            borrowBLL.borrowId = borrowId;
            borrowBLL.personId = personId;
            borrowBLL.paymentId = paymentId;
            borrowBLL.statusId = statusId;
            borrowBLL.amount = amount;
            borrowBLL.deadlineAt = deadlineAt;
            borrowBLL.description = description;

            // Call the BLL method for validation
            return borrowBLL.DataValidatorIntoBorrowBll();
        }

        public CommonValidator.ValidationResult InsertPayBorrowIntoBorrowUi()
        {
            borrowBLL.userId = userId;
            borrowBLL.borrowId = borrowId;
            borrowBLL.returnAmount = returnAmount;
            borrowBLL.paymentId = paymentId;
            borrowBLL.returnDate = returnDate;
            borrowBLL.description = description;

            return borrowBLL.DataValidatorIntoPayBorrowBll();
        }

    }
}
