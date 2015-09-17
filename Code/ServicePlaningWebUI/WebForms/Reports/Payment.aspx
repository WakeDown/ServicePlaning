<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.Payment1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <asp:RadioButtonList ID="rblReportType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblReportType_OnSelectedIndexChanged">
        <asp:ListItem Value="eng">&nbsp;Отчет по инженерам</asp:ListItem>
        <asp:ListItem Value="servAdm">&nbsp;Отчет по серв. администраторам</asp:ListItem>
    </asp:RadioButtonList>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlServiceEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceEngeneer" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceAdmin.ClientID %>' class="col-sm-2 control-label">Серв. администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateMonth.ClientID %>' class="col-sm-2 control-label">Месяц</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtDateMonth" runat="server" CssClass="form-control datepicker-btn-month"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPlaningDate" runat="server" ErrorMessage="Заполните поле &laquo;Месяц&raquo;" Display="Dynamic" ControlToValidate="txtDateMonth" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtDateMonth" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
<%--        <div class="form-group">
            <label for='<%=txtDateClaimBegin.ClientID %>' class="col-sm-2 control-label">Период</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateClaimBegin" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата начала"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateClaimBegin" runat="server" ErrorMessage="Введите дату начала" CssClass="text-danger" ControlToValidate="txtDateClaimBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateClaimEnd" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата окончания"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateClaimEnd" runat="server" ErrorMessage="Введите дату окончания" CssClass="text-danger" ControlToValidate="txtDateClaimEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>--%>

        <%--        <div class="form-group">
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>--%>
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
    
    <asp:MultiView ID="mvReports" runat="server">
    <asp:View ID="vReportEng" runat="server">
            <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCountDev" runat="server" Text="0"></asp:Literal></span>
                <span class="label label-default">Общая сумма:
        <asp:Literal ID="lSummCountDev" runat="server" Text="0"></asp:Literal></span>
            </h5>
            <asp:GridView ID="tblListDev" runat="server" CssClass="table table-striped" DataSourceID="sdsListDev" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="10" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-right&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblListDev_DataBound" EmptyDataText="По вашему запросу ничего не найдено">
                <Columns>
                    <asp:BoundField DataField="device" SortExpression="device" HeaderText="Аппарат" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="date_came" SortExpression="date_came" HeaderText="Дата" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="payment" SortExpression="payment" HeaderText="Сумма оплаты" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="service_engeneer" SortExpression="service_engeneer" HeaderText="Инженер" HeaderStyle-CssClass="sorted-header" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsListDev" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getPaymentList" Name="action" />
                    <asp:QueryStringParameter QueryStringField="eng" Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                    <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                    <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
        <asp:View ID="vReportServAdm" runat="server">
            <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCountSa" runat="server" Text="0"></asp:Literal></span>
                <span class="label label-default">Общая сумма:
        <asp:Literal ID="lSummCountSa" runat="server" Text="0"></asp:Literal></span>
            </h5>
            <asp:GridView ID="tblServiceAdminsPaymentList" runat="server" CssClass="table table-striped" DataSourceID="sdsServiceAdminsPaymentList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="10" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-right&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblServiceAdminsPaymentList_DataBound" EmptyDataText="По вашему запросу ничего не найдено" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="user_name" SortExpression="user_name" HeaderText="ФИО" HeaderStyle-CssClass="sorted-header" FooterStyle-CssClass="bold" FooterText="Всего" />
                    <asp:BoundField DataField="role_name" SortExpression="role_name" HeaderText="Должность" HeaderStyle-CssClass="sorted-header" FooterStyle-CssClass="bold" />
<%--                    <asp:BoundField DataField="device_count" SortExpression="device_count" HeaderText="Всего аппаратов" HeaderStyle-CssClass="sorted-header" FooterStyle-CssClass="bold" />--%>
                    <asp:BoundField DataField="cames_count" SortExpression="cames_count" HeaderText="Доля обслуженных аппаратов" HeaderStyle-CssClass="sorted-header" FooterStyle-CssClass="bold" />
                    <asp:BoundField DataField="base_price" SortExpression="base_price" HeaderText="Базовая ставка" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="payment" SortExpression="payment" HeaderText="Сумма оплаты" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:C}" FooterStyle-CssClass="bold" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsServiceAdminsPaymentList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getServiceAdminsPaymentList" Name="action" />
                    <asp:QueryStringParameter QueryStringField="sadm" Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="True" />
<%--                    <asp:QueryStringParameter QueryStringField="eng" Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />--%>
                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                </SelectParameters>
            </asp:SqlDataSource>
            </asp:View>
        </asp:MultiView>
</asp:Content>
