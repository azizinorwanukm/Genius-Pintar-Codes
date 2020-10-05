<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_schoolcity_update.ascx.vb" Inherits="permatapintar.schoolprofile_schoolcity_update" %>

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
        <td class="fbtd_left">Bandar:</td>
        <td>
            <asp:DropDownList ID="ddlSchoolCity" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>&nbsp;[Berdasarkan bandar yang dimasukkan]</td>
    </tr>
    <tr>
        <td>Nama Bandar:</td>
        <td><asp:TextBox ID="txtSchoolCity" runat="server" Width="250px" MaxLength="150"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSchoolcity_update" runat="server" Text="Kemaskini Nama Bandar" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>


