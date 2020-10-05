<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupengajar/main.Master"
    CodeBehind="student.profile.view.aspx.vb" Inherits="permatapintar.student_profile_view5" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view" TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list" TagPrefix="uc4" %>
<%--<%@ Register Src="../commoncontrol/studentschool_view.ascx" TagName="studentschool_view" TagPrefix="uc2" %>--%>
<%@ Register Src="../commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list" TagPrefix="uc5" %>
<%@ Register Src="../commoncontrol/ukm2_history_list.ascx" TagName="ukm2_history_list" TagPrefix="uc6" %>
<%@ Register Src="../commoncontrol/studentschool_list.ascx" TagName="studentschool_list" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />

    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    <%--&nbsp;<uc2:studentschool_view ID="studentschool_view1" runat="server" />--%>
    &nbsp;<uc7:studentschool_list ID="studentschool_list1" runat="server" />
    &nbsp;<uc5:ukm1_history_list ID="ukm1_history_list1" runat="server" />
    &nbsp;<uc6:ukm2_history_list ID="ukm2_history_list1" runat="server" />
    &nbsp;<uc4:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    &nbsp;
</asp:Content>