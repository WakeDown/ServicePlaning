<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="Counters.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.Counters" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <asp:Button ID="btnInExcel" runat="server" Text="в Excel" OnClick="btnInExcel_OnClick" CssClass="btn btn-primary" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
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
                    <asp:TextBox ID="txtDateMonth" runat="server" CssClass="form-control datepicker-btn-month input-sm"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPlaningDate" runat="server" ErrorMessage="Заполните поле &laquo;Месяц&raquo;" Display="Dynamic" ControlToValidate="txtDateMonth" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtDateMonth" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtWearBegin.ClientID %>' class="col-sm-2 control-label">Износ</label>
            <div class="row">
                <div class="col-sm-1">
                    <div class="input-group">
                        <asp:TextBox ID="txtWearBegin" runat="server" CssClass="form-control input-sm" placeholder=">="></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtWearBegin" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtWearBegin" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-1">
                    <div class="input-group">
                        <asp:TextBox ID="txtWearEnd" runat="server" CssClass="form-control input-sm" placeholder="<="></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtWearEnd" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtWearEnd" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtLoadingBegin.ClientID %>' class="col-sm-2 control-label">Загрузка</label>
            <div class="row">
                <div class="col-sm-1">
                    <div class="input-group">
                        <asp:TextBox ID="txtLoadingBegin" runat="server" CssClass="form-control input-sm" placeholder=">="></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtLoadingBegin" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtLoadingBegin" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-1">
                    <div class="input-group">
                        <asp:TextBox ID="txtLoadingEnd" runat="server" CssClass="form-control input-sm" placeholder="<="></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtLoadingEnd" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtLoadingEnd" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=chklVendor.ClientID %>' class="col-sm-2 control-label">Производитель</label>
            <div class="col-sm-10">
                <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklVendor" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblHasCames.ClientID %>' class="col-sm-2 control-label">Обслуженные</label>
            <div class="col-sm-10">
                <asp:RadioButtonList ID="rblHasCames" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
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
    <div id="pnlReport" runat="server">
        <div class="table">
            <div class="row row-total">
                <div class="col-sm-3 bold align-right">ИТОГО</div>
                <div class="col-sm-1 align-right">
                    <div class="col-sm-6 align-right"><span id="sDevCountTotal" runat="server" class="align-right"></span></div>
                    <div class="col-sm-6 align-right"><span id="sDevCountWithVolTotal" runat="server" class="align-right"></span></div>
                </div>
                <div class="col-sm-1 align-right"><span id="sCurVolTotal" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sPrevVolTotal" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sPrevPrevVolTotal" runat="server"></span></div>
                <div class="col-sm-1 align-right"></div>
                <div class="col-sm-1 align-right"></div>
                <div class="col-sm-1 align-right"></div>
                <div class="col-sm-1 align-right"></div>
                <div class="col-sm-1 align-right"></div>
            </div>
        </div>
        <asp:Repeater ID="tblCounterReport" runat="server" OnItemDataBound="tblCounterReport_OnItemDataBound" OnDataBinding="tblCounterReport_OnDataBinding">
            <%--        DataSourceID="sdsCounterReport"--%>
            <HeaderTemplate>
                <div class="table table-striped">
                    <div class="row">
                        <div class="col-sm-3">
                            <div id="exclo1" class="exclo-container">
                                1
                                <%--                            <a id="exclo1" class="exclo-btn">1</a>--%>
                            </div>
                            <div id="exclo2" class="exclo-container">
                                2
                                <%--                            <a id="exclo2" class="exclo-btn">2</a>--%>
                            </div>
                            <div id="exclo3" class="exclo-container">
                                3
                                <%--                            <a id="exclo3" class="exclo-btn">3</a>--%>
                            </div>
                        </div>
                        <div class="col-sm-1 bold align-right">Аппараты</div>
                        <div class="col-sm-3 bold center">Объем печати</div>
                        <div class="col-sm-3 bold center">Загрузка</div>
                        <div class="col-sm-1 bold align-right"></div>
                        <div class="col-sm-1 bold align-right">Последний</div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3"></div>
                        <div class="col-sm-1">
                            <div class="col-sm-6">всего</div>
                            <div class="col-sm-6">показания</div>
                        </div>
                        <div class="col-sm-1 align-right nowrap"><%# GetCurMonth().ToString("MMM yyyy") %></div>
                        <div class="col-sm-1 align-right nowrap"><%# GetPrevMonth().ToString("MMM yyyy") %></div>
                        <div class="col-sm-1 align-right nowrap"><%# GetPrevPrevMonth().ToString("MMM yyyy") %></div>
                        <%--<div class="col-sm-1 bold align-right"><%# GetCurMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right"><%# GetPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right"><%# GetPrevPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">средняя</div>--%>
                        <div class="col-sm-3">
                            <div class="col-sm-3 align-right nowrap"><%# GetCurMonth().ToString("MMM yyyy") %></div>
                            <div class="col-sm-3 align-right nowrap"><%# GetPrevMonth().ToString("MMM yyyy") %></div>
                            <div class="col-sm-3 align-right nowrap"><%# GetPrevPrevMonth().ToString("MMM yyyy") %></div>
                            <div class="col-sm-3 align-right">средняя</div>
                        </div>
                        <div class="col-sm-1 bold align-right">Износ</div>
                        <div class="col-sm-1 bold align-right">счетчик</div>
                    </div>
            </HeaderTemplate>
            <ItemTemplate>

                <asp:HiddenField ID="hfIdContractor" runat="server" Value='<%#Eval("Id") %>' />
                <div class="row bg collapsed row-action" data-toggle="collapse" data-target='<%# String.Format("#contractList{0}", Container.ItemIndex) %>'>
                    <div class="col-sm-3">
                        <%#Eval("name") %>
                    </div>
                    <div class="col-sm-1 align-right">
                        <div class="col-sm-6 align-right">
                            <%--<span id="sDevCount" runat="server" class="align-right"></span>--%>
                            <%#Eval("ContractorDevCount") %>
                        </div>
                        <div class="col-sm-6 align-right"><span id="sDevCountWithVol" runat="server" class="align-right"></span></div>
                    </div>
                    <div class="col-sm-1 align-right">
                        <%# String.Format("{0:### ### ### ### ###}", Eval("ContractorCurVolTotal")) %>
                        <%--<span id="sContractorCurVol" runat="server"></span>--%>
                    </div>
                    <div class="col-sm-1 align-right">
                        <%# String.Format("{0:### ### ### ### ###}", Eval("ContractorPrevVolTotal")) %>
                        <%--<span id="sContractorPrevVol" runat="server"></span>--%>
                    </div>
                    <div class="col-sm-1 align-right">
                        <%# String.Format("{0:### ### ### ### ###}", Eval("ContractorPrevPrevVolTotal")) %>
                        <%--<span id="sContractorPrevPrevVol" runat="server"></span>--%>
                    </div>
                    <%--                <div class="col-sm-1 align-right"><span id="sContractorCurLoading" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevLoading" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevPrevLoading" runat="server"></span></div>
    <div class="col-sm-1"></div>--%>
                    <div class="col-sm-3">
                        <div class="col-sm-3 align-right"><span id="sContractorCurLoading" runat="server"></span></div>
                        <div class="col-sm-3 align-right"><span id="sContractorPrevLoading" runat="server"></span></div>
                        <div class="col-sm-3 align-right"><span id="sContractorPrevPrevLoading" runat="server"></span></div>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-1"></div>
                </div>
                <div id='<%# String.Format("contractList{0}", Container.ItemIndex) %>' class="contractorContractList collapse">
                    <%-- класс contractorContractList не убирать, по нему работает множественное раскрытие --%>
                    <asp:Repeater ID="rtrContractorContractList" runat="server" OnItemDataBound="rtrContractorContractList_OnItemDataBound">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfIdContract" runat="server" Value='<%#Eval("IdContract") %>' />
                            <div class="row collapsed row-action" data-toggle="collapse" data-target='<%#String.Format("#deviceList{1}Ctr{0}", Eval("IdContractor"), Eval("IdContract")) %>'>
                                <div class="col-sm-3">
                                    <span class="pad-l-sm"><%#Eval("Number") %></span>
                                </div>
                                <div class="col-sm-1 align-right">
                                    <div class="col-sm-6 align-right">
                                        <%#Eval("ContractDevCount") %>
                                        <%--                                    <span id="sContractDevCount" runat="server" class="align-right"></span>--%>
                                        <%--                                    <%# GetItem(Container.DataItem ,6) %>--%>
                                    </div>
                                    <div class="col-sm-6 align-right"><span id="sContractDevCountWithVol" runat="server" class="align-right"></span></div>

                                </div>
                                <div class="col-sm-1 align-right">
                                    <%# String.Format("{0:### ### ### ### ###}", Eval("ContractCurVolTotal")) %>
                                    <%--<span id="sCurVol" runat="server"></span>--%>
                                </div>
                                <div class="col-sm-1 align-right">
                                    <%#String.Format("{0:### ### ### ### ###}", Eval("ContractPrevVolTotal")) %>
                                    <%--<span id="sPrevVol" runat="server"></span>--%>
                                </div>
                                <div class="col-sm-1 align-right">
                                    <%#String.Format("{0:### ### ### ### ###}", Eval("ContractPrevPrevVolTotal")) %>
                                    <%--<span id="sPrevPrevVol" runat="server"></span>--%>
                                </div>
                                <%--                            <div class="col-sm-1 align-right"><span id="sCurLoading" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevLoading" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevPrevLoading" runat="server"></span></div>
                            <div class="col-sm-1"></div>--%>
                                <div class="col-sm-3">
                                    <div class="col-sm-3 align-right">
                                        <span id="sCurLoading" runat="server"></span>
                                    </div>
                                    <div class="col-sm-3 align-right"><span id="sPrevLoading" runat="server"></span></div>
                                    <div class="col-sm-3 align-right"><span id="sPrevPrevLoading" runat="server"></span></div>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-1"></div>
                            </div>
                            <div id='<%# String.Format("deviceList{1}Ctr{0}", Eval("IdContractor"), Eval("IdContract")) %>' class="contractDeviceList collapse">
                                <%-- класс contractDeviceList не убирать, по нему работает множественное раскрытие --%>
                                <div id="phContractorContractsDevicesList" runat="server"></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    </div>
    <%--<asp:HiddenField ID="HdnValue" runat="server" />
    <script type="text/javascript">
        function ExportDivDataToExcel() {
            var html = $("#pnlReport").html();
            html = $.trim(html);
            html = html.replace(/>/g, '&gt;');
            html = html.replace(/</g, '&lt;');
            $("input[id$='HdnValue']").val(html);
        }
    </script>--%>
    <%--    <asp:SqlDataSource ID="sdsCounterReport" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getCounterReportContractorList" Name="action" />
            <asp:QueryStringParameter QueryStringField="mgr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>