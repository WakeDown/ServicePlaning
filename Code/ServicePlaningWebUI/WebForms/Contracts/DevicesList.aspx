<%@ Page Title="Договоры - Оборудование - список" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="DevicesList.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.DevicesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
     <div class="form-horizontal val-form" role="form">
         <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
         <div class="form-group">
            <label for='<%=ddlFilterContractNumber.ClientID %>' class="col-sm-2 control-label">Договор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlFilterContractNumber" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlFilterServiceAdmin.ClientID %>' class="col-sm-2 control-label">Сервисный администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlFilterServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlFilterModel.ClientID %>' class="col-sm-2 control-label">Модель</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlFilterModel" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterSerialNum" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=ddlFilterServiceIntervals.ClientID %>'>Интервал</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlFilterServiceIntervals" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=ddlFilterCity.ClientID %>'>Город</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlFilterCity" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=txtFilterAddress.ClientID %>'>Адрес</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterAddress" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=txtFilterObjectName.ClientID %>'>Наименование объекта</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterObjectName" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=txtFilterContactName.ClientID %>'>Конт. лицо</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterContactName" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
         <div class="form-group">
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <div class="row">
        <div class="col-lg-2">
    <div class="btn-group">
        <button type="button" class="btn btn-primary btn-lg dropdown-toggle" data-toggle="dropdown">
            <span id="lChecksListCount" runat="server" class="badge">0</span> отмеченные <i class="caret"></i>
        </button>
        <ul class="dropdown-menu" role="menu">
            <%--<li>
                <asp:LinkButton ID="btnCheckedAdd2Contract" runat="server" OnClick="btnCheckedAdd2Contract_OnClick"><i class="fa fa-external-link"></i> привязать к договору</asp:LinkButton>
            </li>--%>
            <li><a id="btnCheckedIdsClear" runat="server" href='javascript:void(0)'><i class="fa fa-flag-checkered"></i>снять флажки</a></li>
            <li class="divider"></li>
            <li>
                <asp:LinkButton ID="btnCheckedDelete" runat="server" OnClick="btnCheckedDelete_OnClick" OnClientClick="return ActionConfirm('исключить из договора выбранные аппараты')"><i class="fa fa-trash-o"></i> удалить</asp:LinkButton>
            </li>
        </ul>
    </div>
            </div>
    <div>
        <div class="col-lg-3">
    <asp:DropDownList ID="ddlNewServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList></div><asp:Button ID="btnChangeServiceAdmin" runat="server" Text="сменить СА у отмеченных" CssClass="btn btn-default" OnClick="btnChangeServiceAdmin_OnClick" />
        </div>
        </div>
    <div class="clear-fix"></div>
    
    <asp:PlaceHolder ID="phListServerMessage" runat="server"></asp:PlaceHolder>
        
    <asp:Panel ID="pnlContract2DevicesList" runat="server">
        <asp:HiddenField ID="hfCheckedContract2DevicesIds" runat="server" />
        <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span></h5>
        <table id="tblContract2DevicesList" class="table table-striped">
<%--            <tbody id="tbHeader" runat="server">--%>
                <tr>
                    <th>
                        <span id="pnlListTristate" runat="server" data-toggle="tooltip" title="выделить все">
                            <input id="chkListTristate" runat="server" type="hidden" />
                        </span>
                    </th>
                    <th></th>
                    <th>ID</th>
                    <th>Серийный номер</th>
                    <th>Модель; Местонахождение; Интервал; Конт. лицо</th>
                    <th>Админ</th>
                    <th>&nbsp;</th>
                </tr>
<%--            </tbody>--%>
            <asp:Repeater ID="rtrContract2DevicesList" runat="server" DataSourceID="sdsContract2DevicesList">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input value='<%#Eval("id_contract2devices") %>' type="checkbox" />
                        </td>
                        <td>
                            <a href='<%# GetRedirectUrlWithParams(String.Format("c2d={0}", Eval("id_contract2devices")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                        </td>
                        <td><%#Eval("id_contract2devices") %></td>
                        <td><%#Eval("serial_num") %></td>
                        <td><%#Eval("collected_name") %></td>
                        <td class="nowrap"><%#Eval("service_admin") %></td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_contract2devices") %>' OnClientClick="return ActionConfirm('исключить аппарат из списка')" CssClass="btn btn-link btn-lg" data-toggle="tooltip" title="исключить аппарат из списка"><i class="fa fa-trash-o"></i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        <asp:SqlDataSource ID="sdsContract2DevicesList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsContract2DevicesList_OnSelected">
            <SelectParameters>
                <asp:Parameter DefaultValue="getContract2DevicesList" Name="action" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="id" Name="id_contract" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter QueryStringField="ctrtr" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="mdl" Name="id_device_model" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="snum" Name="serial_num" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="sint" Name="id_service_interval" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="cit" Name="id_city" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="addr" Name="address" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="cont" Name="contact_name" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="sadm" Name="id_service_admin" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter DefaultValue="" QueryStringField="objn" Name="object_name" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter Name="rows_count" QueryStringField="rcn" DefaultValue="30" ConvertEmptyStringToNull="True" DbType="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
</asp:Content>
