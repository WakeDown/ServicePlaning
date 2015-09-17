<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="EditorWizard.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.NewContractWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Добавление договора из файла <a target="_blank" href='<%=String.Format("{0}?f=ctrWiz", ResolveClientUrl("~/WebForms/Handlers/FileHandler.ashx")) %>'>скачать шаблон</a>
    <%--<asp:LinkButton ID="btnDownloadTemplate" runat="server" OnClick="btnDownloadTemplate_OnClick">скачать шаблон</asp:LinkButton>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-inline">
        <div class="form-group">
            <span>Заполните и загрузите файл</span>
        </div>
        <div class="form-group">
            <asp:FileUpload ID="fileUpload" runat="server" />
        </div>
        <div class="form-group">
            <asp:Button ID="btnUpload" runat="server" Text="загрузить" OnClick="btnUpload_OnClick" />
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
