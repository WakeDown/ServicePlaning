using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Devices
{
    public partial class Editor : BasePage
    {
        protected string FormTitle;
        protected const string ListUrl = "~/Devices";

        private int Id
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

            if (!IsPostBack)
            {
                FormTitle = "Добавление аппарата";

                SetDefaults();

                if (Id > 0)
                {
                    Device device = new Device(Id);
                    FillFormData(device);
                }
            }

            RegisterStartupScripts();
        }

        private void SetDefaults()
        {
            txtAge.Enabled = txtCounter.Enabled = txtCounterColour.Enabled = txtInstalationDate.Enabled = txtInvNum.Enabled = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        private void FillLists()
        {
            //MainHelper.DdlFill(ref ddlModel, Db.Db.Srvpl.GetDeviceModelSelectionList(), true);
            MainHelper.LbFill(ref lbModel, Db.Db.Srvpl.GetDeviceModelSelectionList());
            MainHelper.ChkListFill(ref cblDeviceOptions, Db.Db.Srvpl.GetDeviceOptionSelectionList());
        }

        protected void btnSave_Click(object sender, EventArgs e)
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

        protected void btnSaveAndAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams("",false);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            Device device = GetFormData();
            device.Save();
            string messageText = String.Format("Сохранение аппарата с серийным номером {0} прошло успешно", device.SerialNum);
            ServerMessageDisplay(new [] { phServerMessage }, messageText);
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
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        private void FormClear()
        {
            //MainHelper.TxtSetEmptyText(ref txtModel);
            //MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlModel);
            MainHelper.LbSetEmptyOrSelectAllSelectedIndex(ref lbModel);
            
            MainHelper.TxtSetEmptyText(ref txtSerialNum);
            
            MainHelper.ChkListSetSelectedValues(ref cblDeviceOptions, null, true, true);
            MainHelper.TxtSetEmptyText(ref txtCounter);
            MainHelper.TxtSetEmptyText(ref txtAge);
            MainHelper.TxtSetEmptyText(ref txtInstalationDate);
        }

        private Device GetFormData()
        {
            Device device = new Device();

            device.Id = Id;
            //device.Model = MainHelper.TxtGetText(ref txtModel);
            //device.IdModel = MainHelper.DdlGetSelectedValueInt(ref ddlModel);
            device.IdModel = MainHelper.LbGetSelectedValueInt(ref lbModel);
            
            device.SerialNum = MainHelper.TxtGetText(ref txtSerialNum);
            device.InvNum = MainHelper.TxtGetText(ref txtInvNum);
            
            device.OptIds = MainHelper.ChkListGetCheckedValuesInt(ref cblDeviceOptions);
            device.Counter = MainHelper.TxtGetTextInt32(ref txtCounter, true);
            device.CounterColour = MainHelper.TxtGetTextInt32(ref txtCounterColour, true);
            device.Age = MainHelper.TxtGetTextInt32(ref txtAge, true, true);
            device.InstalationDate = MainHelper.TxtGetTextDateTime(ref txtInstalationDate, true);
            device.IdCreator = User.Id;

            return device;
        }

        private void FillFormData(Device device)
        {
            FormTitle = Id == 0 ? "Добавление аппарата" : String.Format("Редактирование аппарата №{0}", device.SerialNum);
            //MainHelper.TxtSetText(ref txtModel, device.Model);
            //MainHelper.DdlSetSelectedValue(ref ddlModel, device.IdModel);
            MainHelper.LbSetSelectedValue(ref lbModel, device.IdModel);

            MainHelper.TxtSetText(ref txtSerialNum, device.SerialNum);
            MainHelper.TxtSetText(ref txtInvNum, device.InvNum, !String.IsNullOrEmpty(device.InvNum));
            
            MainHelper.ChkListSetSelectedValues(ref cblDeviceOptions, device.OptIds.Cast<object>().ToArray(), true, true);

            //Если отсутствуют значение то поле неактивно - при сработке джаваскрипта устанавливается значение "неихвестно"
            
            MainHelper.TxtSetText(ref txtCounter, device.Counter, device.Counter != null);
            MainHelper.TxtSetText(ref txtCounterColour, device.CounterColour, device.CounterColour != null);
            //chkNoCounter.Checked = device.Counter != null;

            string age = device.Age == 0 ? "Гарантия" : device.Age.ToString();
            MainHelper.TxtSetText(ref txtAge, age, device.Age != null);
            //chkNoAge.Checked = device.Age != null;
            MainHelper.TxtSetDate(ref txtInstalationDate, device.InstalationDate, device.InstalationDate != null);
            //chkNoInstalationDate.Checked = device.InstalationDate != null;
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            //string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlModel.ClientID, txtModelSelection.ClientID);
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", lbModel.ClientID, txtModelSelection.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterModelListByName", script, true);
            //</Фильтрация списка>

            //<Деактивация поля по галочнке>
            script = String.Format(@"initControlDisableStateElementAndText('{0}', '{1}', '{2}', false, '{3}');", chkNoInstalationDate.ClientID, txtInstalationDate.ClientID, "неизвестно", rfvTxtInstalationDate.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "cdseatInsDate", script, true);

            script = String.Format(@"initControlDisableStateElementAndText('{0}', '{1}', '{2}', false, '{3}');", chkNoCounter.ClientID, txtCounter.ClientID, "неизвестно", rfvTxtCounter.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "cdseatCounter", script, true);

            script = String.Format(@"initControlDisableStateElementAndText('{0}', '{1}', '{2}', false, '{3}');", chkNoCounterColour.ClientID, txtCounterColour.ClientID, "неизвестно", rfvTxtCounterColour.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "cdseatCounterColour", script, true);

            script = String.Format(@"initControlDisableStateElementAndText('{0}', '{1}', '{2}', false, '{3}');", chkNoAge.ClientID, txtAge.ClientID, "неизвестно", rfvTxtAge.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "cdseatAge", script, true);

            script = String.Format(@"initControlDisableStateElementAndTextIfChecked('{0}', '{1}', '{2}', false, '{3}');", chkNoSerialNum.ClientID, txtSerialNum.ClientID, "неизвестно", rfvTxtSerialNum.ClientID);
            script = String.Format(@"initControlDisableStateElementAndTextIfChecked('{0}', '{1}', '{2}', false, '{3}');", chkNoInvNum.ClientID, txtInvNum.ClientID, "неизвестно", rfvTxtInvNum.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "cdseatSerialNum", script, true);
            //</Деактивация поля>
        }
    }
}