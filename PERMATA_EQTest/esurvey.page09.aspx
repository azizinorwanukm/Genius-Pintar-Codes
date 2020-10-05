<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="esurvey.page09.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page09" %>
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
                <asp:Label ID="lblDomain01" runat="server" Text=""></asp:Label>
            </th>
            <th>
                <asp:Label ID="lblRating" runat="server" Text=""></asp:Label>
            </th>
        </tr>
        <tr>
            <td>121
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
            <td>122
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
            <td>123
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
            <td>124
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
            <td>125
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
            <td>126
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
            <td>127
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
            <td>128
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
            <td>129
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
            <td>130
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
            <td>131
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
            <td>132
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
            <td>133
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
            <td>134
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
            <td>135
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
            <td>136
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
            <td>137
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
            <td>138
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
            <td>139
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
            <td>140
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
                <asp:Button ID="btnPrev" runat="server" Text=" << Prev" CssClass="fbbutton" />
                &nbsp;<asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
