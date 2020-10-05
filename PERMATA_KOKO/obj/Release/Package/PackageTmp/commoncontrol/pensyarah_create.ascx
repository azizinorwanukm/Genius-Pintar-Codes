<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pensyarah_create.ascx.vb" Inherits="permatapintar.pensyarah_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Pensyarah Baru
        </td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="400px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="400px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="400px" MaxLength="254"></asp:TextBox>*&nbsp;
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

    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp; &nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
