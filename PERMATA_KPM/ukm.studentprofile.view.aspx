<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master"
    CodeBehind="ukm.studentprofile.view.aspx.vb" Inherits="permatapintar.ukm_studentprofile_view" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<%@ Register src="commoncontrol/ukm2_history_list.ascx" tagname="ukm2_history_list" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Paparan Maklumat Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc2:ukm2_history_list ID="ukm2_history_list1" runat="server" />
</asp:Content>
