using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libery
{
    internal class login
    {
        public string? user_name {  get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? email {  get; set; }     
    }

    public class empoloye
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
    }

    public class books
    {
        public string? id { get; set; }
        public string? book_name { get; set; }
        public int book_id { get; set; }
        public string? table_book { get; set; }
        public int qty { get; set; }
    }

    public class readers
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public int id { get; set; }
        public SqlDateTime due_date { get; set; }
        public int day {  get; set; }
        public int fine { get; set; }
        public int num { get; set; }
        public string? date1 { get; set; }
    }

    public class New_user 
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public int age { get; set; }
        public string? email { get; set; }
        public string? pass { get; set; }
        public string? confirm_pass { get; set; }     
    }

    public class Librarian
    {
        public string? email { get; set; }
        public string? pass { get; set; }
        public string? name { get; set; }
        public string? id { get; set; }
    }
}
