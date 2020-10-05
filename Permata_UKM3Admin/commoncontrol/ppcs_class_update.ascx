<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_class_update.ascx.vb" Inherits="permatapintar.ppcs_class_update1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini atau Hapus Kelas
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            Sessi PPCS:
        </td>
        <td>
            <asp:TextBox ID="txtPPCSDate" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Kod Kursus:
        </td>
        <td>
            <asp:TextBox ID="txtCourseCode" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Nama Kursus (BM):
        </td>
        <td>
            <asp:TextBox ID="txtCourseNameBM" runat="server" Width="300" MaxLength="150" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
            Kod Kelas:
        </td>
        <td>
            <asp:TextBox ID="txtClassCode" runat="server" Width="150px" MaxLength="254"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
            Nama Kelas:
        </td>
        <td>
            <asp:TextBox ID="txtClassNameBM" runat="server" Width="300px" MaxLength="254"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
            Tempat:
        </td>
        <td>
            <asp:DropDownList ID="ddlTempat" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td class="column_width">
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
            &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapus " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
        </td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
    