using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BudgetPlan.Pages.Incomes
{
    public class IndexModel : PageModel
    {
        public List<IncomesInfo> Incomes = new List<IncomesInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM incomes";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    { 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IncomesInfo incomesInfo = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Month = reader.GetString(1),
                                    Year = reader.GetInt32(2),
                                    Category = reader.GetString(3),
                                    Amount = reader.GetDecimal(4),
                                    RealValues = reader.GetString(5)
                                };

                                Incomes.Add(incomesInfo);
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

    public class IncomesInfo
    {
        public int Id { get; set; }
        public String Month { get; set; }
        public int Year { get; set; }
        public String Category { get; set; }
        public Decimal Amount { get; set; }
        public String RealValues { get; set; }
        public IncomesInfo() 
        {
            Month = DateTime.Now.ToString("MMMM");
            Year = DateTime.Now.Year;
            Category = "";
            Amount = 0;
            RealValues = "Real";
        }
    }
}
