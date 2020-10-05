<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/popup.Master"
    CodeBehind="studentprofile.complete.print.aspx.vb" Inherits="permatapintar.studentprofile_complete_print" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc4:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    &nbsp;<table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
