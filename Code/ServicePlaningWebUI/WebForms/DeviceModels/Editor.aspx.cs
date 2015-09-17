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

namespace ServicePlaningWebUI.WebForms.DeviceModels
{
    public partial class Editor : BaseFilteredPage
    {
        /*queryStringFilterParams
         name - Модель
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("name", txtFilterModel));

            BtnSearchClientId = btnSearch.ClientID;
        }

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
                FormTitle = "Добавление модели";

                if (Id > 0)
                {
                    DeviceModel device = new DeviceModel(Id);
                    FillFormData(device);
                }
            }
        }

        private void FillLists()
        {
            MainHelper.DdlFill(ref ddlDeviceType, Db.Db.Srvpl.GetDeviceTypeSelectionList(), true);
            MainHelper.DdlFill(ref ddlDeviceImprint, Db.Db.Srvpl.GetDeviceImprintSelectionList(), true);
            MainHelper.DdlFill(ref ddlPrintType, Db.Db.Srvpl.GetPrintTypeSelectionList(), true);
            MainHelper.DdlFill(ref ddlCartridgeType, Db.Db.Srvpl.GetCartridgeTypeSelectionList(), true);
            MainHelper.DdlFill(ref ddlClassifier, ClassifierCaterory.GetLowerList(), true, MainHelper.ListFirstItemType.Nullable, "Id", "Number");
            //MainHelper.DdlFill(ref ddlDeviceImprint, Db.Db.Srvpl.GetDeviceImprintSelectionList(), true);
            //MainHelper.DdlFill(ref ddlPrintType, Db.Db.Srvpl.GetPrintTypeSelectionList(), true);
            //MainHelper.DdlFill(ref ddlCartridgeType, Db.Db.Srvpl.GetCartridgeTypeSelectionList(), true);
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

        private void Save()
        {
            DeviceModel deviceModel = GetFormData();
            deviceModel.Save();
            string messageText = String.Format("Сохранение модели {0} прошло успешно", deviceModel.Name);
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
            MainHelper.TxtSetEmptyText(ref txtName);
            MainHelper.TxtSetEmptyText(ref txtVendor);
            MainHelper.TxtSetEmptyText(ref txtSpeed);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlDeviceImprint);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlPrintType);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlCartridgeType);
        }

        private DeviceModel GetFormData()
        {
            DeviceModel deviceModel = new DeviceModel();

            deviceModel.Id = Id;
            deviceModel.IdDeviceType = MainHelper.DdlGetSelectedValueInt(ref ddlDeviceType);
            deviceModel.Name = MainHelper.TxtGetText(ref txtName);
            deviceModel.Vendor = MainHelper.TxtGetText(ref txtVendor);
            //deviceModel.Nickname = MainHelper.TxtGetText(ref txtNickname);
            deviceModel.Speed = MainHelper.TxtGetTextDecimal(ref txtSpeed, true);
            deviceModel.IdDeviceImprint = MainHelper.DdlGetSelectedValueInt(ref ddlDeviceImprint, true);
            deviceModel.IdPrintType = MainHelper.DdlGetSelectedValueInt(ref ddlPrintType, true);
            deviceModel.IdCartridgeType = MainHelper.DdlGetSelectedValueInt(ref ddlCartridgeType, true);
            deviceModel.IdCreator = User.Id;
            deviceModel.MaxVolume = MainHelper.TxtGetTextInt32(ref txtMaxVolume,true);
            deviceModel.IdClassifierCategory = MainHelper.DdlGetSelectedValueInt(ref ddlClassifier, true);

            return deviceModel;
        }

        private void FillFormData(DeviceModel deviceModel)
        {
            FormTitle = Id == 0 ? "Добавление модели" : String.Format("Редактирование модели {0}", deviceModel.Name);
            MainHelper.DdlSetSelectedValue(ref ddlDeviceType, deviceModel.IdDeviceType);
            MainHelper.TxtSetText(ref txtName, deviceModel.Name);
            MainHelper.TxtSetText(ref txtVendor, deviceModel.Vendor);
            //MainHelper.TxtSetText(ref txtNickname, deviceModel.Nickname);
            MainHelper.TxtSetText(ref txtSpeed, deviceModel.Speed);
            MainHelper.DdlSetSelectedValue(ref ddlDeviceImprint, deviceModel.IdDeviceImprint);
            MainHelper.DdlSetSelectedValue(ref ddlPrintType, deviceModel.IdPrintType);
            MainHelper.DdlSetSelectedValue(ref ddlCartridgeType, deviceModel.IdCartridgeType);
            MainHelper.TxtSetText(ref txtMaxVolume, deviceModel.MaxVolume);
        }

        //======

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new DeviceModel().Delete(id);
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

        protected void sdsDeviceModelList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }
    }
}