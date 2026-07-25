# 🚀 Developer Guide: How to Build a New Module (e.g., Add Expense)

যখনই আমরা কোনো নতুন মডিউল বানাবো (যেমন: **Add Expense**, **Add Borrow** ইত্যাদি), তখন ডাটাবেজ থেকে ডেটা নিয়ে ড্রপডাউন-এ লোড করা, ইনপুট চেক করা এবং ডাটাবেজে সেভ করার কাজগুলো নিচের **৫টি সহজ ধাপে** করতে হবে।

---

## 🛠️ Step-by-Step Implementation Guide (ধাপে ধাপে গাইড)

### 1️⃣ STEP 1: Stored Procedures (ডাটাবেজ কাজ)
ডাটাবেজে ২টি Stored Procedure বানাতে হবে:
* **Dropdown-এর জন্য (List Fetching):** ড্রপডাউন-এ ডেটা দেখানোর জন্য। যেমন: `spGetAllExpenseCategories` (এটি Category_ID এবং Category_Name রিটার্ন করবে)।
* **Save করার জন্য (Insertion):** টেবিলে ডেটা সেভ করার জন্য। যেমন: `spInsertExpense` (প্যারামিটার হিসেবে Expense-এর ফিল্ডগুলো নেবে)।

---

### 2️⃣ STEP 2: Data Access Layer (DAL)
ডাটাবেজের সাথে কানেকশন ও কোয়েরি রান করার জন্য `DALayer` ফোল্ডারে কাজ করতে হবে।
* **কোথায় বানাবেন?** `DALayer/Expense/ExpenseDAL.cs` ফাইলে।
* **কী করবেন?** 
  1. ফর্মের প্রতিটি ইনপুট ফিল্ডের জন্য Properties (Variables) ডিক্লেয়ার করুন।
  2. `SaveExpenseToDb()` নামের একটি মেথড লিখুন যা `spInsertExpense` কল করবে।
* **কোড টেমপ্লেট (Code Template):**
  ```csharp
  public class ExpenseDAL
  {
      public int userId { get; set; }
      public decimal amount { get; set; }
      public int categoryId { get; set; }
      public string description { get; set; }

      public bool SaveExpenseToDb()
      {
          using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
          {
              using (SqlCommand cmd = new SqlCommand("spInsertExpense", conn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@UserID", this.userId);
                  cmd.Parameters.AddWithValue("@Amount", this.amount);
                  cmd.Parameters.AddWithValue("@CategoryID", this.categoryId);
                  cmd.Parameters.AddWithValue("@Description", this.description);

                  conn.Open();
                  int rows = cmd.ExecuteNonQuery();
                  return rows > 0; // ডেটা সেভ হলে true, না হলে false
              }
          }
      }
  }
  ```

---

### 3️⃣ STEP 3: Business Logic Layer (BLL)
ইউজার ফর্মে কোনো ভুল বা ফাঁকা ইনপুট দিয়েছে কিনা তা ডাটাবেজে পাঠানোর আগেই চেক করা।
* **কোথায় বানাবেন?** `BLLayer/Expense/ExpenseBLL.cs` ফাইলে।
* **কী করবেন?** 
  1. DAL-এর মতো একই Properties ডিক্লেয়ার করুন।
  2. `CommonValidator` ব্যবহার করে প্রতিটি ফিল্ড ভ্যালিডেট করুন। ভুল থাকলে সেই এরর এনাম রিটার্ন করে দিন। সব ঠিক থাকলে DAL কল করে সেভ করুন।
* **কোড টেমপ্লেট (Code Template):**
  ```csharp
  public class ExpenseBLL
  {
      public int userId { get; set; }
      public string amount { get; set; }
      public int categoryId { get; set; }
      public string description { get; set; }

      private ExpenseDAL expenseDal = new ExpenseDAL();

      public CommonValidator.ValidationResult DataValidatorIntoExpenseBll()
      {
          // ১. ক্যাটাগরি ড্রপডাউন সিলেক্ট করা হয়েছে কিনা চেক
          if (categoryId <= 0) return CommonValidator.ValidationResult.CategoryInvalid; 

          // ২. অ্যামাউন্ট ভ্যালিডেশন
          var result = CommonValidator.ValidateAmount(amount);
          if (result != CommonValidator.ValidationResult.Success) return result;

          // ৩. ডেসক্রিপশন ভ্যালিডেশন
          result = CommonValidator.ValidateDescription(description);
          if (result != CommonValidator.ValidationResult.Success) return result;

          // সব ঠিক থাকলে DAL-এ ডেটা পাস করে সেভ করা
          expenseDal.userId = this.userId;
          expenseDal.amount = Convert.ToDecimal(this.amount);
          expenseDal.categoryId = this.categoryId;
          expenseDal.description = this.description;

          if (expenseDal.SaveExpenseToDb())
          {
              return CommonValidator.ValidationResult.Success;
          }
          return CommonValidator.ValidationResult.StoreProcedureError;
      }
  }
  ```

---

### 4️⃣ STEP 4: UI Model (The Bridge)
এটি UI ফর্মের সাথে BLL-এর সংযোগ ঘটিয়ে ফর্মের কোডকে পরিষ্কার রাখে।
* **কোথায় বানাবেন?** `PersonalExpenseCreditTracker/Modules/Expense/ExpenseUi.cs` ফাইলে।
* **কোড টেমপ্লেট (Code Template):**
  ```csharp
  public class ExpenseUi
  {
      public int userId { get; set; }
      public string amount { get; set; }
      public int categoryId { get; set; }
      public string description { get; set; }

      private ExpenseBLL expenseBll = new ExpenseBLL();

      public CommonValidator.ValidationResult InsertDataIntoExpenseUi()
      {
          expenseBll.userId = userId;
          expenseBll.amount = amount;
          expenseBll.categoryId = categoryId;
          expenseBll.description = description;

          return expenseBll.DataValidatorIntoExpenseBll();
      }
  }
  ```

---

### 5️⃣ STEP 5: UI Form & Helpers (ডিজাইন ও ইভেন্ট কোড)
সবশেষে ইউজার ইন্টারফেসে ডাটাবেজ থেকে ড্রপডাউন ডেটা নিয়ে আসা ও ভুল ইনপুটের জন্য লাল এরর আইকন দেখানো।

1. **ফর্ম লোড-এ ড্রপডাউন ডেটা বাইন্ডিং (Load Dropdowns):**
   `CommonUiFunction.LoadInComboBox` দিয়ে ডাটাবেজ থেকে ড্রপডাউনে ডেটা লোড করুন। এটি নিজে নিজেই প্রথম অপশন হিসেবে placeholders (যেমন "Select Category") অ্যাড করে দেবে।
   ```csharp
   CommonUiFunction.LoadInComboBox("spGetAllExpenseCategories", "Select Category", comboBoxCategory);
   ```

2. **ভ্যালিডেশন এরর মেসেজ সেটআপ করা (Error Messages Setup):**
   * [CommonValidator.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/BLLayer/Common/CommonValidator.cs) ফাইলের `ValidationResult` এনামে নতুন কোনো এরর টাইপ লাগলে যোগ করুন (যেমন `CategoryInvalid`).
   * [ErrorHelper.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Common/ErrorHelper.cs) ফাইলে যান এবং নতুন এনামের জন্য মেসেজটি ডিফাইন করে দিন:
     ```csharp
     case CommonValidator.ValidationResult.CategoryInvalid:
         errorProvider.SetError(comboBox, "Please select an expense category.");
         comboBox.Focus();
         break;
     ```

3. **সেভ বাটনের ক্লিক কোড (Save Button Click Event):**
   ```csharp
   private void btnExpenseSave_Click(object sender, EventArgs e)
   {
       errorProvider1.Clear(); // আগের সব এরর সাইন মুছে ফেলা

       ExpenseUi expenseUi = new ExpenseUi();
       expenseUi.userId = 11; // কারেন্ট ইউজার আইডি
       expenseUi.categoryId = Convert.ToInt32(comboBoxCategory.SelectedValue);
       expenseUi.amount = (txtAmount.Text == "Select Amount") ? "" : txtAmount.Text; // প্লেসহোল্ডার হ্যান্ডেল করা
       expenseUi.description = (txtDescription.Text == "Enter description") ? "" : txtDescription.Text;

       // BLL কল করা ভ্যালিডেশনের জন্য
       CommonValidator.ValidationResult result = expenseUi.InsertDataIntoExpenseUi();

       // রেজাল্ট অনুযায়ী অ্যাকশন নেওয়া
       switch (result)
       {
           case CommonValidator.ValidationResult.Success:
               MessageBox.Show("Expense added successfully!");
               break;
           case CommonValidator.ValidationResult.CategoryInvalid:
               ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxCategory);
               break;
           case CommonValidator.ValidationResult.AmountEmpty:
           case CommonValidator.ValidationResult.AmountInvalid:
               ErrorHelper.ShowValidationError(result, errorProvider1, txtAmount);
               break;
           case CommonValidator.ValidationResult.StoreProcedureError:
               MessageBox.Show("Failed to save expense!");
               break;
       }
   }
   ```

---

## 🎯 এই নিয়মের মূল সুবিধা (কেন করা হচ্ছে?)
1. **ভুল ইনপুট প্রতিরোধ:** ডেটা ভুল থাকলে BLL-ই তা আটকে দেবে। UI-তে ভুল ইনপুটের ঠিক পাশে লাল সতর্ক সংকেত দেখানো হবে।
2. **একই কোড বারবার না লেখা (Reusability):** `CommonUiFunction` ও `ErrorHelper`-এ কোড রেডি থাকায় নতুন কোনো ফর্মে কানেকশন ওপেন/ক্লোজ বা এরর মেসেজ লেখার পেছনে সময় নষ্ট হয় না।
3. **পরিষ্কার ও ক্লিন কোড:** ডিজাইন এবং লজিক আলাদা থাকায় কোড মেনটেইন করা সহজ।
