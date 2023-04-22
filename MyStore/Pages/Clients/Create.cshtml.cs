using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.country = Request.Form["country"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.country.Length == 0)
            {
                errorMessage = "All the fields are requried";
                return;
            }


            //save the new client into the database

            try
            {
                String connectionString = "server=mariadb.vamk.fi;port=3306;database=e2000587_mystore;uid=e2000587;password=RaXTq6V5txC";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO client" + "(name,email,phone,country) VALUES" + "(@name,@email,@phone,@country);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@country", clientInfo.country);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = "";clientInfo.country = "";
            successMessage = "New client Added Correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
