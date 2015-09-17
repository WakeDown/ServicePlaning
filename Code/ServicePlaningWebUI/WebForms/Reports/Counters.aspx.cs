using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;

namespace ServicePlaningWebUI.WebForms.Reports
{

    public partial class Counters : BaseFilteredPage
    {

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsReport
        {
            get { return (Page.Master.Master as Site).UserIsReport; }
        }

        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        protected const string CounterDetailForm = "Reports/CountersDetail";

        protected override void FillFilterLinksDefaults()
        {
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("mth", txtDateMonth, DateTime.Now.ToString("MM.yyyy")));
            FilterLinks.Add(new FilterLink("mgr", ddlServiceManager, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("wst", txtWearBegin));
            FilterLinks.Add(new FilterLink("wen", txtWearEnd));
            FilterLinks.Add(new FilterLink("lst", txtLoadingBegin));
            FilterLinks.Add(new FilterLink("len", txtLoadingEnd));
            FilterLinks.Add(new FilterLink("ven", chklVendor));
            FilterLinks.Add(new FilterLink("hsc", rblHasCames));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);

            LoadTable();

            RegisterStartupScripts();
        }



        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlServiceManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.ChkListFill(ref chklVendor, Db.Db.Srvpl.GetVendorSelectionList());
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (!UserIsSysAdmin && !UserIsReport)
            //{
            //    Response.Redirect(FriendlyUrl.Href("~/Error"));
            //}

            SetDefaults();
        }

        private void SetDefaults()
        {
            if (!IsPostBack)
            {
                //sdsCounterReport.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        private void LoadTable()
        {

            //Выбираем все чтобы фильтровать на веб сервере при отрисовке таблицы
            int? idManager = MainHelper.DdlGetSelectedValueInt(ref ddlServiceManager);
            DateTime? dateMonth = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
            int? wearBegin = MainHelper.TxtGetTextInt32(ref txtWearBegin, true);
            int? wearEnd = MainHelper.TxtGetTextInt32(ref txtWearEnd, true);
            int? loadingBegin = MainHelper.TxtGetTextInt32(ref txtLoadingBegin, true);
            int? loadingEnd = MainHelper.TxtGetTextInt32(ref txtLoadingEnd, true);
            string lstVendor = MainHelper.ChkListGetCheckedValuesString(ref chklVendor);
            bool? hasCames = MainHelper.RblGetValueBool(ref rblHasCames, true);

            DeviceList = Db.Db.Srvpl.GetCounterReportContractorContractDeviceList(null, idManager, dateMonth, wearBegin, wearEnd, loadingBegin, loadingEnd, lstVendor, hasCames);

            //Список контрагентов формируем из списка выводимых аппаратов, чтобы не мучть SQL запросы фильтрами
            //  c = new {int Id};
            var contractorList = from d in DeviceList.AsEnumerable()
                                 group d by new { Id = d.Field<int>("id_contractor"), Name = d.Field<string>("contractor_name") } into grp
                                 select new
                                 {
                                     Id = grp.Key.Id,
                                     Name = grp.Key.Name,
                                     ContractorDevCount = grp.Count(),
                                     ContractorCurVol = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                                     ContractorPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                                     ContractorPrevPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0),
                                     ContractorCurVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0),
                                     ContractorPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0),
                                     ContractorPrevPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0),
                                     ContractorCurVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                                     ContractorPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                                     ContractorPrevPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0)
                                 };
            tblCounterReport.DataSource = contractorList.OrderBy(r => r.Name);
            tblCounterReport.DataBind();
            //-----

            FillTotalRow();


        }


        DataTable DeviceList { get; set; }
        //DataRow[] DeviceData { get; set; }
        //DataTable DeviceData { get; set; }
        //DataTable ContractsList { get; set; }

        protected void tblCounterReport_OnDataBinding(object sender, EventArgs e)
        {

        }

        private void FillTotalRow()
        {
            sDevCountTotal.InnerText = DeviceList.Rows.Count.ToString();
            //Объем печати за текущий месяц

            var curVol = DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0);
            var curVolColor =
                DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0);

            int iCurVol;
            int iCurVolColor;
            int curVolTotal;

            int.TryParse(curVol.ToString(), out iCurVol);
            int.TryParse(curVolColor.ToString(), out iCurVolColor);
            curVolTotal = iCurVol + iCurVolColor;
            sCurVolTotal.InnerText = String.Format("{0:### ### ### ### ### ### ###}", curVolTotal);
            //--

            //Объем печати за предыдущий месяц
            var prevVol = DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0);
            var prevVolColor =
                DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0);

            int iPrevVol;
            int iPrevVolColor;
            int prevVolTotal;

            int.TryParse(prevVol.ToString(), out iPrevVol);
            int.TryParse(prevVolColor.ToString(), out iPrevVolColor);
            prevVolTotal = iPrevVol + iPrevVolColor;
            sPrevVolTotal.InnerText = String.Format("{0:### ### ### ### ### ### ###}", prevVolTotal);
            //--

            //Объем печати за ПРЕДпредыдущий месяц
            var prevPrevVol = DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0);
            var prevPrevVolColor =
                DeviceList.AsEnumerable()
                    .Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0);

            int iPrevPrevVol;
            int iPrevPrevVolColor;
            int prevPrevVolTotal;

            int.TryParse(prevPrevVol.ToString(), out iPrevPrevVol);
            int.TryParse(prevPrevVolColor.ToString(), out iPrevPrevVolColor);
            prevPrevVolTotal = iPrevPrevVol + iPrevPrevVolColor;
            sPrevPrevVolTotal.InnerText = String.Format("{0:### ### ### ### ### ### ###}", prevPrevVolTotal);
            //--

            //sCurVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
            //sPrevVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
            //sPrevPrevVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
        }

        protected void tblCounterReport_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.FindControl("hfIdContractor") == null)
                return;

            //Подтягиваем список договоров для контрагента
            int idContractor = Convert.ToInt32((e.Item.FindControl("hfIdContractor") as HiddenField).Value);

            //int? idManager = MainHelper.DdlGetSelectedValueInt(ref ddlServiceManager);
            //DateTime? dateMonth = MainHelper.TxtGetTextDateTime(ref txtDateMonth);

            var rtrContractorContractList = (e.Item.FindControl("rtrContractorContractList") as Repeater);

            //var ctr = ContractsList.Select(String.Format("id_contractor = {0}", idContractor));
            //Список контрактов формируем из списка выводимых аппаратов, чтобы не мучть SQL запросы фильтрами
            var ctr = from d in DeviceList.Select(String.Format("id_contractor = {0}", idContractor)).AsEnumerable()
                      group d by new { IdContract = d.Field<int>("id_contract"), Number = d.Field<string>("contract_number"), IdContractor = d.Field<int>("id_contractor") } into grp
                      select new
                      {
                          IdContract = grp.Key.IdContract,
                          Number = grp.Key.Number,
                          IdContractor = grp.Key.IdContractor,
                          ContractDevCount = grp.Count(),
                          ContractCurVol = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                          ContractPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                          ContractPrevPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0),
                          ContractCurVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0),
                          ContractPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0),
                          ContractPrevPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0),
                          ContractCurVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                          ContractPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                          ContractPrevPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0)
                      };

            //

            //var devCount = DeviceList.Select(String.Format("id_contractor = {0}", idContractor)).Count();//ctr.Sum(r => r.Field<int>("dev_count"));
            //(e.Item.FindControl("sDevCount") as HtmlContainerControl).InnerText = devCount.ToString();

            //var ctrCurVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sContractorCurVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrCurVol);

            //var ctrPrevVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sContractorPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevVol);

            //var ctrPrevPrevVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sContractorPrevPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevPrevVol);

            //Выводим количество аппаратов у которых есть показания хотябы в одном из выбранных месяцев


            rtrContractorContractList.DataSource = ctr;
            rtrContractorContractList.DataBind();

        }

        protected void rtrContractorContractList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.FindControl("hfIdContract") == null)
                return;

            int idContract = Convert.ToInt32((e.Item.FindControl("hfIdContract") as HiddenField).Value);

            //if (DeviceData == null) return;

            StringBuilder list = new StringBuilder();

            //DataRow[] dev = DeviceList.Select(String.Format("id_contract = {0}", idContract));
            DataRow[] dev = DeviceList.Select(String.Format("id_contract = {0}", idContract));

            //(e.Item.FindControl("sContractDevCount") as HtmlContainerControl).InnerText = dev.Count().ToString();

            //var ctrCurVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sCurVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrCurVol);

            //var ctrPrevVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevVol);

            //var ctrPrevPrevVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            //(e.Item.FindControl("sPrevPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevPrevVol);

            int devWithVol = 0;

            foreach (DataRow row in dev)
            {
                //int idDevice = Convert.ToInt32(row[1]);

                //Объем печати
                //var cur = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and d_year={2} and d_month={3}", idContract, idDevice, GetCurMonth().Year, GetCurMonth().Month));

                //var prev = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and  d_year={2} and d_month={3}", idContract, idDevice, GetPrevMonth().Year, GetPrevMonth().Month));
                //var prevPrev = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and  d_year={2} and d_month={3}", idContract, idDevice, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month));

                int? curVol = null;
                int? prevVol = null;
                int? prevPrevVol = null;

                curVol = GetVol(row[16]); //cur.Any() ? cur[0][7] : String.Empty;
                prevVol = GetVol(row[17]); //prev.Any() ? prev[0][7] : String.Empty;
                prevPrevVol = GetVol(row[18]); //prevPrev.Any() ? prevPrev[0][7] : String.Empty;

                //Если есть хотябы одно значение в показаных месяцах
                bool hasVolInOneMonth = (!String.IsNullOrEmpty(curVol.ToString()) && int.Parse(curVol.ToString()) > 0) || (!String.IsNullOrEmpty(prevVol.ToString()) && int.Parse(prevVol.ToString()) > 0) ||
                                        (!String.IsNullOrEmpty(prevPrevVol.ToString()) && int.Parse(prevPrevVol.ToString()) > 0);

                if (hasVolInOneMonth) devWithVol++;

                //Загрузка аппарата
                int? maxVolume = Db.Db.GetValueIntOrNull(row[5]);

                decimal? curLoading = null;
                decimal? prevLoading = null;
                decimal? prevPrevLoading = null;

                if (maxVolume != null && maxVolume > 0)
                {
                    //Если объем печати 0 то загрузку не выводим

                    curLoading = GetLoading(row[19], curVol); //!String.IsNullOrEmpty(curVol.ToString()) ? Convert.ToDecimal(int.Parse(curVol.ToString()) / Convert.ToDecimal(maxVolume.Value)) : -999;
                    prevLoading = GetLoading(row[20], prevVol); //!String.IsNullOrEmpty(prevVol.ToString()) ? Convert.ToDecimal(int.Parse(prevVol.ToString())) / Convert.ToDecimal(maxVolume.Value) : -999;
                    prevPrevLoading = GetLoading(row[21], prevPrevVol); //!String.IsNullOrEmpty(prevPrevVol.ToString()) ? Convert.ToDecimal(int.Parse(prevPrevVol.ToString())) / Convert.ToDecimal(maxVolume.Value) : -999;


                }

                //string bg = !String.IsNullOrEmpty(prevVol.ToString()) && prev[0][12].ToString().Equals("1")
                //    ? "bg-warning"
                //    : String.Empty;

                //if (!String.IsNullOrEmpty(bg))
                //{
                //    string s = "";
                //}

                list.Append("<div class='row bg-white'>");

                list.Append(String.Format(@"<div class='col-sm-3'><span class='pad-l-mid'><span class='label label-mark small {2}'></span>&nbsp;<a target='_blank' href='{1}'>{0}</a></span></div>", row[2], GetRedirectUrlWithParams(String.Format("id={0}&cid={1}", row[1], row[0]), false, CounterDetailForm), row[23].ToString().Equals("1") ? "label-success success-light" : "label-empty"));//Имя аппарата , передаем в ссылку id аппарата и id договора
                list.Append(String.Format(@"<div class='col-sm-1'></div>"));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", curVol,
                    !String.IsNullOrEmpty(curVol.ToString()) && row[24].ToString().Equals("True") ? "bg-success" : String.Empty
                    ));//Выделяем цветом если выводится среднее значение
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", prevVol,
                    !String.IsNullOrEmpty(prevVol.ToString()) && row[25].ToString().Equals("True") ? "bg-success" : String.Empty
                    ));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", prevPrevVol,
                    !String.IsNullOrEmpty(prevPrevVol.ToString()) && row[26].ToString().Equals("True") ? "bg-success" : String.Empty
                    ));
                list.Append("<div class='col-sm-3'>");
                list.Append(String.Format(@"<div class='col-sm-3 align-right {1}'>{0:P0}</div>", curLoading, curLoading.HasValue && curLoading.Value > 1 ? "bg-danger" : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-3 align-right {1}'>{0:P0}</div>", prevLoading, prevLoading.HasValue && prevLoading.Value > 1 ? "bg-danger" : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-3 align-right {1}'>{0:P0}</div>", prevPrevLoading, prevPrevLoading.HasValue && prevPrevLoading.Value > 1 ? "bg-danger" : String.Empty));
                //list.Append(String.Format(@"<div class='col-sm-1'>{0:dd.MM.yyyy}</div>", row[3]));//Последняя дата счетчика
                list.Append(String.Format(@"<div class='col-sm-3 align-right bold text-success'>{0:P0}</div>", GetLoadingAverage(curLoading, prevLoading, prevPrevLoading)));
                list.Append("</div>");

                var wear = row[6];
                double dWear;
                double.TryParse(wear.ToString(), out dWear);

                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:P0}</div>", wear, dWear >= 1 ? "bg-danger" : dWear >= 0.8 ? "bg-warning" : String.Empty));//Износ
                list.Append(String.Format(@"<div class='col-sm-1 align-right'>{0:### ### ### ### ### ###}</div>", row[4]));//Текущий счетчик
                list.Append("</div>");
            }

            var sContractDevCountWithVol = e.Item.FindControl("sContractDevCountWithVol") as HtmlContainerControl;
            sContractDevCountWithVol.InnerText = devWithVol.ToString();

            //Заполняем количество аппаратов со значениями хотябы в одном месяце из показанных для контрагента
            var sDevCountWithVol = (e.Item.Parent.Parent as RepeaterItem).FindControl("sDevCountWithVol") as HtmlContainerControl;
            if (sDevCountWithVol != null)
            {
                int devCountWithVol;
                int.TryParse(sDevCountWithVol.InnerText, out devCountWithVol);
                devCountWithVol = devCountWithVol + devWithVol;
                sDevCountWithVol.InnerText = devCountWithVol.ToString();
            }
            //---


            //Заполняем общее количество аппаратов со значениями хотябы в одном месяце из показанных
            int devCountWithVolTotal;
            int.TryParse(sDevCountWithVolTotal.InnerText, out devCountWithVolTotal);
            devCountWithVolTotal = devCountWithVolTotal + devWithVol;
            sDevCountWithVolTotal.InnerText = devCountWithVolTotal.ToString();
            //---

            var phContractorContractsDevicesList = e.Item.FindControl("phContractorContractsDevicesList") as HtmlContainerControl;
            phContractorContractsDevicesList.InnerHtml = list.ToString();
        }

        //private IEnumerable<object> FilterDeviceDataList(DataTable devList, DataTable dataList)
        //{
        //    //DataRow[] lst = new DataRow[0];

        //    var result = from dev in devList.AsEnumerable()
        //                 join data in dataList.AsEnumerable() on (int)dev["id_device"] equals (int)data["id_device"]
        //                 select new
        //                 {
        //                     IdDevice = data["id_device"]
        //                 };

        //    return result;
        //}

        private decimal? GetLoadingAverage(params decimal?[] arr)
        {
            List<decimal> list = new List<decimal>();
            decimal? average = null;

            foreach (decimal? d in arr)
            {
                if (d != null)
                {
                    list.Add(d.Value);
                }
            }

            if (list.Any())
            {
                average = list.ToArray().Average();
            }

            return average;
        }

        private decimal? GetLoading(object obj, int? vol)
        {
            var ret = vol != null && obj != null && !String.IsNullOrEmpty(obj.ToString()) && obj != DBNull.Value
                ? Convert.ToDecimal(obj) : -999;
            if (ret == -999)
            {
                return null;
            }

            return (ret);
        }
        private int? GetVol(object obj)
        {
            var ret = obj != null && !String.IsNullOrEmpty(obj.ToString()) && obj != DBNull.Value ? Convert.ToInt32(obj) : -999;
            if (ret == -999 || ret == 0)
            {
                return null;
            }

            return (ret);
        }
        protected DateTime GetCurMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);

            return dt;
        }
        protected DateTime GetPrevMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
            return dt.AddMonths(-1);
        }
        protected DateTime GetPrevPrevMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
            return dt.AddMonths(-2);
        }

        protected string GetItem(object dataItem, int i)
        {
            return (dataItem as DataRow)[i].ToString();
        }

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate.ClientID, chklVendor.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);

            //            script = String.Format(@"$('#excloContractor').on('click', function() {{
            //if ($(this).hasClass('closed')) {{
            //    $('.contractorContractList').collapse('show');
            //    $(this).removeClass('closed').addClass('shown');
            //}}
            //else if ($(this).hasClass('shown')) {{
            //    $('.contractorContractList').collapse('hide');
            //    $(this).removeClass('shown').addClass('closed');
            //}}
            //}});");

            //            ScriptManager.RegisterStartupScript(this, GetType(), "excloContractor", script, true);

            //            script = String.Format(@"$('#excloContract').on('click', function() {{
            //if ($(this).hasClass('closed') || $('#excloContractor').hasClass('closed')) {{
            //    $('.contractorContractList').collapse('show');
            //    $('#excloContractor').removeClass('closed').addClass('shown');
            //
            //    $('.contractDeviceList').collapse('show');
            //    $(this).removeClass('closed').addClass('shown');
            //}}
            //else if ($(this).hasClass('shown')) {{
            //    $('.contractDeviceList').collapse('hide');
            //    $(this).removeClass('shown').addClass('closed');
            //}}
            //}});");

            //            ScriptManager.RegisterStartupScript(this, GetType(), "excloContract", script, true);


            script = String.Format(@"$(document).ready(function() {{
    $('.contractorContractList').collapse({{'toggle': false}});
    $('.contractDeviceList').collapse({{'toggle': false}});
}});");
            ScriptManager.RegisterStartupScript(this, GetType(), "excloFixBug", script, true);

            script = String.Format(@"$('#exclo1').on('click', function() {{
    $('.contractorContractList').collapse('hide');
$('.contractDeviceList').collapse('hide');
}});");

            ScriptManager.RegisterStartupScript(this, GetType(), "exclo1", script, true);

            script = String.Format(@"$('#exclo2').on('click', function() {{
    $('.contractorContractList').collapse('show');
$('.contractDeviceList').collapse('hide');
}});");

            ScriptManager.RegisterStartupScript(this, GetType(), "exclo2", script, true);

            script = String.Format(@"$('#exclo3').on('click', function() {{
    $('.contractorContractList').collapse('show');
$('.contractDeviceList').collapse('show');
}});");

            ScriptManager.RegisterStartupScript(this, GetType(), "exclo3", script, true);
        }

        protected void btnInExcel_OnClick(object sender, EventArgs e)
        {
            /*   int? idManager = MainHelper.DdlGetSelectedValueInt(ref ddlServiceManager);
               DateTime? dateMonth = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
               int? wearBegin = MainHelper.TxtGetTextInt32(ref txtWearBegin, true);
               int? wearEnd = MainHelper.TxtGetTextInt32(ref txtWearEnd, true);
               int? loadingBegin = MainHelper.TxtGetTextInt32(ref txtLoadingBegin, true);
               int? loadingEnd = MainHelper.TxtGetTextInt32(ref txtLoadingEnd, true);
               string lstVendor = MainHelper.ChkListGetCheckedValuesString(ref chklVendor);
               bool? hasCames = MainHelper.RblGetValueBool(ref rblHasCames, true);

               DeviceList = Db.Db.Srvpl.GetCounterReportContractorContractDeviceList(null, idManager, dateMonth, wearBegin, wearEnd, loadingBegin, loadingEnd, lstVendor, hasCames);*/

            //Список контрагентов формируем из списка выводимых аппаратов, чтобы не мучть SQL запросы фильтрами

            var contractorList = from d in DeviceList.AsEnumerable()
                                 group d by new { Id = d.Field<int>("id_contractor"), Name = d.Field<string>("contractor_name") } into grp
                                 select new
                                 {
                                     Id = grp.Key.Id,
                                     Name = grp.Key.Name,
                                     ContractorDevCount = grp.Count(),
                                     ContractorCurVol = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                                     ContractorPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                                     ContractorPrevPrevVol = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0),
                                     ContractorCurVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0),
                                     ContractorPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0),
                                     ContractorPrevPrevVolColor = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0),
                                     ContractorCurVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                                     ContractorPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                                     ContractorPrevPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0)
                                 };
            tblCounterReport.DataSource = contractorList.OrderBy(r => r.Name);
            tblCounterReport.DataBind();
            XLWorkbook exRep = new XLWorkbook();
            IXLWorksheet repSh;
            const int startCol = 1;
            int row = 3, col = 1;
            XLColor green = XLColor.FromArgb(223, 240, 216), red = XLColor.FromArgb(242, 222, 222), yellow = XLColor.FromArgb(252, 248, 227);
            repSh = exRep.Worksheets.Add("Список");
            repSh.SheetView.Freeze(2,1);
            repSh.Style.Font.FontSize = 10;
            repSh.Range(1,1 , 2,12).Style.Fill.BackgroundColor = XLColor.PaleCornflowerBlue;
            
          
          
            foreach (var ctrs in contractorList.OrderBy(r => r.Name))
            {
                int? ctrsDvWAv = 0;
                int ctrsgrp = row + 1;
                repSh.Cell(row, col).Value = ctrs.Name;
                col++;
                
                repSh.Cell(row, col).Value = ctrs.ContractorDevCount;
                col++;
                int tRow = row, tCol = col;
                col++;
                if (!String.IsNullOrEmpty(ctrs.ContractorCurVolTotal.ToString()) && int.Parse(ctrs.ContractorCurVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = ctrs.ContractorCurVolTotal;}
                col++;
                if (!String.IsNullOrEmpty(ctrs.ContractorPrevVolTotal.ToString()) && int.Parse(ctrs.ContractorPrevVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = ctrs.ContractorPrevVolTotal; }
                col++;
                if (!String.IsNullOrEmpty(ctrs.ContractorPrevPrevVolTotal.ToString()) && int.Parse(ctrs.ContractorPrevPrevVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = ctrs.ContractorPrevPrevVolTotal; }
                
               
                row++;
                col = startCol;
                var contracts = from d in DeviceList.Select(String.Format("id_contractor = {0}", ctrs.Id.ToString())).AsEnumerable()
                                group d by new { IdContract = d.Field<int>("id_contract"), Number = d.Field<string>("contract_number"), IdContractor = d.Field<int>("id_contractor") } into grp
                                select new
                                {
                                    IdContract = grp.Key.IdContract,
                                    Number = grp.Key.Number,
                                    IdContractor = grp.Key.IdContractor,
                                    ContractDevCount = grp.Count(),
                                    ContractCurVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol_color"].ToString()) ? (int)v["cur_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["cur_vol"].ToString()) ? (int)v["cur_vol"] : 0),
                                    ContractPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol_color"].ToString()) ? (int)v["prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_vol"].ToString()) ? (int)v["prev_vol"] : 0),
                                    ContractPrevPrevVolTotal = grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol_color"].ToString()) ? (int)v["prev_prev_vol_color"] : 0) + grp.Sum(v => !String.IsNullOrEmpty(v["prev_prev_vol"].ToString()) ? (int)v["prev_prev_vol"] : 0)
                                };
                foreach (var cts in contracts)
                {
                    int ctsgrp = row + 1;
                    int? ctsDvWAv = 0;
                    repSh.Cell(row, col).Value = cts.Number;
                    repSh.Cell(row, col).Style.Alignment.Indent = 1;
                    col++;
                    repSh.Cell(row, col).Value = cts.ContractDevCount;
                    col++;
                    int tRow2 = row, tCol2 = col;
                    col++;
                    if (!String.IsNullOrEmpty(cts.ContractCurVolTotal.ToString()) && int.Parse(cts.ContractCurVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = cts.ContractCurVolTotal; }
                    col++;
                    if (!String.IsNullOrEmpty(cts.ContractPrevVolTotal.ToString()) && int.Parse(cts.ContractPrevVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = cts.ContractPrevVolTotal; }
                    col++;
                    if (!String.IsNullOrEmpty(cts.ContractPrevPrevVolTotal.ToString()) && int.Parse(cts.ContractPrevPrevVolTotal.ToString()) > 0) { repSh.Cell(row, col).Value = cts.ContractPrevPrevVolTotal; }
                   
                    row++;
                    col = startCol;
                    DataRow[] dev = DeviceList.Select(String.Format("id_contract = {0}", cts.IdContract));
                    foreach (var dv in dev)
                    {
                        repSh.Cell(row, col).Value = dv[2];
                        repSh.Cell(row, col).Style.Alignment.Indent = 2;
                        col += 3;
                        var curVol = GetVol(dv[16]);
                        repSh.Cell(row, col).Value = curVol;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = curVol!=null && dv[24].ToString().Equals("True") ? green :XLColor.NoColor;
                        col++;
                        var prevVol = GetVol(dv[17]);
                        repSh.Cell(row, col).Value = prevVol;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = prevVol!=null && dv[25].ToString().Equals("True") ? green :XLColor.NoColor;
                        col++;
                        var prevPrevVol = GetVol(dv[18]);
                        repSh.Cell(row, col).Value = prevPrevVol;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = prevPrevVol != null && dv[26].ToString().Equals("True") ? green : XLColor.NoColor;
                        col++;
                        var curLoading = GetLoading(dv[19], curVol);
                        repSh.Cell(row, col).Value = curLoading;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = curLoading > 1 ? red : XLColor.NoColor;
                        col++;
                        var prevLoading = GetLoading(dv[20], prevVol);
                        repSh.Cell(row, col).Value = prevLoading;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = prevLoading > 1 ? red : XLColor.NoColor;
                        col++;
                        var prevPrevLoading = GetLoading(dv[21],prevPrevVol);
                        repSh.Cell(row, col).Value = prevPrevLoading;
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = prevPrevLoading > 1 ? red : XLColor.NoColor;
                        col++;
                        repSh.Cell(row, col).Value = GetLoadingAverage(curLoading, prevLoading, prevPrevLoading);
                        repSh.Cell(row, col).Style.Font.SetBold();
                        repSh.Cell(row, col).Style.Font.SetFontColor(XLColor.FromArgb(60, 118, 61));
                        ctsDvWAv += curVol != null || prevVol != null || prevPrevVol != null ? 1 : 0;
                        //ctsDvWAv += GetLoadingAverage(curLoading, prevLoading, prevPrevLoading) != null ? 1 : 0;
                        col++;
                        double wear;
                        double.TryParse(dv[6].ToString(), out wear);
                        if (wear != 0) {repSh.Cell(row,col).Value = wear;}
                        repSh.Cell(row, col).Style.Fill.BackgroundColor = wear > 1 ? red : wear > 0.8 ? yellow : XLColor.NoColor;
                        col++;
                        if (!string.IsNullOrEmpty(dv[4].ToString()) && int.Parse(dv[4].ToString()) > 0) {repSh.Cell(row,col).Value = dv[4];}
                       
                        col = startCol;
                        row++;
                    }
                    ctrsDvWAv += ctsDvWAv;
                    repSh.Cell(tRow2, tCol2).Value = ctsDvWAv;
                    repSh.Rows(ctsgrp, row - 1).Group();
                }
                repSh.Cell(tRow, tCol).Value = ctrsDvWAv;
                repSh.Rows(ctrsgrp, row - 1).Group();
            }
            repSh.Range(1, 2, 1, 12).Style.Font.SetBold();
            repSh.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Top;
            repSh.Cell(2, 4).Value = GetCurMonth();
            repSh.Cell(2, 5).Value = GetPrevMonth();
            repSh.Cell(2, 6).Value = GetPrevPrevMonth();
            repSh.Cell(2, 7).Value = GetCurMonth();
            repSh.Cell(2, 8).Value = GetPrevMonth();
            repSh.Cell(2, 9).Value = GetPrevPrevMonth();
            repSh.Range(3, 4, row, 6).Style.NumberFormat.Format = "#,##0";
            repSh.Range(3, 12, row, 12).Style.NumberFormat.Format = "#,##0";
            repSh.Range(1,7 , row,11).Style.NumberFormat.Format = "0%";
            repSh.Range(2, 4, 2, 9).Style.DateFormat.Format = "mmm yyyy";
            repSh.Columns().AdjustToContents();
            repSh.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            repSh.Range(1, 2, 1, 3).Merge().Value = "Аппараты";
            repSh.Range(1, 4, 1, 6).Merge().Value = "Объем печати";
            repSh.Range(1, 7, 1, 10).Merge().Value = "Загрузка";
            repSh.Range(1, 11, 2, 11).Merge().Value = "Износ";
            repSh.Range(1, 12, 2, 12).Merge().Value = "Последний счетчик";
            repSh.Range(1, 12, 2, 12).Cells().Style.Alignment.WrapText = true;
            repSh.Column(12).Width = 11;
            repSh.Cell(2, 2).Value = "Всего";
            repSh.Cell(2, 3).Value = "Показания";
            repSh.Cell(2, 10).Value = "средняя";
           // repSh.Range(1, 7, 1, 10).w
            repSh.Columns(7, 9).Width = 9;
            repSh.Range(2, 1, row, 12).SetAutoFilter();
            repSh.CollapseRows();
            MemoryStream fs = new MemoryStream();
            exRep.SaveAs(fs);
            fs.Position = 0;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(fs.ToArray());
            Response.End();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xlsx");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //pnlReport.RenderControl(hw);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.Close();

            //Response.Clear();
            //Response.AddHeader("content-disposition", "attachment;filename=FileName.xlsx");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //pnlReport.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
            //string fileName = "CounterReport";
            //string html = HdnValue.Value;
            //html = html.Replace("&gt;", ">");
            //html = html.Replace("&lt;", "<");
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
            //HttpContext.Current.Response.ContentType = "application/xls";
            //HttpContext.Current.Response.Write(html);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.Close();
            //HttpContext.Current.Response.End();

        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }
    }
}