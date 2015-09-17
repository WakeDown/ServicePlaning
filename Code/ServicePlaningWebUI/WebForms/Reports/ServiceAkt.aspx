<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceAkt.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.ServiceAkt" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:PlaceHolder runat="server">
        <%:Styles.Render("~/Content/ServiceAkt") %>
    </asp:PlaceHolder>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Акт обслуживания</title>
    <asp:PlaceHolder runat="server">
        <%:Scripts.Render("~/bundles/jquery") %>
        <%:Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rtrServiceAkt" runat="server" DataSourceID="sdsServiceAkt">
                <ItemTemplate>
                    <div class="print">
                        <table class="sheet">
                            <tbody>
                                <tr>
                                    <td>
                                        <table class="sheet-line">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4" class="title">Сервисный лист</td>
                                                    <td rowspan="2" class="title-img align-center">
                                                        <asp:Image ID="imgUnit" runat="server" ImageUrl="~/Images/logo-unit.png"></asp:Image></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="nowrap">от
_________ / __________ / __________г. </td>
                                                    <td class="pad-mid-l pad-mid-r align-right">НОМЕР
ЗАЯВКИ:
                                                        <br />НОМЕР ЗАЯВКИ ЗИП:

                                                    </td>
                                                    <td>
                                                        <div class="text-box"></div>
                                                        <div class="text-box"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="dev-title">Серийный №:</td>
                                                    <td colspan="3" class="dev-val bold"><%#Eval("serial_num")
                                                    %></td>
                                                    <td class="title-info">8-800-200-09-22</td>
                                                </tr>
                                                <tr>
                                                    <td class="dev-title">Инвентарный №:</td>
                                                    <td colspan="3" class="dev-val bold"><%#Eval("inv_num")
                                                    %></td>
                                                    <td class="title-info">help@unitgroup.ru</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="title-line">
                                    <td>
                                        <br>
                                        Информация о пользователе и модели оборудования: </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="sheet-line">
                                            <tbody>
                                                <tr class="marg-mid-t">
                                                    <td class="dev-title"></td>
                                                    <td class="dev-val"></td>
                                                </tr>
                                                <tr class="marg-mid-t">
                                                    <td class="dev-title">Модель:</td>
                                                    <td class="dev-val bold"><%#Eval("model")
                                                    %></td>
                                                </tr>
                                                <tr class="marg-mid-t">
                                                    <td class="dev-title">Организация:</td>
                                                    <td class="dev-val bold"><%#Eval("contractor")
                                                    %></td>
                                                </tr>
                                                <tr class="marg-mid-t">
                                                    <td class="dev-title">Адрес:</td>
                                                    <td class="dev-val bold"><%#Eval("city")
                                                    %>, <%#Eval("ADDRESS") %></td>
                                                </tr>
                                                <tr class="marg-mid-t marg-mid-b">
                                                    <td class="dev-title">Оъект:</td>
                                                    <td class="dev-val bold"><%#Eval("object_name")
                                                    %></td>
                                                </tr>
                                                <tr class="marg-mid-t">
                                                    <td class="dev-title"></td>
                                                    <td class="dev-val bold"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="title-line">
                                    <td>
                                        <br>
                                        При выполнении технического обслуживания проведены
следующие работы: </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="sheet-line f-small pad-3">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>снятие счетчика копий: </td>
                                                    <td class="text-box"></td>
                                                    <td class="text-box"></td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка от тонера и бумажной пыли тракта
прохождения бумаги </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td class="tb-info">ч/б</td>
                                                    <td class="tb-info">цвет</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка валиков, роликов восстановителем
резиновых поверхностей </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td colspan="3">снятие и установка блока
фотобарабана (картриджа, PCU)</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка тормозных площадок восстановителем
резиновых поверхностей</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td colspan="3">снятие и установка блока
проявки (блок тонер-картриджа)</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка внутренних поверхностей лотков подачи
бумаги</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td colspan="3">очистка снятых узлов от тонера
и бумажной пыли</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>двухстороняя очистка стекла экспонирования</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td colspan="3">очистка защитного стекла блока
лазера</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка зеркала, лампы экспонирования, рефлекторов</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td colspan="3">очистка лампы засветки
фоторецептора</td>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>очистка оптических элементов датчиков</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="title-line">
                                    <td>
                                        <br>
                                        При проведении ремонта выявлено:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="sheet-line f-small pad-3">
                                            <tbody>
                                                <tr>
                                                    <td colspan="6">Описание неисправности:</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td colspan="6">&nbsp;</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td colspan="6">&nbsp;</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td colspan="6">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="w-50">
                                                        <br>
                                                        Список
выполненных работ:</td>
                                                    <td colspan="4">
                                                        <br>
                                                        Использованные при ремонте
запасные части (наименование / количество):</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="text-line w-50">&nbsp;</td>
                                                    <td class="text-num">1</td>
                                                    <td class="text-line w-40">&nbsp;</td>
                                                    <td class="min-width">/</td>
                                                    <td class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="text-line w-50">&nbsp;</td>
                                                    <td class="text-num">2</td>
                                                    <td class="text-line w-40">&nbsp;</td>
                                                    <td class="min-width">/</td>
                                                    <td class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="text-line w-50">&nbsp;</td>
                                                    <td class="text-num">3</td>
                                                    <td class="text-line w-40">&nbsp;</td>
                                                    <td class="min-width">/</td>
                                                    <td class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="text-line w-50">&nbsp;</td>
                                                    <td class="text-num">4</td>
                                                    <td class="text-line w-40">&nbsp;</td>
                                                    <td class="min-width">/</td>
                                                    <td class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="w-50">Состояние
оборудования после технического обслуживания/ремонта: </td>
                                                    <td colspan="4">Рекомендации инженера по
эксплуатации оборудования: </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>Исправен [восстановлен, ЗИП не требуется]</td>
                                                    <td colspan="4" class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>Исправен [рекомендуется ЗИП]</td>
                                                    <td colspan="4" class="text-line">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="check-box"></div>
                                                    </td>
                                                    <td>Неисправен [требуется ЗИП]</td>
                                                    <td colspan="4" class="text-line">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="title-line">
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="sheet-line f-small pad-3">
                                            <tbody>
                                                <tr>
                                                    <td colspan="5" class="w-50">Продолжительность
выполнения работ:</td>
                                                    <td colspan="2">Выполнение работ по ТО /
ремонту удостоверяю:</td>
                                                </tr>
                                                <tr>
                                                    <td class="w-25 align-right f-bg">Время начала
работ</td>
                                                    <td class="text-line">&nbsp;</td>
                                                    <td class="text-num">:</td>
                                                    <td class="text-line">&nbsp;</td>
                                                    <td class="w-10">&nbsp;</td>
                                                    <td colspan="2">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="w-25 align-right f-bg"></td>
                                                    <td class="tb-info">час.</td>
                                                    <td class="text-num"></td>
                                                    <td class="tb-info">мин.</td>
                                                    <td class="w-10">&nbsp;</td>
                                                    <td class="w-25 text-line" style="margin-right: 10px;">&nbsp;</td>
                                                    <td class="text-line dev-val bold"><%#Eval("service_engeneer")
                                                    %></td>
                                                </tr>
                                                <tr>
                                                    <td class="w-25 align-right f-bg">Время
окончания работ</td>
                                                    <td class="text-line">&nbsp;</td>
                                                    <td class="text-num">:</td>
                                                    <td class="text-line">&nbsp;</td>
                                                    <td class="w-10">&nbsp;</td>
                                                    <td class="w-25 tb-info">подпись инженера</td>
                                                    <td class="tb-info">ФИО инженера</td>
                                                </tr>
                                                <tr>
                                                    <td class="w-25 align-right f-bg"></td>
                                                    <td class="tb-info">час.</td>
                                                    <td class="text-num"></td>
                                                    <td class="tb-info">мин.</td>
                                                    <td class="w-10">&nbsp;</td>
                                                    <td colspan="2">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="title-line">
                                    <td>
                                        <br>
                                        Заполняется пользователем оборудования по окончании
выполнения работ по техническому обслуживанию / ремонту:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="sheet-line f-small pad-3">
                                            <tbody>
                                                <tr>
                                                    <td>Замечания по качеству и срокам выполнения работ и
обслуживанию:</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr class="text-line">
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr class="tb-info f-smaller">
                                                    <td>Настоящей подписью пользователь подтверждает, что
работы (услуги) по техническому обслуживанию / ремонту оборудования по
данному акту выполнены в полном объеме. </td>
                                                </tr>
                                                <tr class="tb-info f-smaller">
                                                    <td>По выполненным работам (услугам) пользователь
финансовых и имущественных претензий к ООО "Юнит-Копир" не имеет. С
заключениями и рекомендациями инженера ознакомлен.</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="w-100 brd-spc">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="text-line">&nbsp;</td>
                                                                    <td class="text-line">&nbsp;</td>
                                                                    <td class="text-line">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td class="min-width">/</td>
                                                                    <td class="text-line">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td class="min-width">/</td>
                                                                    <td class="text-line">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td class="min-width">г.</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tb-info">ФИО пользователя
оборудования</td>
                                                                    <td class="tb-info">подпись</td>
                                                                    <td class="tb-info">&nbsp;</td>
                                                                    <td class="min-width"></td>
                                                                    <td class="tb-info">дата</td>
                                                                    <td class="min-width"></td>
                                                                    <td class="tb-info">&nbsp;</td>
                                                                    <td class="min-width"></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="w-30">&nbsp;</td>
                                                                    <td class="align-center bold width-40 text-box f-bg">+7</td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                    <td class="width-40 text-box"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="12" class="tb-info align-center">номер
телефона</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="sdsServiceAkt" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getServiceClaimList" Name="action" />
                    <asp:QueryStringParameter QueryStringField="ids" Name="lst_id_service_claim" DefaultValue="" ConvertEmptyStringToNull="True" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
