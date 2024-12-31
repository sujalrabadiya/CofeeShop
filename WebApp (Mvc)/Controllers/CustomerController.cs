using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CofeeShop.Models;

namespace CofeeShop.Controllers
{
    public class CustomerController : Controller
    {
        private IConfiguration configuration;

        public CustomerController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult CustomerList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult CustomerForm(int? CustomerID)
        {
            UserDropDown();

            if (CustomerID.HasValue)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Customer_SelectByPK";
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                CustomerModel customerModel = new CustomerModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    customerModel.CustomerID = Convert.ToInt32(dataRow["CustomerID"]);
                    customerModel.CustomerName = dataRow["CustomerName"].ToString();
                    customerModel.HomeAddress = dataRow["HomeAddress"].ToString();
                    customerModel.Email = dataRow["Email"].ToString();
                    customerModel.MobileNo = dataRow["MobileNo"].ToString();
                    customerModel.GST_NO = dataRow["GST_NO"].ToString();
                    customerModel.CityName = dataRow["CityName"].ToString();
                    customerModel.PinCode = dataRow["PinCode"].ToString();
                    customerModel.NetAmount = Convert.ToDouble(dataRow["NetAmount"]);
                    customerModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                }

                connection.Close();
                return View("CustomerForm", customerModel);
            }
            else
            {
                return View("CustomerForm");
            }
        }


        [HttpPost]

        public IActionResult CustomerSave(CustomerModel customerModel)
        {
            if (customerModel.UserID <= 0)
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

                if (customerModel.CustomerID == null)
                {
                    command.CommandText = "PR_Customer_Insert";
                }
                else
                {
                    command.CommandText = "PR_Customer_UpdateByPK";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerModel.CustomerID;
                }

                command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerModel.CustomerName;
                command.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customerModel.HomeAddress;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = customerModel.Email;
                command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customerModel.MobileNo;
                command.Parameters.Add("@GST_NO", SqlDbType.VarChar).Value = customerModel.GST_NO;
                command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customerModel.CityName;
                command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customerModel.PinCode;
                command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customerModel.NetAmount;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = customerModel.UserID;
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("CustomerList");
            }

            UserDropDown();
            return View("CustomerForm", customerModel);
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

        public IActionResult CustomerDelete(int CustomerID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Customer_DeleteByPK";
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("CustomerList");
        }

        public IActionResult DownloadCustomerListCsv()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("CustomerID,CustomerName,HomeAddress,Email,MobileNo,GSTNO,CityName,PinCode,NetAmount,UserName");

            foreach (DataRow row in table.Rows)
            {
                csvContent.AppendLine($"{row["CustomerID"]},{row["CustomerName"]},{row["HomeAddress"]},{row["Email"]},{row["MobileNo"]},{row["GST_NO"]},{row["CityName"]},{row["PinCode"]},{row["NetAmount"]},{row["UserName"]}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(buffer, "text/csv", "CustomerList.csv");
        }

    }
}
