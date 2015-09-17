using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class Address : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public int idCreator { get; set; }

        public Address()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Address(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_srvpl_address", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getSrvplAddress", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_srvpl_address"];
                Name = dr["name"].ToString();
                OrderNum = (int)dr["order_num"];
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_srvpl_address", Value = Id, DbType = DbType.Int32 };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pOrderNum = new SqlParameter() { ParameterName = "order_num", Value = OrderNum, DbType = DbType.Int32 };
            SqlParameter pidCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveSrvplAddress", pId, pName, pOrderNum, pidCreator);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_srvpl_address", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeSrvplAddress", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}