using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Service
{
    public partial class CameScan : BasePage
    {
        protected string FormTitle;
        protected const string ListUrl = "~/Service";

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
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
                ClearScanViewSelect();
                tblAktScanList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void FormClear()
        {
            txtClaimSelection.Text = string.Empty;
            txtClaimSelection_TextChanged(txtClaimSelection, new EventArgs());
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceActionType);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceEngeneer);
            MainHelper.TxtSetEmptyText(ref txtCounter);
            MainHelper.TxtSetEmptyText(ref txtDateCame);
            //MainHelper.TxtSetEmptyText(ref txtDescr);
            MainHelper.TxtSetEmptyText(ref txtCounterColour);

        }

        private void Save()
        {
            ServiceCame serviceCame = GetFormData();
            string serialNum = MainHelper.TxtGetText(ref txtClaimSelection);
            IIdentity WinId = HttpContext.Current.User.Identity;
            WindowsIdentity wi = (WindowsIdentity)WinId;
            string sid = wi.User.Value;
            serviceCame.Save(sid, UserIsSysAdmin, serialNum);
            serviceCame.Save(sid, UserIsSysAdmin);
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
            int idAktScan;

            int.TryParse(imgAktScan.Attributes["IdAktScan"].ToString(), out idAktScan);

            ServiceCame serviceCame = new ServiceCame
            {
                //Id = Id,
                IdServiceClaim = MainHelper.LbGetSelectedValueInt(ref lbClaim),
                IdServiceActionType = MainHelper.DdlGetSelectedValueInt(ref ddlServiceActionType),
                IdServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlServiceEngeneer),
                DateCame = MainHelper.TxtGetTextDateTime(ref txtDateCame),
                Counter = MainHelper.TxtGetTextInt32(ref txtCounter),
                //Descr = MainHelper.TxtGetText(ref txtDescr),
                IdCreator = User.Id,
                CounterColour = MainHelper.TxtGetTextInt32(ref txtCounterColour, true),
                IdAktScan = idAktScan
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
            //MainHelper.TxtSetText(ref txtDescr, serviceClaim.Descr);
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

        protected void txtClaimSelection_TextChanged(object sender, EventArgs e)
        {
            string serialNum = MainHelper.TxtGetText(ref txtClaimSelection);
            if (!String.IsNullOrEmpty(serialNum))
            {
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

                if (lbClaim.Items.Count > 0) {lbClaim.Focus();}
                else
                {
                    lbClaim.Items.Add(new ListItem("нет данных"));
                }
            }
            else
            {
                lbClaim.Items.Clear();
                lbClaim.DataSource = null;
                lbClaim.DataBind();
            }

            //upClaimList.Update();
        }

        protected void FillClaimList(string serialNum = null, int? idServiceCame = null)
        {
            
                MainHelper.LbFill(ref lbClaim, Db.Db.Srvpl.GeClaimFullNameSelectionList(serialNum, idServiceCame));
            
        }

        protected void lbClaim_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbClaim.Items.Count > 0)
            {
                int claimId = Convert.ToInt32(lbClaim.SelectedValue);
                SetDateCaberBorder(new ServiceClaim(claimId).PlaningDate.Value);
            }
                upDateCame.Update();
            
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


        protected void btnAktScanFileDelete_OnClick(object sender, EventArgs e)
        {
            int idAktScan = Convert.ToInt32((sender as WebControl).Attributes["IdAktScan"]);

            new AktScan().Delete(idAktScan, User.Id);
            RedirectWithParams();
            //ClearScanViewSelect();
            //sdsAktScanList.DataBind();
            //tblAktScanList.DataBind();
        }

        protected void btnAktScanFile_OnClick(object sender, EventArgs e)
        {

            int idAktScan = Convert.ToInt32((sender as LinkButton).Attributes["IdAktScan"]);

            SetFileScanSelected(idAktScan);

            AktScan akt = new AktScan(idAktScan);

            NetworkCredential nc = GetNetCredential4Scan();

            using (
                WindowsImpersonationContextFacade impersonationContext =
                    new WindowsImpersonationContextFacade(nc))
            {
                string archivePath = ConfigurationManager.AppSettings["documentAktScanPath"];
                string path = Path.Combine(archivePath, akt.FileName);

                ClearScanViewSelect();
                FormClear();

                if (File.Exists(path))
                {
                    lblImgNote.Text = akt.Name;
                    string url = String.Format("{0}?path={1}", ResolveClientUrl("~/Handlers/ShowImage.ashx"), path);
                    imgAktScan.ImageUrl = url;
                    imgAktScan.CssClass = "imgNormal";

                    imgAktScan.Attributes["IdAktScan"] = idAktScan.ToString();
                    FileScanDeleteDisplay(true);
                    btnAktScanFileDelete.Attributes["IdAktScan"] = idAktScan.ToString();
                }
                else
                {
                    lblImgNote.Text = "Файл не найден!";
                }
            }
        }

        private void FileScanDeleteDisplay(bool display)
        {
            pnlImgBtns.Visible = display;
        }

        private void SetFileScanSelected(int idAktScan)
        {
            foreach (RepeaterItem item in tblAktScanList.Items)
            {
                var tr = (item.FindControl("trAktScanFile") as HtmlTableRow);
                var btn = item.FindControl("btnAktScanFile") as WebControl;

                if (tr != null)
                {
                    tr.Attributes["class"] = String.Empty;
                }

                if (btn != null && btn.Attributes["IdAktScan"] == idAktScan.ToString())
                {
                    if (tr != null)
                    {
                        tr.Attributes["class"] = "file-selected";
                    }
                }
            }
        }

        private void ClearScanViewSelect()
        {
            lblImgNote.Text = string.Empty;
            imgAktScan.ImageUrl = string.Empty;
            imgAktScan.Attributes["IdAktScan"] = String.Empty;
            FileScanDeleteDisplay(false);
        }

        public static NetworkCredential GetNetCredential4Scan()
        {
            string accUserName = ConfigurationManager.AppSettings["accUserName4Scan"];
            string accUserPass = ConfigurationManager.AppSettings["accUserPass4Scan"];

            string domain = accUserName.Substring(0, accUserName.IndexOf("\\"));
            string name = accUserName.Substring(accUserName.IndexOf("\\") + 1);

            NetworkCredential nc = new NetworkCredential(name, accUserPass, domain);

            return nc;
        }

        protected void btnRotateRight_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = imgAktScan.CssClass.Replace("imgNormal", string.Empty).Replace("rotateL90", string.Empty) + " rotateR90";
        }

        protected void btnRotateLeft_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = imgAktScan.CssClass.Replace("imgNormal", string.Empty).Replace("rotateR90", string.Empty) + " rotateL90";
        }

        protected void btnResizePlus_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = imgAktScan.CssClass.Replace("imgNormal", string.Empty).Replace("smallImgScan", string.Empty) + " bigImgScan";
        }

        protected void btnResizeMinus_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = imgAktScan.CssClass.Replace("imgNormal", string.Empty).Replace("bigImgScan", string.Empty) + " smallImgScan";
        }

        protected void btnImageNormal_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = "imgNormal";
        }

        protected void btnRotateDownUp_OnClick(object sender, EventArgs e)
        {
            imgAktScan.CssClass = imgAktScan.CssClass.Replace("imgNormal", string.Empty).Replace("rotateL90", string.Empty).Replace("rotateR90", string.Empty) + " rotate180";
        }

        protected void Timer1_OnTick(object sender, EventArgs e)
        {
            tblAktScanList.DataBind();
            upScanList.Update();
        }
    }
}