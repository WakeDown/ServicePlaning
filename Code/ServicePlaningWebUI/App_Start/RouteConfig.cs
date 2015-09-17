using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.Services.Protocols;
using Microsoft.AspNet.FriendlyUrls;

namespace ServicePlaningWebUI.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);


            routes.MapPageRoute("", "", "~/WebForms/Service/List.aspx");
            routes.MapPageRoute("ServiceList", "Service", "~/WebForms/Service/List.aspx");
            routes.MapPageRoute("ServicePlan", "Service/Plan", "~/WebForms/Service/Plan.aspx");
            routes.MapPageRoute("ServiceEditor", "Service/Editor", "~/WebForms/Service/Editor.aspx");
            routes.MapPageRoute("ServiceCame", "Service/Editor/Came", "~/WebForms/Service/Came.aspx");
            routes.MapPageRoute("ServiceCameScan", "Service/Editor/CameScan", "~/WebForms/Service/CameScan.aspx");
            routes.MapPageRoute("ServiceEngeneerSet", "Service/EngeneerSet", "~/WebForms/Service/EngeneerSet.aspx");

            routes.MapPageRoute("ContractsList", "Contracts", "~/WebForms/Contracts/List.aspx");
            routes.MapPageRoute("ContractsEditor", "Contracts/Editor", "~/WebForms/Contracts/Editor.aspx");
            routes.MapPageRoute("ContractsDevicesList", "Contracts/Devices", "~/WebForms/Contracts/DevicesList.aspx");
            routes.MapPageRoute("ContractsDevicesEditor", "Contracts/Devices/Editor", "~/WebForms/Contracts/DevicesEditor.aspx");
            routes.MapPageRoute("ContractsSpecPrice", "Contracts/SpecPrice", "~/WebForms/Contracts/SpecPrice.aspx");
            routes.MapPageRoute("ContractsEditorWizard", "Contracts/EditorWizard", "~/WebForms/Contracts/EditorWizard.aspx");

            routes.MapPageRoute("DeviceList", "Devices", "~/WebForms/Devices/List.aspx");
            routes.MapPageRoute("DeviceEditor", "Devices/Editor", "~/WebForms/Devices/Editor.aspx");

            //routes.MapPageRoute("DeviceModelList", "DeviceModels", "~/WebForms/DeviceModels/List.aspx");
            routes.MapPageRoute("DeviceModelEditor", "DeviceModels/Editor", "~/WebForms/DeviceModels/Editor.aspx");

            routes.MapPageRoute("CityEditor", "Cities/Editor", "~/WebForms/Cities/Editor.aspx");

            routes.MapPageRoute("AddressEditor", "Addresses/Editor", "~/WebForms/Addresses/Editor.aspx");

            routes.MapPageRoute("TariffEngeneer", "Settings/TariffEngeneer", "~/WebForms/Settings/TariffEngeneer.aspx");
            routes.MapPageRoute("TariffServADmin", "Settings/TariffServADmin", "~/WebForms/Settings/TariffServADmin.aspx");
            routes.MapPageRoute("Users2Roles", "Settings/Users2Roles", "~/WebForms/Settings/Users2Roles.aspx");

            //Reports
            routes.MapPageRoute("PlanExecute", "Reports/PlanExecute", "~/WebForms/Reports/PlanExecute.aspx");
            routes.MapPageRoute("Payment", "Reports/Payment", "~/WebForms/Reports/Payment.aspx");
            routes.MapPageRoute("ServiceAkt", "Reports/ServiceAkt", "~/WebForms/Reports/ServiceAkt.aspx");
            routes.MapPageRoute("Counters", "Reports/Counters", "~/WebForms/Reports/Counters.aspx");
            routes.MapPageRoute("CountersDetail", "Reports/CountersDetail", "~/WebForms/Reports/CountersDetail.aspx");
            //----

            routes.MapPageRoute("Error", "Error", "~/WebForms/Error.aspx");

            routes.MapPageRoute("OutExecReport", "OutExecRep", "~/WebForms/Output/ExecReport.aspx");

            //routes.MapServiceRoute("ServiceAsmx", "ServiceAsmx", "~/WebForms/Service/Service.asmx");
            //routes.MapServiceRoute("ServiceAsmxCheck", "ServiceAsmx/Check", "~/WebForms/Service/Service.asmx/Check");

            //routes.MapPageRoute("Instruction", "Instruction", "~/WebForms/Instruction.aspx");

            #region Pictures

            //routes.RouteExistingFiles = false;
            routes.MapPageRoute("Chk-unchecked", "Images/Chk_tri_state/unchecked.gif", "~/Images/Chk_tri_state/unchecked.gif");
            routes.MapPageRoute("Chk-checked", "Images/Chk_tri_state/checked.gif", "~/Images/Chk_tri_state/checked.gif");
            routes.MapPageRoute("Chk-intermediate", "Images/Chk_tri_state/intermediate.gif", "~/Images/Chk_tri_state/intermediate.gif");

            routes.MapPageRoute("Chk-unchecked_highlighted", "Images/Chk_tri_state/unchecked_highlighted.gif", "~/Images/Chk_tri_state/unchecked_highlighted.gif");
            routes.MapPageRoute("Chk-checked_highlighted", "Images/Chk_tri_state/checked_highlighted.gif", "~/Images/Chk_tri_state/checked_highlighted.gif");
            routes.MapPageRoute("Chk-intermediate_highlighted", "Images/Chk_tri_state/intermediate_highlighted.gif", "~/Images/Chk_tri_state/intermediate_highlighted.gif");

            #endregion

            #region Docs

            routes.MapPageRoute("Instruction", "Instruction", "~/Docs/Instruction.doc");

            #endregion
        }
        public static Route MapServiceRoute(this RouteCollection routes, string routeName, string url, string virtualPath)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            Route route = new Route(url, new RouteValueDictionary() { { "controller", null }, { "action", null } }, new ServiceRouteHandler(virtualPath));
            routes.Add(routeName, route);
            return route;
        }
        public class ServiceRouteHandler : IRouteHandler
        {
            private readonly string _virtualPath;
            private readonly WebServiceHandlerFactory _handlerFactory = new WebServiceHandlerFactory();

            public ServiceRouteHandler(string virtualPath)
            {
                if (virtualPath == null)
                    throw new ArgumentNullException("virtualPath");
                if (!virtualPath.StartsWith("~/"))
                    throw new ArgumentException("Virtual path must start with ~/", "virtualPath");
                _virtualPath = virtualPath;
            }

            public IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                // Note: can't pass requestContext.HttpContext as the first parameter because that's
                // type HttpContextBase, while GetHandler wants HttpContext.
                return _handlerFactory.GetHandler(HttpContext.Current, requestContext.HttpContext.Request.HttpMethod, _virtualPath, requestContext.HttpContext.Server.MapPath(_virtualPath));
            }
        }
    }
}
