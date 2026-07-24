using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DALayer.Lent
{
    public class LentDAL
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }
      
    }
}
