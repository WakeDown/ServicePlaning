using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class Payment1 : BaseFilteredPage
    {
        private string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        private string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];

        protected bool UserIsServiceEngeneer
        {
            get { return (Page.Master.Master as Site).UserIsServiceEngeneer; }
        }

        protected bool UserIsServiceAdmin
        {
            get { return (Page.Master.Master as Site).UserIsServiceAdmin; }
        }

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsReport
        {
            get { return (Page.Master.Master as Site).UserIsReport; }
        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("rep", rblReportType, "eng"));
            FilterLinks.Add(new FilterLink("eng", ddlServiceEngeneer, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin, User.Id.ToString()));
            //FilterLinks.Add(new FilterLink("dst", txtDateClaimBegin));
            //FilterLinks.Add(new FilterLink("den", txtDateClaimEnd));
            FilterLinks.Add(new FilterLink("mth", txtDateMonth, DateTime.Now.ToString("MM.yyyy")));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FillFilterLists();
            }

            SetActiveView();
        }

        private void SetActiveView()
        {
            View actView;

            switch (rblReportType.SelectedValue)
            {
                //case "dev":
                //    actView = vReportDev;
                //    break;
                case "eng":
                    actView = vReportEng;
                    break;
                case "servAdm":
                    actView = vReportServAdm;
                    break;
                default:
                    if (UserIsServiceAdmin) { actView = vReportEng; }
                    else if (UserIsServiceEngeneer) { actView = vReportServAdm; }
                    else { actView = vReportEng; }
                    break;
            }

            mvReports.SetActiveView(actView);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetDefaults();

            if (!UserIsSysAdmin && !UserIsReport)
            {
                Response.Redirect(FriendlyUrl.Href("~/Error"));
            }
        }


        private void SetDefaults()
        {
            if (!IsPostBack)
            {
                sdsServiceAdminsPaymentList.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
            }

            if (UserIsServiceEngeneer)
            {
                MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, User.Id, false);
                sdsListDev.SelectParameters["id_service_engeneer"].DefaultValue = User.Id.ToString();
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }


        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        private void FillFilterLists()
        {

            //MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void tblServiceAdminsPaymentList_DataBound(object sender, EventArgs e)
        {
            SetCountServiceAdminsPayment();
        }

        private void SetCountServiceAdminsPayment(int count = 0)
        {
            lRowsCountDev.Text = count.ToString();

            DataView dv = (DataView)sdsServiceAdminsPaymentList.Select(DataSourceSelectArguments.Empty);
            lRowsCountSa.Text = dv.Count.ToString();

            string sumCountIn = String.Format("{0:N2}", dv.Table.Compute("Sum(payment)", ""));
            if (string.IsNullOrEmpty(sumCountIn)) sumCountIn = 0.ToString();
            lSummCountSa.Text = sumCountIn;


            //string device_count = dv.Table.Compute("Sum(device_count)", "").ToString();
            string cames_count = dv.Table.Compute("Sum(cames_count)", "").ToString();
            string payment = dv.Table.Compute("Sum(payment)", "").ToString();
            double dblPayment = !String.IsNullOrEmpty(payment) ? Convert.ToDouble(payment) : 0;

            //tblServiceAdminsPaymentList.Columns[2].FooterText = String.Format("{0}", device_count);
            tblServiceAdminsPaymentList.Columns[2].FooterText = String.Format("{0}", cames_count);
            tblServiceAdminsPaymentList.Columns[4].FooterText = String.Format("{0:C}", dblPayment);
        }

        protected void tblListDev_DataBound(object sender, EventArgs e)
        {
            SetCountDev();
        }

        private void SetCountDev(int count = 0)
        {
            lRowsCountDev.Text = count.ToString();

            DataView dv = (DataView)sdsListDev.Select(DataSourceSelectArguments.Empty);
            lRowsCountDev.Text = dv.Count.ToString();

            string sumCountIn = String.Format("{0:N2}", dv.Table.Compute("Sum(payment)", ""));
            if (string.IsNullOrEmpty(sumCountIn)) sumCountIn = 0.ToString();
            lSummCountDev.Text = sumCountIn;
        }

        protected void rblReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}