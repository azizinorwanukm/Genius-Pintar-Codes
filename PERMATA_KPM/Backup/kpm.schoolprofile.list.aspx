<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="kpm.schoolprofile.list.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_list" %>

<%@ Register Src="commoncontrol/schoolprofile_list.ascx" TagName="schoolprofile_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_list id="schoolprofile_list1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Sekolah Baru" CssClass="fbbutton"/>&nbsp;
                <asp:Button ID="btnUpdate_Schoolcity" runat="server" Text="Kemaskini Bandar" CssClass="fbbutton"/>&nbsp;
                <asp:Label ID="lblNewSchool" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table> 
</asp:Content>
