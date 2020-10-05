<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcsusers_update.ascx.vb" Inherits="permatapintar.ppcsusers_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Kemaskini Maklumat Pengguna 
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            *Nama penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
        <td>
            *Login ID(E-Mail):
        </td>
        <td>
            <asp:Label ID="txtLoginID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            <asp:TextBox ID="txtOldLoginID" runat="server" Width="5px" Visible="false"></asp:TextBox>
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
        <td>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="4" Width="250px"
                MaxLength="254"></asp:TextBox>
        </td>
        <td style="vertical-align: top;">
            User Type:
        </td>
        <td style="vertical-align: top;">
            <asp:Label ID="txtUserType" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
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
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
</table>
