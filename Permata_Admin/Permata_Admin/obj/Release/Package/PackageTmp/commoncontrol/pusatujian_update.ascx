<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_update.ascx.vb"
    Inherits="permatapintar.pusatujian_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Pusat Ujian
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pusat
        </td>
        <td>:<asp:TextBox ID="txtPusatName" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Alamat
        </td>
        <td>:<asp:TextBox ID="txtPusatAddress" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Poskod
        </td>
        <td>:<asp:TextBox ID="txtPusatPostcode" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Bandar
        </td>
        <td>:<asp:TextBox ID="txtPusatCity" runat="server" Width="450px" MaxLength="150"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
        </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>Jenis Pusat
        </td>
        <td>:<asp:TextBox ID="txtPusatType" runat="server" Width="450px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>PPD
        </td>
        <td>:<asp:TextBox ID="txtPusatPPD" runat="server" Width="450px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:TextBox ID="txtPusatNoTel" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Fax#
        </td>
        <td>:<asp:TextBox ID="txtPusatNoFax" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Email
        </td>
        <td>:<asp:TextBox ID="txtPusatEmail" runat="server" Width="450px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tahun Ujian
        </td>
        <td>:<asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="150px">
        </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>Jum. Lab
        </td>
        <td>:<asp:TextBox ID="txtPusatJumlahLab" runat="server" Width="150px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Jum.Komputer
        </td>
        <td>:<asp:TextBox ID="txtPusatJumlahKomp" runat="server" Width="150px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Keterangan
        </td>
        <td style="vertical-align: top;">:<asp:TextBox ID="txtKomen" runat="server" Width="450px" MaxLength="500" TextMode="MultiLine" Rows="10"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:LinkButton ID="lnkPusatUjian" runat="server">Senarai Pusat Ujian</asp:LinkButton>
        </td>
    </tr>
</table>
