using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.Models;

namespace ServicePlaningWebUI.WebForms.Addresses
{
    public partial class Editor : BaseFilteredPage
    {
        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("name", txtFilterName));
            FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "100"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormTitle;
        protected const string ListUrl = "~/Contracts";

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }


        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FormTitle = "Добавление адреса";

                if (Id > 0)
                {
                    Models.Address address = new Models.Address(Id);
                    FillFormData(address);
                }
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
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            Models.Address address = GetFormData();
            address.Save();
            string messageText = String.Format("Сохранение адреса {0} прошло успешно", address.Name);
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

        private void FormClear()
        {
            MainHelper.TxtSetEmptyText(ref txtName);
        }

        private Models.Address GetFormData()
        {
            Models.Address address = new Models.Address();

            address.Id = Id;
            address.Name = MainHelper.TxtGetText(ref txtName);
            address.idCreator = User.Id;

            return address;
        }

        private void FillFormData(Models.Address address)
        {
            FormTitle = Id == 0 ? "Добавление адреса" : String.Format("Редактирование адреса {0}", address.Name);
            MainHelper.TxtSetText(ref txtName, address.Name);
        }

        //======

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new Models.Address().Delete(id);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
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