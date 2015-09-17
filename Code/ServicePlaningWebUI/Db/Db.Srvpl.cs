using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ServicePlaningWebUI.Db
{
    public partial class Db
    {
        public class Srvpl
        {
            #region Константы

            public const string sp = "ui_service_planing";
            public const string sp2 = "ui_service_planing_2";

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

            public static DataTable GetSelectionList2(string action, params SqlParameter[] sqlParams)
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp2, action, sqlParams);
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

            public static DataTable GetSerialNumList(string filterText)
            {
                SqlParameter pFilterText = new SqlParameter() { ParameterName = "filter_text", Value = filterText, DbType = DbType.AnsiString };

                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getSerialNumList", pFilterText);
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

            public static DataTable GeClaimFullNameSelectionList(string serialNum = null, int? idServiceCame = null, int? idServiceClaim = null)
            {
                SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };
                SqlParameter pIdServiceCame = new SqlParameter() { ParameterName = "id_service_came", Value = idServiceCame, DbType = DbType.AnsiString };
                SqlParameter pIdServiceClaim = new SqlParameter() { ParameterName = "id_service_claim", Value = idServiceClaim, DbType = DbType.AnsiString };

                return GetSelectionList2("getServiceClaimFullNameSelectionList", pSerialNum, pIdServiceCame, pIdServiceClaim);
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

            public static DataTable CheckDeviceTotalCounterIsNotTooBig(int idClaim, int counter, DateTime dateCame)
            {
                SqlParameter pIdClaim = new SqlParameter() { ParameterName = "id_service_claim", Value = idClaim, DbType = DbType.Int32 };
                SqlParameter pCounter = new SqlParameter() { ParameterName = "counter", Value = counter, DbType = DbType.Int32 };
                SqlParameter pDateCame = new SqlParameter() { ParameterName = "date_came", Value = dateCame, DbType = DbType.DateTime };

                DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "checkDeviceTotalCounterIsNotTooBig", pIdClaim, pCounter, pDateCame);
                //bool flag = false;

                //if (dt.Rows.Count > 0)
                //{
                //    flag = dt.Rows[0]["result"].ToString().Equals("1");
                //}

                return dt;
            }

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

            public static DataTable GetEngeneerDayAlloc(int idServiceEngeneer, DateTime dateMonth/*, int alreadyExec, int claimCount*/)
            {
                //SqlParameter pAlreadyExec = new SqlParameter() { ParameterName = "already_exec", Value = alreadyExec, DbType = DbType.Int32 };
                //SqlParameter pClaimCount = new SqlParameter() { ParameterName = "claim_count", Value = claimCount, DbType = DbType.Int32 };
                SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_service_engeneer", Value = idServiceEngeneer, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };

                DataTable dt = GetSelectionList("getEngeneerDayAlloc", /*pAlreadyExec, pClaimCount,*/ pIdServiceEngeneer, pDateMonth);

                return dt;
            }

            public static DataTable GetPlanExecuteContractorList(int idServiceEngeneer, DateTime? dateMonth, int? isDone, int? noSet, DateTime? dateBegin, DateTime? dateEnd)
            {
                SqlParameter pIsDone = new SqlParameter() { ParameterName = "is_done", Value = isDone, DbType = DbType.Int32 };
                SqlParameter pNoSet = new SqlParameter() { ParameterName = "no_set", Value = noSet, DbType = DbType.Int32 };
                SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_service_engeneer", Value = idServiceEngeneer, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.DateTime };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.DateTime };

                DataTable dt = GetSelectionList("getPlanExecuteContractorList", pIsDone, pNoSet, pIdServiceEngeneer, pDateMonth, pDateBegin, pDateEnd);

                return dt;
            }

            public static DataTable GetPlanExecuteDeviceList(int? idServiceEngeneer, DateTime? dateMonth, int? isDone, int? noSet, DateTime? dateBegin, DateTime? dateEnd, int? idServiceAdmin, int? idServiceManager)
            {
                SqlParameter pIsDone = new SqlParameter() { ParameterName = "is_done", Value = isDone, DbType = DbType.Int32 };
                SqlParameter pNoSet = new SqlParameter() { ParameterName = "no_set", Value = noSet, DbType = DbType.Int32 };
                SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_service_engeneer", Value = idServiceEngeneer, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.DateTime };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.DateTime };
            SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = idServiceAdmin, DbType = DbType.Int32 };
            SqlParameter pIdServiceManager = new SqlParameter() { ParameterName = "id_manager", Value = idServiceManager, DbType = DbType.Int32 };

            DataTable dt = GetSelectionList("getPlanExecuteDeviceList", pIsDone, pNoSet, pIdServiceEngeneer, pDateMonth, pDateBegin, pDateEnd, pIdServiceAdmin, pIdServiceManager);

                return dt;
            }

            public static DataTable GetPlanExecuteServAdminContractorList(int idServiceAdmin, DateTime? dateMonth, int? isDone, int? noSet, DateTime? dateBegin, DateTime? dateEnd)
            {
                SqlParameter pIsDone = new SqlParameter() { ParameterName = "is_done", Value = isDone, DbType = DbType.Int32 };
                SqlParameter pNoSet = new SqlParameter() { ParameterName = "no_set", Value = noSet, DbType = DbType.Int32 };
                SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = idServiceAdmin, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.DateTime };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.DateTime };

                DataTable dt = GetSelectionList("getPlanExecuteServAdminContractorList", pIsDone, pNoSet, pIdServiceAdmin, pDateMonth, pDateBegin, pDateEnd);

                return dt;
            }

            public static DataTable GetPlanExecuteServManagerContractorList(int idServiceManager, DateTime? dateMonth, int? isDone, int? noSet, DateTime? dateBegin, DateTime? dateEnd)
            {
                SqlParameter pIsDone = new SqlParameter() { ParameterName = "is_done", Value = isDone, DbType = DbType.Int32 };
                SqlParameter pNoSet = new SqlParameter() { ParameterName = "no_set", Value = noSet, DbType = DbType.Int32 };
                SqlParameter pIdServiceManager = new SqlParameter() { ParameterName = "id_manager", Value = idServiceManager, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.DateTime };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.DateTime };

                DataTable dt = GetSelectionList("getPlanExecuteServManagerContractorList", pIsDone, pNoSet, pIdServiceManager, pDateMonth, pDateBegin, pDateEnd);

                return dt;
            }

            public static DataTable GetCounterReportContractorContractList(int? idContractor, int? idServiceManager, DateTime? dateMonth)
            {
                SqlParameter pIdServiceManager = new SqlParameter() { ParameterName = "id_manager", Value = idServiceManager, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };

                DataTable dt = GetSelectionList("getCounterReportContractorContractsList", pIdServiceManager, pDateMonth, pIdContractor);

                return dt;
            }

            public static DataTable GetCounterReportContractorContractDeviceList(int? idContractor, int? idServiceManager, DateTime? dateMonth, int? wearBegin, int? wearEnd, int? loadingBegin, int? loadingEnd, string lstVendor, bool? hasCames)
            {
                SqlParameter pIdServiceManager = new SqlParameter() { ParameterName = "id_manager", Value = idServiceManager, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pWearBegin = new SqlParameter() { ParameterName = "wear_begin", Value = wearBegin, DbType = DbType.Int32 };
                SqlParameter pWearEnd = new SqlParameter() { ParameterName = "wear_end", Value = wearEnd, DbType = DbType.Int32 };
                SqlParameter pLoadingBegin = new SqlParameter() { ParameterName = "loading_begin", Value = loadingBegin, DbType = DbType.Int32 };
                SqlParameter pLoadingEnd = new SqlParameter() { ParameterName = "loading_end", Value = loadingEnd, DbType = DbType.Int32 };
                SqlParameter pLstVendor = new SqlParameter() { ParameterName = "lst_vendor", Value = lstVendor, DbType = DbType.AnsiString };
                SqlParameter pHasCames = new SqlParameter() { ParameterName = "has_cames", Value = hasCames, DbType = DbType.Boolean };

                DataTable dt = GetSelectionList("getCounterReportContractorContractDeviceList", pIdServiceManager, pDateMonth, pIdContractor, pWearBegin, pWearEnd, pLoadingBegin, pLoadingEnd, pLstVendor, pHasCames);

                return dt;
            }

            public static DataTable GetCounterReportContractorContractDeviceData(int? idContractor, int? idServiceManager, DateTime? dateMonth)
            {
                SqlParameter pIdServiceManager = new SqlParameter() { ParameterName = "id_manager", Value = idServiceManager, DbType = DbType.Int32 };
                SqlParameter pDateMonth = new SqlParameter() { ParameterName = "date_month", Value = dateMonth, DbType = DbType.DateTime };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                

                DataTable dt = GetSelectionList("getCounterReportContractorContractDeviceData", pIdServiceManager, pDateMonth, pIdContractor);

                return dt;
            }

            public static DataTable GetVendorSelectionList()
            {
                DataTable dt = GetSelectionList("getVendorSelectionList");
                return dt;
            }

            internal static DataTable GetContractorDevice(int idDevice, int idContract)
            {
                SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };

                var dt = ExecuteQueryStoredProcedure(sp, "getContract2Devices", pIdDevice, pIdContract);
                return dt;
            }
        }
    }
}