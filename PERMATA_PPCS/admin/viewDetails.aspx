<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="viewDetails.aspx.vb" Inherits="permatapintar.viewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="4">
                Kemaskini Maklumat Pengguna
            </td>
        </tr>
        <tr class="fbform_header">
            <td colspan="4">
                Butir-butir Akaun
            </td>
        </tr>
        <tr>
            <td>
                User Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                User Year:
            </td>
            <td>
                <asp:DropDownList ID="ddlUsersYear" runat="server" AutoPostBack="false" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                Sessi PPCS:
            </td>
            <td>
                <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                *Nama penuh:
            </td>
            <td>
                <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Login ID(E-Mail):
            </td>
            <td>
                <asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Nombor IC:
            </td>
            <td>
                <asp:TextBox ID="txtICNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Kata Laluan:
            </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *No. Telefon:
            </td>
            <td>
                <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Mengesahkan kata laluan:
            </td>
            <td>
                <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                *Alamat:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="SingleLine" Width="650px" MaxLength="500"></asp:TextBox>
            </td>
        </tr>
        <tr style="text-align: left">
            <td>
                &nbsp;
            </td>
            <td colspan="3" class="fbaside_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
                &nbsp; &nbsp;<asp:Button ID="btndelete" runat="server" Text=" Hapus " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Label ID="lblUsersYear" runat="server" Text=""></asp:Label>|
    <asp:Label ID="lblPPCSDate" runat="server" Text=""></asp:Label>|
    <asp:Label ID="lblLoginID" runat="server" Text=""></asp:Label>
</asp:Content>
