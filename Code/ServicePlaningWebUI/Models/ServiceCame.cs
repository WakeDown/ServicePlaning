using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class ServiceCame :Db.Db
    {
        public int Id;
        public int IdServiceClaim;
        public DateTime DateCame;
        public string Descr;
        public int? Counter;
        public int? CounterColour;
        public int IdServiceEngeneer;
        public int IdServiceActionType;
        public int? IdCreator;
        public bool NoPay;
        public int? IdAktScan { get; set; }
        public bool? ProcessEnabled { get; set; }
        public bool? DeviceEnabled { get; set; }
        public bool? NeedZip { get; set; }
        public bool? NoCounter { get; set; }
        public bool? CounterUnavailable { get; set; }

        public ServiceCame()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public ServiceCame(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_came", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "getServiceCame", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_service_came"];
                IdServiceClaim = (int)dr["id_service_claim"];
                DateCame = (DateTime)dr["date_came"];
                Descr = dr["descr"].ToString();
                Counter = GetValueIntOrNull(dr["counter"].ToString());
                IdServiceEngeneer = (int)dr["id_service_engeneer"];
                IdServiceActionType = (int)dr["id_service_action_type"];
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());
                CounterColour = GetValueIntOrNull(dr["counter_colour"].ToString());
                IdAktScan = GetValueIntOrNull(dr["id_akt_scan"].ToString());
                NoPay = GetValueBool(dr["no_pay"].ToString());
                ProcessEnabled = GetValueBoolOrNull(dr["process_enabled"].ToString());
                DeviceEnabled = GetValueBoolOrNull(dr["device_enabled"].ToString());
                NeedZip = GetValueBoolOrNull(dr["need_zip"].ToString());
                NoCounter = GetValueBoolOrNull(dr["no_counter"].ToString());
                CounterUnavailable = GetValueBoolOrNull(dr["counter_unavailable"].ToString());
            }
        }

        public void Save(bool isSysAdmin = false, string serialNum = null)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_came", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdServiceClaim = new SqlParameter() { ParameterName = "id_service_claim", Value = IdServiceClaim, DbType = DbType.Int32 };
            SqlParameter pDescr = new SqlParameter() { ParameterName = "descr", Value = Descr, DbType = DbType.AnsiString };
            SqlParameter pDateCame = new SqlParameter() { ParameterName = "date_came", Value = DateCame, DbType = DbType.DateTime };
            SqlParameter pCounter = new SqlParameter() { ParameterName = "counter", Value = Counter, DbType = DbType.Int32 };
            SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_service_engeneer", Value = IdServiceEngeneer, DbType = DbType.Int32 };
            SqlParameter pIdServiceActionType = new SqlParameter() { ParameterName = "id_service_action_type", Value = IdServiceActionType, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pCounterColour = new SqlParameter() { ParameterName = "counter_colour", Value = CounterColour, DbType = DbType.Int32 };
            SqlParameter pIdAktScan = new SqlParameter() { ParameterName = "id_akt_scan", Value = IdAktScan, DbType = DbType.Int32 };
            SqlParameter pIsSysAdmin = new SqlParameter() { ParameterName = "is_sys_admin", Value = isSysAdmin, DbType = DbType.Boolean };
            SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };
            SqlParameter pNoPay = new SqlParameter() { ParameterName = "no_pay", Value = NoPay, SqlDbType = SqlDbType.Bit };
            SqlParameter pProcessEnabled = new SqlParameter() { ParameterName = "process_enabled", Value = ProcessEnabled, SqlDbType = SqlDbType.Bit };
            SqlParameter pDeviceEnabled = new SqlParameter() { ParameterName = "device_enabled", Value = DeviceEnabled, SqlDbType = SqlDbType.Bit };
            SqlParameter pNeedZip = new SqlParameter() { ParameterName = "need_zip", Value = NeedZip, SqlDbType = SqlDbType.Bit };
            SqlParameter pNoCounter = new SqlParameter() { ParameterName = "no_counter", Value = NoCounter, SqlDbType = SqlDbType.Bit };
            SqlParameter pCounterUnavailable = new SqlParameter() { ParameterName = "counter_unavailable", Value = CounterUnavailable, SqlDbType = SqlDbType.Bit };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveServiceCame", pId, pIdServiceClaim, pDescr, pDateCame, pCounter, pIdServiceEngeneer, pIdServiceActionType, pIdCreator, pCounterColour, pIdAktScan, pIsSysAdmin, pSerialNum, pNoPay, pProcessEnabled, pDeviceEnabled, pNeedZip, pNoCounter, pCounterUnavailable);

            try
            {
                string id = dt.Rows[0]["id"].ToString();
                Uri uri = new Uri(String.Format("{0}/Claim/RemoteCreate4ZipClaim?idServiceCame={1}", DbModel.OdataServiceUri, id));
                DbModel.GetApiClient().DownloadString(uri);
            }
            catch (Exception)
            {
                throw new Exception("Инцидент для заявки на ЗИП НЕ создан. Акт СОХРАНЕН!");
            }
        }
        
        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_came", Value = id, DbType = DbType.Int32 };
            ExecuteStoredProcedure(Srvpl.sp, "closeServiceCame", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}