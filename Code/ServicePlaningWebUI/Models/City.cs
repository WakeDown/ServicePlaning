using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class City : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Region { get; set;}
        public string Area { get; set; }
        public string Name { get; set; }
        public string Locality { get; set; }
        public string Coord { get; set; }
        public int? Sla1 { get; set; }
        public int? Sla2 { get; set; }
        public int? Sla3 { get; set; }

        public City()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public City(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_city", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getcity", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_city"];
                Region = dr["region"].ToString();
                Area = dr["area"].ToString();
                Name = dr["name"].ToString();
                Locality = dr["locality"].ToString();
                Coord = dr["coord"].ToString();
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_city", Value = Id, DbType = DbType.Int32 };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pRegion = new SqlParameter() { ParameterName = "region", Value = Region, DbType = DbType.AnsiString };
            SqlParameter pArea = new SqlParameter() { ParameterName = "area", Value = Area, DbType = DbType.AnsiString };
            SqlParameter pLocality = new SqlParameter() { ParameterName = "locality", Value = Locality, DbType = DbType.AnsiString };
            SqlParameter pCoord = new SqlParameter() { ParameterName = "coord", Value = Coord, DbType = DbType.AnsiString };
            SqlParameter pSla1 = new SqlParameter() { ParameterName = "sla_1", Value = Sla1, DbType = DbType.Int32 };
            SqlParameter pSla2 = new SqlParameter() { ParameterName = "sla_2", Value = Sla2, DbType = DbType.Int32 };
            SqlParameter pSla3 = new SqlParameter() { ParameterName = "sla_3", Value = Sla3, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Unit.sp, "saveCity", pId, pRegion, pArea, pName, pLocality, pCoord, pSla1, pSla2, pSla3);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_city", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Unit.sp, "closeCity", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}