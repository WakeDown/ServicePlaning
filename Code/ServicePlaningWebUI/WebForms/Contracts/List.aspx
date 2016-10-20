<%@ Page Title="Договоры - список" Language="C#" MasterPageFile="~/WebForms/Masters/List.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.List" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новый договор</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNumber" runat="server" class="form-control input-sm" MaxLength="150"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер аппарата</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterSerialNum" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtPrice.ClientID %>' class="col-sm-2 control-label">Сумма по договору</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtPrice" runat="server" class="form-control input-sm" MaxLength="15"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtPrice" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtPrice" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractTypes.ClientID %>' class="col-sm-2 control-label">Тип договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContractTypes" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
<%--        <div class="form-group">
            <label for='<%=ddlServceTypes.ClientID %>' class="col-sm-2 control-label">Тип ТО</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServceTypes" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>--%>
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
            <label for='<%=ddlContractStatus.ClientID %>' class="col-sm-2 control-label">Статус договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateBegin.ClientID %>' class="col-sm-2 control-label">Период действия</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateBegin" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата начала"></asp:TextBox>
                        <%--<span class="input-group-иет">
                        <span class="btn btn-default" onclick="$(this).datepicker('#<%=txtDateBegin.ClientID %>');"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        <%--<span class="input-group-btn">
                        <i class="glyphicon glyphicon-minus"></i>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateBegin" runat="server" ErrorMessage="Введите дату начала" CssClass="text-danger" ControlToValidate="txtDateBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateEnd" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата окончания"></asp:TextBox>
                        <%--<span class="input-group-addon">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateEnd" runat="server" ErrorMessage="Введите дату окончания" CssClass="text-danger" ControlToValidate="txtDateEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblNoLinkedDevice.ClientID %>' class="col-sm-2 control-label">Без привязанного оборудования</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblNoLinkedDevice" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="да" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <span class="help-block">
<%--                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtInstalationDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm"></asp:TextBox>
<%--                <span class="help-block">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtAge" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>--%>
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
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span></h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="10" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;"  PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnRowDataBound="tblList_RowDataBound">
        <Columns>
             <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                   <span class='row-mark <%# SetRowMark(Eval("is_active").ToString(), Eval("mark").ToString()) %>' >&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id_contract" SortExpression="id_contract" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                   <a href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, FormUrl) %>' class="btn btn-link no-padding" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                    <a href='<%# GetRedirectUrlWithParams(String.Format("ctrid={0}", Eval("id_contract")), false, ServiceClaimUrl) %>' class="btn btn-link no-padding" data-toggle="tooltip" title="новая заявка"><i class="fa fa-wrench fa-lg"></i></a>
                    <a href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, "~/Contracts/Devices/Editor") %>' class="btn btn-link no-padding" data-toggle="tooltip" title="добавить оборудование"><i class="fa fa-print fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="">
                <ItemTemplate>
                   <%# Eval("collected_name")  %>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="number" SortExpression="number" HeaderText="№ договора" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="contract_type" SortExpression="contract_type" HeaderText="Тип договора" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="sla_1_hours" SortExpression="sla_1_hours" HeaderText="SLA 1" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="sla_2_hours" SortExpression="sla_2_hours" HeaderText="SLA 2" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="sla_3_hours" SortExpression="sla_3_hours" HeaderText="SLA 3" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="sla_4_hours" SortExpression="sla_4_hours" HeaderText="SLA 4" HeaderStyle-CssClass="sorted-header" />
<%--            <asp:BoundField DataField="contractor" SortExpression="contractor" HeaderText="Контрагент" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField HeaderText="Контрагент">
                <ItemTemplate>
                    <div>
                   <%# Eval("contractor")  %></div>
                    <small>
                         <%# Eval("note")  %>
                    </small>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="price" SortExpression="price" HeaderText="Сумма" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField HeaderText="Период действия (остаток)" ItemStyle-CssClass="">
                <ItemTemplate>
                   <%# String.Format("{0:d} - {1:d} ({2} д.)", Eval("date_begin"), Eval("date_end"), Eval("residue"))  %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Оборудование">
                <ItemTemplate>
                   <ul class="list-unstyled">
                        <li>
                            <a class="btn btn-link" data-toggle="tooltip" title="перейти к списку" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, "~/Contracts/Devices") %>'><%# String.Format("{0} шт.:", Eval("device_count")) %></a>
                            <%--<a class="btn btn-link" data-toggle="tooltip" title="добавить" href='<%# String.Format("{0}?id={1}", FriendlyUrl.Href("~/Contracts/Devices/Editor"), Eval("id_contract")) %>'><i class="fa fa-plus fa-fw"></i></a>--%>
                            <asp:Repeater ID="rtrContract2DevicesList" runat="server" DataSourceID="sdsContract2DevicesList">
                                <ItemTemplate>
                                    <li>
                                        <h5 class="small nomargin pad-l-sm"><%#String.Format("{0} - {1}", Eval("count"), Eval("service_interval")) %></h5>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                    <asp:SqlDataSource ID="sdsContract2DevicesList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getContract2DevicesIntervalGroupList" Name="action" DbType="String" />
                            <asp:Parameter DefaultValue="-1" Name="id_contract" DbType="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                   <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_contract") %>' OnClientClick="return DeleteConfirm('договор')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getContractList" Name="action" />
            <asp:QueryStringParameter QueryStringField="number" Name="number" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="price" Name="price" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctype" Name="id_contract_type" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="stype" Name="id_service_type" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctrtr" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="state" Name="id_contract_status" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="mngr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="nodvl" Name="no_device_linked" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int16" />
            <asp:QueryStringParameter QueryStringField="sernum" Name="serial_num" DefaultValue="" ConvertEmptyStringToNull="True" />
             <asp:QueryStringParameter Name="rows_count" QueryStringField="rcnt" DefaultValue="10" ConvertEmptyStringToNull="True" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
