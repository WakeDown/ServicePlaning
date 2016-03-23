using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Threading;
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
        protected const string CameEditorUrl = "~/Service/Editor/Came";

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsServiceTech
        {
            get { return (Page.Master.Master as Site).UserIsServiceTech; }
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
            MainHelper.DdlSetSelectedValue(ref ddlServiceActionType, "1");
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
                if (Id > 0)
                {
                    SetDefaultValues();
                }
                FormDisplay();
                txtClaimSelection.Focus();
            }
            btnSaveAndAddNew.Attributes.Add("onClick", "this.innerText='Идет сохранение...';$(this).removeClass('btn-primary');$(this).addClass('btn-danger');$(this).addClass('disabled');");
            //
        }

        private void FillLists()
        {
            //MainHelper.DdlFill(ref ddlServiceClaim, Db.Db.Srvpl.GetServiceClaimSelectionList(Id), true);//Выбираем только текущую заявку
            MainHelper.DdlFill(ref ddlServiceActionType, Db.Db.Srvpl.GetServiceActionTypeSelectionList(), true);

            string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
            MainHelper.DdlFill(ref ddlServiceEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup), true);


            if (!String.IsNullOrEmpty(Request.QueryString["ctor"]))
            {
                string idContractorStr = Request.QueryString["ctor"].ToString();
                int? idContractor = null;
                int ctr;
                int.TryParse(idContractorStr, out ctr);
                if (ctr > 0)idContractor = ctr;
                FillCtorList(idContractor);
            }
        }

        protected void btnSaveAndAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                int? idContracotr = GetContractorFilterId();
                RedirectWithParams($"ctor={idContracotr}", true, CameEditorUrl);
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
            IIdentity WinId = HttpContext.Current.User.Identity;
            WindowsIdentity wi = (WindowsIdentity)WinId;
            string sid = wi.User.Value;
            serviceCame.Save(sid, UserIsSysAdmin || UserIsServiceTech, serialNum);
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
                CounterColour = MainHelper.TxtGetTextInt32(ref txtCounterColour, true),
                NoPay = MainHelper.RblGetValueBool(ref rblNoPay),
                ProcessEnabled = MainHelper.RblGetValueBool(ref rblProcessEnabled),
                DeviceEnabled = MainHelper.RblGetValueBool(ref rblDeviceEnabled),
                NeedZip = MainHelper.RblGetValueBool(ref rblNeedZip),
                NoCounter = MainHelper.RblGetValueBool(ref rblNoCounter),
                CounterUnavailable = MainHelper.RblGetValueBool(ref rblCounterUnavailable),
                ZipDescr = MainHelper.TxtGetText(ref txtZipDescr),
                DateWorkStart = MainHelper.TxtGetText(ref txtDateWorkStart),
                DateWorkEnd = MainHelper.TxtGetText(ref txtDateWorkEnd)
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
            MainHelper.RblSetValue(ref rblProcessEnabled, serviceCame.ProcessEnabled);
            MainHelper.RblSetValue(ref rblDeviceEnabled, serviceCame.DeviceEnabled);
            MainHelper.RblSetValue(ref rblNeedZip, serviceCame.NeedZip);
            MainHelper.RblSetValue(ref rblNoCounter, serviceCame.NoCounter);
            MainHelper.RblSetValue(ref rblCounterUnavailable, serviceCame.CounterUnavailable);
            MainHelper.RblSetValue(ref rblNoPay, serviceCame.NoPay);
            MainHelper.TxtSetText(ref txtZipDescr, serviceCame.ZipDescr);
            MainHelper.TxtSetText(ref txtDateWorkStart, serviceCame.DateWorkStart);
            MainHelper.TxtSetText(ref txtDateWorkEnd, serviceCame.DateWorkEnd);
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

            script = $"$(\"[timemask = '1']\").mask(\"99:99\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "timeMask", script, true);
        }

        protected void btnClaimSelection_Click(object sender, EventArgs e)
        {
            string serialNum = MainHelper.TxtGetText(ref txtClaimSelection);
            string idContractorStr = MainHelper.DdlGetSelectedValue(ref ddlContractor, true);
            int? idContractor = GetContractorFilterId();
            
            FillClaimList(serialNum, idContractor: idContractor);

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

        protected void FillClaimList(string serialNum = null, int? idServiceCame = null, int? idServiceClaim = null, int? idContractor=null)
        {
            MainHelper.LbFill(ref lbClaim, Db.Db.Srvpl.GeClaimFullNameSelectionList(serialNum, idServiceCame, idServiceClaim, idContractor: idContractor));
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
            txtCounterColour.Focus();
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

        public int? GetContractorFilterId()
        {
            string idContractorStr = MainHelper.DdlGetSelectedValue(ref ddlContractor, true);
            int? idContracotr = null;
            if (idContractorStr != null)
            {
                int ctor;
                int.TryParse(idContractorStr, out ctor);
                if (ctor > 0) idContracotr = ctor;
            }
            return idContracotr;
        }

        protected void btnShowSerialNums_Click(object sender, EventArgs e)
        {
            lbSerialNums.Items.Clear();

            string serial = MainHelper.TxtGetText(ref txtClaimSelection);
            int? idContracotr = GetContractorFilterId();


            if (!String.IsNullOrEmpty(serial))
            {
                lbSerialNums.Visible = true;
                var dt = Db.Db.Srvpl.GetSerialNumList(serial, idContracotr);
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

        protected void rblCounterUnavailable_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormDisplay();
        }

        protected void FormDisplay()
        {
            if (rblProcessEnabled.SelectedIndex >= 0 && rblProcessEnabled.SelectedValue.Equals("0"))
            {
                MainHelper.RblSetValue(ref rblDeviceEnabled, "0");
            }

            if (rblNeedZip.SelectedIndex >= 0 && rblNeedZip.SelectedValue.Equals("1"))
            {
                
                zipDescrContainer.Visible = true;
                refvtxtZipDescr.Enabled = true;
                refvtxtZipDescr.ErrorMessage = "Укажите Спиок ЗИП";
                txtZipDescr.Attributes["placeholder"] = "Укажите спиок ЗИП";
                zipDescrLabel.InnerText = "Спиок ЗИП";
                
            }
            else
            {
                zipDescrContainer.Visible = false;
                refvtxtZipDescr.Enabled = false;
                
            }



            if (rblProcessEnabled.SelectedValue.Equals("0") && rblDeviceEnabled.SelectedValue.Equals("0") &&
                rblNeedZip.SelectedValue.Equals("0"))
            {
                zipDescrContainer.Visible = true;
                refvtxtZipDescr.Enabled = true;
                refvtxtZipDescr.ErrorMessage = "Укажите что случилось";
                txtZipDescr.Attributes["placeholder"] = "Укажите что случилось";
                zipDescrLabel.InnerText = "Что случилось";
            }

            rblCounterUnavailableContainer.Visible = rblNoCounter.SelectedIndex >=0 && rblNoCounter.SelectedValue.Equals("0");
            if (!rblCounterUnavailableContainer.Visible)
            {
                rblCounterUnavailable.SelectedIndex = -1;
                rfvTxtCounter.Enabled = true;
                cvTxtCounter.Enabled = true;
                rfvrblCounterUnavailable.Enabled = true;
            }
            else
            {
                rfvrblCounterUnavailable.Enabled = false;
                rfvTxtCounter.Enabled = false;
                cvTxtCounter.Enabled = false;
            }
                pnlCounters.Visible = rblCounterUnavailableContainer.Visible && rblCounterUnavailable.SelectedIndex >= 0 && rblCounterUnavailable.SelectedValue.Equals("0");

            rfvTxtCounter.Enabled = cvTxtCounter .Enabled= pnlCounters.Visible;

            if (rblCounterUnavailableContainer.Visible && rblCounterUnavailable.SelectedValue.Equals("1"))
            {
                rfvTxtDescr.Enabled = true;
                txtDescr.Attributes["placeholder"] = "Укажите что со счетчиком";
                descrContainer.Visible = true;
            }
            else
            {
                descrContainer.Visible = false;
                rfvTxtDescr.Enabled = false;
            }
        }

        protected void rblNoCounter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormDisplay();
        }

        protected void rblNeedZip_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormDisplay();
        }

        protected void rblProcessEnabled_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormDisplay();
        }
        protected void rblDeviceEnabled_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormDisplay();
        }
        protected void txtContractorSelection_OnTextChanged(object sender, EventArgs e)
        {
            FillCtorList();
        }

        protected void FillCtorList(int? idContractor = null)
        {
            if (idContractor.HasValue)
            {
                MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(idContractor:idContractor.Value), true,
                    MainHelper.ListFirstItemType.SelectAll);
            }
            else
            {
                string text = MainHelper.TxtGetText(ref txtContractorSelection);
                MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text), true,
                    MainHelper.ListFirstItemType.SelectAll);
            }
            if (ddlContractor.Items.Count > 1)
            {
                ddlContractor.SelectedIndex = 1;
            }
            ddlContractor.Focus();
        }
    }
}