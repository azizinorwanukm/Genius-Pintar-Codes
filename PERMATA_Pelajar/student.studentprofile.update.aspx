<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="student.studentprofile.update.aspx.vb" Inherits="permatapintar.student_studentprofile_update" %>

<%@ Register src="commoncontrol/studentprofile_update_mykad.ascx" tagname="studentprofile_update_mykad" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_update_mykad ID="studentprofile_update_mykad1" 
        runat="server" />
</asp:Content>
