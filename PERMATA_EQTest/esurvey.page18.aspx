<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="esurvey.page18.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:label id="lblInstruction" runat="server" text=""></asp:label>
    </p>

    <asp:label id="lblMsgTop" runat="server" text="" forecolor="Red"></asp:label>
    <table id="mycustomtable">
        <tr>
            <th>#
            </th>
            <th>
                <asp:label id="lblDomain01" runat="server" text=""></asp:label>
            </th>
            <th>
                <asp:label id="lblRating" runat="server" text=""></asp:label>
            </th>
        </tr>
        <tr class="alt">
            <td></td>
            <td>
                <asp:label id="lblQ000" runat="server" text=""></asp:label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>182
            </td>
            <td>
                <applet name="mymaze" code="Maze.class" codebase="MazeJava" width="400" height="300"
                                alt="A Java Applet.  If you can read this, your browser doesn't support Java"
                                id="Applet1">
                                <param name="tempurl" value="" />
                                <param name="bgc_red" value="255" />
                                <param name="bgc_green" value="255" />
                                <param name="num_rows" value="11" />
                                <param name="num_columns" value="11" />
                                <param name="bgc_blue" value="255" />
                            </applet>
                <br />
                <br />
                <asp:label id="lblQ001" runat="server" text=""></asp:label>
            </td>
            <td style="vertical-align: bottom;">
                <asp:radiobuttonlist id="Q1" runat="server" enableviewstate="False" repeatdirection="Horizontal"
                    repeatlayout="Table" cssclass="fbradio">

                    <asp:ListItem Value="1">&nbsp;1&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;2&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;3&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;4&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;5&nbsp;</asp:ListItem>

                </asp:radiobuttonlist>
            </td>
        </tr>
        <tr class="alt">
            <td>&nbsp;
            </td>
            <td>
                <asp:label id="lblNext" runat="server" text=""></asp:label>
            </td>
            <td>
                <asp:button id="btnPrev" runat="server" text=" << Prev" cssclass="fbbutton" />
                &nbsp;<asp:button id="btnNext" runat="server" text="Next >>" cssclass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:label id="lblMsg" runat="server" text="" forecolor="Red"></asp:label>
            </td>
        </tr>
    </table>
</asp:Content>
