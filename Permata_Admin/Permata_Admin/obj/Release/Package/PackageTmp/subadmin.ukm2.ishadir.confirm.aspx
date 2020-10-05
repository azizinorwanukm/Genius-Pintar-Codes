<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.ukm2.ishadir.confirm.aspx.vb" Inherits="permatapintar.subadmin_ukm2_ishadir_confirm" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;
    <uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                Tahun Ujian:&nbsp;<asp:DropDownList ID="ddlExamYear" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnHadir" runat="server" Text="Hadir" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnTidakHadir" runat="server" Text="Tidak Hadir" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnResetAll" runat="server" Text="Reset UKM2" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnResetExamStart" runat="server" Text="Reset ExamStart" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Reset ExamStart: ExamStart=NULL,ExamEnd=NULL,Status=NULL,IsHadir='Y'.<br />
                Markah tidak dikemaskini.<br />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
</asp:Content>
