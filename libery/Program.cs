using System;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace libery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string query1;
            string query2;
            string query3;
            string query4;
            string query5;
            string query6;
            string query7;
            string query8;
            string query9;
            string query10;
            string query11;
            string query12;
            string query13;
            string query14;
            string query15;
            string query16;
            string query17;
            string query18;
            string query19;
            string query20;
            string query21;
            string query22;
            string query23;
            string query24;
            string query25;
            string query26;

            MainConncet maincon = new MainConncet();

            login lg = new login();
            empoloye emp = new empoloye();
            books bk = new books();
            readers read = new readers();
            New_user ns = new New_user();
            Librarian lib = new Librarian();

            using (SqlConnection con =  maincon.Connection())
            {
            main: Console.WriteLine(" \nWelcome ");
                int log = 0;
                try
                {
                    Console.WriteLine(" \n1)Admin Log in \n2)Librarian Log in");
                    log = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e) { Console.WriteLine(e); }
                if(log == 1) 
                {
                user: try
                    {
                        Console.WriteLine("\nWelcome to admin login");                    
                        Console.WriteLine("\nEnter your email");
                        lg.user_name = Console.ReadLine();
                        Console.WriteLine("\nPassword");
                        lg.password = Console.ReadLine();
                    }
                    catch(Exception e) {  Console.WriteLine(e); }
                    int num = 0;
                    con.Open();
                    query1 = $"Exec login_details @email='{lg.user_name}',@pass = '{lg.password}';";
                    SqlCommand cmd = new SqlCommand(query1,con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    try
                    {
                        while (reader.Read())
                        {
                            emp.id = reader.GetInt32(0);
                            emp.name = reader.GetString(1);
                            emp.email = reader.GetString(2);
                            ++num;
                            /*Console.WriteLine($"\nEmploye id  = {emp.id} \nEmploye Name = {emp.name} \nEmploye Email = {emp.email} \n");*/
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                    con.Close();

                    if(num > 0)
                    {
                        option: Console.WriteLine("\nSelect option \n1)Search for The books \n2)Enter new books \n3)Return books \n4)New User \n5)View \n6)Add New Librarian \n7)Add New Admin \n8)Remove Admin \n9)Remove Librarian \n10)Log out\n");
                        int opt = Convert.ToInt32(Console.ReadLine());
                        if (opt == 1)
                        {
                            Console.WriteLine("Enter the book name");
                            bk.book_name = Console.ReadLine();

                            con.Open();
                            query3 = $"SELECT * from books where book_name = '{bk.book_name}';";
                            SqlCommand cmd3 = new SqlCommand(query3, con);
                            SqlDataReader reader3 = cmd3.ExecuteReader();

                            try
                            {
                                while (reader3.Read())
                                {
                                    bk.book_id = reader3.GetInt32(0);
                                    bk.table_book = reader3.GetString(2);
                                    bk.qty = reader3.GetInt32(3);

                                    Console.WriteLine($"Book id = {bk.book_id} \nBook Name = {bk.table_book} \nBook Quainty = {bk.qty} \n\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            con.Close();

                            if (bk.qty >= 1)
                            {
                                Console.WriteLine("\nWould you like to take the book y/n");
                                string? answer = Console.ReadLine();
                                if (answer == "y" || answer == "Y")
                                {
                                    --bk.qty;
                                    int num1 = 0;
                                    Console.WriteLine("\nEnter User id");
                                    read.id = Convert.ToInt32(Console.ReadLine());
                                    
                                    con.Open();
                                    query25 = $"Exec user_login @user = {read.id};";
                                    SqlCommand cmd25 = new SqlCommand(query25, con);
                                    SqlDataReader dr25 = cmd25.ExecuteReader();
                                    try
                                    {
                                        while (dr25.Read())
                                        {
                                            read.name = dr25.GetString(0);
                                            ++num1;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    con.Close();
                                    if(num1 >= 1)
                                    {
                                        con.Open();
                                        query26 = $"SELECT count(user_id) from readers\r\nWHERE user_id = {read.id};";
                                        SqlCommand cmd26 = new SqlCommand(query26, con);
                                        SqlDataReader dr26 = cmd26.ExecuteReader();
                                        while (dr26.Read())
                                        {
                                            read.num = dr26.GetInt32(0);                                         
                                        }
                                        con.Close();

                                        if(read.num < 2)
                                        {
                                            Console.WriteLine("\nEnter the name:");
                                            read.name = Console.ReadLine();
                                            Console.WriteLine("\nEnter the email:");
                                            read.email = Console.ReadLine();
                                            con.Open();
                                            query4 = $"INSERT INTO readers (red_name,email,book_id,out_date) VALUES ('{read.name}','{read.email}',{bk.book_id},GETDATE());";
                                            SqlCommand cmd4 = new SqlCommand(query4, con);
                                            int row = cmd4.ExecuteNonQuery();

                                            query5 = $"update books set quantity = {bk.qty} where id ={bk.book_id};";
                                            SqlCommand cmd5 = new SqlCommand(query5, con);
                                            int rows = cmd5.ExecuteNonQuery();
                                            Console.WriteLine($"{rows} books is updated");
                                            con.Close();

                                            con.Open();
                                            query6 = "SELECT max(id) from readers;";
                                            SqlCommand cmd6 = new SqlCommand(query6, con);
                                            SqlDataReader dr = cmd6.ExecuteReader();

                                            try
                                            {
                                                while (dr.Read())
                                                {
                                                    read.id = dr.GetInt32(0);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex);
                                            }
                                            con.Close();

                                            con.Open();
                                            query7 = $"update readers\r\nset in_date = (SELECT dbo.due_date(out_date) AS due_date FROM readers where id = {read.id}) WHERE id = {read.id};";
                                            SqlCommand cmd7 = new SqlCommand(query7, con);
                                            cmd7.ExecuteNonQuery();
                                            con.Close();

                                            con.Open();
                                            query8 = $"SELECT in_date from readers where id={read.id};";
                                            SqlCommand cmd8 = new SqlCommand(query8, con);
                                            SqlDataReader red = cmd8.ExecuteReader();
                                            try
                                            {
                                                while (red.Read())
                                                {
                                                    read.due_date = red.GetDateTime(0);
                                                    Console.WriteLine($"\nDue date will be on {read.due_date}");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex);
                                            }
                                            con.Close();
                                        }
                                        else
                                        {
                                            Console.WriteLine("User already took 2 Books");
                                            goto menu;
                                        }                                       
                                    }
                                    else
                                    {
                                        Console.WriteLine("You are not a user");
                                        goto menu;
                                    }                                  
                                }
                                else
                                {
                                    Console.WriteLine("\n");
                                    goto menu;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No book avaiable");
                                goto menu;
                            }
                            Console.WriteLine("\nwould you like to take some more books y/n");
                            string? ans = Console.ReadLine();
                            if (ans == "y" || ans == "Y")
                            {
                                goto option;
                            }                           
                            menu: Console.WriteLine("\nwould you like to go to Menu y/n");
                            string? ans1 = Console.ReadLine();
                            if (ans1 == "y" || ans1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 2)
                        {

                            Console.WriteLine("new books\n");
                            Console.WriteLine("\nEnter the name of the new book");
                            bk.book_name = Console.ReadLine();
                            Console.WriteLine("Enter the quantity");
                            bk.qty = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                con.Open();
                                query9 = $"INSERT into books (book_name,quantity) VALUES ('{bk.book_name}',{bk.qty});";
                                SqlCommand cmd9 = new SqlCommand(query9, con);
                                int new_book = cmd9.ExecuteNonQuery();
                                Console.WriteLine($"\n{new_book} book is add successfully");
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Console.WriteLine("\nwould you like to go to Menu y/n");
                            string? ans1 = Console.ReadLine();
                            if (ans1 == "y" || ans1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 3)
                        {
                            Console.WriteLine("Enter your name");
                            read.name = Console.ReadLine();
                            Console.WriteLine("Enter the name of the book for return:");
                            bk.book_name = Console.ReadLine();
                            Console.WriteLine("Enter the book id");
                            bk.book_id = Convert.ToInt32(Console.ReadLine());

                            con.Open();
                            query13 = $"Exec return_details @book_name='{bk.book_name}',@name = '{read.name}';";
                            SqlCommand cmd13 = new SqlCommand(query13, con);
                            SqlDataReader dr3 = cmd13.ExecuteReader();
                            string name, book;
                            try
                            {
                                while (dr3.Read())
                                {
                                    name = dr3.GetString(0);
                                    book = dr3.GetString(1);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            con.Close();

                            con.Open();
                            query10 = $"SELECT quantity from books where book_name = '{bk.book_name}';";
                            SqlCommand cmd10 = new SqlCommand(query10, con);
                            SqlDataReader dr2 = cmd10.ExecuteReader();

                            try
                            {
                                while (dr2.Read())
                                {
                                    bk.qty = dr2.GetInt32(0);
                                    ++bk.qty;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            con.Close();

                            try
                            {
                                con.Open();
                                query11 = $"update books\r\nset quantity = {bk.qty}\r\nWHERE book_name = '{bk.book_name}';";
                                SqlCommand cmd11 = new SqlCommand(query11, con);
                                cmd11.ExecuteNonQuery();
                                con.Close();

                                con.Open();
                                query12 = $"delete readers WHERE red_name = '{read.name}' AND book_id = {bk.book_id};";
                                SqlCommand cmd12 = new SqlCommand(query12, con);
                                cmd12.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ex) { Console.WriteLine(ex); }

                            Console.WriteLine("Book returned successfully \n");
                            Console.WriteLine("\nwould you like to go to Menu y/n");
                            string? ans1 = Console.ReadLine();
                            if (ans1 == "y" || ans1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 4)
                        {
                            Console.WriteLine("Welcome");
                            Console.WriteLine("Enter user name");
                            ns.name = Console.ReadLine();
                            Console.WriteLine("Enter user Age");
                            ns.age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter valide email id ");
                            ns.email = Console.ReadLine();
                            Console.WriteLine("Enter user password");
                            pass1: ns.pass = Console.ReadLine();
                            Console.WriteLine("Re-enter user password");
                            ns.confirm_pass = Console.ReadLine();
                                                        
                            if(ns.pass == ns.confirm_pass)
                            {
                                con.Open();
                                query17 = $"insert into new_user\r\n    (name,age,email,[password])\r\nVALUES\r\n    ('{ns.name}',{ns.age},'{ns.email}','{ns.pass}');";
                                SqlCommand cmd17 = new SqlCommand(query17, con);
                                int new_ = cmd17.ExecuteNonQuery();
                                Console.WriteLine($"{new_} user is creadeted succesfully");
                                con.Close();
                            }
                            else
                            {
                                Console.WriteLine("Password does not match");
                                goto pass1;
                            }
                            Console.WriteLine("\nwould you like to go to Menu y/n");
                            string? ans1 = Console.ReadLine();
                            if (ans1 == "y" || ans1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 5)
                        {
                            
                           option1: Console.WriteLine("\nEnter the option To View Details \n1)All Books \n2)All users \n3)All admins \n4)All Librarian \n5)Books Not Returned \n");
                            int option = Convert.ToInt32(Console.ReadLine());
                            if(option == 1) 
                            {
                                con.Open();
                                query14 = "SELECT * from books;";
                                SqlCommand cmd14 = new SqlCommand(query14, con);
                                SqlDataReader dr14 = cmd14.ExecuteReader();
                                try
                                {
                                    while (dr14.Read())
                                    {
                                        bk.book_id = dr14.GetInt32(0);
                                        bk.id= dr14.GetString(1);
                                        bk.book_name = dr14.GetString(2);
                                        bk.qty = dr14.GetInt32(3);

                                        Console.WriteLine(bk.book_id + "\t" + bk.id +"\t" + bk.book_name + "\t" + bk.qty);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                con.Close();
                            }
                            else if(option == 2) 
                            {
                                con.Open();
                                query15 = "SELECT * from new_user;";
                                SqlCommand cmd15 = new SqlCommand( query15, con);
                                SqlDataReader dr15 = cmd15.ExecuteReader();
                                try
                                {
                                    while (dr15.Read())
                                    {
                                        ns.id = dr15.GetString(1);
                                        ns.name= dr15.GetString(2);
                                        ns.age = dr15.GetInt32(3);
                                        ns.email = dr15.GetString(4);

                                        Console.WriteLine(ns.id + "\t" + ns.name + "\t" + ns.age + "\t" + ns.email);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                con.Close();
                            }
                            else if (option == 3)
                            {
                                con.Open();
                                query16 = "SELECT * from admin WHERE Isactive = 1;";
                                SqlCommand cmd16 = new SqlCommand(query16, con);
                                SqlDataReader dr16 = cmd16.ExecuteReader();
                                try
                                {
                                    while (dr16.Read())
                                    {
                                        emp.id = dr16.GetInt32(0);
                                        emp.name = dr16.GetString(1);
                                        emp.email = dr16.GetString(2);

                                        Console.WriteLine(emp.id + "\t" + emp.name + "\t" + emp.email); ;
                                    }
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else if (option==4)
                            {
                                con.Open();
                                query20 = "SELECT * from Librarian where isactive = 1;";
                                SqlCommand cmd20 = new SqlCommand( query20, con);
                                SqlDataReader dr20 = cmd20.ExecuteReader();
                                try
                                {
                                    while (dr20.Read())
                                    {
                                        lib.id = dr20.GetString(1);
                                        lib.name = dr20.GetString(2);
                                        lib.email = dr20.GetString(3);

                                        Console.WriteLine(lib.id + "\t" + lib.name + "\t" + lib.email); ;
                                    }
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else if(option == 5)
                            {                              
                                Console.WriteLine("select any one option:\n");
                                oop: Console.WriteLine("1)Show all Unreturned Books: \n2)Search by Date:");
                                int bks = Convert.ToInt32(Console.ReadLine());
                                if (bks == 1)
                                {
                                    con.Open();
                                    query21 = "SELECT * from readers;";
                                    SqlCommand cmd21 = new SqlCommand(query21, con);
                                    SqlDataReader dr21 = cmd21.ExecuteReader();
                                    try
                                    {
                                        while (dr21.Read())
                                        {
                                            read.id = dr21.GetInt32(0);
                                        }
                                        con.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    con.Open();
                                    query22 = $"update readers\r\nset rem_day = DATEDIFF(DAY, GETDATE(), in_date)\r\nWHERE id ={read.id};";
                                    SqlCommand cmd22 = new SqlCommand(query22, con);
                                    cmd22.ExecuteNonQuery();
                                    con.Close();

                                    con.Open();
                                    query21 = "SELECT * from readers;";
                                    SqlCommand cmd23 = new SqlCommand(query21, con);
                                    SqlDataReader dr23 = cmd23.ExecuteReader();
                                    try
                                    {
                                        while (dr23.Read())
                                        {
                                            read.id = dr23.GetInt32(0);
                                            read.name = dr23.GetString(1);
                                            read.email = dr23.GetString(2);
                                            read.day = dr23.GetInt32(6);

                                            if (read.day < 0)
                                            {
                                                read.num = -(read.day);
                                                read.fine = read.num * 100;
                                                read.day = read.num;
                                                
                                                Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t reminding days = before {read.day} \t fine = {read.fine}");
                                            }
                                            else
                                            {
                                                read.fine = 0;
                                                Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t reminding days = {read.day} \t fine = {read.fine}");
                                            }
                                        }
                                        con.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                                else if (bks == 2)
                                {
                                    con.Open() ;
                                    
                                    Console.WriteLine("Enter the due date (YYYY-MM-DD):");
                                    read.date1 = Console.ReadLine();
                                    
                                    Console.WriteLine(read.due_date);

                                    query24 = $"SELECT * from readers \r\nwhere in_date = '{read.date1}';";
                                    SqlCommand cmd24 = new SqlCommand(query24, con);
                                    SqlDataReader dr24 = cmd24.ExecuteReader();
                                    try
                                    {
                                        while (dr24.Read())
                                        {
                                            read.id = dr24.GetInt32(0);
                                            read.name = dr24.GetString(1);
                                            read.email = dr24.GetString(2);
                                            read.day = dr24.GetInt32(6);

                                            if (read.day < 0)
                                            {
                                                read.num = -(read.day);
                                                read.fine = read.num * 100;
                                                read.day = read.num;
                                                Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t reminding days = before {read.day} \t fine = {read.fine}");
                                            }
                                            else
                                            {
                                                read.fine = 0;
                                                Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t reminding days = {read.day} \t fine = {read.fine}");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    con.Close();
                                } 
                                else
                                {
                                    Console.WriteLine("Enter valide option");
                                    goto oop;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter valide option:");
                                goto option1;
                            }
                            Console.WriteLine("\nwould you like to go to Menu y/n");
                            string? ans1 = Console.ReadLine();
                            if (ans1 == "y" || ans1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 6)
                        {
                            Console.WriteLine("Adding new name");
                            Console.WriteLine("Enter New Librarian Name:");
                            lib.name = Console.ReadLine();
                            Console.WriteLine("Enter Librarian Email Id:");
                            lib.email = Console.ReadLine();
                            Console.WriteLine("Enter Librarian Password:");
                            lib.pass = Console.ReadLine();

                            con.Open();
                            query19 = $"insert into Librarian\r\n    (lib_name,lib_email,lib_pass,isactive)\r\nVALUES\r\n    ('{lib.name}','{lib.email}','{lib.pass}',1);";
                            SqlCommand cmd19 = new SqlCommand( query19, con);
                            
                            int rr=cmd19.ExecuteNonQuery();

                            Console.WriteLine($"{rr} Librarian is Added");

                            Console.WriteLine("Would to like to goo to menu page (y/n)");
                            string? page2 = Console.ReadLine();
                            if (page2 == "y" || page2 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 7)
                        {                             
                                try
                                {
                                    Console.WriteLine("Adding new Admin");
                                    Console.WriteLine("Enter New Admin name:");
                                    lg.name = Console.ReadLine();
                                    Console.WriteLine("Enter New Admin Email:");
                                    lg.email = Console.ReadLine();
                                    Console.WriteLine("Enter Admin password:");
                                    lg.password = Console.ReadLine();

                                    con.Open();
                                    query2 = $"insert into admin ([name],email,[password],isactive)\r\nVALUES\r\n    ('{lg.name}','{lg.email}','{lg.password}',1);";
                                    SqlCommand cmd2 = new SqlCommand(query2, con);
                                    int row = cmd2.ExecuteNonQuery();
                                    Console.WriteLine($"{row} Admin created successfully");
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }          
                            Console.WriteLine("Would to like to goo to menu page (y/n)");
                            string? page = Console.ReadLine();
                            if(page == "y" || page == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 8)
                        {
                            Console.WriteLine("Enter admin Name: ");
                            emp.name = Console.ReadLine();
                            Console.WriteLine("Enter Admin Id:");
                            emp.id = Convert.ToInt32(Console.ReadLine());

                            con.Open() ;
                            query23 = $"update admin\r\nset isactive = 0\r\nwhere id = {emp.id} AND name = '{emp.name}';";
                            SqlCommand cmd23 = new SqlCommand(query23,con);
                            int row1 = cmd23.ExecuteNonQuery();
                            Console.WriteLine($"{row1} Admin is updated:");
                            con.Close();

                            Console.WriteLine("Would to like to goo to menu page (y/n)");
                            string? page1 = Console.ReadLine();
                            if (page1 == "y" || page1 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if (opt == 9)
                        {
                            Console.WriteLine("Enter Librarian Name: ");
                            lib.name = Console.ReadLine();
                            Console.WriteLine("Enter Librarian Id:");
                            lib.id = Console.ReadLine();

                            con.Open();
                            query23 = $"update Librarian\r\nset isactive = 0\r\nwhere Librarian_id= '{lib.id}' AND lib_name = '{lib.name}';";
                            SqlCommand cmd23 = new SqlCommand(query23, con);
                            int row1 = cmd23.ExecuteNonQuery();
                            Console.WriteLine($"{row1} Librarian is updated:");
                            con.Close();

                            Console.WriteLine("Would to like to goo to menu page (y/n)");
                            string? page3 = Console.ReadLine();
                            if (page3 == "y" || page3 == "Y")
                            {
                                goto option;
                            }
                        }
                        else if(opt == 10)
                        {
                            goto main;
                        }
                        else
                        {
                            Console.WriteLine("Enter valide option");
                            goto option;                          
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter Valid Email or Password");
                        goto user;
                    }
                }

                else if(log == 2)
                {
                    int A = 0;
                    Console.WriteLine("Welcome to Librarian login");
                    Console.WriteLine("Enter email id");
                    lib.email = Console.ReadLine();
                    Console.WriteLine("Password");
                    lib.pass = Console.ReadLine();

                    con.Open();
                    query18 = $"Exec Librarian_login @email1='{lib.email}',@pass1 = '{lib.pass}';";
                    SqlCommand cmd18 = new SqlCommand( query18, con);
                    SqlDataReader dr18 = cmd18.ExecuteReader();
               
                        try
                        {
                            while (dr18.Read())
                            {
                                lib.id = dr18.GetString(1);
                                lib.name = dr18.GetString(2);
                                lib.email = dr18.GetString(3);

                                ++A;
                                Console.WriteLine($"\nid = {lib.id} \nname = {lib.name} \nemail= {lib.email} \n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        con.Close();
                    if (A > 0)
                    {
                    }
                    option: Console.WriteLine("\nSelect option \n1)Search for a books \n2)Enter new books \n3)Returns books \n4)New User \n5)View \n");
                    int opt = Convert.ToInt32(Console.ReadLine());

                    if (opt == 1)
                    {
                        Console.WriteLine("Enter the book name");
                        bk.book_name = Console.ReadLine();

                        con.Open();
                        query3 = $"SELECT * from books where book_name = '{bk.book_name}';";
                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        SqlDataReader reader3 = cmd3.ExecuteReader();

                        try
                        {
                            while (reader3.Read())
                            {
                                bk.book_id = reader3.GetInt32(0);
                                bk.table_book = reader3.GetString(2);
                                bk.qty = reader3.GetInt32(3);

                                Console.WriteLine($"Book id = {bk.book_id} \nBook Name = {bk.table_book} \nBook Quainty = {bk.qty} \n\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        con.Close();

                        if (bk.qty >= 1)
                        {
                            Console.WriteLine("\nWould you like to take the book y/n");
                            string? answer = Console.ReadLine();
                            if (answer == "y" || answer == "Y")
                            {
                                --bk.qty;
                                Console.WriteLine("Enter the name:");
                                read.name = Console.ReadLine();
                                Console.WriteLine("Enter the email:");
                                read.email = Console.ReadLine();

                                con.Open();
                                query4 = $"INSERT INTO readers (red_name,email,book_id,out_date) VALUES ('{read.name}','{read.email}',{bk.book_id},GETDATE());";
                                SqlCommand cmd4 = new SqlCommand(query4, con);
                                int row = cmd4.ExecuteNonQuery();

                                query5 = $"update books set quantity = {bk.qty} where id ={bk.book_id};";
                                SqlCommand cmd5 = new SqlCommand(query5, con);
                                int rows = cmd5.ExecuteNonQuery();
                                Console.WriteLine($"{rows} books is updated");
                                con.Close();

                                con.Open();
                                query6 = "SELECT max(id) from readers;";
                                SqlCommand cmd6 = new SqlCommand(query6, con);
                                SqlDataReader dr = cmd6.ExecuteReader();

                                try
                                {
                                    while (dr.Read())
                                    {
                                        read.id = dr.GetInt32(0);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                con.Close();

                                con.Open();
                                query7 = $"update readers\r\nset in_date = (SELECT dbo.due_date(out_date) AS due_date FROM readers where id = {read.id}) WHERE id = {read.id};";
                                SqlCommand cmd7 = new SqlCommand(query7, con);
                                cmd7.ExecuteNonQuery();
                                con.Close();

                                con.Open();
                                query8 = $"SELECT in_date from readers where id={read.id};";
                                SqlCommand cmd8 = new SqlCommand(query8, con);
                                SqlDataReader red = cmd8.ExecuteReader();
                                try
                                {
                                    while (red.Read())
                                    {
                                        read.due_date = red.GetDateTime(0);
                                        Console.WriteLine($"Due date will be {read.due_date}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                con.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No book avaiable");
                        }

                        Console.WriteLine("\nwould to like to take some more books y/n");
                        string? ans = Console.ReadLine();
                        if (ans == "y" || ans == "Y")
                        {
                            goto option;
                        }
                    }
                    else if (opt == 2)
                    {

                        Console.WriteLine("new books\n");
                        Console.WriteLine("Enter the name of the new book");
                        bk.book_name = Console.ReadLine();
                        Console.WriteLine("Enter the quantity");
                        bk.qty = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            con.Open();
                            query9 = $"INSERT into books (book_name,quantity) VALUES ('{bk.book_name}',{bk.qty});";
                            SqlCommand cmd9 = new SqlCommand(query9, con);
                            int new_book = cmd9.ExecuteNonQuery();
                            Console.WriteLine($"{new_book} book is add successfully");
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    else if (opt == 3)
                    {
                        Console.WriteLine("Enter your name");
                        read.name = Console.ReadLine();
                        Console.WriteLine("Enter the name of the book for return:");
                        bk.book_name = Console.ReadLine();
                        Console.WriteLine("Enter the book id");
                        bk.book_id = Convert.ToInt32(Console.ReadLine());

                        con.Open();
                        query13 = $"Exec return_details @book_name='{bk.book_name}',@name = '{read.name}';";
                        SqlCommand cmd13 = new SqlCommand(query13, con);
                        SqlDataReader dr3 = cmd13.ExecuteReader();
                        string name, book;
                        try
                        {
                            while (dr3.Read())
                            {
                                name = dr3.GetString(0);
                                book = dr3.GetString(1);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        con.Close();

                        con.Open();
                        query10 = $"SELECT quantity from books where book_name = '{bk.book_name}';";
                        SqlCommand cmd10 = new SqlCommand(query10, con);
                        SqlDataReader dr2 = cmd10.ExecuteReader();

                        try
                        {
                            while (dr2.Read())
                            {
                                bk.qty = dr2.GetInt32(0);
                                ++bk.qty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        con.Close();

                        try
                        {
                            con.Open();
                            query11 = $"update books\r\nset quantity = {bk.qty}\r\nWHERE book_name = '{bk.book_name}';";
                            SqlCommand cmd11 = new SqlCommand(query11, con);
                            cmd11.ExecuteNonQuery();
                            con.Close();

                            con.Open();
                            query12 = $"delete readers WHERE red_name = '{read.name}' AND book_id = {bk.book_id};";
                            SqlCommand cmd12 = new SqlCommand(query12, con);
                            cmd12.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }

                        Console.WriteLine("Book returned successfully");
                    }
                    else if (opt == 4)
                    {
                        Console.WriteLine("Welcome");
                        Console.WriteLine("Enter user name");
                        ns.name = Console.ReadLine();
                        Console.WriteLine("Enter user Age");
                        ns.age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter valide email id ");
                        ns.email = Console.ReadLine();
                        Console.WriteLine("Enter user password");
                    pass1: ns.pass = Console.ReadLine();
                        Console.WriteLine("Re-enter user password");
                        ns.confirm_pass = Console.ReadLine();

                        if (ns.pass == ns.confirm_pass)
                        {
                            con.Open();
                            query17 = $"insert into new_user\r\n    (name,age,email,[password])\r\nVALUES\r\n    ('{ns.name}',{ns.age},'{ns.email}','{ns.pass}');";
                            SqlCommand cmd17 = new SqlCommand(query17, con);
                            int new_ = cmd17.ExecuteNonQuery();
                            Console.WriteLine($"{new_} user is creadeted succesfully");
                            con.Close();
                        }
                        else
                        {
                            Console.WriteLine("Password does not match");
                            goto pass1;
                        }
                    }
                    else if (opt == 5)
                    {

                    option1: Console.WriteLine("Enter the option To View Details \n1)All Books \n2)All users \n3)All admins \n4)All Librarian \n5)Books Not Returned");
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 1)
                        {
                            con.Open();
                            query14 = "SELECT * from books;";
                            SqlCommand cmd14 = new SqlCommand(query14, con);
                            SqlDataReader dr14 = cmd14.ExecuteReader();
                            try
                            {
                                while (dr14.Read())
                                {
                                    bk.book_id = dr14.GetInt32(0);
                                    bk.id = dr14.GetString(1);
                                    bk.book_name = dr14.GetString(2);
                                    bk.qty = dr14.GetInt32(3);

                                    Console.WriteLine(bk.book_id + "\t" + bk.id + "\t" + bk.book_name + "\t" + bk.qty);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            con.Close();
                        }
                        else if (option == 2)
                        {
                            con.Open();
                            query15 = "SELECT * from new_user;";
                            SqlCommand cmd15 = new SqlCommand(query15, con);
                            SqlDataReader dr15 = cmd15.ExecuteReader();
                            try
                            {
                                while (dr15.Read())
                                {
                                    ns.id = dr15.GetString(1);
                                    ns.name = dr15.GetString(2);
                                    ns.age = dr15.GetInt32(3);
                                    ns.email = dr15.GetString(4);

                                    Console.WriteLine(ns.id + "\t" + ns.name + "\t" + ns.age + "\t" + ns.email);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            con.Close();
                        }
                        else if (option == 3)
                        {
                            con.Open();
                            query16 = "SELECT * from admin WHERE Isactive = 1;";
                            SqlCommand cmd16 = new SqlCommand(query16, con);
                            SqlDataReader dr16 = cmd16.ExecuteReader();
                            try
                            {
                                while (dr16.Read())
                                {
                                    emp.id = dr16.GetInt32(0);
                                    emp.name = dr16.GetString(1);
                                    emp.email = dr16.GetString(2);

                                    Console.WriteLine(emp.id + "\t" + emp.name + "\t" + emp.email); ;
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else if (option == 4)
                        {
                            con.Open();
                            query20 = "SELECT * from Librarian where isactive = 1;";
                            SqlCommand cmd20 = new SqlCommand(query20, con);
                            SqlDataReader dr20 = cmd20.ExecuteReader();
                            try
                            {
                                while (dr20.Read())
                                {
                                    lib.id = dr20.GetString(1);
                                    lib.name = dr20.GetString(2);
                                    lib.email = dr20.GetString(3);

                                    Console.WriteLine(lib.id + "\t" + lib.name + "\t" + lib.email); ;
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else if (option == 5)
                        {
                            con.Open();
                            query21 = "SELECT * from readers;";
                            SqlCommand cmd21 = new SqlCommand(query21, con);
                            SqlDataReader dr21 = cmd21.ExecuteReader();
                            try
                            {
                                while (dr21.Read())
                                {
                                    read.id = dr21.GetInt32(0);
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }

                            con.Open();
                            query22 = $"update readers\r\nset rem_day = DATEDIFF(DAY, GETDATE(), in_date)\r\nWHERE id ={read.id};";
                            SqlCommand cmd22 = new SqlCommand(query22, con);
                            cmd22.ExecuteNonQuery();
                            con.Close();

                            con.Open();
                            query21 = "SELECT * from readers;";
                            SqlCommand cmd23 = new SqlCommand(query21, con);
                            SqlDataReader dr23 = cmd23.ExecuteReader();
                            try
                            {
                                while (dr23.Read())
                                {
                                                                      
                                    read.id = dr23.GetInt32(0);
                                    read.name = dr23.GetString(1);
                                    read.email = dr23.GetString(2);
                                    read.day = dr23.GetInt32(6);

                                    if (read.day < 0)
                                    {
                                        read.num = -(read.day);
                                        read.fine = read.num * 100;
                                        Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t remind days = before {read.day} \t fine = {read.fine}");
                                    }
                                    else
                                    {
                                        read.fine = 0;
                                        Console.WriteLine($"id = {read.id} \t name = {read.name} \t email = {read.email} \t remind days = {read.day} \t fine = {read.fine}");
                                    }
                                }                              
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter valide option:");
                            goto option1;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Enter valide option");
                    goto main;
                }
            }
        }
    }
}