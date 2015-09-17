using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class List : BaseFilteredPage
    {
        /*queryStringFilterParams
         number - № договора
         price - Сумма по договору
         ctype - Тип договора
         stype - Тип ТО
         ctrtr - Контрагент
         state - Статус договора
         mngr - Менеджер договора
         dst - Дата начала
         den - дата окончания
         nodvl - нет привязанных устройств
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("number", txtNumber));
            FilterLinks.Add(new FilterLink("price", txtPrice));
            FilterLinks.Add(new FilterLink("ctype", ddlContractTypes));
            //FilterLinks.Add(new FilterLink("stype", ddlServceTypes));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("state", ddlContractStatus));
            FilterLinks.Add(new FilterLink("mngr", ddlManager));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("inn", txtContractorInn));
            FilterLinks.Add(new FilterLink("nodvl", rblNoLinkedDevice));
            FilterLinks.Add(new FilterLink("sernum", txtFilterSerialNum));
            FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "10"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormUrl = FriendlyUrl.Resolve("~/Contracts/Editor");
        protected string ServiceClaimUrl = FriendlyUrl.Resolve("~/Service/Editor");

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FillFilterLists();
            }

            base.Page_Load(sender, e);

            RegisterStartupScripts();
        }

        //protected void Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        FillFilterLists();
        //    }
        //}

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlContractTypes, Db.Db.Srvpl.GetContractTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.DdlFill(ref ddlServceTypes, Db.Db.Srvpl.GetServiceTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Srvpl.GetContractorFilterSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlContractStatus, Db.Db.Srvpl.GetContractStatusSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Srvpl.GetManagerFilterSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
        }

        //protected string GetFormUrl(string id = null)
        //{
        //    string url = FormUrl;

        //    if (id != null)
        //    { url = MainHelper.UrlQueryStringParamsReplace(FormUrl, String.Format("id={0}", id)); }

        //    return url;
        //}

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new Contract().Delete(id);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        //protected void rtrContractList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        SqlDataSource ds = (SqlDataSource)e.Item.FindControl("sdsContract2DevicesList");
        //        ds.SelectParameters["id_contract"].DefaultValue =
        //            DataBinder.Eval(e.Item.DataItem, "id_contract").ToString();
        //        Repeater rtr = (Repeater)e.Item.FindControl("rtrContract2DevicesList");
        //        rtr.DataSourceID = ds.ID;
        //    }
        //}

        protected void tblList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SqlDataSource ds = (SqlDataSource)e.Row.FindControl("sdsContract2DevicesList");
                ds.SelectParameters["id_contract"].DefaultValue =
                    DataBinder.Eval(e.Row.DataItem, "id_contract").ToString();
                Repeater rtr = (Repeater)e.Row.FindControl("rtrContract2DevicesList");
                rtr.DataSourceID = ds.ID;
            }
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }

        //protected void btnDelete_OnClick(object sender, EventArgs e)
        //{
        //    int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
        //    new Contract().Delete(id);
        //    Response.Redirect(Request.Url.ToString());
        //}

        //protected void rtrContract2DevicesList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Repeater rtr = (sender as Repeater);

        //    if (!rtr.Visible)rtr.Visible = true;
        //}
        //protected void btnSearch_OnClick(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorInn.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>
        }

        protected string SetRowMark(string statusSysname, string markDanger)
        {
            string result = string.Empty;

            switch (statusSysname.ToUpper())
            {
                case "0":
                    result = "bg-warning";
                    break;
                case "1":
                    result = "bg-success";
                    break;
            }

            if (markDanger.Equals("1"))
            {
                result = "bg-danger";
            }

            return result;
        }
    }
}