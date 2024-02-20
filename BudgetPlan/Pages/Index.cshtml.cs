using BudgetPlan.Pages.Incomes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BudgetPlan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql1 = "CREATE TABLE [dbo].[expenses] ([id] INT IDENTITY (1, 1) NOT NULL, [month] VARCHAR (20) NOT NULL, [year] INT NOT NULL, [category] VARCHAR (20) NOT NULL, [amount] DECIMAL (10, 2) NOT NULL, [realValues] VARCHAR (20) NOT NULL, PRIMARY KEY CLUSTERED ([id] ASC))";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                }
            }
            catch (Exception ex)
            {                
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql2 = "CREATE TABLE [dbo].[incomes] ([id] INT IDENTITY (1, 1) NOT NULL, [month] VARCHAR (20) NOT NULL, [year] INT NOT NULL, [category] VARCHAR (20) NOT NULL, [amount] DECIMAL (10, 2) NOT NULL, [realValues] VARCHAR (20) NOT NULL, PRIMARY KEY CLUSTERED ([id] ASC))";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
