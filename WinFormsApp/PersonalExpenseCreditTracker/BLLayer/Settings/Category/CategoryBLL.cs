using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using DALayer.Settings.Category;
using DALayer.Common;
using System.Data;

namespace BLLayer.Settings.Category
{
    public class CategoryBLL
    {
        public int UserId { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategory { get; set; }
        public int IsActive { get; set; }
        public int Inactive { get; set; }
        public int status { get; set; }
        string ErrorMsg;

        
        CommonValidator.ValidationResult result;

        CategoryDAL categoryDAL = new CategoryDAL();

        public CommonValidator.ValidationResult AddExpenseCategoryDataIntoCategoryBll()
        {
            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryName = CategoryName;

            if (categoryDAL.AddExpenseCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }


        public CommonValidator.ValidationResult AddExpenseSubCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(SubCategory);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.SubCategory = SubCategory;

            if (categoryDAL.AddExpenseSubCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }


        public CommonValidator.ValidationResult AddCreditCategoryDataIntoCategoryBll()
        {
            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryName = CategoryName;

            if (categoryDAL.AddCreditCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }


        public CommonValidator.ValidationResult AddCreditSubCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(SubCategory);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.SubCategory = SubCategory;

            if (categoryDAL.AddCreditSubCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult UpdateExpenseCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.CategoryName = CategoryName;

            if (categoryDAL.UpdateExpenseCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult UpdateCreditCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.CategoryName = CategoryName;

            if (categoryDAL.UpdateCreditCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult UpdateCreditSubCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.CategoryName = CategoryName;



            if (categoryDAL.UpdateCreditSubCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult UpdateExpenseSubCategoryDataIntoCategoryBll()
        {
            //CategoryID Validation
            result = CommonValidator.ValidateCategory(CategoryID);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Category Name Validation
            result = CommonValidator.ValidationCategoryName(CategoryName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (IsActive == 1)
                categoryDAL.status = 1;
            else
                categoryDAL.status = 0;

            categoryDAL.UserId = UserId;
            categoryDAL.CategoryID = CategoryID;
            categoryDAL.CategoryName = CategoryName;

            if (categoryDAL.UpdateExpenseSubCategoryDataIntoCategoryDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public string GetErrorMsg(string spName, string paramName1, string paramName2, string paramName3)
        {
            if (IsActive == 1)
                status = 1;
            else
                status = 0;

            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveErrorCategoryDataIntoCategory(spName, UserId, CategoryID, status, CategoryName, paramName1, paramName2, paramName3);

            if (dataTable.Rows.Count > 0)
            {
                ErrorMsg = dataTable.Rows[0]["MESSAGE"].ToString();
            }
            return ErrorMsg;
        }
    }
}
