<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="studentprofile.view.aspx.vb" Inherits="permatapintar.studentprofile_view" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view" TagPrefix="uc3" %>

<%@ Register src="../commoncontrol/StudentUni_list.ascx" tagname="StudentUni_list" tagprefix="uc2" %>
<%@ Register Src="../commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list" TagPrefix="uc5" %>
<%@ Register Src="../commoncontrol/ukm2_history_list.ascx" TagName="ukm2_history_list" TagPrefix="uc6" %>
<%@ Register Src="../commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list" TagPrefix="uc7" %>
<%@ Register src="../commoncontrol/studentschool_list.ascx" tagname="studentschool_list" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_list ID="studentschool_list1" runat="server" />
    &nbsp;<uc2:StudentUni_list ID="StudentUni_list1" runat="server" />

    <%--&nbsp;<h3>UKM3 History List</h3>
    <br />
    Ujian Kreativiti<br />
    Ujian EQ<br />
    Ujian Tekanan<br />
    Etika, Moral, Disiplin
    <br />
    Kemahiran Komunikasi<br />
    Kemahiran Kepimpinan<br />
    Temuduga<br />
    Matematik<br />
    Sains<br />
    Minat Sains<br />
    <h3>Kolej PERMATApintar Negara History List</h3>--%>
    &nbsp;<uc5:ukm1_history_list ID="ukm1_history_list1" runat="server" />
    &nbsp;<uc6:ukm2_history_list ID="ukm2_history_list1" runat="server" />
    &nbsp;<uc7:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    <p>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </p>
</asp:Content>
