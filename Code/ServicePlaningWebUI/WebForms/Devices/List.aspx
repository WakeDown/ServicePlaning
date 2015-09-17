<%@ Page Title="Оборудование - список" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Devices.List" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <div class="btn-group">
        <a class="btn btn-primary btn-lg" type="button" href='<%=  GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новый аппарат</a>
    </div>
    <%--    <asp:LinkButton ID="btnAdd2Contract" runat="server">привязать отмеченные</asp:LinkButton>--%>
    <div class="btn-group">
        <button type="button" class="btn btn-primary btn-lg dropdown-toggle" data-toggle="dropdown">
            <span id="lChecksCount" runat="server" class="badge">0</span> отмеченные <i class="caret"></i>
        </button>
        <ul class="dropdown-menu" role="menu">
            <li>
                <asp:LinkButton ID="btnCheckedAdd2Contract" runat="server" OnClick="btnCheckedAdd2Contract_OnClick"><i class="fa fa-external-link"></i> привязать к договору</asp:LinkButton>
<%--                <a id="btnAddChecked" runat="server" href='javascript:void(0)'>привязать к договору</a>--%>
            </li>
            <li><a id="btnCheckedIdsClear" runat="server" href='javascript:void(0)'><i class="fa fa-flag-checkered"></i> снять флажки</a></li>
            <li class="divider"></li>
            <li>
                 <asp:LinkButton ID="btnCheckedDelete" runat="server" OnClick="btnCheckedDelete_OnClick" OnClientClick="return DeleteConfirm('выбранные аппараты')"><i class="fa fa-trash-o"></i> удалить</asp:LinkButton>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlModel.ClientID %>' class="col-sm-2 control-label">Модель</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtModelSelection" runat="server" class="form-control input-sm width-20" placeholder="поиск" MaxLength="33"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtModel" runat="server" class="form-control input-sm"></asp:TextBox>--%>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSerialNum" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSpeed.ClientID %>' class="col-sm-2 control-label">Скорость печати</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSpeed" runat="server" class="form-control input-sm"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlDeviceImprint.ClientID %>' class="col-sm-2 control-label">Отпечаток</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlDeviceImprint" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlPrintType.ClientID %>' class="col-sm-2 control-label">Тип печати</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlPrintType" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlCartridgeType.ClientID %>' class="col-sm-2 control-label">Тип картриджа</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCartridgeType" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounter.ClientID %>' class="col-sm-2 control-label">Счетчик</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtCounter" runat="server" class="form-control input-sm"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtCounter" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtAge.ClientID %>' class="col-sm-2 control-label">Возраст</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtAge" runat="server" class="form-control input-sm"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtAge" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtAge" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtInstalationDate.ClientID %>' class="col-sm-2 control-label">Дата установки</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txtInstalationDate" runat="server" CssClass="form-control  datepicker-btn input-sm"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtInstalationDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtInstalationDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblIsLinked.ClientID %>' class="col-sm-2 control-label">Привязан к договору</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblIsLinked" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="да" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <%//TODO: Переделать вставку списка в коде, можно сделать метод который заполняет Список (Дроп или Радио) как тройственный флаг (все, да, нет) %>
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
                <%--                <asp:LinkButton ID="btnFilterClear" runat="server" class="btn btn-default" OnClick="btnFilterClear_OnClick"><i class="glyphicon glyphicon-refresh"></i>&nbsp;очистить</asp:LinkButton>--%>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <h5 class="pull-left"><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span></h5>
    <%--    <h5 class="pull-left"><span class="label label-primary"> из них выделено:</span></h5>--%>
    <asp:HiddenField ID="hfCheckedDeviceIds" runat="server" />
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="10" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-right&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                   <span class='row-mark <%# SetRowMark(Eval("linked_now").ToString()) %>' >&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <HeaderTemplate>
                    <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                        <input id="chkTristate" runat="server" type="hidden" />
                    </span>
                </HeaderTemplate>
                <ItemTemplate>
                    <input value='<%#Eval("id_device") %>' type="checkbox" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id_device" SortExpression="id_device" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
<%--                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_device")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="на договоре"><i class="fa fa-external-link-square fa-lg"></i></a>--%>
                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_device")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                    <asp:LinkButton ID="btnAdd2Contract" runat="server" OnClick="btnAdd2Contract_OnClick" CommandArgument='<%#Eval("id_device") %>' CssClass="btn btn-link" data-toggle="tooltip" title="привязать к договору"><i class="fa fa-external-link-square fa-lg"></i></asp:LinkButton>
                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, ContracDevtFormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="список оборудования"><i class="fa fa-print fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="model" SortExpression="model" HeaderText="Модель" HeaderStyle-CssClass="sorted-header" />
<%--            <asp:BoundField DataField="speed" SortExpression="speed" HeaderText="Скорость печати" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:BoundField DataField="serial_num" SortExpression="serial_num" HeaderText="Серийный номер" HeaderStyle-CssClass="sorted-header" />
            <%--<asp:BoundField DataField="device_imprint" SortExpression="device_imprint" HeaderText="Отпечаток" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="print_type" SortExpression="print_type" HeaderText="Тип печати" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="cartridge_type" SortExpression="cartridge_type" HeaderText="Тип картриджа" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:BoundField DataField="counter" SortExpression="counter" HeaderText="Счетчик" HeaderStyle-CssClass="sorted-header" />
<%--            <asp:BoundField DataField="age" SortExpression="age" HeaderText="Возраст" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField  SortExpression="age" HeaderText="Возраст" HeaderStyle-CssClass="sorted-header">
                <ItemTemplate>
                    <%# Eval("age").ToString() == "0" ? "Гарантия" : Eval("age").ToString()  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="instalation_date" SortExpression="instalation_date" HeaderText="Дата установки" DataFormatString="{0:d}" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField  SortExpression="contract_num" HeaderText="№ договора" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <span class="nowrap"><a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, ContracFormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="к договору"><i class="fa fa-sign-out"></i></a>&nbsp;
                    <%#Eval("contract_num") %></span>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:BoundField DataField="contract_num" SortExpression="contract_num" HeaderText="№ договора" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_device") %>' OnClientClick="return DeleteConfirm('аппарат')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getDeviceList" Name="action" />
            <asp:QueryStringParameter Name="id_device_model" QueryStringField="model" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="serial_num" QueryStringField="sernum" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="speed" QueryStringField="speed" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="id_device_imprint" QueryStringField="imprint" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="id_print_type" QueryStringField="printype" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="id_cartridge_type" QueryStringField="cartype" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="counter" QueryStringField="counter" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="age" QueryStringField="age" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="instalation_date" QueryStringField="insdate" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="is_linked" QueryStringField="islnk" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int16" />
            <asp:QueryStringParameter Name="rows_count" QueryStringField="rcnt" DefaultValue="10" ConvertEmptyStringToNull="True" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

