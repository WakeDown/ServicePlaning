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
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class DevicesEditor : BasePage
    {
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        private const string succesMessageSesKey = "succesMessageSesKey";
        private const string IsSavedSesKey = "IsSavedSesKey";

        /*queryStringFilterParams
         mdl - Модель
         snum - Серийный номер
         sint - Интервал
         cit - Город
         adr - Адрес
         cont - Конт. лицо
         */



        protected string FormTitle = "Добавление аппарата к договору";
        protected const string ListUrl = "~/Contracts";
        protected string DevicesListUrl = FriendlyUrl.Href("~/Devices");
        protected const string qspCheckedDeviceIds = "lstids";
        protected const string qspDeviceId = "devid";
        private const string sesContract2DevicesIdsKey = "contract2devicesIds";
        protected const string qspContract2DeviceId = "c2d";

        protected bool CheckedDevices
        {
            get { return !String.IsNullOrEmpty(Request.QueryString[qspCheckedDeviceIds]); }
        }

        //private int _id;

        private const string idContractKey = "vskeyidContract";

        private int Id//Здесь это id_contract к которому привязан аппарат
        {
            get
            {
                //if (_id != new int()) return _id;

                //if (c2d != new Contract2Devices()) return c2d.IdContract;

                int id;
                int.TryParse(Request.QueryString["id"], out id);

                if (ViewState[idContractKey] != null && id <= 0)
                {
                    id = (int) ViewState[idContractKey];
                }

                return id;
            }
            set
            {
                ViewState[idContractKey] = value;
            }
        }

        private int IdContract2Device//id_contract2device для редактирования
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString[qspContract2DeviceId], out id);

                return id;
            }
        }

        private Contract2Devices c2d { get; set; }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IdContract2Device > 0)
                {
                    c2d = new Contract2Devices(IdContract2Device);
                    Id = c2d.IdContract;
                }

                FillLists(c2d);
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            FormPartsDisplay();

            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    Contract contract = new Contract(Id);
                    FormTitle = String.Format("Добавление аппаратов к договору №{0}", contract.Number);
                }

                //Contract2Devices c2d = null;
                //if (IdContract2Device > 0)
                //{
                //    //c2d = new Contract2Devices(IdContract2Device);

                //}

                FillFormData(c2d);
                DisplayGraphickList();

                if (IdContract2Device > 0)
                {
                    DisableFormParts();
                }
            }

            RegisterStartupScripts();

            //Так как при сохранении происходит редирект, то показываем сообщение об успехе после редиректа
            bool isSaved = Session[IsSavedSesKey] != null && (bool)Session[IsSavedSesKey];

            if (isSaved)
            {
                string messageText = Session[succesMessageSesKey] == null
                    ? null
                    : Session[succesMessageSesKey].ToString();
                ServerMessageDisplay(new[] { phServerMessage, phServerMessageTop }, messageText);
                Session[succesMessageSesKey] = String.Empty;
            }

            Session[IsSavedSesKey] = false;
        }

        private void DisableFormParts(bool disable = true)
        {
            ddlContractNumber.Enabled = ddlDevices.Enabled = btnDeviceBrowse.Enabled = !disable;
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{

        //}


        //protected void rtrContract2DevicesList_OnPreRender(object sender, EventArgs e)
        //{
        //    Contract2DevicesListDisplay();
        //}

        //private void Contract2DevicesListDisplay()
        //{
        //    bool hasData = rtrContract2DevicesList.Items.Count > 0;

        //    tbNoData.Visible = !hasData;
        //    tbHeader.Visible = hasData;
        //}

        private void FillLists(Contract2Devices contract2Devices = null)
        {
            int idContract = Id;
            string idDevice = null;
            if (contract2Devices != null)
            {
                idContract = contract2Devices.IdContract;
                idDevice = contract2Devices.LstIdDevice.First().ToString();
            }

            //==Form
            MainHelper.DdlFill(ref ddlContractNumber, Db.Db.Srvpl.GetContractSelectionList(idContract), true);

            if (CheckedDevices && contract2Devices == null)
            {
                //Выбранное оборудование
                string deviceIds = Request.QueryString[qspCheckedDeviceIds];
                MainHelper.ChkListFill(ref chklCheckedDeviceList, Db.Db.Srvpl.GetDeviceSelectionList(Id, deviceIds));
            }
            else
            {
                MainHelper.DdlFill(ref ddlDevices, Db.Db.Srvpl.GetDeviceSelectionList(idContract, idDevice), true);
            }

            MainHelper.DdlFill(ref ddlServiceIntervals, Db.Db.Srvpl.GetServiceIntervalSelectionList(), true);
            //MainHelper.DdlFill(ref ddlCity, Db.Db.Unit.GetCitiesSelectionList(), true);
            MainHelper.DdlFill(ref ddlAddress, Db.Db.Srvpl.GetAddressesSelectionList(), true);

            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true);

            Contract contract = new Contract(Id);

            FillServiceSchedule(contract.DateBegin, contract.DateEnd);

            //==Filter
            //////MainHelper.DdlFill(ref ddlFilterModel, Db.Db.Srvpl.GetDeviceModelSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            //////MainHelper.DdlFill(ref ddlFilterServiceIntervals, Db.Db.Srvpl.GetServiceIntervalSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            //////MainHelper.DdlFill(ref ddlFilterCity, Db.Db.Unit.GetCitiesSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            //////MainHelper.DdlFill(ref ddlFilterServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected class ScheduleDates
        {
            public DateTime Date { get; set; }

            public ScheduleDates(DateTime d)
            {
                this.Date = d;
            }
        }

        protected void FillServiceSchedule(DateTime? dateBegin, DateTime? dateEnd)
        {
            if (dateBegin == null || dateEnd == null) return;

            List<DateTime> monthes = new List<DateTime>();

            DateTime date = dateBegin.Value;

            while (date <= dateEnd.Value)
            {
                monthes.Add(new DateTime(date.Year, date.Month, 1));
                date = date.AddMonths(1);
            }

            List<ScheduleDates> years = new List<ScheduleDates>();
            foreach (DateTime month in monthes)
            {
                if (years.Count > 0 && years.Last().Date.Year >= month.Year) continue;
                years.Add(new ScheduleDates(new DateTime(month.Year, 1, 1)));
            }

            rtrServiceSchedule.DataSource = years;
            rtrServiceSchedule.DataBind();

            foreach (RepeaterItem item in rtrServiceSchedule.Items)
            {
                string year = (item.FindControl("hfSchedYear") as HiddenField).Value;

                foreach (DateTime month in monthes)
                {
                    if (year.Equals(month.Year.ToString()))
                    {
                        string css = "active";

                        if ((month.Month < DateTime.Now.Month && month.Year == DateTime.Now.Year) || month.Year < DateTime.Now.Year)
                        {
                            css = "past";
                        }

                        (item.FindControl("pnlSchedMonth" + month.Month) as Panel).CssClass = css;
                    }
                }

                if (Convert.ToInt32(year) == DateTime.Now.Year)
                {
                    (item.FindControl("pnlSchedMonth" + DateTime.Now.Month) as Panel).CssClass += " current";
                }

                
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams();
                //RedirectWithParams("id=" + Id, false);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage, phServerMessageTop }, ex.Message, true);
            }
        }

        private void Save()
        {
            //bool addScheduleDates2ServicePlan = chkAddScheduleDates2ServicePlan.Checked;

            Contract2Devices contract2Devices = GetFormData();
            
            contract2Devices.Save(true);

            //StringBuilder messageText = new StringBuilder();

            //foreach (int id in contract2Devices.LstIdDevice)
            //{
            //    Device device = new Device(id);
            //    messageText.AppendLine(String.Format("Аппарат с серийным номером {0} успешно привязан к договору", device.SerialNum));
            //}

            string messageText = String.Empty;

            //Если аппаратов больше одного то используем большой список иначе одиночный
            if (contract2Devices.LstIdDevice.Count() > 1)
            {
                messageText = String.Format("Аппараты в количестве {0} шт. успешно привязаны к договору",
                    contract2Devices.LstIdDevice.Count());
            }
            else
            {
                Device device = new Device(contract2Devices.LstIdDevice[0]);
                messageText = String.Format("Аппарат с серийным номером {0} успешно привязан к договору", device.SerialNum);
            }

            Session[IsSavedSesKey] = true;
            ServerMessageDisplay(new[] { phServerMessage, phServerMessageTop }, messageText);
            Session[succesMessageSesKey] = messageText;
        }

        //private void SaveGraphick()
        //{
        //    Contract2Devices contract2Devices = GetFormData();
        //    contract2Devices.Save(true);
        //}

        //protected void btnSaveGraphick_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SaveGraphick();
        //    }
        //    catch (Exception ex)
        //    {
        //        ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
        //    }
        //}

        protected void btnSaveAndBack_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave_Click(sender, e);
                //RedirectBack();
                RedirectWithParams(String.Empty, false, ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void FormClear()
        {
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlDevices);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlCity);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceIntervals);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlAddress);
            //MainHelper.TxtSetEmptyText(ref txtAddress);
            MainHelper.TxtSetEmptyText(ref txtContactName);
            MainHelper.TxtSetEmptyText(ref txtComment);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new Contract2Devices().Delete(id);
                RedirectWithParams();
                //ServerMessageDisplay(new [] { phServerMessage });
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private Contract2Devices GetFormData()
        {
            Contract2Devices contract2Devices = new Contract2Devices();

            contract2Devices.Id = IdContract2Device;

            contract2Devices.IdContract = Id;

            int[] lstDeviceIds = { };
            if (CheckedDevices)
            {
                lstDeviceIds = MainHelper.ChkListGetCheckedValuesInt(ref chklCheckedDeviceList);
            }
            else
            {
                lstDeviceIds = new[] { MainHelper.DdlGetSelectedValueInt(ref ddlDevices) };
            }

            //contract2Devices.IdDevice = MainHelper.DdlGetSelectedValueInt(ref ddlDevices);
            contract2Devices.LstIdDevice = lstDeviceIds;

            contract2Devices.IdServiceAdmin = MainHelper.DdlGetSelectedValueInt(ref ddlServiceAdmin);

            contract2Devices.IdServiceInterval = MainHelper.DdlGetSelectedValueInt(ref ddlServiceIntervals);
            contract2Devices.IdCity = MainHelper.DdlGetSelectedValueInt(ref ddlCity);
            contract2Devices.Address = ddlAddress.SelectedItem.Text;
            //contract2Devices.Address = MainHelper.TxtGetText(ref txtAddress);
            contract2Devices.ContactName = MainHelper.TxtGetText(ref txtContactName);
            contract2Devices.Comment = MainHelper.TxtGetText(ref txtComment);
            contract2Devices.ObjectName = MainHelper.TxtGetText(ref txtObjectName);
            contract2Devices.Coord = MainHelper.TxtGetText(ref txtCoord);
            contract2Devices.IdCreator = User.Id;

            contract2Devices.ScheduleDates = GetScheduleDates();

            return contract2Devices;
        }

        private DateTime[] GetScheduleDates()
        {
            List<DateTime> dates = new List<DateTime>();

            foreach (RepeaterItem item in rtrServiceSchedule.Items)
            {
                int year = Convert.ToInt32((item.FindControl("hfSchedYear") as HiddenField).Value);

                for (int i =1;i<=12;i++)
                {
                    bool selected = (item.FindControl("hfSchedMonth" + i) as HiddenField).Value != "";

                    if (selected)
                    {
                        dates.Add(new DateTime(year, i, 1));
                    }
                }
            }

            return dates.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillFormData(Contract2Devices contract2Devices = null)
        {
            int idContract = Id;

            if (contract2Devices != null)
            {
                idContract = contract2Devices.IdContract;
                FormTitle = String.Format("Редактирование аппарата на договоре");
            }

            MainHelper.DdlSetSelectedValue(ref ddlContractNumber, idContract);

            if (CheckedDevices && contract2Devices == null)
            {
                foreach (ListItem li in chklCheckedDeviceList.Items)
                {
                    li.Selected = true;
                }
            }
            else
            {
                string idDevice = Request.QueryString[qspDeviceId];

                if (contract2Devices != null)
                {
                    idDevice = contract2Devices.LstIdDevice.First().ToString();
                }

                if (!String.IsNullOrEmpty(idDevice))
                {
                    MainHelper.DdlSetSelectedValue(ref ddlDevices, idDevice);
                }
            }

            if (contract2Devices != null)
            {
                MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, contract2Devices.IdServiceAdmin);

                if (contract2Devices.IdCity > 0)
                    MainHelper.DdlFill(ref ddlCity, Db.Db.Unit.GetCitiesSelectionList(idCity: contract2Devices.IdCity), true);

                MainHelper.DdlSetSelectedValue(ref ddlCity, contract2Devices.IdCity);
                MainHelper.DdlSetSelectedValue(ref ddlServiceIntervals, contract2Devices.IdServiceInterval);
                //MainHelper.TxtSetText(ref txtAddress, contract2Devices.Address);
                try
                {
                    ddlAddress.Items.FindByText(contract2Devices.Address).Selected = true;
                }
                catch
                {
                }
                
                MainHelper.TxtSetText(ref txtObjectName, contract2Devices.ObjectName);
                MainHelper.TxtSetText(ref txtContactName, contract2Devices.ContactName);
                MainHelper.TxtSetText(ref txtComment, contract2Devices.Comment);
                FillScheduleDates(contract2Devices);
            }
        }

        private void FillScheduleDates(Contract2Devices contract2Devices)
        {
            if (contract2Devices == null) return;

            contract2Devices.GetScheduleDates();

            if (contract2Devices.ScheduleDates == null) return;

            contract2Devices.GetCameDates();


            foreach (RepeaterItem item in rtrServiceSchedule.Items)
            {
                string year = (item.FindControl("hfSchedYear") as HiddenField).Value;

                foreach (DateTime date in contract2Devices.ScheduleDates)
                {
                    string css = (item.FindControl("pnlSchedMonth" + date.Month) as Panel).CssClass;

                    var hfSchedMonth = (item.FindControl("hfSchedMonth" + date.Month) as HiddenField);
                    hfSchedMonth.Value = "";

                    if (year.Equals(date.Year.ToString()))
                    {
                        if ((contract2Devices.CameDates != null && contract2Devices.CameDates.Contains(date)))
                        {
                            css = "cameing";
                        }
                        else if (date.Year < DateTime.Now.Year || (date.Year == DateTime.Now.Year && date.Month < DateTime.Now.Month))
                        {
                            css = "selected-past";
                        }
                        else
                        {
                            css = " selected";
                        }

                        hfSchedMonth.Value = "1";
                    }

                    (item.FindControl("pnlSchedMonth" + date.Month) as Panel).CssClass = css;
                }
            }
        }

        //////protected void btnSearch_OnClick(object sender, EventArgs e)
        //////{
        //////    Search();
        //////}

        //////protected void sdsContract2DevicesList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        //////{
        //////    int count = e.AffectedRows;
        //////    SetRowsCount(count);
        //////}

        //////private void SetRowsCount(int count = 0)
        //////{
        //////    lRowsCount.Text = count.ToString();
        //////}

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            //<Фильтрация списка по вводимому тексту>
            /*script = String.Format(@"var btn = $('#{0}');
            $(btn).click(function() {{ 
                DisplayList(null);
            }});

            var DisplayList = function(lock) {{
                var ddl = $('#{1}'); var dis = ddl.prop('disabled');
                if (lock != null && (lock || !lock)) {{ dis = !lock; }}
                ddl.prop('disabled', !dis); 
                $(btn).removeClass('btn-success').removeClass('btn-danger');
                var btnClass = dis ? 'btn-danger' : 'btn-success';
                $(btn).addClass(btnClass);
                var title = dis ? 'заблокировать список' : 'разблокировать список';
                $(btn).tooltip('hide').attr('data-original-title', title).tooltip('fixTitle').tooltip();
                var ico = $(btn).children('i');
                 $(ico).removeClass('fa-unlock-alt').removeClass('fa-lock');
                var icoClass = dis ? 'fa-lock' : 'fa-unlock-alt' ;
                $(ico).addClass(icoClass);
            }} 

            //$('#{1}').change(function() {{DisplayList(true);}});

            $(function() {{ DisplayList(true); }}); 
            ", btnContractUnlock.ClientID, ddlContractNumber.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "displayContractList", script, true);*/
            //</Фильтрация списка>

            ////////<Чекбокс с квадратиком (tristate checkbox)>
            //////script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false); }});",
            //////        pnlListTristate.ClientID, pnlContract2DevicesList.ClientID);

            //////ScriptManager.RegisterStartupScript(this, GetType(), "tristateListCheckbox", script, true);
            ////////</Чекбокс>

            ////////<Отметки выбанных записей>
            //////script = String.Format(@"$(function() {{initSelectedMemmory('{0}', '{2}', '{1}', '{3}');  }});", "tblContract2DevicesList", lChecksListCount.ClientID, sesContract2DevicesIdsKey, hfCheckedContract2DevicesIds.ClientID);

            //////ScriptManager.RegisterStartupScript(this, GetType(), "checkedListRows", script, true);
            ////////вешаем обработчики
            //////btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRows();");

            ////////</вешаем обработчики>
            //</Отметки>

            if (CheckedDevices)
            {
                //<Чекбокс с квадратиком (tristate checkbox)>
                //HtmlContainerControl span = tblList.HeaderRow.FindControl("pnlTristate") as HtmlContainerControl;
                script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                    pnlTristate.ClientID, chklCheckedDeviceList.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);
                //</Чекбокс>

                //<Отметки выбанных записей>
                script = String.Format(@"var CheckedRows = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows);CheckedRows();", chklCheckedDeviceList.ClientID, lChecksCount.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);

                //string sesDeviceIdsKey = "sesDeviceIdsKey";

                //script = String.Format(@"$(function() {{initSelectedMemmory('{0}', '{2}', '{1}');  }});",
                //    chklCheckedDeviceList.ClientID, lChecksCount.ClientID, sesDeviceIdsKey);

                //ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);
                //вешаем обработчики
                //btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRows();");

                //string backContractId = Request.QueryString["bctid"];

                //string otherQueryParams = String.Empty;
                //if (!String.IsNullOrEmpty(backContractId)) otherQueryParams = String.Format("id={0}", Request.QueryString["bctid"]);
                //btnAddChecked.Attributes.Add("onclick", String.Format("RedirectWithValuesAndClear('{0}', '{1}', '{2}');", Contract2DevicesFormUrl, qsValues, otherQueryParams));
                //</вешаем обработчики>
                //</Отметки>


            }


            //<Отметки графика обслуживания>
            script =
                String.Format(
                    @" $( document ).ready( function(){{ $('.schedule-field .active, .schedule-field .selected').on('click', function() {{  $(this).toggleClass('selected');   $(this).find(""input[id*='hfSchedMonth']"").val($(this).hasClass('selected') ? '1' : ''); }} ); }} );");
            ScriptManager.RegisterStartupScript(this, GetType(), "checkedScheduleFields", script, true);

            script =
                String.Format(
                    @" $( document ).ready( function(){{ $('.schedule-header').on('click', function() {{ $(this).parent().find('.schedule-field .active, .schedule-field .selected').click(); }} ); }} );");
            ScriptManager.RegisterStartupScript(this, GetType(), "checkedScheduleHeaders", script, true);
            //</Отметки графика обслуживания>

            //<Фильтрация списка по вводимому тексту>
            //script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlCity.ClientID, txtCityFilter.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterCity", script, true);

            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlAddress.ClientID, txtAddressFilter.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterAddress", script, true);
            //</Фильтрация списка>
        }

        protected void btnDeviceBrowse_Click(object sender, EventArgs e)
        {
            //id договора по которому можно будет вернуться
            string newQueryParams = String.Format("bctid={0}", MainHelper.DdlGetSelectedValue(ref ddlContractNumber, true));
            RedirectWithParams(newQueryParams, false, DevicesListUrl);

        }

        protected void ddlContractNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string newQueryParams = String.Format("id={0}", MainHelper.DdlGetSelectedValue(ref ddlContractNumber, true));

            RedirectWithParams(newQueryParams);
        }

        private void FormPartsDisplay()
        {
            pnlCheckedDeviceList.Visible = pnlDeviceList.Visible = false;

            if (!CheckedDevices)
            {
                pnlDeviceList.Visible = true;
            }
            else
            {
                //Показываем выбранное оборудование
                pnlCheckedDeviceList.Visible = true;
            }
        }


        //////protected void btnCheckedDelete_OnClick(object sender, EventArgs e)
        //////{
        //////    int[] ctr2DevIds = GetCheckedDeviceIds();
        //////    Delete(ctr2DevIds);
        //////}

        //////private void Delete(int[] ctr2DevIds)
        //////{
        //////    if (ctr2DevIds.Any())
        //////    {
        //////        foreach (int ctr2devId in ctr2DevIds)
        //////        {
        //////            try
        //////            {
        //////                new Contract2Devices().Delete(ctr2devId);

        //////            }
        //////            catch (Exception ex)
        //////            {
        //////                ServerMessageDisplay(new[] { phListServerMessage }, ex.Message, true);
        //////            }
        //////        }

        //////        RedirectWithParams();
        //////    }
        //////}

        //////protected int[] GetCheckedDeviceIds()
        //////{
        //////    string ctr2DevIds = hfCheckedContract2DevicesIds.Value;

        //////    string[] result = { };

        //////    if (!String.IsNullOrEmpty(ctr2DevIds))
        //////    {
        //////        result = ctr2DevIds.Split(',');
        //////    }

        //////    int[] ids = result.Select(c2d => Convert.ToInt32(c2d)).ToArray();

        //////    return ids;
        //////}
        protected void ddlServiceIntervals_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayGraphickList();
        }

        private void DisplayGraphickList()
        {
            int idServiceInterval = MainHelper.DdlGetSelectedValueInt(ref ddlServiceIntervals);

            bool needsGraphickList = Db.Db.Srvpl.CheckServiceIntervalNeedsGraphickList(idServiceInterval);
            pnlGriphick.Visible = needsGraphickList;
        }

        protected void txtCityFilter_OnTextChanged(object sender, EventArgs e)
        {
            string filter = txtCityFilter.Text;
            MainHelper.DdlFill(ref ddlCity, Db.Db.Unit.GetCitiesSelectionList(filter), true);
            
        }
    }
}