using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class Device2Options : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdDevice { get; set; }
        public int IdOption { get; set; }
        public int? IdCreator { get; set; }

        public Device2Options()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Device2Options(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device2option", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "getDevice2Options", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device2option"];
                IdDevice = (int)dr["id_device"];
                IdOption = (int)dr["id_device_option"];
                IdCreator = (int)dr["id_creator"];
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device2option", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = IdDevice, DbType = DbType.Int32 };
            SqlParameter pIdOption = new SqlParameter() { ParameterName = "id_device_option", Value = IdOption, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "saveDevice2Options", pId, pIdDevice, pIdOption, pIdCreator);
            
            //if (dt.Rows.Count > 0)
            //{
            //    Id = (int)dt.Rows[0]["id_device2option"];
            //}
            //else
            //{
            //    throw new Exception("При сохранении привязки опции к устройству не вернулся её ID.");
            //}
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device2option", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Db.Db.Srvpl.sp, "closeDevice2Options", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}