using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Password")]
        public string PassCode { get; set; }

        public User()
        {

        }
        public User(string userid, string passcode) 
        {
            //login
            UserId = userid;
            PassCode = passcode;
        }

        public User(int id, string userid, string firstname, string lastname, string passcode)
        {
            //update
            UserId = userid;
            PassCode = passcode;
            Id = id;
            LastName = lastname;
            FirstName = firstname;
        }

        public User(string userid, string firstname, string lastname, string passcode)
        {
            //create
            UserId = userid;
            PassCode = passcode;
            LastName = lastname;
            FirstName = firstname;
        }
    }
}
