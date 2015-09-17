using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.Objects
{
    public class BasePage : Page
    {
        const string qspPrxusr = "prxusr";

        protected new User User
        {
            get { return (Page.Master.Master as Site).User; }
            set
            {
                if (Page != null && Page.Master != null && Page.Master.Master != null && Page.Master.Master is Site)
                (Page.Master.Master as Site).User = value;
            }
        }

        //private bool isRefreshed = false;

        //protected override void OnLoad(EventArgs e)
        //{
        //    if (!IsPostBack) { Session["__TICKET"] = 0; }

        //    int sessionTicket = Convert.ToInt32(Session["__TICKET"]);
        //    int webTicket = Convert.ToInt32(Request["__TICKET"]) + 1;

        //    if (webTicket > sessionTicket)
        //    {
        //        Session["__TICKET"] = webTicket;
        //    }
        //    else
        //    {
        //        isRefreshed = true;
        //    }

        //    ClientScript.RegisterHiddenField("__TICKET", webTicket.ToString());

        //    base.OnLoad(e);
        //}

        //public bool IsRefreshed
        //{
        //    get { return isRefreshed; }
        //}

        public bool IsProxyUser
        {
            get
            {
                string prxusr = Request.QueryString[qspPrxusr];
                return !String.IsNullOrEmpty(prxusr);
            }
        }

        private const string RedirectBackUrlKey = "vskeyRedirectBackUrl";
        protected string RedirectBackUrl { get { if (ViewState[RedirectBackUrlKey] != null) { return ViewState[RedirectBackUrlKey].ToString(); } else { return "~/"; } } set { ViewState[RedirectBackUrlKey] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsProxyUser)
                {
                    string proxySid = Request.QueryString[qspPrxusr];
                    User = new User(proxySid);
                }
                else
                {
                    User = new User(true);
                }


                SetRedirectUrl();
            }
        }

        private void SetRedirectUrl()
        {
            if (Request.UrlReferrer != null)
            {
                RedirectBackUrl = Request.UrlReferrer.PathAndQuery;
            }
        }

        protected void RedirectBack()
        {
            string url = FriendlyUrl.Href(RedirectBackUrl);
            Response.Redirect(url, true);
        }

        protected void RedirectWithParams(Dictionary<string, string> newQueryParams, bool merge = true, string newAbsolutePath = null)
        {
            string srtNewqueryParams = MainHelper.UrlQueryStringParamsJoin(newQueryParams);
            RedirectWithParams(srtNewqueryParams, merge, newAbsolutePath);
        }

        //protected void Reload(params string[] newQueryParams)
        //{
        //    string queryParams = MainHelper.UrlQueryStringParamsJoin(newQueryParams);
        //    Reload(queryParams);
        //}

        /// <summary>
        /// If merge is fals then querystring params will be replaced
        /// </summary>
        /// <param name="newQueryParams"></param>
        /// <param name="merge"></param>
        /// /// <param name="newAbsolutePath"></param>
        protected void RedirectWithParams(string newQueryParams = null, bool merge = true, string newAbsolutePath = null)
        {

            string url = GetRedirectUrlWithParams(newQueryParams, merge, newAbsolutePath);

            Response.Redirect(url, true);



            //=========================
            //if (noQueryParams) Response.Redirect(Request.Url.AbsolutePath, true);

            //string url = Request.Url.PathAndQuery;

            //if (!String.IsNullOrEmpty(newQueryParams))
            //{
            //    url = MainHelper.UrlQueryStringParamsReplace(url, newQueryParams);
            //}

            //Response.Redirect(url, true);
        }

        protected string GetRedirectUrlWithParams(string newQueryParams = null, bool merge = true, string newAbsolutePath = null)
        {
            string currUrl = Request.Url.PathAndQuery;
            string url = currUrl;

            if (merge)
            {
                //merge qs params
                url = MainHelper.UrlQueryStringParamsMerge(currUrl, newQueryParams, newAbsolutePath);
            }
            else
            {
                //replace
                string absolutePath = newAbsolutePath ?? Request.Url.AbsolutePath;
                //string s = FriendlyUrl.Resolve(String.Format("{0}", absolutePath));
                //Uri newUrl = new Uri(String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Host, absolutePath.Remove(0, 1)));
                url = absolutePath;
                if (!String.IsNullOrEmpty(newQueryParams))
                {
                    url = String.Format("{0}?{1}", absolutePath, newQueryParams);
                }
            }

            if (url.StartsWith("/"))
                url = url.Insert(0, "~");
            if (!url.StartsWith("~/"))
                url = url.Insert(0, "~/");


            return FriendlyUrl.Href(url);
        }

        protected void ServerMessageDisplay(PlaceHolder[] arrPlaceHolder, string text = null, bool error = false, bool display = true)
        {
            //string bgClass = error ? "bg-danger" : "bg-success";
            string bgClass = error ? "alert-danger" : "alert-success";
            string textClass = error ? "text-danger" : "text-success";
            if (String.IsNullOrEmpty(text) && !error) text = "Операция прошла успешно";

            //string message = String.Format("<blockquote class='{0}'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><h5 class='{1}'>{2}</h5></blockquote>", bgClass, textClass, text);

            string message = String.Format("<div class='alert {0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong></div>", bgClass, text);
            Literal lServerMessage = new Literal() { Text = message };

            foreach (PlaceHolder ph in arrPlaceHolder)
            {
                ph.Controls.Add(lServerMessage);
            }
        }

        //protected string GetContractsUrl()
        //{
        //    string url = FriendlyUrl.Resolve("~/Contracts");
        //    string url2 = FriendlyUrl.Resolve("~/");

        //    if (Request.UrlReferrer != null && (Request.UrlReferrer.AbsolutePath.Equals(url) || Request.UrlReferrer.AbsolutePath.Equals(url2)))
        //    {
        //        url = RedirectBackUrl;
        //    }
        //    return url;
        //}
    }
}