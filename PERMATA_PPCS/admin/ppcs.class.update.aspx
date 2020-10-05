<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ppcs.class.update.aspx.vb" Inherits="permatapintar.ppcs_class_update" %>

<%@ Register Src="../commoncontrol/ppcs_class_list.ascx" TagName="ppcs_class_list"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_class_update.ascx" TagName="ppcs_class_update"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Am>Pengurusan Kelas
            </td>
        </tr>
    </table>
    <uc2:ppcs_class_update ID="ppcs_class_update1" runat="server" />
    &nbsp;
    <uc1:ppcs_class_list ID="ppcs_class_list1" runat="server" />
</asp:Content>
