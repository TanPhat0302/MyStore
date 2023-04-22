using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using ServiceReference1;
using System;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {

                var client = new country_to_timezone();
                var result = await client.country_to_timezone();



                String connectionString = "server=mariadb.vamk.fi;port=3306;database=e2000587_mystore;uid=e2000587;password=RaXTq6V5txC";

                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    // Creating query string
                    string sql = "SELECT * FROM client";
                    // New command object
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    // New reader object
                    MySqlDataReader rdr = cmd.ExecuteReader();
              
                    // While reader has rows
                    while (rdr.Read())
                    {

                        listClients.Add(new ClientInfo() { id = rdr.GetString(0), name = rdr.GetString(1), email = rdr.GetString(2), phone = rdr.GetString(3),
                            country = rdr.GetString(4) });
                    
                   

                    }
                    rdr.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Excaption:" + ex.ToString());
                }

            
            
            
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String country;
        public String timezone;
    }
}
