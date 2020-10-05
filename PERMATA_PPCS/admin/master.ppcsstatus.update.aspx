<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="master.ppcsstatus.update.aspx.vb" Inherits="permatapintar.master_ppcsstatus_update" %>

<%@ Register Src="../commoncontrol/master_PPCSStatus_update.ascx" TagName="master_PPCSStatus_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>Kemaskini PPCS Status
                
            </td>
        </tr>
    </table>

    <uc1:master_PPCSStatus_update ID="master_PPCSStatus_update1" runat="server" />
</asp:Content>
