using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BankApplication.model
{
    public class User1
    {
        //for in memory test
        [Key]   //Indicates this property is the primary key for the entity
        public int UserID { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
    }
}
