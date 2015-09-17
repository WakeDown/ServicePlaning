<%@ Page Title="Договоры - Оборудование - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="DevicesEditor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.DevicesEditor" %>

<%@ Register TagPrefix="asp" Namespace="ServicePlaningWebUI.Objects" Assembly="ServicePlaningWebUI" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <%-- ScrollSpy   (Список, форма) боковая кнопочная ориентация как одностраничные сайты  --%>


    <div class="panel-body">
        <%--                <div class="row">--%>
        <%--<div class="col-sm-12">--%>
        <div class="form-horizontal val-form" role="form">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:PlaceHolder ID="phServerMessageTop" runat="server"></asp:PlaceHolder>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlContractNumber.ClientID %>'>Договор</label>
                <div class="col-sm-10">
                    <div class="input-group full-width">
                        <asp:DropDownList ID="ddlContractNumber" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlContractNumber_OnSelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <%--<span class="input-group-btn">
                            <span id="btnContractUnlock" runat="server" class="btn"><i class="fa"></i></span>
                        </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlContractNumber" runat="server" ErrorMessage="Выберите договор" ControlToValidate="ddlContractNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDdlContractNumberBrowse" runat="server" ErrorMessage="Выберите договор" ControlToValidate="ddlContractNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgListBrowse" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--<asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="ddlContractNumber" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlDevices.ClientID %>'>Оборудование</label>
                <div class="col-sm-10">
                    <div id="pnlCheckedDeviceList" runat="server">
                        <%-- При попадании в данный редактор с выбранными устройствами - показываем количество выбранных устройств и по нажатию кнопки раскрываем список --%>
                        <%--<div class="panel-heading collapsed" data-toggle="collapse" data-target="#collapseDevices">
                            <div class="panel-title">
                                выбрано <span class="badge">0</span> устройств <a class="btn btn-link">раскрыть список</a>
                            </div>
                        </div>
                        <div class="panel-collapse collapse">--%>
                        <div class="input-group full-width form-control ">
                            <div id="pnlCheckedDevicesTitle" class="collapsed form-device-chk-list-title" data-toggle="collapse" data-target="#pnlCheckedDevices">
                                выбрано устройств <span id="lChecksCount" runat="server" class="badge">0</span> <span class="title"><%--<span class="text"></span>--%><%--<span class="ico"></span>--%></span>
                            </div>
                            <div id="pnlCheckedDevices" class="panel-collapse collapse">
                                <div>
                                    <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                                        <input id="chkTristate" runat="server" type="hidden" />
                                        все
                                    </span>
                                </div>
                                <asp:CheckBoxList ID="chklCheckedDeviceList" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>
                        <span class="help-block">
                            <asp:RequiredFieldValidatorForCheckBoxLists ID="rfvChklCheckedDeviceList" runat="server" ErrorMessage="Выберите оборудование" ControlToValidate="chklCheckedDeviceList" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidatorForCheckBoxLists>
                            <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        </span>
                        <%--</div>--%>
                    </div>
                    <div id="pnlDeviceList" runat="server">
                        <div class="input-group full-width">
                            <asp:DropDownList ID="ddlDevices" runat="server" CssClass="form-control" placeholder="Оборудование">
                            </asp:DropDownList>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnDeviceBrowse" runat="server" OnClick="btnDeviceBrowse_Click" class="btn btn-default" data-toggle="tooltip" title="обзор" ValidationGroup="vgListBrowse"><i class="fa fa-ellipsis-h"></i></asp:LinkButton>
                                <%--                            <span class="btn btn-default bold" data-toggle="tooltip" title="обзор">...</span>--%>
                            </span>
                        </div>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlDevices" runat="server" ErrorMessage="Выберите оборудование" ControlToValidate="ddlDevices" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                            <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        </span>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlServiceAdmin.ClientID %>'>Сервисный администратор</label>
                <div class="col-sm-10">
                    <div class="input-group full-width">
                        <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlServiceAdmin" runat="server" ErrorMessage="Выберите сервисного администратора" ControlToValidate="ddlServiceAdmin" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--                            <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="ddlContractNumber" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <asp:UpdatePanel ID="upServiceSchedule" runat="server">
                        <ContentTemplate>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlServiceIntervals.ClientID %>'>Интервал</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlServiceIntervals" runat="server" CssClass="form-control" placeholder="Интервал" OnSelectedIndexChanged="ddlServiceIntervals_OnSelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvddlServiceIntervals" runat="server" ErrorMessage="Выберите интервал" ControlToValidate="ddlServiceIntervals" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group" id="pnlGriphick" runat="server" Visible="False">
                <label class="col-sm-2 control-label">График обслуживания</label>
                <div class="col-sm-10">
                    <%--                    <div>
                    <asp:CheckBox ID="chkAddScheduleDates2ServicePlan" runat="server" Checked="True" />&nbsp;создать/обновить план работ по графику</div>--%>
                    
                            <asp:Repeater ID="rtrServiceSchedule" runat="server">
                                <HeaderTemplate>
                                    <table class="tbl-schedule">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--<div class="col-sm-2 schedule-field">
                                        <asp:CheckBox ID="chkScheduleDate" runat="server" />&nbsp;<%# String.Format("{0:MMMM yyyy}", Eval("Date")) %>
                                        <asp:HiddenField ID="hfScheduleDate" runat="server" Value='<%# Eval("Date") %>' />
                                    </div>--%>
                                    <tr>
                                        <th class="schedule-header">
                                            <asp:Panel ID="pnlSchedYear" runat="server">
                                                <%# String.Format("{0:yyyy}", Eval("Date")) %>
                                            </asp:Panel>
                                            <asp:HiddenField ID="hfSchedYear" runat="server" Value='<%# Convert.ToDateTime(Eval("Date")).Year %>' />
                                        </th>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth1" runat="server" CssClass="disabled">
                                                1
                                            <asp:HiddenField ID="hfSchedMonth1" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth2" runat="server" CssClass="disabled">
                                                2
                                            <asp:HiddenField ID="hfSchedMonth2" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth3" runat="server" CssClass="disabled">
                                                3
                                            <asp:HiddenField ID="hfSchedMonth3" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth4" runat="server" CssClass="disabled">
                                                4
                                            <asp:HiddenField ID="hfSchedMonth4" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth5" runat="server" CssClass="disabled">
                                                5
                                            <asp:HiddenField ID="hfSchedMonth5" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth6" runat="server" CssClass="disabled">
                                                6
                                            <asp:HiddenField ID="hfSchedMonth6" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth7" runat="server" CssClass="disabled">
                                                7
                                            <asp:HiddenField ID="hfSchedMonth7" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth8" runat="server" CssClass="disabled">
                                                8
                                            <asp:HiddenField ID="hfSchedMonth8" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth9" runat="server" CssClass="disabled">
                                                9
                                            <asp:HiddenField ID="hfSchedMonth9" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth10" runat="server" CssClass="disabled">
                                                10
                                            <asp:HiddenField ID="hfSchedMonth10" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth11" runat="server" CssClass="disabled">
                                                11
                                            <asp:HiddenField ID="hfSchedMonth11" runat="server" />
                                            </asp:Panel>
                                        </td>
                                        <td class="schedule-field">
                                            <asp:Panel ID="pnlSchedMonth12" runat="server" CssClass="disabled">
                                                12
                                            <asp:HiddenField ID="hfSchedMonth12" runat="server" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                       
                    
                </div>
            </div>
                 </ContentTemplate>
                    </asp:UpdatePanel>
<%--            <div class="form-group">
            <div class="col-sm-10 col-sm-push-2">
                    <asp:LinkButton ID="btnSaveGraphick" runat="server" OnClick="btnSaveGraphick_Click" CssClass="btn btn-primary btn-sm" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;сохранить график обслуживания</asp:LinkButton>
                        </div>
                </div>--%>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlCity.ClientID %>'>Город</label>
                <div class="col-sm-10">
                    <div class="input-group full-width">
                        <span class="input-group-btn width-20">
                            <asp:TextBox ID="txtCityFilter" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="30"></asp:TextBox>
                        </span>
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlCity" runat="server" ErrorMessage="Выберите город" ControlToValidate="ddlCity" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                            <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        </span>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label required-mark" for='<%=ddlAddress.ClientID %>'>Адрес</label>
                <div class="col-sm-10">
                    <%--<asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtAddress" runat="server" ErrorMessage="Заполните поле &laquo;Адрес&raquo;" ControlToValidate="txtAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>
                    </span>--%>
                    <div class="input-group full-width">
                        <span class="input-group-btn width-20">
                            <asp:TextBox ID="txtAddressFilter" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="50"></asp:TextBox>
                        </span>
                        <asp:DropDownList ID="ddlAddress" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlAddress" runat="server" ErrorMessage="Выберите адрес" ControlToValidate="ddlAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                            <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        </span>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtObjectName.ClientID %>'>Наименование объекта</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtObjectName" runat="server" class="form-control"></asp:TextBox>
                    <span class="help-block">
                        <%--                        <asp:RequiredFieldValidator ID="rfvTxtObjectName" runat="server" ErrorMessage="Заполните поле &laquo;Наименование объекта&raquo;" ControlToValidate="txtObjectName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>--%>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtContactName.ClientID %>'>Конт. лицо</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="150" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtContactName" runat="server" ErrorMessage="Заполните поле &laquo;Конт. лицо&raquo;" ControlToValidate="txtContactName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtCoord.ClientID %>'>Координаты объекта</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtCoord" runat="server" class="form-control"></asp:TextBox>
                    <span class="help-block">
                        <%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Заполните поле &laquo;Адрес&raquo;" ControlToValidate="txtAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>--%>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtComment.ClientID %>'>Комментарий</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtComment" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                    <%--                    <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-default btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку договоров" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>--%>
                    <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку договоров" href='<%= FriendlyUrl.Href(ListUrl)  %>'><i class="fa fa-mail-reply"></i></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
