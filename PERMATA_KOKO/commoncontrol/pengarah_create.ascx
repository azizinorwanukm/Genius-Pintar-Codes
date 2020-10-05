<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_create.ascx.vb" Inherits="permatapintar.pengarah_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Pengarah Baru
        </td>
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
        <td colspan="2">Maklumat Login
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Login ID:
        </td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="400px" MaxLength="254"></asp:TextBox>
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
            <asp:TextBox ID="txtTahun" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox></td>
    </tr>
     <tr>
        <td></td>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnCreate" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Pengarah</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>