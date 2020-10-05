<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="sekolah.aspx.vb" Inherits="UKM_SEMAKAN.sekolah" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>
        Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">
                SEMAKAN KELAYAKAN KE SEKOLAH PERMATAPINTAR
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSemak" runat="server" Text=" Semak " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="Mesej sistem..." ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
