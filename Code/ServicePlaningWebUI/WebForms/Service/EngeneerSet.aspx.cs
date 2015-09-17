using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class EngeneerSet : BaseFilteredPage
    {
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        private const string sesClearCheckedRows = "sesClearCheckedRows";

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("ctr", ddlContractor));
            FilterLinks.Add(new FilterLink("cit", ddlCity));
            FilterLinks.Add(new FilterLink("addr", ddlAddress));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin));
            FilterLinks.Add(new FilterLink("mth", txtDateMonth, DateTime.Now.ToString("MM.yyyy")));
            //FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "30"));
            FilterLinks.Add(new FilterLink("nst", rblNoSet, "-13"));
            FilterLinks.Add(new FilterLink("dne", rblDone, "-13"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
                FillLists();
                ClearCheckedRows();
            }

            base.Page_Load(sender, e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaults();
            }

            RegisterStartupScripts();
        }

        private void SetDefaults()
        {
            sdsContractorList.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");

            MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, User.Id);
        }

        private void FillLists()
        {
            //Заполняем список групп (Организаций) инженеров
            var lstEngeneerGroups = AdHelper.GetGroupListFromAdUnit("OU=engeneer-groups,OU=Service,OU=System-Groups,OU=UNIT");
            lstEngeneerGroups = lstEngeneerGroups.OrderBy(n => n.Name).ToList();
            MainHelper.DdlFill(ref ddlEngeneerGroup, lstEngeneerGroups, true, MainHelper.ListFirstItemType.SelectAll);

            string defVal = "S-1-5-21-1970802976-3466419101-4042325969-4014";

            ddlEngeneerGroup.SelectedValue = defVal;
            FillEngeneersList(defVal);
        }

        protected void ddlEngeneerGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string val = ddlEngeneerGroup.SelectedValue;
            if (val.Equals("-13")) val = null;
            FillEngeneersList(val);
        }

        protected void FillEngeneersList(string userGroupSid=null)
        {
            if (String.IsNullOrEmpty(userGroupSid))
            {
                MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup),
                    true);
            }
            else
            {
                MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(null, groupSid: userGroupSid),
                    true);
            }
        }

        private string GetQueryStringValue(string key)
        {
            return Request.QueryString[key] ?? String.Empty;
        }

        //private string AddressFilter
        //{
        //    get { return GetQueryStringValue("addr"); }
        //}
        ////ddlAddress.SelectedItem == null ? null : ddlAddress.SelectedItem.Text;
        //private int IdCityFilter
        //{
        //    get
        //    {
        //        return String.IsNullOrEmpty(GetQueryStringValue("cit"))
        //            ? -1
        //            : Convert.ToInt32(
        //                GetQueryStringValue("cit"));
        //    }
        //} //MainHelper.DdlGetSelectedValueInt(ref ddlCity);

        //private int IdContractorFilter {
        //    get
        //    {
        //        return String.IsNullOrEmpty(GetQueryStringValue("ctr"))
        //            ? -1
        //            : Convert.ToInt32(
        //                GetQueryStringValue("ctr"));
        //    }
        //} //MainHelper.DdlGetSelectedValueInt(ref ddlContractor);

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);

            string address = GetQueryStringValue("addr");
            int idCity = String.IsNullOrEmpty(GetQueryStringValue("cit")) ? -1 : Convert.ToInt32(GetQueryStringValue("cit"));
            int idContractor = String.IsNullOrEmpty(GetQueryStringValue("ctr")) ? -1 : Convert.ToInt32(GetQueryStringValue("ctr"));

            string contractorFilter = MainHelper.TxtGetText(ref txtContractorSelection);
            string addressFilter = MainHelper.TxtGetText(ref txtAddressFilter);
            string cityFilter = MainHelper.TxtGetText(ref txtCityFilter);

            MainHelper.DdlFill(ref ddlCity, Db.Db.Srvpl.GetContract2DevicesCitiesSelectionList(cityFilter, idContractor, address), String.IsNullOrEmpty(cityFilter), MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlAddress, Db.Db.Srvpl.GetContract2DevicesAddressSelectionList(addressFilter, idContractor, idCity), String.IsNullOrEmpty(addressFilter), MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Srvpl.GetContractorShortSelectionList(contractorFilter, -1, true, false, idCity, address), String.IsNullOrEmpty(contractorFilter), MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        private void SetRowsCount(int count = 0)
        {
            //lRowsCount.Text = count.ToString();
        }

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        protected void sdsDeviceList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            //int count = e.AffectedRows;

        }

        protected const string sesDeviceIdsKey = "ContractorsDevicesIds";

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            //<Фильтрация списка по вводимому тексту> Переделана на фильтр в SQL
            //script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlCity.ClientID, txtCityFilter.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filteCity", script, true);

            //script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filteContactor", script, true);
            //</Фильтрация списка>


            //<Отметки выбанных записей>
            script = String.Format(@"$(function() {{initSelectedMemmoryNoTable('{0}', '{2}', '{1}', '{3}');  }});",
                pnlContractorsDevices.ClientID, lChecksCount.ClientID, sesDeviceIdsKey, hfCheckedContractorsDevices.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "selMem", script, true);
            //вешаем обработчики
            btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRowsNoTable();");

            //</вешаем обработчики>
            //</Отметки>

            foreach (RepeaterItem item in tblContractorList.Items)
            {
                //<Чекбокс с квадратиком (tristate checkbox)>

                HtmlContainerControl pnlTristate = (HtmlContainerControl)item.FindControl("pnlTristate");

                if (pnlTristate != null)
                {
                    var tblDeviceList = item.FindControl("tblDeviceList") as Repeater;
                    if (tblDeviceList != null && tblDeviceList.Items.Count > 0)
                    {
                        pnlTristate.Visible = true;
                        string pnlDevicesId = String.Format("pnlDevices{0}", item.ItemIndex);

                        script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                            pnlTristate.ClientID, pnlDevicesId);

                        ScriptManager.RegisterStartupScript(this, GetType(),
                            String.Format("tristateCheckbox{0}", item.ItemIndex), script, true);
                        //</Чекбокс>

                        //<Отметки выбанных записей>
                        HtmlContainerControl lChecksCountInner =
                            (HtmlContainerControl)item.FindControl("lChecksCountInner");

                        script =
                            String.Format(
                                @"var CheckedRows{2} = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows{2});CheckedRows{2}();",
                                pnlDevicesId, lChecksCountInner.ClientID, item.ItemIndex);

                        ScriptManager.RegisterStartupScript(this, GetType(), String.Format("checkedRows{0}", item.ItemIndex),
                            script, true);

                        //btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRowsNoTable();");
                    }
                    else
                    {
                        pnlTristate.Visible = false;
                    }
                }
                //</Чекбокс с квадратиком
            }

            if (Session[sesClearCheckedRows] != null && (bool) Session[sesClearCheckedRows])
            {
                script = "$( document ).ready(function() {{ClearCheckedRowsNoTable();}});";
                ScriptManager.RegisterStartupScript(this, GetType(), String.Format("clearAfterEngeneerSet"),
                             script, true);
                Session[sesClearCheckedRows] = false;
            }
        }

        protected void tblList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlServiceEngeneer = (DropDownList)e.Row.FindControl("ddlServiceEngeneer");

                //MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup),
                //    true);

                //HiddenField hfIdServiceEngeneerPlan = (HiddenField)e.Row.FindControl("hfIdServiceEngeneerPlan");
                //int idServiceEngeneerPlan;
                //int.TryParse(hfIdServiceEngeneerPlan.Value, out idServiceEngeneerPlan);

                //if (idServiceEngeneerPlan > 0)
                //{
                //    MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, idServiceEngeneerPlan);
                //}
            }
        }

        protected void btnSetEngeneer_OnClick(object sender, EventArgs e)
        {
            try
            {
                int idServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer);
                string[] arrC2dIds = hfCheckedContractorsDevices.Value.Split(',');

                List<int> shownItemList = new List<int>();

                foreach (RepeaterItem ctrItem in tblContractorList.Items)
                {
                    var tblDeviceList = (ctrItem.FindControl("tblDeviceList") as Repeater);
                    bool hasChanged = false;

                    foreach (RepeaterItem item in tblDeviceList.Items)
                    {
                        //CheckBox chkSetClaim = (CheckBox)item.FindControl("chkSetClaim");
                        HiddenField hfIdContract2Devices = (HiddenField)item.FindControl("hfIdContract2Devices");
                        int idContract2Devices = MainHelper.HfGetValueInt32(ref hfIdContract2Devices);

                        if (arrC2dIds.Contains(idContract2Devices.ToString()))
                        {
                            //DropDownList ddlServiceEngeneer = (DropDownList)row.FindControl("ddlServiceEngeneer");
                            //int idServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer);

                            HiddenField hfIdClaim = (HiddenField)item.FindControl("hfIdClaim");
                            int idClaim = MainHelper.HfGetValueInt32(ref hfIdClaim);

                            ServiceClaim claim = new ServiceClaim() { Id = idClaim, IdContract2Devices = idContract2Devices, IdServiceEngeneer = idServiceEngeneer, IdCreator = User.Id };
                            claim.Save();
                            hasChanged = true;
                        }
                    }

                    if (hasChanged)
                    {
                        shownItemList.Add(ctrItem.ItemIndex);
                    }
                }

                tblContractorList.DataBind();

                foreach (RepeaterItem ctrItem in tblContractorList.Items)
                {
                    if (shownItemList.Contains(ctrItem.ItemIndex))
                    {
                        ShowDevices(ctrItem.ItemIndex, true);
                    }
                }

                ClearCheckedRows();
                //ShowOpenedDeviceLists();
                //RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void ClearCheckedRows()
        {
            Session[sesClearCheckedRows] = true;

            //hfCheckedContractorsDevices.Value = String.Empty;
            //foreach (RepeaterItem item in tblContractorList.Items)
            //{
            //    var tblDeviceList = (item.FindControl("tblDeviceList") as Repeater);

            //    if (tblDeviceList != null)
            //    {
            //        foreach (RepeaterItem devItem in tblDeviceList.Items)
            //        {
            //            var chkSetClaim = devItem.FindControl("chkSetClaim") as HtmlInputCheckBox;
            //            if (chkSetClaim != null)
            //            {
            //                chkSetClaim.Checked = false;
            //            }
            //        }
            //    }
            //}

            //string script = "$( document ).ready(function() {{ClearCheckedRowsNoTable();}});";
            //ScriptManager.RegisterStartupScript(this, GetType(), String.Format("clearAfterEngeneerSet"),
            //             script, true);

        }

        //private void ShowOpenedDeviceLists()
        //{
        //    foreach (RepeaterItem item in tblContractorList.Items)
        //    {

        //    }
        //}

        protected void txtContractorSelection_OnTextChanged(object sender, EventArgs e)
        {
            FillFilterLists();
            //string text = MainHelper.TxtGetText(ref txtContractorSelection);
            //MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text));
            ddlContractor.Focus();
        }

        protected void txtAddressFilter_OnTextChanged(object sender, EventArgs e)
        {
            FillFilterLists();
            //string text = MainHelper.TxtGetText(ref txtAddressFilter);
            //MainHelper.DdlFill(ref ddlAddress, Db.Db.Srvpl.GetContract2DevicesAddressSelectionList(text));
            ddlAddress.Focus();
        }

        protected void txtCityFilter_OnTextChanged(object sender, EventArgs e)
        {
            FillFilterLists();
            //string text = MainHelper.TxtGetText(ref txtCityFilter);
            //MainHelper.DdlFill(ref ddlCity, Db.Db.Srvpl.GetContract2DevicesCitiesSelectionList(text));
            ddlCity.Focus();
        }

        protected void tblContractorList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SqlDataSource sdsDeviceList = e.Item.FindControl("sdsDeviceList") as SqlDataSource;

                if (sdsDeviceList != null)
                {
                    sdsDeviceList.SelectParameters["id_contractor"].DefaultValue = DataBinder.Eval(e.Item.DataItem, "id_contractor").ToString();

                    sdsDeviceList.SelectParameters["date_month"].DefaultValue = sdsContractorList.SelectParameters["date_month"].DefaultValue;

                    //ShowDevices(e.Item.ItemIndex);
                }
            }
        }

        protected void btnShowDevices_OnClick(object sender, EventArgs e)
        {
            int itemIndex = Convert.ToInt32((sender as WebControl).Attributes["ItemIndex"]);

            ShowDevices(itemIndex);

            //string script = String.Format("$( document ).ready(function() {{$('#pnlDevices{0}').focus();}});", itemIndex);
            ////string script = String.Format("document.getElementById('pnlDevices{0}').focus();", itemIndex);
            //ScriptManager.RegisterStartupScript(this, GetType(), String.Format("focusDevicePnl{0}", itemIndex), script, true);
            ////Session[sesFocus] = String.Format("{0}", (sender as WebControl).ClientID);
            string script = String.Format("$( document ).ready(function() {{$('#{0}').focus();}});", (sender as WebControl).ClientID);
            ScriptManager.RegisterStartupScript(this, GetType(), String.Format("focusDevicePnl{0}", itemIndex), script, true);

        }

        private void ShowDevices(int itemIndex, bool? display = null)
        {
            var tblDeviceList = (tblContractorList.Items[itemIndex].FindControl("tblDeviceList") as Repeater);
            var sdsDeviceList = (tblContractorList.Items[itemIndex].FindControl("sdsDeviceList") as SqlDataSource);

            var hfFlag = (tblContractorList.Items[itemIndex].FindControl("hfFlag") as HiddenField);

            if (display == null)
                display = hfFlag != null && String.IsNullOrEmpty(hfFlag.Value);

            if (display.Value)
            {
                if (Request.QueryString["mth"] == null)
                { sdsDeviceList.SelectParameters["date_month"].DefaultValue = txtDateMonth.Text; }

                tblDeviceList.DataSource = sdsDeviceList;
                hfFlag.Value = "1";
            }
            else
            {
                tblDeviceList.DataSource = null;
                hfFlag.Value = String.Empty;
            }

            tblDeviceList.DataBind();
        }

        protected void tblDeviceList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnShowAllDevices_OnClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in tblContractorList.Items)
            {
                ShowDevices(item.ItemIndex, true);
            }
        }

        protected void btnhideAllDevices_OnClick(object sender, EventArgs e)
        {
            RedirectWithParams();
        }

        protected void sdsContractorList_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.CommandTimeout = 0;
        }

    }
}