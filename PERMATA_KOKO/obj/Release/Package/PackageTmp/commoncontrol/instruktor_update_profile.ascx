<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_update_profile.ascx.vb" Inherits="permatapintar.instruktor_update_profile" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Instruktor
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="400px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="400px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>No. Telefon:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="400px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Email:
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="400px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat:
        </td>
        <td>
            <asp:TextBox ID="txtAddress1" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;"></td>
        <td>
            <asp:TextBox ID="txtAddress2" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtPostcode" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlState" AutoPostBack="false" runat="server" Width="400px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kewangan
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Nama Bank:
        </td>
        <td>
            <asp:TextBox ID="txtBankName" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Akaun#:
        </td>
        <td>
            <asp:TextBox ID="txtAcctNo" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kokurikulum
        </td>
    </tr>
     <tr>
        <td>Tahun:</td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
    </tr>
     <tr>
        <td>Kelas:</td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td style="vertical-align: top;">Badan Beruniform:
        </td>
        <td>
            <asp:Label ID="lblUniform" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kelab & Persatuan:
        </td>
        <td>
            <asp:Label ID="lblPersatuan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Sukan & Permainan:
        </td>
        <td>
            <asp:Label ID="lblSukan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Rumah Sukan:
        </td>
        <td>
            <asp:Label ID="lblRumahsukan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblMYKADOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
