<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="login.aspx.vb" Inherits="PERMATA_EQTest.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #mycustomtable
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            width: 50%;
            border-collapse: collapse;
        }
        #mycustomtable td, #mycustomtable th
        {
            font-size: 1em;
            border: 1px solid #98bf21;
            padding: 3px 7px 2px 7px;
        }
        #mycustomtable th
        {
            font-size: 1.1em;
            text-align: left;
            padding-top: 5px;
            padding-bottom: 4px;
            background-color: #A7C942;
            color: #ffffff;
        }
        #mycustomtable tr.alt td
        {
            color: #000000;
            background-color: #EAF2D3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="mycustomtable">
        <tr>
            <th colspan="2">
                 PERMATApintar Emotional Quotient Test
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Alumni ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAlumniID" runat="server" Text="" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Log Masuk" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblLoginMsg" runat="server" Text="Log masuk menggunakan Alumni ID "></asp:Label>
                <asp:Label ID="lblSurveyID" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</asp:Content>
