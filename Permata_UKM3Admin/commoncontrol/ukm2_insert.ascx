<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_insert.ascx.vb" Inherits="permatapintar.ukm2_insert" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Restore UKM2
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun Ujian
        </td>
        <td>:<asp:DropDownList ID="ddlExamYear" runat="server" Width="150px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Exam Start
        </td>
        <td>:<asp:TextBox ID="txtExamStart" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Exam End
        </td>
        <td>:<asp:TextBox ID="txtExamEnd" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Last Page
        </td>
        <td>:<asp:TextBox ID="txtLastPage" runat="server" Width="150px" MaxLength="150" Text="ukm2.end.aspx"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Status
        </td>
        <td>:<asp:TextBox ID="txtStatus" runat="server" Width="150px" MaxLength="150" Text="DONE"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>VCI
        </td>
        <td>:<asp:TextBox ID="txtVCI" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>PRI
        </td>
        <td>:<asp:TextBox ID="txtPRI" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>WMI
        </td>
        <td>:<asp:TextBox ID="txtWMI" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>PSI
        </td>
        <td>:<asp:TextBox ID="txtPSI" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Total Score
        </td>
        <td>:<asp:TextBox ID="txtTotalScore" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Total %
        </td>
        <td>:<asp:TextBox ID="txtTotalPercentage" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnInsert" runat="server" Text="Restore" CssClass="fbbutton" />&nbsp;|&nbsp;<asp:LinkButton
                ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>

</table>
