<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="esurvey.page06.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page06" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="lblInstruction" runat="server" Text=""></asp:Label>
    </p>

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
        <tr class="alt">
            <td></td>
            <td>
                <asp:Label ID="lblQ000" runat="server" Text=""></asp:Label></td>
            <td></td>
        </tr>
        <tr>
            <td>75
            </td>
            <td>
                <asp:Image ID="img01" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape1.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
            <td>76
            </td>
            <td>
                <asp:Image ID="img02" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape2.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
            <td>77
            </td>
            <td>
                <asp:Image ID="img03" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape3.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
            <td>78
            </td>
            <td>
                <asp:Image ID="img04" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape4.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
            <td>79
            </td>
            <td>
                <asp:Image ID="img05" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape5.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
            <td>80
            </td>
            <td>
                <asp:Image ID="img06" runat="server" Width="150px" Height="120px" ImageUrl="img/Landscape6.jpg" ImageAlign="AbsBottom"></asp:Image><br />
                <br />
                <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
            </td>
            <td style="vertical-align: bottom;">
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
