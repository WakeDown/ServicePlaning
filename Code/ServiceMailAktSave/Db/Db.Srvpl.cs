using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceMailAktSave.Db
{
    public partial class Db
    {
        public class Srvpl
        {
            #region Константы

            public const string sp = "ui_service_planing";

            #endregion

            #region Common

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetSelectionList(string action, params SqlParameter[] sqlParams)
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, action, sqlParams);
                return dt;
            }

            #endregion

            /// <summary>
            /// Контрагенты из программы Эталон (список выбора)
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractorShortSelectionList(string filterText = null, int idContractor = -1, bool orderByName = false, bool showInn = true, int idCity = -1, string address = null)
            {
                SqlParameter pFilterText = new SqlParameter() { ParameterName = "filter_text", Value = filterText, DbType = DbType.AnsiString };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pOrderByName = new SqlParameter() { ParameterName = "order_by_name", Value = orderByName, DbType = DbType.Boolean };
                SqlParameter pShowInn = new SqlParameter() { ParameterName = "show_inn", Value = showInn, DbType = DbType.Boolean };
                SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = idCity, DbType = DbType.Int32 };
                SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = address, DbType = DbType.AnsiString };

                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getContractorShortSelectionList", pFilterText, pIdContractor, pOrderByName, pShowInn, pIdCity, pAddress);
                return dt;
            }

            #region CartridgeTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetCartridgeTypeSelectionList()
            {
                return GetSelectionList("getCartridgeTypeSelectionList");
            }

            #endregion

            #region ServiceClaimStatuses

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceClaimStatusSelectionList()
            {
                return GetSelectionList("getServiceClaimStatusSelectionList");
            }

            #endregion
            
            #region ContractStatuses

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractStatusSelectionList()
            {
                return GetSelectionList("getContractStatusSelectionList"); 
            }

            #endregion

            #region ContractScheduleList

            public static DataTable GetContractScheduleList()
            {
                return GetSelectionList("getContractScheduleList");
            }

            #endregion

            #region ContractTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractTypeSelectionList()
            {
                return GetSelectionList("getContractTypeSelectionList");
            }

            #endregion

            #region DeviceImprints

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceImprintSelectionList()
            {
                return GetSelectionList("getDeviceImprintSelectionList");
            }

            #endregion


            #region DeviceOptions

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceOptionSelectionList()
            {
                return GetSelectionList("getDeviceOptionSelectionList");
            }

            #endregion

            #region Devices

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceSelectionList()
            {
                return GetSelectionList("getDeviceSelectionList");
            }


            /// <summary>
            /// FilteredSelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceSelectionList(int idContract, string deviceIds = null)
            {
                //id_contract для того чтобы органичить список "Без уже добавленых"
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pDeviceIds = new SqlParameter() { ParameterName = "lst_id_device", Value = deviceIds, DbType = DbType.AnsiString };

                return GetSelectionList("getDeviceSelectionList", pIdContract, pDeviceIds);
            }
            
            #endregion

            #region Contract2Devices

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            //public static DataTable GetContract2DevicesSelectionList()
            //{
            //    return GetSelectionList("getDeviceSelectionList");
            //}

            /// <summary>
            /// FilteredSelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContract2DevicesSelectionList(int idContract, int? idDevice = null, int? idCity = null, string address = null)
            {
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };
                SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = idCity, DbType = DbType.Int32 };
                SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = address, DbType = DbType.AnsiString };

                return GetSelectionList("getContract2DevicesSelectionList", pIdContract, pIdDevice, pIdCity, pAddress);
            }

            #endregion

            #region DeviceModels

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceModelSelectionList()
            {
                return GetSelectionList("getDeviceModelSelectionList");
            }

            #endregion

            #region DeviceTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceTypeSelectionList()
            {
                return GetSelectionList("getDeviceTypeSelectionList");
            }

            #endregion

            #region PrintTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetPrintTypeSelectionList()
            {
                return GetSelectionList("getPrintTypeSelectionList");
            }

            #endregion

            #region ServiceActionTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceActionTypeSelectionList()
            {
                return GetSelectionList("getServiceActionTypeSelectionList");
            }

            #endregion

            #region ServiceIntervals

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceIntervalSelectionList()
            {
                return GetSelectionList("getServiceIntervalSelectionList");
            }

            #endregion

            #region ServiceTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceTypeSelectionList()
            {
                return GetSelectionList("getServiceTypeSelectionList");
            }

            #endregion

            #region ServiceCameTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceClaimTypeSelectionList()
            {
                return GetSelectionList("getServiceClaimTypeSelectionList");
            }

            #endregion

            #region ServiceZones

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceZoneSelectionList()
            {
                return GetSelectionList("getServiceZoneSelectionList");
            }

            #endregion

            #region FilterSelectionLists


            public static DataTable GetManagerFilterSelectionList()
            {
                return GetSelectionList("getContractsManagerFilterSelectionList");
            }

            public static DataTable GetContractorFilterSelectionList()
            {
                return GetSelectionList("getContractContractorFilterSelectionList");
            }

            #endregion

            #region Contracts

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractSelectionList(int? idContract = null, string name = null)
            {
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = name, DbType = DbType.AnsiString };

                return GetSelectionList("getContractSelectionList", pIdContract, pName);
            }

            #endregion

            #region ContractZipStates

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractZipStateSelectionList()
            {
                return GetSelectionList("getContractZipStateSelectionList");
            }

            #endregion

            #region ServiceClaims

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceClaimSelectionList(int? idServiceClaim = null, int? idServiceClaimStatus = null)
            {
                SqlParameter pIdServiceClaim = new SqlParameter() { ParameterName = "id_service_claim", Value = idServiceClaim, DbType = DbType.Int32 };
                SqlParameter pIdServiceClaimStatus = new SqlParameter() { ParameterName = "id_service_claim_status", Value = idServiceClaimStatus, DbType = DbType.Int32 };

                return GetSelectionList("getServiceClaimSelectionList", pIdServiceClaim, pIdServiceClaimStatus);
            }

            public static DataTable GeClaimFullNameSelectionList(string serialNum = null, int? idServiceCame = null)
            {
                SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };
                SqlParameter pIdServiceCame = new SqlParameter() { ParameterName = "id_service_came", Value = idServiceCame, DbType = DbType.AnsiString };

                return GetSelectionList("getServiceClaimFullNameSelectionList", pSerialNum, pIdServiceCame);
            }

            #endregion

            #region Tariff

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable SetNewDeviceTariff(bool all, int idCreator)
            {
                SqlParameter pAll = new SqlParameter() { ParameterName = "all", Value = all, DbType = DbType.Boolean };
                SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

                return GetSelectionList("setNewDeviceTariff", pAll, pIdCreator);
            }

            #endregion

            #region UserRoles

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetUserRoleSelectionList()
            {
                return GetSelectionList("getUserRoleSelectionList");
            }

            #endregion

            #region PriceDiscont

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractPriceDiscountSelectionList()
            {
                return GetSelectionList("getPriceDiscountSelectionList");
            }



            #endregion

            #region Contract2Devices

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContract2DevicesAddressSelectionList(string filter = null, int idContractor = -1, int idCity = -1)
            {
                SqlParameter pFilter = new SqlParameter() { ParameterName = "address", Value = filter, DbType = DbType.AnsiString };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = idCity, DbType = DbType.Int32 };

                return GetSelectionList("getContract2DevicesAddressSelectionList", pFilter, pIdContractor, pIdCity);
            }

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContract2DevicesCitiesSelectionList(string filter = null, int idContractor = -1, string address = null)
            {
                SqlParameter pFilter = new SqlParameter() { ParameterName = "name", Value = filter, DbType = DbType.AnsiString };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = address, DbType = DbType.AnsiString };

                return GetSelectionList("getContract2DevicesCitiesSelectionList", pFilter, pIdContractor, pAddress);
            }

            #endregion


            internal static DataTable GetAddressesSelectionList()
            {
                return GetSelectionList("getSrvplAddressSelectionList");
            }

            public static bool CheckServiceIntervalNeedsGraphickList(int idServiceInterval)
            {
                SqlParameter pIdServiceInterval = new SqlParameter() { ParameterName = "id_service_interval", Value = idServiceInterval, DbType = DbType.Int32 };

                DataTable dt =  GetSelectionList("getServiceInterval", pIdServiceInterval);
                bool needsGraphickList = false;

                if (dt.Rows.Count > 0)
                {
                    needsGraphickList = dt.Rows[0]["needs_graphick_list"].ToString()=="1";
                }

                return needsGraphickList;
            }
        }
    }
}