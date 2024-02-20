using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Net;

namespace BudgetPlan.Pages.Expenses
{
    public class EditModel : PageModel
    {
        public ExpensesInfo expensesInfo = new ExpensesInfo();
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
                    String sql = "SELECT * FROM expenses WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read())
                            {
                                expensesInfo.Id = reader.GetInt32(0);
                                expensesInfo.Month = reader.GetString(1);
                                expensesInfo.Year = reader.GetInt32(2);
                                expensesInfo.Category = reader.GetString(3);
                                expensesInfo.Amount = reader.GetDecimal(4);
                                expensesInfo.RealValues = reader.GetString(5);
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
            expensesInfo.Id = Int32.Parse(Request.Form["id"]);
            expensesInfo.Month = Request.Form["month"];
            expensesInfo.Year = Int32.Parse(Request.Form["year"]); 
            expensesInfo.Amount = Decimal.Parse(Request.Form["amount"]);

            if (expensesInfo.Month.Length == 0 || expensesInfo.Year == 0 || expensesInfo.Amount == 0)
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
                    String sql = "UPDATE expenses" +
                        " SET month=@month, year=@year, amount=@amount " +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", expensesInfo.Id);
                        command.Parameters.AddWithValue("@month", expensesInfo.Month);
                        command.Parameters.AddWithValue("@year", expensesInfo.Year);
                        command.Parameters.AddWithValue("@amount", expensesInfo.Amount);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            expensesInfo.Month = ""; 
            expensesInfo.Year = 2024; 
            expensesInfo.Category = ""; 
            expensesInfo.Amount = 0; 
            expensesInfo.RealValues = "";

            successMessage = "New Expense Added";

            Response.Redirect("/Expenses/Index");
        }
    }
}
