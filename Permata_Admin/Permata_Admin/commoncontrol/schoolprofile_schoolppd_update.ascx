<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_schoolppd_update.ascx.vb" Inherits="permatapintar.schoolprofile_schoolppd_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList></td>

    </tr>
    <tr>
        <td class="fbtd_left">PPD:</td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>&nbsp;[Berdasarkan PPD yang dimasukkan]</td>
    </tr>
    <tr>
        <td>Nama PPD:</td>
        <td>
            <asp:TextBox ID="txtSchoolPPD" runat="server" Width="250px" MaxLength="150"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSchoolPPD_update" runat="server" Text="Kemaskini Nama PPD" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>
