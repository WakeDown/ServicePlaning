<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="TariffEngeneer.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Settings.TariffEngeneer" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Настройка тарифа для расчета оплаты инженерам
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSetTariff" runat="server" type="submit" class="btn btn-primary btn-lg" OnClick="btnSetTariff_Click">Установить новые тарифы для оборудования</asp:LinkButton>
<%--                <asp:CheckBox ID="chkAll" runat="server" />--%>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">Формула</label>
            <div class="col-sm-9">
                <div class="input-group full-width">
                        <div class="row h6">
                            <div class="col-sm-3">
                                <strong>Тариф предварительный&nbsp;=</strong>
                            </div>
                            <div class="col-sm-9">
                                (Скорость печати (за страницу) * Значение скорости аппарата) + Формат + Тип печати + Сумма опций + Возраст (если неизветсен, то 3)
                            </div>
                        </div>
                    <div class="h6">
                        <div class="row">
                            <div class="col-sm-3">
                                <strong>Тариф конечный&nbsp;=</strong>
                            </div>
                            <div class="col-sm-4">
                                Если (Способ печати = Лазерный картридж)
                            </div>
                            <div class="col-sm-5">
                                <strong>Тариф конечный</strong> = Тариф предварительный * 0.9
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-4">
                                Если (Способ печати = Струйный картридж) 
                            </div>
                            <div class="col-sm-5">
                                <strong>Тариф конечный</strong> = Тариф предварительный * 0.7
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-4">
                                Если (Скорость печати > 89) 
                            </div>
                            <div class="col-sm-5">
                                <strong>Тариф конечный</strong> = Тариф предварительный * 1.5
                            </div>
                        </div>
                        </div>
                </div>
            </div>
        </div>
        <asp:Repeater ID="rtrFeatures" runat="server" DataSourceID="sdsFeatures">
            <ItemTemplate>
                <div class="form-group">
                    <label class="col-sm-3 control-label"><%#Eval("name") %></label>
                    <div class="col-sm-9">
                        <div class="input-group full-width">
                            <asp:HiddenField ID="hfIdFeature" runat="server" Value='<%#Eval("id_feature") %>' />
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" MaxLength="10" Text='<%#Eval("price") %>'></asp:TextBox>
                        </div>
                        <span class="help-block">
                            <asp:CompareValidator ID="cvTxtPrice" runat="server" ErrorMessage="Введите число" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ControlToValidate="txtPrice" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                            <%--                            <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Модель&raquo;" ControlToValidate="txtModel" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                        </span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="sdsFeatures" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
            <SelectParameters>
                <asp:Parameter DefaultValue="getTariffFeatureList" Name="action" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="col-sm-offset-3 col-sm-9">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <%--                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку оборудования" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>--%>
            </div>
        </div>
    </div>

</asp:Content>
