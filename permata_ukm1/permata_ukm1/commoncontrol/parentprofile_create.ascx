<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parentprofile_create.ascx.vb"
    Inherits="permatapintar.parentprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Bapa/Penjaga
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherFullname" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherJob" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Ibu
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherFullname" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherJob" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Keluarga
        </td>
    </tr>
    <tr>
        <td>
            Nombor Talipon:
        </td>
        <td>
            <asp:TextBox ID="txtFamilyContactNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: right;">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Maklumat Ibubapa/Penjaga"
                CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
