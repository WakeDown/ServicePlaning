using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class Payment : BaseFilteredPage
    {
        private string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        private string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];

        protected bool UserIsServiceManager
        {
            get { return (Page.Master.Master as Site).UserIsServiceManager; }
        }

        protected bool UserIsServiceEngeneer
        {
            get { return (Page.Master.Master as Site).UserIsServiceEngeneer; }
        }

        protected bool UserIsServiceAdmin
        {
            get { return (Page.Master.Master as Site).UserIsServiceAdmin; }
        }

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsReport
        {
            get { return (Page.Master.Master as Site).UserIsReport; }
        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("rep", rblReportType));
            FilterLinks.Add(new FilterLink("eng", ddlServiceEngeneer, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("mgr", ddlServiceManager, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("dst", txtDateClaimBegin));
            FilterLinks.Add(new FilterLink("den", txtDateClaimEnd));
            FilterLinks.Add(new FilterLink("mth", txtDateMonth, DateTime.Now.ToString("MM.yyyy")));
            FilterLinks.Add(new FilterLink("dne", rblDone, "-13"));
            FilterLinks.Add(new FilterLink("nst", rblNoSet, "1"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        private bool IsSlideShow
        {
            get
            {
                return Request.QueryString["output"] != null && Request.QueryString["output"] == "execRep";
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (IsSlideShow) //Для показа по телевизору
            {
                lblLasRefresh.Visible = true;
                Timer1.Enabled = true;
                //rblReportType.Style.Add("display", "none");
            }

            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);



            SetActiveView();

                RegisterStartupScripts();
        }

        private void FormPartsAccess()
        {
            if (UserIsServiceManager)
            {
                foreach (ListItem item in rblReportType.Items)
                {
                    if (!item.Value.Equals("execServManagers")) item.Enabled = false;
                }

                mvReports.SetActiveView(vReportExecManagers);
            }
        }

        private void SetActiveView()
        {
            View actView;

            switch (rblReportType.SelectedValue)
            {
                //case "dev":
                //    actView = vReportDev;
                //    break;
                case "execEng":
                    actView = vReportExecEngeneers;
                    break;
                case "execServAdmins":
                    actView = vReportExecServAdmins;
                    break;
                case "execServManagers":
                    actView = vReportExecManagers;
                    break;
                case "execEngAlloc":
                    actView = vReportAllocEngeneers;
                    break;
                default:
                    //if (UserIsServiceAdmin) { actView = vReportExecServAdmins; }
                    //else if (UserIsServiceEngeneer) { actView = vReportExecEngeneers; }
                    //else {actView = vReportExecEngeneers;}
                    actView = vNull;
                    break;
            }

            mvReports.SetActiveView(actView);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetDefaults();

            if (!UserIsServiceEngeneer && !UserIsServiceAdmin && !UserIsServiceManager && !UserIsSysAdmin && !UserIsReport)
            {
                Response.Redirect(FriendlyUrl.Href("~/Error"));
            }

            if (!IsPostBack)
            {
                FormPartsAccess();
            }

            if (UserIsSysAdmin && !IsSlideShow)
            {
                btnSlideshow.Visible = true;
            }
        }

        protected void btnSlideshow_OnClick(object sender, EventArgs e)
        {
            RedirectWithParams("output=execRep&hidenav=1");
        }

        private void SetDefaults()
        {
            if (UserIsServiceEngeneer)
            {
                MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, User.Id, false);
                sdsListExecByEngeneers.SelectParameters["id_service_engeneer"].DefaultValue = User.Id.ToString();
                sdsEngeneersAlloc.SelectParameters["id_service_engeneer"].DefaultValue = User.Id.ToString();
            }
            if (UserIsServiceAdmin)
            {
                MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, User.Id, false);
                //sdsListDev.SelectParameters["id_service_admin"].DefaultValue = sdsListExecByEngeneers.SelectParameters["id_service_admin"].DefaultValue 
                sdsListExecByServAdmins.SelectParameters["id_service_admin"].DefaultValue = User.Id.ToString();
            }
            if (UserIsServiceManager)
            {
                MainHelper.DdlSetSelectedValue(ref ddlServiceManager, User.Id, false);
                //sdsListDev.SelectParameters["id_service_admin"].DefaultValue = sdsListExecByEngeneers.SelectParameters["id_service_admin"].DefaultValue 
                sdsListExecByServManagers.SelectParameters["id_manager"].DefaultValue = User.Id.ToString();
            }

            if (!IsPostBack)
            {
                sdsListExecByEngeneers.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
                sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
                sdsListExecByServManagers.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }


        }

        private void FillFilterLists()
        {

            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
        }

        //protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        //{
        //    int count = e.AffectedRows;
        //    SetRowsCount(count);
        //}

        //private void SetRowsCount(int count = 0)
        //{
        //    lRowsCountDev.Text = count.ToString();
        //}

        private void RegisterStartupScripts()
        {
            string script = string.Empty;

            script = String.Format(@"var GetCheckedDeviceIds = function() {{ var allVals = [];$('#{0} :checkbox:checked').each(function() {{allVals.push($(this).val());}}); $('#{1}').attr('value',  allVals.join());   }};", tblListExecEngeneersContainer.ClientID, hfCheckedDeviceIds.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "GetCheckedDeviceIdsScript", script, true);

            btnPrint.OnClientClick = "GetCheckedDeviceIds()";
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void SetExecEngeneersSum()
        {
            //SetCountExec();

            DataView dv = (DataView)sdsListExecByEngeneers.Select(DataSourceSelectArguments.Empty);

            string plan_cnt = dv.Table.Compute("Sum(plan_cnt)", "").ToString();
            string done_cnt = dv.Table.Compute("Sum(done_cnt)", "").ToString();
            string residue = dv.Table.Compute("Sum(residue)", "").ToString();

            var lPlanSum = tblListExecEngeneers.Controls[tblListExecEngeneers.Controls.Count - 1].Controls[0].FindControl("lPlanSum") as Literal;
            var lDoneSum = tblListExecEngeneers.Controls[tblListExecEngeneers.Controls.Count - 1].Controls[0].FindControl("lDoneSum") as Literal;
            var lResitudeSum = tblListExecEngeneers.Controls[tblListExecEngeneers.Controls.Count - 1].Controls[0].FindControl("lResitudeSum") as Literal;

            if (lPlanSum != null)
            {
                lPlanSum.Text = plan_cnt;
            }

            if (lDoneSum != null)
            {
                lDoneSum.Text = done_cnt;
            }

            if (lResitudeSum != null)
            {
                lResitudeSum.Text = residue;
            }

            var lDonePercentSum = tblListExecEngeneers.Controls[tblListExecEngeneers.Controls.Count - 1].Controls[0].FindControl("lDonePercentSum") as Literal;
            var footerChart = tblListExecEngeneers.Controls[tblListExecEngeneers.Controls.Count - 1].Controls[0].FindControl("footerChart") as HtmlContainerControl;

            if (footerChart != null && lDonePercentSum != null && !String.IsNullOrEmpty(done_cnt) && !String.IsNullOrEmpty(plan_cnt))
            {
                int sumDonePercent = Convert.ToInt32((Convert.ToDecimal(done_cnt) / Convert.ToDecimal(plan_cnt)) * 100);
                footerChart.Style.Value = String.Format("width: {0:N0}%", sumDonePercent);
                lDonePercentSum.Text = String.Format("{0}%", sumDonePercent);
                //footerChart.InnerText = String.Format("{0}%", sumDonePercent);
            }
        }

        private void SetCountExec(int count = 0)
        {
            //lRowsCountExec.Text = count.ToString();

            //DataView dv = (DataView)sdsListEngeneers.Select(DataSourceSelectArguments.Empty);
            //lRowsCountExec.Text = dv.Count.ToString();


            //string sumCountIn = String.Format("{0:N2}", dv.Table.Compute("Sum(payment)", ""));
            //if (string.IsNullOrEmpty(sumCountIn)) sumCountIn = 0.ToString();
            //lSummCountExec.Text = sumCountIn;
        }

        protected void rblReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void tblListExecEngeneers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string script = string.Empty;

                int idServiceEngeneer;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "id_service_engeneer").ToString(), out idServiceEngeneer);

                //DateTime? dateMonth = Db.Db.GetValueDateTimeOrNull(MainHelper.TxtGetText(ref txtDateMonth));
                //DateTime? dateBegin = Db.Db.GetValueDateTimeOrNull(MainHelper.TxtGetText(ref txtDateClaimBegin));
                //DateTime? dateEnd = Db.Db.GetValueDateTimeOrNull(MainHelper.TxtGetText(ref txtDateClaimEnd));
                //int? isDone = Db.Db.GetValueIntOrNull(MainHelper.RblGetValueBool(ref rblDone, true));
                //int? noSet = Db.Db.GetValueIntOrNull(MainHelper.RblGetValueBool(ref rblNoSet, true));

                DateTime? dateMonth = Db.Db.GetValueDateTimeOrNull(Request.QueryString["mth"]);
                DateTime? dateBegin = Db.Db.GetValueDateTimeOrNull(Request.QueryString["dst"]);
                DateTime? dateEnd = Db.Db.GetValueDateTimeOrNull(Request.QueryString["den"]);
                int? isDone = Db.Db.GetValueIntOrNull(Request.QueryString["dne"]);
                int? noSet = Db.Db.GetValueIntOrNull(Request.QueryString["nst"]);

                //DateTime? dateMonth =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByEngeneers.SelectParameters["date_month"].DefaultValue);
                //DateTime? dateBegin =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByEngeneers.SelectParameters["date_begin"].DefaultValue);
                //DateTime? dateEnd =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByEngeneers.SelectParameters["date_end"].DefaultValue);
                //int? isDone = Db.Db.GetValueIntOrNull(sdsListExecByEngeneers.SelectParameters["is_done"].DefaultValue);
                //int? noSet = Db.Db.GetValueIntOrNull(sdsListExecByEngeneers.SelectParameters["no_set"].DefaultValue);

                var dtCtrs = Db.Db.Srvpl.GetPlanExecuteContractorList(idServiceEngeneer, dateMonth, isDone, noSet,
                    dateBegin, dateEnd);
                var dtDevices = Db.Db.Srvpl.GetPlanExecuteDeviceList(idServiceEngeneer, dateMonth, isDone, noSet,
                    dateBegin, dateEnd, null, null);

                var phListExecEngeneersContractorsDevices =
                    e.Item.FindControl("phListExecEngeneersContractorsDevices") as PlaceHolder;

                int i = 0;

                foreach (DataRow row in dtCtrs.Rows)
                {
                    var divContractorContainer = new HtmlGenericControl("div");
                    divContractorContainer.Attributes["class"] = "row collapsed row-action bg-white";
                    divContractorContainer.Attributes["data-toggle"] = "collapse";
                    divContractorContainer.Attributes["data-target"] = String.Format("#deviceList{0}Ctr{1}",
                        row["id_service_engeneer"], row["id_contractor"]);

                    var divIn1 = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divIn1);
                    divIn1.Attributes["class"] = "col-sm-3";

                    var divTristate = new HtmlGenericControl("div");
                    divIn1.Controls.Add(divTristate);
                    divTristate.Attributes["class"] = "tristate-container";

                    var divTristateIn1 = new HtmlGenericControl("div");
                    divTristate.Controls.Add(divTristateIn1);
                    divTristateIn1.ID = String.Format("pnlTristate{0}Ctr{1}", row["id_service_engeneer"],
                        row["id_contractor"]);
                    divTristateIn1.Attributes["data-toggle"] = "tooltip";
                    divTristateIn1.Attributes["title"] = "выделить все";

                    var divTristateIn2 = new HtmlInputHidden();
                    divTristateIn1.Controls.Add(divTristateIn2);
                    divTristateIn2.ID = "chkTristate";

                    var spanContractor = new HtmlGenericControl("span");
                    divIn1.Controls.Add(spanContractor);
                    spanContractor.Attributes["class"] = "pad-l-sm";
                    spanContractor.InnerText = row["contractor"].ToString();

                    var spanContractorChecked = new HtmlGenericControl("small");
                    divIn1.Controls.Add(spanContractorChecked);
                    //spanContractorChecked.Attributes["class"] = "small";
                    spanContractorChecked.InnerHtml = "&nbsp;отмечено&nbsp;";

                    var spanContractorCheckedNum = new HtmlGenericControl("span");
                    spanContractorChecked.Controls.Add(spanContractorCheckedNum);
                    spanContractorCheckedNum.ID = String.Format("lChecksCountInner{0}Ctr{1}", row["id_service_engeneer"],
                        row["id_contractor"]);
                    ;
                    spanContractorCheckedNum.Attributes["class"] = "bold text-success";
                    spanContractorCheckedNum.InnerText = "0";


                    var divChart = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divChart);
                    divChart.Attributes["class"] = "col-sm-3";

                    var divChartIn1 = new HtmlGenericControl("div");
                    divChart.Controls.Add(divChartIn1);
                    divChartIn1.Attributes["class"] = "chrt-plan-light";


                    var divChartIn2 = new HtmlGenericControl("div");
                    divChartIn1.Controls.Add(divChartIn2);
                    divChartIn2.Attributes["class"] = "chrt-exec-light";
                    divChartIn2.Attributes["style"] = String.Format("width: {0:N0}%", row["done_percent"]);

                    var divPlanCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divPlanCnt);
                    divPlanCnt.Attributes["class"] = "col-sm-1 align-right";
                    divPlanCnt.InnerText = row["plan_cnt"].ToString();

                    var divDoneCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDoneCnt);
                    divDoneCnt.Attributes["class"] = "col-sm-1 align-right";
                    divDoneCnt.InnerText = row["done_cnt"].ToString();

                    var divResidue = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divResidue);
                    divResidue.Attributes["class"] = "col-sm-1 align-right";
                    divResidue.InnerText = row["residue"].ToString();

                    var divDonePercent = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDonePercent);
                    divDonePercent.Attributes["class"] = "col-sm-1 align-right";
                    divDonePercent.InnerText = String.Format("{0} %", row["done_percent"]);

                    phListExecEngeneersContractorsDevices.Controls.Add(divContractorContainer);

                    var divDevicesContainer = new HtmlGenericControl("div");
                    divDevicesContainer.Attributes["class"] = "collapse";
                    divDevicesContainer.Attributes["id"] = String.Format("deviceList{0}Ctr{1}",
                        row["id_service_engeneer"],
                        row["id_contractor"]);

                    DataRow[] devices = dtDevices.Select(String.Format("id_contractor = {0}", row["id_contractor"]));

                    foreach (DataRow drDevice in devices)
                    {
                        var divDeviceCont = new HtmlGenericControl("div");
                        divDevicesContainer.Controls.Add(divDeviceCont);
                        divDeviceCont.Attributes["class"] = "row";

                        var divDeviceContIn1 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn1);
                        divDeviceContIn1.Attributes["class"] = "col-sm-3";

                        var divDeviceChk = new HtmlInputCheckBox();
                        divDeviceContIn1.Controls.Add(divDeviceChk);
                        divDeviceChk.ID = "chkSetClaim";
                        divDeviceChk.Value = drDevice["id_service_claim"].ToString();

                        var spanDevice = new HtmlGenericControl("span");
                        divDeviceContIn1.Controls.Add(spanDevice);
                        spanDevice.Attributes["class"] = "pad-l-mid";
                        spanDevice.InnerText = drDevice["device"].ToString();

                        var divDeviceContIn2 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn2);
                        divDeviceContIn2.Attributes["class"] = "col-sm-5";
                        divDeviceContIn2.InnerHtml = String.Format("{0}&nbsp;{1}", drDevice["city"], drDevice["address"]);

                        var divDeviceContIn3 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn3);
                        divDeviceContIn3.Attributes["class"] = "col-sm-1";
                        divDeviceContIn3.InnerHtml = String.Format("{0:dd.MM.yyyy}", drDevice["date_came"]);

                        var divDeviceContIn4 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn4);
                        divDeviceContIn4.Attributes["class"] = "col-sm-1";
                        divDeviceContIn4.InnerText = drDevice["done_percent"].ToString();
                    }

                    string idEngeneer = row["id_service_engeneer"].ToString();
                    string idContractor = row["id_contractor"].ToString();

                    string pnlDevicesId = divDevicesContainer.Attributes["id"];
                    string pnlTristateId = String.Format("cphMainContent_cphList_tblListExecEngeneers_{0}_{1}",
                        divTristateIn1.ID, e.Item.ItemIndex);

                    script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                        pnlTristateId, pnlDevicesId);

                    ScriptManager.RegisterStartupScript(this, GetType(),
                        String.Format("tristateCheckbox{0}Eng{1}Ctr{2}", i, idEngeneer, idContractor), script, true);
                    //</Чекбокс>

                    //<Отметки выбанных записей>
                    string spanContractorCheckedNumId =
                        String.Format("cphMainContent_cphList_tblListExecEngeneers_{0}_{1}",
                            spanContractorCheckedNum.ID, e.Item.ItemIndex);

                    script =
                        String.Format(
                            @"var CheckedRows{2}Eng{3}Ctr{4} = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows{2}Eng{3}Ctr{4});CheckedRows{2}Eng{3}Ctr{4}();",
                            pnlDevicesId, spanContractorCheckedNumId, i, idEngeneer, idContractor);

                    ScriptManager.RegisterStartupScript(this, GetType(),
                        String.Format("checkedRows{0}Eng{1}Ctr{2}", i, idEngeneer, idContractor),
                        script, true);

                    phListExecEngeneersContractorsDevices.Controls.Add(divDevicesContainer);

                    i++;
                }

                //<Отметки выбанных записей>
                //script = String.Format(@"$(function() {{initSelectedMemmoryNoTable('{0}', '{2}', '{1}', '{3}');  }});",
                //    tblListExecEngeneersContainer.ClientID, lChecksCount.ClientID, sesDeviceIdsKey, hfCheckedDeviceIds.ClientID);

                //ScriptManager.RegisterStartupScript(this, GetType(), "selMem", script, true);
                //вешаем обработчики
                //btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRowsNoTable();");

                //</вешаем обработчики>
                //</Отметки>

                //////Repeater tblListExecContractors = e.Item.FindControl("tblListExecContractors") as Repeater;
                //////SqlDataSource sdsListExecContractors = e.Item.FindControl("sdsListExecContractors") as SqlDataSource;

                //////if (tblListExecContractors != null && sdsListExecContractors != null)
                //////{
                //////    sdsListExecContractors.SelectParameters["id_service_engeneer"].DefaultValue =
                //////        DataBinder.Eval(e.Item.DataItem, "id_service_engeneer").ToString();


                //////    sdsListExecContractors.SelectParameters["date_month"].DefaultValue =
                //////        sdsListExecByEngeneers.SelectParameters["date_month"].DefaultValue;


                //////    tblListExecContractors.DataSource = sdsListExecContractors;
                //////    tblListExecContractors.DataBind();
                //////}
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                SetExecEngeneersSum();
            }
        }

        //private const string sesDeviceIdsKey = "sesDeviceIdsKeyListExecEngeneers";

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            //var lstIds = new List<int>();

            //foreach (RepeaterItem item in tblListExecEngeneers.Items)
            //{
            //    var tblListExecContractors = item.FindControl("tblListExecContractors") as Repeater;

            //    if (tblListExecContractors != null)
            //    {
            //        foreach (RepeaterItem ctrItem in tblListExecContractors.Items)
            //        {
            //            var tblListExecDevice = ctrItem.FindControl("tblListExecDevice") as Repeater;

            //            if (tblListExecDevice != null)
            //            {
            //                foreach (RepeaterItem devItem in tblListExecDevice.Items)
            //                {
            //                    var chkSetClaim = devItem.FindControl("chkSetClaim") as HtmlInputCheckBox;

            //                    if (chkSetClaim != null && chkSetClaim.Checked)
            //                    {
            //                        lstIds.Add(Convert.ToInt32(chkSetClaim.Value));
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            
            string devIds = hfCheckedDeviceIds.Value;

            if (!String.IsNullOrEmpty(devIds))
            {
                RedirectWithParams(String.Format("ids={0}", devIds), false, FriendlyUrl.Resolve("~/Reports/ServiceAkt"));
            }
        }

        protected void tblListExecContractors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            //////Repeater tblListExecDevice = e.Item.FindControl("tblListExecDevice") as Repeater;
            //////SqlDataSource sdsListExecDevice = e.Item.FindControl("sdsListExecDevice") as SqlDataSource;

            //////if (tblListExecDevice != null && sdsListExecDevice != null)
            //////{
            //////    sdsListExecDevice.SelectParameters["id_service_engeneer"].DefaultValue =
            //////        DataBinder.Eval(e.Item.DataItem, "id_service_engeneer").ToString();
            //////    sdsListExecDevice.SelectParameters["id_contractor"].DefaultValue =
            //////        DataBinder.Eval(e.Item.DataItem, "id_contractor").ToString();

            //////    sdsListExecDevice.SelectParameters["date_month"].DefaultValue =
            //////            sdsListExecByEngeneers.SelectParameters["date_month"].DefaultValue;

            //////    tblListExecDevice.DataSource = sdsListExecDevice;
            //////    tblListExecDevice.DataBind();

            //////    HtmlContainerControl lChecksCountInner =
            //////        (HtmlContainerControl)e.Item.FindControl("lChecksCountInner");

            //////    string script = string.Empty;

            //////    HtmlContainerControl pnlTristate = (HtmlContainerControl)e.Item.FindControl("pnlTristate");

            //////    string idEngeneer = DataBinder.Eval(e.Item.DataItem, "id_service_engeneer").ToString();
            //////    string idContractor = DataBinder.Eval(e.Item.DataItem, "id_contractor").ToString();

            //////    string pnlDevicesId = String.Format("deviceList{0}Ctr{1}",
            //////        idEngeneer, idContractor);

            //////    script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
            //////        pnlTristate.ClientID, pnlDevicesId);

            //////    ScriptManager.RegisterStartupScript(this, GetType(),
            //////        String.Format("tristateCheckbox{0}Eng{1}Ctr{2}", e.Item.ItemIndex, idEngeneer, idContractor), script, true);
            //////    //</Чекбокс>

            //////    //<Отметки выбанных записей>
            //////    //string pnlDevicesId = String.Format("contractorList{0}", ctrItem.ItemIndex);

            //////    script =
            //////        String.Format(
            //////            @"var CheckedRows{2}Eng{3}Ctr{4} = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows{2}Eng{3}Ctr{4});CheckedRows{2}Eng{3}Ctr{4}();",
            //////            pnlDevicesId, lChecksCountInner.ClientID, e.Item.ItemIndex, idEngeneer, idContractor);

            //////    ScriptManager.RegisterStartupScript(this, GetType(),
            //////        String.Format("checkedRows{0}Eng{1}Ctr{2}", e.Item.ItemIndex, idEngeneer, idContractor),
            //////        script, true);
            //////}
        }

        protected void tblListExecServAdmins_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string script = string.Empty;

                int idServiceAdmin;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "id_service_admin").ToString(), out idServiceAdmin);

                DateTime? dateMonth = Db.Db.GetValueDateTimeOrNull(Request.QueryString["mth"]);
                DateTime? dateBegin =Db.Db.GetValueDateTimeOrNull(Request.QueryString["dst"]);
                DateTime? dateEnd =Db.Db.GetValueDateTimeOrNull(Request.QueryString["den"]);
                int? isDone = Db.Db.GetValueIntOrNull(Request.QueryString["dne"]);
                int? noSet = Db.Db.GetValueIntOrNull(Request.QueryString["nst"]);

                //DateTime? dateMonth =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue);

                //DateTime? dateBegin =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByServAdmins.SelectParameters["date_begin"].DefaultValue);
                //DateTime? dateEnd =
                //    Db.Db.GetValueDateTimeOrNull(sdsListExecByServAdmins.SelectParameters["date_end"].DefaultValue);
                //int? isDone = Db.Db.GetValueIntOrNull(sdsListExecByServAdmins.SelectParameters["is_done"].DefaultValue);
                //int? noSet = Db.Db.GetValueIntOrNull(sdsListExecByServAdmins.SelectParameters["no_set"].DefaultValue);

                var dtCtrs = Db.Db.Srvpl.GetPlanExecuteServAdminContractorList(idServiceAdmin, dateMonth, isDone, noSet,
                    dateBegin, dateEnd);
                var dtDevices = Db.Db.Srvpl.GetPlanExecuteDeviceList(null, dateMonth, isDone, noSet,
                    dateBegin, dateEnd, idServiceAdmin, null);

                var phListExecServAdminContractorsDevices =
                    e.Item.FindControl("phListExecServAdminContractorsDevices") as PlaceHolder;

                int i = 0;

                foreach (DataRow row in dtCtrs.Rows)
                {
                    var divContractorContainer = new HtmlGenericControl("div");
                    divContractorContainer.Attributes["class"] = "row collapsed row-action bg-white";
                    divContractorContainer.Attributes["data-toggle"] = "collapse";
                    divContractorContainer.Attributes["data-target"] = String.Format("#deviceList{0}Ctr{1}",
                        row["id_service_admin"], row["id_contractor"]);

                    var divIn1 = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divIn1);
                    divIn1.Attributes["class"] = "col-sm-3";


                    var spanContractor = new HtmlGenericControl("span");
                    divIn1.Controls.Add(spanContractor);
                    spanContractor.Attributes["class"] = "pad-l-sm";
                    spanContractor.InnerText = row["contractor"].ToString();

                    var divChart = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divChart);
                    divChart.Attributes["class"] = "col-sm-3";

                    var divChartIn1 = new HtmlGenericControl("div");
                    divChart.Controls.Add(divChartIn1);
                    divChartIn1.Attributes["class"] = "chrt-plan-light";


                    var divChartIn2 = new HtmlGenericControl("div");
                    divChartIn1.Controls.Add(divChartIn2);
                    divChartIn2.Attributes["class"] = "chrt-exec-light";
                    divChartIn2.Attributes["style"] = String.Format("width: {0:N0}%", row["done_percent"]);

                    var divPlanCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divPlanCnt);
                    divPlanCnt.Attributes["class"] = "col-sm-1 align-right";
                    divPlanCnt.InnerText = row["plan_cnt"].ToString();

                    var divDoneCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDoneCnt);
                    divDoneCnt.Attributes["class"] = "col-sm-1 align-right";
                    divDoneCnt.InnerText = row["done_cnt"].ToString();

                    var divResidue = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divResidue);
                    divResidue.Attributes["class"] = "col-sm-1 align-right";
                    divResidue.InnerText = row["residue"].ToString();

                    var divDonePercent = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDonePercent);
                    divDonePercent.Attributes["class"] = "col-sm-1 align-right";
                    divDonePercent.InnerText = String.Format("{0} %", row["done_percent"]);

                    phListExecServAdminContractorsDevices.Controls.Add(divContractorContainer);

                    var divDevicesContainer = new HtmlGenericControl("div");
                    divDevicesContainer.Attributes["class"] = "collapse";
                    divDevicesContainer.Attributes["id"] = String.Format("deviceList{0}Ctr{1}",
                        row["id_service_admin"],
                        row["id_contractor"]);

                    DataRow[] devices = dtDevices.Select(String.Format("id_contractor = {0}", row["id_contractor"]));

                    foreach (DataRow drDevice in devices)
                    {
                        var divDeviceCont = new HtmlGenericControl("div");
                        divDevicesContainer.Controls.Add(divDeviceCont);
                        divDeviceCont.Attributes["class"] = "row";

                        var divDeviceContIn1 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn1);
                        divDeviceContIn1.Attributes["class"] = "col-sm-3";

                        var divDeviceChk = new HtmlInputCheckBox();
                        divDeviceContIn1.Controls.Add(divDeviceChk);
                        divDeviceChk.ID = "chkSetClaim";
                        divDeviceChk.Value = drDevice["id_service_claim"].ToString();

                        var spanDevice = new HtmlGenericControl("span");
                        divDeviceContIn1.Controls.Add(spanDevice);
                        spanDevice.Attributes["class"] = "pad-l-mid";
                        spanDevice.InnerText = drDevice["is_limit_device_claims"].ToString().Equals("1") ? "неизвестно" : drDevice["device"].ToString();

                        var divDeviceContIn2 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn2);
                        divDeviceContIn2.Attributes["class"] = "col-sm-4";
                        divDeviceContIn2.InnerHtml = String.Format("{0}&nbsp;{1}", drDevice["city"], drDevice["address"]);

                        var divDeviceContInEng = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContInEng);
                        divDeviceContInEng.Attributes["class"] = "col-sm-1";
                        divDeviceContInEng.InnerHtml = drDevice["service_engeneer"].ToString();

                        var divDeviceContIn3 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn3);
                        divDeviceContIn3.Attributes["class"] = "col-sm-1";
                        divDeviceContIn3.InnerHtml = String.Format("{0:dd.MM.yyyy}", drDevice["date_came"]);

                        var divDeviceContIn4 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn4);
                        divDeviceContIn4.Attributes["class"] = "col-sm-1";
                        divDeviceContIn4.InnerText = drDevice["done_percent"].ToString();
                    }

                    phListExecServAdminContractorsDevices.Controls.Add(divDevicesContainer);

                    i++;
                }

                
            }

            //Repeater tblListExecServAdminContractors = e.Item.FindControl("tblListExecServAdminContractors") as Repeater;
            //SqlDataSource sdsListExecServAdminContractors = e.Item.FindControl("sdsListExecServAdminContractors") as SqlDataSource;

            //if (tblListExecServAdminContractors != null && sdsListExecServAdminContractors != null)
            //{
            //    sdsListExecServAdminContractors.SelectParameters["id_service_admin"].DefaultValue =
            //        DataBinder.Eval(e.Item.DataItem, "id_service_admin").ToString();

            //    sdsListExecServAdminContractors.SelectParameters["date_month"].DefaultValue =
            //            sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue;

            //    tblListExecServAdminContractors.DataSource = sdsListExecServAdminContractors;
            //    tblListExecServAdminContractors.DataBind();
            //}

            if (e.Item.ItemType == ListItemType.Footer)
            {
                SetExecServAdminsSum();
            }
        }

        protected void tblListExecServAdminContractors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            

            //Repeater tblListExecDevice = e.Item.FindControl("tblListExecDevice") as Repeater;
            //SqlDataSource sdsListExecDevice = e.Item.FindControl("sdsListExecDevice") as SqlDataSource;

            //if (tblListExecDevice != null && sdsListExecDevice != null)
            //{
            //    sdsListExecDevice.SelectParameters["id_service_admin"].DefaultValue =
            //        DataBinder.Eval(e.Item.DataItem, "id_service_admin").ToString();
            //    sdsListExecDevice.SelectParameters["id_contractor"].DefaultValue =
            //        DataBinder.Eval(e.Item.DataItem, "id_contractor").ToString();

            //    sdsListExecDevice.SelectParameters["date_month"].DefaultValue =
            //            sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue;

            //    tblListExecDevice.DataSource = sdsListExecDevice;
            //    tblListExecDevice.DataBind();
            //}
        }

        protected void SetExecServAdminsSum()
        {
            //SetCountExec();

            DataView dv = (DataView)sdsListExecByServAdmins.Select(DataSourceSelectArguments.Empty);

            string plan_cnt = dv.Table.Compute("Sum(plan_cnt)", "").ToString();
            string done_cnt = dv.Table.Compute("Sum(done_cnt)", "").ToString();
            string residue = dv.Table.Compute("Sum(residue)", "").ToString();

            var lPlanSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lPlanSum") as Literal;
            var lDoneSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lDoneSum") as Literal;
            var lResitudeSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lResitudeSum") as Literal;

            if (lPlanSum != null)
            {
                lPlanSum.Text = plan_cnt;
            }

            if (lDoneSum != null)
            {
                lDoneSum.Text = done_cnt;
            }

            if (lResitudeSum != null)
            {
                lResitudeSum.Text = residue;
            }

            var lDonePercentSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lDonePercentSum") as Literal;
            var footerChart = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("footerChart") as HtmlContainerControl;

            if (footerChart != null && lDonePercentSum != null && !String.IsNullOrEmpty(done_cnt) && !String.IsNullOrEmpty(plan_cnt))
            {
                int sumDonePercent = Convert.ToInt32((Convert.ToDecimal(done_cnt) / Convert.ToDecimal(plan_cnt)) * 100);
                footerChart.Style.Value = String.Format("width: {0:N0}%", sumDonePercent);
                lDonePercentSum.Text = String.Format("{0}%", sumDonePercent);
                //footerChart.InnerText = String.Format("{0}%", sumDonePercent);
            }
        }

        protected void tblListExecServManagers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string script = string.Empty;

                int idServiceManager;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "id_manager").ToString(), out idServiceManager);

                DateTime? dateMonth = Db.Db.GetValueDateTimeOrNull(
                    Request.QueryString["mth"]
                    //sdsListExecByServManagers.SelectParameters["date_month"].DefaultValue
                    );

                DateTime? dateBegin =
                    Db.Db.GetValueDateTimeOrNull(Request.QueryString["dst"]
                    //sdsListExecByServManagers.SelectParameters["date_begin"].DefaultValue
                    );
                DateTime? dateEnd =
                    Db.Db.GetValueDateTimeOrNull(Request.QueryString["den"]
                    //sdsListExecByServManagers.SelectParameters["date_end"].DefaultValue
                    );
                int? isDone = Db.Db.GetValueIntOrNull(Request.QueryString["dne"]
                    //sdsListExecByServManagers.SelectParameters["is_done"].DefaultValue
                    );
                int? noSet = Db.Db.GetValueIntOrNull(Request.QueryString["nst"]
                    //sdsListExecByServManagers.SelectParameters["no_set"].DefaultValue
                    );

                var dtCtrs = Db.Db.Srvpl.GetPlanExecuteServManagerContractorList(idServiceManager, dateMonth, isDone, noSet,
                    dateBegin, dateEnd);
                var dtDevices = Db.Db.Srvpl.GetPlanExecuteDeviceList(null, dateMonth, isDone, noSet,
                    dateBegin, dateEnd, null, idServiceManager);

                var phListExecServManagersContractorsDevices =
                    e.Item.FindControl("phListExecServManagersContractorsDevices") as PlaceHolder;

                int i = 0;

                foreach (DataRow row in dtCtrs.Rows)
                {
                    var divContractorContainer = new HtmlGenericControl("div");
                    divContractorContainer.Attributes["class"] = "row collapsed row-action bg-white";
                    divContractorContainer.Attributes["data-toggle"] = "collapse";
                    divContractorContainer.Attributes["data-target"] = String.Format("#deviceList{0}Ctr{1}",
                        row["id_manager"], row["id_contractor"]);

                    var divIn1 = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divIn1);
                    divIn1.Attributes["class"] = "col-sm-3";


                    var spanContractor = new HtmlGenericControl("span");
                    divIn1.Controls.Add(spanContractor);
                    spanContractor.Attributes["class"] = "pad-l-sm";
                    spanContractor.InnerText = row["contractor"].ToString();

                    var divChart = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divChart);
                    divChart.Attributes["class"] = "col-sm-3";

                    var divChartIn1 = new HtmlGenericControl("div");
                    divChart.Controls.Add(divChartIn1);
                    divChartIn1.Attributes["class"] = "chrt-plan-light";


                    var divChartIn2 = new HtmlGenericControl("div");
                    divChartIn1.Controls.Add(divChartIn2);
                    divChartIn2.Attributes["class"] = "chrt-exec-light";
                    divChartIn2.Attributes["style"] = String.Format("width: {0:N0}%", row["done_percent"]);

                    var divPlanCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divPlanCnt);
                    divPlanCnt.Attributes["class"] = "col-sm-1 align-right";
                    divPlanCnt.InnerText = row["plan_cnt"].ToString();

                    var divDoneCnt = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDoneCnt);
                    divDoneCnt.Attributes["class"] = "col-sm-1 align-right";
                    divDoneCnt.InnerText = row["done_cnt"].ToString();

                    var divResidue = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divResidue);
                    divResidue.Attributes["class"] = "col-sm-1 align-right";
                    divResidue.InnerText = row["residue"].ToString();

                    var divDonePercent = new HtmlGenericControl("div");
                    divContractorContainer.Controls.Add(divDonePercent);
                    divDonePercent.Attributes["class"] = "col-sm-1 align-right";
                    divDonePercent.InnerText = String.Format("{0} %", row["done_percent"]);

                    phListExecServManagersContractorsDevices.Controls.Add(divContractorContainer);

                    var divDevicesContainer = new HtmlGenericControl("div");
                    divDevicesContainer.Attributes["class"] = "collapse";
                    divDevicesContainer.Attributes["id"] = String.Format("deviceList{0}Ctr{1}",
                        row["id_manager"],
                        row["id_contractor"]);

                    DataRow[] devices = dtDevices.Select(String.Format("id_contractor = {0}", row["id_contractor"]));

                    foreach (DataRow drDevice in devices)
                    {
                        var divDeviceCont = new HtmlGenericControl("div");
                        divDevicesContainer.Controls.Add(divDeviceCont);
                        divDeviceCont.Attributes["class"] = "row";

                        var divDeviceContIn1 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn1);
                        divDeviceContIn1.Attributes["class"] = "col-sm-3";

                        var divDeviceChk = new HtmlInputCheckBox();
                        divDeviceContIn1.Controls.Add(divDeviceChk);
                        divDeviceChk.ID = "chkSetClaim";
                        divDeviceChk.Value = drDevice["id_service_claim"].ToString();

                        var spanDevice = new HtmlGenericControl("span");
                        divDeviceContIn1.Controls.Add(spanDevice);
                        spanDevice.Attributes["class"] = "pad-l-mid";
                        spanDevice.InnerText = drDevice["device"].ToString();

                        var divDeviceContIn2 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn2);
                        divDeviceContIn2.Attributes["class"] = "col-sm-5";
                        divDeviceContIn2.InnerHtml = String.Format("{0}&nbsp;{1}", drDevice["city"], drDevice["address"]);

                        var divDeviceContInEng = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContInEng);
                        divDeviceContInEng.Attributes["class"] = "col-sm-1";
                        divDeviceContInEng.InnerHtml = drDevice["service_engeneer"].ToString();

                        var divDeviceContIn3 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn3);
                        divDeviceContIn3.Attributes["class"] = "col-sm-1";
                        divDeviceContIn3.InnerHtml = String.Format("{0:dd.MM.yyyy}", drDevice["date_came"]);

                        var divDeviceContIn4 = new HtmlGenericControl("div");
                        divDeviceCont.Controls.Add(divDeviceContIn4);
                        divDeviceContIn4.Attributes["class"] = "col-sm-1";
                        divDeviceContIn4.InnerText = drDevice["done_percent"].ToString();
                    }

                    phListExecServManagersContractorsDevices.Controls.Add(divDevicesContainer);

                    i++;
                }
            }

            //Repeater tblListExecServManagerContractors = e.Item.FindControl("tblListExecManagersContractors") as Repeater;
            //SqlDataSource sdsListExecServManagerContractors = e.Item.FindControl("sdsListExecManagersContractors") as SqlDataSource;

            //if (tblListExecServManagerContractors != null && sdsListExecServManagerContractors != null)
            //{
            //    sdsListExecServManagerContractors.SelectParameters["id_manager"].DefaultValue =
            //        DataBinder.Eval(e.Item.DataItem, "id_manager").ToString();

            //    sdsListExecServManagerContractors.SelectParameters["date_month"].DefaultValue =
            //            sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue;

            //    tblListExecServManagerContractors.DataSource = sdsListExecServManagerContractors;
            //    tblListExecServManagerContractors.DataBind();
            //}

            if (e.Item.ItemType == ListItemType.Footer)
            {
                SetExecServManagersSum();
            }
        }

        protected void tblListExecManagersContractors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater tblListExecDevice = e.Item.FindControl("tblListExecDevice") as Repeater;
            SqlDataSource sdsListExecDevice = e.Item.FindControl("sdsListExecDevice") as SqlDataSource;

            if (tblListExecDevice != null && sdsListExecDevice != null)
            {
                sdsListExecDevice.SelectParameters["id_manager"].DefaultValue =
                    DataBinder.Eval(e.Item.DataItem, "id_manager").ToString();
                sdsListExecDevice.SelectParameters["id_contractor"].DefaultValue =
                    DataBinder.Eval(e.Item.DataItem, "id_contractor").ToString();

                sdsListExecDevice.SelectParameters["date_month"].DefaultValue =
                        sdsListExecByServAdmins.SelectParameters["date_month"].DefaultValue;

                tblListExecDevice.DataSource = sdsListExecDevice;
                tblListExecDevice.DataBind();
            }
        }

        protected void SetExecServManagersSum()
        {
            //SetCountExec();

            DataView dv = (DataView)sdsListExecByServManagers.Select(DataSourceSelectArguments.Empty);

            string plan_cnt = dv.Table.Compute("Sum(plan_cnt)", "").ToString();
            string done_cnt = dv.Table.Compute("Sum(done_cnt)", "").ToString();
            string residue = dv.Table.Compute("Sum(residue)", "").ToString();

            var lPlanSum = tblListExecServManagers.Controls[tblListExecServManagers.Controls.Count - 1].Controls[0].FindControl("lPlanSum") as Literal;
            var lDoneSum = tblListExecServManagers.Controls[tblListExecServManagers.Controls.Count - 1].Controls[0].FindControl("lDoneSum") as Literal;
            var lResitudeSum = tblListExecServManagers.Controls[tblListExecServManagers.Controls.Count - 1].Controls[0].FindControl("lResitudeSum") as Literal;

            if (lPlanSum != null)
            {
                lPlanSum.Text = plan_cnt;
            }

            if (lDoneSum != null)
            {
                lDoneSum.Text = done_cnt;
            }

            if (lResitudeSum != null)
            {
                lResitudeSum.Text = residue;
            }

            var lDonePercentSum = tblListExecServManagers.Controls[tblListExecServManagers.Controls.Count - 1].Controls[0].FindControl("lDonePercentSum") as Literal;
            var footerChart = tblListExecServManagers.Controls[tblListExecServManagers.Controls.Count - 1].Controls[0].FindControl("footerChart") as HtmlContainerControl;

            if ((footerChart != null && lDonePercentSum != null))
            {
                done_cnt = String.IsNullOrEmpty(done_cnt) ? "0" : done_cnt;
                plan_cnt = String.IsNullOrEmpty(plan_cnt) ? "0" : plan_cnt;

                if (!plan_cnt.Equals("0"))
                {
                    int sumDonePercent = Convert.ToInt32((Convert.ToDecimal(done_cnt)/Convert.ToDecimal(plan_cnt))*100);
                    footerChart.Style.Value = String.Format("width: {0:N0}%", sumDonePercent);
                    lDonePercentSum.Text = String.Format("{0}%", sumDonePercent);
                    //footerChart.InnerText = String.Format("{0}%", sumDonePercent);
                }
            }
        }


        protected void Timer1_OnTick(object sender, EventArgs e)
        {
            string rep = "execServAdmins";

            if (rblReportType.SelectedValue == rep) rep = "execEng";

            RedirectWithParams(String.Format("rep={0}", rep));
        }

        

        protected void tblEngeneersAlloc_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var phDaysAlloc = e.Item.FindControl("phDaysAlloc") as PlaceHolder;

                if (phDaysAlloc != null)
                {
                    DateTime dateMonth;
                    DateTime.TryParse(Request.QueryString["mth"], out dateMonth);
                    bool flag = dateMonth != new DateTime();

                    var hfIdServiceEngeneer =
                         (e.Item.FindControl("hfIdServiceEngeneer") as HiddenField);

                    int alreadyExec;
                    int.TryParse((e.Item.FindControl("hfAlreadyExec") as HiddenField).Value, out alreadyExec);

                    int claimsCount;
                    int.TryParse((e.Item.FindControl("hfClaimsCount") as HiddenField).Value, out claimsCount);

                    DateTime lastExecDay;
                    DateTime.TryParse((e.Item.FindControl("hfLastExecDay") as HiddenField).Value, out lastExecDay);

                    int idServiceEngeneer;
                    int.TryParse(hfIdServiceEngeneer.Value, out idServiceEngeneer);

                    int daysCount = DateTime.DaysInMonth(dateMonth.Year, dateMonth.Month);
                    var dt = Db.Db.Srvpl.GetEngeneerDayAlloc(idServiceEngeneer, dateMonth);

                    for (int i = 1; i <= daysCount; i++)
                    {
                        HtmlTableCell tdDayAlloc = new HtmlTableCell();
                        DateTime day = new DateTime(dateMonth.Year, dateMonth.Month, i);

                        if (flag)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string query = String.Format("id_service_engeneer = {0} and date_came = '{1:dd/MM/yyyy}'", idServiceEngeneer, day);
                                DataRow[] drs = dt.Select(query);

                                if (drs.Any())
                                {
                                    tdDayAlloc.InnerText = drs[0]["exec_claims"].ToString();
                                }
                            }

                            if (day > DateTime.Now)
                            {
                                tdDayAlloc.Attributes["class"] = "bg-warning text-danger text-center text-middle";

                                int emptyDaysCnt = DateTime.DaysInMonth(day.Year, day.Month) - lastExecDay.Day;

                                decimal mustExecPerDay = (Convert.ToDecimal(claimsCount) - Convert.ToDecimal(alreadyExec)) / Convert.ToDecimal(emptyDaysCnt);

                                if (mustExecPerDay > 0 && day.Day == DateTime.DaysInMonth(day.Year, day.Month))
                                {
                                    tdDayAlloc.InnerText = String.Format("{0:N2}", mustExecPerDay);
                                    //tdDayAlloc.Attributes["class"] = "bg-danger";
                                }
                            }

                        }


                        phDaysAlloc.Controls.Add(tdDayAlloc);
                    }


                }
            }
        }


        protected void rtrDaysInMonth_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var tdDayAlloc = e.Item.FindControl("tdDayAlloc") as HtmlTableCell;

                if (tdDayAlloc != null)
                {
                    //int idServiceEngeneer = Convert.ToInt32(DataBinder.Eval((e.Item.Parent.Parent as RepeaterItem).DataItem, "id_service_engeneer"));

                    var hfIdServiceEngeneer =
                        ((e.Item.Parent.Parent as RepeaterItem).FindControl("hfIdServiceEngeneer") as HiddenField);
                    DateTime day = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "day"));
                    int alreadyExec;
                    int.TryParse(((e.Item.Parent.Parent as RepeaterItem).FindControl("hfAlreadyExec") as HiddenField).Value, out alreadyExec);

                    int claimsCount;
                    int.TryParse(((e.Item.Parent.Parent as RepeaterItem).FindControl("hfClaimsCount") as HiddenField).Value, out claimsCount);

                    DateTime lastExecDay;
                    DateTime.TryParse(((e.Item.Parent.Parent as RepeaterItem).FindControl("hfLastExecDay") as HiddenField).Value, out lastExecDay);

                    if (hfIdServiceEngeneer != null)
                    {
                        int idServiceEngeneer;
                        int.TryParse(hfIdServiceEngeneer.Value, out idServiceEngeneer);

                        //var dt = Db.Db.Srvpl.GetEngeneerDayAlloc(idServiceEngeneer, day, alreadyExec, claimsCount);

                        //if (dt.Rows.Count > 0)
                        //{
                        //    tdDayAlloc.InnerText = dt.Rows[0]["exec_claims"].ToString();
                        //}
                    }

                    if (day > DateTime.Now)
                    {
                        tdDayAlloc.Attributes["class"] = "bg-warning text-danger text-center text-middle";

                        int emptyDaysCnt = DateTime.DaysInMonth(day.Year, day.Month) - lastExecDay.Day;

                        decimal mustExecPerDay = (Convert.ToDecimal(claimsCount) - Convert.ToDecimal(alreadyExec)) / Convert.ToDecimal(emptyDaysCnt);

                        if (mustExecPerDay > 0 && day.Day == DateTime.DaysInMonth(day.Year, day.Month))
                        {
                            tdDayAlloc.InnerText = String.Format("{0:N2}", mustExecPerDay);
                            //tdDayAlloc.Attributes["class"] = "bg-danger";
                        }
                    }
                }
            }
        }

        protected string SetMarkExecDiff(string strExecDiff)
        {
            string result = string.Empty;

            if (strExecDiff.Contains("-"))
            {
                result = "bg-danger";
            }
            else
            {
                result = "bg-success";
            }

            //int execDiff;
            //int.TryParse(strExecDiff, out execDiff);

            //if (execDiff >= 0)
            //{
            //    result = "bg-success";
            //}
            //else
            //{
            //    result = "bg-danger";
            //}

            return result;
        }

        protected string SetRowMark(string strNormExecPercent)
        {
            string result = String.Empty;

            decimal normExecPercent;
            decimal.TryParse(strNormExecPercent, out normExecPercent);

            if (normExecPercent >= 100)
            {
                result = "bg-success";
            }
            else if (normExecPercent < 100 && normExecPercent >= 75)
            {
                result = "bg-success-warning";
            }
            else if (normExecPercent < 75 && normExecPercent >= 50)
            {
                result = "bg-warning";
            }
            else
            {
                result = "bg-danger";
            }

            return result;
        }
    }
}