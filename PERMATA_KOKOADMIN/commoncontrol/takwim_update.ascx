<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="takwim_update.ascx.vb" Inherits="permatapintar.takwim_update" %>

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
        <td >Kategori:
        </td>
        <td>
            <asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="false" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td >Tarikh:
        </td>
        <td>
            <asp:TextBox ID="txtTarikh" runat="server" Width="150px" MaxLength="250"></asp:TextBox>*&nbsp;
            <asp:ImageButton ID="btnDate" runat="server" ImageUrl="~/img/department-store-emoticon.png" AlternateText=".."  Width="15" Height="15"/>
            <asp:Calendar ID="calTarikh" runat="server" Visible="false" BackColor="White"></asp:Calendar>
        </td>
    </tr>
    <tr>
        <td >Masa:
        </td>
        <td>
            <asp:TextBox ID="txtMasa" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td >Tempat:
        </td>
        <td>
            <asp:TextBox ID="txtTempat" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td >Tajuk:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="450px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width:20%; vertical-align:top;">Catatan:
        </td>
        <td>
            <asp:TextBox ID="txtCatatan" runat="server" Width="450px" TextMode="MultiLine" Rows="10"></asp:TextBox>&nbsp;
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
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Takwim</asp:LinkButton>
        </td>
    </tr>
    
</table>
<asp:Label ID="lblTarikh" runat="server" Text="" ForeColor="red"></asp:Label>

