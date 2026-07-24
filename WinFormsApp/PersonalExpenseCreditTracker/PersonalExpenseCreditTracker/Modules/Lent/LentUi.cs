using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Lent;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public class LentUi
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }

        private LentBLL lentBLL = new LentBLL();

        public CommonValidator.ValidationResult  InsertDataIntoLentUi()
        {
            lentBLL.userId = userId;
            lentBLL.lentId = lentId;
            lentBLL.personId = personId;
            lentBLL.paymentId = paymentId;
            lentBLL.statusId = statusId;
            lentBLL.amount = amount;
            lentBLL.deadlineAt = deadlineAt;
            lentBLL.description = description;

            return lentBLL.InsertDataIntoLentBll();
        }

        public static List<string> retriveListForComboBoxAtUi(string spName, string colName, int userId)
        {
            return LentBLL.retriveListForComboBoxAtBal(spName, colName, userId);
        }
        public static List<string> retriveListForComboBoxAtUi(string spName, string colName)
        {
            return LentBLL.retriveListForComboBoxAtBal(spName, colName);
        }
    }
}
