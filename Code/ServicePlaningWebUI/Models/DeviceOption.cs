using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class DeviceOption : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int? IdCreator { get; set; }

        public DeviceOption()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public DeviceOption(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_option", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "getDeviceOption", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device_option"];
                Name = dr["name"].ToString();
                Nickname = dr["nickname"].ToString();
                IdCreator = (int)dr["id_creator"];
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_option", Value = Id, DbType = DbType.Int32 };
            SqlParameter pNumber = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pNickname = new SqlParameter() { ParameterName = "nickname", Value = Nickname, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "saveDeviceOption", pId, pNumber, pNickname, pIdCreator);


            if (dt.Rows.Count > 0)
            {
                Id = (int)dt.Rows[0]["id_device_option"];
            }
            else
            {
                throw new Exception("При сохранении опции не вернулся её ID.");
            }
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_option", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Db.Db.Srvpl.sp, "closeDeviceOption", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}