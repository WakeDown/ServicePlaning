<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="CameScan.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Service.CameScan" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="tbl-akt-scan">
                <tr>
                    <td rowspan="2" class="akt-scan-file-list">
                        <div class="akt-scan-file-list-container">
                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_OnTick" Interval="900000" Enabled="True"></asp:Timer>
                            <asp:UpdatePanel ID="upScanList" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Repeater ID="tblAktScanList" runat="server" DataSourceID="sdsAktScanList">
                                        <HeaderTemplate>
                                            <table class="tbl-akt-scan-lst">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="trAktScanFile" runat="server">
                                                <td>
                                                    <asp:LinkButton ID="btnAktScanFile" runat="server" IdAktScan='<%#Eval("id_akt_scan") %>' OnClick="btnAktScanFile_OnClick"><%#Eval("file_name") %></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:SqlDataSource ID="sdsAktScanList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="getAktScanList" Name="action" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td class="act-scan-file-view">
                        <div id="pnlImgBtns" runat="server" class="pad-b-sm" visible="False">
                            <div class="pull-left">
                                <asp:Label ID="lblImgNote" runat="server" Text="" CssClass="text-danger bg"></asp:Label>
                            </div>
                            <div class="text-center">
                                <asp:LinkButton ID="btnRotateLeft" runat="server" OnClick="btnRotateLeft_OnClick" CssClass="btn btn-primary"><i class="fa fa-reply"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnResizePlus" runat="server" OnClick="btnResizePlus_OnClick" CssClass="btn btn-primary"><i class="fa fa-plus"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnImageNormal" runat="server" OnClick="btnImageNormal_OnClick" CssClass="btn btn-primary"><i class="fa fa-circle"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnResizeMinus" runat="server" OnClick="btnResizeMinus_OnClick" CssClass="btn btn-primary"><i class="fa fa-minus"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnRotateRight" runat="server" OnClick="btnRotateRight_OnClick" CssClass="btn btn-primary"><i class="fa fa-share"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnRotateDownUp" runat="server" OnClick="btnRotateDownUp_OnClick" CssClass="btn btn-primary"><i class="fa fa-refresh"></i></asp:LinkButton>
                                <div class="pull-right">
                                    <asp:LinkButton ID="btnAktScanFileDelete" runat="server" IdAktScan='' OnClick="btnAktScanFileDelete_OnClick" class="btn btn-danger btn-lg">удалить</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="clearfix"></div>
                        <div id="imgScanContainer" runat="server" class="act-scan-file-view-container">
                            <asp:Image ID="imgAktScan" runat="server" IdAktScan="" CssClass="imgNormal" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="akt-scan-form">
                        <div id="formDevices" class="form-horizontal val-form col-sm-10" role="form">
                            <div class="form-group">
                                <label for='<%=txtClaimSelection.ClientID %>' class="col-sm-2 control-label required-mark">Серийный номер</label>
                                <div class="col-sm-10">
                                    <asp:UpdatePanel ID="upClaimList" runat="server">
                                        <ContentTemplate>

                                            <asp:TextBox ID="txtClaimSelection" runat="server" CssClass="form-control input-sm" placeholder="поиск" MaxLength="50" OnTextChanged="txtClaimSelection_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:ListBox ID="lbClaim" runat="server" Height="120px" CssClass="full-width input-sm" OnSelectedIndexChanged="lbClaim_OnSelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                                            <span class="help-block">
                                                <asp:RequiredFieldValidator ID="rfvLbClaim" runat="server" ErrorMessage="Выберите заявку" Display="Dynamic" ControlToValidate="lbClaim" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                                            </span>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-10 pad-b-sm">
                            <div class="col-sm-push-2 col-sm-10 pad-0">
                                <div id="formDevices2" class="form-inline val-form" role="form">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <label for='<%=txtCounter.ClientID %>' class="sr-only">Счетчик</label>
                                            <asp:TextBox ID="txtCounter" runat="server" CssClass="form-control" MaxLength="15" placeholder="Счетчик"></asp:TextBox>
                                            <span class="help-block">
                                                <asp:RequiredFieldValidator ID="rfvTxtCounter" runat="server" ErrorMessage="Заполните поле &laquo;Счетчик&raquo;" Display="Dynamic" ControlToValidate="txtCounter" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvTxtCounter" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <label for='<%=txtCounterColour.ClientID %>' class="sr-only">Счетчик(цветной)</label>
                                            <asp:TextBox ID="txtCounterColour" runat="server" CssClass="form-control" MaxLength="15" placeholder="Счетчик(цветной)"></asp:TextBox>
                                            <span class="help-block">
                                                <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Счетчик&raquo;" Display="Dynamic" ControlToValidate="txtCounter" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <label for='<%=txtDateCame.ClientID %>' class="sr-only">Дата обслуживания</label>
                                            <asp:TextBox ID="txtDateCame" runat="server" CssClass="form-control" placeholder="Дата обслуживания"></asp:TextBox>
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
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div id="formDevices3" class="form-horizontal val-form col-sm-10" role="form">
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
                                        <asp:RequiredFieldValidator ID="rfvDdlServiceEngeneer" runat="server" ErrorMessage="Выберите инженера" Display="Dynamic" ControlToValidate="ddlServiceEngeneer" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="form-group">
                                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg btn-big" data-toggle="tooltip" title="сохранить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-xlg"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
                        </div>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
