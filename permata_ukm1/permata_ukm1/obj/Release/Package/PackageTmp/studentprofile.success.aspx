<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="studentprofile.success.aspx.vb" Inherits="permatapintar.studentprofile_success" %>

<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc4:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;
    <uc2:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;
    <uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<div class="info">
        • Tahniah. Pendaftaran anda telah berjaya dilakukan. <a href="default.aspx">Kemaskini</a></div>
</asp:Content>
