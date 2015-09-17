<%@ Page Title="Оборудование - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Devices.Editor" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Import Namespace="ServicePlaningWebUI.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=lbModel.ClientID %>' class="col-sm-2 control-label required-mark">Модель</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <%--                    <span class="input-group-btn width-20">--%>
                    <asp:TextBox ID="txtModelSelection" runat="server" class="form-control" placeholder="поиск" MaxLength="33"></asp:TextBox>
                    <%--                    </span>--%>
                    <%--                <asp:TextBox ID="txtModel" runat="server" class="form-control" MaxLength="50"></asp:TextBox>--%>
                    <%--<asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control">
                    </asp:DropDownList>--%>
                    <asp:ListBox ID="lbModel" runat="server" CssClass="full-width models-list"></asp:ListBox>
                </div>
                <span class="help-block">
                    <%--<asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Модель&raquo;" ControlToValidate="txtModel" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:RequiredFieldValidator ID="rfvDdlModel" runat="server" ErrorMessage="Выберите модель" Display="Dynamic" ControlToValidate="ddlModel" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvDdlModelEmpty" runat="server" ErrorMessage="Выберите модель" Display="Dynamic" ControlToValidate="ddlModel" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label required-mark">Серийный номер</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtSerialNum" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    <span class="input-group-addon">
                        <%--<span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>--%>
                        <asp:CheckBox ID="chkNoSerialNum" runat="server" />&nbsp;неизвестно
                    </span>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtSerialNum" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtSerialNum" runat="server" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ErrorMessage="Допустимы только латинские буквы и цифры" ValidationExpression="^[A-Za-z0-9]+$"></asp:RegularExpressionValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label required-mark">Инвентарный номер</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtInvNum" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    <span class="input-group-addon">
                        <%--<span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>--%>
                        <asp:CheckBox ID="chkNoInvNum" runat="server" />&nbsp;неизвестно
                    </span>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtInvNum" runat="server" ErrorMessage="Заполните поле &laquo;Инвентарный номер&raquo;" ControlToValidate="txtInvNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtInvNum" runat="server" ControlToValidate="txtInvNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ErrorMessage="Допустимы только латинские буквы и цифры" ValidationExpression="^[A-Za-z0-9]+$"></asp:RegularExpressionValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=cblDeviceOptions.ClientID %>' class="col-sm-2 control-label">Доп. опции</label>
            <div class="col-sm-10">
                <asp:CheckBoxList ID="cblDeviceOptions" runat="server" CssClass="chk-lst list-inline" RepeatLayout="UnorderedList"></asp:CheckBoxList>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceImprint" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlDeviceImprint" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtInstalationDate.ClientID %>' class="col-sm-2 control-label required-mark">Дата установки</label>
            <%--<div class="row">--%>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">
                        <%--<span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>--%>
                        <asp:CheckBox ID="chkNoInstalationDate" runat="server" />
                    </span>
                    <asp:TextBox ID="txtInstalationDate" runat="server" CssClass="form-control datepicker-btn"></asp:TextBox>

                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtInstalationDate" runat="server" ErrorMessage="Заполните поле &laquo;Дата установки&raquo;" Display="Dynamic" ControlToValidate="txtInstalationDate" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvTxtInstalationDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtInstalationDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounter.ClientID %>' class="col-sm-2 control-label required-mark">Счетчик</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">
                        <asp:CheckBox ID="chkNoCounter" runat="server" />
                    </span>
                    <asp:TextBox ID="txtCounter" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtCounter" runat="server" ErrorMessage="Заполните поле &laquo;Счетчик&raquo;" Display="Dynamic" ControlToValidate="txtCounter" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvTxtCounter" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounterColour.ClientID %>' class="col-sm-2 control-label required-mark">Счетчик (цветной)</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">
                        <asp:CheckBox ID="chkNoCounterColour" runat="server" />
                    </span>
                    <asp:TextBox ID="txtCounterColour" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtCounterColour" runat="server" ErrorMessage="Заполните поле &laquo;Счетчик (цветной)&raquo;" Display="Dynamic" ControlToValidate="txtCounterColour" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvTxtCounterColour" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounterColour" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtAge.ClientID %>' class="col-sm-2 control-label required-mark">Возраст</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">
                        <asp:CheckBox ID="chkNoAge" runat="server" />
                    </span>
                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtAge" runat="server" ErrorMessage="Заполните поле &laquo;Возраст&raquo;" Display="Dynamic" ControlToValidate="txtAge" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
<%--                    <asp:CompareValidator ID="cvTxtAge" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtAge" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>

        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSave" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-plus fa-sm"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку оборудования" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку оборудования" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
    </div>
</asp:Content>
