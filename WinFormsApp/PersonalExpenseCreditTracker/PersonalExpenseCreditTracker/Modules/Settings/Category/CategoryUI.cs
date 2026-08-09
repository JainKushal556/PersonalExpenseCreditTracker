using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using BLLayer.Settings.Category;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public class CategoryUI
    {
        public int UserId { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategory { get; set; }
        public int IsActive { get; set; }
        public int Inactive { get; set; }


        CategoryBLL categoryBLL = new CategoryBLL();

        public CommonValidator.ValidationResult AddExpenseCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.AddExpenseCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult AddExpenseSubCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.SubCategory = SubCategory;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.AddExpenseSubCategoryDataIntoCategoryBll();
        }


        public CommonValidator.ValidationResult AddCreditCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.AddCreditCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult AddCreditSubCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.SubCategory = SubCategory;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.AddCreditSubCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult UpdateExpenseCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.UpdateExpenseCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult UpdateCreditCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.UpdateCreditCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult UpdateCreditSubCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.UpdateCreditSubCategoryDataIntoCategoryBll();
        }

        public CommonValidator.ValidationResult UpdateExpenseSubCategoryDataIntoCategoryUI()
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.UpdateExpenseSubCategoryDataIntoCategoryBll();
        }

        public string GetErrorMsg(string spName, string paramName1, string paramName2, string paramName3)
        {
            categoryBLL.UserId = UserId;
            categoryBLL.CategoryID = CategoryID;
            categoryBLL.CategoryName = CategoryName;
            categoryBLL.Inactive = Inactive;
            categoryBLL.IsActive = IsActive;

            return categoryBLL.GetErrorMsg(spName, paramName1, paramName2, paramName3);
        }
    }
}
