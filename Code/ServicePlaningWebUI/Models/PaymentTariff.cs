using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class PaymentTariff : Db.Db, IDbObject<int>
    {
        public int IdUserRole { get; set; }
        public Double Price { get; set; }
        public int IdCreator { get; set; }

        public PaymentTariff()
        {
            if (IdUserRole > 0)
            {
                Get(IdUserRole);
            }
        }

        public PaymentTariff(int idUserRole)
        {
            Get(idUserRole);
        }

        public void Get(int idUserRole)
        {
        }

        public void Save()
        {
            SqlParameter pIdUserRole = new SqlParameter() { ParameterName = "id_user_role", Value = IdUserRole, DbType = DbType.Int32 };
            SqlParameter pPrice = new SqlParameter() { ParameterName = "price", Value = Price, DbType = DbType.Double };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "savePaymentTariff", pIdUserRole, pPrice, pIdCreator);
        }

        public void Delete(int id)
        {
        }

        public void Delete(int id, int idCreator)
        {
        }
    }
}