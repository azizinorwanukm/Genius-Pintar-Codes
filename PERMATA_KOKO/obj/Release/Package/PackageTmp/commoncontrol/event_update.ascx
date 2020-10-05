<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="event_update.ascx.vb" Inherits="permatapintar.event_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Acara Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Tarikh:
        </td>
        <td>
            <asp:TextBox ID="txtEventDate" runat="server" Width="150px" MaxLength="250"></asp:TextBox>*&nbsp;
            <asp:ImageButton ID="btnDate" runat="server" ImageUrl="~/img/department-store-emoticon.png" AlternateText=".." Width="15" Height="15" />
            <asp:Calendar ID="calEventDate" runat="server" Visible="false" BackColor="White"></asp:Calendar>
        </td>
    </tr>
    <tr>
        <td>Kokurikulum:
        </td>
        <td>
            <asp:DropDownList ID="ddlKokurikulum" runat="server" AutoPostBack="false" Width="450px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>Tajuk:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Agenda:
        </td>
        <td>
            <asp:TextBox ID="txtAgenda" runat="server" Width="450px" TextMode="MultiLine" Rows="10"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Acara</asp:LinkButton>
        </td>
    </tr>

</table>
<asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>