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
        public class Zipcl
        {
            #region Константы

            public const string sp = "ui_zip_claims";

            #endregion

            public static DataTable GetClaimUnitHistoryList(int? idDevice)
            {
                SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getClaimUnitHistoryList", pIdDevice);
                return dt;
            }
        }
    }
}