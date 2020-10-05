<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_create.ascx.vb"
    Inherits="permatapintar.pusatujian_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Pusat Ujian Baru
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Nama Pusat
        </td>
        <td>
            :<asp:TextBox ID="txtPusatName" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Alamat
        </td>
        <td>
            :<asp:TextBox ID="txtPusatAddress" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:TextBox ID="txtPusatPostcode" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:TextBox ID="txtPusatCity" runat="server" Width="450px" MaxLength="150"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>
            Jenis Pusat
        </td>
        <td>
            :<asp:TextBox ID="txtPusatType" runat="server" Width="450px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            PPD
        </td>
        <td>
            :<asp:TextBox ID="txtPusatPPD" runat="server" Width="450px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Tel#
        </td>
        <td>
            :<asp:TextBox ID="txtPusatNoTel" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Fax#
        </td>
        <td>
            :<asp:TextBox ID="txtPusatNoFax" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:TextBox ID="txtPusatEmail" runat="server" Width="450px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Tahun Ujian
        </td>
        <td>
            :<asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>
            Jumlah Lab
        </td>
        <td>
            :<asp:TextBox ID="txtPusatJumlahLab" runat="server" Width="200px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Jumlah Komputer
        </td>
        <td>
            :<asp:TextBox ID="txtPusatJumlahKomp" runat="server" Width="200px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnCreate" runat="server" Text="Tambah" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
