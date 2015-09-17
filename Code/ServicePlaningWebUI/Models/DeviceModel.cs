using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class DeviceModel : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdDeviceType { get;set;}
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public decimal? Speed { get; set; }
        public int? IdDeviceImprint { get; set; }
        public int? IdPrintType { get; set; }
        public int? IdCartridgeType { get; set; }
        public int? IdCreator { get; set; }
        public int? MaxVolume { get; set; }
        public int? IdClassifierCategory { get; set; }

        public DeviceModel()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public DeviceModel(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_model", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getDeviceModel", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device_model"];
                IdDeviceType = (int) dr["id_device_type"];
                Vendor = dr["vendor"].ToString();
                Name = dr["name"].ToString();
                Nickname = dr["nickname"].ToString();
                Speed = (decimal?)dr["speed"];
                IdDeviceImprint = (int?)dr["id_device_imprint"];
                IdPrintType = (int?)dr["id_print_type"];
                IdCartridgeType = (int?)dr["id_cartridge_type"];
                IdCreator = (int)dr["id_creator"];
                MaxVolume = GetValueIntOrNull(dr["max_volume"]);
                IdClassifierCategory = GetValueIntOrNull(dr["id_classifier_category"]);
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_model", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdDeviceType = new SqlParameter() { ParameterName = "id_device_type", Value = Id, DbType = DbType.Int32 };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pVendor = new SqlParameter() { ParameterName = "vendor", Value = Vendor, DbType = DbType.AnsiString };
            SqlParameter pNickname = new SqlParameter() { ParameterName = "nickname", Value = Nickname, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pSpeed = new SqlParameter() { ParameterName = "speed", Value = Speed, DbType = DbType.Decimal };
            SqlParameter pIdDeviceImprint = new SqlParameter() { ParameterName = "id_device_imprint", Value = IdDeviceImprint, DbType = DbType.Int32 };
            SqlParameter pIdPrintType = new SqlParameter() { ParameterName = "id_print_type", Value = IdPrintType, DbType = DbType.Int32 };
            SqlParameter pIdCartridgeType = new SqlParameter() { ParameterName = "id_cartridge_type", Value = IdCartridgeType, DbType = DbType.Int32 };
            SqlParameter pMaxVolume = new SqlParameter() { ParameterName = "max_volume", Value = MaxVolume, DbType = DbType.Int32 };
            SqlParameter pIdClassifierCategory = new SqlParameter() { ParameterName = "id_classifier_category", Value = IdClassifierCategory, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveDeviceModel", pId, pIdDeviceType, pName, pVendor, pNickname, pIdCreator, pSpeed, pIdDeviceImprint, pIdPrintType, pIdCartridgeType, pMaxVolume, pIdClassifierCategory);

            //if (dt.Rows.Count > 0)
            //{
            //    Id = (int)dt.Rows[0]["id_device_model"];
            //}
            //else
            //{
            //    throw new Exception("При сохранении модели не вернулся её ID.");
            //}
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_model", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeDeviceModel", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}