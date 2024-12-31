using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class CountryRepository
    {
        #region configuration
        private readonly string _configuration;
        public CountryRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<CountryModel> GetAll()
        {
            var countryList = new List<CountryModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countryList.Add(new CountryModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDate"])
                    });
                }
            }
            return countryList;
        }
        #endregion

        #region GetById
        public CountryModel GetById(int id)
        {
            CountryModel country = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.AddWithValue("@CountryID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    country = new CountryModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString()
                    };
                }
            }
            return country;
        }
        #endregion

        #region Insert
        public bool Insert(CountryModel country)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_Insert";
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(CountryModel country)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_Update";
                cmd.Parameters.AddWithValue("@CountryID", country.CountryID);
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_Delete";
                cmd.Parameters.AddWithValue("@CountryID", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
