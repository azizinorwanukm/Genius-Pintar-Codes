<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="studentprofile.complete.view.aspx.vb" Inherits="permatapintar.studentprofile_complete_view" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register src="commoncontrol/studentschool_view.ascx" tagname="studentschool_view" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:studentschool_view ID="studentschool_view1" runat="server" />
&nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" /> &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <a href="studentprofile.complete.print.aspx?studentid=<%=Request.QueryString("studentid")%>" target="_blank">Format Cetakan</a>
            </td>
        </tr>
    </table>
</asp:Content>
