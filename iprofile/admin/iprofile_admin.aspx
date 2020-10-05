<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="admin.Master" CodeBehind="iprofile_admin.aspx.vb" Inherits="permatapintar.iprofile_admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tablelogin" border="0px">
        <tr>
            <td style="width: 30px;">&nbsp;
            </td>
            <td valign="middle">
                <h1>Araken I-PROFILE</h1>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td valign="middle" style="text-align: right;">&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <h2>SYSTEM ADMIN</h2>

            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left">
                <asp:Label ID="lbllogin_id" runat="server" Text="TEL#"></asp:Label>:
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMYKAD" Width="300px" MaxLength="20" runat="server" Height="25px" Font-Bold="true" Font-Size="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="FULL NAME"></asp:Label>:
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtFullname" Width="300px" MaxLength="20" runat="server" Height="25px" Font-Bold="true" Font-Size="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="EXAM YEAR"></asp:Label>:
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtExamYear" Width="300px" MaxLength="20" runat="server" Height="25px" Font-Bold="true" Font-Size="16px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Create New " CssClass="mybutton" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkUserlist" runat="server">User List</asp:LinkButton>
                &nbsp;<asp:Label ID="lblSelectedLang" runat="server" Text=""></asp:Label>&nbsp;
                <table width="100%" style="border: 0px none;">
                    <tr>
                        <td></td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblStudentID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentFullname" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
