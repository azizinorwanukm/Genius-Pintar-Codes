<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.ppcsstatus.create.aspx.vb" Inherits="permatapintar.ppcs_ppcsstatus_create" %>
<%@ Register src="../commoncontrol/master_PPCSStatus_create.ascx" tagname="master_PPCSStatus_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>Tambah PPCS Status
                
            </td>
        </tr>
    </table>
   
    <uc1:master_PPCSStatus_create ID="master_PPCSStatus_create1" runat="server" />
</asp:Content>
