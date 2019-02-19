using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImplementingREST
{
    public class UtilityClass
    {
    }
    public class UserModel
    {
        public string stud_id = "";
        public string password = "";
    }
    public class SignUpModel
    {
        public string stud_id = "";
        public string first_name = "";
        public string last_name = "";
        public string password = "";
    }
    public class Person
    {
        public string phone_no = "";
        public string email = "";
        public string address = "";
        public string city = "";
        public string state = "";
        public string country = "";
        public string emergency_contact = "";
        public string relationship = "";
    }
    public class Student
    {
        public string student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email_id { get; set; }
        public string phone_no { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string emergency_contact { get; set; }
        public string relationship { get; set; }
        public string error { get; set; }
    }
}