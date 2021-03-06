﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Users2Roles.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Settings.Users2Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Назначение ролей
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <%--<div class="form-group">
            <label class="col-sm-3 control-label">Формула</label>
            <div class="col-sm-9">
                ИТОГО = &laquo;Базовая ставка&raquo; * SQRT(количество обслуженного оборудования факт)
            </div>
        </div>--%>
        <asp:Repeater ID="rtrUsers" runat="server" OnItemDataBound="rtrUsers_OnItemDataBound">
            <ItemTemplate>
                <div class="form-group">
                    <label class="col-sm-3 control-label"><%#Eval("name") %></label>
                    <div class="col-sm-9">
                        <div class="input-group full-width">
                            <asp:HiddenField ID="hfIdUser" runat="server" Value='<%#Eval("id") %>' />
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("id") %>' />
                            <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
<%--                        <span class="help-block">
                            <asp:CompareValidator ID="cvTxtPrice" runat="server" ErrorMessage="Введите число" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ControlToValidate="txtPrice" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rfvTxtPrice" runat="server" ErrorMessage='<%# String.Format("Заполните поле &laquo;{0}&raquo;", Eval("role_name")) %>' ControlToValidate="TxtPrice" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        </span>--%>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%--<asp:SqlDataSource ID="sdsUser2UserRoles" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
            <SelectParameters>
                <asp:Parameter DefaultValue="getUser2UserRoleList" Name="action" />
            </SelectParameters>
        </asp:SqlDataSource>--%>
        <div class="col-sm-offset-3 col-sm-9">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
