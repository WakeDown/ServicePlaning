<%@ Page Title="Оборудование - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/ListEditor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.DeviceModels.Editor" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Import Namespace="ServicePlaningWebUI.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div id="formDevices" class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlDeviceType.ClientID %>' class="col-sm-2 control-label required-mark">Тип оборудования</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlDeviceType" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceType" runat="server" ErrorMessage="Выберите тип оборудования" Display="Dynamic" ControlToValidate="ddlDeviceType" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtName.ClientID %>' class="col-sm-2 control-label required-mark">Название модели</label>
            <div class="col-sm-10 val-control-container">
                <div class="input-group full-width">
                    <span class="input-group-btn width-30">
                        <asp:TextBox ID="txtVendor" runat="server" class="form-control width-30" placeholder="Производитель" MaxLength="150"></asp:TextBox>
                    </span>
                    <asp:TextBox ID="txtName" runat="server" class="form-control" MaxLength="150" placeholder="Модель"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtVendor" runat="server" ErrorMessage="Заполните поле &laquo;Производитель&raquo;" ControlToValidate="txtVendor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvTxtModel" runat="server" ErrorMessage="Заполните поле &laquo;Модель&raquo;" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <%--<div class="form-group">
            <label for='<%=txtNickname.ClientID %>' class="col-sm-2 control-label">Сокращение</label>
            <div class="col-sm-10 val-control-container">
                <asp:TextBox ID="txtNickname" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                <span class="help-block">
                </span>
            </div>
        </div>--%>
        <div class="form-group">
            <label for='<%=txtSpeed.ClientID %>' class="col-sm-2 control-label">Скорость печати</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSpeed" runat="server" class="form-control" MaxLength="13"></asp:TextBox>
                <h5><small>Для отделения десятичной части используйте запятую, например - 2,4</small></h5>
                <span class="help-block">
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Double" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlDeviceImprint.ClientID %>' class="col-sm-2 control-label">Формат</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlDeviceImprint" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceImprint" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlDeviceImprint" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlPrintType.ClientID %>' class="col-sm-2 control-label">Тип печати</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlPrintType" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceImprint" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlDeviceImprint" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlCartridgeType.ClientID %>' class="col-sm-2 control-label">Способ печати</label><%--Тип картриджа--%>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCartridgeType" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceImprint" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlDeviceImprint" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
         <div class="form-group">
            <label for='<%=txtMaxVolume.ClientID %>' class="col-sm-2 control-label">Максимальный объем печати</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtMaxVolume" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                <span class="help-block">
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="cvTxtMaxVolume" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtMaxVolume" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlClassifier.ClientID %>' class="col-sm-2 control-label">№ подтипа (классификатор)</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlClassifier" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <%--<span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlDeviceImprint" runat="server" ErrorMessage="Выберите отпечаток" Display="Dynamic" ControlToValidate="ddlDeviceImprint" InitialValue='-1' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>--%>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSaveAndAddNew" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <asp:LinkButton ID="btnSaveAndBack" runat="server" type="submit" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и перейти к списку оборудования" OnClick="btnSaveAndBack_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-mail-reply fa-sm"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку оборудования" href='<%= FriendlyUrl.Href(ListUrl) %>'><i class="fa fa-mail-reply "></i></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtFilterModel.ClientID %>' class="col-sm-2 control-label">Модель</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterModel" runat="server" class="form-control input-sm"></asp:TextBox>
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
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="True" AllowPaging="True" PageSize="50" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc">
        <Columns>
            
            <asp:BoundField DataField="id_device_model" SortExpression="id_device_model" HeaderText="ID" ItemStyle-CssClass="min-width bold" />
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <a href='<%#  GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_device_model")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="редактировать"><i class="fa fa-edit fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Модель" />
            <asp:BoundField DataField="classifier_category_number" SortExpression="classifier_category_number" HeaderText="№ подтипа" />
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_device_model") %>' OnClientClick="return DeleteConfirm('модель')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsDeviceModelList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getDeviceModelList" Name="action" />
            <asp:QueryStringParameter Name="name" QueryStringField="name" DefaultValue="" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
