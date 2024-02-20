using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Net;

namespace BudgetPlan.Pages.Incomes
{
    public class CreateModel : PageModel
    {
        public IncomesInfo incomesInfo = new IncomesInfo();
        public List<String> Categories = [];
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            Categories.Add("Salary");
            Categories.Add("Transport");
            Categories.Add("Other");
        }
        public void OnPost() 
        {
            incomesInfo.Month = Request.Form["month"];
            incomesInfo.Year = Int32.Parse(Request.Form["year"]);
            incomesInfo.Category = Request.Form["category"];
            incomesInfo.Amount = Decimal.Parse(Request.Form["amount"]);
            incomesInfo.RealValues = Request.Form["realValues"];

            if (incomesInfo.Month.Length == 0 || incomesInfo.Year == 0 ||
                incomesInfo.Category.Length == 0 || incomesInfo.Amount == 0 ||
                incomesInfo.RealValues.Length == 0)
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
                    String sql = "INSERT INTO incomes" +
                        "(month, year, category, amount, realValues) VALUES " +
                        "(@month, @year, @category, @amount, @realValues)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@month", incomesInfo.Month);
                        command.Parameters.AddWithValue("@year", incomesInfo.Year);
                        command.Parameters.AddWithValue("@category", incomesInfo.Category);
                        command.Parameters.AddWithValue("@amount", incomesInfo.Amount);
                        command.Parameters.AddWithValue("@realValues", incomesInfo.RealValues);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            incomesInfo.Month = DateTime.Now.ToString("MMMM");
            incomesInfo.Year = DateTime.Now.Year;
            incomesInfo.Category = "";
            incomesInfo.Amount = 0;
            incomesInfo.RealValues = "";

            successMessage = "New Income Added";

            Response.Redirect("/Incomes/Index");
        }
    }
}
