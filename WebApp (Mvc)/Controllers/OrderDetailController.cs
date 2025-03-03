using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using CofeeShop.Models;

namespace CofeeShop.Controllers
{
    public class OrderDetailController : Controller
    {
        private IConfiguration configuration;

        public OrderDetailController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult OrderDetailList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult OrderDetailForm(int? OrderDetailID)
        {
            OrderDropDown();
            ProductDropDown();
            UserDropDown();

            if (OrderDetailID.HasValue)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_OrderDetail_SelectByPK";
                command.Parameters.AddWithValue("@OrderDetailID", OrderDetailID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                OrderDetailModel orderDetailModel = new OrderDetailModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    orderDetailModel.OrderDetailID = Convert.ToInt32(dataRow["OrderDetailID"]);
                    orderDetailModel.OrderID = Convert.ToInt32(dataRow["OrderID"]);
                    orderDetailModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    orderDetailModel.Quantity = Convert.ToInt32(dataRow["Quantity"]);
                    orderDetailModel.Amount = Convert.ToDouble(dataRow["Amount"]);
                    orderDetailModel.TotalAmount = Convert.ToDouble(dataRow["TotalAmount"]);
                    orderDetailModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                }

                connection.Close();
                return View("OrderDetailForm", orderDetailModel);
            }
            else
            {
                return View("OrderDetailForm");
            }
        }

        [HttpPost]

        public IActionResult OrderDetailSave(OrderDetailModel orderDetailModel)
        {
            if (orderDetailModel.OrderID <= 0)
            {
                ModelState.AddModelError("OrderID", "A valid Order is required.");
            }

            if (orderDetailModel.ProductID <= 0)
            {
                ModelState.AddModelError("ProductID", "A valid Product is required.");
            }

            if (orderDetailModel.UserID <= 0)
            {
                ModelState.AddModelError("UserID", "A valid User is required.");
            }

            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (orderDetailModel.OrderDetailID == null)
                {
                    command.CommandText = "PR_OrderDetail_Insert";
                }
                else
                {
                    command.CommandText = "PR_OrderDetail_UpdateByPK";
                    command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = orderDetailModel.OrderDetailID;
                }

                command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderDetailModel.OrderID;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = orderDetailModel.ProductID;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = orderDetailModel.Quantity;
                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = orderDetailModel.Amount;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = orderDetailModel.TotalAmount;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = orderDetailModel.UserID;
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("OrderDetailList");
            }
            OrderDropDown();
            ProductDropDown();
            UserDropDown();
            return View("OrderDetailForm", orderDetailModel);
        }


        public void OrderDropDown()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Order_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<OrderDropDownModel> orderList = new List<OrderDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                OrderDropDownModel orderDropDownModel = new OrderDropDownModel();
                orderDropDownModel.OrderID = Convert.ToInt32(data["OrderID"]);
                orderList.Add(orderDropDownModel);
            }
            ViewBag.OrderList = orderList;
        }

        public void ProductDropDown()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Product_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<ProductDropDownModel> productList = new List<ProductDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                ProductDropDownModel productDropDownModel = new ProductDropDownModel();
                productDropDownModel.ProductID = Convert.ToInt32(data["ProductID"]);
                productDropDownModel.ProductName = data["ProductName"].ToString();
                productList.Add(productDropDownModel);
            }
            ViewBag.ProductList = productList;
        }

        public void UserDropDown()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_User_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                UserDropDownModel userDropDownModel = new UserDropDownModel();
                userDropDownModel.UserID = Convert.ToInt32(data["UserID"]);
                userDropDownModel.UserName = data["UserName"].ToString();
                userList.Add(userDropDownModel);
            }
            ViewBag.UserList = userList;
        }

        public IActionResult OrderDetailDelete(int OrderDetailID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_OrderDetail_DeleteByPK";
                command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = OrderDetailID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("OrderDetailList");
        }

        public IActionResult DownloadOrderDetailListCsv()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("OrderDetailID,OrderID,ProductName,Quantity,Amount,TotalAmount,UserName");

            foreach (DataRow row in table.Rows)
            {
                csvContent.AppendLine($"{row["OrderDetailID"]},{row["OrderID"]},{row["ProductName"]},{row["Quantity"]},{row["Amount"]},{row["TotalAmount"]},{row["UserName"]}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(buffer, "text/csv", "OrderDetailList.csv");
        }

    }
}
