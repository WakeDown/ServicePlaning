<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="SpecPrice.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Contracts.SpecPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <div class="col-sm-6">
                <label for='<%=ddlContracts.ClientID %>' class="control-label">Договор</label>
                <asp:DropDownList ID="ddlContracts" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlContracts_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlContracts" runat="server" ErrorMessage="Выберите договор" ControlToValidate="ddlContracts" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <asp:UpdatePanel ID="upNomenclature" runat="server">
                <ContentTemplate>
                    <div class="col-sm-3">
                        <asp:HiddenField ID="hfIdNomenclatureNum" runat="server" />
                        <asp:TextBox ID="txtCatalogNum" runat="server" class="form-control" MaxLength="20" placeholder="каталожный №" OnTextChanged="txtCatalogNum_OnTextChanged"></asp:TextBox>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvTxtCatalogNum" runat="server" ErrorMessage="Заполните поле &laquo;каталожный №&raquo;" ControlToValidate="txtCatalogNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        </span>

                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtNomenclatureName" runat="server" class="form-control" MaxLength="50" placeholder="Наиманование"></asp:TextBox>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="refTxtNomenclatureName" runat="server" ErrorMessage="Заполните поле &laquo;Наиманование&raquo;" ControlToValidate="txtNomenclatureName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        </span>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="col-sm-2">
                <asp:TextBox ID="txtPrice" runat="server" class="form-control" MaxLength="13" placeholder="Цена, руб."></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPrice" runat="server" ErrorMessage="Заполните поле &laquo;Цена&raquo;" ControlToValidate="txtPrice" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div class="col-sm-2">
                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" data-toggle="tooltip" title="добавить спеццену" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
            </div>
        </div>
    </div>
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
    </h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblList_DataBound">
        <Columns>
            <%--            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Наименование" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:BoundField DataField="catalog_num" SortExpression="catalog_num" HeaderText="Каталожный №" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header nowrap" />
            <asp:BoundField DataField="price" SortExpression="price" HeaderText="Цена, руб." ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getSrvplContractSpecPriceList" Name="action" />
            <asp:QueryStringParameter QueryStringField="id" Name="id_srvpl_contract" DefaultValue="" ConvertEmptyStringToNull="True" />
            <%--             <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="30" ConvertEmptyStringToNull="True" />--%>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
