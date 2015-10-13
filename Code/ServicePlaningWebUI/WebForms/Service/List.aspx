<%@ Page Title="Обслуживание - список" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новый выезд</a>
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, PlanUrl) %>'>новый план</a>
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormCameUrl) %>'>ввод актов</a>
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormCameScanUrl) %>'>ввод актов (скан)</a>
    <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, EngeneerSetUrl) %>'>назначить инженеров</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlDevice.ClientID %>' class="col-sm-2 control-label">Аппарат</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtDeviceSelection" runat="server" class="form-control input-sm width-20" placeholder="поиск" MaxLength="33"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceAdmin.ClientID %>' class="col-sm-2 control-label">Сервисный администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtClaimNum.ClientID %>' class="col-sm-2 control-label">№ выезда</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtClaimNum" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtContractNum.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtContractNum" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="input-group full-width">
                            <span class="input-group-btn width-20">
                                <asp:TextBox ID="txtContractorSelection" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30" AutoPostBack="True" OnTextChanged="txtContractorSelection_OnTextChanged"></asp:TextBox>
                            </span>
                            <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                            </asp:DropDownList>
                            <span class="help-block">
                                <%--<asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                            </span>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceTypes.ClientID %>' class="col-sm-2 control-label">Тип обслуживания</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceTypes" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceActionTypes.ClientID %>' class="col-sm-2 control-label">Тип ТО</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceActionTypes" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceEngeneer" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlCreator.ClientID %>' class="col-sm-2 control-label">Кем создано</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCreator" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateClaimBegin.ClientID %>' class="col-sm-2 control-label">Период (план)</label>
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
        </div>
        <div class="form-group">
            <label for='<%=txtDateMonthPlan.ClientID %>' class="col-sm-2 control-label">Месяц (план)</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtDateMonthPlan" runat="server" CssClass="form-control input-sm datepicker-btn-month"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RegularExpressionValidator ID="revTxtDateMonthPlan" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtDateMonthPlan" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateCameBegin.ClientID %>' class="col-sm-2 control-label">Период (факт)</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateCameBegin" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата начала"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateCameBegin" runat="server" ErrorMessage="Введите дату начала" CssClass="text-danger" ControlToValidate="txtDateCameBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateCameEnd" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата окончания"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateCameEnd" runat="server" ErrorMessage="Введите дату окончания" CssClass="text-danger" ControlToValidate="txtDateCameEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceClaimStatus.ClientID %>' class="col-sm-2 control-label">Статус</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlServiceClaimStatus" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <%--                <asp:RadioButtonList ID="rblServiceClaimStatus" runat="server" RepeatDirection="Horizontal" CssClass="form-control  chk-lst"></asp:RadioButtonList>--%>
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
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="False" AllowPaging="False" PageSize="50" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <span class='row-mark <%# SetRowMark(Eval("claim_status").ToString()) %>'>&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id_service_claim" SortExpression="id_service_claim" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <a href='<%# GetRedirectUrlWithParams(String.Format("ctrid={1}&id={0}", Eval("id_service_claim"), Eval("id_contract")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать" <%# DisableIfDoneStr(Eval("claim_status").ToString()) %>><i class="fa fa-edit fa-lg"></i></a>
                    <a href='<%# GetRedirectUrlWithParams(String.Format("id={0}&clid={1}", Eval("id_service_came"), Eval("id_service_claim")), false, FormCameUrl) %>' class="btn btn-link" data-toggle="tooltip" title="отметка об обслуживании" <%# DisableIfDoneStr(Eval("claim_status").ToString()) %>><i class="fa fa-cog fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:TemplateField>
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="device" SortExpression="device" HeaderText="Аппарат" HeaderStyle-CssClass="sorted-header" HtmlEncode="false" />

            <asp:BoundField DataField="contractor" SortExpression="contractor" HeaderText="Контрагент" HeaderStyle-CssClass="sorted-header" />

            <asp:BoundField DataField="planing_date" SortExpression="planing_date" HeaderText="Месяц (план)" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:MMMM yyyy}" ItemStyle-CssClass="nowrap" />
            <asp:BoundField DataField="date_came" SortExpression="date_came" HeaderText="Дата выезда" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:d}" ItemStyle-CssClass="nowrap" />
            <%--<asp:TemplateField HeaderText="Оборудование">
                <ItemTemplate>
                   <ul class="list-unstyled">
                        <li>
                            <a class="btn btn-link" data-toggle="tooltip" title="перейти к списку" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, "~/Contracts/Devices/Editor") %>'><%# String.Format("{0} шт.:", Eval("device_count")) %></a>
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
                            <asp:Parameter DefaultValue="" Name="action" DbType="String" />
                            <asp:Parameter DefaultValue="-1" Name="id_contract" DbType="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="service_engeneer" SortExpression="service_engeneer" HeaderText="ФИО инженера" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="nowrap" />
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_service_claim") %>' OnClientClick="return DeleteConfirm('заявку')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить" Visible='<%#UserIsSysAdmin || UserIsServiceTech %>'><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="action" />
            <asp:QueryStringParameter QueryStringField="clnum" Name="claim_num" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctnum" Name="number" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="dev" Name="id_device" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctr" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="sttpe" Name="id_service_type" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="satpe" Name="id_service_action_type" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="eng" Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="cre" Name="id_creator" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="dclst" Name="date_claim_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
            <asp:QueryStringParameter QueryStringField="dclen" Name="date_claim_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
            <asp:QueryStringParameter QueryStringField="dcst" Name="date_came_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
            <asp:QueryStringParameter QueryStringField="dcen" Name="date_came_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
            <asp:QueryStringParameter QueryStringField="ste" Name="id_service_claim_status" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter DefaultValue="" QueryStringField="sadm" Name="id_service_admin" ConvertEmptyStringToNull="true" />
            <asp:QueryStringParameter Name="rows_count" QueryStringField="rcnt" DefaultValue="30" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
