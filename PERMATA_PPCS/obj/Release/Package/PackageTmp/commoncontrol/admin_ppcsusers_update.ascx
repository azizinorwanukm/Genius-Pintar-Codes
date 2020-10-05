<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_ppcsusers_update.ascx.vb"
    Inherits="permatapintar.admin_ppcsusers_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini Maklumat Petugas PPCS
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            *Login ID(E-Mail):
        </td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="254"></asp:TextBox>
            
        </td>
    </tr>
    <tr>
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
            *Mengesahkan kata laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Nama penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            *Nombor IC:
        </td>
        <td>
            <asp:TextBox ID="txtICNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            *No. Telefon:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            *Alamat:
        </td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" Width="350px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtPostcode" runat="server" Width="100px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
           Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="350px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Negeri:
        </td>
        <td>
            <asp:TextBox ID="txtState" runat="server" Width="350px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr style="text-align: left">
        <td>
            &nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" /> |
            <asp:LinkButton ID="lnkppcsuserlist" runat="server">Senarai Petugas</asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID="lblLoginIDOld" runat="server" Text="" Visible="false"></asp:Label>
