<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_update_koko.ascx.vb" Inherits="permatapintar.instruktor_update_koko" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Instruktor
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="400px" MaxLength="250" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>No. Telefon:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Email:
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat:
        </td>
        <td>
            <asp:TextBox ID="txtAddress1" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;"></td>
        <td>
            <asp:TextBox ID="txtAddress2" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtPostcode" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlState" AutoPostBack="false" runat="server" Width="400px" Enabled="false" CssClass="fbreadonly">
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
            <asp:TextBox ID="txtLoginID" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="400px" MaxLength="15" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
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
            <asp:TextBox ID="txtBankName" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Akaun#:
        </td>
        <td>
            <asp:TextBox ID="txtAcctNo" runat="server" Width="400px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kokurikulum
        </td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td>
            <asp:TextBox ID="txtTahun" runat="server" Width="400px" MaxLength="250" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;</td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kelas:
        </td>
        <td>
            <asp:DropDownList ID="ddlKelas" AutoPostBack="true" runat="server" Width="400px" Enabled="false" CssClass="fbreadonly">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Badan Beruniform:
        </td>
        <td>
            <asp:DropDownList ID="ddlUniform" AutoPostBack="false" runat="server" Width="400px">
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkKetuaUniform" Text="Ketua Badan Beruniform" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kelab & Persatuan:
        </td>
        <td>
            <asp:DropDownList ID="ddlPersatuan" AutoPostBack="false" runat="server" Width="400px">
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkKetuaPersatuan" Text="Ketua Kelab & Persatuan" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Sukan & Permainan:
        </td>
        <td>
            <asp:DropDownList ID="ddlSukan" AutoPostBack="false" runat="server" Width="400px">
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkKetuaSukan" Text="Ketua Sukan & Permainan" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Rumah Sukan:
        </td>
        <td>
            <asp:DropDownList ID="ddlRumahsukan" AutoPostBack="false" runat="server" Width="400px">
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkKetuaRumahsukan" Text="Ketua Rumah Sukan" runat="server" />
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
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Instruktor</asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID="lblMYKADOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>