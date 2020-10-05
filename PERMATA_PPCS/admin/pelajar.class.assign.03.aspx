<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.class.assign.03.aspx.vb" Inherits="permatapintar.pelajar_class_assign_03" %>

<%@ Register Src="../commoncontrol/ppcs_class_view.ascx" TagName="ppcs_class_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_class_assign.ascx" TagName="ppcs_class_assign"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pelajar>Menentukan Kelas
            </td>
        </tr>
    </table>
    <uc1:ppcs_class_view ID="ppcs_class_view1" runat="server" />
    &nbsp;
    <uc2:ppcs_class_assign ID="ppcs_class_assign1" runat="server" />
</asp:Content>
