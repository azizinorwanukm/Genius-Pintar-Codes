<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/mara.Master" CodeBehind="mara.ukm1.state.student.list.aspx.vb" Inherits="permatapintar.mara_ukm1_state_student_list" %>
<%@ Register src="commoncontrol/ukm1_state_student_list.ascx" tagname="ukm1_state_student_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_state_student_list ID="ukm1_state_student_list1" runat="server" />
</asp:Content>
