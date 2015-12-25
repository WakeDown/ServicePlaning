using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicePlaningWebUI.Db
{
    public partial class Db
    {
        #region Константы

        private static SqlConnection unitConn { get { return new SqlConnection(ConfigurationManager.ConnectionStrings["unitConnectionString"].ConnectionString); } }

        #endregion

        public static void ExecuteStoredProcedure(string spName, string action, params SqlParameter[] sqlParams)
        {
            using (var conn = unitConn)
            using (var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 100000
            })
            {
                if (!string.IsNullOrEmpty(action) && !string.IsNullOrWhiteSpace(action))
                {
                    SqlParameter pAction = new SqlParameter() { ParameterName = "action", Value = action, DbType = DbType.AnsiString };
                    cmd.Parameters.Add(pAction);
                }

                cmd.Parameters.AddRange(sqlParams);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable ExecuteQueryStoredProcedure(string spName, string action, params SqlParameter[] sqlParams)
        
        {
            DataTable dt = new DataTable();

            using (var conn = unitConn)
            using (var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 100000
            })
            {
                if (!string.IsNullOrEmpty(action) && !string.IsNullOrWhiteSpace(action))
                {
                    SqlParameter pAction = new SqlParameter() { ParameterName = "action", Value = action, DbType = DbType.AnsiString };
                    cmd.Parameters.Add(pAction);
                }

                cmd.Parameters.AddRange(sqlParams);
                conn.Open();
                dt.Load(cmd.ExecuteReader());
            }

            return dt;
        }

        public static object ExecuteScalarStoredProcedure(string spName, string action, params SqlParameter[] sqlParams)
        {
            object result;

            using (var conn = unitConn)
            using (var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 100000
            })
            {
                if (!string.IsNullOrEmpty(action) && !string.IsNullOrWhiteSpace(action))
                {
                    SqlParameter pAction = new SqlParameter() { ParameterName = "action", Value = action, DbType = DbType.AnsiString };
                    cmd.Parameters.Add(pAction);
                }
                cmd.Parameters.AddRange(sqlParams);
                conn.Open();
                result = cmd.ExecuteScalar();
            }

            return result;
        }

        public static int? GetValueIntOrNull(object value)
        {
            int? result = null;

            if (value != null && !String.IsNullOrEmpty(value.ToString()))
            {
                result = Convert.ToInt32(value);
            }

            return result;
        }

        protected decimal? GetValueDeciamlOrNull(string value)
        {
            decimal? result = null;

            if (!String.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        public static DateTime? GetValueDateTimeOrNull(object value)
        {
            DateTime? result = null;

            if (value != null && !String.IsNullOrEmpty(value.ToString()))
            {
                result = Convert.ToDateTime(value);
            }

            return result;
        }

        protected bool GetValueBool(object value)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                result = Convert.ToBoolean(value);
            }

            return result;
        }

        protected bool? GetValueBoolOrNull(object value)
        {
            bool result = false;
            if (value == DBNull.Value) return null;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                result = Convert.ToBoolean(value);
            }

            return result;
        }
    }
}