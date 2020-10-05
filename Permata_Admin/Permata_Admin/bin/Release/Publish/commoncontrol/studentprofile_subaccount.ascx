<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_subaccount.ascx.vb"
    Inherits="permatapintar.studentprofile_subaccount" %>

<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Maklumat Pelajar 
        </td>
        <td style="text-align: right;"><asp:LinkButton ID="lnkDelete" runat="server">Delete Sub-Account</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>MYKAD/MYKID#
        </td>
        <td>:<asp:Label ID="lblMYKAD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Pelajar
        </td>
        <td>:<asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>AlumniID
        </td>
        <td>:<asp:Label ID="lblAlumniID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
     <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
