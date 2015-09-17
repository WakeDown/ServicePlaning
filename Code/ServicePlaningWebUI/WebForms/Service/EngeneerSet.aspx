<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="EngeneerSet.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.EngeneerSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <div class="form-horizontal val-form" role="form">
         <div class="form-group">
            <label for='<%=ddlEngeneerGroup.ClientID %>' class="col-sm-1 control-label">Организация</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlEngeneerGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEngeneerGroup_OnSelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceEngeneer.ClientID %>' class="col-sm-1 control-label">Инженер</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlServiceEngeneer" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <asp:LinkButton ID="btnSetEngeneer" runat="server" OnClick="btnSetEngeneer_OnClick" CssClass="btn btn-primary">
                <i class="fa fa-cog"></i>&nbsp;назначить для отмеченых&nbsp;<asp:Label ID="lChecksCount" runat="server" CssClass="badge" Text="0"></asp:Label>
            </asp:LinkButton>&nbsp;
            <a id="btnCheckedIdsClear" runat="server" href='javascript:void(0)'><%--<i class="fa fa-flag-checkered"></i>--%> снять все флажки</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlServiceAdmin.ClientID %>' class="col-sm-2 control-label">Сервисный администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
                    <div class="col-sm-10">
                        <div class="input-group full-width">
                            <span class="input-group-btn width-20">
                                <asp:TextBox ID="txtContractorSelection" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30" AutoPostBack="True" OnTextChanged="txtContractorSelection_OnTextChanged"></asp:TextBox>
                            </span>
                            <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=ddlCity.ClientID %>'>Город</label>
            <div class="col-sm-10">
                <asp:UpdatePanel ID="upCity" runat="server">
                    <ContentTemplate>
                        <div class="input-group full-width">
                            <span class="input-group-btn width-20">
                                <asp:TextBox ID="txtCityFilter" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30" OnTextChanged="txtCityFilter_OnTextChanged" AutoPostBack="True"></asp:TextBox>
                            </span>
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for='<%=ddlAddress.ClientID %>'>Адрес</label>
            <div class="col-sm-10">
                <asp:UpdatePanel ID="upAddress" runat="server">
                    <ContentTemplate>
                        <div class="input-group full-width">

                            <span class="input-group-btn width-20">
                                <asp:TextBox ID="txtAddressFilter" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="50" OnTextChanged="txtAddressFilter_OnTextChanged" AutoPostBack="True"></asp:TextBox>
                            </span>
                            <asp:DropDownList ID="ddlAddress" runat="server" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateMonth.ClientID %>' class="col-sm-2 control-label">Месяц</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtDateMonth" runat="server" CssClass="form-control datepicker-btn-month"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtDateMonth" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblDone.ClientID %>' class="col-sm-2 control-label">Выполненные</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblDone" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="да" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblNoSet.ClientID %>' class="col-sm-2 control-label">Назначенные</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblNoSet" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="да" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
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
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <%--    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>&nbsp;&nbsp;<a id="btnCheckedIdsClear" runat="server" href='javascript:void(0)'> снять флажки</a></h5>--%>
    <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <div id="pnlContractorsDevices" runat="server">
        <div class="row pad-l-sm">
            <asp:Repeater ID="tblServiceIntervalPlanGroup" runat="server" DataSourceID="sdsServiceIntervalPlanGroup">
                <ItemTemplate>
                    <div class="col-sm-2"><span runat="server" class="label label-mark" style='<%#String.Format("background-color: #{0}", Eval("color"))%>'>&nbsp;</span>&nbsp;-&nbsp;<%#Eval("name") %></div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="sdsServiceIntervalPlanGroup" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getServiceIntervalPlanGroupList" Name="action" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <asp:HiddenField ID="hfCheckedContractorsDevices" runat="server" />
        <asp:Repeater ID="tblContractorList" runat="server" OnItemDataBound="tblContractorList_OnItemDataBound" DataSourceID="sdsContractorList">
            <HeaderTemplate>
                <div class="row row-bg pad-l-sm">
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnShowAllDevices" runat="server" OnClick="btnShowAllDevices_OnClick">показать все</asp:LinkButton>
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnhideAllDevices" runat="server" OnClick="btnhideAllDevices_OnClick">свернуть все</asp:LinkButton>
                    </div>
                </div>
                <div class="table table-striped">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row header-main">
                    <div class="col-sm-12">
                        <div class="tristate-container">
                            <div id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                                <input id="chkTristate" runat="server" type="hidden" />
                            </div>
                        </div>
                        <%--                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                        <asp:LinkButton ID="btnShowDevices" runat="server" OnClick="btnShowDevices_OnClick" CssClass="link-control-header" ItemIndex='<%#Container.ItemIndex %>'>
                            <%#Eval("NAME") %>
                            &nbsp;<span id="lCountDevices" runat="server" class="bold text-primary"><%#Eval("dev_cnt") %></span>&nbsp;/
                            &nbsp;<span id="lCountClaims" runat="server" class="bold text-primary"><%#Eval("cnt") %></span>&nbsp;
                            /&nbsp;<span id="lCountSettedClaims" runat="server" class="bold text-primary"><%#Eval("set_cnt") %></span>
                            &nbsp;<small>отмечено&nbsp;<span id="lChecksCountInner" runat="server" class="bold text-success">0</span></small>
                            <asp:HiddenField ID="hfFlag" runat="server" />
                        </asp:LinkButton>
                        <%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </div>
                </div>
                <div id='<%#String.Format("pnlDevices{0}", Container.ItemIndex) %>'>
                    <%--                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnShowDevices" EventName="Click"/>
                    </Triggers>
                        <contenttemplate>--%>
                    <asp:Repeater ID="tblDeviceList" runat="server" OnItemDataBound="tblDeviceList_OnItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-sm-1">
                                    <span runat="server" class="label label-success success-light label-mark" visible='<%#Eval("has_came").ToString().Equals("1") %>'>&nbsp;</span>
                                    <input id="chkSetClaim" runat="server" visible='<%#Eval("has_came").ToString().Equals("0") &&  !String.IsNullOrEmpty(Eval("id_service_claim").ToString()) %>' value='<%#Eval("id_contract2devices") %>' type="checkbox" />
                                    <asp:HiddenField ID="hfIdClaim" runat="server" Value='<%# Eval("id_service_claim") %>' />
                                    <asp:HiddenField ID="hfIdContract2Devices" runat="server" Value='<%# Eval("id_contract2devices") %>' />
                                    <%#Eval("id_service_claim") %>
                                </div>
                                <div class="col-sm-3">
                                    <%# Eval("is_limit_device_claims").ToString().Equals("1") ? "неизвестное" : Eval("device") %>
                                </div>
                                <div class="col-sm-2">
                                    <span runat="server" class="label label-mark" style='<%#String.Format("background-color: #{0}", Eval("mark_color"))%>'>&nbsp;</span>
                                    <%#Eval("contract_number") %><div class="text-danger" runat="server" visible='<%# Eval("show_date_end").ToString().Equals("1") %>'><%# String.Format("(до {0:dd.MM.yy})" ,Eval("contract_date_end")) %></div>
                                    <div class="text-danger" runat="server" visible='<%# Eval("show_contract_state").ToString().Equals("1") %>'><%# String.Format("({0})" ,Eval("contract_state")) %></div>
                                </div>
                                <div class="col-sm-3">
                                    <%# Eval("is_limit_device_claims").ToString().Equals("1") ? "неизвестное" : String.Format("{0}, {1}", Eval("city"), Eval("address"))  %>
                                </div>
                                <div class="col-sm-1 nowrap">
                                    <%# String.Format("{0:MMMM yyyy}", Eval("planing_date")) %>
                                </div>
                                <div class="col-sm-2 nowrap">
                                    <asp:HiddenField ID="hfIdServiceEngeneerPlan" runat="server" Value='<%# Eval("id_service_engeneer_plan") %>' />
                                    <%#Eval("service_engeneer") %>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource ID="sdsDeviceList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsDeviceList_OnSelected">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getSrvplContractorDeviceList" Name="action" />
                            <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                            <asp:QueryStringParameter QueryStringField="cit" Name="id_city" DefaultValue="" ConvertEmptyStringToNull="True" />
                            <asp:QueryStringParameter QueryStringField="addr" Name="address" DefaultValue="" ConvertEmptyStringToNull="True" />
                            <asp:QueryStringParameter QueryStringField="sadm" Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="true" />
                            <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                            <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                            <asp:Parameter Name="show_inn" DefaultValue="0" ConvertEmptyStringToNull="True" DbType="Int16" />
                            <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="sdsContractorList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected" OnSelecting="sdsContractorList_OnSelecting">
            <SelectParameters>
                <asp:Parameter DefaultValue="getSrvplContractorList" Name="action" />
                <asp:QueryStringParameter QueryStringField="ctr" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                <asp:QueryStringParameter QueryStringField="cit" Name="id_city" DefaultValue="" ConvertEmptyStringToNull="True" />
                <asp:QueryStringParameter QueryStringField="addr" Name="address" DefaultValue="" ConvertEmptyStringToNull="True" />
                <asp:QueryStringParameter QueryStringField="sadm" Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="true" />
                <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                <asp:Parameter Name="show_inn" DefaultValue="0" ConvertEmptyStringToNull="True" DbType="Int16" />
                <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
