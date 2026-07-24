using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Modules.Lent;
namespace PersonalExpenseCreditTracker.Common
{
    public class CommonUiFunction
    {

        public static void LoadInComboBox(string spName, string colName, int userId, string initialText, ComboBox comboBox)
        {
            List<string> dataList = LentUi.retriveListForComboBoxAtUi(spName, colName,userId);
            comboBox.Items.Add(initialText);
            foreach (string person in dataList)
            {
                comboBox.Items.Add(person);
            }
        }
    }
}
