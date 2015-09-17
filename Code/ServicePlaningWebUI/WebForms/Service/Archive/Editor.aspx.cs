using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class Editor : BasePage
    {
        protected string FormTitle;
        protected const string ListUrl = "~/Service";
        protected const string qspContractId = "ctrid";

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        private int ContractId
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString[qspContractId], out id);

                return id;
            }
        }

        protected bool OneDeviceList
        {
            get { return Id > 0; }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillLists();
                SetDefaultValues();
            }
        }

        private void SetDefaultValues()
        {
            int contractId;
            int.TryParse(Request.QueryString[qspContractId], out contractId);

            MainHelper.DdlSetSelectedValue(ref ddlContract, contractId);

            DataTable dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(contractId);
            MainHelper.ChkListFill(ref chklDeviceList, dtDevices);
            MainHelper.DdlFill(ref ddlDevice, dtDevices);
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            FormPartsDisplay();

            if (!IsPostBack)
            {
                FormTitle = "Добавление выезда на обслуживание";

                ServiceClaim serviceClaim = new ServiceClaim();

                if (Id > 0)
                {
                    serviceClaim = new ServiceClaim(Id);
                }

                FillFormData(serviceClaim);
            }

            RegisterStartupScripts();
        }

        private void FormPartsDisplay()
        {
            pnlOneDeviceList.Visible = pnlDeviceList.Visible = false;

            if (!OneDeviceList)
            {
                pnlDeviceList.Visible = true;
            }
            else
            {
                //Показываем конкретное оборудование для редактирования заявки к нему
                pnlOneDeviceList.Visible = true;
            }
        }

        private void FillLists()
        {
            MainHelper.DdlFill(ref ddlContract, Db.Db.Srvpl.GetContractSelectionList(), true);

            DataTable dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(0);

            MainHelper.DdlFill(ref ddlDevice, dtDevices);
            MainHelper.ChkListFill(ref chklDeviceList, dtDevices);

            MainHelper.DdlFill(ref ddlServiceType, Db.Db.Srvpl.GetServiceTypeSelectionList(), true);

            string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true);
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
                ServerMessageDisplay(ref phServerMessage, ex.Message, true);
            }
        }

        private void Save()
        {
            ServiceClaim serviceClaim = GetFormData();
            serviceClaim.Save();
            string messageText = String.Format("Сохранение выезда № {0} прошло успешно", serviceClaim.Number);
            ServerMessageDisplay(ref phServerMessage, messageText);
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
                ServerMessageDisplay(ref phServerMessage, ex.Message, true);
            }
        }

        private ServiceClaim GetFormData()
        {
            int[] lstDeviceIds;

            //При редактировании выводится только то устройство, которое на которое составлена заявка без возможности изменить
            if (OneDeviceList)
            {
                lstDeviceIds = new[] { MainHelper.DdlGetSelectedValueInt(ref ddlDevice) };
            }
            else
            {
                lstDeviceIds = MainHelper.ChkListGetCheckedValuesInt(ref chklDeviceList);
            }

            ServiceClaim serviceClaim = new ServiceClaim
            {
                Id = Id,
                IdContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract),
                LstIdDevice = lstDeviceIds,
                IdServiceType = MainHelper.DdlGetSelectedValueInt(ref ddlServiceType),
                IdServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer, true),
                PlaningDate = MainHelper.TxtGetTextDateTime(ref txtPlaningDate, true),
                Descr = MainHelper.TxtGetText(ref txtDescr),
                IdCreator = User.Id
            };

            //serviceClaim.OrderNum ???

            return serviceClaim;
        }

        private void FillFormData(ServiceClaim serviceClaim)
        {
            FormTitle = Id == 0 ? "Добавление выезда на обслуживание" : String.Format("Редактирование выезда №{0}", serviceClaim.Number);

            //MainHelper.DdlSetSelectedValue(ref ddlContract, serviceClaim.IdContract);

            if (OneDeviceList)
            {
                //txtContractSelection.Enabled = 
                    ddlContract.Enabled = false;
            }

            if (OneDeviceList)
            {
                //При редактировании выводим только то устройство, которое на которое составлена заявка без возможности изменить
                MainHelper.DdlSetSelectedValue(ref ddlDevice, serviceClaim.LstIdDevice[0], false);
            }
            else
            {
                foreach (ListItem li in chklDeviceList.Items)
                {
                    li.Selected = true;
                }
            }

            MainHelper.DdlSetSelectedValue(ref ddlServiceType, serviceClaim.IdServiceType);
            MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, serviceClaim.IdServiceEngeneer);
            MainHelper.TxtSetDate(ref txtPlaningDate, serviceClaim.PlaningDate);
            MainHelper.TxtSetText(ref txtDescr, serviceClaim.Descr);
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            //string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContract.ClientID, txtContractSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filterContractListByNumber", script, true);

            ////script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlDevice.ClientID, txtDeviceSelection.ClientID);

            ////ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListBySerial", script, true);
            //</Фильтрация списка>

            if (!OneDeviceList && chklDeviceList.Items.Count > 0)
            {
                //<Чекбокс с квадратиком (tristate checkbox)>
                script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                    pnlTristate.ClientID, chklDeviceList.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);
                //</Чекбокс>

                //<Отметки выбанных записей>
                script =
                    String.Format(
                        @"var CheckedRows = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows);CheckedRows();",
                        chklDeviceList.ClientID, lChecksCount.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);
                //</Чекбокс с квадратиком
            }
        }

        protected void ddlContract_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int contractId = MainHelper.DdlGetSelectedValueInt(ref ddlContract);
            string newQueryParams = String.Format("{0}={1}", qspContractId, contractId);

            RedirectWithParams(newQueryParams);
        }
    }
}