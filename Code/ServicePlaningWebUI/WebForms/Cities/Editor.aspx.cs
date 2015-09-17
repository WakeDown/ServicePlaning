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

namespace ServicePlaningWebUI.WebForms.Cities
{
    public partial class Editor : BaseFilteredPage
    {
        /*queryStringFilterParams
         name - Город
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("name", txtFilterCity));
            FilterLinks.Add(new FilterLink("reg", txtFilterRegion));
            FilterLinks.Add(new FilterLink("area", txtFilterArea));
            FilterLinks.Add(new FilterLink("loc", txtFilterLocality));

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
                FormTitle = "Добавление города";

                if (Id > 0)
                {
                    City city = new City(Id);
                    FillFormData(city);
                }
            }
        }

        private void FillLists()
        {
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
            City city = GetFormData();
            city.Save();
            string messageText = String.Format("Сохранение города {0} прошло успешно", city.Name);
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
        }

        private City GetFormData()
        {
            City city = new City();

            city.Id = Id;
            city.Region = MainHelper.TxtGetText(ref txtRegion);
            city.Area = MainHelper.TxtGetText(ref txtArea);
            city.Name = MainHelper.TxtGetText(ref txtName);
            city.Locality = MainHelper.TxtGetText(ref txtLocality);
            city.Coord = MainHelper.TxtGetText(ref txtCoord);

            return city;
        }

        private void FillFormData(City city)
        {
            FormTitle = Id == 0 ? "Добавление города" : String.Format("Редактирование города {0}", city.Name);
            MainHelper.TxtSetText(ref txtRegion, city.Region);
            MainHelper.TxtSetText(ref txtArea, city.Area);
            MainHelper.TxtSetText(ref txtName, city.Name);
            MainHelper.TxtSetText(ref txtLocality, city.Locality);
            MainHelper.TxtSetText(ref txtCoord, city.Coord);
        }

        //======

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new City().Delete(id);
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