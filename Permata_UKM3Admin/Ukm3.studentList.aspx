<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Ukm3.studentList.aspx.vb" Inherits="permatapintar.Ukm3_studentList" %>

<%@ Register Src="~/commoncontrol/studentprofile_search.ascx" TagPrefix="uc1" TagName="studentprofile_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Maklumat Pelajar>Carian Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_search runat="server" ID="studentprofile_search" />



</asp:Content>
