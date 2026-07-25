# 📘 Lent মডিউলের আর্কিটেকচার ও ডেটা ফ্লো গাইড (সহজ ভাষায়)

এই গাইডে আমাদের প্রজেক্টের **Add Lent** ফিচারটি কীভাবে কাজ করছে, কোন ফাইল কী দায়িত্ব পালন করছে এবং ডেটা কীভাবে এক লেয়ার থেকে অন্য লেয়ারে যাচ্ছে তা একদম সহজ ভাষায় ব্যাখ্যা করা হয়েছে। 

অন্য কোনো ডেভেলপার যদি নতুন কোনো মডিউল (যেমন: **Add Expense**) বানাতে চান, তবে এই **Lent** মডিউলের ফ্লো দেখেই হুবহু একই নিয়মে তৈরি করে ফেলতে পারবেন।

---

## 🏗️ Lent মডিউলে ব্যবহৃত ফাইলসমূহ (Files Involved)

আমরা **৩-লেয়ার আর্কিটেকচার** ব্যবহার করেছি। Lent মডিউলের জন্য নিচের ফাইলগুলো কাজ করছে:

1. **UI / Presentation Layer (ডিজাইন ও ইউজার ইন্টারফেস):**
   * **[AddLentControls.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Modules/Lent/AddLentControls.cs):** এটি আমাদের WinForm স্ক্রিন, যেখানে ইউজার ইনপুট দেয় এবং সেভ বাটনে ক্লিক করে।
   * **[LentUi.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Modules/Lent/LentUi.cs):** এটি একটি সাহায্যকারী ক্লাস (UI Model)। এটি ফর্মের ইনপুটগুলোকে এক জায়গায় জড়ো করে BLL-এ পাঠায়।

2. **BLL / Business Logic Layer (ভ্যালিডেশন ও লজিক):**
   * **[LentBLL.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/BLLayer/Lent/LentBLL.cs):** এখানে সমস্ত চেকিং বা ভ্যালিডেশন করা হয় (যেমন: অ্যামাউন্ট ঠিক আছে কিনা, তারিখ ঠিক আছে কিনা)।

3. **DAL / Data Access Layer (ডাটাবেজ কানেকশন):**
   * **[LentDAL.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/DALayer/Lent/LentDAL.cs):** এটি সরাসরি SQL Server ডাটাবেজের সাথে যোগাযোগ করে ডেটা ইনসার্ট/সেভ করে।

4. **Common Helpers (শেয়ার্ড কোড):**
   * **[CommonValidator.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/BLLayer/Common/CommonValidator.cs):** সব মডিউলের জন্য কমন চেকিং রুলস ও এনাম এরর লিস্ট।
   * **[CommonUiFunction.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Common/CommonUiFunction.cs):** ড্রপডাউন লোড করার কমন মেথড।
   * **[ErrorHelper.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Common/ErrorHelper.cs):** ভুল ইনপুটের পাশে লাল এরর আইকন দেখানোর কমন মেথড।

---

## 🔄 ডেটা কীভাবে কাজ করছে? (Step-by-Step Flow)

কাজটিকে মূলত ২টি ভাগে ভাগ করা যায়:

### প্রথম ভাগ: ড্রপডাউন-এ ডাটাবেজের ভ্যালু লোড করা (Form Load)
ফর্মটি যখন স্ক্রিনে ওপেন হয়, তখন ড্রপডাউনগুলোতে (ComboBox) ডাটাবেজ থেকে ডেটা এসে লোড হয়।

1. **UI ফাইলে কল:** `AddLentControls_Load` মেথডে আমরা `CommonUiFunction.LoadInComboBox` কল করি।
   ```csharp
   CommonUiFunction.LoadInComboBox("spGetAllPersons", 11, "Select Person", comboBoxLentSelectPerson);
   ```
2. **ডাটাবেজ থেকে ডেটা আনা:** `SqlHelper` ডাটাবেজ থেকে Stored Procedure (যেমন: `spGetAllPersons`) রান করে একটি `DataTable` রিটার্ন করে।
3. **প্লেসহোল্ডার যোগ করা:** `CommonUiFunction` ঐ টেবিলের শুরুতে একটি ফেইক রো (Row) ঢুকিয়ে দেয় যার আইডি `0` এবং লেখা থাকে `"Select Person"`। এর ফলে ড্রপডাউন ওপেন করলেই প্রথমে "Select Person" লেখাটি সিলেক্ট হয়ে থাকে।

---

### দ্বিতীয় ভাগ: সেভ বাটনে ক্লিক ও ডেটা সেভ হওয়া (Save & Validate)
ইউজার যখন ডেটা ইনপুট দিয়ে **Save** বাটনে ক্লিক করে, তখন নিচের ধাপগুলো ঘটে:

#### ১. ইনপুট সংগ্রহ ও ক্লিনিং (UI Layer):
* **[AddLentControls.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Modules/Lent/AddLentControls.cs)**-এ `btnLentAddSave_Click` ইভেন্ট ফায়ার হয়।
* প্রথমে `errorProvider1.Clear()` করে আগের এরর সাইনগুলো মুছে ফেলা হয়।
* এরপর একটি `LentUi` অবজেক্ট তৈরি করে ফর্মের মানগুলো অ্যাসাইন করা হয়। এখানে প্লেসহোল্ডারগুলো ক্লিন করা হয়:
  * যদি অ্যামাউন্ট ফিল্ডে `"Select Amount"` লেখা থাকে, তবে ডাটাবেজে পাঠানোর জন্য খালি স্ট্রিং `""` পাঠানো হয়।
  * যদি কোনো ডেডলাইন সিলেক্ট না করা হয়, তবে `DateTime.MinValue` পাঠানো হয়।
* সবশেষে ভ্যালিডেশনের জন্য কল করা হয়: `lentUi.InsertDataIntoLentUi();`

#### ২. লজিক ও ভ্যালিডেশন চেক (BLL Layer):
* **[LentUi.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/Modules/Lent/LentUi.cs)** মেথডটি সমস্ত ডেটা **[LentBLL.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/BLLayer/Lent/LentBLL.cs)**-এর অবজেক্টে কপি করে দেয় এবং `lentBll.DataValidatorIntoLentBll()` কল করে।
* BLL ফাইলে এক এক করে ইনপুটগুলো চেক করা হয় **[CommonValidator.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/BLLayer/Common/CommonValidator.cs)** এর সাহায্য নিয়ে:
  ```csharp
  result = CommonValidator.ValidatePerson(personId);
  if (result != CommonValidator.ValidationResult.Success) return result;
  
  result = CommonValidator.ValidateAmount(amount);
  if (result != CommonValidator.ValidationResult.Success) return result;
  ```
* **ভুল থাকলে:** কোনো একটি ভ্যালিডেশন চেক ফেইল করলেই BLL কোড ওখানেই থেমে যায় এবং একটি নির্দিষ্ট এরর এনাম (যেমন: `ValidationResult.AmountEmpty`) রিটার্ন করে UI-তে পাঠিয়ে দেয়।
* **সব ঠিক থাকলে:** যদি সব চেক পাস করে (`Success` হয়), তবে BLL সমস্ত ডেটা নিয়ে **[LentDAL.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/DALayer/Lent/LentDAL.cs)** ফাইলে পাঠায় ডাটাবেজে সেভ করার জন্য।

#### ৩. ডাটাবেজে ডেটা সেভ (DAL Layer):
* **[LentDAL.cs](file:///e:/Dekstop%20Application/PersonalExpenseCreditTracker/WinFormsApp/PersonalExpenseCreditTracker/DALayer/Lent/LentDAL.cs)**-এর `SaveLentToDb()` মেথডটি রান হয়।
* এটি ডাটাবেজের কানেকশন ওপেন করে Stored Procedure `"spInsertLent"` কল করে এবং SQL Parameter বাইন্ডিংয়ের মাধ্যমে ডেটা সেভ করে।
* সেভ সফল হলে এটি BLL-কে `true` ফেরত দেয়, আর ফেইল করলে `false` ফেরত দেয়।
* BLL ডাটাবেজ রেসপন্স অনুযায়ী UI-কে যথাক্রমে `ValidationResult.Success` অথবা `ValidationResult.StoreProcedureError` পাঠায়।

#### ৪. স্ক্রিনে ফলাফল বা লাল এরর আইকন দেখানো (UI Layer Error Display):
* UI ফর্মের `switch (result)` ব্লকে BLL থেকে ফেরত আসা এনামটি চেক করা হয়।
* **সেভ সফল হলে:** `Success` কেস রান হয় এবং মেসেজ বক্স দেখায়: `"Lent added successfully!"`।
* **ভুল থাকলে (Error):** ভুল এনাম অনুযায়ী `ErrorHelper.ShowValidationError` কল করা হয়।
  ```csharp
  case CommonValidator.ValidationResult.AmountEmpty:
      ErrorHelper.ShowValidationError(result, errorProvider1, txtLentAddAmount);
      break;
  ```
* এটি সরাসরি ইউজার ইন্টারফেসে সেই নির্দিষ্ট ইনপুট কন্ট্রোলটির (যেমন: `txtLentAddAmount`) পাশে একটি **লাল গোল সতর্ক সংকেত আইকন (Error Provider)** বসিয়ে দেয় এবং কার্সার সেখানে নিয়ে যায় যাতে ইউজার বুঝতে পারে ঠিক কোন জায়গায় ভুল হয়েছে।

---

## 💡 অন্য মডিউলে কাজ করার সময় যা মনে রাখতে হবে (Developer Key Takeaways)

আপনি যদি অন্য কোনো নতুন মডিউল (যেমন: **Add Expense**) তৈরি করতে চান, তবে লেন্টের এই সিস্টেমটি দেখে আপনাকে যা করতে হবে:
1. ডাটাবেজে ডেটা লোড এবং সেভ করার Stored Procedures বানিয়ে নিন।
2. নতুন মডিউলের জন্য একটি **DAL** ক্লাস বানান যা ডাটাবেজে ইনসার্ট কুয়েরি চালাবে।
3. একটি **BLL** ক্লাস বানান যেখানে ইনপুট চেক করার জন্য `CommonValidator`-এর মেথডগুলো একে একে কল করবেন।
4. ফর্মে `CommonUiFunction.LoadInComboBox` দিয়ে ড্রপডাউন লোড করুন।
5. ফর্মে সেভ বাটনের ক্লিকে ইনপুট সংগ্রহ করে UI Model-এ দিয়ে BLL কল করুন এবং `switch (result)` দিয়ে এররগুলো `ErrorHelper` এর মাধ্যমে স্ক্রিনে লাল এরর আইকন দিয়ে প্রদর্শন করুন।
