using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class Contract2Devices : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdContract { get; set; }
        public int[] LstIdDevice { get; set; }
        public int? IdServiceInterval { get; set; }
        public int? IdCity { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Comment { get; set; }
        public int? IdCreator { get; set; }
        public int? IdServiceAdmin { get; set; }
        public string ObjectName { get; set; }
        public string Coord { get; set; }
        public DateTime[] ScheduleDates { get; set; }//Даты графика обслуживания (плановые выезды)
        public DateTime[] CameDates { get; set; }//Даты обслуживания (плановые выезды)

        public Contract2Devices()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Contract2Devices(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2Devices", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_contract2devices"];
                IdContract = (int)dr["id_contract"];
                LstIdDevice = new[] { (int)dr["id_device"] };
                IdServiceInterval = (int)dr["id_service_interval"];
                IdCity = (int)dr["id_city"];
                Address = dr["address"].ToString();
                ContactName = dr["contact_name"].ToString();
                Comment = dr["comment"].ToString();
                IdCreator = (int)dr["id_creator"];
                IdServiceAdmin = GetValueIntOrNull(dr["id_service_admin"].ToString());
                ObjectName = dr["object_name"].ToString();
                Coord = dr["coord"].ToString();
            }
        }

        public void GetScheduleDates()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = Id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2DevicesScheduleDates", pId);

            if (dt.Rows.Count > 0)
            {
                List<DateTime> lstDt = new List<DateTime>();

                foreach (DataRow row in dt.Rows)
                {
                    lstDt.Add(Convert.ToDateTime(row["planing_date"]));
                }

                ScheduleDates = lstDt.ToArray();
            }
        }

        public void GetCameDates()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = Id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2DevicesCameDates", pId);

            if (dt.Rows.Count > 0)
            {
                List<DateTime> lstDt = new List<DateTime>();

                foreach (DataRow row in dt.Rows)
                {
                    lstDt.Add(Convert.ToDateTime(row["planing_date"]));
                }

                CameDates = lstDt.ToArray();
            }
        }

        public void Save()
        {
            Save(false);
        }

        public static void SaveServiceAdmin(int id, int idServoceAdmin)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = idServoceAdmin, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveContract2DeviceServiceAdmin", pId, pIdServiceAdmin);
        }

        public void Save(bool addScheduleDates2ServicePlan, bool isHandlingDevicesContract = false)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = IdContract, DbType = DbType.Int32 };
            
            //SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = IdDevice, DbType = DbType.Int32 };
            string lstIdDevice = string.Join(",", LstIdDevice);
            SqlParameter pLstIdDevice = new SqlParameter() { ParameterName = "lst_id_device", Value = lstIdDevice, DbType = DbType.AnsiString };

            SqlParameter pIdServiceInterval = new SqlParameter() { ParameterName = "id_service_interval", Value = IdServiceInterval, DbType = DbType.Int32 };
            SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = IdCity, DbType = DbType.Int32 };
            SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = Address, DbType = DbType.AnsiString };
            SqlParameter pContactName = new SqlParameter() { ParameterName = "contact_name", Value = ContactName, DbType = DbType.AnsiString };
            SqlParameter pComment = new SqlParameter() { ParameterName = "comment", Value = Comment, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = IdServiceAdmin, DbType = DbType.Int32 };
            SqlParameter pObjectName = new SqlParameter() { ParameterName = "object_name", Value = ObjectName, DbType = DbType.AnsiString };
            SqlParameter pCoord = new SqlParameter() { ParameterName = "coord", Value = Coord, DbType = DbType.AnsiString };
            SqlParameter pScheduleDates = new SqlParameter() { ParameterName = "lst_schedule_dates", Value = String.Join(",", ScheduleDates), DbType = DbType.AnsiString };
            SqlParameter pAddScheduleDates2ServicePlan = new SqlParameter() { ParameterName = "add_scheduled_dates2service_plan", Value = addScheduleDates2ServicePlan, DbType = DbType.Boolean };


            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveContract2Devices", pId, pIdContract, /*pIdDevice,*/pLstIdDevice, pIdServiceInterval, pIdCity, pAddress, pContactName, pComment, pIdCreator, pIdServiceAdmin, pObjectName, pCoord, pScheduleDates, pAddScheduleDates2ServicePlan);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract2devices", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeContract2Devices", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}