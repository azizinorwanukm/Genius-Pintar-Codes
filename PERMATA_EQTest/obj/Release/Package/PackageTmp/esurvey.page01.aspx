<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master"
    CodeBehind="esurvey.page01.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="lblInstruction" runat="server" Text=""></asp:Label>
    </p>
    <table id="tablelegend">
        <tr>
            <th>1
            </th>
            <th>2
            </th>
            <th>3
            </th>
            <th>4
            </th>
            <th>5
            </th>
        </tr>
        <tr>

            <td>
                <asp:Label ID="lblLevel1" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel2" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel3" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel4" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel5" runat="server" Text=""></asp:Label>
            </td>

        </tr>
    </table>
    &nbsp;
    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table id="mycustomtable">
        <tr>
            <th>#
            </th>
            <th>
                <asp:Label ID="lblDomain01" runat="server" Text=""></asp:Label><asp:LinkButton ID="lnkSave" runat="server" CssClass="LinkButton" Visible="false">Simpan</asp:LinkButton>
            </th>
            <th>
                <asp:Label ID="lblRating" runat="server" Text=""></asp:Label>
            </th>
        </tr>
        <tr>
            <td>1
            </td>
            <td>
                <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q1" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>2
            </td>
            <td>
                <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q2" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>3
            </td>
            <td>
                <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q3" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>4
            </td>
            <td>
                <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q4" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>5
            </td>
            <td>
                <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q5" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>6
            </td>
            <td>
                <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q6" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>7
            </td>
            <td>
                <asp:Label ID="lblQ007" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q7" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>8
            </td>
            <td>
                <asp:Label ID="lblQ008" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q8" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>9
            </td>
            <td>
                <asp:Label ID="lblQ009" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q9" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>10
            </td>
            <td>
                <asp:Label ID="lblQ010" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q10" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>11
            </td>
            <td>
                <asp:Label ID="lblQ011" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q11" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>12
            </td>
            <td>
                <asp:Label ID="lblQ012" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q12" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>13
            </td>
            <td>
                <asp:Label ID="lblQ013" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q13" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>14
            </td>
            <td>
                <asp:Label ID="lblQ014" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q14" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>15
            </td>
            <td>
                <asp:Label ID="lblQ015" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q15" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>16
            </td>
            <td>
                <asp:Label ID="lblQ016" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q16" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>17
            </td>
            <td>
                <asp:Label ID="lblQ017" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q17" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>18
            </td>
            <td>
                <asp:Label ID="lblQ018" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q18" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>19
            </td>
            <td>
                <asp:Label ID="lblQ019" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q19" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="alt">
            <td>20
            </td>
            <td>
                <asp:Label ID="lblQ020" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="Q20" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Table" CssClass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblNext" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
