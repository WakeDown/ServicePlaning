<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Came.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.Came" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtClaimSelection.ClientID %>' class="col-sm-2 control-label required-mark">Серийный номер</label>
            <div class="col-sm-10">
                <%--<asp:DropDownList ID="ddlServiceClaim" runat="server" CssClass="form-control">
                    </asp:DropDownList>--%>
                <%--                <asp:UpdatePanel ID="upClaimList" runat="server">
                    <ContentTemplate>--%>
                <script type="text/javascript">
                    function EnterEvent(e) {
                        if (e.keyCode == 13) {
                            __doPostBack('<%=btnClaimSelection.UniqueID%>', "");
                        }
                    }
                </script>
                <div class="input-group">

                    <asp:TextBox ID="txtClaimSelection" runat="server" CssClass="form-control" placeholder="поиск" MaxLength="50"
                        onkeypress="return EnterEvent(event)"></asp:TextBox>
                    <div class="input-group-addon">
                        <asp:LinkButton ID="btnShowSerialNums" runat="server" type="submit" class="btn btn-success btn-xs" data-toggle="tooltip" title="" OnClick="btnShowSerialNums_Click">серийники</asp:LinkButton>
                    </div>
                </div>
                <asp:Button ID="btnClaimSelection" runat="server" Style="display: none" Text="Button" OnClick="btnClaimSelection_Click" />
                <%--OnTextChanged="txtClaimSelection_TextChanged"  AutoPostBack="True"--%>
                <asp:ListBox ID="lbSerialNums" runat="server" Height="150px" CssClass="full-width input-sm" OnSelectedIndexChanged="lbSerialNums_OnSelectedIndexChanged" AutoPostBack="True" Visible="False"></asp:ListBox>
                <asp:ListBox ID="lbClaim" runat="server" Height="150px" CssClass="full-width input-sm" OnSelectedIndexChanged="lbClaim_OnSelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                <span class="help-block">
                    <%--<asp:RequiredFieldValidator ID="rfvDdlServiceClaim" runat="server" ErrorMessage="Выберите заявку" Display="Dynamic" ControlToValidate="ddlServiceClaim" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <asp:RequiredFieldValidator ID="rfvLbClaim" runat="server" ErrorMessage="Выберите заявку" Display="Dynamic" ControlToValidate="lbClaim" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
                <%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
        <%--<asp:HiddenField ID="hfIdServiceClaim" runat="server" />
        <div class="form-group">
            <label for='<%=lbDevice.ClientID %>' class="col-sm-2 control-label">Аппарат</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                        <asp:TextBox ID="txtDeviceSelection" runat="server" class="form-control input-sm width-20" placeholder="поиск" MaxLength="33"></asp:TextBox>
                    <asp:ListBox ID="lbDevice" runat="server" Height="150px" CssClass="full-width input-sm"></asp:ListBox>
                </div>
            </div>
        </div>--%>
        <div class="form-group">
            <label for='<%=ddlServiceActionType.ClientID %>' class="col-sm-2 control-label required-mark">Вид работ</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceActionType" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlServiceActionType" runat="server" ErrorMessage="Выберите вид работ" Display="Dynamic" ControlToValidate="ddlServiceActionType" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceEngeneer.ClientID %>' class="col-sm-2 control-label required-mark">ФИО инженера</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceEngeneer" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlServiceEngeneer" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlServiceEngeneer" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <%--        <script type="text/javascript">
            function checkCounter() {

                //var counter = $("#txtCounter").val();
                var url = "<%=String.Format("{0}/Check", FriendlyUrl.Resolve("~/WebForms/Service/Service.asmx")) %>";
                alert(url);
                $.ajax({
                    type: "POST",
                    url: url,
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    success: function (msg) {
                        alert(1);
                        //if (msg != null && msg != '') {
                        //    $('#counterNoteMessage').show();
                        //    $('#counterNoteMessage').innerText = msg;
                        //} else { $('#counterNoteMessage').hide(); }

                    },
                    error: function () {
                        alert('error');
                    }
                });
            }
        </script>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <label for='<%=txtDateCame.ClientID %>' class="col-sm-2 control-label required-mark">Дата обслуживания</label>
                    <div class="col-sm-3">
                        <div class="input-group">

                            <asp:TextBox ID="txtDateCame" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDateCame_OnTextChanged"></asp:TextBox>
                            <%--<span class="input-group-btn">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        </div>
                        <span class="help-block">

                            <asp:RequiredFieldValidator ID="rfvЕxtDateCame" runat="server" ErrorMessage="Заполните поле &laquo;Дата обслуживания&raquo;" Display="Dynamic" ControlToValidate="txtDateCame" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvЕxtDateCame" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtDateCame" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                            <asp:UpdatePanel ID="upDateCame" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:RangeValidator ID="rvDateCame" runat="server" ErrorMessage="Выберите другую дату" ControlToValidate="txtDateCame" ValidationGroup="vgForm" CssClass="text-danger" Type="Date" Display="Dynamic" SetFocusOnError="True" Enabled="False" MinimumValue="01.01.1900" MaximumValue="03.03.3333"></asp:RangeValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </span>
                    </div>
                </div>
                <div class="form-group">
                    <label for='<%=rblProcessEnabled.ClientID %>' class="col-sm-2 control-label required-mark">Процесс печати восстановлен</label>
                    <div class="col-sm-2">
                        <asp:RadioButtonList ID="rblProcessEnabled" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                            <asp:ListItem Text="да" Value="1"></asp:ListItem>
                            <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <label for='<%=rblDeviceEnabled.ClientID %>' class="col-sm-2 control-label required-mark">Оборудование полностью восстановлено</label>
                    <div class="col-sm-2">
                        <asp:RadioButtonList ID="rblDeviceEnabled" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                            <asp:ListItem Text="да" Value="1"></asp:ListItem>
                            <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <label for='<%=rblNeedZip.ClientID %>' class="col-sm-2 control-label required-mark">Необходим заказ ЗИП</label>
                    <div class="col-sm-2">
                        <asp:RadioButtonList ID="rblNeedZip" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst" AutoPostBack="True" OnSelectedIndexChanged="rblNeedZip_OnSelectedIndexChanged">
                            <asp:ListItem Text="да" Value="1"></asp:ListItem>
                            <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <label for='<%=rblNoCounter.ClientID %>' class="col-sm-2 control-label required-mark">Счетчик предусмотрен</label>
                    <div class="col-sm-2">
                        <asp:RadioButtonList ID="rblNoCounter" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst" AutoPostBack="True" OnSelectedIndexChanged="rblNoCounter_OnSelectedIndexChanged">
                            <asp:ListItem Text="да" Value="0"></asp:ListItem>
                            <asp:ListItem Text="нет" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group" id="rblCounterUnavailableContainer" runat="server">
                    <label for='<%=rblCounterUnavailable.ClientID %>' class="col-sm-2 control-label required-mark">Счетчик доступен</label>
                    <div class="col-sm-2">
                        <asp:RadioButtonList ID="rblCounterUnavailable" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst" AutoPostBack="True" OnSelectedIndexChanged="rblCounterUnavailable_OnSelectedIndexChanged">
                            <asp:ListItem Text="да" Value="0"></asp:ListItem>
                            <asp:ListItem Text="нет" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div id="pnlCounters" runat="server">
                <div class="form-group">
                    <label for='<%=txtCounter.ClientID %>' class="col-sm-2 control-label required-mark">Счетчик</label>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtCounter" runat="server" CssClass="form-control" MaxLength="15" AutoPostBack="true" OnTextChanged="txtCounter_OnTextChanged"></asp:TextBox>
                            <%--<span class="input-group-btn">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        </div>

                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvTxtCounter" runat="server" Enabled="False" ErrorMessage="Заполните поле &laquo;Счетчик&raquo;" Display="Dynamic" ControlToValidate="txtCounter" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvTxtCounter" runat="server" Enabled="False" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                        </span>
                    </div>
                </div>
                <div class="form-group">
                    <label for='<%=txtCounterColour.ClientID %>' class="col-sm-2 control-label">Счетчик(цветной)</label>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtCounterColour" runat="server" CssClass="form-control" MaxLength="15" AutoPostBack="true" OnTextChanged="txtCounterColour_OnTextChanged"></asp:TextBox>
                            <%--<span class="input-group-btn">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        </div>
                        <span class="help-block">
                            <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Счетчик&raquo;" Display="Dynamic" ControlToValidate="txtCounter" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                        </span>
                    </div>
                </div>
                    </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <blockquote class="bg-warning" id="counterNoteMessage" runat="server" visible="false"></blockquote>
                    </div>
                </div>
           

        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <%--                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку заявок" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>--%>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку заявок" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblNoPay.ClientID %>' class="col-sm-2 control-label"></label>
            <div class="col-sm-3">
                <asp:RadioButtonList ID="rblNoPay" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="оплачивать" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="НЕ оплачивать" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDescr.ClientID %>' class="col-sm-2 control-label">Комментарий</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtDescr" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <span class="help-block">
                                        <asp:RequiredFieldValidator ID="rfvTxtDescr" Enabled="False" runat="server" ErrorMessage="Заполните поле &laquo;Комментарий&raquo;" Display="Dynamic" ControlToValidate="txtDescr" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
<%--                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtDateClaim" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
                 </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
