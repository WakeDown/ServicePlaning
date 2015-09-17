using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class Plan : BaseFilteredPage
    {
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        protected const string sesDeviceIdsKey = "devicePlanIds";

        /*queryStringFilterParams
         number - № договора
         ctrtr - Контрагент
         inn - ИНН контрагента (поле подбора)
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("number", txtNumber));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("inn", txtContractorInn));
            FilterLinks.Add(new FilterLink("pldt", txtPlaningDate));
            FilterLinks.Add(new FilterLink("ninp", rblNotInPlanList, "1"));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormTitle;
        protected const string ListUrl = "~/Service";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FillFilterLists();
            }

            base.Page_Load(sender, e);

            FillFormData();
            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                rtrServiceIntervalPlanGroups.DataBind();
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Srvpl.GetContractorFilterSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
        }

        private void FillFormData()
        {
            //MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, User.Id);
            DateTime? planingDate = MainHelper.TxtGetTextDateTime(ref txtPlaningDate, true);

            if (planingDate != null)
            {
                DateTime date = planingDate.Value;
                FormTitle = String.Format("Формирование плана выездов на {0:MMMM} {1} года", date, date.Year);
            }
        }

        protected void btnSaveAndAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                string messageText = String.Format("Сохранение плана прошло некорректно!\r\n{0}", ex.Message);
                ServerMessageDisplay(new[] { phServerMessage, phServerMessageBottom }, messageText, true);
            }
        }

        protected void btnSaveAndBack_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams(String.Empty, false, ListUrl);
            }
            catch (Exception ex)
            {
                string messageText = String.Format("Сохранение плана прошло некорректно!\r\n{0}", ex.Message);
                ServerMessageDisplay(new[] { phServerMessage, phServerMessageBottom }, messageText, true);
            }
        }

        private void Save()
        {
            ServiceClaim serviceClaim = GetFormData();
            serviceClaim.Save();

            string messageText = String.Format("Сохранение плана прошло успешно");
            ServerMessageDisplay(new[] { phServerMessage, phServerMessageBottom }, messageText);
        }

        private ServiceClaim GetFormData()
        {
            List<int> lstrContract2DevicesIds = new List<int>();
            DateTime? planingDate = MainHelper.TxtGetTextDateTime(ref txtPlaningDate, true);

            //foreach (RepeaterItem riServInt in rtrServiceIntervalPlanGroups.Items)
            //{
            //    GridView tblDeviceList = (GridView)riServInt.FindControl("tblDeviceList");

            //    foreach (GridViewRow row in tblDeviceList.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            CheckBox chkIdContract2Devices = (CheckBox)row.FindControl("chkIdContract2Devices");

            //            if (chkIdContract2Devices.Checked)
            //            {
            //                int idContract2Devices = Convert.ToInt32(((HiddenField)row.FindControl("hfIdContract2Devices")).Value);
            //                lstrContract2DevicesIds.Add(idContract2Devices);
            //            }
            //        }
            //    }
            //}

            foreach (var id in hfCheckedDevicePlanIds.Value.Split(','))
            {
                int idContract2Devices = Convert.ToInt32(id);
                lstrContract2DevicesIds.Add(idContract2Devices);
            }

            ServiceClaim serviceClaim = new ServiceClaim
            {
                Id = 0,
                LstIdContract2Devices = lstrContract2DevicesIds.ToArray(),
                PlaningDate = planingDate,
                IdCreator = User.Id
            };


            //ServiceClaim serviceClaim = new ServiceClaim
            //{
            //    Id = 0,
            //    IdContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract),
            //    LstIdDevice = lstDeviceIds.ToArray(),
            //    IdServiceType = MainHelper.DdlGetSelectedValueInt(ref ddlServiceType),
            //    IdServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer, true),
            //    PlaningDate = MainHelper.TxtGetTextDateTime(ref txtPlaningDate, true),
            //    Descr = MainHelper.TxtGetText(ref txtDescr),
            //    IdCreator = User.Id
            //};

            return serviceClaim;
        }

        protected void rtrServiceIntervalPlanGroups_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            //InitTristateAndSelect(e.Item);
        }

        protected void rtrServiceIntervalPlanGroups_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            InitTristateAndSelect(e.Item);
        }

        private void InitTristateAndSelect(RepeaterItem item)
        {
            string script = String.Empty;

            //<Чекбокс с квадратиком (tristate checkbox)>
            //foreach (RepeaterItem item in rtrServiceIntervalPlanGroups.Items)
            //{
            //RepeaterItem item = e.Item;

            GridView tblDeviceList = (GridView)item.FindControl("tblDeviceList");
            if (tblDeviceList.Rows.Count > 0)
            {
                HtmlContainerControl pnlTristate =
                    (HtmlContainerControl)tblDeviceList.HeaderRow.FindControl("pnlTristate");

                script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                    pnlTristate.ClientID, tblDeviceList.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(),
                    String.Format("tristateCheckbox{0}", item.ItemIndex), script, true);
                //</Чекбокс>

                //<Отметки выбанных записей>
                HtmlContainerControl lChecksCount = (HtmlContainerControl)item.FindControl("lChecksCount");

                script =
                    String.Format(
                        @"var CheckedRows{2} = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows{2});CheckedRows{2}();",
                        tblDeviceList.ClientID, lChecksCount.ClientID, item.ItemIndex);

                ScriptManager.RegisterStartupScript(this, GetType(), String.Format("checkedRows{0}", item.ItemIndex),
                    script, true);
            }
            //}
            //</Чекбокс с квадратиком
        }

        protected string SetNoServiceMonthsCountVisual(string monthsCount)
        {
            StringBuilder result = new StringBuilder();

            int count;
            int.TryParse(monthsCount, out count);

            result.Append("<div class='no-service-manth-square nowrap'>");

            for (int i = 1; i <= count; i++)
            {
                result.Append("<span class='danger'>&nbsp;&nbsp;</span>");
            }

            //if (count == 0)
            //{
            //    result.Append("<span class='success'>&nbsp;&nbsp;</span>");
            //}

            result.Append("</div>");

            return result.ToString();
        }

        
        private string LastContractName;
        private string LastGroupId;

        protected void tblDeviceList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string currContractName = ((DataRowView)e.Row.DataItem)["contract"].ToString();
                string currGroupId = ((DataRowView)e.Row.DataItem)["id_service_interval_plan_group"].ToString();

                if (String.IsNullOrEmpty(LastGroupId) || !LastGroupId.Equals(currGroupId))
                {
                    LastContractName = string.Empty;
                    LastGroupId = currGroupId;
                }

                if (!LastContractName.Equals(currContractName))
                {
                    ((Literal)e.Row.FindControl("litContract")).Text = String.Format("<p>{0}</p>", currContractName);

                    LastContractName = currContractName;

                    ((Literal)e.Row.FindControl("litLastClaimIndend")).Text = "<br /><br /><br /><br />";
                    ((Literal)e.Row.FindControl("litLastCameIndent")).Text = "<br /><br /><br /><br />";
                }
            }
        }

        protected void sdsDeviceList_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.CommandTimeout = 10000;
        }

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            //<Память для фильтра на раскрытие/закрытие>
            script = String.Format(@"$(function() {{ initFilterExpandMemmory('{0}', '{1}') }});", "filterHead", "filterPanel");

            ScriptManager.RegisterStartupScript(this, GetType(), "filterExpandMemmory", script, true);
            //</Память для фильтра на раскрытие/закрытие>

            //<Фильтрация списка по вводимому тексту>
            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorInn.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>

            //<Отметки выбанных записей>
            script = String.Format(@"$(function() {{initSelectedMemmoryNoTable('{0}', '{2}', '{1}', '{3}');  }});",
                pnlDevices.ClientID, lChecksCount.ClientID, sesDeviceIdsKey, hfCheckedDevicePlanIds.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "selMem", script, true);
            //вешаем обработчики
            btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRowsNoTable();");

            //</вешаем обработчики>
            //</Отметки>

        }
    }
}