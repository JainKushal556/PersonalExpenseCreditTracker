using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Modules.Lent;
using System.Data;
namespace PersonalExpenseCreditTracker.Common
{
    public class CommonUiFunction
    {

        public static void LoadInComboBox(string spName, string colName, int userId, string initialText, ComboBox comboBox)
        {
            //// Retrieve data from the database
            ////List<string> dataList = LentUi.retriveListForComboBoxAtUi(spName, colName, userId);
            //DataTable dataTable = LentUi.retriveListForComboBoxAtUi(spName, colName, userId);
            //// Add the default placeholder item
            //comboBox.Items.Add(initialText);

            //// Add each retrieved item to the ComboBox
            ////foreach (string person in dataList)
            ////{
            ////    comboBox.Items.Add(person);
            ////}

            ////comboBox.DataSource = dataTable;
            ////comboBox.DisplayMember = colName;
            ////comboBox.ValueMember = PersonID;
            DataTable dataTable = LentUi.retriveListForComboBoxAtUi(spName, colName, userId);


        }

        public static void LoadInComboBox(string spName, string colName, string initialText, ComboBox comboBox)
        {
            // Retrieve data from the database
            //List<string> dataList = LentUi.retriveListForComboBoxAtUi(spName, colName);
            // Add the default placeholder item
            comboBox.Items.Add(initialText);
            // Add each retrieved item to the ComboBox
            //foreach (string data in dataList)
            //{
            //    comboBox.Items.Add(data);
            //}
        }
    }
}
