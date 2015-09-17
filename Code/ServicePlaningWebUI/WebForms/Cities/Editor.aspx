<%@ Page Title="Оборудование - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/ListEditor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Cities.Editor" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Import Namespace="ServicePlaningWebUI.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtRegion.ClientID %>' class="col-sm-2 control-label">Регион</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtRegion" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Название города&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtArea.ClientID %>' class="col-sm-2 control-label">Район</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtArea" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Название города&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtName.ClientID %>' class="col-sm-2 control-label">Город</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtName" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Название города&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtLocality.ClientID %>' class="col-sm-2 control-label">Населенный пункт</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtLocality" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Название города&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCoord.ClientID %>' class="col-sm-2 control-label">Географические координаты</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtCoord" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Название города&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
<%--                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку оборудования" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>--%>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку договоров" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtFilterRegion.ClientID %>' class="col-sm-2 control-label">Регион</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterRegion" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterArea.ClientID %>' class="col-sm-2 control-label">Район</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterArea" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterCity.ClientID %>' class="col-sm-2 control-label">Город</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterCity" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterLocality.ClientID %>' class="col-sm-2 control-label">Неселенный пункт</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterLocality" runat="server" class="form-control input-sm"></asp:TextBox>
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphList" runat="server">
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span></h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="True" PageSize="50" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;"  PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc">
        <Columns>
            <asp:BoundField DataField="id_city" SortExpression="id_city" HeaderText="ID" ItemStyle-CssClass="min-width bold" />
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_device_model")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="region" SortExpression="region" HeaderText="Регион" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="area" SortExpression="area" HeaderText="Район" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Город" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="locality" SortExpression="locality" HeaderText="Населенный пункт" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="coord" SortExpression="coord" HeaderText="Георг. координаты" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_city") %>' OnClientClick="return DeleteConfirm('город')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_unit" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsDeviceModelList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getCityList" Name="action" />
            <asp:QueryStringParameter Name="name" QueryStringField="name" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="region" QueryStringField="reg" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="area" QueryStringField="area" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter Name="locality" QueryStringField="loc" DefaultValue="" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
