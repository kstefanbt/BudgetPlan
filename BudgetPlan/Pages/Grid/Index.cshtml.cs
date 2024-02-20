using BudgetPlan.Pages.Expenses;
using BudgetPlan.Pages.Incomes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Numerics;

namespace BudgetPlan.Pages.Grid
{
    public class IndexModel : PageModel
    {
        public List<Info> Infos = [];
        public List<String> ExpenseCategories = ["Food", "Transport", "Bills", "Other"];
        public List<String> IncomeCategories = ["Salary", "Transport", "Other"];
        public List<String> TypeValues = ["Real", "Planned"];

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mysql;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT SUM(amount), month, year, category, realValues FROM expenses GROUP BY month, year, category, realValues";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Info info = new Info();
                                var amount = reader.GetDecimal(0);
                                var date = reader.GetString(1) + " " + reader.GetInt32(2);
                                var category = reader.GetString(3);
                                var realValues = reader.GetString(4);
                                var tempInfo = Infos.Find(x => x.Date == date);
                                if (tempInfo!=null) 
                                {
                                    if (category == "Food" && realValues == "Real") 
                                    {
                                        tempInfo.ExpenseAmount1Real = amount;
                                    }
                                    if (category == "Food" && realValues == "Planned")
                                    {
                                        tempInfo.ExpenseAmount1Planned = amount;
                                    }
                                    if (category == "Transport" && realValues == "Real")
                                    {
                                        tempInfo.ExpenseAmount2Real = amount;
                                    }
                                    if (category == "Transport" && realValues == "Planned")
                                    {
                                        tempInfo.ExpenseAmount2Planned = amount;
                                    }
                                    if (category == "Bills" && realValues == "Real")
                                    {
                                        tempInfo.ExpenseAmount3Real = amount;
                                    }
                                    if (category == "Bills" && realValues == "Planned")
                                    {
                                        tempInfo.ExpenseAmount3Planned = amount;
                                    }
                                    if (category == "Other" && realValues == "Real")
                                    {
                                        tempInfo.ExpenseAmount4Real = amount;
                                    }
                                    if (category == "Other" && realValues == "Planned")
                                    {
                                        tempInfo.ExpenseAmount4Planned = amount;
                                    }
                                }
                                else
                                {
                                    info.Date = date;

                                    if (category == "Food" && realValues == "Real")
                                    {
                                        info.ExpenseAmount1Real = amount;
                                    }
                                    if (category == "Food" && realValues == "Planned")
                                    {
                                        info.ExpenseAmount1Planned = amount;
                                    }
                                    if (category == "Transport" && realValues == "Real")
                                    {
                                        info.ExpenseAmount2Real = amount;
                                    }
                                    if (category == "Transport" && realValues == "Planned")
                                    {
                                        info.ExpenseAmount2Planned = amount;
                                    }
                                    if (category == "Bills" && realValues == "Real")
                                    {
                                        info.ExpenseAmount3Real = amount;
                                    }
                                    if (category == "Bills" && realValues == "Planned")
                                    {
                                        info.ExpenseAmount3Planned = amount;
                                    }
                                    if (category == "Other" && realValues == "Real")
                                    {
                                        info.ExpenseAmount4Real = amount;
                                    }
                                    if (category == "Other" && realValues == "Planned")
                                    {
                                        info.ExpenseAmount4Planned = amount;
                                    }

                                    Infos.Add(info);
                                }
                            }
                        }
                    }
                    String sql2 = "SELECT SUM(amount), month, year, category, realValues FROM incomes GROUP BY month, year, category, realValues";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Info info = new Info();
                                var amount = reader.GetDecimal(0);
                                var date = reader.GetString(1) + " " + reader.GetInt32(2);
                                var category = reader.GetString(3);
                                var realValues = reader.GetString(4);
                                var tempInfo = Infos.Find(x => x.Date == date);
                                if (tempInfo != null)
                                {
                                    if (category == "Salary" && realValues == "Real")
                                    {
                                        tempInfo.IncomeAmount1Real = amount;
                                    }
                                    if (category == "Salary" && realValues == "Planned")
                                    {
                                        tempInfo.IncomeAmount1Planned = amount;
                                    }
                                    if (category == "Transport" && realValues == "Real")
                                    {
                                        tempInfo.IncomeAmount2Real = amount;
                                    }
                                    if (category == "Transport" && realValues == "Planned")
                                    {
                                        tempInfo.IncomeAmount2Planned = amount;
                                    }
                                    if (category == "Other" && realValues == "Real")
                                    {
                                        tempInfo.IncomeAmount3Real = amount;
                                    }
                                    if (category == "Other" && realValues == "Planned")
                                    {
                                        tempInfo.IncomeAmount3Planned = amount;
                                    }
                                }
                                else
                                {
                                    info.Date = date;

                                    if (category == "Salary" && realValues == "Real")
                                    {
                                        info.IncomeAmount1Real = amount;
                                    }
                                    if (category == "Salary" && realValues == "Planned")
                                    {
                                        info.IncomeAmount1Planned = amount;
                                    }
                                    if (category == "Transport" && realValues == "Real")
                                    {
                                        info.IncomeAmount2Real = amount;
                                    }
                                    if (category == "Transport" && realValues == "Planned")
                                    {
                                        info.IncomeAmount2Planned = amount;
                                    }
                                    if (category == "Other" && realValues == "Real")
                                    {
                                        info.IncomeAmount3Real = amount;
                                    }
                                    if (category == "Other" && realValues == "Planned")
                                    {
                                        info.IncomeAmount3Planned = amount;
                                    }

                                    Infos.Add(info);
                                }

                            }
                        }
                    }
                }
                foreach(var info in Infos)
                {
                    info.TotalRealExpenses = info.ExpenseAmount1Real + info.ExpenseAmount2Real + info.ExpenseAmount3Real + info.ExpenseAmount4Real;
                    info.TotalPlannedExpenses = info.ExpenseAmount1Planned + info.ExpenseAmount2Planned + info.ExpenseAmount3Planned + info.ExpenseAmount4Planned;
                    info.TotalRealIncomes = info.IncomeAmount1Real + info.IncomeAmount2Real + info.IncomeAmount3Real;
                    info.TotalPlannedIncomes = info.IncomeAmount1Planned + info.IncomeAmount2Planned + info.IncomeAmount3Planned;
                    info.RealProfit = info.TotalRealIncomes - info.TotalRealExpenses;
                    info.PlannedProfit = info.TotalPlannedIncomes - info.TotalPlannedExpenses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class Info
    {
        public String Date { get; set; }
        public Decimal ExpenseAmount1Real { get; set; }
        public Decimal ExpenseAmount1Planned { get; set; }
        public Decimal ExpenseAmount2Real { get; set; }
        public Decimal ExpenseAmount2Planned { get; set; }
        public Decimal ExpenseAmount3Real { get; set; }
        public Decimal ExpenseAmount3Planned { get; set; }
        public Decimal ExpenseAmount4Real { get; set; }
        public Decimal ExpenseAmount4Planned { get; set; }
        public Decimal IncomeAmount1Real { get; set; }
        public Decimal IncomeAmount1Planned { get; set; }
        public Decimal IncomeAmount2Real { get; set; }
        public Decimal IncomeAmount2Planned { get; set; }
        public Decimal IncomeAmount3Real { get; set; }
        public Decimal IncomeAmount3Planned { get; set; }
        public Decimal TotalRealExpenses {  get; set; }
        public Decimal TotalPlannedExpenses { get; set; }
        public Decimal TotalRealIncomes {  get; set; }
        public Decimal TotalPlannedIncomes { get; set; }
        public Decimal RealProfit { get; set; }
        public Decimal PlannedProfit { get; set; }


        public Info()
        {
            Date="";
            ExpenseAmount1Real = 0;
            ExpenseAmount1Planned = 0;
            ExpenseAmount2Real = 0;
            ExpenseAmount2Planned = 0;
            ExpenseAmount3Real = 0;
            ExpenseAmount3Planned = 0;
            ExpenseAmount4Real = 0;
            ExpenseAmount4Planned = 0;
            IncomeAmount1Real = 0;
            IncomeAmount1Planned = 0;
            IncomeAmount2Real = 0;
            IncomeAmount2Planned = 0;
            IncomeAmount3Real = 0;
            IncomeAmount3Planned = 0;
        }
    }
}
