using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class Contract : Db.Db, IDbObject<int>
    {
        public int Id;
        public string Number;
        public decimal? Price;
        public int? IdServiceType;
        public int? IdContractType;
        public int? IdContractor;
        public int? IdContractStatus;
        public int? IdManager;
        public DateTime? DateBegin;
        public DateTime? DateEnd;
        public int? IdCreator;
        public int? IdZipState;
        public string Note;
        public int? IdContractProlong;
        public int? IdPriceDiscount;
        public bool PeriodReduction;
        public int? HandlingDevices;
        public bool? ClientSdNumRequired;

        public Contract()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Contract(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_contract"];
                Number = dr["number"].ToString();
                Price = GetValueDeciamlOrNull(dr["price"].ToString());
                IdServiceType = GetValueIntOrNull(dr["id_service_type"].ToString());
                IdContractType = (int)dr["id_contract_type"];
                IdContractor = (int)dr["id_contractor"];
                IdContractStatus = (int)dr["id_contract_status"];
                IdManager = (int)dr["id_manager"];
                DateBegin = GetValueDateTimeOrNull(dr["date_begin"].ToString());
                DateEnd = GetValueDateTimeOrNull(dr["date_end"].ToString());
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());
                IdZipState = GetValueIntOrNull(dr["id_zip_state"].ToString());
                Note = dr["note"].ToString();
                IdContractProlong = GetValueIntOrNull(dr["id_contract_prolong"].ToString());
                IdPriceDiscount = GetValueIntOrNull(dr["id_price_discount"].ToString());
                PeriodReduction = GetValueBool(dr["period_reduction"].ToString());
                HandlingDevices = GetValueIntOrNull(dr["handling_devices"].ToString());
                ClientSdNumRequired = GetValueBoolOrNull(dr["client_sd_num_required"].ToString());
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = Id, DbType = DbType.Int32 };
            SqlParameter pNumber = new SqlParameter() { ParameterName = "number", Value = Number, DbType = DbType.AnsiString };
            SqlParameter pPrice = new SqlParameter() { ParameterName = "price", Value = Price, DbType = DbType.Decimal };
            SqlParameter pIdServiceType = new SqlParameter() { ParameterName = "id_service_type", Value = IdServiceType, DbType = DbType.Int32 };
            SqlParameter pIdContractType = new SqlParameter() { ParameterName = "id_contract_type", Value = IdContractType, DbType = DbType.Int32 };
            SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = IdContractor, DbType = DbType.Int32 };
            SqlParameter pIdContractStatus = new SqlParameter() { ParameterName = "id_contract_status", Value = IdContractStatus, DbType = DbType.Int32 };
            SqlParameter pIdManager = new SqlParameter() { ParameterName = "id_manager", Value = IdManager, DbType = DbType.Int32 };
            SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = DateBegin, DbType = DbType.DateTime };
            SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = DateEnd, DbType = DbType.DateTime };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pIdZipState = new SqlParameter() { ParameterName = "id_zip_state", Value = IdZipState, DbType = DbType.Int32 };
            SqlParameter pNote = new SqlParameter() { ParameterName = "note", Value = Note, DbType = DbType.AnsiString };
            SqlParameter pIdContractProlong = new SqlParameter() { ParameterName = "id_contract_prolong", Value = IdContractProlong, DbType = DbType.Int32 };
            SqlParameter pIdPriceDiscount = new SqlParameter() { ParameterName = "id_price_discount", Value = IdPriceDiscount, DbType = DbType.Int32 };
            SqlParameter pHandlingDevices = new SqlParameter() { ParameterName = "handling_devices", Value = HandlingDevices, DbType = DbType.Int32 };
            SqlParameter pClientSdNumRequired = new SqlParameter() { ParameterName = "client_sd_num_required", Value = ClientSdNumRequired, SqlDbType = SqlDbType.Bit };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveContract", pId, pNumber, pPrice, pIdServiceType, pIdContractType, pIdContractor, pIdContractStatus, pIdManager, pDateBegin, pDateEnd, pIdCreator, pIdZipState, pNote, pIdContractProlong, pIdPriceDiscount, pHandlingDevices, pClientSdNumRequired);


            //TODO: Перенести исключения на уровень БД!!!

            if (dt.Rows.Count > 0)
            {
                Id = (int)dt.Rows[0]["id_contract"];
            }
            else
            {
                throw new Exception("При сохранении договора не вернулся его ID.");
            }
        }

        public static void SetPeriodReduction(int idContract, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "setPeriodReduction", pId, pIdCreator);
        }


        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeContract", pId);
        }


        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }

        public void DevicesProlong()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdContractProlong = new SqlParameter() { ParameterName = "id_contract_prolong", Value = IdContractProlong, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "copyContract2Devices", pId, pIdContractProlong, pIdCreator);
        }

        public static void Deactivate(int idContract, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "deactivateContract", pId, pIdCreator);
        }

        public static void Pause(int idContract, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "pauseContract", pId, pIdCreator);
        }

        public static void Enable(int idContract, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "enableContract", pId, pIdCreator);
        }

        public void AddFakeClaims(int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "addContractFakeClaims", pId, pIdCreator);
        }
    }
}