using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class ServiceClaim : Db.Db
    {
        public int Id;
        public int IdDevice;
        public int IdContract2Devices;
        public int[] LstIdContract2Devices;
        public int IdContract;
        public int? IdServiceClaimType;
        public DateTime? PlaningDate;
        public string Number;
        public int? IdServiceEngeneer;
        public string Descr;
        public int? IdCreator;
        public int? OrderNum;
        public int IdServiceClaimStatus;

        public ServiceClaim()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public ServiceClaim(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_claim", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "getServiceClaim", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_service_claim"];
                IdContract2Devices = (int)dr["id_contract2devices"];
                IdDevice = (int)dr["id_device"];
                IdContract = (int)dr["id_contract"];
                IdServiceClaimType = GetValueIntOrNull(dr["id_service_claim_type"].ToString());
                PlaningDate = (DateTime)dr["planing_date"];
                Number = dr["number"].ToString();
                IdServiceEngeneer = GetValueIntOrNull(dr["id_service_engeneer"].ToString());
                OrderNum = (int)dr["order_num"];
                Descr = dr["descr"].ToString();
                IdCreator = (int)dr["id_creator"];
                IdServiceClaimStatus = (int)dr["id_service_claim_status"];

            }
        }

        public void Save(bool isManual = false)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_claim", Value = Id, DbType = DbType.Int32 };
            
            //SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = IdDevice, DbType = DbType.Int32 };
            string lstIdContract2Device = LstIdContract2Devices == null ? IdContract2Devices.ToString() : string.Join(",", LstIdContract2Devices);
            SqlParameter pLstIdContract2Device = new SqlParameter() { ParameterName = "lst_id_contract2devices", Value = lstIdContract2Device, DbType = DbType.AnsiString };

            //SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = IdContract, DbType = DbType.Int32 };
            SqlParameter pIdServiceClaimType = new SqlParameter() { ParameterName = "id_service_claim_type", Value = IdServiceClaimType, DbType = DbType.Int32 };
            SqlParameter pPlaningDate = new SqlParameter() { ParameterName = "planing_date", Value = PlaningDate, DbType = DbType.DateTime };
            SqlParameter pNumber = new SqlParameter() { ParameterName = "number", Value = Number, DbType = DbType.AnsiString };
            SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_service_engeneer", Value = IdServiceEngeneer, DbType = DbType.Int32 };
            SqlParameter pOrderNum = new SqlParameter() { ParameterName = "order_num", Value = OrderNum, DbType = DbType.Int32 };
            SqlParameter pDescr = new SqlParameter() { ParameterName = "descr", Value = Descr, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pIsManual = new SqlParameter() { ParameterName = "is_manual", Value = isManual, DbType = DbType.Boolean };//Ручное сохранение

            //SqlParameter pIdServiceClaimStatus = new SqlParameter() { ParameterName = "id_service_claim_status", Value = IdServiceClaimStatus, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveServiceClaim", pId, /*pIdDevice,*/pLstIdContract2Device, /*pIdContract, */ pIdServiceClaimType, pPlaningDate, pNumber, pIdServiceEngeneer, pOrderNum, pDescr, pIdCreator, pIsManual/*, pIdServiceClaimStatus*/);
        }

        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_service_claim", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeServiceClaim", pId, pIdCreator);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}