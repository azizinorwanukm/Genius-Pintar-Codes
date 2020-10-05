<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.ppcsstatus.list.aspx.vb" Inherits="permatapintar.ppcs_ppcsstatus_list" %>

<%@ Register Src="../commoncontrol/master_PPCSStatus_list.ascx" TagName="master_PPCSStatus_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>PPCS Status
                
            </td>
        </tr>
    </table>
    <uc1:master_PPCSStatus_list ID="master_PPCSStatus_list1" runat="server" />
    &nbsp;
</asp:Content>
