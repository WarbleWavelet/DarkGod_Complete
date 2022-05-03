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
        //Add();
        //Delete();
        Query();
        //Update();

        Console.ReadKey();

        conn.Close();
    }


    static void Add()
    {
        MySqlCommand cmd = new MySqlCommand("insert into userinfo set name='张辽',age=40", conn);
        cmd.ExecuteNonQuery();
        int id = (int)cmd.LastInsertedId;
        Console.WriteLine("已增id：{0}",id);
    }


    static void Delete()
    {
        MySqlCommand cmd = new MySqlCommand("", conn);
    }


    static void Query()
    {
        //MySqlCommand cmd = new MySqlCommand("select * from userinfo", conn);
        MySqlCommand cmd = new MySqlCommand("select * from userinfo where name='赵云'", conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("name");
            int age = reader.GetInt32("age");
            Console.WriteLine(String.Format("已查：id:{0} name:{1} age:{2}", id, name, age));

        }
    }


    static void Update()
    {

    }


}