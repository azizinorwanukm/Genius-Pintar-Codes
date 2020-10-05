<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="addClass.aspx.vb" Inherits="permatapintar.addClass" %>

<%@ Register Src="../commoncontrol/ppcs_class_create.ascx" TagName="ppcs_class_create"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_class_list.ascx" TagName="ppcs_class_list"
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
    <uc1:ppcs_class_create ID="ppcs_class_create1" runat="server" />
    <br />
    <uc2:ppcs_class_list ID="ppcs_class_list1" runat="server" />
    &nbsp;
</asp:Content>
