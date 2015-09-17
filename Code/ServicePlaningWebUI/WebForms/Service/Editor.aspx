<%@ Page Title="Обслуживание - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.Editor" %>

<%@ Register TagPrefix="asp" Namespace="ServicePlaningWebUI.Objects" Assembly="ServicePlaningWebUI" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <asp:HiddenField ID="hfIdContract2Devices" runat="server" />
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlContract.ClientID %>' class="col-sm-2 control-label required-mark">Договор</label>
            <div class="col-sm-10">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <div class="input-group full-width">

                            <span class="input-group-btn width-20">
                                <asp:TextBox ID="txtContractSelection" runat="server" CssClass="form-control width-20" placeholder="поиск" MaxLength="33" OnTextChanged="txtContractSelection_OnTextChanged" AutoPostBack="True"></asp:TextBox>
                            </span>
                            <asp:DropDownList ID="ddlContract" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlContract_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                        </div>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlContract" runat="server" ErrorMessage="Выберите договор" Display="Dynamic" ControlToValidate="ddlContract" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvDdlContractEmpty" runat="server" ErrorMessage="Выберите договор" Display="Dynamic" ControlToValidate="ddlContract" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label required-mark">Оборудование</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <%--<span class="input-group-btn width-20">
                        <asp:TextBox ID="txtDeviceSelection" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="33"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlDevice" runat="server" ErrorMessage="Выберите оборудование" Display="Dynamic" ControlToValidate="ddlDevice" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDdlDeviceEmpty" runat="server" ErrorMessage="Выберите оборудование" Display="Dynamic" ControlToValidate="ddlDevice" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    </span>--%>
                    <div id="pnlOneDeviceList" runat="server">
                        <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="pnlDeviceList" runat="server">
                    <div class="input-group full-width form-control">

                        <div id="pnlDevicesTitle" class="collapsed form-device-chk-list-title" data-toggle="collapse" data-target="#pnlDevices">
                            выбрано устройств <span id="lChecksCount" runat="server" class="badge">0</span> <span class="title"></span>
                        </div>
                        <div id="pnlDevices" class="panel-collapse collapse">
                            <div class="pad-t-sm">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label" for='<%=ddlFilterCity.ClientID %>'>Город</label>
                                    <div class="col-sm-11">
                                        <div class="input-group full-width">
                                            <span class="input-group-btn width-20">
                                                <asp:TextBox ID="txtCityFilter" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30" OnTextChanged="txtCityFilter_OnTextChanged" AutoPostBack="True"></asp:TextBox>
                                            </span>
                                            <asp:DropDownList ID="ddlFilterCity" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label" for='<%=txtFilterAddress.ClientID %>'>Адрес</label>
                                    <div class="col-sm-11">
                                        <asp:TextBox ID="txtFilterAddress" runat="server" class="form-control input-sm" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                                    <input id="chkTristate" runat="server" type="hidden" />
                                    <%--                                    <span>все</span>--%>
                                </span>
                            </div>
                            <%--<asp:CheckBoxList ID="chklDeviceList" runat="server" CssClass="chk-lst"></asp:CheckBoxList>--%>

                            <div id="tblDeviceList" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hfLstCheckedDeviceIds" runat="server" />
                                        <asp:Repeater ID="rtrDeviceList" runat="server">
                                            <ItemTemplate>
                                                <div class='row <%# Eval("is_new").ToString().Equals("1") ? "bg-warning" : String.Empty %>'>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkIdC2d" runat="server" Value='<%#Eval("id") %>'  />&nbsp;<%#Eval("name") %>
<%--                                                                                                         <input type="checkbox" ID="chkIdC2d" runat="server" Value='<%#Eval("id") %>' />&nbsp;<%#Eval("name") %>--%>
                                                    </div>
                                                    <div class="col-sm-2"><%#Eval("short_city") %></div>
                                                    <div class="col-sm-3"><%#Eval("address") %></div>
                                                    <div class="col-sm-3"><%#Eval("object_name") %></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                    </div>
                </div>
            </div>
            <span class="help-block">
                <%--                    <asp:RequiredFieldValidatorForCheckBoxLists ID="rfvChklDeviceList" runat="server" ErrorMessage="Выберите оборудование" ControlToValidate="chklDeviceList" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidatorForCheckBoxLists>--%>
            </span>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=ddlServiceClaimType.ClientID %>' class="col-sm-2 control-label required-mark">Тип выезда</label>
        <div class="col-sm-10">
            <asp:DropDownList ID="ddlServiceClaimType" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <span class="help-block">
                <asp:RequiredFieldValidator ID="rfvDdlServiceClaimType" runat="server" ErrorMessage="Выберите тип выезда" Display="Dynamic" ControlToValidate="ddlServiceClaimType" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
            </span>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=ddlServiceEngeneer.ClientID %>' class="col-sm-2 control-label">ФИО инженера</label>
        <div class="col-sm-10">
            <asp:DropDownList ID="ddlServiceEngeneer" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <span class="help-block">
                <%--<asp:RequiredFieldValidator ID="rfvDdlServiceEngeneer" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlServiceEngeneer" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
            </span>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=txtPlaningDate.ClientID %>' class="col-sm-2 control-label required-mark">Месяц (план)</label>
        <div class="col-sm-3">
            <div class="input-group">
                <asp:TextBox ID="txtPlaningDate" runat="server" CssClass="form-control datepicker-btn-month"></asp:TextBox>
                <%--<span class="input-group-btn">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
            </div>
            <span class="help-block">
                <asp:RequiredFieldValidator ID="rfvTxtPlaningDate" runat="server" ErrorMessage="Заполните поле &laquo;Месяц (план)&raquo;" Display="Dynamic" ControlToValidate="txtPlaningDate" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtPlaningDate" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
            </span>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=txtDescr.ClientID %>' class="col-sm-2 control-label">Комментарий</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtDescr" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            <span class="help-block">
                <%--                    <asp:RequiredFieldValidator ID="rfvTxtInstalationDate" runat="server" ErrorMessage="Заполните поле &laquo;Дата установки&raquo;" Display="Dynamic" ControlToValidate="txtInstalationDate" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtDateClaim" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
            </span>
        </div>
    </div>
    <div class="col-sm-offset-2 col-sm-10">
        <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
            <%--                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку заявок" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>--%>
            <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку заявок" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
        </div>
    </div>
    </div>
</asp:Content>
