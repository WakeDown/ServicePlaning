using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class Came : BasePage
    {
        protected string FormTitle;
        protected const string ListUrl = "~/Service";

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        private int IdClaim
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["clid"], out id);

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

        private void SetDefaultValues()
        {
            MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, User.Id);
            //MainHelper.TxtSetDate(ref txtDateCame, DateTime.Now, true, true);
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FormTitle = "Отметка об обслуживании заявки";

                if (Id > 0)
                {
                    ServiceCame serviceCame = new ServiceCame(Id);
                    FillFormData(serviceCame);
                }
                else if (IdClaim > 0)
                {
                    FillClaimList(null, null, IdClaim);
                    if (lbClaim.Items.Count > 0)
                    {
                        lbClaim.Items[0].Selected = true;
                    }
                }
            }

            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaultValues();
            }

            txtClaimSelection.Focus();
        }

        private void FillLists()
        {
            //MainHelper.DdlFill(ref ddlServiceClaim, Db.Db.Srvpl.GetServiceClaimSelectionList(Id), true);//Выбираем только текущую заявку
            MainHelper.DdlFill(ref ddlServiceActionType, Db.Db.Srvpl.GetServiceActionTypeSelectionList(), true);

            string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true);

        }

        protected void btnSaveAndAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                //RedirectWithParams();
                FormClear();
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void FormClear()
        {
            txtClaimSelection.Text = string.Empty;
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceActionType);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceEngeneer);
            MainHelper.TxtSetEmptyText(ref txtCounter);
            MainHelper.TxtSetEmptyText(ref txtDateCame);
            MainHelper.TxtSetEmptyText(ref txtDescr);
            MainHelper.TxtSetEmptyText(ref txtCounterColour);
            lbClaim.Items.Clear();

            counterNoteMessage.Visible = false;
        }

        private void Save()
        {
            ServiceCame serviceCame = GetFormData();
            string serialNum = MainHelper.TxtGetText(ref txtClaimSelection);
            serviceCame.Save(UserIsSysAdmin, serialNum);
            ServiceClaim serviceClaim = new ServiceClaim(serviceCame.IdServiceClaim);
            string messageText = String.Format("Сохранение отметки об обслуживании заявки № {0} прошло успешно", serviceClaim.Number);
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

        private ServiceCame GetFormData()
        {
            ServiceCame serviceCame = new ServiceCame
            {
                //Id = Id,
                IdServiceClaim = MainHelper.LbGetSelectedValueInt(ref lbClaim),
                IdServiceActionType = MainHelper.DdlGetSelectedValueInt(ref ddlServiceActionType),
                IdServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer),
                DateCame = MainHelper.TxtGetTextDateTime(ref txtDateCame),
                Counter = MainHelper.TxtGetTextInt32(ref txtCounter),
                Descr = MainHelper.TxtGetText(ref txtDescr),
                IdCreator = User.Id,
                CounterColour = MainHelper.TxtGetTextInt32(ref txtCounterColour, true)
            };

            return serviceCame;
        }

        private void FillFormData(ServiceCame serviceCame)
        {
            ServiceClaim serviceClaim = new ServiceClaim(serviceCame.IdServiceClaim);

            FormTitle = String.Format("Отметка об обслуживании заявки №{0}", serviceClaim.Number);

            if (Id > 0)
            {
                FillClaimList(null, serviceCame.Id);
                MainHelper.TxtSetText(ref txtClaimSelection, String.Empty, false);
            }

            MainHelper.LbSetSelectedValue(ref lbClaim, serviceCame.IdServiceClaim);

            SetDateCaberBorder(serviceClaim.PlaningDate.Value);

            MainHelper.DdlSetSelectedValue(ref ddlServiceActionType, serviceCame.IdServiceActionType);
            MainHelper.DdlSetSelectedValue(ref ddlServiceEngeneer, serviceCame.IdServiceEngeneer);
            MainHelper.TxtSetText(ref txtCounter, serviceCame.Counter);
            MainHelper.TxtSetText(ref txtCounterColour, serviceCame.CounterColour);
            MainHelper.TxtSetDate(ref txtDateCame, serviceCame.DateCame);
            MainHelper.TxtSetText(ref txtDescr, serviceClaim.Descr);
        }

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            //<Фильтрация списка по вводимому тексту>
            //string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", lbClaim.ClientID, txtClaimSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListByNumber", script, true);
            //Жуткие тормоза когда большой список


            //script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlDevice.ClientID, txtDeviceSelection.ClientID);

            //ScriptManager.RegisterStartupScript(this, GetType(), "filterDeviceListBySerial", script, true);
            //</Фильтрация списка>

            //<календарь>
            //if (!String.IsNullOrEmpty(hfPlaningDate.Value))
            //{
            //    DateTime planingDate = Convert.ToDateTime(hfPlaningDate.Value);

            //    script = String.Format(@"$('#{0}').datepicker({{ language: 'ru', todayBtn: 'linked', format: 'dd.mm.yy', autoclose: true, startDate: ""{1}"", endDate: ""{2}"" }});", txtDateCame.ClientID, , new DateTime(planingDate.Year, planingDate.Month, DateTime.DaysInMonth(planingDate.Year, planingDate.Month)).ToShortDateString());
            //}
            //else
            //{
            script = String.Format(@"$('#{0}').datepicker({{ language: 'ru', todayBtn: 'linked', format: 'dd.mm.yy', autoclose: true }});", txtDateCame.ClientID);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "datepickerCameActivate", script, true);
            //</календарь>

            script = String.Format(@"if ($('#{0}').val() == '0') {{ return confirm('Вы действительно хотите сохранить 0 в поле счетчик?'); }};", txtCounter.ClientID);
            btnSaveAndAddNew.OnClientClick = script;
        }

        protected void btnClaimSelection_Click(object sender, EventArgs e)
        {
            string serialNum = MainHelper.TxtGetText(ref txtClaimSelection);

            FillClaimList(serialNum);

            if (lbClaim.Items.Count > 0)
            {

                //Устанавливаем на текущий месяц
                foreach (ListItem item in lbClaim.Items)
                {
                    string s = DateTime.Now.ToString("MM.yyyy");

                    if (item.Text.Contains(s))
                    {
                        lbClaim.SelectedValue = item.Value;
                        break;
                    }
                }

                if (lbClaim.SelectedIndex == -1) lbClaim.SelectedIndex = 0;
            }

            lbClaim_OnSelectedIndexChanged(null, null);
            lbClaim.Focus();
        }

        protected void FillClaimList(string serialNum = null, int? idServiceCame = null, int? idServiceClaim = null)
        {
            MainHelper.LbFill(ref lbClaim, Db.Db.Srvpl.GeClaimFullNameSelectionList(serialNum, idServiceCame, idServiceClaim));
        }

        protected void lbClaim_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lbClaim.SelectedValue))
            {
                int claimId = Convert.ToInt32(lbClaim.SelectedValue);
                SetDateCaberBorder(new ServiceClaim(claimId).PlaningDate.Value);

                upDateCame.Update();
            }
        }

        protected void SetDateCaberBorder(DateTime planingDate)
        {
            rvDateCame.Enabled = true;

            rvDateCame.MinimumValue = new DateTime(planingDate.Year, planingDate.Month, 1).ToShortDateString();
            rvDateCame.MaximumValue =
                new DateTime(planingDate.Year, planingDate.Month,
                    DateTime.DaysInMonth(planingDate.Year, planingDate.Month)).ToShortDateString();

            rvDateCame.ErrorMessage = String.Format("Необходимо выбрать дату месяца {0:MMMM}", planingDate);
        }

        protected void txtCounterColour_OnTextChanged(object sender, EventArgs e)
        {
            DoCheckCounter();
            
        }

        private void DoCheckCounter()
        {
            int? counterMustBe;
            bool flag = CheckCounter(out counterMustBe);

            DisplayCounterNote(flag, counterMustBe);
        }

        protected void txtCounter_OnTextChanged(object sender, EventArgs e)
        {
            DoCheckCounter();
        }

        protected void txtDateCame_OnTextChanged(object sender, EventArgs e)
        {
            DoCheckCounter();
        }
        

        private void DisplayCounterNote(bool flag, int? counterMustBe)
        {
            counterNoteMessage.Visible = flag;
            if (flag) counterNoteMessage.InnerText = String.Format("Вы уверены что ввели правильное значение счетчиков? Ожидаемое значение: {0}", counterMustBe.HasValue ? counterMustBe.Value.ToString() :  "не удалось определить");
        }

        private bool CheckCounter(out int? counterMustBe)
        {
            //Если введенное значение счетчиков  превышает значение за месяц более чем на 50 % то true
            bool flag = false;
            counterMustBe = null;

            int? counter = MainHelper.TxtGetTextInt32(ref txtCounter, true);
            int? counterColor = MainHelper.TxtGetTextInt32(ref txtCounterColour, true);
            DateTime? dateCame = MainHelper.TxtGetTextDateTime(ref txtDateCame, true);

            if ((counter.HasValue || counterColor.HasValue) && dateCame.HasValue)
            {
                int totalCounter = counter ?? 0;
                totalCounter += counterColor ?? 0;
                int idServiceClaim = MainHelper.LbGetSelectedValueInt(ref lbClaim);

                var dt = Db.Db.Srvpl.CheckDeviceTotalCounterIsNotTooBig(idServiceClaim, totalCounter, dateCame.Value);

                if (dt.Rows.Count > 0)
                {
                    flag = dt.Rows[0]["result"].ToString().Equals("1");
                    counterMustBe = Db.Db.GetValueIntOrNull(dt.Rows[0]["counter_must_be"].ToString());
                }
            }

            return flag;
        }

        protected void btnShowSerialNums_Click(object sender, EventArgs e)
        {
            lbSerialNums.Items.Clear();

            string serial = MainHelper.TxtGetText(ref txtClaimSelection);
            if (!String.IsNullOrEmpty(serial))
            {
                lbSerialNums.Visible = true;
                var dt = Db.Db.Srvpl.GetSerialNumList(serial);
                if (dt.Rows.Count > 0)
                {
                    MainHelper.LbFill(ref lbSerialNums, dt);
                    //lbSerialNums.DataSource = dt;
                    //lbSerialNums.DataTextField = "name";
                    //lbSerialNums.DataBind();
                    
                }
                else
                {
                    lbSerialNums.Items.Add(new ListItem(){Value = "не найдено соответствий"});
                }
            }
            else
            {
                lbSerialNums.Visible = false;
                
                //lbSerialNums.DataSource = null;
                //lbSerialNums.DataBind();
            }
        }

        protected void lbSerialNums_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string serial = lbSerialNums.SelectedItem.Text;
            txtClaimSelection.Text = serial;
            FillClaimList(serial);
        }
    }
}