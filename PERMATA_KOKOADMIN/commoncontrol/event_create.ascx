<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="event_create.ascx.vb" Inherits="permatapintar.event_create" %>

<script type="text/javascript">
    $(function () {
        $('[id*=ddlKumpulan]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Acara Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text="" ForeColor="Red"></asp:Label>
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
            <asp:DropDownList ID="ddlKokoID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlKokoID_SelectedIndexChanged" Width="450px">
            </asp:DropDownList>*
        </td>
    </tr>

    <tr>
        <td>Kumpulan:
        </td>
        <td>
            <asp:CheckBoxList ID="ddlKumpulan" runat="server" AutoPostBack="false" Width="450px" Height="100px">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td>Tajuk:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>

    <tr>
        <td style="vertical-align: top;">Agenda:
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
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Acara</asp:LinkButton>
        </td>
    </tr>

</table>
<asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>