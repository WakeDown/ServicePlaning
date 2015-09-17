<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Site.Master" AutoEventWireup="true" CodeBehind="ExecReport.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Output.ExecReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    test
    <%--<asp:Repeater ID="tblListExecServAdmins" runat="server" DataSourceID="sdsListExecByServAdmins" OnItemDataBound="tblListExecServAdmins_OnItemDataBound">
                    <HeaderTemplate>
                        <div class="row header">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-3">
                                % выполнения
                            </div>
                            <div class="col-sm-1">План</div>
                            <div class="col-sm-1">Выполнено</div>
                            <div class="col-sm-1">Невыполнено</div>
                            <div class="col-sm-1 nowrap">% выполнения</div>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="row bg">
                            <div class="col-sm-3"><%#Eval("service_admin") %></div>
                            <div class="col-sm-3">
                                <div class="chrt-plan">
                                    <div class="chrt-exec" style='<%# String.Format("width: {0}%", Eval("done_percent")) %>'></div>
                                </div>
                            </div>
                            
                            <div class="col-sm-1 align-right"><%#Eval("plan_cnt") %></div>
                            <div class="col-sm-1 align-right"><%#Eval("done_cnt") %></div>
                            <div class="col-sm-1 align-right"><%#Eval("residue") %></div>
                            <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div class="row header">
                            <div class="col-sm-3 align-right">Всего</div>
                            <div class="col-sm-3">
                                <div class="chrt-plan">
                                    <div id="footerChart" runat="server" class="chrt-exec" style='<%# String.Format("width: {0}%", Eval("done_percent")) %>'></div>
                                </div>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lPlanSum" runat="server"></asp:Literal></div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDoneSum" runat="server"></asp:Literal></div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lResitudeSum" runat="server"></asp:Literal></div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDonePercentSum" runat="server"></asp:Literal></div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
    <asp:SqlDataSource ID="sdsListExecByServAdmins" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getPlanExecuteServAdminList" Name="action" />
                        <asp:QueryStringParameter QueryStringField="eng" Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="True" />
                        <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                        <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
</asp:Content>
