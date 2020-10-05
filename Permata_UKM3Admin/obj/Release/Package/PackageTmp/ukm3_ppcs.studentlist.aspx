<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_ppcs.studentlist.aspx.vb" Inherits="permatapintar.ukm3_ppcs_studentlist" %>

<%@ Register Src="~/commoncontrol/student_searchPpcs.ascx" TagPrefix="uc1" TagName="student_searchPpcs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_searchPpcs runat="server" id="student_searchPpcs" />
</asp:Content>
