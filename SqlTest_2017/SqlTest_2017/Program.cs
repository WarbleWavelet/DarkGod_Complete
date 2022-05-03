using MySql.Data.MySqlClient;
using System;
using System.Data;

class Program
{
    static MySqlConnection conn;
    static void Main(string[] args)
    {
        conn = new MySqlConnection("server=localhost;User Id=root;passwrod=;Database=studymysql;Charset=utf8");
        if (conn.State != ConnectionState.Open)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("连不上" + ex);
            }
        }
        Add();
        //Delete();
        //Query();
        //Update();

        Console.ReadKey();

        conn.Close();
    }


    static void Add()
    {
        MySqlCommand cmd = new MySqlCommand("insert into userinfo set name='黄忠',age=40", conn);
        cmd.ExecuteNonQuery();

        Console.WriteLine("已增");
    }
    static void Delete()
    {
        MySqlCommand cmd = new MySqlCommand("", conn);
    }
    static void Query()
    {
        MySqlCommand cmd = new MySqlCommand("", conn);
    }
    static void Update()
    {
        MySqlCommand cmd = new MySqlCommand("", conn);
    }
}