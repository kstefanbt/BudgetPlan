using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BudgetPlan.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        public List<ExpensesInfo> Expenses = [];
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM expenses";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    { 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ExpensesInfo expensesInfo = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Month = reader.GetString(1),
                                    Year = reader.GetInt32(2),
                                    Category = reader.GetString(3),
                                    Amount = reader.GetDecimal(4),
                                    RealValues = reader.GetString(5)
                                };
                                
                                Expenses.Add(expensesInfo);
                            }
                        }
                    }
                }
            }
            catch ( Exception ex)
            { 
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ExpensesInfo
    {
        public int Id { get; set; }
        public String Month { get; set; }
        public int Year { get; set; }
        public String Category { get; set; }
        public Decimal Amount { get; set; }
        public String RealValues { get; set; }
        public ExpensesInfo() 
        {
            Month = DateTime.Now.ToString("MMMM");
            Year = DateTime.Now.Year;
            Category = "";
            Amount = 0;
            RealValues = "Real";
        }
    }
}
