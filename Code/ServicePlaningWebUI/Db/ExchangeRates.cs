using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicePlaningWebUI.Db
{
    public partial class Db
    {
        public class ExchangeRates
        {
            #region Константы

            public const string sp = "hp_exchange_rate";

            #endregion

            #region Common

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetSelectionList(string action, params SqlParameter[] sqlParams)
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, action, sqlParams);
                return dt;
            }

            #endregion

            public static DataTable GetExchangeRatesOnDate(DateTime dateRate)
            {
                SqlParameter pDateRate = new SqlParameter() { ParameterName = "date_rate", Value = dateRate, DbType = DbType.DateTime };

                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getExchangeRatesOnDate", pDateRate);
                return dt;
            }
        }
    }
}