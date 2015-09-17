using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class List : BaseFilteredPage
    {
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        protected bool UserIsServiceAdmin { get { return (Page.Master.Master as Site).UserIsServiceAdmin; } }
        protected bool UserIsSysAdmin {get{return (Page.Master.Master as Site).UserIsSysAdmin;}}
        private const string sesKeyIsEdit = "sesKeyIsEdit";

        private bool isSearch
        {
            get { return Session[sesKeyIsEdit] != null && (bool)Session[sesKeyIsEdit] == true; }
            set
            {
                if (Session[sesKeyIsEdit] != null)
                {
                    Session[sesKeyIsEdit] = value;
                }
                else
                {
                    Session.Add(sesKeyIsEdit, value);
                }
            }
        }

        /*queryStringFilterParams
         clnum - номер заявки
         ctnum - № договора
         dev - устройство
         ctr - Контрагент
         sttpe - Тип обслуживания
         satpe - Тип ТО
         eng - Инженер
         cre - Кем создано 
         dclst - Заявка - дата начала 
         dclen - Заявка - дата окончания    
         dcst - Приход на заявку - дата начала 
         dcen - Приход на заявку - дата окончания  
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("clnum", txtClaimNum));
            FilterLinks.Add(new FilterLink("ctnum", txtContractNum));
            FilterLinks.Add(new FilterLink("dev", ddlDevice));
            FilterLinks.Add(new FilterLink("ctr", ddlContractor));
            FilterLinks.Add(new FilterLink("sttpe", ddlServiceTypes));
            FilterLinks.Add(new FilterLink("satpe", ddlServiceActionTypes));
            FilterLinks.Add(new FilterLink("eng", ddlServiceEngeneer));
            FilterLinks.Add(new FilterLink("cre", ddlCreator));
            FilterLinks.Add(new FilterLink("dclst", txtDateClaimBegin));
            FilterLinks.Add(new FilterLink("dclen", txtDateClaimEnd));
            FilterLinks.Add(new FilterLink("dcst", txtDateCameBegin));
            FilterLinks.Add(new FilterLink("dcen", txtDateCameEnd));
            FilterLinks.Add(new FilterLink("ste", ddlServiceClaimStatus));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin));
            FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "30"));
            FilterLinks.Add(new FilterLink("mth", txtDateMonthPlan, DateTime.Now.ToString("MM.yyyy")));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormUrl = FriendlyUrl.Resolve("~/Service/Editor");
        protected string FormCameUrl = FriendlyUrl.Resolve("~/Service/Editor/Came");
        protected string FormCameScanUrl = FriendlyUrl.Resolve("~/Service/Editor/CameScan");
        protected string PlanUrl = FriendlyUrl.Resolve("~/Service/Plan");
        protected string EngeneerSetUrl = FriendlyUrl.Resolve("~/Service/EngeneerSet");

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);

            if (isSearch)
            {
                sdsList.SelectParameters["action"].DefaultValue = "getServiceClaimList";
                isSearch = false;
            }

            RegisterStartupScripts();
        }

        //protected void Page_PreLoad(object sender, EventArgs e)
        //{
        //if (!IsPostBack)
        //{
        //    FillFilterLists();
        //}
        //}

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlDevice, Db.Db.Srvpl.GetDeviceSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Srvpl.GetContractorShortSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceTypes, Db.Db.Srvpl.GetServiceTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceActionTypes, Db.Db.Srvpl.GetServiceActionTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlCreator, Db.Db.Users.GetUsersSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceClaimStatus, Db.Db.Srvpl.GetServiceClaimStatusSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.RblFill(ref rblServiceClaimStatus, Db.Db.Srvpl.GetServiceClaimStatusSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new ServiceClaim().Delete(id, User.Id);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            isSearch = true;
            Search();
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        private void RegisterStartupScripts()
        {
            string script = string.Empty;

            //<Фильтрация списка по вводимому тексту>
            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlDevice.ClientID, txtDeviceSelection.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListBySerialNum", script, true);

            //script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListBySerialNum", script, true);
            //</Фильтрация списка>
        }

        //protected void tblList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        SqlDataSource ds = (SqlDataSource)e.Row.FindControl("sdsContract2DevicesList");
        //        ds.SelectParameters["id_contract"].DefaultValue =
        //            DataBinder.Eval(e.Row.DataItem, "id_contract").ToString();
        //        Repeater rtr = (Repeater)e.Row.FindControl("rtrContract2DevicesList");
        //        rtr.DataSourceID = ds.ID;
        //    }
        //}

        protected string SetRowMark(string statusSysname)
        {
            string result = string.Empty;

            switch (statusSysname.ToUpper())
            {
                case "NEW":
                    result = "bg-warning";
                    break;
                case "DONE":
                    result = "bg-success";
                    break;
            }

            return result;
        }

        protected bool DisableIfDone(string statusSysname)
        {
            bool result = false;

            switch (statusSysname.ToUpper())
            {
                case "DONE":
                    result = true;
                    break;
            }

            return result;
        }

        protected string DisableIfDoneStr(string statusSysname)
        {
            //string result = string.Empty;

            //switch (statusSysname.ToUpper())
            //{
            //    case "DONE":
            //        result = "disabled";
            //        break;
            //}

            //return result;

            string result = DisableIfDone(statusSysname) && (!UserIsSysAdmin && !UserIsServiceAdmin) ? "disabled" : string.Empty;
            return result;
        }

        protected void txtContractorSelection_OnTextChanged(object sender, EventArgs e)
        {
            string text = MainHelper.TxtGetText(ref txtContractorSelection);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text));
            ddlContractor.Focus();
        }
    }
}