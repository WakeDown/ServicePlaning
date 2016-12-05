using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class Editor : BasePage
    {
        protected string FormTitle;


        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        protected bool IsProlong
        {
            get
            {
                return Request.QueryString["prng"] != null && Request.QueryString["prng"] == "1";
            }
        }

        //protected string c2dFormUrl;

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
            //c2dFormUrl = "~/Contracts/Devices/Editor";

            if (!IsPostBack)
            {
                FormTitle = "Добавление договора";

                if (Id > 0)
                {
                    Contract contract = new Contract(Id);
                    FillFormData(contract);
                }
            }

            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool isEdit = Id > 0;

            ddlContractStatus.Enabled = !isEdit;
        }

        private void FillLists()
        {
            

            MainHelper.DdlFill(ref ddlContractTypes, Db.Db.Srvpl.GetContractTypeSelectionList(), true);
            //MainHelper.DdlFill(ref ddlServceTypes, Db.Db.Srvpl.GetServiceTypeSelectionList(), true);
            //MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(), true);
            MainHelper.DdlFill(ref ddlContractStatus, Db.Db.Srvpl.GetContractStatusSelectionList(), true);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(), true);
            MainHelper.DdlFill(ref ddlContractZipState, Db.Db.Srvpl.GetContractZipStateSelectionList(), true);
            MainHelper.RblFill(ref rblPriceDiscount, Db.Db.Srvpl.GetContractPriceDiscountSelectionList());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save();
                string queryParams = String.Format("id={0}", id);
                RedirectWithParams(queryParams);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSaveAndBack_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save();
                //string queryParams = String.Format("id={0}", id);
                string url = FriendlyUrl.Href("~/Contracts");
                RedirectWithParams(string.Empty, false, url);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private int Save(bool devicesProlong = false)
        {
            Contract contract = GetFormData();
            contract.Save();
            int id = contract.Id;

            if (devicesProlong)
            {
                contract.DevicesProlong();
            }

            string messageText = String.Format("Сохранение договора №{0} прошло успешно", contract.Number);
            ServerMessageDisplay(new [] { phServerMessage }, messageText);
            return id;
        }

        private void FormClear()
        {
            MainHelper.TxtSetEmptyText(ref txtNumber);
            MainHelper.TxtSetEmptyText(ref txtPrice);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlContractTypes);
            //MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServceTypes);
            MainHelper.TxtSetEmptyText(ref txtContractorInn);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlContractor);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlContractStatus);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlManager);
            MainHelper.TxtSetEmptyText(ref txtDateBegin);
            MainHelper.TxtSetEmptyText(ref txtDateEnd);
        }

        //Пролонгация договора
        private int Copy()
        {
            Contract contract = GetFormData();

            contract.IdContractProlong = contract.Id;
            contract.Id = -1;
            contract.Number = null;
            contract.DateBegin = contract.DateEnd = null;
            contract.Price = null;

            contract.Save();
            int id = contract.Id;
            string messageText = String.Format("Сохранение договора №{0} прошло успешно", contract.Number);
            ServerMessageDisplay(new[] { phServerMessage }, messageText);
            return id;
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Copy();
                RedirectWithParams(String.Format("id={0}&prng=1", id), false);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        

        protected void btnSaveAndAddDevices_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save();
                Redirect2DevicesEditor(id);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }

            //string url = GetDevicesListUrl(Id);
            //Response.Redirect(url);
        }

        protected void btnSaveAndAddSpecPrice_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save();
                Redirect2SpecPriceEditor(id);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        private void Redirect2SpecPriceEditor(int id)
        {
            string url = "~/Contracts/SpecPrice";
            RedirectWithParams("id=" + id, false, url);
        }

        private void Redirect2DevicesEditor(int id)
        {
            string url = "~/Contracts/Devices/Editor";
            RedirectWithParams("id=" + id, false, url);
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
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnProlong_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save(true);
                Redirect2DevicesEditor(id);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private Contract GetFormData()
        {
            Contract contract = new Contract();

            contract.Id = Id;
            //contract.IdContractProlong = 
            contract.Number = MainHelper.TxtGetText(ref txtNumber);
            contract.Price = MainHelper.TxtGetTextDecimal(ref txtPrice);
           // contract.IdServiceType = MainHelper.DdlGetSelectedValueInt(ref ddlServceTypes);
            contract.IdContractType = MainHelper.DdlGetSelectedValueInt(ref ddlContractTypes);
            contract.IdContractor = MainHelper.DdlGetSelectedValueInt(ref ddlContractor);
            contract.IdContractStatus = MainHelper.DdlGetSelectedValueInt(ref ddlContractStatus);
            contract.IdManager = MainHelper.DdlGetSelectedValueInt(ref ddlManager);
            contract.DateBegin = MainHelper.TxtGetTextDateTime(ref txtDateBegin);
            contract.DateEnd = MainHelper.TxtGetTextDateTime(ref txtDateEnd);
            contract.IdCreator = User.Id;
            contract.IdZipState = MainHelper.DdlGetSelectedValueInt(ref ddlContractZipState);
            contract.Note = MainHelper.TxtGetText(ref txtNote);
            contract.HandlingDevices = MainHelper.TxtGetTextInt32(ref txtHandlingDevices, true);
            contract.ClientSdNumRequired = MainHelper.RblGetValueBool(ref rblClientSdNumRequired, true);
            contract.Sla1 = MainHelper.TxtGetTextInt32(ref txtSla1, true);
            contract.Sla2 = MainHelper.TxtGetTextInt32(ref txtSla2, true);
            contract.Sla3 = MainHelper.TxtGetTextInt32(ref txtSla3, true);
            contract.Sla4 = MainHelper.TxtGetTextInt32(ref txtSla4, true);

            return contract;
        }

        private void FillFormData(Contract contract)
        {
            bool isEdit = Id > 0;

            FormTitle = !isEdit ? "Добавление договора" : String.Format("Редактирование договора №{0}", contract.Number);
            pnlDevicesListWarning.Visible = !isEdit;
            btnDeactivate.Visible = btnPause.Visible = btnEnable.Visible = isEdit;
            MainHelper.TxtSetText(ref txtNumber, contract.Number);
            MainHelper.TxtSetText(ref txtPrice, contract.Price);
            MainHelper.DdlSetSelectedValue(ref ddlContractTypes, contract.IdContractType);
            //MainHelper.DdlSetSelectedValue(ref ddlServceTypes, contract.IdServiceType);

            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(null, contract.IdContractor ?? -1));
            MainHelper.DdlSetSelectedValue(ref ddlContractor, contract.IdContractor);
            MainHelper.DdlSetSelectedValue(ref ddlContractStatus, contract.IdContractStatus);
            MainHelper.DdlSetSelectedValue(ref ddlManager, contract.IdManager);
            MainHelper.TxtSetDate(ref txtDateBegin, contract.DateBegin, IsProlong);//Период действия договора нельзя редактировать так как устройства могут быть привязаны на заданный период действия
            MainHelper.TxtSetDate(ref txtDateEnd, contract.DateEnd, IsProlong);
            MainHelper.DdlSetSelectedValue(ref ddlContractZipState, contract.IdZipState);
            MainHelper.TxtSetText(ref txtNote, contract.Note);
            MainHelper.TxtSetText(ref txtHandlingDevices, contract.HandlingDevices);
            MainHelper.RblSetValue(ref rblClientSdNumRequired, contract.ClientSdNumRequired);
            MainHelper.TxtSetText(ref txtSla1, contract.Sla1);
            MainHelper.TxtSetText(ref txtSla2, contract.Sla2);
            MainHelper.TxtSetText(ref txtSla3, contract.Sla3);
            MainHelper.TxtSetText(ref txtSla4, contract.Sla4);

            btnProlong.Visible = IsProlong;
            btnCopy.Visible = !IsProlong;

            btnContractPeriodReduction.Enabled = !contract.PeriodReduction;
            
            if (IsProlong)
            {
                btnProlong.Text = "Сохранить и скопировать аппараты с договора №" +
                                  (new Contract(contract.IdContractProlong.Value).Number);
            }
        }

        protected void txtContractorInn_OnTextChanged(object sender, EventArgs e)
        {
            string text = MainHelper.TxtGetText(ref txtContractorInn);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text));
            ddlContractor.Focus();
        }

        //protected string GetDevicesListUrl(int id)
        //{
        //    string url = "~/Contracts/Devices/Editor";

        //    if (id != null)
        //    { url = RedirectWithParams(String.Format("id={0}", id),); }

        //    return url;
        //}

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorInn.ClientID);
            //Очень долго отрабатывает на слабых компах, решено заменить на серверный аналог
            //ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>
        }

        protected void btnContractPeriodReduction_OnClick(object sender, EventArgs e)
        {
            try
            {
            int idContract = Id;
            int idCreator = User.Id;

            Contract.SetPeriodReduction(idContract, idCreator);
            RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                int idContract = Id;
                int idCreator = User.Id;

                Contract.Deactivate(idContract, idCreator);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                int idContract = Id;
                int idCreator = User.Id;

                Contract.Pause(idContract, idCreator);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            try
            {
                int idContract = Id;
                int idCreator = User.Id;

                Contract.Enable(idContract, idCreator);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }
    }
}