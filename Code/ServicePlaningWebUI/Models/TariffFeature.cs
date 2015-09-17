using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class TariffFeature : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SysName { get; set; }
        public Double Price { get; set; }
        public int IdCreator { get; set; }

        public TariffFeature()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public TariffFeature(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_feature", Value = Id, DbType = DbType.Int32 };
            SqlParameter pPrice = new SqlParameter() { ParameterName = "price", Value = Price, DbType = DbType.Double };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveTariffFeature", pId, pPrice, pIdCreator);
        }

        public void Delete(int id)
        {
        }

        public void Delete(int id, int idCreator)
        {
        }
    }
}