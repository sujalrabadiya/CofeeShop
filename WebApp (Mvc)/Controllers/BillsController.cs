using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CofeeShop.Models;

namespace CofeeShop.Controllers
{
    public class BillsController : Controller
    {
        private IConfiguration configuration;

        public BillsController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult BillsList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult BillsForm(int? BillID)
        {
            UserDropDown();
            OrderDropDown();

            if (BillID.HasValue)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Bills_SelectByPK";
                command.Parameters.AddWithValue("@BillID", BillID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                BillsModel billModel = new BillsModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    billModel.BillID = Convert.ToInt32(dataRow["BillID"]);
                    billModel.BillNumber = dataRow["BillNumber"].ToString();
                    billModel.BillDate = Convert.ToDateTime(dataRow["BillDate"]);
                    billModel.OrderID = Convert.ToInt32(dataRow["OrderID"]);
                    billModel.TotalAmount = Convert.ToDouble(dataRow["TotalAmount"]);
                    billModel.Discount = Convert.ToDouble(dataRow["Discount"]);
                    billModel.NetAmount = Convert.ToDouble(dataRow["NetAmount"]);
                    billModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                }

                connection.Close();
                return View("BillsForm", billModel);
            }
            else
            {
                return View("BillsForm");
            }
        }


        [HttpPost]

        public IActionResult BillsSave(BillsModel billModel)
        {
            if (billModel.OrderID <= 0)
            {
                ModelState.AddModelError("OrderID", "A valid Order is required.");
            }

            if (billModel.UserID <= 0)
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

                if (billModel.BillID == null)
                {
                    command.CommandText = "PR_Bills_Insert";
                }
                else
                {
                    command.CommandText = "PR_Bills_UpdateByPK";
                    command.Parameters.Add("@BillID", SqlDbType.Int).Value = billModel.BillID;
                }

                command.Parameters.Add("@BillNumber", SqlDbType.VarChar).Value = billModel.BillNumber;
                command.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = billModel.BillDate;
                command.Parameters.Add("@OrderID", SqlDbType.Int).Value = billModel.OrderID;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billModel.TotalAmount;
                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = billModel.Discount;
                command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = billModel.NetAmount;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = billModel.UserID;
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("BillsList");
            }

            UserDropDown();
            OrderDropDown();
            return View("BillsForm", billModel);
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

        public IActionResult BillsDelete(int BillID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Bills_DeleteByPK";
                command.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("BillsList");
        }

        public IActionResult DownloadBillsListCsv()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("BillID,BillNumber,BillDate,OrderID,TotalAmount,Discount,NetAmount,UserName");

            foreach (DataRow row in table.Rows)
            {
                csvContent.AppendLine($"{row["BillID"]},{row["BillNumber"]},{row["BillDate"]},{row["OrderID"]},{row["TotalAmount"]},{row["Discount"]},{row["NetAmount"]},{row["UserName"]}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(buffer, "text/csv", "BillList.csv");
        }

    }
}
