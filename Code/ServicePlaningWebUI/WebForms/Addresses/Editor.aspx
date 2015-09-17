<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/ListEditor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Addresses.Editor" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtName.ClientID %>' class="col-sm-2 control-label">Адрес</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtName" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ErrorMessage="Заполните поле &laquo;Адрес&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку договоров" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtFilterName.ClientID %>' class="col-sm-2 control-label">Адрес</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterName" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm"></asp:TextBox>
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphList" runat="server">
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span></h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="False" PageSize="50" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;"  PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc">
        <Columns>
<%--            <asp:BoundField DataField="id_srvpl_address" SortExpression="id_srvpl_address" HeaderText="ID" ItemStyle-CssClass="min-width bold" />--%>
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_srvpl_address"))) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Адрес" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="count" SortExpression="count" HeaderText="Количество" ItemStyle-CssClass="min-width" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_srvpl_address") %>' OnClientClick="return DeleteConfirm('улицу')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getSrvplAddressList" Name="action" />
            <asp:QueryStringParameter Name="name" QueryStringField="name" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="rows_count" QueryStringField="rcnt" DefaultValue="100" ConvertEmptyStringToNull="True" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>