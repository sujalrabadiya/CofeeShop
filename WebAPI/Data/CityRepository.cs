using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class CityRepository
    {
        #region configuration
        private readonly string _configuration;
        public CityRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<CityModel> GetAll()
        {
            var cityList = new List<CityModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand conn = connection.CreateCommand();
                conn.CommandType = CommandType.StoredProcedure;
                conn.CommandText = "PR_LOC_City_SelectAll";
                SqlDataReader reader = conn.ExecuteReader();
                while (reader.Read())
                {
                    cityList.Add(new CityModel()
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDate"]),
                    });
                }
                return cityList;
            }
        }
        #endregion

        #region GetById
        public CityModel GetById(int id)
        {
            CityModel city = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.AddWithValue("@CityID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    city = new CityModel()
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                    };
                }
            }
            return city;
        }
        #endregion

        #region Insert
        public bool Insert(CityModel city)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_Insert";
                cmd.Parameters.AddWithValue("@StateID", city.StateID);
                cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion


        #region Update
        public bool Update(CityModel city)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_Update";
                cmd.Parameters.AddWithValue("@CityID", city.CityID);
                cmd.Parameters.AddWithValue("@StateID", city.StateID);
                cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
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
                cmd.CommandText = "PR_LOC_City_Delete";
                cmd.Parameters.AddWithValue("@CityID", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region GetCountries
        public IEnumerable<CountryDropDownModel> GetCountries()
        {
            var countries = new List<CountryDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_SelectComboBox", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    countries.Add(new CountryDropDownModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString()
                    });
                }
            }

            return countries;
        }
        #endregion

        #region States by CountryId
        public IEnumerable<StateDropDownModel> GetStatesByCountryID(int countryID)
        {
            var states = new List<StateDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectComboBoxByCountryID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    states.Add(new StateDropDownModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString()
                    });
                }
            }

            return states;
        }
        #endregion

    }
}
