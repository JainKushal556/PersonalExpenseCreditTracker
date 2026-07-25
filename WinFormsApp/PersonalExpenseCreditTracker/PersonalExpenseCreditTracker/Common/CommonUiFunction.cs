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

        public static void LoadInComboBox(string spName,int userId, string initialText, ComboBox comboBox)
        {
            DataTable dataTable = LentUi.retriveListForComboBoxAtUi(spName,userId);
            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }


        public static void LoadInComboBox(string spName, string initialText, ComboBox comboBox)
        {            
            DataTable dataTable = LentUi.retriveListForComboBoxAtUi(spName);
            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }
    }
}
