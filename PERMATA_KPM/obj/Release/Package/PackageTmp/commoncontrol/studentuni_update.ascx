<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentuni_update.ascx.vb" Inherits="permatapintar.studentuni_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Universiti
        </td>
        <td style="text-align: right;">
            
        </td>
    </tr>
    
    <tr>
        <td style="width: 20%;">
            Nama Universiti
        </td>
        <td>
            :<asp:TextBox ID="txtUniName" runat="server" Width="300px" MaxLength="250" Style="text-transform: uppercase;"></asp:TextBox>

        </td>
    </tr>
    <tr>
        <td>
            Negara
        </td>
        <td>
            :<asp:DropDownList ID="ddlUniCountry" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td>
            Bidang Pengajian
        </td>
        <td>
            :<asp:TextBox ID="txtUniCourse" runat="server" Width="300px" MaxLength="250" Style="text-transform: uppercase;"></asp:TextBox>
        </td>
    </tr>
   <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:LinkButton ID="lnkStudentProfileView" runat="server">View Student Profile</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>