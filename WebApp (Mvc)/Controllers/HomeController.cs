using Microsoft.AspNetCore.Mvc;
using CofeeShop.Models;
using System.Diagnostics;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CofeeShop.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult Index()
        {
            var dashboardData = new Dashboard
            {
                Counts = new List<DashboardCounts>(),
                RecentOrders = new List<RecentOrder>(),
                RecentProducts = new List<RecentProduct>(),
                TopCustomers = new List<TopCustomer>(),
                TopSellingProducts = new List<TopSellingProduct>(),
                NavigationLinks = new List<QuickLinks>()
            };

            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("usp_GetDashboardData", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dashboardData.Counts.Add(new DashboardCounts
                        {
                            Metric = reader["Metric"].ToString(),
                            Value = Convert.ToInt32(reader["Value"])
                        });
                    }

                    // Fetch recent orders
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dashboardData.RecentOrders.Add(new RecentOrder
                            {
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                PaymentMode = reader["PaymentMode"].ToString()
                            });
                        }
                    }

                    // Fetch recent products
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dashboardData.RecentProducts.Add(new RecentProduct
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString(),
                                ProductCode = reader["ProductCode"].ToString(),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }

                    // Fetch top customers
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dashboardData.TopCustomers.Add(new TopCustomer
                            {
                                CustomerName = reader["CustomerName"].ToString(),
                                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }

                    // Fetch top selling products
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dashboardData.TopSellingProducts.Add(new TopSellingProduct
                            {
                                ProductName = reader["ProductName"].ToString(),
                                TotalSoldQuantity = Convert.ToInt32(reader["TotalSoldQuantity"]),
                                ProductCode = reader["ProductCode"].ToString()
                            });
                        }
                    }
                }
            }

            dashboardData.NavigationLinks = new List<QuickLinks> {
                new QuickLinks {ActionMethodName = "Index", ControllerName="Home", LinkName="Dashboard" },
                new QuickLinks {ActionMethodName = "CountryList", ControllerName="Country", LinkName="Country" },
                new QuickLinks {ActionMethodName = "StateList", ControllerName="State", LinkName="State" },
                new QuickLinks {ActionMethodName = "CityList", ControllerName="City", LinkName="City" }
            };

            var model = new Dashboard
            {
                Counts = dashboardData.Counts,
                RecentOrders = dashboardData.RecentOrders,
                RecentProducts = dashboardData.RecentProducts,
                TopCustomers = dashboardData.TopCustomers,
                TopSellingProducts = dashboardData.TopSellingProducts,
                NavigationLinks = dashboardData.NavigationLinks
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
