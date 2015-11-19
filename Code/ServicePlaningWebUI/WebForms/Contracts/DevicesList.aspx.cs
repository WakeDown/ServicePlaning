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

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class DevicesList : BaseFilteredPage
    {
        private string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        private string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

        public bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("id", ddlFilterContractNumber, null, false));
            FilterLinks.Add(new FilterLink("mdl", ddlFilterModel));
            FilterLinks.Add(new FilterLink("snum", txtFilterSerialNum));
            FilterLinks.Add(new FilterLink("sint", ddlFilterServiceIntervals));
            FilterLinks.Add(new FilterLink("cit", ddlFilterCity));
            FilterLinks.Add(new FilterLink("addr", txtFilterAddress));
            FilterLinks.Add(new FilterLink("cont", txtFilterContactName));
            FilterLinks.Add(new FilterLink("sadm", ddlFilterServiceAdmin));
            FilterLinks.Add(new FilterLink("objn", txtFilterObjectName));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];

        /*queryStringFilterParams
         mdl - Модель
         snum - Серийный номер
         sint - Интервал
         cit - Город
         adr - Адрес
         cont - Конт. лицо
         */



        protected string FormTitle = "Список аппаратов на договоре";
        protected const string ListUrl = "~/Contracts";
        protected string DevicesListUrl = FriendlyUrl.Href("~/Devices");
        protected string FormUrl = FriendlyUrl.Href("~/Contracts/Devices/Editor");
        protected const string qspCheckedDeviceIds = "lstids";
        protected const string qspDeviceId = "devid";
        private const string sesContract2DevicesIdsKey = "contract2devicesIds";

        protected bool CheckedDevices
        {
            get { return !String.IsNullOrEmpty(Request.QueryString[qspCheckedDeviceIds]); }
        }

        private int Id//Здесь это id_contract к которому привязан аппарат (редактирования привязок не предполагается, только удаление)
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillLists();
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
            //////FormPartsDisplay();

            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    Contract contract = new Contract(Id);
                    FormTitle = String.Format("Список аппаратов на договоре №{0}", contract.Number);
                }

                //////FillFormData();
            }

            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            changeAsBtnContainer.Visible = UserIsSysAdmin;
        }

        private void FillLists()
        {
            //==Form


            //////if (CheckedDevices)
            //////{
            //////    //Выбранное оборудование
            //////    string deviceIds = Request.QueryString[qspCheckedDeviceIds];
            //////    MainHelper.ChkListFill(ref chklCheckedDeviceList, Db.Db.Srvpl.GetDeviceSelectionList(Id, deviceIds));
            //////}
            //////else
            //////{
            //////    MainHelper.DdlFill(ref ddlDevices, Db.Db.Srvpl.GetDeviceSelectionList(Id), true);
            //////}

            //////MainHelper.DdlFill(ref ddlServiceIntervals, Db.Db.Srvpl.GetServiceIntervalSelectionList(), true);
            //////MainHelper.DdlFill(ref ddlCity, Db.Db.Unit.GetCitiesSelectionList(), true);

            //////MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true);


            //==Filter
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Srvpl.GetContractorFilterSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlFilterContractNumber, Db.Db.Srvpl.GetContractSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlFilterModel, Db.Db.Srvpl.GetDeviceModelSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlFilterServiceIntervals, Db.Db.Srvpl.GetServiceIntervalSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlFilterCity, Db.Db.Unit.GetCitiesSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            var saList = Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup);
            MainHelper.DdlFill(ref ddlFilterServiceAdmin, saList, true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlNewServiceAdmin, saList, true, MainHelper.ListFirstItemType.Nullable); 
        }

        //////protected void btnSave_Click(object sender, EventArgs e)
        //////{
        //////    try
        //////    {
        //////        Save();
        //////        RedirectWithParams("id=" + Id, false);
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
        //////    }
        //////}

        //////private void Save()
        //////{

        //////    Contract2Devices contract2Devices = GetFormData();
        //////    contract2Devices.Save();

        //////    //StringBuilder messageText = new StringBuilder();

        //////    //foreach (int id in contract2Devices.LstIdDevice)
        //////    //{
        //////    //    Device device = new Device(id);
        //////    //    messageText.AppendLine(String.Format("Аппарат с серийным номером {0} успешно привязан к договору", device.SerialNum));
        //////    //}

        //////    string messageText = String.Empty;

        //////    //Если аппаратов больше одного то используем большой список иначе одиночный
        //////    if (contract2Devices.LstIdDevice.Count() > 1)
        //////    {
        //////        messageText = String.Format("Аппараты в количестве {0} шт. успешно привязаны к договору",
        //////            contract2Devices.LstIdDevice.Count());
        //////    }
        //////    else
        //////    {
        //////        Device device = new Device(contract2Devices.LstIdDevice[0]);
        //////        messageText = String.Format("Аппарат с серийным номером {0} успешно привязан к договору", device.SerialNum);
        //////    }

        //////    ServerMessageDisplay(new[] { phServerMessage }, messageText);

        //////}

        //////protected void btnSaveAndBack_Click(object sender, EventArgs e)
        //////{
        //////    try
        //////    {
        //////        btnSave_Click(sender, e);
        //////        //RedirectBack();
        //////        RedirectWithParams(String.Empty, false, ListUrl);
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
        //////    }
        //////}

        //////private void FormClear()
        //////{
        //////    MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlDevices);
        //////    MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlCity);
        //////    MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceIntervals);
        //////    MainHelper.TxtSetEmptyText(ref txtAddress);
        //////    MainHelper.TxtSetEmptyText(ref txtContactName);
        //////    MainHelper.TxtSetEmptyText(ref txtComment);
        //////}

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
                ServerMessageDisplay(new[] { phListServerMessage }, ex.Message, true);
            }
        }

        //////private Contract2Devices GetFormData()
        //////{
        //////    Contract2Devices contract2Devices = new Contract2Devices();

        //////    contract2Devices.IdContract = Id;

        //////    int[] lstDeviceIds = { };
        //////    if (CheckedDevices)
        //////    {
        //////        lstDeviceIds = MainHelper.ChkListGetCheckedValuesInt(ref chklCheckedDeviceList);
        //////    }
        //////    else
        //////    {
        //////        lstDeviceIds = new[] { MainHelper.DdlGetSelectedValueInt(ref ddlDevices) };
        //////    }

        //////    //contract2Devices.IdDevice = MainHelper.DdlGetSelectedValueInt(ref ddlDevices);
        //////    contract2Devices.LstIdDevice = lstDeviceIds;

        //////    contract2Devices.IdServiceAdmin = MainHelper.DdlGetSelectedValueInt(ref ddlServiceAdmin);

        //////    contract2Devices.IdServiceInterval = MainHelper.DdlGetSelectedValueInt(ref ddlServiceIntervals);
        //////    contract2Devices.IdCity = MainHelper.DdlGetSelectedValueInt(ref ddlCity);
        //////    contract2Devices.Address = MainHelper.TxtGetText(ref txtAddress);
        //////    contract2Devices.ContactName = MainHelper.TxtGetText(ref txtContactName);
        //////    contract2Devices.Comment = MainHelper.TxtGetText(ref txtComment);
        //////    contract2Devices.ObjectName = MainHelper.TxtGetText(ref txtObjectName);
        //////    contract2Devices.Coord = MainHelper.TxtGetText(ref txtCoord);
        //////    contract2Devices.IdCreator = User.Id;

        //////    return contract2Devices;
        //////}

        /// <summary>
        /// редактирования привязок не предполагается, только удаление
        /// </summary>
        //////private void FillFormData()
        //////{
        //////    MainHelper.DdlSetSelectedValue(ref ddlContractNumber, Id);

        //////    if (CheckedDevices)
        //////    {
        //////        foreach (ListItem li in chklCheckedDeviceList.Items)
        //////        {
        //////            li.Selected = true;
        //////        }
        //////    }
        //////    else
        //////    {
        //////        string idDevice = Request.QueryString[qspDeviceId];
        //////        if (!String.IsNullOrEmpty(idDevice))
        //////        {
        //////            MainHelper.DdlSetSelectedValue(ref ddlDevices, idDevice);
        //////        }
        //////    }
        //////    //MainHelper.DdlSetSelectedValue(ref ddlCity, contract2Devices.IdCity);
        //////    //MainHelper.DdlSetSelectedValue(ref ddlServiceIntervals, contract2Devices.IdServiceInterval);
        //////    //MainHelper.TxtSetText(ref txtAddress, contract2Devices.Address);
        //////    //MainHelper.TxtSetText(ref txtContactName, contract2Devices.ContactName);
        //////    //MainHelper.TxtSetText(ref txtComment, contract2Devices.Comment);
        //////}

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void sdsContract2DevicesList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }

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

            //<Чекбокс с квадратиком (tristate checkbox)>
            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false); }});",
                    pnlListTristate.ClientID, pnlContract2DevicesList.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateListCheckbox", script, true);
            //</Чекбокс>

            //<Отметки выбанных записей>
            script = String.Format(@"$(function() {{initSelectedMemmory('{0}', '{2}', '{1}', '{3}');  }});", "tblContract2DevicesList", lChecksListCount.ClientID, sesContract2DevicesIdsKey, hfCheckedContract2DevicesIds.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "checkedListRows", script, true);
            //вешаем обработчики
            btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRows();");

            //</вешаем обработчики>
            //</Отметки>


            //<Фильтрация списка по вводимому тексту>
            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorInn.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>

            //////if (CheckedDevices)
            //////{
            //////    //<Чекбокс с квадратиком (tristate checkbox)>
            //////    //HtmlContainerControl span = tblList.HeaderRow.FindControl("pnlTristate") as HtmlContainerControl;
            //////    script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
            //////        pnlTristate.ClientID, chklCheckedDeviceList.ClientID);

            //////    ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);
            //////    //</Чекбокс>

            //////    //<Отметки выбанных записей>
            //////    script = String.Format(@"var CheckedRows = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows);CheckedRows();", chklCheckedDeviceList.ClientID, lChecksCount.ClientID);

            //////    ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);

            //////    //string sesDeviceIdsKey = "sesDeviceIdsKey";

            //////    //script = String.Format(@"$(function() {{initSelectedMemmory('{0}', '{2}', '{1}');  }});",
            //////    //    chklCheckedDeviceList.ClientID, lChecksCount.ClientID, sesDeviceIdsKey);

            //////    //ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);
            //////    //вешаем обработчики
            //////    //btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRows();");

            //////    //string backContractId = Request.QueryString["bctid"];

            //////    //string otherQueryParams = String.Empty;
            //////    //if (!String.IsNullOrEmpty(backContractId)) otherQueryParams = String.Format("id={0}", Request.QueryString["bctid"]);
            //////    //btnAddChecked.Attributes.Add("onclick", String.Format("RedirectWithValuesAndClear('{0}', '{1}', '{2}');", Contract2DevicesFormUrl, qsValues, otherQueryParams));
            //////    //</вешаем обработчики>
            //////    //</Отметки>
            //////}
        }

        //////protected void btnDeviceBrowse_Click(object sender, EventArgs e)
        //////{
        //////    //id договора по которому можно будет вернуться
        //////    string newQueryParams = String.Format("bctid={0}", MainHelper.DdlGetSelectedValue(ref ddlContractNumber, true));
        //////    RedirectWithParams(newQueryParams, false, DevicesListUrl);

        //////}

        //////protected void ddlContractNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        //////{
        //////    string newQueryParams = String.Format("id={0}", MainHelper.DdlGetSelectedValue(ref ddlContractNumber, true));

        //////    RedirectWithParams(newQueryParams);
        //////}

        //////private void FormPartsDisplay()
        //////{
        //////    pnlCheckedDeviceList.Visible = pnlDeviceList.Visible = false;

        //////    if (!CheckedDevices)
        //////    {
        //////        pnlDeviceList.Visible = true;
        //////    }
        //////    else
        //////    {
        //////        //Показываем выбранное оборудование
        //////        pnlCheckedDeviceList.Visible = true;
        //////    }
        //////}

        protected void btnCheckedDelete_OnClick(object sender, EventArgs e)
        {
            int[] ctr2DevIds = GetCheckedDeviceIds();
            Delete(ctr2DevIds);
        }

        private void Delete(int[] ctr2DevIds)
        {
            if (ctr2DevIds.Any())
            {
                foreach (int ctr2devId in ctr2DevIds)
                {
                    try
                    {
                        new Contract2Devices().Delete(ctr2devId);

                    }
                    catch (Exception ex)
                    {
                        ServerMessageDisplay(new[] { phListServerMessage }, ex.Message, true);
                    }
                }

                RedirectWithParams();
            }
        }

        protected int[] GetCheckedDeviceIds()
        {
            string ctr2DevIds = hfCheckedContract2DevicesIds.Value;

            string[] result = { };

            if (!String.IsNullOrEmpty(ctr2DevIds))
            {
                result = ctr2DevIds.Split(',');
            }

            int[] ids = result.Select(c2d => Convert.ToInt32(c2d)).ToArray();

            return ids;
        }

        protected void btnChangeServiceAdmin_OnClick(object sender, EventArgs e)
        {
            int? idSa = MainHelper.DdlGetSelectedValueInt(ref ddlNewServiceAdmin, true);

            if (idSa.HasValue)
            {
                int[] ctr2DevIds = GetCheckedDeviceIds();
                ChangeServiceAdmin(ctr2DevIds, idSa.Value);
            }
        }

        private void ChangeServiceAdmin(int[] ctr2DevIds, int newIdServiceAdmin)
        {
            if (ctr2DevIds.Any())
            {
                foreach (int ctr2devId in ctr2DevIds)
                {
                    try
                    {
                        Contract2Devices.SaveServiceAdmin(ctr2devId, newIdServiceAdmin);
                    }
                    catch (Exception ex)
                    {
                        ServerMessageDisplay(new[] { phListServerMessage }, ex.Message, true);
                    }
                }

                RedirectWithParams();
            }
        }
    }
}