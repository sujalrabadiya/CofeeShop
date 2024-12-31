using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class StateRepository
    {
        #region configuration
        private readonly string _configuration;
        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<StateModel> GetAll()
        {
            var stateList = new List<StateModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stateList.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDate"])
                    });
                }
            }
            return stateList;
        }
        #endregion

        #region GetById
        public StateModel GetById(int id)
        {
            StateModel state = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.AddWithValue("@StateID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    state = new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString()
                    };
                }
            }
            return state;
        }
        #endregion

        #region Insert
        public bool Insert(StateModel state)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_Insert";
                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
                cmd.Parameters.AddWithValue("@StateName", state.StateName);
                cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(StateModel state)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_Update";
                cmd.Parameters.AddWithValue("@StateID", state.StateID);
                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
                cmd.Parameters.AddWithValue("@StateName", state.StateName);
                cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
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
                cmd.CommandText = "PR_LOC_State_Delete";
                cmd.Parameters.AddWithValue("@StateID", id);
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
