<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.Master" AutoEventWireup="true" CodeBehind="CountersDetail.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.CountersDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <asp:Literal ID="lFormTitle" runat="server"></asp:Literal>
    <h5>
        <asp:Literal ID="lDeviceAddress" runat="server"></asp:Literal>
    </h5>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=rblDataSource.ClientID %>' class="col-sm-1 control-label">Источник</label>
            <div class="col-sm-4">
                <asp:RadioButtonList ID="rblDataSource" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst input-sm" AutoPostBack="True">
                    <asp:ListItem Text="все" Value="all" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="только UN1T Счетчик" Value="un1t_cnt"></asp:ListItem>
                    <asp:ListItem Text="только Инженер" Value="eng"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <label for='<%=rblRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblRowsCount" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst input-sm" AutoPostBack="True">
                    <asp:ListItem Text="все" Value="0"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <%--        <div class="form-group">
            
        </div>--%>
        <div class="form-group">
            <label for='<%=rblNullDiff.ClientID %>' class="col-sm-1 control-label">Разница</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblNullDiff" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst input-sm" AutoPostBack="True">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <%--                    <asp:ListItem Text="только 0" Value="0"></asp:ListItem>--%>
                    <asp:ListItem Text="кроме 0" Value="1" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <p>
                    <asp:LinkButton ID="btnShowHistory" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnShowHistory_OnClick">История установки ЗИПа</asp:LinkButton>
                        </p>
                    <%--<div class="panel-group" id="accordion">
            <div class="panel panel-default" id="pnl-history">
                <div id="historyHead" class="panel-heading">
                    <div class="panel-title collapsed" data-toggle="collapse" data-target="#historyPanel">
                        
                        <a class="title"><i class="fa fa-clock-o"></i></a>
                    </div>
                </div>
                <div id="historyPanel" class="panel-collapse collapse">
                    <div class="panel-body">--%>
                    <asp:Repeater ID="rtrClimUnitsHistory" runat="server" Visible="False">
                        <HeaderTemplate>
                            <table class="table table-striped text-smaller">
                                <tr>
                                    <td colspan="10">Последние 50 записей
                                    </td>
                                </tr>
                                <tr>
                                    <th>№
                                    </th>
                                    <th>№ заявки</th>
                                    <th>Каталожный номер
                                    </th>
                                    <th>Наименование
                                    </th>
                                    <th>Количество
                                    </th>
                                    <th>Дата создания заявки
                                    </th>
                                    <th>Статус заявки
                                    </th>
                                    <th>Счетчик
                                    </th>
                                    <th>Счетчик (цветной)
                                    </th>
                                    <th>ФИО инженера
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("row_num") %></td>
                                <td class='nowrap' style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'>
                                    <a target="_blank" href='<%# String.Format("{1}?id={0}", Eval("id_claim"), ClaimEditorForm) %>'><%#Eval("id_claim") %></a>
                                </td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("catalog_num") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("name") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("count") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("date_create") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("claim_state") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("counter") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("counter_colour") %></td>
                                <td class="nowrap" style='<%# String.Format("background-color: {0}", Eval("BgCol")) %>'><%#Eval("service_engeneer") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <%--<asp:SqlDataSource ID="sdsClimUnitsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="getClaimUnitHistoryList" Name="action" />
                                <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>

            </div>
        </div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <div class="clearfix"></div>
    <asp:Repeater ID="tblDeviceCounterHistory" runat="server" DataSourceID="sdsDeviceCounterHistory">
        <HeaderTemplate>
            <table class="table table-striped auto-width">
                <tr>
                    <th class="min-width">Дата
                    </th>
                    <th class="min-width text-right">Счетчик<br />
                        ЧБ
                    </th>
                    <th class="min-width text-right">Счетчик<br />
                        Цвет
                    </th>
                    <td class="min-width text-right bold-light text-success">Счетчик<br />
                        общий
                    </td>
                    <th class="text-right">Разница<br />
                        ЧБ</th>
                    <th class="text-right">Разница<br />
                        Цвет</th>
                    <td class="text-right bold-light text-success">Разница<br />
                        общий</td>
                    <th></th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="nowrap text-right"><%#String.Format("{0:d}", Eval("date_counter")) %></td>
                <td id="tdCounter" runat="server" class="nowrap text-right"><%# String.Format("{0:### ### ### ### ###}", Eval("counter")) %></td>
                <td id="tdCounterColor" runat="server" class="nowrap text-right"><%# String.Format("{0:### ### ### ### ###}", Eval("counter_color")) %></td>
                <td id="tdCounterTotal" runat="server" class="nowrap text-right bold-light text-success"><%# String.Format("{0:### ### ### ### ###}", Eval("counter_total")) %></td>
                <td id="tdDiff" runat="server" class="nowrap text-right"><%#Eval("volume_counter") %></td>
                <td id="tdDiffColor" runat="server" class="nowrap text-right"><%#Eval("volume_counter_color") %></td>
                <td id="tdDiffTotal" runat="server" class="nowrap text-right bold-light text-success"><%#Eval("volume_counter_total") %></td>
                <td class="nowrap"><%#Eval("name") %></td>
                <td class="nowrap">
                    <a id="fileLink" runat="server" target="_blank" href='<%# Eval("akt_scan_full_path") %>' visible='<%# !String.IsNullOrEmpty(Eval("id_akt_scan").ToString()) %>'>скан акта обслуживания №<%#Eval("id_akt_scan") %></a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsDeviceCounterHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing_reports" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getDeviceCounterDetail" Name="action" />
            <asp:QueryStringParameter QueryStringField="id" Name="id_device" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:QueryStringParameter QueryStringField="cid" Name="id_contract" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:ControlParameter DefaultValue="" ControlID="rblRowsCount" Name="rows_count" />
            <asp:ControlParameter DefaultValue="" ControlID="rblDataSource" Name="data_source" />
            <asp:ControlParameter DefaultValue="" ControlID="rblNullDiff" Name="not_null_volume" DbType="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
