using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Net;

namespace BudgetPlan.Pages.Expenses
{
    public class CreateModel : PageModel
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
        }
        public void OnPost() 
        {
            expensesInfo.Month = Request.Form["month"];
            expensesInfo.Year = Int32.Parse(Request.Form["year"]);
            expensesInfo.Category = Request.Form["category"]; 
            expensesInfo.Amount = Decimal.Parse(Request.Form["amount"]);
            expensesInfo.RealValues = Request.Form["realValues"];

            if (expensesInfo.Month.Length == 0 || expensesInfo.Year == 0 ||
                expensesInfo.Category.Length == 0 || expensesInfo.Amount == 0 ||
                expensesInfo.RealValues.Length == 0)
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
                    String sql = "INSERT INTO expenses" +
                        "(month, year, category, amount, realValues) VALUES " +
                        "(@month, @year, @category, @amount, @realValues)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@month", expensesInfo.Month);
                        command.Parameters.AddWithValue("@year", expensesInfo.Year);
                        command.Parameters.AddWithValue("@category", expensesInfo.Category);
                        command.Parameters.AddWithValue("@amount", expensesInfo.Amount);
                        command.Parameters.AddWithValue("@realValues", expensesInfo.RealValues);

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
