using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Net;

namespace BudgetPlan.Pages.Incomes
{
    public class EditModel : PageModel
    {
        public IncomesInfo incomesInfo = new IncomesInfo();
        public List<String> Categories = [];
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            Categories.Add("Food");
            Categories.Add("Transport");
            Categories.Add("Bills");
            Categories.Add("Other");

            int id = Int32.Parse(Request.Query["id"]);

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM incomes WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read())
                            {
                                incomesInfo.Id = reader.GetInt32(0);
                                incomesInfo.Month = reader.GetString(1);
                                incomesInfo.Year = reader.GetInt32(2);
                                incomesInfo.Category = reader.GetString(3);
                                incomesInfo.Amount = reader.GetDecimal(4);
                                incomesInfo.RealValues = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost() 
        {
            incomesInfo.Id = Int32.Parse(Request.Form["id"]);
            incomesInfo.Month = Request.Form["month"];
            incomesInfo.Year = Int32.Parse(Request.Form["year"]);
            incomesInfo.Amount = Decimal.Parse(Request.Form["amount"]);

            if (incomesInfo.Month.Length == 0 || incomesInfo.Year == 0 || incomesInfo.Amount == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE incomes" +
                        " SET month=@month, year=@year, amount=@amount " +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", incomesInfo.Id);
                        command.Parameters.AddWithValue("@month", incomesInfo.Month);
                        command.Parameters.AddWithValue("@year", incomesInfo.Year);
                        command.Parameters.AddWithValue("@amount", incomesInfo.Amount);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            incomesInfo.Month = "";
            incomesInfo.Year = 2024;
            incomesInfo.Category = "";
            incomesInfo.Amount = 0;
            incomesInfo.RealValues = "";

            successMessage = "New Income Added";

            Response.Redirect("/incomes/Index");
        }
    }
}
