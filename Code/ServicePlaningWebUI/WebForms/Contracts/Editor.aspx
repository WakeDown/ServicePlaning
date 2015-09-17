<%@ Page Title="Договоры - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.Master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.Editor" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <%-- <ul class="nav nav-tabs">
  <li class="active"><a href='<%= %>'>Договор</a></li>
  <li><a href="#">Оборудование</a></li>
</ul>
     <div>--%>
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtNumber.ClientID %>' class="col-sm-2 control-label required-mark">№ договора</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNumber" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtNumber" runat="server" ErrorMessage="Заполните поле &laquo;№ договора&raquo;" ControlToValidate="txtNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtPrice.ClientID %>' class="col-sm-2 control-label required-mark">Сумма по договору</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtPrice" runat="server" class="form-control" MaxLength="15"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPrice" runat="server" ErrorMessage="Заполните поле &laquo;Сумма по договору&raquo;" ControlToValidate="txtPrice" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvTxtPrice" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtPrice" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractTypes.ClientID %>' class="col-sm-2 control-label required-mark">Тип договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContractTypes" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlContractTypes" runat="server" ErrorMessage="Выберите тип договора" ControlToValidate="ddlContractTypes" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <%--<div class="form-group">
            <label for='<%=ddlServceTypes.ClientID %>' class="col-sm-2 control-label required-mark">Тип ТО</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServceTypes" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlServceTypes" runat="server" ErrorMessage="Выберите тип ТО" ControlToValidate="ddlServceTypes" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>--%>
        <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label required-mark">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="30" AutoPostBack="True" OnTextChanged="txtContractorInn_OnTextChanged"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlContractor" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDdlContractorEmpty" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>

                </div>
                <small>наберите часть имени и нажмите Enter
                </small>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtNote.ClientID %>' class="col-sm-2 control-label">Примечание</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNote" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;№ договора&raquo;" ControlToValidate="txtNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractStatus.ClientID %>' class="col-sm-2 control-label required-mark">Статус договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlContractStatus" runat="server" ErrorMessage="Выберите статус договора" ControlToValidate="ddlContractStatus" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractZipState.ClientID %>' class="col-sm-2 control-label required-mark">Статус ЗИП</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContractZipState" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlContractZipState" runat="server" ErrorMessage="Выберите статус ЗИП договора" ControlToValidate="ddlContractZipState" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblPriceDiscount.ClientID %>' class="col-sm-2 control-label">Скидка</label>
            <div class="col-sm-10">
                <asp:RadioButtonList ID="rblPriceDiscount" runat="server" CssClass="form-control rbl-control" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label required-mark">Менеджер договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlManager" runat="server" ErrorMessage="Выберите менеджера договора" ControlToValidate="ddlManager" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateBegin.ClientID %>' class="col-sm-2 control-label required-mark">Период действия</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateBegin" runat="server" CssClass="form-control datepicker-btn" placeholder="Дата начала"></asp:TextBox>
                        <%--<span class="input-group-иет">
                        <span class="btn btn-default" onclick="$(this).datepicker('#<%=txtDateBegin.ClientID %>');"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        <%--<span class="input-group-btn">
                        <i class="glyphicon glyphicon-minus"></i>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtDateBegin" runat="server" ErrorMessage="Заполните поле &laquo;Период действия - Дата начала&raquo;" ControlToValidate="txtDateBegin" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvTxtDateBegin" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtDateBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateEnd" runat="server" CssClass="form-control datepicker-btn" placeholder="Дата окончания"></asp:TextBox>
                        <%--<span class="input-group-addon">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtDateEnd" runat="server" ErrorMessage="Заполните поле &laquo;Период действия - Дата окончания&raquo;" ControlToValidate="txtDateEnd" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvTxtDateEnd" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtDateEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-5">
                    <asp:LinkButton ID="btnContractPeriodReduction" runat="server" OnClick="btnContractPeriodReduction_OnClick" Enabled="False" class="btn btn-default btn-sm">уменьшить дату окончания на 1 день</asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtHandlingDevices.ClientID %>' class="col-sm-2 control-label">Минимальное количество обслуживаемых ежемесячно устройств</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtHandlingDevices" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;№ договора&raquo;" ControlToValidate="txtNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div id="pnlDevicesListWarning" runat="server">
            <div class="col-sm-10  col-sm-offset-2">
                <blockquote class='bg-warning'>
                    <h5 class="text-warning">Для добавления оборудования сохраните договор</h5>
                </blockquote>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnProlong" runat="server" type="submit" class="btn btn-danger btn-lg" OnClick="btnProlong_Click" ValidationGroup="vgForm" Text="Скопировать привязанные устройства с договора №" Visible="False"><i class="fa fa-save fa-lg"></i>&nbsp;</asp:LinkButton>
            </div>
        </div>
        <div class="form-group">

            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndBack" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку договоров" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-plus fa-sm"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndAddDevices" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и добавить оборудование" OnClick="btnSaveAndAddDevices_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-print fa-sm"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndAddSpecPrice" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и добавить спеццены" OnClick="btnSaveAndAddSpecPrice_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-list-alt fa-sm"></i></asp:LinkButton>
                <asp:LinkButton ID="btnCopy" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Копировать для пролонгации" OnClick="btnCopy_Click" ValidationGroup="vgForm" Visible="False"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку договоров" href='<%=  FriendlyUrl.Href("~/Contracts") %>'><i class="fa fa-mail-reply"></i></a>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnDeactivate" runat="server" type="submit" class="btn btn-danger btn-lg" data-toggle="tooltip" title="Расторжение" OnClick="btnDeactivate_Click" ValidationGroup="vgForm" Visible="False" OnClientClick="return Confirm('После выполнения процедуры расторжения отменить изменения будет НЕВОЗМОЖНО! Вы действительно хотите расторгнуть договор?')"><i class="fa fa-stop fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnPause" runat="server" type="submit" class="btn btn-warning btn-lg" data-toggle="tooltip" title="Приостановить" OnClick="btnPause_Click" ValidationGroup="vgForm" Visible="False"><i class="fa fa-pause fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnEnable" runat="server" type="submit" class="btn btn-success btn-lg" data-toggle="tooltip" title="Возобновить" OnClick="btnEnable_Click" ValidationGroup="vgForm" Visible="False"><i class="fa fa-play fa-lg"></i></asp:LinkButton>
            </div>
        </div>
    </div>
    <%--        </div>--%>



    <%--</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFormButtons" runat="server">--%>
    <%--    <button type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить"><i class="glyphicon glyphicon-floppy-disk"></i></button>--%>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="cphSummaryErrorText" runat="server">
    <%=SummaryErrorText %>
</asp:Content>--%>
