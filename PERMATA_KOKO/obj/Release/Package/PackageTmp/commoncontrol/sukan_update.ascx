<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="sukan_update.ascx.vb" Inherits="permatapintar.sukan_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Sukan
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Sukan & Permainan:
        </td>
        <td>
            <asp:TextBox ID="txtSukan" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbform_sap">&nbsp;</td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Sukan & Permainan</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblSukanOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
