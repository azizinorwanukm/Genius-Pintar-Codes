<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_schoolcity_update.ascx.vb" Inherits="permatapintar.schoolprofile_schoolcity_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Kemaskini Bandar: Pilih Negeri dan Bandar
        </td>
    </tr>
    <tr>
        <td>
            Negeri:&nbsp;
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="150px">
            </asp:DropDownList>
            Bandar:&nbsp;
            <asp:DropDownList ID="ddlSchoolCity" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
            &nbsp;[Berdasarkan bandar yang dimasukkan]
        </td>
    </tr>
    <tr>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSchoolcity_update" runat="server" Text="Kemaskini Nama Bandar" CssClass="fbbutton" />&nbsp;
            Nama Bandar Sebenar:&nbsp;<asp:TextBox ID="txtSchoolCity" runat="server" Width="350px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>


