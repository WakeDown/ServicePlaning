<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="PlanExecute.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <asp:RadioButtonList ID="rblReportType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblReportType_OnSelectedIndexChanged">
        <%--        <asp:ListItem Value="dev">&nbsp;Отчет по аппаратам</asp:ListItem>--%>
        <asp:ListItem Value="execEng">&nbsp;Отчет по инженерам</asp:ListItem>
        <asp:ListItem Value="execEngAlloc">&nbsp;Распределение по инженерам</asp:ListItem>
        <asp:ListItem Value="execServAdmins">&nbsp;Отчет по серв. администраторам</asp:ListItem>
        <asp:ListItem Value="execServManagers">&nbsp;Отчет по менеджерам</asp:ListItem>
    </asp:RadioButtonList>
    <asp:LinkButton ID="btnSlideshow" runat="server" Visible="False" class="btn btn-default btn-sm" OnClick="btnSlideshow_OnClick" ValidationGroup="vgFilter"><i class="fa fa-film"></i>&nbsp;запуск слайдшоу</asp:LinkButton>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_OnTick" Interval="300000" Enabled="False"></asp:Timer>
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
            <label for='<%=ddlServiceManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceManager" runat="server" CssClass="form-control input-sm">
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
        <div class="form-group" style="display:none">
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
    <asp:Label ID="lblLasRefresh" runat="server" Text="<%#DateTime.Now.ToString() %>" Visible="False"></asp:Label>
    <asp:MultiView ID="mvReports" runat="server">
        <asp:View ID="vNull" runat="server"></asp:View>
        <asp:View ID="vReportAllocEngeneers" runat="server">
            <asp:Repeater ID="tblEngeneersAlloc" runat="server" DataSourceID="sdsEngeneersAlloc" OnItemDataBound="tblEngeneersAlloc_OnItemDataBound">
                <HeaderTemplate>
                    <table class="table">
                        <tr>
                            <th></th>
                            <th>ФИО инженера</th>
                            <th>
                                <div>План</div>
                                <div>Факт</div>
                                <div>Осталось</div>
                            </th>
                            <th class='<%# Request.QueryString["nrmord"] == "1" ? "header-asc" : Request.QueryString["nrmord"] == "0" ? "header-desc" : "sorted-header" %>'> 
                                <a href='<%# Request.QueryString["nrmord"] == "1" ? GetRedirectUrlWithParams("nrmord=") : Request.QueryString["nrmord"] == "0" ? GetRedirectUrlWithParams("nrmord=1") : GetRedirectUrlWithParams("nrmord=0") %>'>Норма</a>
                                

                            </th>
                            <asp:Repeater ID="rtrDaysInMonth" runat="server" DataSourceID="sdsDaysInMonth">
                                <ItemTemplate>
                                    <th>
                                        <%#Eval("dasplay_day") %>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsDaysInMonth" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="getDaysInMonth" Name="action" />
                                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><div class="row-mark row-mark-big <%# SetRowMark(Eval("norm_exec_percent").ToString()) %>">&nbsp;&nbsp;</div></td>
                        <td class="text-middle"><div class="nowrap" data-toggle="tooltip" title='<%#String.Format("{0}, {1}, {2}", Eval("engeneer_city"), Eval("engeneer_org"), Eval("engeneer_pos")) %>'><%#Eval("service_engeneer") %></div></td>
                        <td class="text-middle">
                            <div class="align-right"><%#Eval("cnt") %></div>
                            <div class="align-right"><%#Eval("exec_claims") %></div>
                            <div class="align-right"><%#Eval("residue") %></div>
                        </td>
                        <td>
                            <div class="align-right"><%#String.Format("({1:N2}) {0:N2}",Eval("norm"), Eval("cur_norm")) %></div>
                            <div class="align-right"><%#String.Format("{0:N2}",Eval("cur_norm_exec")) %></div>
                            <div class='align-right <%# SetMarkExecDiff(Eval("exec_diff").ToString()) %>'><%#String.Format("{0:N2}",Eval("exec_diff")) %></div>
                        </td>
                        <asp:HiddenField ID="hfIdServiceEngeneer" runat="server" Value='<%#Eval("id_service_engeneer") %>' />
                        <asp:HiddenField ID="hfClaimsCount" runat="server" Value='<%#Eval("cnt") %>' />
                        <asp:HiddenField ID="hfAlreadyExec" runat="server" Value='<%#Eval("exec_claims") %>' />
                        <asp:HiddenField ID="hfLastExecDay" runat="server" Value='<%#Eval("last_exec_day") %>' />

                        <asp:PlaceHolder ID="phDaysAlloc" runat="server">
                            
                        </asp:PlaceHolder>
                        <%--<asp:Repeater ID="rtrDaysInMonth" runat="server" DataSourceID="sdsDaysInMonth" OnItemDataBound="rtrDaysInMonth_OnItemDataBound">
                            <ItemTemplate>
                                <td id="tdDayAlloc" runat="server" class="text-center text-middle"></td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:SqlDataSource ID="sdsDaysInMonth" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="getDaysInMonth" Name="action" />
                                <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                            </SelectParameters>
                        </asp:SqlDataSource>--%>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="sdsEngeneersAlloc" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getEngeneerAllocList" Name="action" />
                    <asp:QueryStringParameter QueryStringField="eng" Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                    <asp:QueryStringParameter QueryStringField="nrmord" Name="norm_order" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int16" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
        <asp:View ID="vReportExecEngeneers" runat="server">
            <div class="pnl-btns">
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-primary" OnClick="btnPrint_OnClick"><i class="fa fa-print"></i>&nbsp;распечатать выбранные<%--&nbsp;<asp:Label ID="lChecksCount" runat="server" CssClass="badge" Text="0"></asp:Label>--%></asp:LinkButton>
                <asp:HiddenField ID="hfCheckedDeviceIds" runat="server" />
            </div>
            <div class="table" id="tblListExecEngeneersContainer" runat="server">
                <asp:Repeater ID="tblListExecEngeneers" runat="server" DataSourceID="sdsListExecByEngeneers" OnItemDataBound="tblListExecEngeneers_OnItemDataBound">
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
                        <div class="row bg collapsed row-action" data-toggle="collapse" data-target='<%# String.Format("#contractorList{0}", Container.ItemIndex) %>'>
                            <div class="col-sm-3"><%#Eval("service_engeneer") %></div>
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
                        <div id='<%# String.Format("contractorList{0}", Container.ItemIndex) %>' class="collapse">
                            <asp:PlaceHolder ID="phListExecEngeneersContractorsDevices" runat="server"></asp:PlaceHolder>
                            <%--<asp:Repeater ID="tblListExecContractors" runat="server" OnItemDataBound="tblListExecContractors_OnItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row collapsed row-action bg-white" data-toggle="collapse" data-target='<%# String.Format("#deviceList{0}Ctr{1}", Eval("id_service_engeneer"), Eval("id_contractor")) %>'>
                                        <div class="col-sm-3">
                                            <div class="tristate-container">
                                                <div id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                                                    <input id="chkTristate" runat="server" type="hidden" />
                                                </div>
                                            </div>
                                            <span class="pad-l-sm"><%#Eval("contractor") %></span>&nbsp;<small>отмечено&nbsp;<span id="lChecksCountInner" runat="server" class="bold text-success">0</span></small>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="chrt-plan-light">
                                                <div class="chrt-exec-light" style='<%# String.Format("width: {0}%", Eval("done_percent")) %>'></div>
                                            </div>
                                        </div>

                                        <div class="col-sm-1 align-right"><%#Eval("plan_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("residue") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                    </div>
                                    <div id='<%# String.Format("deviceList{0}Ctr{1}", Eval("id_service_engeneer"), Eval("id_contractor")) %>' class="collapse">
                                        <asp:Repeater ID="tblListExecDevice" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <input id="chkSetClaim" runat="server" value='<%#Eval("id_service_claim") %>' type="checkbox" />
                                                        <span class="pad-l-mid"><%#Eval("device") %></span>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <%#Eval("city") %>,&nbsp;<%#Eval("address") %>
                                                    </div>
                                                    <div class="col-sm-1"><%# String.Format("{0:dd.MM.yyyy}", Eval("date_came")) %></div>
                                                    <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="sdsListExecDevice" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="getPlanExecuteDeviceList" Name="action" />
                                                <asp:Parameter Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                                <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsListExecContractors" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="getPlanExecuteContractorList" Name="action" />
                                    <asp:Parameter Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                    <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
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
                                <asp:Literal ID="lPlanSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDoneSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lResitudeSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDonePercentSum" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sdsListExecByEngeneers" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getPlanExecuteEngeneerList" Name="action" />
                        <asp:QueryStringParameter QueryStringField="eng" Name="id_service_engeneer" DefaultValue="" ConvertEmptyStringToNull="True" />
                        <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                        <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </asp:View>
        <asp:View ID="vReportExecServAdmins" runat="server">
            <div class="table">
                <asp:Repeater ID="tblListExecServAdmins" runat="server" DataSourceID="sdsListExecByServAdmins" OnItemDataBound="tblListExecServAdmins_OnItemDataBound">
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
                        <div class="row bg collapsed row-action" data-toggle="collapse" data-target='<%# String.Format("#contractorList{0}", Container.ItemIndex) %>'>
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
                        <div id='<%# String.Format("contractorList{0}", Container.ItemIndex) %>' class="collapse">
                            <asp:PlaceHolder ID="phListExecServAdminContractorsDevices" runat="server"></asp:PlaceHolder>
                            <%--<asp:Repeater ID="tblListExecServAdminContractors" runat="server" OnItemDataBound="tblListExecServAdminContractors_OnItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row collapsed row-action bg-white" data-toggle="collapse" data-target='<%# String.Format("#deviceList{0}Ctr{1}", Eval("id_service_admin"), Eval("id_contractor")) %>'>
                                        <div class="col-sm-3"><span class="pad-l-sm"><%#Eval("contractor") %></span></div>
                                        <div class="col-sm-3">
                                            <div class="chrt-plan-light">
                                                <div class="chrt-exec-light" style='<%# String.Format("width: {0}%", Eval("done_percent")) %>'></div>
                                            </div>
                                        </div>

                                        <div class="col-sm-1 align-right"><%#Eval("plan_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("residue") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                    </div>
                                    <div id='<%# String.Format("deviceList{0}Ctr{1}", Eval("id_service_admin"), Eval("id_contractor")) %>' class="collapse">
                                        <asp:Repeater ID="tblListExecDevice" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-3"><span class="pad-l-mid"><%#Eval("device") %></span></div>
                                                    <div class="col-sm-3">
                                                        <%#Eval("city") %>,&nbsp;<%#Eval("address") %>
                                                    </div>
                                                    <div class="col-sm-2"><%#Eval("service_engeneer") %></div>
                                                    <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="sdsListExecDevice" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="getPlanExecuteDeviceList" Name="action" />
                                                <asp:Parameter Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                                <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsListExecServAdminContractors" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="getPlanExecuteServAdminContractorList" Name="action" />
                                    <asp:Parameter Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="True" />
                                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                    <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
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
                                <asp:Literal ID="lPlanSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDoneSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lResitudeSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDonePercentSum" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sdsListExecByServAdmins" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getPlanExecuteServAdminList" Name="action" />
                        <asp:QueryStringParameter QueryStringField="sadm" Name="id_service_admin" DefaultValue="" ConvertEmptyStringToNull="True" />
                        <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                        <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </asp:View>
        <asp:View ID="vReportExecManagers" runat="server">
            <div class="table">
                <asp:Repeater ID="tblListExecServManagers" runat="server" DataSourceID="sdsListExecByServManagers" OnItemDataBound="tblListExecServManagers_OnItemDataBound">
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
                        <div class="row bg collapsed row-action" data-toggle="collapse" data-target='<%# String.Format("#contractorList{0}", Container.ItemIndex) %>'>
                            <div class="col-sm-3"><%#Eval("manager") %></div>
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
                        <div id='<%# String.Format("contractorList{0}", Container.ItemIndex) %>' class="collapse">
                            <asp:PlaceHolder ID="phListExecServManagersContractorsDevices" runat="server"></asp:PlaceHolder>
                            <%--<asp:Repeater ID="tblListExecManagersContractors" runat="server" OnItemDataBound="tblListExecManagersContractors_OnItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row collapsed row-action bg-white" data-toggle="collapse" data-target='<%# String.Format("#deviceList{0}Ctr{1}", Eval("id_manager"), Eval("id_contractor")) %>'>
                                        <div class="col-sm-3"><span class="pad-l-sm"><%#Eval("contractor") %></span></div>
                                        <div class="col-sm-3">
                                            <div class="chrt-plan-light">
                                                <div class="chrt-exec-light" style='<%# String.Format("width: {0}%", Eval("done_percent")) %>'></div>
                                            </div>
                                        </div>

                                        <div class="col-sm-1 align-right"><%#Eval("plan_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_cnt") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("residue") %></div>
                                        <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                    </div>
                                    <div id='<%# String.Format("deviceList{0}Ctr{1}", Eval("id_manager"), Eval("id_contractor")) %>' class="collapse">
                                        <asp:Repeater ID="tblListExecDevice" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-3"><span class="pad-l-mid"><%#Eval("device") %></span></div>
                                                    <div class="col-sm-4">
                                                        <%#Eval("city") %>,&nbsp;<%#Eval("address") %>
                                                    </div>
                                                    <div class="col-sm-1"><%#Eval("service_engeneer") %></div>
                                                    <div class="col-sm-1"><%# String.Format("{0:dd.MM.yyyy}", Eval("date_came")) %></div>
                                                    <div class="col-sm-1 align-right"><%#Eval("done_percent") %>%</div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="sdsListExecDevice" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="getPlanExecuteDeviceList" Name="action" />
                                                <asp:Parameter Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                                <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                                <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsListExecManagersContractors" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="getPlanExecuteServManagerContractorList" Name="action" />
                                    <asp:Parameter Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
                                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                                    <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                                    <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
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
                                <asp:Literal ID="lPlanSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDoneSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lResitudeSum" runat="server"></asp:Literal>
                            </div>
                            <div class="col-sm-1 align-right">
                                <asp:Literal ID="lDonePercentSum" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sdsListExecByServManagers" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getPlanExecuteServManagerList" Name="action" />
                        <asp:QueryStringParameter QueryStringField="mgr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
                        <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
                        <asp:QueryStringParameter Name="is_done" QueryStringField="dne" DefaultValue="-13" ConvertEmptyStringToNull="True" DbType="Int16" />
                        <asp:QueryStringParameter Name="no_set" QueryStringField="nst" DefaultValue="1" ConvertEmptyStringToNull="True" DbType="Int16" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </asp:View>
    </asp:MultiView>


</asp:Content>
