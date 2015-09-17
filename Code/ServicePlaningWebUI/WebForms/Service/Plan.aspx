<%@ Page Title="Сервис - план" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.Plan" Culture="ru-RU" UICulture="ru-RU" %>

<%@ Register TagPrefix="asp" Namespace="ServicePlaningWebUI.Objects" Assembly="ServicePlaningWebUI" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div id="pnlServicePlanCalendar"></div>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="col-sm-offset-2 col-sm-10">
        <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    </div>
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtPlaningDate.ClientID %>' class="col-sm-2 control-label required-mark">Месяц</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtPlaningDate" runat="server" CssClass="form-control datepicker-btn-month" AutoPostBack="true"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPlaningDate" runat="server" ErrorMessage="Заполните поле &laquo;Месяц&raquo;" Display="Dynamic" ControlToValidate="txtPlaningDate" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtPlaningDate" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label required-mark">Оборудование</label>
            <div class="col-sm-10">
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default" id="filter">

                        <div id="filterPanel" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div class="form-horizontal val-form" role="form">
                                    <div class="form-group">
                                        <label for='<%=txtNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtNumber" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
                                            <span class="help-block">
                                                <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" for='<%=ddlServiceAdmin.ClientID %>'>Сервисный администратор</label>
                                        <div class="col-sm-10">
                                            <div class="input-group full-width">
                                                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
                                        <div class="col-sm-10">
                                            <div class="input-group full-width">
                                                <span class="input-group-btn width-20">
                                                    <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30"></asp:TextBox>
                                                </span>
                                                <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                                <span class="help-block">
                                                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for='<%=rblNotInPlanList.ClientID %>' class="col-sm-2 control-label">Отсутствуют в плане</label>
                                        <div class="col-sm-10">
                                            <asp:RadioButtonList ID="rblNotInPlanList" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                                                <asp:ListItem Text="да" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="все" Value="0"></asp:ListItem>

                                                <%--                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <span class="help-block">
                                                <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-2 col-sm-10">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                                            <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="filterHead" class="panel-heading">
                            <div class="panel-title collapsed" data-toggle="collapse" data-target="#filterPanel">
                                <a class="title"><i class="glyphicon glyphicon-filter"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="pnlDevices" runat="server" class="input-group full-width form-control">
                    <%--<asp:Repeater ID="rtrContracts" runat="server" DataSourceID="sdsContracts">
                            <ItemTemplate>--%>
                    <%--<hr class="smallmargin teeny" />
                                <h5><%# Eval("collected_name") %></h5>
                                <div class="clearfix"></div>--%>
                    <asp:HiddenField ID="hfCheckedDevicePlanIds" runat="server" />
                    <h5>Всего выбрано: <asp:Label ID="lChecksCount" runat="server" CssClass="badge" Text="0"></asp:Label>&nbsp;&nbsp;<a id="btnCheckedIdsClear" runat="server" href='javascript:void(0)'><%--<i class="fa fa-flag-checkered"></i>--%> снять флажки</a></h5>
                    <asp:Repeater ID="rtrServiceIntervalPlanGroups" runat="server" DataSourceID="sdsServiceIntervalPlanGroups" OnItemDataBound="rtrServiceIntervalPlanGroups_OnItemDataBound" OnItemCreated="rtrServiceIntervalPlanGroups_OnItemCreated">
                        <ItemTemplate>
                            <div id="pnlDevicesTitle" class="form-device-chk-list-title" data-toggle="collapse" data-target='<%# String.Format("#{0}", Container.FindControl("pnlDevices").ClientID) %>'>
                                <h4><strong><%# Eval("name") %></strong> <span id="lChecksCount" runat="server" class="badge">0</span> <span class="title title-smaller"></span></h4>
                                <%--<hr class="teeny" />--%>
                            </div>
                            <div class="clearfix"></div>
                            <div id="pnlDevices" runat="server" class="panel-collapse collapse in">
                                <asp:HiddenField ID="hfServiceIntervalPlanGroupId" runat="server" Value='<%# Eval("id_service_interval_plan_group") %>' />
                                <asp:GridView ID="tblDeviceList" runat="server" CssClass="table table-striped" DataSourceID="sdsDeviceList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="30" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" EmptyDataText="нет записей" OnRowDataBound="tblDeviceList_OnRowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="id_contract2devices" SortExpression="id_contract2devices" HeaderText="ID" HeaderStyle-CssClass="sorted-header" Visible="false" />
                                        <%--<asp:BoundField DataField="name" SortExpression="name" HeaderText="Аппарат" HeaderStyle-CssClass="sorted-header" HtmlEncode="false" />--%>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все в группе">
                                                    <input id="chkTristate" runat="server" type="hidden" />
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfIdContract2Devices" runat="server" Value='<%#Eval("id_contract2devices") %>' />
                                                <div>
                                                    <asp:Literal ID="litContract" runat="server"></asp:Literal>
                                                </div>
                                                <%--<div class="clearfix"></div>--%>
                                                <div class="row">
                                                    <div class="col-sm-1">
                                                       <%-- <asp:CheckBox ID="chkIdContract2Devices" runat="server" value='<%#Eval("id_contract2devices") %>' />--%>
                                                         <input ID="chkIdContract2Devices" runat="server" value='<%#Eval("id_contract2devices") %>' type="checkbox" />
                                                    </div>
                                                    <div class="col-sm-10"><%# Eval("name") %></div>
                                                    <div class="col-sm-1"><%# SetNoServiceMonthsCountVisual(Eval("no_service_month_count").ToString()) %></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="min-width nowrap" HeaderText="Последняя дата заявки">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Literal ID="litLastClaimIndend" runat="server"></asp:Literal>
                                                </div>
                                                <%# Eval("last_claim_date", "{0:MMMM yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="min-width nowrap" HeaderText="Последняя дата выезда">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Literal ID="litLastCameIndent" runat="server"></asp:Literal>
                                                </div>
                                                <%# Eval("last_came_date", "{0:d}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="last_claim_date" SortExpression="last_claim_date" HeaderText="Последняя дата заявки" HeaderStyle-CssClass="sorted-header" />
                                        <asp:BoundField DataField="last_came_date" SortExpression="last_came_date" HeaderText="Последняя дата выезда" HeaderStyle-CssClass="sorted-header" />--%>
                                    </Columns>
                                </asp:GridView>
                                <%--<asp:CheckBoxList ID="chklDeviceList" runat="server" DataSourceID="sdsDeviceList" DataTextField="name" DataValueField="id"></asp:CheckBoxList>--%>
                                <asp:SqlDataSource ID="sdsDeviceList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelecting="sdsDeviceList_OnSelecting">
                                    <%--OnSelected="sdsContracts_OnSelected"--%>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="getContract2DevicesPlanSelectionList" Name="action" />
                                        <asp:ControlParameter ControlID='hfServiceIntervalPlanGroupId' Name="id_service_interval_plan_group" />
                                        <asp:ControlParameter ControlID='txtPlaningDate' Name="planing_date" DbType="Date" />
                                        <asp:ControlParameter ControlID='rblNotInPlanList' Name="not_in_plan_list" DbType="Int16" />
                                        <asp:ControlParameter  ControlID="ddlServiceAdmin" Name="id_service_admin" DefaultValue="" DbType="Int32" ConvertEmptyStringToNull="True" />
                                        <asp:ControlParameter ControlID="txtNumber" Name="number" DefaultValue="" ConvertEmptyStringToNull="True" />
                                        <asp:ControlParameter ControlID="ddlContractor" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="clearfix"></div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource ID="sdsServiceIntervalPlanGroups" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                        <%--OnSelected="sdsContracts_OnSelected"--%>
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getServiceIntervalPlanGroupList" Name="action" />
                            <%--<asp:Parameter DefaultValue="1" Name="is_active" />--%>
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <%--</ItemTemplate>
                        </asp:Repeater>
                        <asp:SqlDataSource ID="sdsContracts" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="getContractList" Name="action" />
                                <asp:Parameter DefaultValue="1" Name="is_active" />
                            </SelectParameters>
                        </asp:SqlDataSource>--%>
                </div>

                <span class="help-block">
                    <%--<asp:RequiredFieldValidatorForCheckBoxLists ID="rfvChklDeviceList" runat="server" ErrorMessage="Выберите оборудование" ControlToValidate="chklDeviceList" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidatorForCheckBoxLists>--%>
                </span>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessageBottom" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сформировать и продолжить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сформировать и перейти к списку выездов" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку выездов" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
    </div>

</asp:Content>
