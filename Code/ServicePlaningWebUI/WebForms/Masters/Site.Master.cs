using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;

namespace ServicePlaningWebUI.WebForms.Masters
{
    public partial class Site : MasterPage
    {
        const string vskUser = "vskUser";
        private string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        string serviceTechRightGroupSid = ConfigurationManager.AppSettings["serviceTechRightGroupSid"];
        string dsuPlanAccessRightGroup = ConfigurationManager.AppSettings["dsuPlanAccessRightGroup"];
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string dsuPlanReportsRightGroup = ConfigurationManager.AppSettings["dsuPlanReportsRightGroup"];


        private string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";
        private string serviceAdminRightGroupVSKey = "serviceAdminRightGroupVSKey";
        private string serviceEngeneersRightGroupVSKey = "serviceEngeneersRightGroupVSKey";
        private string serviceTechRightGroupSidVSKey = "serviceTechRightGroupSidVSKey";
        private string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        private string dsuPlanReportsRightGroupVSKey = "dsuPlanReportsRightGroupVSKey";


        public bool UserIsReport
        {
            get { return (bool)ViewState[dsuPlanReportsRightGroupVSKey]; }
            set { ViewState[dsuPlanReportsRightGroupVSKey] = value; }
        }

        public bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        public bool UserIsServiceAdmin
        {
            get { return (bool)ViewState[serviceAdminRightGroupVSKey]; }
            set { ViewState[serviceAdminRightGroupVSKey] = value; }
        }

        public bool UserIsServiceEngeneer
        {
            get { return (bool)ViewState[serviceEngeneersRightGroupVSKey]; }
            set { ViewState[serviceEngeneersRightGroupVSKey] = value; }
        }

        public bool UserIsServiceTech
        {
            get { return (bool)ViewState[serviceTechRightGroupSidVSKey]; }
            set { ViewState[serviceTechRightGroupSidVSKey] = value; }
        }

        public bool UserIsServiceManager
        {
            get { return (bool)ViewState[serviceManagerRightGroupVSKey]; }
            set { ViewState[serviceManagerRightGroupVSKey] = value; }
        }

        public User User { get { return (User)ViewState[vskUser] ?? new User(); } set { ViewState[vskUser] = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currLogin = User.Login;

                //Проверка доступа в программу
                bool userCanAccess = Db.Db.Users.CheckUserRights(currLogin, dsuPlanAccessRightGroup);
                if (!userCanAccess) Response.Redirect(FriendlyUrl.Href("~/Error"));

                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
                UserIsServiceAdmin = Db.Db.Users.CheckUserRights(User.Login, serviceAdminRightGroup);
                UserIsServiceEngeneer = Db.Db.Users.CheckUserRights(User.Login, serviceEngeneersRightGroup);
                UserIsServiceManager = Db.Db.Users.CheckUserRights(User.Login, serviceManagerRightGroup);
                UserIsReport = Db.Db.Users.CheckUserRights(User.Login, dsuPlanReportsRightGroup);
                UserIsServiceTech = Db.Db.Users.CheckUserRights(User.Login, groupSid:serviceTechRightGroupSid);

                if (!UserIsServiceEngeneer && !UserIsServiceManager && !UserIsSysAdmin && !UserIsServiceAdmin && !UserIsReport)
                {
                    Response.Redirect(FriendlyUrl.Href("~/Error"));
                }

                LoadFormSettings();
                SetUserName();

                //Автопереход для инженеров на страницу Отчета
                if (!UserIsSysAdmin && !UserIsServiceTech && (UserIsServiceEngeneer /*|| userIsSysAdmin*/) && Request.Path.Equals("/"))
                {
                    Response.Redirect(FriendlyUrl.Href("~/Reports/PlanExecute"));
                }

                if (!UserIsSysAdmin && !UserIsServiceTech && UserIsServiceManager && !Request.Path.Equals(FriendlyUrl.Href("~/Reports/PlanExecute")))
                {
                    Response.Redirect(FriendlyUrl.Href("~/Reports/PlanExecute"));
                }

                if (!UserIsSysAdmin && !UserIsServiceTech && UserIsReport && !Request.Path.Equals(FriendlyUrl.Href("~/Reports/PlanExecute")) &&
                    !Request.Path.Equals(FriendlyUrl.Href("~/Reports/Payment")) && !Request.Path.Equals(FriendlyUrl.Href("~/Reports/Counters")) && !Request.Path.Equals(FriendlyUrl.Href("~/Reports/CountersDetail")))
                {
                    Response.Redirect(FriendlyUrl.Href("~/Reports/PlanExecute"));
                }
            }

            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayFormParts();
            }

            pnlNavbar.Visible = Request.QueryString["hidenav"] == null;
        }

        private void DisplayFormParts()
        {
            if (UserIsSysAdmin)
            {
                liSettings.Visible = liPayment.Visible = liReports.Visible = liPlanExec.Visible = liCounters.Visible = liContract2device.Visible = true;
            }

            if (UserIsSysAdmin || UserIsServiceAdmin)
            {
                liReports.Visible = liPlanExec.Visible = liService.Visible = liContracts.Visible = liDevices.Visible = true;
            }

            if (UserIsSysAdmin || UserIsServiceEngeneer)
            {
                liReports.Visible = liPlanExec.Visible = true;
            }

            if (UserIsReport)
            {
                liReports.Visible = liPayment.Visible = liPlanExec.Visible = liCounters.Visible = true;
            }

            if (UserIsServiceTech)
            {
                liSettings.Visible = liCounters.Visible = liContract2device.Visible = true;
            }
        }

        private void SetUserName()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string curName = User.DisplayName;//Page.User.Identity.Name;
                LoginName.FormatString = curName;
            }
        }

        private void LoadFormSettings()
        {
            aServiceDesk.HRef = WebConfigurationManager.AppSettings["serviceDeskAddress"];
            //aReports.HRef = WebConfigurationManager.AppSettings["reportsUrl"];
        }

        private void RegisterStartupScripts()
        {
            ScriptManager.ScriptResourceMapping.AddDefinition(
        "jquery",
        new ScriptResourceDefinition
        {
            Path = "~/jquery-2.1.0.min.js",
            DebugPath = "~/jquery-2.1.0.js",
            LoadSuccessExpression = "jQuery"
        });

            //--Текущая вкладка меню
            string script = @"$(document).ready(function () {
                var url = window.location;
                $('ul.nav').find('.active').removeClass('active');
                $('ul.nav li a').each(function () {
                    if (this.href == url) {
                        $(this).parent().addClass('active');
                    }
                }); 
            });";

            ScriptManager.RegisterStartupScript(this, GetType(), "navCurrentTab", script, true);

            //====/>

            //--Включаем datepicker.js если атрибут date не поддерживается в текущем браузере
            script = @"$('.datepicker-btn').datepicker({ language: 'ru', todayBtn: 'linked', format: 'dd.mm.yyyy', autoclose: true });";

            ScriptManager.RegisterStartupScript(this, GetType(), "datepickerActivate", script, true);

            script = @"$('.datepicker-btn-month').datepicker({ language: 'ru', todayBtn: 'linked', minViewMode: 1, format: 'mm.yyyy', autoclose: true });";

            ScriptManager.RegisterStartupScript(this, GetType(), "datepickerMonthsActivate", script, true);

            //====/>

            //--Активируем подсказки
            script = @"$('[data-toggle=tooltip]').tooltip();";

            ScriptManager.RegisterStartupScript(this, GetType(), "tooltipActivate", script, true);
            //====/>

            //--Убираем автоподсказки для текстовых полей подсказки
            script = @"$(document).ready(function(){$(document).on('focus', ':input', function(){$( this ).attr( 'autocomplete', 'off' );});});";

            ScriptManager.RegisterStartupScript(this, GetType(), "tooltipAutocompleteOff", script, true);
            //====/>


            //--Активируем логгер
            //            script = @"var log = log4javascript.getDefaultLogger(); $('a').on('click', function() { var actn = new Object();
            //actn.id = $(this).prop('id');	
            //actn.href = $(this).prop('href');
            //	
            //	log.info(actn); });";

            //            ScriptManager.RegisterStartupScript(this, GetType(), "logger", script, true);
            //====/>

            //--Снежинки
            var now = DateTime.Now;
            if ((now.Month == 12 && now.Day > 15) || (now.Month == 1 && now.Day < 20)) ScriptManager.RegisterClientScriptInclude(this, GetType(), "happyNewYear", "http://www.fortress-design.com/js/snow-fall.js");
            //====/>
            //<script src="http://www.fortress-design.com/js/snow-fall.js" type="text/javascript"></script>
        }

    }
}