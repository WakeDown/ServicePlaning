using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            if (!OneDeviceList)
            {
                FillDeviceList();
            }
        }

        private void FillDeviceList()
        {
            int contractId;
            int.TryParse(Request.QueryString[qspContractId], out contractId);

            int? idCity = MainHelper.DdlGetSelectedValueInt(ref ddlFilterCity);
            string address = MainHelper.TxtGetText(ref txtFilterAddress);

            DataTable dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(contractId, null, idCity, address);
            //MainHelper.ChkListFill(ref chklDeviceList, dtDevices);
            string[] vals = GetSelectedDeviceValues(rtrDeviceList);

            rtrDeviceList.DataSource = dtDevices;
            rtrDeviceList.DataBind();

            SetSelectedDeviceValues(rtrDeviceList, vals);

            var strSelVal = ddlDevice.SelectedValue;
            MainHelper.DdlFill(ref ddlDevice, dtDevices);
            if (strSelVal != null)ddlDevice.SelectedValue = strSelVal;

        }

        private void SetSelectedDeviceValues(Repeater rtr, string[] vals)
        {
            foreach (RepeaterItem item in rtr.Items)
            {
                var chkIdC2d = item.FindControl("chkIdC2d") as CheckBox;

                if (chkIdC2d != null && !String.IsNullOrEmpty(chkIdC2d.Attributes["Value"]) && vals.Contains(chkIdC2d.Attributes["Value"]))
                {
                    chkIdC2d.Checked = true;
                }
            }
        }

        private string[] GetSelectedDeviceValues(Repeater rtr)
        {
            List<string> lst = new List<string>();

            foreach (RepeaterItem item in rtr.Items)
            {
                var chkIdC2d = item.FindControl("chkIdC2d") as CheckBox;

                if (chkIdC2d != null && chkIdC2d.Checked)
                {
                    lst.Add(chkIdC2d.Attributes["Value"]);
                }
            }

            return lst.ToArray();
        }

        private void SetDefaultValues()
        {
            int contractId;
            int.TryParse(Request.QueryString[qspContractId], out contractId);

            MainHelper.DdlSetSelectedValue(ref ddlContract, contractId);
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            FormPartsDisplay();
            FormAdditions();

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



        private void FormAdditions()
        {
            //Для отключения кнопки после нажатия
            btnSaveAndAddNew.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSaveAndAddNew, null) + ";");
            //btnSaveAndBack.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSaveAndBack, null) + ";");
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

            // DataTable dtDevices = new DataTable();

            //if (!OneDeviceList)
            //{
            //    dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(ContractId);
            //}
            //else
            //{
            //    dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(null, );
            //}

            //MainHelper.DdlFill(ref ddlDevice, dtDevices);
            //MainHelper.ChkListFill(ref chklDeviceList, dtDevices);

            MainHelper.DdlFill(ref ddlServiceClaimType, Db.Db.Srvpl.GetServiceClaimTypeSelectionList(), true);

            string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true);

            FillCitiesList();
        }

        private void FillCitiesList()
        {
            int contractId;
            int.TryParse(Request.QueryString[qspContractId], out contractId);
            string city = MainHelper.TxtGetText(ref txtCityFilter);
            string address = MainHelper.TxtGetText(ref txtFilterAddress);

            MainHelper.DdlFill(ref ddlFilterCity, Db.Db.Srvpl.GetContract2DevicesCitiesSelectionList(city, contractId, address), true, MainHelper.ListFirstItemType.SelectAll);
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
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            ServiceClaim serviceClaim = GetFormData();
            serviceClaim.Save(true);
            string messageText = String.Format("Сохранение выезда № {0} прошло успешно", serviceClaim.Number);
            ServerMessageDisplay(new[] { phServerMessage }, messageText);
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
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private ServiceClaim GetFormData()
        {
            int[] lstDeviceIds;

            ////При редактировании выводится только то устройство, которое на которое составлена заявка без возможности изменить
            if (OneDeviceList)
            {
                lstDeviceIds = new[] { MainHelper.DdlGetSelectedValueInt(ref ddlDevice) };
            }
            else
            {
                //lstDeviceIds = MainHelper.ChkListGetCheckedValuesInt(ref chklDeviceList);
                List<int> lstC2d = new List<int>();
                foreach (RepeaterItem item in rtrDeviceList.Items)
                {
                    var chkIdC2d = item.FindControl("chkIdC2d") as CheckBox;

                    if (chkIdC2d != null && chkIdC2d.Checked)
                    {
                        int idC2d = Convert.ToInt32(chkIdC2d.Attributes["Value"]);

                        lstC2d.Add(idC2d);
                    }
                }

                //string[] strDevIds = hfLstCheckedDeviceIds.Value.Split(',');

                //foreach (string id in strDevIds)
                //{
                //    int idC2d;
                //    int.TryParse(id, out idC2d);

                //    lstC2d.Add(idC2d);
                //}

                lstDeviceIds = lstC2d.ToArray();
            }

            ServiceClaim serviceClaim = new ServiceClaim
            {
                Id = Id,
                LstIdContract2Devices = lstDeviceIds,
                //IdContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract),
                IdDevice = MainHelper.DdlGetSelectedValueInt(ref ddlDevice),
                IdServiceClaimType = MainHelper.DdlGetSelectedValueInt(ref ddlServiceClaimType),
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
                DataTable dtDevices = Db.Db.Srvpl.GetContract2DevicesSelectionList(serviceClaim.IdContract, serviceClaim.IdDevice);
                MainHelper.DdlFill(ref ddlDevice, dtDevices);
                //При редактировании выводим только то устройство, которое на которое составлена заявка без возможности изменить
                MainHelper.DdlSetSelectedValue(ref ddlDevice, serviceClaim.IdDevice, false);
                txtContractSelection.Enabled =
                    ddlContract.Enabled = false;
            }
            else
            {
                //foreach (ListItem li in chklDeviceList.Items)
                //{
                //    li.Selected = true;
                //}

                //foreach (RepeaterItem item in rtrDeviceList.Items)
                //{
                //    var chkIdC2d = item.FindControl("chkIdC2d") as CheckBox;

                //    if (chkIdC2d != null)
                //    {
                //        chkIdC2d.Checked = true;
                //        //if (String.IsNullOrEmpty(hfLstCheckedDeviceIds.Value))
                //        //{
                //        //    hfLstCheckedDeviceIds.Value += chkIdC2d.Attributes["Value"];
                //        //}
                //        //else
                //        //{
                //        //    hfLstCheckedDeviceIds.Value += String.Format(",{0}", chkIdC2d.Attributes["Value"]);
                //        //}
                //        //chkIdC2d_OnCheckedChanged(chkIdC2d, new EventArgs());
                //    }
                //}
            }

            MainHelper.HfSetValue(ref hfIdContract2Devices, serviceClaim.IdContract2Devices);
            MainHelper.DdlSetSelectedValue(ref ddlServiceClaimType, serviceClaim.IdServiceClaimType);
            MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, serviceClaim.IdServiceEngeneer);
            //MainHelper.TxtSetDate(ref txtPlaningDate, serviceClaim.PlaningDate);
            MainHelper.TxtSetText(ref txtPlaningDate, String.Format("{0:MM/yyyy}", serviceClaim.PlaningDate ?? DateTime.Now));
            MainHelper.TxtSetText(ref txtDescr, serviceClaim.Descr);
        }

        private const string sesDeviceIdsKey = "sesDeviceIdsKey";

        private void RegisterStartupScripts()
        {
            string script = string.Empty;

            //<Фильтрация списка по вводимому тексту>
            //string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContract.ClientID, txtContractSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filterContractListByNumber", script, true);

            ////script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlDevice.ClientID, txtDeviceSelection.ClientID);

            ////ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListBySerial", script, true);
            //</Фильтрация списка>

            if (!OneDeviceList && rtrDeviceList.Items.Count > 0)
            {
                //<Чекбокс с квадратиком (tristate checkbox)>
                script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                    pnlTristate.ClientID, tblDeviceList.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);
                //</Чекбокс>

                //<Отметки выбанных записей>
                //script = String.Format(@"$(function() {{initSelectedMemmoryNoTable('{0}', '{2}', '{1}', '{3}');  }});",
                //tblDeviceList.ClientID, lChecksCount.ClientID, sesDeviceIdsKey, hfLstCheckedDeviceIds.ClientID);

                //ScriptManager.RegisterStartupScript(this, GetType(), "selMem", script, true);

                script =
                    String.Format(
                        @"var CheckedRows = function() {{ $('#{1}').text($('#{0} :checkbox:checked').length); }}; $('#{0} :checkbox').change(CheckedRows);CheckedRows();",
                        tblDeviceList.ClientID, lChecksCount.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);
                //</Чекбокс с квадратиком
            }

            //<Память для фильтра на раскрытие/закрытие>
            script = String.Format(@"$(function() {{ initFilterExpandMemmory('{0}', '{1}') }});", "pnlDevicesTitle", "pnlDevices");

            ScriptManager.RegisterStartupScript(this, GetType(), "filterNewClaimDevicesExpandMemmory", script, true);
            //</Память для фильтра на раскрытие/закрытие>
        }

        protected void ddlContract_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int contractId = MainHelper.DdlGetSelectedValueInt(ref ddlContract);
            string newQueryParams = String.Format("{0}={1}", qspContractId, contractId);

            RedirectWithParams(newQueryParams);
        }

        protected void txtContractSelection_OnTextChanged(object sender, EventArgs e)
        {
            string name = MainHelper.TxtGetText(ref txtContractSelection);

            var dt = Db.Db.Srvpl.GetContractSelectionList(null, name);

            MainHelper.DdlFill(ref ddlContract, dt, dt.Rows.Count > 1);

            if (ddlContract.Items.Count == 1)
            {
                ddlContract_OnSelectedIndexChanged(ddlContract, null);
            }
        }

        protected void txtCityFilter_OnTextChanged(object sender, EventArgs e)
        {
            FillCitiesList();
            ddlFilterCity.Focus();
        }

        protected void chkIdC2d_OnCheckedChanged(object sender, EventArgs e)
        {
            //foreach (RepeaterItem item in rtrDeviceList.Items)
            //{
            //    var chkIdC2d = item.FindControl("chkIdC2d") as CheckBox;//CheckBox;

            //    if (chkIdC2d != null && chkIdC2d.Checked)
            //    {
            //        int idC2d = Convert.ToInt32(chkIdC2d.Attributes["Value"]);
            //    }
            //}

            if (sender is CheckBox)
            {
                var chk = sender as CheckBox;

                if (!String.IsNullOrEmpty(chk.Attributes["Value"]))
                {
                    if (chk.Checked)
                    {
                        if (String.IsNullOrEmpty(hfLstCheckedDeviceIds.Value))
                        {
                            hfLstCheckedDeviceIds.Value += chk.Attributes["Value"];
                        }
                        else
                        {
                            hfLstCheckedDeviceIds.Value += String.Format(",{0}", chk.Attributes["Value"]);
                        }
                    }
                    else
                    {
                        hfLstCheckedDeviceIds.Value.Replace(chk.Attributes["Value"], "");
                    }
                }
            }
        }
    }
}