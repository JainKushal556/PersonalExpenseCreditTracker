using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalExpenseCreditTracker.Forms.Authentication;
using BLLayer.Authentication;

namespace PersonalExpenseCreditTracker.Session
{
    public class LogedInUser
    {
       private static int userId{get;set;}

       public static void SetUserId(int id)
       {
           userId = id;
       }
       
       public static int GetUserId()
       {
           return userId;
       }
    }
}
